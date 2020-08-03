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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Demo.yFiles.Layout.LogicGate.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Hierarchic;
using yWorks.Layout.Router.Polyline;

[assembly : XmlnsDefinition("http://www.yworks.com/yfiles.net/5.0/demos/LogicGateWindow", "Demo.yFiles.Layout.LogicGate")]
[assembly : XmlnsPrefix("http://www.yworks.com/yfiles.net/5.0/demos/LogicGateWindow", "demo")]

namespace Demo.yFiles.Layout.LogicGate
{
  /// <summary>
  /// This demo shows how ports can be used in a read-world example
  /// by modeling wires, inputs, and outputs of digital logic elements.
  /// </summary>
  public partial class LogicGateForm : Form
  {

    #region Initialization

    /// <summary>
    /// The default style
    /// </summary>
    private readonly LogicGateNodeStyle defaultStyle = new LogicGateNodeStyle();

    /// <summary>
    /// The orthogonal edge router
    /// </summary>
    private ILayoutAlgorithm orthogonalEdgeRouter;

    private LayoutData oerData;

    private ILayoutAlgorithm hl;

    private LayoutData hlData;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogicGateForm"/> class.
    /// </summary>
    public LogicGateForm() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      RegisterMenuCommands();
      RegisterToolStripCommands();
      PopulateNodesList();
    }

    #region Style Listbox

    /// <summary>
    /// Fill the node list that acts as a source for nodes.
    /// </summary>
    private void PopulateNodesList() {
      var graph = new DefaultGraph
                    {
                      NodeDefaults = {Size = new SizeD(50, 30), Style = defaultStyle}
                    };

      // Create some nodes
      CreateNode(graph, PointD.Origin, LogicGateType.And);
      CreateNode(graph, PointD.Origin, LogicGateType.Nand);
      CreateNode(graph, PointD.Origin, LogicGateType.Or);
      CreateNode(graph, PointD.Origin, LogicGateType.Nor);
      CreateNode(graph, PointD.Origin, LogicGateType.Not);
      // Create an IC
      CreateNode(graph, PointD.Origin, LogicGateType.Timer, "555", new SizeD(70, 120));
      CreateNode(graph, PointD.Origin, LogicGateType.ADConverter, "2-bit A/D\nConverter", new SizeD(70, 120));

      foreach (var node in graph.Nodes) {
        nodeStyleListBox.Items.Add(node);
      }
      nodeStyleListBox.ItemHeight = 65;
      //Handle list item drawing
      nodeStyleListBox.DrawItem += nodeStyleListBox_DrawItem;

      // enable drag and drop
      nodeStyleListBox.MouseDown += nodeStyleListBox_MouseDown;
    }

    /// <summary>
    /// Creates a node of the specified type.
    /// </summary>
    /// <remarks>
    /// The method will specify the ports that the node should have based on its type.
    /// </remarks>
    private void CreateNode(IGraph graph, PointD location, LogicGateType type, string label = null, SizeD? size = null) {
      RectD newBounds = RectD.FromCenter(location, graph.NodeDefaults.Size);
      INode node;
      if (type >= LogicGateType.Timer) {
        node = graph.CreateNode(RectD.FromCenter(location, (SizeD)size), new ShapeNodeStyle {
            Pen = new Pen(Brushes.Black, 2)
        });
      } else {
        node = graph.CreateNode(newBounds, new LogicGateNodeStyle { GateType = type });
      }

      if (label != null) {
        graph.AddLabel(node, label, InteriorLabelModel.Center);
      }

      var portDescriptors = PortDescriptor.CreatePortDescriptors(type);

      // use relative port locations
      var model = new FreeNodePortLocationModel();

      // add ports for all descriptors using the descriptor as the tag of the port
      foreach (var descriptor in portDescriptors) {
        // use the descriptor's location as offset 
        var portLocationModelParameter = model.CreateParameter(PointD.Origin, new PointD(descriptor.X, descriptor.Y));
        graph.AddPort(node, portLocationModelParameter, tag : descriptor);
      }
    }

