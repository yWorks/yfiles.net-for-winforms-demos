/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.2.
 ** Copyright (c) 2000-2019 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout.Hierarchic;

namespace Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder
{
  public partial class InteractiveNodesGraphBuilderForm : Form
  {

    private readonly AdjacentNodesGraphBuilder<BusinessData, BusinessData> graphBuilder;
    private readonly ObservableCollection<BusinessData> nodeSource;

    #region Initialization

    public InteractiveNodesGraphBuilderForm() {
      InitializeComponent();
      RegisterToolStripCommands();

      // create new input mode
      GraphViewerInputMode inputMode = new GraphViewerInputMode {SelectableItems = GraphItemTypes.None};
      // add a custom input mode that allows dragging nodes from the graph to the lists
      inputMode.Add(new NodeDragInputMode {Priority = -1});
      graphControl.InputMode = inputMode;

      graphControl.FocusIndicatorManager.ShowFocusPolicy = ShowFocusPolicy.Always;
      graphControl.CurrentItemChanged += CurrentItemChanged;

      // create the graph builder
      graphBuilder = new AdjacentNodesGraphBuilder<BusinessData, BusinessData>(graphControl.Graph) {
        NodesSource = nodeSource = CreateInitialBusinessData(),
        SuccessorProvider = data => data.Successors,
        PredecessorProvider = data => data.Predecessors,
        NodeLabelProvider = data => data.NodeName,
      };
      graphBuilder.NodeCreated += (sender, e) => {
        // Set optimal node size
        var l1 = e.Item.Labels.FirstOrDefault();
        if (l1 == null) return;
        var bestSize = new SizeD((l1.PreferredSize.Width) + 10, l1.PreferredSize.Height + 12);
        // Set node to that size. Location is irrelevant here, since we're running a layout anyway
        e.Graph.SetNodeLayout(e.Item, new RectD(e.Item.Layout.ToPointD(), bestSize));
      };
      InitializeGraphDefaults();

      // Create the graph from the model data
      graphBuilder.BuildGraph();

      // Load description
      try {
        description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      } catch (MissingMethodException) {
        // Workaround for https://github.com/microsoft/msbuild/issues/4581
        description.Text = "The description is not available with this version of .NET Core.";
      }
    }
    
    private void RegisterToolStripCommands() {
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    /// <summary>
    /// Sets default styles for the graph.
    /// </summary>
    public void InitializeGraphDefaults() {
      var graph = graphControl.Graph;
      graph.NodeDefaults.Style = new ShapeNodeStyle {
        Shape = ShapeNodeShape.RoundRectangle,
        Brush = new SolidBrush(Color.FromArgb(255, 255, 237, 204)),
        Pen = Pens.DarkOrange
      };
      graph.NodeDefaults.Labels.Style = new DefaultLabelStyle {
        Font = new Font("Arial", 13, FontStyle.Regular, GraphicsUnit.Pixel)
      };
      graph.NodeDefaults.Labels.LayoutParameter = InteriorLabelModel.Center;

      graph.EdgeDefaults.Style = new PolylineEdgeStyle {
        SmoothingLength = 20,
        TargetArrow = Arrows.Default
      };
    }

    #endregion

    private void OnLoad(object sender, EventArgs e) {
      ApplyLayout(false);
      foreach (var data in nodeSource) {
        nodesListBox.Items.Add(data);
      }
    }

    #region Maintaining Business data

    /// <summary>
    /// Create the node source.
    /// </summary>
    /// <returns>A collection of type BusinessData</returns>
    private static ObservableCollection<BusinessData> CreateInitialBusinessData() {
      var nameDataMap = new Dictionary<string, BusinessData> {
        {"Jenny", new BusinessData("Jenny")},
        {"Julia", new BusinessData("Julia")},
        {"Marc", new BusinessData("Marc")},
        {"Martin", new BusinessData("Martin")},
        {"Natalie", new BusinessData("Natalie")},
        {"Nicole", new BusinessData("Nicole")},
        {"Petra", new BusinessData("Petra")},
        {"Stephen", new BusinessData("Stephen")},
        {"Tim", new BusinessData("Tim")},
        {"Tom", new BusinessData("Tom")},
        {"Tony", new BusinessData("Tony")}
      };

      nameDataMap["Julia"].Predecessors.Add(nameDataMap["Jenny"]);
      nameDataMap["Julia"].Successors.Add(nameDataMap["Petra"]);
      nameDataMap["Marc"].Predecessors.Add(nameDataMap["Julia"]);
      nameDataMap["Marc"].Successors.Add(nameDataMap["Tim"]);
      nameDataMap["Martin"].Predecessors.Add(nameDataMap["Julia"]);
      nameDataMap["Martin"].Successors.Add(nameDataMap["Natalie"]);
      nameDataMap["Martin"].Successors.Add(nameDataMap["Nicole"]);
      nameDataMap["Nicole"].Successors.Add(nameDataMap["Petra"]);
      nameDataMap["Tim"].Successors.Add(nameDataMap["Tom"]);
      nameDataMap["Tom"].Successors.Add(nameDataMap["Tony"]);
      nameDataMap["Tony"].Successors.Add(nameDataMap["Tim"]);
      nameDataMap["Tony"].Predecessors.Add(nameDataMap["Julia"]);
      nameDataMap["Stephen"].Successors.Add(nameDataMap["Tom"]);

      return new ObservableCollection<BusinessData> {
        nameDataMap["Marc"],
        nameDataMap["Martin"],
        nameDataMap["Stephen"]
      };
    }

    private BusinessData currentItem;

    /// <summary>
    /// The selected value of a listbox changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListBox_SelectedValueChanged(object sender, EventArgs e) {
      var listBox = (ListBox)sender;
      var selected = (BusinessData) listBox.SelectedItem;
      if (SetCurrentItem(selected)) {
        graphControl.CurrentItem = graphControl.Graph.Nodes.FirstOrDefault(n => n.Tag == selected);
      }
    }

