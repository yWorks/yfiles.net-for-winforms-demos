/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.4.
 ** Copyright (c) 2000-2021 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Demo.yFiles.Complete.RotatableNodes.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;
using yWorks.GraphML;
using yWorks.Layout;
using yWorks.Layout.Circular;
using yWorks.Layout.Hierarchic;
using yWorks.Layout.Organic;
using yWorks.Layout.Orthogonal;
using yWorks.Layout.Radial;
using yWorks.Layout.Router;
using yWorks.Layout.Router.Polyline;
using yWorks.Layout.Tree;
using Brushes = System.Drawing.Brushes;

[assembly: XmlnsDefinition("http://www.yworks.com/yFiles.net/demos/RotatableNodes/1.0", "Demo.yFiles.Complete.RotatableNodes")]
[assembly: XmlnsPrefix("http://www.yworks.com/yFiles.net/demos/RotatableNodes/1.0", "demo")]


namespace Demo.yFiles.Complete.RotatableNodes
{
  /// <summary>
  /// Demo code that shows how support for rotated node visualizations can be implemented on top of the yFiles library.
  /// A custom <see cref="INodeStyle"/> implementation is used to encapsulate most of the added functionality.
  /// </summary>
  public partial class RotatableNodesForm : Form
  {
    #region Initialization

    public RotatableNodesForm() {
      InitializeComponent();
      graphControl.FileOperationsEnabled = true;

      openButton.SetCommand(Commands.Open, graphControl);
      saveButton.SetCommand(Commands.SaveAs, graphControl);

      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      resetZoomButton.SetCommand(Commands.Zoom, 1, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
      
      cutButton.SetCommand(Commands.Cut, graphControl);
      copyButton.SetCommand(Commands.Copy, graphControl);
      pasteButton.SetCommand(Commands.Paste, graphControl);
      deleteButton.SetCommand(Commands.Delete, graphControl);

      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);

      groupButton.SetCommand(Commands.GroupSelection, graphControl);
      ungroupButton.SetCommand(Commands.UngroupSelection, graphControl);
      enterGroupButton.SetCommand(Commands.EnterGroup, graphControl);
      exitGroupButton.SetCommand(Commands.ExitGroup, graphControl);

      // load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

    }

    /// <summary>
    /// Called upon loading of the form.
    /// Initializes the application.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);

      InitializeInputMode();
      InitializeGraphML();
      InitializeGraph();

      layoutBox.SelectedIndex = 0;
      sampleBox.SelectedIndex = 0;

      LoadGraph("sine");
    }

    /// <summary>
    /// Initializes the interaction with the graph.
    /// </summary>
    private void InitializeInputMode() {
      var geim = new GraphEditorInputMode
      {
        // Enable orthogonal edge editing
        OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext(),

        // enable snapping only for the resizing of nodes and only to the same sizes of other nodes.
        SnapContext = new GraphSnapContext
        {
          Enabled = false,
          CollectNodePairSegmentSnapLines = false,
          CollectNodePairSnapLines = false,
          CollectEdgeSnapLines = false,
          CollectNodeSnapLines = false,
          CollectPortSnapLines = false,
          SnapBendAdjacentSegments = false,
          CollectNodeSizes = true,
        },
        LabelSnapContext = new LabelSnapContext {
          Enabled = false
        },
        AllowClipboardOperations = true,
        AllowGroupingOperations = true,
      };
      geim.WaitInputMode.WaitingStarted += (sender, args) => EnableControls(false);
      geim.WaitInputMode.WaitingEnded += (sender, args) => EnableControls(true);
      graphControl.InputMode = geim;
    }

