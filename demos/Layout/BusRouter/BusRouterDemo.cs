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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Demo.yFiles.Layout.BusRouterDemo.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Layout.Router;

[assembly: XmlnsDefinition("http://www.yworks.com/yfiles.net/5.0/demos/BusRouterWindow", "Demo.yFiles.Layout.BusRouterDemo")]
[assembly: XmlnsPrefix("http://www.yworks.com/yfiles.net/5.0/demos/BusRouterWindow", "demo")]
namespace Demo.yFiles.Layout.BusRouterDemo
{
  /// <summary>
  /// This Demo shows how to use a <see cref="BusRouter"/> as layout.
  /// </summary>
  public partial class BusRouterDemo : Form
  {
    private readonly BusRouter layout = new BusRouter
                                   {
                                     Scope = Scope.RouteAllEdges,
                                     MinimumDistanceToNode = 10,
                                     MinimumDistanceToEdge = 5,
                                     PreferredBackboneSegmentCount = 1,
                                     CrossingCost = 1,
                                   };

    #region Initialization

    /// <summary>
    /// Initializes a new instance of the <see cref="BusRouterDemo"/> class.
    /// </summary>
    public BusRouterDemo() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      RegisterMenuCommands();
      RegisterToolStripCommands();
    }


    private void RegisterMenuCommands() {
      // File menu
      openFileToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveAsToolStripMenuItem.SetCommand(Commands.Save, graphControl);

      // View menu
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitGraphBoundsToolStripMenuItem.SetCommand(Commands.FitContent, graphControl);
    }