    /// <summary>
    /// Handles the MouseDown event of the nodeStyleListBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
    private void nodeStyleListBox_MouseDown(object sender, MouseEventArgs e) {
      ListBox listBox = (ListBox)sender;
      if (e.Button == MouseButtons.Left) {
        int indexOfItemUnderMouseToDrag = listBox.IndexFromPoint(e.X, e.Y);
        var node = (INode)listBox.Items[indexOfItemUnderMouseToDrag];
        //SetStyleDefaultCommand.Execute(node, graphControl);
        // Get the index of the item the mouse is below.
        if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {
          DataObject dao = new DataObject();

          //Initialize drag operation if we actually did hit something
          dao.SetData(typeof(INode), node);
          listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
        }
      } else if (e.Button == MouseButtons.Right) {
        // select clicked item and show context menu
        listBox.SelectedIndex = listBox.IndexFromPoint(e.X, e.Y);
        //SetStyleDefaultCommand.Execute(listBox.SelectedItem, graphControl);
        nodeStyleListBox.ContextMenuStrip.Show();
      }
    }

    /// <summary>
    /// Paint the node style representation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void nodeStyleListBox_DrawItem(object sender, DrawItemEventArgs e) {
      ListBox listBox = (ListBox)sender;
      int i = e.Index;
      INode node = (INode)listBox.Items[i];
      node = new SimpleNode {
          Labels = node.Labels,
          Layout = node.Layout,
          Style = node.Style,
          Tag = node.Tag
      };
      if (node.Layout.Height > 50) {
        ((SimpleNode) node).Layout = RectD.FromCenter(node.Layout.GetCenter(), new SizeD(20, 30));
      }

      Rectangle bounds = e.Bounds;
      Rectangle insets = Rectangle.FromLTRB(5, 5, 5, 25);

      Graphics g = e.Graphics;
      SmoothingMode oldMode = g.SmoothingMode;
      Region oldClip = g.Clip;

      // in .net 3.5 there are repaint issues - none of the below seems to help, there
      // are still sometimes background rendering artifacts left over.
      g.IntersectClip(bounds);
      g.FillRegion(new SolidBrush(e.BackColor), g.Clip);
      g.Clear(e.BackColor);
      e.DrawBackground();

      var sx = (float)((bounds.Width - insets.Left - insets.Right) / node.Layout.Width);
      var sy = (float)((bounds.Height - insets.Top - insets.Bottom) / node.Layout.Height);

      if (sx > 0 && sy > 0) {
        var transform = g.Transform;
        g.SmoothingMode = SmoothingMode.HighQuality;

        g.TranslateTransform((float)(bounds.X + insets.Left), (float)(bounds.Y + insets.Top));
        g.ScaleTransform(Math.Min(sx, sy), Math.Min(sx, sy));
        g.TranslateTransform((float)(-node.Layout.X), (float)(-node.Layout.Y));

        //Get the renderer from the style, this requires the dummy node instance.
        var renderContext = new RenderContext(g, null) { ViewTransform = g.Transform, WorldTransform = g.Transform };
        node.Style.Renderer.GetVisualCreator(node, node.Style).CreateVisual(renderContext).Paint(renderContext, g);
        var lgns = node.Style as LogicGateNodeStyle;
        if (lgns != null) {
          g.DrawString(lgns.GateType.ToString(), new Font("Arial", 8.25f), Brushes.Black, -10, 20);
        } else if (node.Labels.Any()) {
          g.DrawString(node.Labels.First().Text.Replace("\n", " "), new Font("Arial", 8.25f), Brushes.Black, -10, 20);
        }
        g.Transform = transform;
        g.SmoothingMode = oldMode;
      }

