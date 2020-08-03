/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
 ** 72070 Tuebingen, Germany. All rights reserved.
 ** 
 ** yFiles demo files exhibit yFiles.NET functionalities. Any redistribution
 ** of demo files in source code or binary form, with or without
 ** modification, is not permitted.
 ** 
 ** Owners of a valid software license for a yFiles.NET version that this
 ** demo is shipped with are allowed to use the demo source code as basis
 ** for their own yFiles.NET powered applications. Use of such programs is
 ** governed by the rights and conditions as set out in the yFiles.NET
 ** license agreement.
 ** 
 ** THIS SOFTWARE IS PROVIDED ''AS IS'' AND ANY EXPRESS OR IMPLIED
 ** WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 ** MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN
 ** NO EVENT SHALL yWorks BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 ** SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
 ** TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 ** PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 ** LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 ** NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 ** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 ** 
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Demo.yFiles.Layout.Tree.Configuration;
using Demo.yFiles.Option.DataBinding;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Layout.Tree;

namespace Demo.yFiles.Layout.Tree
{
  public partial class NodePlacerPanel : UserControl
  {
    private readonly IDictionary<string, NodePlacerDescriptor> levelConfigurations =
      new Dictionary<string, NodePlacerDescriptor>();

    private readonly IList<NodePlacerDescriptor> nodePlacers = new List<NodePlacerDescriptor>();
    private List<INodePlacerConfiguration> descriptorCollection;

    private TreeLayout previewLayout;
    private DefaultSelectionProvider<INodePlacerConfiguration> selectionProvider;

    public NodePlacerPanel() {
      InitializeComponent();
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.DefaultNodePlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.SimpleNodePlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.BusPlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.DoubleLinePlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.LeftRightPlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.ARNodePlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.AssistantPlacer.Name);
      nodePlacerTypeComboBox.Items.Add(NodePlacerConfigurations.None.Name);

      SetupNodePlacerOptions();
      SetupPreview();
    }

    public IList<NodePlacerDescriptor> NodePlacers {
      get { return nodePlacers; }
    }

    #region UI event handlers that connect to the outside world

    /// <summary>
    /// Raises the <see cref="ReloadConfiguration"/> event.
    /// </summary>
    protected virtual void OnReloadConfiguration(EventArgs args) {
      if (ReloadConfiguration != null) {
        ReloadConfiguration(this, args);
      }
    }

    /// <summary>
    /// Occurs when a request to reload the configuration is issued.
    /// </summary>
    public event EventHandler ReloadConfiguration;

    /// <summary>
    /// Raises the <see cref="ApplyConfiguration"/> event.
    /// </summary>
    protected virtual void OnApplyConfiguration(EventArgs args) {
      if (ApplyConfiguration != null) {
        ApplyConfiguration(this, args);
      }
    }

    /// <summary>
    /// Occurs when a request to Apply the configuration is issued.
    /// </summary>
    public event EventHandler ApplyConfiguration;

    private void OnReloadButtonClicked(object sender, EventArgs e) {
      OnReloadConfiguration(e);
      levelConfigurations.Clear();
      //Force update of current property
      SetLevel(Level);
    }

    private void OnApplyButtonClicked(object sender, EventArgs e) {
      OnApplyConfiguration(e);
    }

    #endregion

    #region Level dependency property

    private int level = 0;

    public int Level {
      get { return level; }
      set { 
        if(level != value) {
          level = CoerceLevel(value);
          OnLevelChanged(value);
        } 
      }
    }

    private int CoerceLevel(int level) {
      if (level < 0) {
        return 0;
      }
      if (level >= nodePlacers.Count) {
        return nodePlacers.Count - 1;
      }
      return level;
    }

    private void OnLevelChanged(int level) {
      SetLevel(level);
    }