    /// <summary>
    /// Set the current item and update the successor and predecessor list.
    /// </summary>
    /// <param name="selected">The item to select.</param>
    /// <returns>Whether the current item has changed.</returns>
    private bool SetCurrentItem(BusinessData selected) {
      if (currentItem == selected) {
        return false;
      }
      currentItem = selected;
      currentItemLabel.Text = currentItem != null ? currentItem.NodeName : null;
      predecessorsListBox.Items.Clear();
      successorListBox.Items.Clear();
      if (currentItem != null) {
        foreach (var item in currentItem.Predecessors) {
          predecessorsListBox.Items.Add(item);
        }
        foreach (var item in currentItem.Successors) {
          successorListBox.Items.Add(item);
        }
      }
      return true;
    }
    
    /// <summary>
    /// Called when the GraphControl's current item has changed.
    /// </summary>
    /// <remarks>
    /// Updates the current item and the successor and predecessor lists.
    /// </remarks>
    private void CurrentItemChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
      var item = (BusinessData) (graphControl.CurrentItem != null ? graphControl.CurrentItem.Tag : null);
      if (SetCurrentItem(item) && (item == null || nodeSource.Contains(item))) {
        nodesListBox.SelectedItem = item;
      }
    }

    /// <summary>
    /// Updates the graph after changes to the business data.
    /// </summary>
    /// <param name="incremental">Whether to keep the unchanged parts of the graph stable.</param>
    /// <param name="incrementalNodes">The nodes which have changed.</param>
    public void Update(bool incremental, params BusinessData[] incrementalNodes) {
      graphBuilder.UpdateGraph();
      ApplyLayout(incremental, incrementalNodes);
    }
    
    /// <summary>
    /// Applies the layout.
    /// </summary>
    /// <remarks>
    /// Uses an <see cref="HierarchicLayout"/>. If single graph
    /// items are created or removed, the incremental mode of this layout
    /// algorithm is used to keep most of the current layout of the graph
    /// unchanged.
    /// </remarks>
    /// <param name="incremental">if set to <see langword="true"/> [incremental].</param>
    /// <param name="incrementalNodes">The incremental nodes.</param>
    private async void ApplyLayout(bool incremental, params BusinessData[] incrementalNodes) {
      var layout = new HierarchicLayout();
      HierarchicLayoutData layoutData = null;
      if (!incremental) {
        layout.LayoutMode = LayoutMode.FromScratch;
      } else {
        layout.LayoutMode = LayoutMode.Incremental;

        if (incrementalNodes.Any()) {
          // we need to add hints for incremental nodes
          layoutData = new HierarchicLayoutData {
            IncrementalHints = {IncrementalLayeringNodes = {Source = incrementalNodes.Select(graphBuilder.GetNode)}}
          };
        }
      }
      await graphControl.MorphLayout(layout, TimeSpan.FromSeconds(1), layoutData);
    }

