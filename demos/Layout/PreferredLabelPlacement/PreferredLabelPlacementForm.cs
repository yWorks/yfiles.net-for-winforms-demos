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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using yWorks.Algorithms.Geometry;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Hierarchic;
using yWorks.Layout.Labeling;
using yWorks.Layout.Orthogonal;
using yWorks.Layout.Tree;

namespace Demo.yFiles.Layout.PreferredLabelPlacement
{
  /// <summary>
  /// </summary>
  public partial class PreferredLabelPlacementForm : Form
  {
    #region Static fields and constructors

    private const string PreferredPlacement = "PREFERRED_PLACEMENT";
    private const string LabelText = "ITEM_TEXT";
    private const string PlacementDistance = "ITEM_DISTANCE";
    private const string PlacementAlongEdge = "ITEM_PLACEMENT";
    private const string PlacementSideOfEdge = "ITEM_SIDE";
    private const string PlacementSideReference = "ITEM_SIDE_REFERENCE";
    private const string Angle = "ITEM_ANGLE";
    private const string AngleReference = "ITEM_ANGLE_REFERENCE";
    private const string AngleRotation = "ITEM_ANGLE_ROTATION";
    private const string AngleAdd180Degree = "ITEM_ANGLE_ADD_180_DEGREE";

    private static readonly IList<LabelPlacements> PlacementsAlongEdge;
    private static readonly IList<LabelPlacements> PlacementsSideOfEdge;
    private static readonly IList<LabelSideReferences> SideReferences;
    private static readonly IList<LabelAngleReferences> AngleReferences;
    private static readonly IList<LabelAngleOnRightSideRotations> AngleRotations;

    static PreferredLabelPlacementForm() {
      PlacementsAlongEdge = new List<LabelPlacements>
                              {
                                LabelPlacements.AtCenter,
                                LabelPlacements.AtSource,
                                LabelPlacements.AtTarget,
                                LabelPlacements.Anywhere
                              };
      PlacementsSideOfEdge = new List<LabelPlacements>
                               {
                                 LabelPlacements.OnEdge,
                                 LabelPlacements.RightOfEdge,
                                 LabelPlacements.LeftOfEdge,
                                 LabelPlacements.Anywhere
                               };
      SideReferences = new List<LabelSideReferences>
                         {
                           LabelSideReferences.RelativeToEdgeFlow,
                           LabelSideReferences.AbsoluteWithLeftInNorth,
                           LabelSideReferences.AbsoluteWithRightInNorth
                         };
      AngleReferences = new List<LabelAngleReferences>
                          {
                            LabelAngleReferences.RelativeToEdgeFlow,
                            LabelAngleReferences.Absolute
                          };
      AngleRotations = new List<LabelAngleOnRightSideRotations>
                         {
                           LabelAngleOnRightSideRotations.Clockwise,
                           LabelAngleOnRightSideRotations.CounterClockwise
                         };
    }

    /// <summary>
    /// Automatically generated by Visual Studio. Wires up the UI components and
    /// adds a <see cref="GraphControl"/> to the window.
    /// </summary>
    public PreferredLabelPlacementForm() {
      InitializeComponent();
      // load description
      description.LoadFile(new MemoryStream(yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.description), RichTextBoxStreamType.RichText);

      // set commands to buttons and menu items
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitToSizeButton.SetCommand(Commands.FitContent, graphControl);
      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);
    }

    // the mapper for the preferred placement information
    private readonly DictionaryMapper<ILabel, PreferredPlacementDescriptor> descriptorMapper = new DictionaryMapper<ILabel, PreferredPlacementDescriptor>();

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeGraph"/>
    protected virtual void OnLoad(object src, EventArgs e) {
      InitializeGraph();
      InitializeStyles();
      InitializeInputModes();
      InitializeOptions();

      InitializeAlgorithms();
      InitializeLayoutComboBox();

      LoadInitialGraph();
      graphControl.FitGraphBounds();
      UpdateOptionHandler(graphControl.Graph.Labels);
    }

    #endregion

    #region Properties

    /// <summary>
    /// The option handler for the preferred placement descriptions.
    /// </summary>
    public OptionHandler Handler { get; private set; }

    #endregion

    #region Initialization

    private void LoadInitialGraph() {
      graphControl.ImportFromGraphML("Resources\\preferredplacement.graphml");
      UpdateOptionHandler(graphControl.Graph.Labels);
    }

    private void InitializeGraph() {
      var graph = graphControl.Graph;

      // add preferred placement information to each new label
      graph.LabelAdded += (source, evt) => descriptorMapper[evt.Item] = new PreferredPlacementDescriptor();
      graph.LabelRemoved += (source, evt) => descriptorMapper.RemoveValue(evt.Item);

      graph.SetUndoEngineEnabled(true);
    }