    internal void SetLevel(int level) {
      levelConfigurations.Clear();
      Level = level;
      if (level < nodePlacers.Count) {
        CurrentDescriptor = nodePlacers[level];
        //Use 1-based level indices
        levelUpDown.Value = level + 1;

        layerVisualizationBorder.BackColor = CurrentBrush;
        nodePlacerTypeComboBox.SelectedItem = CurrentDescriptor.Name;
      }
    }

    #endregion

    #region CurrentDescriptor property

    private NodePlacerDescriptor currentDescriptor;

    public NodePlacerDescriptor CurrentDescriptor {
      get { return currentDescriptor; }
      set { if(currentDescriptor != value) {
        currentDescriptor = value;
        OnDescriptorChanged(value);
      }}
    }

    private void OnDescriptorChanged(NodePlacerDescriptor descriptor) {
       SetDescriptor(descriptor);
    }

    private void SetDescriptor(NodePlacerDescriptor npd) {
      nodePlacers[Level] = npd;
      if (descriptorCollection.Count == 0) {
        descriptorCollection.Add(npd.Configuration);
      } else {
        descriptorCollection[0] = npd.Configuration;
      }

      rotationGrid.Enabled = npd.Rotatable;
      nodePlacerTypeLabel.Text = npd.Name;
      nodePlacerDescriptionTextBox.Text = npd.Description;

      if (npd.Configuration == null || npd.Name == NodePlacerConfigurations.None.Name) {
        editorControl.Visible = false;
        nodeSettingsLabel.Visible = true;
      } else {
        selectionProvider.UpdatePropertyViewsNow();
        editorControl.Visible = true;
        nodeSettingsLabel.Visible = false;
      }
      UpdatePreview();
    }

    #endregion

    #region Option handler setup

    /// <summary>
    /// Configure option handler related stuff
    /// </summary>
    private void SetupNodePlacerOptions() {
      // create the EditorControl
      var editorFactory = new DefaultEditorFactory();
      nodePlacerOptionHandler = new OptionHandler("Placer Options");
      editorControl = editorFactory.CreateControl(nodePlacerOptionHandler, true, true);
      editorControl.Dock = DockStyle.Fill;
      //order is important here...
      placerOptionsBox.Controls.Add(editorControl);
      placerOptionsBox.PerformLayout();

      descriptorCollection = new List<INodePlacerConfiguration>();
      selectionProvider = new DefaultSelectionProvider<INodePlacerConfiguration>(descriptorCollection,
                                                                                 delegate { return true; })
      {
        ContextLookup = Lookups.CreateContextLookupChainLink(
          (subject, type) =>
          ((type == typeof(IPropertyMapBuilder) && (subject is INodePlacerConfiguration))
             ? new AttributeBasedPropertyMapBuilderAttribute().CreateBuilder(subject.GetType())
             : null))
      };
      //when the selection content changes, trigger this action (usually rebuild associated option handlers)
      selectionProvider.SelectedItemsChanged += SelectionProviderSelectionChanged;

      selectionProvider.PropertyItemsChanged += delegate {
        UpdatePreview();
        SelectionProviderSelectionChanged(this, null);
      };
    }


    /// <summary>
    /// Update the properties when the current node placer descriptor has changed.
    /// </summary>
    private void SelectionProviderSelectionChanged(object sender, EventArgs e) {
      //We just rebuild the option handler from scratch
      nodePlacerOptionHandler.BuildFromSelection(selectionProvider,
                                                     Lookups.CreateContextLookupChainLink(
                                                       (subject, type) =>
                                                       ((type == typeof(IOptionBuilder) && subject is INodePlacerConfiguration) ? new SortableOptionBuilder() : null)));
    }

    #endregion

    #region Preview configuration and update