    #endregion
    
    #region Adding and removing elements

    /// <summary>
    /// The "add successor" button has been clicked.
    /// </summary>
    /// <remarks>
    /// Asks for a name for the new element and updates the lists.
    /// </remarks>
    private void AddSuccessor(object sender, EventArgs e) {
      if (currentItem != null) {
        var businessData = AddNewItem();
        if (businessData == null) return;
        currentItem.Successors.Add(businessData);
        successorListBox.Items.Add(businessData);
        Update(true, businessData);
      }
    }
    
    /// <summary>
    /// The "remove successor" button has been clicked.
    /// </summary>
    /// <remarks>
    /// Removes the current item and updates the list.
    /// </remarks>
    private void RemoveSuccessor(object sender, EventArgs e) {
      if (currentItem != null) {
        var item = (BusinessData) successorListBox.SelectedItem;
        if (item != null) {
          currentItem.Successors.Remove(item);
          successorListBox.Items.Remove(item);
        }
        Update(true);
      }

    }
    
    /// <summary>
    /// The "add predecessor" button has been clicked.
    /// </summary>
    /// <remarks>
    /// Asks for a name for the new element and updates the lists.
    /// </remarks>
    private void AddPredecessor(object sender, EventArgs e) {
      if (currentItem != null) {
        var businessData = AddNewItem();
        if (businessData == null) return;
        currentItem.Predecessors.Add(businessData);
        predecessorsListBox.Items.Add(businessData);
        Update(true, businessData);
      }

    }
    
    /// <summary>
    /// The "remove predecessor" button has been clicked.
    /// </summary>
    /// <remarks>
    /// Removes the current item and updates the list.
    /// </remarks>
    private void RemovePredecessor(object sender, EventArgs e) {
      if (currentItem != null) {

        var item = (BusinessData) predecessorsListBox.SelectedItem;
        if (item != null) {
          currentItem.Predecessors.Remove(item);
          predecessorsListBox.Items.Remove(item);
        }
        Update(true);
      }
    }
    
    /// <summary>
    /// The "add to node source" button has been clicked.
    /// </summary>
    /// <remarks>
    /// Asks for a name for the new element and updates the lists.
    /// </remarks>
    private void AddNode(object sender, EventArgs e) {
      var businessData = AddNewItem();
      if (businessData == null) return;
      nodeSource.Add(businessData);
      nodesListBox.Items.Add(businessData);
      Update(true, businessData);
    }
    
    /// <summary>
    /// The "remove from node source" button has been clicked.
    /// </summary>
    /// <remarks>
    /// Removes the current item and updates the list.
    /// </remarks>
    private void RemoveNode(object sender, EventArgs e) {
      var item = (BusinessData) nodesListBox.SelectedItem;
      if (item != null) {
        nodeSource.Remove(item);
        nodesListBox.Items.Remove(item);
      }
      Update(true);
    }

    /// <summary>
    /// Opens a dialog to enter a name for a new element.
    /// </summary>
    /// <remarks>
    /// If a name has been entered and OK has been clicked
    /// a new BusinessData object is returned.
    /// If no name has been entered or Cancel has been clicked
    /// null is returned.
    /// </remarks>
    /// <returns>A newly created business object or null</returns>
    private BusinessData AddNewItem() {
      NewItemForm newItemForm = new NewItemForm();
      BusinessData result = null;
      if (newItemForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(newItemForm.Value)) {
        result = new BusinessData(newItemForm.Value);
      }
      newItemForm.Dispose();
      return result;
    }

    #endregion

    #region Drag and Drop

    /// <summary>
    /// Handles drag over on the "Node source list".
    /// </summary>
    private void nodeSourceList_DragOver(object sender, DragEventArgs e) {
      HandleDragOver(sender, e);
    }
    
