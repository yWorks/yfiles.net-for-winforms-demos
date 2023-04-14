/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.5.
 ** Copyright (c) 2000-2023 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Demo.yFiles.Layout.PortCandidateDemo.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Hierarchic;

[assembly : XmlnsDefinition("http://www.yworks.com/yfilesNET/5.0/demos/PortCandidateWindow", "Demo.yFiles.Layout.PortCandidateDemo")]
[assembly : XmlnsPrefix("http://www.yworks.com/yfilesNET/5.0/demos/PortCandidateWindow", "demo")]

namespace Demo.yFiles.Layout.PortCandidateDemo
{

  /// <summary>
  /// This demo shows how to use PortCandidateSets in conjunction with <see cref="HierarchicLayout"/>.
  /// </summary>
  public partial class PortCandidateDemo : Form
  {

    #region Graph creation and layout

    /// <summary>
    /// Perform the layout operation
    /// </summary>
    private async Task ApplyLayout() {
      // layout starting, disable button
      runButton.Enabled = false;

      // create the layout algorithm
      var layout = new HierarchicLayout
      {
        OrthogonalRouting = true,
        LayoutOrientation = LayoutOrientation.TopToBottom
      };

      // do the layout
      await graphControl.MorphLayout(layout,
        TimeSpan.FromSeconds(1),
        new HierarchicLayoutData {
          //Automatically determine port constraints for source and target
          NodePortCandidateSets = {
            Delegate = node => {
              var candidateSet = new PortCandidateSet();
              // iterate the port descriptors
              var descriptors = PortDescriptor.CreatePortDescriptors(((FlowChartNodeStyle)node.Style).FlowChartType);
              foreach (var portDescriptor in descriptors) {
                PortCandidate candidate;
                // isn't a fixed port candidate (location is variable)
                if (portDescriptor.X == int.MaxValue) {
                  // create a port candidate at the specified side (east, west, north, south) and apply a cost to it
                  candidate = PortCandidate.CreateCandidate(portDescriptor.Side, portDescriptor.Cost);
                } else {
                  // create a candidate at a fixed location and side
                  var x = portDescriptor.X - node.Layout.Width / 2;
                  var y = portDescriptor.Y - node.Layout.Height / 2;
                  candidate = PortCandidate.CreateCandidate(x, y, portDescriptor.Side, portDescriptor.Cost);
                }
                candidateSet.Add(candidate, portDescriptor.Capacity);
              }
              return candidateSet;
            }
          },
          SourceGroupIds = { Delegate = edge => {
            // create bus-like edge groups for outgoing edges at Start nodes
            var sourceNode = edge.SourcePort.Owner as INode;
            if (sourceNode != null && ((((FlowChartNodeStyle)sourceNode.Style).FlowChartType)) == FlowChartType.Start) {
              return sourceNode;
            }
            return null;
          }},

          TargetGroupIds = { Delegate = edge => {
            // create bus-like edge groups for incoming edges at Branch nodes
            var targetNode = edge.TargetPort.Owner as INode;
            if (targetNode != null && (((FlowChartNodeStyle)targetNode.Style).FlowChartType) == FlowChartType.Branch) {
              return targetNode;
            }
            return null;
          }}
        });

      // enable button again
      runButton.Enabled = true;
    }

    #endregion

    #region Initialization

    /// <summary>
    /// The default style
    /// </summary>
    private readonly FlowChartNodeStyle defaultStyle = new FlowChartNodeStyle();

    /// <summary>
    /// Initializes a new instance of the <see cref="PortCandidateDemo"/> class.
    /// </summary>
    public PortCandidateDemo() {
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
      var graph = new DefaultGraph();

      // Create some nodes
      CreateNode(graph, PointD.Origin, FlowChartType.Start);
      CreateNode(graph, PointD.Origin, FlowChartType.Operation);
      CreateNode(graph, PointD.Origin, FlowChartType.Branch);
      CreateNode(graph, PointD.Origin, FlowChartType.End);

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
    /// Method that creates a node of the specified type. The method will specify the ports
    /// that the node should have based on its type.
    /// </summary>
    private static void CreateNode(IGraph graph, PointD location, FlowChartType type) {
      var size = FlowChartNodeStyle.GetNodeTypeSize(type);
      RectD newBounds = new RectD(location, size);
      graph.CreateNode(newBounds, new FlowChartNodeStyle {FlowChartType = type});
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
          dao.SetData(typeof(INode), new SimpleNode { Style = node.Style, Layout = node.Layout });
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

      Rectangle bounds = e.Bounds;
      Rectangle insets = Rectangle.FromLTRB(5, 5, 5, 25);

      Graphics g = e.Graphics;
      SmoothingMode oldMode = g.SmoothingMode;
      Region oldClip = g.Clip;

      // in .net 3.5 there are repaint issues - none of the below seems to help, there
      // are still sometimes background rendering artefacts left over.
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
        g.TranslateTransform((float)(-node.Layout.GetCenter().X+35), (float)(-node.Layout.Y));

        //Get the renderer from the style, this requires the dummy node instance.
        var renderContext = new RenderContext(g, null) { ViewTransform = g.Transform, WorldTransform = g.Transform };
        node.Style.Renderer.GetVisualCreator(node, node.Style).CreateVisual(renderContext).Paint(renderContext, g);
        g.Transform = transform;
        g.SmoothingMode = oldMode;
      }

      g.Clip = oldClip;
      e.DrawFocusRectangle();
    }