    /// <summary>
    /// Registers the tool strip commands.
    /// </summary>
    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeInputModes"/>
    /// <seealso cref="InitializeGraph"/>
    protected async void Demo_Load(object sender, EventArgs e) {
      // initialize the input mode
      InitializeInputModes();

      // initialize the graph
      await InitializeGraph();
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and creating a small sample graph.
    /// </summary>
    protected virtual async Task InitializeGraph() {
      graphControl.ImportFromGraphML("Resources/default.graphml");
      await DoLayout(Scope.RouteAllEdges);
    }

    /// <summary>
    /// Calls <see cref="CreateEditorMode"/> and registers
    /// the result as the <see cref="CanvasControl.InputMode"/>.
    /// </summary>
    protected virtual void InitializeInputModes() {
      graphControl.InputMode = CreateEditorMode();
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl,
    /// a <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <remarks>
    /// The control uses a custom node creation callback that creates business objects for newly
    /// created nodes.
    /// </remarks>
    /// <returns>a new GraphEditorInputMode instance</returns>
    protected virtual IInputMode CreateEditorMode() {
      var mode = new GraphEditorInputMode
      {
        // don't allow nodes to be created using a mouse click
        NodeCreator = (context, graph, location, parent) => graphControl.Graph.CreateNode(
          new RectD(location, new SizeD(50, 50)), BusRouterNodeStyles.GetRandomStyle()),
        // disable node resizing
        ShowHandleItems = GraphItemTypes.Bend | GraphItemTypes.Edge,
        // don't allow edges to be created by the user
        AllowCreateEdge = false,
        // enable orthogonal edge editing
        OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext(),
        // enable marquee selection for nodes only
        MarqueeSelectableItems = GraphItemTypes.Node,
        // enable context snapping
        SnapContext = new GraphSnapContext()
      };
      return mode;
    }

    #endregion

    #region Event handlers

    /// <summary>
    /// Called when the run button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private async void OnRunButtonClicked(object sender, EventArgs e) {
      await DoLayout(Scope.RouteAllEdges);
    }

    /// <summary>
    /// Creates the edges between all selected nodes, resulting in a complete subgraph.
    /// </summary>
    private async void OnConnectNodesClick(object sender, EventArgs e) {
      var graph = graphControl.Graph;

      // find the first "Network" node, if any
      var selectedNodes = new HashSet<INode>(graph.Nodes.Where(graphControl.Selection.IsSelected));
      if(selectedNodes.Count == 0) {
        return;
      }

      edgesToRoute.Clear();

      // iterate over all selected nodes to see if we need to create a new edge or modify the old one
      var edgeStyle = new PolylineEdgeStyle {Pen = PenStyles.GetRandomPen()};
      
      
      foreach (var node in selectedNodes) {
        //Create a complete subgraph
        foreach (var otherNode in selectedNodes) {
          if(otherNode != node) {
            edgesToRoute.Add(graph.CreateEdge(node, otherNode, edgeStyle));
          }
        }
      }
      graphControl.Invalidate();
      await DoLayout(Scope.RouteAffectedEdges);
    }

    #endregion

    /// <summary>
    /// mark all edges that should be rerouted
    /// </summary>
    private readonly HashSet<IEdge> edgesToRoute = new HashSet<IEdge>();

    /// <summary>
    /// Perform the layout operation
    /// </summary>
    private async Task DoLayout(Scope scope) {
      // layout starting, disable button
      runButton.Enabled = false;

      var busRouterData = new BusRouterData
      {
        EdgeDescriptors = { Delegate = edge => new BusDescriptor(((PolylineEdgeStyle) edge.Style).Pen)}
      };

      // layout applies only to selected subset of edges
      if (scope == Scope.RouteAffectedEdges) {
        busRouterData.AffectedEdges.Items = edgesToRoute;
      }

      // tell the layout algorithm about the scope it should operate in
      layout.Scope = scope;

      // do the layout
      await graphControl.MorphLayout(layout, TimeSpan.FromSeconds(1), busRouterData);
      // layout finished, enable layout button again
      runButton.Enabled = true;
    }

    private async void OnRefreshButtonClicked(object sender, EventArgs e) {
      await InitializeGraph();
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new BusRouterDemo());
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }
  }

  #region Business logic

  /// <summary>
  /// Contains known node styles
  /// </summary>
  public static class BusRouterNodeStyles
  {
    /// <summary>
    /// Node is represented by a computer icon.
    /// </summary>
    public static readonly INodeStyle Computer = new MemoryImageNodeStyle {Image = Resources.computer};

    /// <summary>
    /// Node is represented by a network icon.
    /// </summary>
    public static readonly INodeStyle Network = new MemoryImageNodeStyle {Image = Resources.network};

    /// <summary>
    /// Node is represented by a printer icon
    /// </summary>
    public static readonly INodeStyle Printer = new MemoryImageNodeStyle {Image = Resources.printer};

    private static readonly Random random = new Random();
    /// <summary>
    /// Gets a random style.
    /// </summary>
    /// <returns>A random style</returns>
    public static INodeStyle GetRandomStyle() {
      var r = random.Next(0, 10);
      if(r < 3) { // 30% chance
        return Network;
      }
      if(r < 5) { // 20% chance
        return Printer;
      }
      // 50% chance
      return Computer;
    }
  }

  /// <summary>
  /// Contains predefined <see cref="Pen"/>s.
  /// </summary>
  public static class PenStyles
  {
    /// <summary>
    /// Contains all <see cref="Pen"/>s that can be used.
    /// </summary>
    private static readonly Pen[] Values;
    private static readonly Random Random = new Random();

    static PenStyles() {
      Values = new[]{
                     Color.Black, Color.Orange, Color.DarkCyan, 
                     Color.DarkGray, Color.Brown, Color.DarkBlue, Color.DarkMagenta, Color.DarkSlateBlue, Color.Purple
                   }.Select(color => new Pen(new SolidBrush(color), 2)).ToArray();
    }

    private static int index;
    /// <summary>
    /// Gets a random <see cref="Pen"/>.
    /// </summary>
    /// <returns>A random <see cref="Pen"/> object</returns>
    public static Pen GetRandomPen() {
      // no more predefined values, generate default (dark) color
      if(Values.Length <= index) {
        return new Pen(new SolidBrush(
          Color.FromArgb((byte) Random.Next(150), (byte) Random.Next(150), (byte) Random.Next(150))), 2);
      }
      // use next predefined value
      return Values[index++];
    }
  }

  #endregion
}