    /// <summary>
    /// Handles drag over on the "predecessor list".
    /// </summary>
    /// <remarks>
    /// Forbids dropping if no current item is selected.
    /// </remarks>
    private void predecessorList_DragOver(object sender, DragEventArgs e) {
      if (currentItem != null) {
        HandleDragOver(sender, e);
      } else {
        e.Effect = DragDropEffects.None;
      }
    }
    
    /// <summary>
    /// Handles drag over on the "successor list".
    /// </summary>
    /// <remarks>
    /// Forbids dropping if no current item is selected.
    /// </remarks>
    private void successorList_DragOver(object sender, DragEventArgs e) {
      if (currentItem != null) {
        HandleDragOver(sender, e);
      } else {
        e.Effect = DragDropEffects.None;
      }
    }

    private void HandleDragOver(object sender, DragEventArgs e) {
      if (e.Data.GetDataPresent(typeof (BusinessData))) {
        e.Effect = DragDropEffects.Copy;
      } else {
        e.Effect = DragDropEffects.None;
      }
    }
    
    /// <summary>
    /// Handles drop on the "node source list".
    /// </summary>
    private void nodeSourceList_DragDrop(object sender, DragEventArgs e) {
      var item = GetDroppedItem(sender, e);
      nodeSource.Add(item);
      Update(false);
    }
    
    /// <summary>
    /// Handles drop on the "predecessor list".
    /// </summary>
    private void predecessorList_DragDrop(object sender, DragEventArgs e) {
      BusinessData item;
      if (currentItem != null && ((item = GetDroppedItem(sender, e)) != null)) {
        currentItem.Predecessors.Add(item);
        Update(false);
      }
    }
    
    /// <summary>
    /// Handles drop on the "successor list".
    /// </summary>
    private void successorList_DragDrop(object sender, DragEventArgs e) {
      BusinessData item;
      if (currentItem != null && ((item = GetDroppedItem(sender, e)) != null)) {
        currentItem.Successors.Add(item);
        Update(false);
      }
    }


    /// <summary>
    /// Gets the dropped item.
    /// </summary>
    /// <returns>The dropped item or null.</returns>
    private BusinessData GetDroppedItem(object sender, DragEventArgs e) {
      var target = (ListBox) sender;
      BusinessData item = null;
      if (e.Data.GetDataPresent(typeof (BusinessData))) {
        item = (BusinessData) e.Data.GetData(typeof (BusinessData));
        // Perform drag-and-drop, depending upon the effect.
        if (item != null) {
          target.Items.Add(item);
        }
      }
      return item;
    }

    /// <summary>
    /// Handles drop over the trashcan.
    /// </summary>
    /// <remarks>
    /// Removes the dropped item from all lists.
    /// </remarks>
    private void trashcan_DragDrop(object sender, DragEventArgs e) {
      if (e.Data.GetDataPresent(typeof (BusinessData))) {
        var item = (BusinessData) e.Data.GetData(typeof (BusinessData));
        bool changed = false;
        foreach (var data in nodeSource) {
          changed |= data.Successors.Remove(item);
          changed |= data.Predecessors.Remove(item);
          if (data == currentItem) {
            successorListBox.Items.Remove(item);
            predecessorsListBox.Items.Remove(item);
          }
        }
        changed |= nodeSource.Remove(item);
        nodesListBox.Items.Remove(item);
        if (changed) {
          Update(false);
        }
      }
    }

    #endregion
    
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new InteractiveNodesGraphBuilderForm());
    }

  }

  #region Business data

  /// <summary>
  /// Represents an object of the business data.
  /// </summary>
  public class BusinessData : ICloneable
  {
    public BusinessData() :this("Unnamed") {}

    public BusinessData(string name) {
      NodeName = name;
      Successors = new ObservableCollection<BusinessData>();
      Predecessors = new ObservableCollection<BusinessData>();
    }

    public String NodeName { get; set; }
    public ObservableCollection<BusinessData> Successors { get; set; }
    public ObservableCollection<BusinessData> Predecessors { get; set; }

    public override string ToString() {
      return NodeName;
    }

    public object Clone() {
      BusinessData clone = new BusinessData(NodeName);
      foreach (var successor in Successors) {
        clone.Successors.Add(successor);
      }
      foreach (var predecessor in Predecessors) {
        clone.Predecessors.Add(predecessor);
      }
      return clone;
    }
  }

  #endregion

}
