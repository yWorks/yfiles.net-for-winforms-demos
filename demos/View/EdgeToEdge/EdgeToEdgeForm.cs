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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.EdgeToEdge.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.EdgeToEdge
{
  /// <summary>
  /// This application demonstrates the use of edge-to-edge connections. Edges can be created interactively
  /// between nodes, nodes and edges and between two edges. Also, this application enables moving the source or 
  /// target of an edge to another owner.
  /// Connecting the source or target of an edge to itself is prohibited since this is conceptually forbidden.
  /// </summary>
  /// <remarks>
  /// Edge-to-edge connections have to be enabled explicitly using the property <see cref="CreateEdgeInputMode.AllowEdgeToEdgeConnections" />.
  /// <para>
  /// This demo also includes customized implementations of <see cref="IPortCandidateProvider" />, <see cref="IEdgeReconnectionPortCandidateProvider" />,
  /// <see cref="IHitTestable" />, <see cref="IEdgePortHandleProvider" /> and <see cref="IPortLocationModel" />
  /// to enable custom behavior like reconnecting an existing edge to another edge, starting edge creation from an edge etc.
  /// </para>
  /// </remarks>
  public partial class EdgeToEdgeForm : Form
  {
    private const int GridSize = 50;
    private GraphSnapContext snapContext;
    private readonly GridInfo gridInfo = new GridInfo(GridSize);

    /// <summary>
    /// Automatically generated by Visual Studio.
    /// Wires up the UI components and adds a 
    /// <see cref="GraphControl"/> to the form.
    /// </summary>
    public EdgeToEdgeForm() {
      InitializeComponent();

      graphOverviewControl.GraphControl = graphControl;
      graphControl.FileOperationsEnabled = true;

      RegisterToolStripCommands();
      RegisterMenuItemCommands();

      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    #region Command registration

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      loadGraphMLButton.SetCommand(Commands.Open, graphControl);
      saveGraphMLButton.SetCommand(Commands.SaveAs, graphControl);
      printButton.SetCommand(Commands.Print, graphControl);

      cutButton.SetCommand(Commands.Cut, graphControl);
      copyButton.SetCommand(Commands.Copy, graphControl);
      pasteButton.SetCommand(Commands.Paste, graphControl);
      deleteButton.SetCommand(Commands.Delete, graphControl);

      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);

      zoom11ToolStripMenuItem.SetCommand(Commands.Zoom, 1.0d, graphControl);

      groupButton.SetCommand(Commands.GroupSelection, graphControl);
      ungroupButton.SetCommand(Commands.UngroupSelection, graphControl);
    }

    private void RegisterMenuItemCommands() {
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentToolStripMenuItem.SetCommand(Commands.FitContent, graphControl);

      openToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveToolStripMenuItem.SetCommand(Commands.SaveAs, graphControl);
      printToolStripMenuItem.SetCommand(Commands.Print, graphControl);

      cutToolStripMenuItem.SetCommand(Commands.Cut, graphControl);
      copyToolStripMenuItem.SetCommand(Commands.Copy, graphControl);
      pasteToolStripMenuItem.SetCommand(Commands.Paste, graphControl);
      deleteToolStripMenuItem.SetCommand(Commands.Delete, graphControl);
      undoToolStripMenuItem.SetCommand(Commands.Undo, graphControl);
      redoToolStripMenuItem.SetCommand(Commands.Redo, graphControl);

      groupSelectionToolStripMenuItem.SetCommand(Commands.GroupSelection, graphControl);
      ungroupSelectionToolStripMenuItem.SetCommand(Commands.UngroupSelection, graphControl);
      expandGroupToolStripMenuItem.SetCommand(Commands.ExpandGroup, graphControl);
      collapseGroupToolStripMenuItem.SetCommand(Commands.CollapseGroup, graphControl);
      enterGroupToolStripMenuItem.SetCommand(Commands.EnterGroup, graphControl);
      exitGroupToolStripMenuItem.SetCommand(Commands.ExitGroup, graphControl);
    }

    #endregion

    /// <summary>
    /// Initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeInputModes"/>
    /// <seealso cref="InitializeGraph"/>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      // initialize the graph
      InitializeGraph();

      // initialize the snapcontext
      InitializeSnapContext();

      // initialize the input mode
      InitializeInputModes();
      GraphControl.FitGraphBounds();
    }

    protected virtual void InitializeSnapContext() {
      snapContext = new GraphSnapContext();
      snapContext.Enabled = false;
      // disable grid snapping
      snapContext.GridSnapType = GridSnapTypes.None;
      // add constraint provider for nodes, bends, and ports
      snapContext.NodeGridConstraintProvider = new GridConstraintProvider<INode>(gridInfo);
      snapContext.BendGridConstraintProvider = new GridConstraintProvider<IBend>(gridInfo);
      snapContext.PortGridConstraintProvider = new GridConstraintProvider<IPort>(gridInfo);
    }

    /// <summary>
    /// Calls <see cref="CreateEditorMode"/> and registers
    /// the result as the <see cref="CanvasControl.InputMode"/>.
    /// </summary>
    /// <remarks>Enables edge-to-edge connections.</remarks>
    protected virtual void InitializeInputModes() {
      var inputMode = CreateEditorMode();
      EnableEdgeToEdgeConnections(inputMode);
      graphControl.InputMode = inputMode;
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl,
    /// a <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <returns>a new GraphEditorInputMode instance.</returns>
    protected virtual GraphEditorInputMode CreateEditorMode() {
      var mode = new GraphEditorInputMode
      {
        SnapContext = snapContext,
        OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext { Enabled = false },
      };

      // randomize edge color
      mode.CreateEdgeInputMode.EdgeCreationStarted += (sender, args) => SetRandomEdgeColor(args.Item);
      return mode;
    }

    /// <summary>
    /// Enables edge-to-edge connections on the input mode.
    /// </summary>
    /// <param name="mode"></param>
    private void EnableEdgeToEdgeConnections(GraphEditorInputMode mode) {
      // enable edge-to-edge
      mode.CreateEdgeInputMode.AllowEdgeToEdgeConnections = true;

      // create bends only when shift is pressed
      mode.CreateBendInputMode.PressedRecognizer = EventRecognizers.CreateAndRecognizer(MouseEventRecognizers.LeftPressed,
                                                                                        KeyEventRecognizers.ShiftPressed);
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and customizing behavior
    /// </summary>
    protected virtual void InitializeGraph() {

      #region Enable undoability

      // Get the default graph instance and enable undoability support.
      Graph.SetUndoEngineEnabled(true);

      #endregion
      
      #region Configure Graph defaults

      // set the default node style
      Graph.NodeDefaults.Style = new ShinyPlateNodeStyle { Brush = Brushes.Orange };

      // assign default edge style
      Graph.EdgeDefaults.Style = new PolylineEdgeStyle();
      Graph.EdgeDefaults.ShareStyleInstance = false;

      // assign a port style for the ports at the edges
      Graph.EdgeDefaults.Ports.Style = new NodeStylePortStyleAdapter(new ShapeNodeStyle { Shape = ShapeNodeShape.Ellipse, Brush = Brushes.Black, Pen = null }) { RenderSize = new SizeD(3, 3) };

      #endregion

      #region customize behavior

      // enable edge port candidates
      Graph.GetDecorator().EdgeDecorator.PortCandidateProviderDecorator.SetFactory(edge => new EdgeSegmentPortCandidateProvider(edge));
      // set IEdgeReconnectionPortCandidateProvider to allow re-connecting edges to other edges 
      Graph.GetDecorator().EdgeDecorator.EdgeReconnectionPortCandidateProviderDecorator.SetImplementation(
        EdgeReconnectionPortCandidateProviders.AllNodeAndEdgeCandidates);
      Graph.GetDecorator().EdgeDecorator.HandleProviderDecorator.SetFactory(edge => new PortRelocationHandleProvider(null, edge) {
          Visualization = Visualization.Live
      });

      // load a sample graph
      graphControl.ImportFromGraphML("Resources/sample.graphml");
      graphControl.FitGraphBounds();

      #endregion
    }

    /// <summary>
    /// Creates a random colored pen and uses that one for the style.
    /// </summary>
    private void SetRandomEdgeColor(IEdge edge) {
      var polylineEdgeStyle = edge.Style as PolylineEdgeStyle;
      if (polylineEdgeStyle != null) {
        var random = new Random();
        var pen = new Pen(new SolidBrush(Color.FromArgb(255, (byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255))), 2);
        polylineEdgeStyle.Pen = pen;
      }
    }

    /// <summary>
    /// Returns the GraphControl instance used in the form.
    /// </summary>
    public GraphControl GraphControl {
      get { return graphControl; }
    }

    /// <summary>
    /// Gets the currently registered IGraph instance from the GraphControl.
    /// </summary>
    public IGraph Graph {
      get { return GraphControl.Graph; }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }
    
    private void exportImageButton_Click(object sender, EventArgs e) {
      ExportImage();
    }
    
    private void newButton_Click(object sender, EventArgs e) {
      ClearGraph();
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e) {
      ClearGraph();
    }

    private void snappingButton_Click(object sender, EventArgs e) {
      ((GraphEditorInputMode)graphControl.InputMode).SnapContext.Enabled = snappingButton.Checked;
    }

    private void orthogonalEditingButton_Click(object sender, EventArgs e) {
      ((GraphEditorInputMode)graphControl.InputMode).OrthogonalEdgeEditingContext.Enabled = orthogonalEditingButton.Checked;
    }

    private void ClearGraph() {
      graphControl.Graph.Clear();
    }

    private void ExportImage() {
      graphControl.UpdateContentRect(new InsetsD(5, 5, 5, 5));
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Filter = "JPEG Files|*.jpg";
      if (dialog.ShowDialog(FindForm()) == DialogResult.OK) {
        graphControl.ExportToBitmap(dialog.FileName, "image/jpeg");
      }
    }

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new EdgeToEdgeForm());
    }

    #endregion
  }

  #region customized implementations

  /// <summary>
  /// A port candidate provider that aggregates different <see cref="IPortLocationModel">PortLocationModels</see>
  /// to provide a number of port candidates along each segment of the edge.
  /// </summary>
  public class EdgeSegmentPortCandidateProvider : PortCandidateProviderBase
  {
    private readonly IEdge edge;

    public EdgeSegmentPortCandidateProvider(IEdge edge) {
      this.edge = edge;
    }

    protected override IEnumerable<IPortCandidate> GetPortCandidates(IInputModeContext context) {
      List<IPortCandidate> candidates = new List<IPortCandidate>();
      // add equally distributed port candidates along the edge
      for (int i = 1; i < 10; ++i) {
        candidates.Add(new DefaultPortCandidate(edge, EdgePathPortLocationModel.Instance.CreateRatioParameter(0.1 * i)));
      }
      // add a dynamic candidate that can be used if shift is pressed to assign the exact location.
      candidates.Add(new DefaultPortCandidate(edge, EdgePathPortLocationModel.Instance));
      return candidates;
    }
  }

  #endregion
}