      g.Clip = oldClip;
      e.DrawFocusRectangle();
    }

    #endregion


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
    private async void Demo_Load(object sender, EventArgs e) {
      InitializeLayout();
      await InitializeGraph();

      // initialize the input mode
      InitializeInputModes();
      edgeCreationPolicyComboBox.SelectedIndex = 0;
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and creating a small sample graph.
    /// </summary>
    protected virtual async Task InitializeGraph() {
      IGraph graph = graphControl.Graph;

      // set the default style for all new nodes
      graph.NodeDefaults.Style = defaultStyle;
      graph.NodeDefaults.Size = new SizeD(50, 30);

      // set the default style for all new node labels
      graph.NodeDefaults.Labels.Style = new DefaultLabelStyle();

      // set the default style for all new edge labels
      graph.EdgeDefaults.Style = new PolylineEdgeStyle {
        SourceArrow = new Arrow { Type = ArrowType.None },
        TargetArrow = new Arrow { Type = ArrowType.None },
        Pen = new Pen(Brushes.Black, 3) { EndCap = LineCap.Square, StartCap = LineCap.Square }
      };

      // disable edge cropping
      graph.GetDecorator().PortDecorator.EdgePathCropperDecorator.HideImplementation();

      // don't delete ports a removed edge was connected to
      graph.NodeDefaults.Ports.AutoCleanUp = false;

      // set a custom port candidate provider
      graphControl.Graph.GetDecorator().NodeDecorator.PortCandidateProviderDecorator.SetImplementation(new DescriptorDependentPortCandidateProvider());

      // read initial graph from embedded resource
      graphControl.ImportFromGraphML("Resources\\defaultGraph.graphml");
      
      // do the layout
      await ApplyLayout(hl, hlData, true);
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl,
    /// a <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <remarks>
    /// The control uses a custom node creation callback that creates business objects for newly
    /// created nodes.
    /// </remarks>
    private void InitializeInputModes() {
      var mode = new GraphEditorInputMode {
          // don't allow nodes to be created using a mouse click
          AllowCreateNode = false,
          // don't allow bends to be created using a mouse drag on an edge
          AllowCreateBend = false,
          // disable node resizing
          ShowHandleItems = GraphItemTypes.Bend | GraphItemTypes.Edge,
          // enable orthogonal edge creation and editing
          OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext(),
          // enable drag and drop
          NodeDropInputMode = { Enabled = true },
          // disable moving labels
          MoveLabelInputMode = { Enabled = false },
          // enable snapping for easier orthogonal edge editing
          SnapContext = new GraphSnapContext { Enabled = true },
          CreateEdgeInputMode = {
              // only allow starting an edge creation over a valid port candidate
              StartOverCandidateOnly = true,
              // show all port candidates when hovering over a node
              ShowPortCandidates = ShowPortCandidates.All
          }
      };
      // wrap the original node creator so it copies the ports and labels from the dragged node
      var originalNodeCreator = mode.NodeDropInputMode.ItemCreator;
      mode.NodeDropInputMode.ItemCreator =
        (context, graph, draggedNode, dropTarget, layout) => {
          if (draggedNode != null) {
            var newNode = originalNodeCreator(context, graph, new SimpleNode { Style = draggedNode.Style, Layout = draggedNode.Layout }, dropTarget, layout);
            // copy the ports
            foreach (var port in draggedNode.Ports) {
              var descriptor = (PortDescriptor) port.Tag;
              var portStyle = new NodeStylePortStyleAdapter(new ShapeNodeStyle {
                Brush = descriptor.EdgeDirection == EdgeDirection.In ? Brushes.Green : Brushes.DodgerBlue,
                Pen = null,
                Shape = ShapeNodeShape.Rectangle
              }) { RenderSize = new SizeD(5, 5) };
              var newPort = graph.AddPort(newNode, port.LocationParameter, portStyle, port.Tag);
              // create the port labels
              var parameter = new InsideOutsidePortLabelModel().CreateOutsideParameter();
              graph.AddLabel(newPort, descriptor.LabelText, parameter, tag: descriptor);
            }
            // copy the labels
            foreach (var label in draggedNode.Labels) {
              graph.AddLabel(newNode, label.Text, label.LayoutParameter, label.Style, tag: label.Tag);
            }
            return newNode;
          }
          // fallback
          return originalNodeCreator(context, graph, draggedNode, dropTarget, layout);
        };

      mode.CreateEdgeInputMode.EdgeCreated += (sender, args) => {
        var sourcePortLabel = args.SourcePort.Labels.FirstOrDefault();
        var targetPortLabel = args.TargetPort.Labels.FirstOrDefault();
        if (sourcePortLabel != null) {
          ReplaceLabelModel(args.SourcePort, sourcePortLabel);
        }
        if (targetPortLabel != null) {
          ReplaceLabelModel(args.TargetPort, targetPortLabel);
        }
      };

      graphControl.Graph.EdgeRemoved += (sender, args) => {
        var sourcePortLabel = args.SourcePort.Labels.FirstOrDefault();
        var targetPortLabel = args.TargetPort.Labels.FirstOrDefault();
        if (sourcePortLabel != null && !graphControl.Graph.EdgesAt(args.SourcePort).Any()) {
          graphControl.Graph.SetLabelLayoutParameter(sourcePortLabel,
            new InsideOutsidePortLabelModel().CreateOutsideParameter());
        }
        if (targetPortLabel != null && !graphControl.Graph.EdgesAt(args.TargetPort).Any()) {
          graphControl.Graph.SetLabelLayoutParameter(targetPortLabel,
            new InsideOutsidePortLabelModel().CreateOutsideParameter());
        }
      };

      graphControl.InputMode = mode;
    }

    private void ReplaceLabelModel(IPort port, ILabel label) {
      var descriptor = (PortDescriptor) port.Tag;
      graphControl.Graph.SetLabelLayoutParameter(label, descriptor.LabelPlacementWithEdge);
    }

    /// <summary>
    /// Initializes the layout algorithm and its layout data.
    /// </summary>
    private void InitializeLayout() {
      orthogonalEdgeRouter = new EdgeRouter {
        ConsiderNodeLabels = true
      };

      hl = new HierarchicLayout {
        OrthogonalRouting = true,
        LayoutOrientation = LayoutOrientation.LeftToRight,
        ConsiderNodeLabels = true
      };

      // outgoing edges must be routed to the right of the node
      // we use the same value for all edges, which is a strong port constraint that forces
      // the edge to leave at the east (right) side
      var east = PortConstraint.Create(PortSide.East, true);
      // incoming edges must be routed to the left of the node
      // we use the same value for all edges, which is a strong port constraint that forces
        // the edge to enter at the west (left) side
      var west = PortConstraint.Create(PortSide.West, true);

      MapperDelegate<IEdge, PortConstraint> sourceDelegate = edge => ((PortDescriptor) edge.SourcePort.Tag).X == 0 ? west : east;
      MapperDelegate<IEdge, PortConstraint> targetDelegate = edge => ((PortDescriptor) edge.TargetPort.Tag).X == 0 ? west : east;
      oerData = new PolylineEdgeRouterData {
        SourcePortConstraints = { Delegate = sourceDelegate},
        TargetPortConstraints = { Delegate = targetDelegate }
      };

      hlData = new HierarchicLayoutData {
        SourcePortConstraints = { Delegate = sourceDelegate },
        TargetPortConstraints = { Delegate = targetDelegate }
      };
    }
    
    #endregion

    #region Event handlers

    /// <summary>
    /// Exits the demo.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private async void OnRunIHLButtonClicked(object sender, EventArgs e) {
      await ApplyLayout(hl, hlData, false);
    }

    /// <summary>
    /// Formats the current graph.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private async void OnRunOrthoButtonClicked(object sender, EventArgs e) {
      await ApplyLayout(orthogonalEdgeRouter, oerData, true);
    }

    #endregion

    #region Graph creation and layout

    /// <summary>
    /// Perform the layout operation
    /// </summary>
    private async Task ApplyLayout(ILayoutAlgorithm layout, LayoutData layoutData, bool animateViewport) {
      // layout starting, disable button
      
      toolStripButton2.Enabled = false;
      toolStripButton3.Enabled = false;
      // do the layout
      var executor = new LayoutExecutor(graphControl, layout)
      {
          LayoutData = layoutData,
          Duration = TimeSpan.FromSeconds(1),
          AnimateViewport = animateViewport
      };
      await executor.Start();

      // layout finished, enable layout button again
      toolStripButton2.Enabled = true;
      toolStripButton3.Enabled = true;
    }

    #endregion
    /// <summary>
    /// Perform the layout operation
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new LogicGateForm());
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private void EdgeCreationPolicyComboBox_OnSelectionChanged(object sender, EventArgs e) {
      if (graphControl != null && graphControl.InputMode != null) {
        ((GraphEditorInputMode) graphControl.InputMode).CreateEdgeInputMode.EdgeDirectionPolicy =
          (EdgeDirectionPolicy) edgeCreationPolicyComboBox.SelectedItem;
      }
    }
  }

  #region PortCandidateProvider implementation

  /// <summary>
  /// <see cref="IPortCandidateProvider"/> implementation. Provides all available ports with the specified
  /// edge direction.
  /// </summary>
  public class DescriptorDependentPortCandidateProvider : IPortCandidateProvider
  {
    /// <summary>
    /// not queried
    /// </summary>
    /// <returns>no elements</returns>
    public IEnumerable<IPortCandidate> GetSourcePortCandidates(IInputModeContext context, IPortCandidate target) {
      return GetCandidatesForDirection(EdgeDirection.Out, context);
    }

    public IEnumerable<IPortCandidate> GetTargetPortCandidates(IInputModeContext context, IPortCandidate source) {
      return GetCandidatesForDirection(EdgeDirection.In, context);
    }

    public IEnumerable<IPortCandidate> GetSourcePortCandidates(IInputModeContext context) {
      return GetCandidatesForDirection(EdgeDirection.Out, context);
    }

    public IEnumerable<IPortCandidate> GetTargetPortCandidates(IInputModeContext context) {
      return GetCandidatesForDirection(EdgeDirection.In, context);
    }

    /// <summary>
    /// Returns the suitable candidates based on the specified <see cref="EdgeDirection"/>.
    /// </summary>
    private IEnumerable<IPortCandidate> GetCandidatesForDirection(EdgeDirection direction, IInputModeContext context) {
      // If EdgeDirectionPolicy.DetermineFromPortCandidates is used, CreateEdgeInputMode queries GetSourcePortCandidates
      // as well as GetTargetPortCandidates to collect possible port candidates to start the edge creation.
      // In this case this method is called twice (with EdgeDirection.In and EdgeDirection.Out) so for each call we
      // should only return the *valid* port candidates of a port as otherwise for each port a valid as well as an invalid
      // candidate is returned.
      var provideAllCandidates = true;
      var ceim = context.ParentInputMode as CreateEdgeInputMode;
      if (ceim != null) {
        // check the edge direction policy as well as whether candidates are collected for starting or ending the edge creation
        provideAllCandidates = ceim.EdgeDirectionPolicy != EdgeDirectionPolicy.DetermineFromPortCandidates
                               || ceim.IsCreationInProgress;
      }

      var candidates = new List<IPortCandidate>();
      // iterate over all available ports
      foreach (var port in context.GetGraph().Ports) {
        // create a port candidate, invalidate it (so it is visible but not usable)
        var candidate = new DefaultPortCandidate(port) {Validity = PortCandidateValidity.Invalid};
        // get the port descriptor
        var portDescriptor = port.Tag as PortDescriptor;
        // make the candidate valid if the direction is the same as the one supplied
        if (portDescriptor != null && portDescriptor.EdgeDirection == direction) {
          candidate.Validity = PortCandidateValidity.Valid;
        }
        // add the candidate to the list
        if (provideAllCandidates || candidate.Validity == PortCandidateValidity.Valid) {
          candidates.Add(candidate);
        }
      }
      // and return the list
      return candidates;
    }
  }

  #endregion

  #region Business logic

  /// <summary>
  /// Specifies the type of a logical gate node.
  /// </summary>
  public enum LogicGateType
  {
    And,
    Nand,
    Or,
    Nor,
    Not,
    Timer,
    ADConverter
  }

  #endregion
}
