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
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Demo.yFiles.Layout.InteractiveOrganic.Properties;
using yWorks.Algorithms;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.GraphML;
using yWorks.Layout;
using yWorks.Layout.Organic;
using yWorks.Utils;

namespace Demo.yFiles.Layout.InteractiveOrganic
{
  /// <summary>
  /// Sample Form that demonstrates the usage of <see cref="InteractiveOrganicLayout"/>
  /// </summary>
  public partial class InteractiveOrganicForm : Form
  {
    /// <summary>
    /// Automatically generated by Visual Studio.
    /// Wires up the UI components and adds a 
    /// <see cref="GraphControl"/> to the form.
    /// </summary>
    public InteractiveOrganicForm() 
    {
      InitializeComponent();
      // enable load and save
      graphControl.FileOperationsEnabled = true;
      // register zoom commands on buttons
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitGraphBounds, graphControl);
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeInputModes"/>
    /// <seealso cref="InitializeGraph"/>
    protected override void OnLoad(EventArgs e) 
    {
      base.OnLoad(e);
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // initialize the graph
      InitializeGraph();

      // initialize the input mode
      InitializeInputModes();
    }

    /// <summary>
    /// Calls <see cref="CreateEditorMode"/> and registers
    /// the result as the <see cref="CanvasControl.InputMode"/>.
    /// </summary>
    protected virtual void InitializeInputModes()
    {
      graphControl.InputMode = CreateEditorMode();
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl,
    /// a <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <returns>a new GraphEditorInputMode instance</returns>
    protected virtual IInputMode CreateEditorMode()
    {

      // create default interaction with a number of disabled input modes.
      GraphEditorInputMode mode = new GraphEditorInputMode
                                    {
                                      // no bend creation
                                      CreateBendInputMode = {Enabled = false},
                                      // only nodes can be selected
                                      SelectableItems = GraphItemTypes.Node | GraphItemTypes.Edge,
                                      // only nodes may be marquee selected
                                      MarqueeSelectableItems = GraphItemTypes.Node,
                                      // only nodes and edges can be selected via clicks
                                      ClickSelectableItems = GraphItemTypes.Node | GraphItemTypes.Edge,
                                      // clicks will be reported for nodes, only
                                      ClickableItems = GraphItemTypes.Node,
                                      // handles won't be shown for any items
                                      ShowHandleItems = GraphItemTypes.None,
                                      // when creating new edges, bends and self-loops are not allowed
                                      CreateEdgeInputMode = {AllowCreateBend = false, AllowSelfloops = false},
                                    };

      // prepare the move input mode for interacting with the layout algorithm
      InitMoveMode(mode.MoveInputMode);


      // We could also allow direct moving of nodes, without requiring selection of the nodes, first.
      // However by default this conflicts with the edge creation gesture, which we will simply disable, here.
      // uncomment the following lines to be able to move nodes without selecting them first
      //
      // var unselected = mode.CreateMoveUnselectedInputMode(EventRecognizers.Create(EventRecognizers.Always));
      // unselected.Priority = mode.MoveInputMode.Priority + 1;
      // mode.Add(unselected);
      // mode.CreateEdgeInputMode.Enabled = false;
      // InitMoveMode(unselected);

      return mode;
    }

    /// <summary>
    /// Registers the listeners to the given move input mode in order to tell the organic layout what
    /// nodes are moved interactively.
    /// </summary>
    /// <param name="moveInputMode">The input mode that should be observed</param>
    private void InitMoveMode(MoveInputMode moveInputMode) {
      // whenever a drag is starting, reset the collection of moved nodes.
      moveInputMode.DragStarting += (sender, args) => movedNodes.Clear();

      // register callbacks to notify the organic layout of changes
      moveInputMode.DragStarted += OnMoveInitialized;
      moveInputMode.DragCanceled += OnMoveCanceled;
      moveInputMode.Dragged += OnMoving;
      moveInputMode.DragFinished += OnMovedFinished;
    }

    /// <summary>
    /// Called once the move operation has been initialized
    /// </summary>
    /// <remarks>
    /// Calculates which components stay fixed and which nodes will be moved by the user.
    /// </remarks>
    private void OnMoveInitialized(object sender, EventArgs eventArgs) {
      if (layout != null) {
        CopiedLayoutGraph copy = this.copiedLayoutGraph;
        var componentNumber = copy.CreateNodeMap();
        GraphConnectivity.ConnectedComponents(copy, componentNumber);
        System.Collections.Generic.HashSet<int> movedComponents = new System.Collections.Generic.HashSet<int>();
        System.Collections.Generic.HashSet<Node> selectedNodes = new System.Collections.Generic.HashSet<Node>();
        foreach (INode node in movedNodes) {
          Node copiedNode = copy.GetCopiedNode(node);
          if (copiedNode != null) {
            // remember that we nailed down this node
            selectedNodes.Add(copiedNode);
            // remember that we are moving this component
            movedComponents.Add(componentNumber.GetInt(copiedNode));
            //Update the position of the node in the CLG to match the one in the IGraph
            layout.SetCenter(copiedNode, node.Layout.X + node.Layout.Width * 0.5, node.Layout.Y + node.Layout.Height * 0.5);
            //Actually, the node itself is fixed at the start of a drag gesture
            layout.SetInertia(copiedNode, 1.0);
            //Increasing has the effect that the layout will consider this node as not completely placed...
            // In this case, the node itself is fixed, but it's neighbors will wake up
            IncreaseHeat(copiedNode, layout, 0.5);
          }
        }

        // there are components that won't be moved - nail the nodes down so that they don't spread apart infinitely
        foreach (var copiedNode in copy.Nodes) {
          if (!movedComponents.Contains(componentNumber.GetInt(copiedNode))) {
            layout.SetInertia(copiedNode, 1);
          } else {
            if (!selectedNodes.Contains(copiedNode)) {
              // make it float freely
              layout.SetInertia(copiedNode, 0);
            }
          }
        }

        // dispose the map
        copy.DisposeNodeMap(componentNumber);

        //Notify the layout algorithm that there is new work to do...
        layout.WakeUp();
      }
    }
    
    /// <summary>
    /// Notifies the layout of the new positions of the interactively moved nodes.
    /// </summary>
    private void OnMoving(object sender, InputModeEventArgs inputModeEventArgs) {
      if (layout != null) {
        CopiedLayoutGraph copy = this.copiedLayoutGraph;
        foreach (INode node in movedNodes) {
          Node copiedNode = copy.GetCopiedNode(node);
          if (copiedNode != null) {
            //Update the position of the node in the CLG to match the one in the IGraph
            layout.SetCenter(copiedNode, node.Layout.GetCenter().X, node.Layout.GetCenter().Y);
            //Increasing the heat has the effect that the layout will consider these nodes as not completely placed...
            IncreaseHeat(copiedNode, layout, 0.05);
          }
        }
        //Notify the layout algorithm that there is new work to do...
        layout.WakeUp();
      }
    }

    /// <summary>
    /// Resets the state in the layout when the user cancels the move operation.
    /// </summary>    
    private void OnMoveCanceled(object sender, InputModeEventArgs inputModeEventArgs) {
      if (layout != null) {
        CopiedLayoutGraph copy = this.copiedLayoutGraph;
        foreach (INode node in movedNodes) {
          Node copiedNode = copy.GetCopiedNode(node);
          if (copiedNode != null) {
            //Update the position of the node in the CLG to match the one in the IGraph
            layout.SetCenter(copiedNode, node.Layout.GetCenter().X, node.Layout.GetCenter().Y);
            layout.SetStress(copiedNode, 0);
          }
        }
        foreach (var copiedNode in copy.Nodes) {
          //Reset the node's inertia to be fixed
          layout.SetInertia(copiedNode, 1.0);
          layout.SetStress(copiedNode, 0);
        }
        //We don't want to restart the layout (since we canceled the drag anyway...)
      }
    }

    /// <summary>
    /// Helper method that increases the heat of the neighbors of a given node by a given value.
    /// </summary>
    /// <remarks>
    /// This will make the layout move the neighbor nodes more quickly.
    /// </remarks>
    private static void IncreaseHeat(Node copiedNode, InteractiveOrganicLayout layout, double delta) {
      //Increase Heat of neighbors
      foreach (Node neighbor in copiedNode.Neighbors) {
        double oldStress = layout.GetStress(neighbor);
        layout.SetStress(neighbor, Math.Min(1, oldStress + delta));
      }
    }

    /// <summary>
    /// Called once the interactive move is finished.
    /// </summary>
    /// <remarks>
    /// Updates the state of the interactive layout.
    /// </remarks>
    private void OnMovedFinished(object sender, InputModeEventArgs inputModeEventArgs) {
      if (layout != null) {
        CopiedLayoutGraph copy = this.copiedLayoutGraph;
        foreach (INode node in movedNodes) {
          Node copiedNode = copy.GetCopiedNode(node);
          if (copiedNode != null) {
            //Update the position of the node in the CLG to match the one in the IGraph
            layout.SetCenter(copiedNode, node.Layout.GetCenter().X, node.Layout.GetCenter().Y);
            layout.SetStress(copiedNode, 0);
          }
        }
        foreach (var copiedNode in copy.Nodes) {
          //Reset the node's inertia to be fixed
          layout.SetInertia(copiedNode, 1.0);
          layout.SetStress(copiedNode, 0);
        }
      }
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and creating a small sample graph.
    /// </summary>
    protected virtual void InitializeGraph()
    {
      IGraph graph = Graph;

      // load a sample graph
      new GraphMLIOHandler().Read(graph, "Resources/sample.graphml");

      // set some defaults
      graph.NodeDefaults.Style = Enumerable.First(graph.Nodes).Style;
      graph.NodeDefaults.ShareStyleInstance = true;

      // we start with a simple run of OrganicLayout to get a good starting result
      // the algorithm is optimized to "unfold" graphs quicker than
      // interactive organic, so we use this result as a starting solution
      var initialLayout = new OrganicLayout { MinimumNodeDistance = 50 };
      graph.ApplyLayout(initialLayout);

      // center the initial graph
      GraphControl.FitGraphBounds();

      movedNodes = new List<INode>();

      // we wrap the PositionHandler for nodes so that we always have the collection of nodes 
      // that are currently being moved available in "movedNodes".
      // this way we do not need to know how the node is moved and do not have to guess
      // what elements are currently being moved based upon selection, etc.
      graph.GetDecorator().NodeDecorator.PositionHandlerDecorator.SetImplementationWrapper(
        (item, implementation) => new CollectingPositionHandlerWrapper(item, movedNodes, implementation));

      // create a copy of the graph for the layout algorithm
      LayoutGraphAdapter adapter = new LayoutGraphAdapter(graphControl.Graph);
      copiedLayoutGraph = adapter.CreateCopiedLayoutGraph();

      // create and start the layout algorithm
      layout = StartLayout();
      WakeUp();

      // register a listener so that structure updates are handled automatically
      graph.NodeCreated += delegate(object source, ItemEventArgs<INode> args) {
                             if (layout != null) {
                               var center = args.Item.Layout.GetCenter();
                               layout.SyncStructure(true);
                               //we nail down all newly created nodes
                               var copiedNode = copiedLayoutGraph.GetCopiedNode(args.Item);
                               layout.SetCenter(copiedNode, center.X, center.Y);
                               layout.SetInertia(copiedNode, 1);
                               layout.SetStress(copiedNode, 0);
                             }
                           };
      graph.NodeRemoved += OnStructureChanged;
      graph.EdgeCreated += OnStructureChanged;
      graph.EdgeRemoved += OnStructureChanged;
    }

    /// <summary>
    /// Wraps an existing position handler and adds moved nodes to a provided shared collection.
    /// </summary>
    /// <remarks>
    /// This makes it possible to always know which nodes are being moved, no matter what code
    /// uses the PositionHandler.
    /// </remarks>
    private class CollectingPositionHandlerWrapper : ConstrainedPositionHandler
    {
      private readonly INode item;
      private readonly ICollection<INode> movedNodes;

      public CollectingPositionHandlerWrapper(INode item, ICollection<INode> movedNodes, IPositionHandler baseImplementation):base(baseImplementation) {
        this.item = item;
        this.movedNodes = movedNodes;
      }

      protected override void OnInitialized(IInputModeContext context, PointD originalLocation) {
        // we remember the item in the collection when the drag is initialized.
        movedNodes.Add(item);
      }

      protected override PointD ConstrainNewLocation(IInputModeContext context, PointD originalLocation, PointD newLocation) {
        // we do not really constrain the location - this is just a convenience class
        return newLocation;
      }
    }

    /// <summary>
    /// If the graph structure has been changed interactively, synchronize the structure with the layout algorithm.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="eventArgs"></param>
    private void OnStructureChanged(object source, EventArgs eventArgs) {
      Synchronize(source, eventArgs);
    }

    /// <summary>
    /// Create a new layout instance and start it in a new thread
    /// </summary>
    /// <returns></returns>
    private InteractiveOrganicLayout StartLayout() {
      // create the layout
      InteractiveOrganicLayout organicLayout = new InteractiveOrganicLayout { MaximumDuration = 2000 };

      // use an animator that animates an infinite animation
      var animator = new Animator(GraphControl) { AutoInvalidation = false };
      animator.Animate(delegate {
                         if (!organicLayout.Running) {
                           animator.Stop();
                           return;
                         }
                         if (organicLayout.CommitPositionsSmoothly(50, 0.05) > 0) {
                           GraphControl.Invalidate();
                         }
                       }, TimeSpan.MaxValue);

      // run the layout in a separate thread
      var thread = new Thread(new ThreadStart(delegate {
        // we run the real interactive version of the organic layout
        // previously we only calculated the initial layout using OrganicLayout
        organicLayout.ApplyLayout(copiedLayoutGraph);
        // stop the animator when the layout returns (does not normally happen at all)
        graphControl.Invoke(new Action<Animator>(animator1 => animator1.Stop()), animator);
        }));
      thread.IsBackground = true;
      thread.Start();
      
      return organicLayout;
    }

    #region Properties

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

    /// <summary>
    /// Gets the currently registered IGraphSelection instance from the GraphControl.
    /// </summary>
    public IGraphSelection Selection {
      get { return GraphControl.Selection; }
    }

    private InteractiveOrganicLayout layout;
    private CopiedLayoutGraph copiedLayoutGraph;
    private List<INode> movedNodes;

    /// <summary>
    /// Wakes the layout algorithm up to calculate an initial layout.
    /// </summary>
    private void WakeUp(object sender = null, EventArgs e = null) {
      if (layout != null) {
        // we make all nodes freely movable
        foreach (var copiedNode in copiedLayoutGraph.Nodes) {
          layout.SetInertia(copiedNode, 0);
        }
        // then wake up the layout
        layout.WakeUp();
        // and after two second we freeze the nodes again...

        var timer = new System.Windows.Forms.Timer { Interval = 2000 };
        timer.Tick += delegate {
          foreach (var copiedNode in copiedLayoutGraph.Nodes) {
            layout.SetInertia(copiedNode, 1);
          }
          timer.Stop();
        };
        timer.Start();
      }
    }

    protected void Synchronize(object sender, EventArgs e) {
      if (layout != null) {
        layout.SyncStructure(true);
      }
    }

    #endregion

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new InteractiveOrganicForm());
    }

    #endregion
  }
}