    private void OnGraphControlSelectionChanged(object sender, SelectionEventArgs<IModelItem> e) {
      UpdateOptionHandler(GetAffectedLabels());
    }

    private void InitializeInputModes() {
      var geim = new GraphEditorInputMode
                   {
                     AllowCreateEdge = false,
                     AllowCreateNode = false,
                     AllowCreateBend = false,
                   };
      // if edge creation is allowed, add a new label when an edge has been created
      geim.CreateEdgeInputMode.EdgeCreated += (sender, args) => graphControl.Graph.AddLabel(args.Item, "Label");

      // update the option handler settings when the selection changes
      geim.MultiSelectionFinished += OnGraphControlSelectionChanged;
      graphControl.InputMode = geim;
    }

    private void InitializeStyles() {
      var graph = graphControl.Graph;
      graph.NodeDefaults.Style = new ShinyPlateNodeStyle { Brush = Brushes.Orange, DrawShadow = false};
      graph.NodeDefaults.Size = new SizeD(50, 30);

      graph.EdgeDefaults.Labels.LayoutParameter = FreeEdgeLabelModel.Instance.CreateDefaultParameter();
      graph.EdgeDefaults.Labels.Style = new DefaultLabelStyle
      {
        BackgroundPen = Pens.LightBlue,
        BackgroundBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 255)),
        AutoFlip = false
      };
    }

    #endregion

    #region Layout algorithms

    private ILayoutAlgorithm layoutAlgorithm;
    private Dictionary<string, ILayoutAlgorithm> layoutAlgorithms;

    /// <summary>
    /// Does the label placement using the generic labeling algorithm. Before this, the model of the labels is
    /// set according to the option handlers settings.
    /// </summary>
    private async Task DoLayout(bool fitViewToContent) {
      // fix node layout stage is used to keep the bounding box of the graph in the view port
      var layoutExecutor = new LayoutExecutor(graphControl, new FixNodeLayoutStage(layoutAlgorithm)) {
          Duration = TimeSpan.FromMilliseconds(500),
          AnimateViewport = fitViewToContent,
          LayoutData =
              new CompositeLayoutData(
                  new FixNodeLayoutData {FixedNodes = {Delegate = node => true}},
                  new LabelingData {EdgeLabelPreferredPlacement = {Mapper = descriptorMapper}})

      };
      await layoutExecutor.Start();
    }

    private void InitializeAlgorithms() {
      layoutAlgorithms =
        new Dictionary<string, ILayoutAlgorithm>
          {
            {"Generic Edge Labeling", NonTreeEdgeRouterStage.CreateFastLabeling()},
            {"Hierarchic, Top to Bottom", CreateHierarchicLayout(LayoutOrientation.TopToBottom)},
            {"Hierarchic, Left to Right", CreateHierarchicLayout(LayoutOrientation.LeftToRight)},
            {"Tree", CreateTreeLayout()},
            {"Orthogonal", CreateOrthogonalLayout()}
          };
    }

    private static ILayoutAlgorithm CreateHierarchicLayout(LayoutOrientation layoutOrientation) {
      var hl = new HierarchicLayout
                  {
                    IntegratedEdgeLabeling = true,
                    LayoutOrientation = layoutOrientation
                  };
      DisableAutoFlipping(hl);
      return hl;
    }

    private static ILayoutAlgorithm CreateTreeLayout() {
      var reductionStage = new TreeReductionStage
                             {
                               NonTreeEdgeRouter = new NonTreeEdgeRouterStage(),
                               NonTreeEdgeSelectionKey = LayoutKeys.AffectedEdgesDpKey
                             };
      var treeLayout = new TreeLayout {IntegratedEdgeLabeling = true};
      treeLayout.PrependStage(reductionStage);
      DisableAutoFlipping(treeLayout);
      return treeLayout;
    }

    private static ILayoutAlgorithm CreateOrthogonalLayout() {
      var orthogonalLayout = new OrthogonalLayout {IntegratedEdgeLabeling = true};
      DisableAutoFlipping(orthogonalLayout);
      return orthogonalLayout;
    }

    private static void DisableAutoFlipping(MultiStageLayout multiStageLayout) {
      var labelLayoutTranslator = (LabelLayoutTranslator) multiStageLayout.Labeling;
      labelLayoutTranslator.AutoFlipping = false;
    }

    #endregion

    #region Option handler

    /// <summary>
    /// Whether or not the option handler is currently updated non-interactively.
    /// </summary>
    private bool updatingOptionHandler;

    private void InitializeOptions() {
      Handler = CreateOptionHandler();

      DefaultEditorFactory tableEditorFactory = new DefaultEditorFactory();
      var editorControl = tableEditorFactory.CreateControl(Handler, true, true);
      editorControl.Dock = DockStyle.Fill;
      editorControl.IsAutoAdopt = true;
      editorControl.IsAutoCommit = true;
     
      //order is important here...
      editorControl.Padding = new Padding(5);
      propertiesPanel.Controls.Add(editorControl);
      propertiesPanel.Controls.SetChildIndex(editorControl, 0);
      propertiesPanel.PerformLayout();
    }

    private OptionHandler CreateOptionHandler() {
      var handler = new OptionHandler(PreferredPlacement);

      IOptionItem item;
      handler.AddString(LabelText, "Label").PropertyChanged += OptionHandlerPropertyChanged;
      handler.AddDouble(PlacementDistance, 5.0).PropertyChanged += OptionHandlerPropertyChanged;
      item = handler.AddList(PlacementAlongEdge, PlacementsAlongEdge, PlacementsAlongEdge[0]);
      item.PropertyChanged += OptionHandlerPropertyChanged;
      item = handler.AddList(PlacementSideOfEdge, PlacementsSideOfEdge, PlacementsSideOfEdge[0]);
      item.PropertyChanged += OptionHandlerPropertyChanged;
      item = handler.AddList(PlacementSideReference, SideReferences, SideReferences[0]);
      item.PropertyChanged += OptionHandlerPropertyChanged;
      handler.AddDouble(Angle, 0.0).PropertyChanged += OptionHandlerPropertyChanged;
      item = handler.AddList(AngleReference, AngleReferences, AngleReferences[0]);
      item.PropertyChanged += OptionHandlerPropertyChanged;
      item = handler.AddList(AngleRotation, AngleRotations, AngleRotations[0]);
      item.PropertyChanged += OptionHandlerPropertyChanged;
      handler.AddBool(AngleAdd180Degree, false).PropertyChanged += OptionHandlerPropertyChanged;

      var rm = new ResourceManager("Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement",
                                   Assembly.GetExecutingAssembly());
      var rmf = new ResourceManagerI18NFactory();
      rmf.AddResourceManager(handler.Name, rm);
      handler.I18nFactory = rmf;

      return handler;
    }

    private async void OptionHandlerPropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (updatingOptionHandler) {
        return;
      }

      UpdateLabelValues(GetAffectedLabels());
      await DoLayout(false);
    }

    private IEnumerable<ILabel> GetAffectedLabels() {
      var selectedLabels = graphControl.Selection.SelectedLabels;
      return selectedLabels.Count > 0 ? (IEnumerable<ILabel>) selectedLabels : graphControl.Graph.Labels;
    }

    private void UpdateLabelValues(IEnumerable<ILabel> labels) {
      if (descriptorMapper == null) {
        return;
      }

      var handler = Handler;
      foreach (var edgeLabel in labels) {
        if (edgeLabel.Owner is INode || edgeLabel.Owner is IPort) {
          // this demo shouldn't have node or port labels but we make sure that nothing goes wrong
          continue;
        }

        var prototype = descriptorMapper[edgeLabel];
        var descriptor = new PreferredPlacementDescriptor(prototype);

        var item = handler.GetItemByName(LabelText);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          graphControl.Graph.SetLabelText(edgeLabel, (string) item.Value);
        }
        item = handler.GetItemByName(PlacementAlongEdge);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.PlaceAlongEdge = (LabelPlacements) item.Value;
        }
        item = handler.GetItemByName(PlacementSideOfEdge);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.SideOfEdge = (LabelPlacements) item.Value;
        }
        item = handler.GetItemByName(PlacementSideReference);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.SideReference = (LabelSideReferences) item.Value;
        }
        item = handler.GetItemByName(Angle);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.Angle = Geom.ToRadians((double) item.Value);
        }
        item = handler.GetItemByName(AngleReference);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.AngleReference = (LabelAngleReferences) item.Value;
        }
        item = handler.GetItemByName(AngleRotation);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.AngleRotationOnRightSide = (LabelAngleOnRightSideRotations) item.Value;
        }
        item = handler.GetItemByName(AngleAdd180Degree);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.AngleOffsetOnRightSide = GetLabelAngleOnRightSideOffset((bool) item.Value);
        }
        item = handler.GetItemByName(PlacementDistance);
        if (item.Value != OptionItem.VALUE_UNDEFINED) {
          descriptor.DistanceToEdge = (double) item.Value;
        }

        if (!descriptor.Equals(prototype)) {
          descriptorMapper[edgeLabel] = descriptor;
        }
      }
    }

    private void UpdateOptionHandler(IEnumerable<ILabel> labels) {
      if (descriptorMapper == null) {
        return;
      }

      var valuesUndefined = true;
      string text = null;
      LabelPlacements? placement = null;
      LabelPlacements? side = null;
      LabelSideReferences? sideReference = null;
      double? angle = null;
      LabelAngleReferences? angleReference = null;
      LabelAngleOnRightSideRotations? angleRotation = null;
      bool? hasAngleOffset = null;
      double? distance = null;

      foreach (var label in labels) {
        if (label.Owner is INode || label.Owner is IPort) {
          // this demo shouldn't have node or port labels but we make sure that nothing goes wrong
          continue;
        }

        var descriptor = descriptorMapper[label];
        if (valuesUndefined) {
          text = label.Text;
          placement = descriptor.PlaceAlongEdge;
          side = descriptor.SideOfEdge;
          sideReference = descriptor.SideReference;
          angle = descriptor.Angle;
          angleReference = descriptor.AngleReference;
          angleRotation = descriptor.AngleRotationOnRightSide;
          hasAngleOffset = descriptor.IsAngleOffsetOnRightSide180;
          distance = descriptor.DistanceToEdge;
          valuesUndefined = false;
        } else {
          if (text != null && !text.Equals(label.Text)) {
            text = null;
          }
          if (placement != null && placement.Value != descriptor.PlaceAlongEdge) {
            placement = null;
          }
          if (side != null && side.Value != descriptor.SideOfEdge) {
            side = null;
          }
          if (sideReference != null && sideReference.Value != descriptor.SideReference) {
            sideReference = null;
          }
          if (angle != null && angle.Value.Equals(descriptor.Angle)) {
            angle = null;
          }
          if (angleReference != null && angleReference.Value != descriptor.AngleReference) {
            angleReference = null;
          }
          if (angleRotation != null && angleRotation.Value != descriptor.AngleRotationOnRightSide) {
            angleRotation = null;
          }
          if (hasAngleOffset != null && hasAngleOffset.Value != descriptor.IsAngleOffsetOnRightSide180) {
            hasAngleOffset = null;
          }
          if (distance != null && distance.Value.Equals(descriptor.DistanceToEdge)) {
            distance = null;
          }

          if (text == null && placement == null && side == null && sideReference == null && angle == null &&
              angleReference == null && angleRotation == null && distance == 0) {
            break;
          }
        }
      }

      // If, for a single property, there are multiple values present in the set of selected edge labels, the
      // respective option item is set to indicate an "undefined value" state.
      updatingOptionHandler = true;
      var handler = Handler;
      var unDef = OptionItem.VALUE_UNDEFINED;
      handler.GetItemByName(LabelText).Value = text ?? unDef;
      handler.GetItemByName(PlacementAlongEdge).Value = placement == null ? unDef : placement.Value;
      handler.GetItemByName(PlacementSideOfEdge).Value = side == null ? unDef : side.Value;
      handler.GetItemByName(PlacementSideReference).Value = sideReference == null ? unDef : sideReference.Value;
      handler.GetItemByName(Angle).Value = angle == null ? unDef : Geom.ToDegrees(angle.Value);
      handler.GetItemByName(AngleReference).Value = angleReference == null ? unDef : angleReference.Value;
      handler.GetItemByName(AngleRotation).Value = angleRotation == null ? unDef : angleRotation.Value;
      handler.GetItemByName(AngleAdd180Degree).Value = hasAngleOffset == null ? unDef : hasAngleOffset.Value;
      handler.GetItemByName(PlacementDistance).Value = distance == null ? unDef : distance.Value;
      updatingOptionHandler = false;
    }

    private static LabelAngleOnRightSideOffsets GetLabelAngleOnRightSideOffset(bool hasAngleOffset) {
      return hasAngleOffset ? LabelAngleOnRightSideOffsets.Semi : LabelAngleOnRightSideOffsets.None;
    }

    #endregion

    #region UI handlers

    private async void OnDoLayoutButtonClicked(object sender, EventArgs e) {
     await DoLayout(true);
    }

    private void InitializeLayoutComboBox() {
      foreach (var algorithm in layoutAlgorithms.Keys) {
        layoutComboBox.ComboBox.Items.Add(algorithm);  
      }
      layoutComboBox.SelectedIndex = 0;
      layoutAlgorithm = layoutAlgorithms[(string) layoutComboBox.SelectedItem];
      layoutComboBox.SelectedIndexChanged += (sender, e) => {
                                           layoutAlgorithm = layoutAlgorithms[(string) layoutComboBox.SelectedItem];
                                           DoLayout(true);
                                         };
    }

    #endregion

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new PreferredLabelPlacementForm());
    }

    #endregion
  }
}