    #endregion

    /// <summary>
    /// Registers the tool strip commands.
    /// </summary>
    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
      openButton.SetCommand(Commands.Open, graphControl);
      saveButton.SetCommand(Commands.SaveAs, graphControl);
    }

    private void RegisterMenuCommands() {
      // File menu
      openFileToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveAsToolStripMenuItem.SetCommand(Commands.SaveAs, graphControl);

      // View menu
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitGraphBoundsToolStripMenuItem.SetCommand(Commands.FitContent, graphControl);
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeInputModes"/>
    /// <seealso cref="InitializeGraph"/>
    private void Demo_Load(object sender, EventArgs e) {
      InitializeGraph();
      InitializeInputModes();
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and creating a small sample graph.
    /// </summary>
    private async Task InitializeGraph() {
      IGraph graph = graphControl.Graph;
      // set the style as the default for all new nodes
      graph.NodeDefaults.Style = defaultStyle;

      // set the default style for all new node labels
      graph.NodeDefaults.Labels.Style = new DefaultLabelStyle {Font = new Font("Arial", 12)};

      // set the default style for all new edge labels
      graph.EdgeDefaults.Style = new PolylineEdgeStyle
      {
        SourceArrow = new Arrow { Type = ArrowType.None },
        TargetArrow = new Arrow { Type = ArrowType.None },
        Pen = new Pen(Brushes.DarkSlateGray, 1)
      };

      // edges should be painted last - be at the highest layer
      graphControl.GraphModelManager.EdgeGroup.ToFront();

      // don't delete ports a removed edge was connected to
      graph.NodeDefaults.Ports.AutoCleanUp = false;

      // newly created edges will always connect to the node's center
      graph.GetDecorator().NodeDecorator.PortCandidateProviderDecorator.SetFactory(
        PortCandidateProviders.FromNodeCenter);

      ReadSampleGraph();

      await ApplyLayout();
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
      GraphEditorInputMode mode = new GraphEditorInputMode {
        // don't allow nodes to be created using a mouse click
        AllowCreateNode = false,
        // disable node resizing
        ShowHandleItems = GraphItemTypes.Bend | GraphItemTypes.Edge,
        // edge creation - uncomment to allow edges only to be created orthogonally
        // CreateEdgeInputMode = {OrthogonalEdgeCreation = true},
        // OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext(),
        // enable drag and drop
        NodeDropInputMode = {Enabled = true}
      };
      // wrap the original node creator so it removes the label from the dragged node
      var originalNodeCreator = mode.NodeDropInputMode.ItemCreator;
      mode.NodeDropInputMode.ItemCreator =
        (context, graph, draggedNode, dropTarget, layout) =>
        {
          return originalNodeCreator(context, graph, new SimpleNode { Style = draggedNode.Style, Layout = draggedNode.Layout }, dropTarget, layout);
        };
      return mode;
    }

    /// <summary>
    /// Reads the sample graph.
    /// </summary>
    private void ReadSampleGraph() {
      graphControl.ImportFromGraphML("Resources\\defaults.graphml");
    }

    #endregion

    #region Event handlers

    /// <summary>
    /// Called when the run button is clicked.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void OnRunButtonClicked(object sender, EventArgs e) {
      ApplyLayout();
    }

    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new PortCandidateDemo());
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }
  }

  #region Business logic

  /// <summary>
  /// Specifies the type of the node.
  /// </summary>
  public enum FlowChartType
  {
    Start,
    Operation,
    Branch,
    End
  }

  #endregion
}