    /// <summary>
    /// Creates and configures the graph that is used for the placer configuration preview.
    /// </summary>
    private void SetupPreview() {
      IGraph graph = previewControl.Graph;
      DictionaryMapper<INode, bool> assistantMap =
        graph.MapperRegistry.CreateMapper<INode, bool>(AssistantNodePlacer.AssistantNodeDpKey);
      graph.NodeDefaults.Size = new SizeD(40, 30);
      graph.NodeDefaults.Style = new ShinyPlateNodeStyle
      {
        Brush = Brushes.LightGray,
        Insets = new InsetsD(5),
        DrawShadow = false,
        Pen = Pens.Black
      };
      var rootStyle = new ShinyPlateNodeStyle
      {
        Brush = Brushes.Red,
        Insets = new InsetsD(5),
        Pen = Pens.Black,
        DrawShadow = false
      };
      var assistantStyle = new ShinyPlateNodeStyle
      {
        Brush = Brushes.LightGray,
        Insets = new InsetsD(5),
        Pen = new Pen(Brushes.Black, 1) {DashStyle = DashStyle.Dash},
        DrawShadow = false
      };
      INode root = graph.CreateNode();
      graph.SetStyle(root, rootStyle);
      INode n1 = graph.CreateNode();
      graph.SetStyle(n1, assistantStyle);
      assistantMap[n1] = true;
      INode n2 = graph.CreateNode();
      INode n3 = graph.CreateNode();
      INode n4 = graph.CreateNode();
      INode n5 = graph.CreateNode(new RectD(0, 0, 60, 30));
      graph.CreateEdge(root, n1);
      graph.CreateEdge(root, n2);
      graph.CreateEdge(root, n3);
      graph.CreateEdge(root, n4);
      graph.CreateEdge(root, n5);
      previewLayout = new TreeLayout();
    }

    ///<summary>
    /// Update the preview canvas: Apply a new layout with the current placer.
    ///</summary>
    public void UpdatePreview() {
      INodePlacer placer = CurrentDescriptor.Configuration != null
                             ? CurrentDescriptor.Configuration.CreateNodePlacer()
                             : null;
      previewLayout.DefaultNodePlacer = placer ?? new DefaultNodePlacer();
      try {
        previewControl.Graph.ApplyLayout(previewLayout);
        previewControl.FitGraphBounds();
      } catch (Exception) {}
    }

    #endregion