    /// <summary>
    /// Initialize loading from and saving to graphml-files.
    /// </summary>
    private void InitializeGraphML() {
      // initialize (de-)serialization for load/save commands
      var graphmlHandler = new GraphMLIOHandler();
      graphmlHandler.Parsed += (sender, e) => {
        // after loading apply wrap node styles, node label models and port location models in rotatable decorators
        var graph = graphControl.Graph;
        foreach (var node in graph.Nodes.Where(node => !graph.IsGroupNode(node))) {
          if (!(node.Style is RotatableNodeStyleDecorator)) {
            graph.SetStyle(node, new RotatableNodeStyleDecorator(node.Style));
          }
          foreach (var label in node.Labels.Where(label => !(label.LayoutParameter.Model is RotatableNodeLabelModelDecorator))) {
            graph.SetLabelLayoutParameter(label, new RotatableNodeLabelModelDecorator(label.LayoutParameter.Model).CreateWrappingParameter(label.LayoutParameter));
          }
          foreach (var port in node.Ports.Where(port => !(port.LocationParameter.Model is RotatablePortLocationModelDecorator))) {
            graph.SetPortLocationParameter(port, RotatablePortLocationModelDecorator.Instance.CreateWrappingParameter(port.LocationParameter));
          }
        }
      };

      graphControl.GraphMLIOHandler = graphmlHandler;
    }

    /// <summary>
    /// Initializes styles and decorators for the graph.
    /// </summary>
    private void InitializeGraph() {
      var foldingManager = new FoldingManager();
      var graph = foldingManager.CreateFoldingView().Graph;

      var decorator = graph.GetDecorator();

      // For rotated nodes, need to provide port candidates that are backed by a rotatable port location model
      // If you want to support non-rotated port candidates, you can just provide undecorated instances here
      decorator.NodeDecorator.PortCandidateProviderDecorator.SetFactory(
          node => node.Style is RotatableNodeStyleDecorator, CreatePortCandidateProvider);

      decorator.PortDecorator.EdgePathCropperDecorator.SetImplementation(new AdjustOutlinePortInsidenessEdgePathCropper());
      decorator.NodeDecorator.GroupBoundsCalculatorDecorator.SetImplementation(new RotationAwareGroupBoundsCalculator());

      graph.NodeDefaults.Style = new RotatableNodeStyleDecorator(new ShinyPlateNodeStyle { Brush = Brushes.Orange, DrawShadow = false });
      graph.NodeDefaults.ShareStyleInstance = false;
      graph.NodeDefaults.Size = new SizeD(100, 50);

      var coreLabelModel = new InteriorLabelModel();
      graph.NodeDefaults.Labels.LayoutParameter =
          new RotatableNodeLabelModelDecorator(coreLabelModel).CreateWrappingParameter(InteriorLabelModel.Center);
      
      // Make ports visible
      graph.NodeDefaults.Ports.Style = new NodeStylePortStyleAdapter(new ShapeNodeStyle { Shape = ShapeNodeShape.Ellipse, Brush = Brushes.Red });
      // Use a rotatable port model as default
      graph.NodeDefaults.Ports.LocationParameter =
          new RotatablePortLocationModelDecorator().CreateWrappingParameter(FreeNodePortLocationModel.NodeTopAnchored);

      graph.GroupNodeDefaults.Style = new PanelNodeStyle { Color = Color.LightBlue };

      graph.EdgeDefaults.Labels.LayoutParameter = new EdgePathLabelModel { Distance = 10 }.CreateDefaultParameter();

      // enable undo
      foldingManager.MasterGraph.SetUndoEngineEnabled(true);

      graphControl.Graph = graph;
    }

