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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.HighDpi.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.HighDpi
{
  /// <summary>
  /// Simple demo that hosts a <see cref="GraphControl"/>
  /// which enables graph editing via the default <see cref="GraphEditorInputMode"/> 
  /// input mode for editing graphs.
  /// </summary>
  /// <remarks>
  /// This demo also supports grouped graphs. Selected nodes can be grouped 
  /// in so-called group nodes using CTRL-G and again be ungrouped using CTRL-U. 
  /// To move sets of nodes into and out of group nodes using the mouse, hold down 
  /// the SHIFT key while dragging.
  /// <para>
  /// Apart from graph editing, the demo demonstrates various basic features that are already
  /// present on GraphControl (either as predefined commands or as simple method calls),
  /// for example load/save/export.
  /// </para>
  /// <para>
  /// In addition to the GraphControl itself, the demo also shows how to use the GraphOverviewControl.
  /// </para>
  /// </remarks>
  public partial class HighDpiForm : Form
  {
    /// <summary>
    /// Automatically generated by Visual Studio.
    /// Wires up the UI components and adds a 
    /// <see cref="GraphControl"/> to the form.
    /// </summary>
    public HighDpiForm() {
      InitializeComponent();

      // Set scaling to follow the current Windows settings
      ConfigureDpiScaling();
      UpdateControlScaleInfo();

      graphControl.ScaleChanged += (sender, args) => UpdateControlScaleInfo();

      scaleFactor.SelectedIndex = 0;
      scaleFactor.SelectedIndexChanged += SelectedScaleFactorChanged;

      graphOverviewControl.GraphControl = graphControl;

      // Set up a sample graph and set appropriate defaults
      InitializeGraph();
      // Allow editing
      graphControl.InputMode = CreateEditorMode();

      try {
        description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      } catch (MissingMethodException) {
        // Workaround for https://github.com/microsoft/msbuild/issues/4581
        description.Text = "The description is not available with this version of .NET Core.";
      }
    }

    private void UpdateControlScaleInfo() {
      scaleLabel2.Text = string.Format("GraphControl scaling factor: {0:N0} % ({1:N0} dpi)",
        graphControl.Scale * 100, graphControl.Scale * 96);
    }

    private void SelectedScaleFactorChanged(object sender, EventArgs eventArgs) {
      if (scaleFactor.SelectedIndex == 0) {
        // Automatic
        graphControl.SetDpiScale();
        graphOverviewControl.SetDpiScale();
      } else {
        var newScale = Convert.ToDouble(scaleFactor.SelectedItem, CultureInfo.InvariantCulture);
        graphControl.Scale = newScale;
        graphOverviewControl.Scale = newScale;
      }
    }

    /// <summary>
    /// Sets the proper DPI scaling for the GraphControl and the overview.
    /// </summary>
    private void ConfigureDpiScaling() {
      graphControl.SetDpiScale();
      graphOverviewControl.SetDpiScale();

      scaleLabel1.Text = string.Format("Windows scaling factor: {0:N0} % ({1:N0} dpi)",
        graphControl.Scale * 100, graphControl.Scale * 96);
    }

    /// <summary>
    /// Initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeGraph"/>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      GraphControl.FitGraphBounds();
    }

    /// <summary>
    /// Creates a pre-configured <see cref="GraphSnapContext"/> for this demo.
    /// </summary>
    protected virtual GraphSnapContext CreateGraphSnapContext() {
      return new GraphSnapContext
      {
        Enabled = true,
        GridSnapType = GridSnapTypes.None,
        // Un-comment the following lines to add constraint provider for nodes, bends, and ports
        // NodeGridConstraintProvider = new SimpleGridConstraintProvider<INode>(gridInfo),
        // BendGridConstraintProvider = new SimpleGridConstraintProvider<IBend>(gridInfo),
        // PortGridConstraintProvider = new SimpleGridConstraintProvider<IPort>(gridInfo)
      };
    }

    /// <summary>
    /// Creates a pre-configured <see cref="LabelSnapContext"/> for this demo.
    /// </summary>
    protected virtual LabelSnapContext CreateLabelSnapContext() {
      return new LabelSnapContext
      {
        Enabled = true,
        SnapDistance = 15,
        SnapLineExtension = 100
      };
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl, a <see cref="GraphEditorInputMode" />.
    /// </summary>
    /// <returns>a new GraphEditorInputMode instance and configures snapping and orthogonal edge editing</returns>
    private IInputMode CreateEditorMode() {
      var mode = new GraphEditorInputMode
      {
        AllowGroupingOperations = true,
        SnapContext = CreateGraphSnapContext(),
        LabelSnapContext = CreateLabelSnapContext(),
        OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext { Enabled = false },
      };

      // make bend creation more important than moving of selected edges
      // this has the effect that dragging a selected edge (not its bends)
      // will create a new bend instead of moving all bends
      // This is especially nicer in conjunction with orthogonal
      // edge editing because this creates additional bends every time
      // the edge is moved otherwise
      mode.CreateBendInputMode.Priority = mode.MoveInputMode.Priority - 1;
      return mode;
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and creating a small sample graph.
    /// </summary>
    private void InitializeGraph() {
      graphControl.ImportFromGraphML("Resources/sample.graphml");
      graphControl.Graph.SetUndoEngineEnabled(true);

      #region Configure grouping

      // configure the group node style.

      //PanelNodeStyle is a nice style especially suited for group nodes
      Color groupNodeColor = Color.FromArgb(255, 214, 229, 248);
      Graph.GroupNodeDefaults.Style = new CollapsibleNodeStyleDecorator(new PanelNodeStyle {
        Color = groupNodeColor,
        Insets = new InsetsD(5, 20, 5, 5),
        LabelInsetsColor = Color.Bisque
      });

      // Set a different label style and parameter
      Graph.GroupNodeDefaults.Labels.Style = new DefaultLabelStyle {
        StringFormat = { Alignment = StringAlignment.Far }
      };
      var labelModel = new InteriorStretchLabelModel() { Insets = new InsetsD(15, 1, 1, 1) };
      var param = labelModel.CreateParameter(InteriorStretchLabelModel.Position.North);
      Graph.GroupNodeDefaults.Labels.LayoutParameter = param;

      #endregion

      #region Configure Graph defaults

      // Set the default node style
      Graph.NodeDefaults.Style = new ShinyPlateNodeStyle {
        Brush = Brushes.Orange,
        DrawShadow = true
      };

      // Set the default node label position to centered below the node with the FreeNodeLabelModel that supports label snapping
      Graph.NodeDefaults.Labels.LayoutParameter = FreeNodeLabelModel.Instance.CreateParameter(
          new PointD(0.5, 1.0), new PointD(0, 10), new PointD(0.5, 0.0), new PointD(0, 0), 0);

      // Set the default edge label position with the SmartEdgeLabelModel that supports label snapping
      Graph.EdgeDefaults.Labels.LayoutParameter = new SmartEdgeLabelModel().CreateParameterFromSource(0, 0, 0.5);

      #endregion

    }

    /// <summary>
    /// Returns the GraphControl instance used in the form.
    /// </summary>
    public GraphControl GraphControl
    {
      get { return graphControl; }
    }

    /// <summary>
    /// Gets the currently registered IGraph instance from the GraphControl.
    /// </summary>
    public IGraph Graph
    {
      get { return GraphControl.Graph; }
    }

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new HighDpiForm());
    }

    #endregion
  }
}