    /// <summary>
    /// Switch the options whenever a new node placer type is selected
    /// </summary>
    /// <remarks>If we didn't change the level, try to retrieve a previously set configuration, otherwise, start from scratch</remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NodePlacerComboBoxSelectionChanged(object sender, EventArgs e) {
      var name = nodePlacerTypeComboBox.SelectedItem as string;
      if (name != null) {
        if (CurrentDescriptor.Name != (string)nodePlacerTypeComboBox.SelectedItem) {
          //Check if we already have a configuration for this level...
          NodePlacerDescriptor desc;
          if (levelConfigurations.TryGetValue(name, out desc)) {
            CurrentDescriptor = desc;
          } else {
            //Load from descriptor configurations... we use reflection
            foreach (
              PropertyInfo propertyInfo in
                typeof(NodePlacerConfigurations).GetProperties(BindingFlags.Static | BindingFlags.Public)) {
              var d = propertyInfo.GetValue(null, null) as NodePlacerDescriptor;
              if (d != null && d.Name == name) {
                CurrentDescriptor = d;
                levelConfigurations[name] = d;
                break;
              }
            }
          }
        }
      }
    }

    private Color CurrentBrush {
      get {
        Color layerBrush = LayerBrushes[Level % LayerBrushes.Length];
        return layerBrush;
      }
    }


    public static readonly Color[] LayerBrushes = {
                                                    Color.Red,
                                                    Color.FromArgb(255, 128, 0),
                                                    Color.FromArgb(224, 224, 0),
                                                    Color.FromArgb(64, 208, 64),
                                                    Color.FromArgb(0, 255, 255),
                                                    Color.Blue
                                                  };

    private EditorControl editorControl;
    private OptionHandler nodePlacerOptionHandler;

    #region Rotation Buttons

    private void LeftRotateButtonClicked(object sender, EventArgs e) {
      var rotablePlacer = CurrentDescriptor.Configuration as IRotatableNodePlacerConfiguration;
      if (rotablePlacer != null) {
        //Rotate the placer 90 degrees counter clockwise - we modify the modification matrix of the placer configuration
        rotablePlacer.SetModificationMatrix(
          rotablePlacer.ModificationMatrix.Multiply(RotatableNodePlacerBase.Matrix.Rot90));
        UpdatePreview();
      }
    }

    private void RightRotateButtonClicked(object sender, EventArgs e) {
      var rotablePlacer = CurrentDescriptor.Configuration as IRotatableNodePlacerConfiguration;
      if (rotablePlacer != null) {
        //Rotate the placer 90 degrees clockwise - we modify the modification matrix of the placer configuration
        rotablePlacer.SetModificationMatrix(
          rotablePlacer.ModificationMatrix.Multiply(RotatableNodePlacerBase.Matrix.Rot270));
        UpdatePreview();
      }
    }

    private void MirrorHorizButtonClicked(object sender, EventArgs e) {
      var rotablePlacer = CurrentDescriptor.Configuration as IRotatableNodePlacerConfiguration;
      if (rotablePlacer != null) {
        //Mirror the placer horizontally - we modify the modification matrix of the placer configuration
        rotablePlacer.SetModificationMatrix(
          rotablePlacer.ModificationMatrix.Multiply(RotatableNodePlacerBase.Matrix.MirHor));
        UpdatePreview();
      }
    }

    private void MirrorVertButtonClicked(object sender, EventArgs e) {
      var rotablePlacer = CurrentDescriptor.Configuration as IRotatableNodePlacerConfiguration;
      if (rotablePlacer != null) {
        //Mirror the placer vertically - we modify the modification matrix of the placer configuration
        rotablePlacer.SetModificationMatrix(
          rotablePlacer.ModificationMatrix.Multiply(RotatableNodePlacerBase.Matrix.MirVert));
        UpdatePreview();
      }
    }

    #endregion

    private void levelUpDown_ValueChanged(object sender, EventArgs e) {
      Level = (int) (levelUpDown.Value - 1);
    }
  }

  /// <summary>
  /// Custom option builder that allows to reorder the displayed properties based on an additional sort criterion (i.e. not alphabetically)
  /// </summary>
  internal class SortableOptionBuilder : AttributeBasedOptionBuilder
  {
    protected override PropertyInfo[] SortProperties(PropertyInfo[] properties, IOptionBuilderContext context) {
      Array.Sort(properties, new MyPropertyInfoComparer());
      return properties;
    }

    #region Nested type: MyPropertyInfoComparer

    private class MyPropertyInfoComparer : IComparer<PropertyInfo>
    {
      #region IComparer<PropertyInfo> Members

      public int Compare(PropertyInfo x1, PropertyInfo x2) {
        var oia1 =
          GetAttributes<OptionItemAttributeAttribute>(x1).FirstOrDefault(
            attr => ((OptionItemAttributeAttribute)attr).Name == "OptionItem.Index") as
          OptionItemAttributeAttribute;
        var oia2 =
          GetAttributes<OptionItemAttributeAttribute>(x2).FirstOrDefault(
            attr => ((OptionItemAttributeAttribute)attr).Name == "OptionItem.Index") as
          OptionItemAttributeAttribute;

        if (oia1 != null && oia2 != null) {
          return ((int)oia1.Value).CompareTo((int)oia2.Value);
        }
        string s1 = x1.Name;
        string s2 = x2.Name;
        return s1.CompareTo(s2);
      }

      #endregion

      private static IEnumerable<Attribute> GetAttributes<T>(PropertyInfo info) where T : Attribute {
        return Attribute.GetCustomAttributes(info, typeof(T));
      }
    }

    #endregion
  }
}