    /// <summary>
    /// Creates an <see cref="IPortCandidateProvider"/> that considers the node's shape and rotation.
    /// </summary>
    private static IPortCandidateProvider CreatePortCandidateProvider(INode node) {
      var rotatedPortModel = RotatablePortLocationModelDecorator.Instance;
      var freeModel = FreeNodePortLocationModel.Instance;

      var rnsd = (RotatableNodeStyleDecorator) node.Style;
      var wrapped = rnsd.Wrapped;
      var sns = wrapped as ShapeNodeStyle;

      if (wrapped is ShinyPlateNodeStyle || wrapped is BevelNodeStyle ||
          sns != null && sns.Shape == ShapeNodeShape.RoundRectangle) {
        return PortCandidateProviders.Combine(
            //Take all existing ports - these are assumed to have the correct port location model
            PortCandidateProviders.FromUnoccupiedPorts(node),
            //Provide explicit candidates - these are all backed by a rotatable port location model
            PortCandidateProviders.FromCandidates(
                //Port candidates at the corners that are slightly inset
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(freeModel.CreateParameter(new PointD(0, 0), new PointD(5, 5)))),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(freeModel.CreateParameter(new PointD(0, 1), new PointD(5, -5)))),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(freeModel.CreateParameter(new PointD(1, 0), new PointD(-5, 5)))),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(freeModel.CreateParameter(new PointD(1, 1), new PointD(-5, -5)))),
                //Port candidates at the sides and the center
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeLeftAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeBottomAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeCenterAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeTopAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeRightAnchored))
                ));
      }
      if (sns != null && sns.Shape == ShapeNodeShape.Rectangle) {
        return PortCandidateProviders.Combine(
            PortCandidateProviders.FromUnoccupiedPorts(node),
            PortCandidateProviders.FromCandidates(
                //Port candidates at the corners
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeTopLeftAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeTopRightAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeBottomLeftAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeBottomRightAnchored)),
                //Port candidates at the sides and the center
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeLeftAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeBottomAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeCenterAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeTopAnchored)),
                new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(FreeNodePortLocationModel.NodeRightAnchored))
                ));
      }
      if (sns != null) {
        // Can be an arbitrary shape. First create a dummy node that is not rotated
        var dummyNode = new SimpleNode
        {
          Style = sns,
          Layout = node.Layout
        };
        var shapeProvider = PortCandidateProviders.FromShapeGeometry(dummyNode, 0);
        var shapeCandidates = shapeProvider.GetTargetPortCandidates(null);
        var rotatingCandidates =
            shapeCandidates.Select(c => new DefaultPortCandidate(node, rotatedPortModel.CreateWrappingParameter(c.LocationParameter)));
        return PortCandidateProviders.Combine(
            PortCandidateProviders.FromUnoccupiedPorts(node),
            PortCandidateProviders.FromCandidates(rotatingCandidates));
      }
      return null;
    }

    /// <summary>
    /// Loads the graph from the 'Resources' folder.
    /// </summary>
    private void LoadGraph(String graphName) {
      string fileName = string.Format("Resources" + System.IO.Path.DirectorySeparatorChar + "{0}.graphml", graphName);
      graphControl.Graph.Clear();
      using (StreamReader reader = new StreamReader(fileName)) {
        var ioHandler = graphControl.GraphMLIOHandler;
        ioHandler.Read(graphControl.Graph, reader);
      }
      graphControl.FitGraphBounds();
      
      // clear undo-queue
      graphControl.Graph.GetUndoEngine().Clear();
    }

    #endregion

    #region Layout

    private async void LayoutChooserBoxSelectedIndexChanged(object sender, EventArgs e) {
      if (graphControl != null) {
        await ApplyLayout();
      }
    }

    private async void OnLayoutClick(object sender, EventArgs e) {
      await ApplyLayout();
    }

    /// <summary>
    /// Runs a layout algorithm which is configured to consider node rotations.
    /// </summary>
    public async Task ApplyLayout() {
      var graph = graphControl.Graph;

      // provide the rotated outline and layout for the layout algorithm
      graph.MapperRegistry.CreateDelegateMapper(RotatedNodeLayoutStage.RotatedNodeLayoutDpKey, node => {
        var style = node.Style;
        var outline = style.Renderer.GetShapeGeometry(node, style).GetOutline();
        if (outline == null) {
          outline = new GeneralPath(4);
          outline.AppendRectangle(node.Layout.ToRectD(), false);
        }
        return new RotatedNodeLayoutStage.RotatedNodeShape(outline,
            style is RotatableNodeStyleDecorator
                ? ((RotatableNodeStyleDecorator) style).GetRotatedLayout(node)
                : (IOrientedRectangle) new OrientedRectangle(node.Layout));
      });

      // get the selected layout algorithm
      var layout = GetLayoutAlgorithm();

      // wrap the algorithm in RotatedNodeLayoutStage to make it aware of the node rotations
      var rotatedLayout = new RotatedNodeLayoutStage(layout) {EdgeRoutingMode = GetRoutingMode()};

      // apply the layout
      await graphControl.MorphLayout(rotatedLayout, TimeSpan.FromMilliseconds(700));
      
      // clean up mapper registry
      graph.MapperRegistry.RemoveMapper(RotatedNodeLayoutStage.RotatedNodeLayoutDpKey);
    }

    private void EnableControls(bool enable) {
      sampleBox.Enabled = enable;
      layoutBox.Enabled = enable;
      layoutButton.Enabled = enable;
    }

    /// <summary>
    /// Gets the layout algorithm selected by the user.
    /// </summary>
    /// <returns></returns>
    private ILayoutAlgorithm GetLayoutAlgorithm() {
      var graph = graphControl.Graph;
      var layoutName = layoutBox.SelectedItem as string;
      ILayoutAlgorithm layout = new HierarchicLayout();
      if (layoutName != null) {
        if (layoutName == "Layout: Hierarchic") {
          layout = new HierarchicLayout();
        } else if (layoutName == "Layout: Organic") {
          layout = new OrganicLayout {
            PreferredEdgeLength = 1.5 * Math.Max(graph.NodeDefaults.Size.Width, graph.NodeDefaults.Size.Height)
          };
        } else if (layoutName == "Layout: Orthogonal") {
          layout = new OrthogonalLayout();
        } else if (layoutName == "Layout: Circular") {
          layout = new CircularLayout();
        } else if (layoutName == "Layout: Tree") {
          layout = new TreeReductionStage(new TreeLayout()) {NonTreeEdgeRouter = new OrganicEdgeRouter()};
        } else if (layoutName == "Layout: Balloon") {
          layout = new TreeReductionStage(new BalloonLayout()) {NonTreeEdgeRouter = new OrganicEdgeRouter()};
        } else if (layoutName == "Layout: Radial") {
          layout = new RadialLayout();
        } else if (layoutName == "Routing: Polyline") {
          layout = new EdgeRouter();
        } else if (layoutName == "Routing: Organic") {
          layout = new OrganicEdgeRouter {EdgeNodeOverlapAllowed = false};
        }
      }
      return layout;
    }

    /// <summary>
    /// Get the routing mode that suits the selected layout algorithm.
    /// </summary>
    /// <remarks>
    /// Layout algorithm that place edge ports in the center of the node don't need to add a routing step.
    /// </remarks>
    private RotatedNodeLayoutStage.RoutingMode GetRoutingMode() {
      var layoutName = layoutBox.SelectedItem as string;
      if (layoutName != null && (layoutName == "Layout: Hierarchic" || 
                                 layoutName == "Layout: Orthogonal" || 
                                 layoutName == "Layout: Tree" ||
                                 layoutName == "Routing: Polyline")) {
        return RotatedNodeLayoutStage.RoutingMode.ShortestStraightPathToBorder;
      }
      return RotatedNodeLayoutStage.RoutingMode.NoRouting;
    }

    #endregion

    /// <summary>
    /// Toggle snapping.
    /// </summary>
    private void SnappingButtonClick(object sender, EventArgs e) {
      ((GraphEditorInputMode) graphControl.InputMode).SnapContext.Enabled = snappingButton.Checked;
      ((GraphEditorInputMode) graphControl.InputMode).LabelSnapContext.Enabled = snappingButton.Checked;
    }

    ///// <summary>
    ///// Toggle orthogonal edge editing.
    ///// </summary>
    private void OrthogonalEditingButtonClick(object sender, EventArgs e) {
      ((GraphEditorInputMode) graphControl.InputMode).OrthogonalEdgeEditingContext.Enabled = orthogonalEdgesButton.Checked;
    }

    ///// <summary>
    ///// Load the selected graph.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    private void GraphChooserBoxSelectedIndexChanged(object sender, EventArgs e) {
      var graphName = sampleBox.SelectedItem as string;
      if (graphName != null && graphControl != null) {
        if (graphName == "Sample: Sine Wave") {
          LoadGraph("sine");
        } else if (graphName == "Sample: Circle") {
          LoadGraph("circle");
        }
      }
    }

    #region Startup

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new RotatableNodesForm());
    }

    #endregion
  }
}
