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
using System.Linq;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.DragAndDrop.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Input.DragAndDrop
{

  /// <summary>
  /// This demo creates a simple style chooser that shows how to use class <see cref="NodeDropInputMode"/> for drag and drop.
  /// In contrast to <see cref="DropInputMode"/>, <see cref="NodeDropInputMode"/> shows a preview of the node
  /// while dragging, leverages snapping and allows for dropping nodes into group nodes.
  /// </summary>
  /// <remarks>To create a node, drag the desired node style from the left panel onto the canvas. See the dragged node
  /// snap to the grid positions and to other nodes.</remarks>
  public partial class DragAndDropForm
  {
    private string[] functionOptions = { "Snapping & Preview", "Preview", "None" };


    /// <summary>
    /// Enables support for dropping nodes on the given <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <param name="editorInputMode">The GraphEditorInputMode for this application.</param>
    private void ConfigureNodeDropInputMode(GraphEditorInputMode editorInputMode) {
      // Obtain an input mode for handling dropped nodes for the GraphEditorInputMode.
      var nodeDropInputMode = editorInputMode.NodeDropInputMode;
      // by default the mode available in GraphEditorInputMode is disabled, so first enable it
      nodeDropInputMode.Enabled = true;

      // we want nodes that have a GroupNodeStyle assigned to be created as group nodes.
      nodeDropInputMode.IsGroupNodePredicate = draggedNode => draggedNode.Style is GroupNodeStyle;

      // we enable dropping nodes onto leaf nodes ...
      nodeDropInputMode.AllowNonGroupNodeAsParent = true;
      // ... but we restrict that feature to the third nodes (and group nodes).
      nodeDropInputMode.IsValidParentPredicate =
        node =>
          graphControl.Graph.IsGroupNode(node) ||
          (node.Labels.Count == 2 && node.Labels[1].Text.Contains("convert"));
      
      var labelDropInputMode = editorInputMode.LabelDropInputMode;
      labelDropInputMode.Enabled = true;
      labelDropInputMode.AutoEditLabel = true;
      labelDropInputMode.UseBestMatchingParameter = true;
      var portDropInputMode = editorInputMode.PortDropInputMode;
      portDropInputMode.Enabled = true;
      portDropInputMode.UseBestMatchingParameter = true;
    }

    public DragAndDropForm() {
      InitializeComponent();

      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      InitializeStylesList();

            // add the visual grid
      const int gridWidth = 80;
      GridInfo gridInfo = new GridInfo(gridWidth);
      
      var grid = new GridVisualCreator(gridInfo);
      graphControl.BackgroundGroup.AddChild(grid, CanvasObjectDescriptors.AlwaysDirtyInstance);

      // Create and configure a GraphSnapContext to enable snapping
      var context = new GraphSnapContext
      {
        NodeToNodeDistance = 30,
        NodeToEdgeDistance = 20,
        SnapOrthogonalMovement = false,
        SnapDistance = 10,
        SnapSegmentsToSnapLines = true,
        NodeGridConstraintProvider = new GridConstraintProvider<INode>(gridInfo),
        BendGridConstraintProvider = new GridConstraintProvider<IBend>(gridInfo),
        SnapBendsToSnapLines = true,
        GridSnapType = GridSnapTypes.All
      };

      // Create and register a graph editor input mode for editing the graph
      // in the canvas.
      var editorInputMode = new GraphEditorInputMode();
      editorInputMode.SnapContext = context;
      editorInputMode.AllowGroupingOperations = true;

      ConfigureNodeDropInputMode(editorInputMode);

      // use the mode in our control
      graphControl.InputMode = editorInputMode;


      // Set the default node style to the style of the first item in the list of nodes
      if (styleListBox.Items.Count > 0) {
        INode node = styleListBox.Items[0] as INode;
        if (node != null) {
          graphControl.Graph.NodeDefaults.Style = node.Style;
        }
      }
      graphControl.Graph.SetUndoEngineEnabled(true);

      DemoStyles.InitDemoStyles(graphControl.Graph);

      // populate the control with some nodes
      CreateSampleGraph();

      featuresComboBox.Items.AddRange(functionOptions);
      featuresComboBox.SelectedIndex = 0;
    }

    private void CreateSampleGraph() {
      // Create a group node in which the dragged node can be dropped
      INode groupNode = graphControl.Graph.CreateGroupNode(null, new RectD(100, 100, 70, 70));
      graphControl.Graph.AddLabel(groupNode, "Group Node");
      graphControl.Graph.AddLabel(groupNode, "Drop a node over me!", ExteriorLabelModel.South);

      // Create a node to which the dragged node can snap
      INode node1 = graphControl.Graph.CreateNode(new RectD(300, 100, 30, 30));
      graphControl.Graph.AddLabel(node1, "Sample Node", ExteriorLabelModel.North);
      graphControl.Graph.AddLabel(node1, "Drag a node near me!", ExteriorLabelModel.South);

      // Create a node which can be converted to a group node automatically, if a node is dropped onto it
      INode node2 = graphControl.Graph.CreateNode(new RectD(450, 200, 30, 30), DemoStyles.CreateDemoNodeStyle(Themes.PaletteGreen));
      graphControl.Graph.AddLabel(node2, "Sample Node", ExteriorLabelModel.North);
      graphControl.Graph.AddLabel(node2, "Drag a node onto me to convert me to a group node!", ExteriorLabelModel.South);
    }

    #region Initialize node palette

    /// <summary>
    /// Initializes the style panel of this demo.
    /// </summary>
    private void InitializeStylesList() {
      //Handle list item drawing
      styleListBox.DrawItem += styleListBox_DrawItem;

      // register for the mouse down event to initiate the drag operation
      styleListBox.MouseDown += styleListBox_MouseDown;

      const int nodeWidth = 60;
      const int nodeHeight = 40;

      // Create a new Graph in which the palette nodes live
      IGraph nodeContainer = new DefaultGraph();
      DemoStyles.InitDemoStyles(nodeContainer);
      
      var defaultLabelStyle = new DefaultLabelStyle{
        BackgroundPen = new Pen(new SolidBrush(Color.FromArgb(101, 152, 204)), 1),
        BackgroundBrush = Brushes.White,
        Insets = new InsetsD(3, 5, 3, 5)
      };

      nodeContainer.NodeDefaults.Labels.Style = defaultLabelStyle;
      nodeContainer.EdgeDefaults.Labels.Style = defaultLabelStyle;
      
      // Create some nodes
      nodeContainer.CreateNode(new RectD(0, 0, nodeWidth, nodeHeight), DemoStyles.CreateDemoShapeNodeStyle(ShapeNodeShape.Rectangle));
      nodeContainer.CreateNode(new RectD(0, 0, nodeWidth, nodeHeight));

      INode node = nodeContainer.CreateGroupNode(layout:new RectD(0, 0, 70, 70));
      nodeContainer.AddLabel(node, "Group Node");
      
      var nodeLabelContainer = nodeContainer.CreateNode(new RectD(0, 0, 70, 70), VoidNodeStyle.Instance, "Node Label Container");
      var nodeLabel = nodeContainer.AddLabel(nodeLabelContainer, "Node Label", InteriorLabelModel.Center);

      var edgeLabelContainer = nodeContainer.CreateNode(new RectD(0, 0, 70, 70), VoidNodeStyle.Instance, "Edge Label Container");
      var edgeLabelTemplate = nodeContainer.AddLabel(edgeLabelContainer, "Edge Label", FreeNodeLabelModel.Instance.CreateDefaultParameter());

      // Add nodes to listview
      foreach (INode n in nodeContainer.Nodes) {
        styleListBox.Items.Add(n);
      }
    }

    #endregion

    #region Combobox events

    private void FeatureSelectionChanged(object sender, EventArgs e) {
      var nodeDropInputMode = ((GraphEditorInputMode) graphControl.InputMode).NodeDropInputMode;
      var labelDropInputMode = ((GraphEditorInputMode) graphControl.InputMode).LabelDropInputMode;
      var portDropInputMode = ((GraphEditorInputMode) graphControl.InputMode).PortDropInputMode;
      switch (featuresComboBox.SelectedIndex) {
        case 0:
          nodeDropInputMode.SnappingEnabled = true;
          nodeDropInputMode.ShowPreview = true;
          labelDropInputMode.ShowPreview = true;
          portDropInputMode.SnappingEnabled = true;
          portDropInputMode.ShowPreview = true;
          break;
        case 1:
          nodeDropInputMode.SnappingEnabled = false;
          nodeDropInputMode.ShowPreview = true;
          labelDropInputMode.ShowPreview = true;
          portDropInputMode.SnappingEnabled = false;
          portDropInputMode.ShowPreview = true;
          break;
        case 2:
          nodeDropInputMode.SnappingEnabled = false;
          nodeDropInputMode.ShowPreview = false;
          labelDropInputMode.ShowPreview = false;
          portDropInputMode.SnappingEnabled = false;
          portDropInputMode.ShowPreview = false;
          break;
      }
    }

    #endregion


    private void styleListBox_MouseDown(object sender, MouseEventArgs e) {
      ListBox listBox = (ListBox)sender;
      // Get the index of the item the mouse is below.
      int indexOfItemUnderMouseToDrag = listBox.IndexFromPoint(e.X, e.Y);

      if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {
        var node = (INode)listBox.Items[indexOfItemUnderMouseToDrag];
        if (node.Tag == "Node Label Container") {
            DataObject dao = new DataObject();
            dao.SetData(typeof(ILabel), node.Labels.First());
            listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
          }
          else if (node.Tag == "Edge Label Container") {
            var labelTemplate = node.Labels.First();
            //Not all label models return a valid geometry when the path is empty
            var p1 = new SimplePort(new SimpleNode() {Layout = new RectD(PointD.Origin, new SizeD(1, 1))}, FreeNodePortLocationModel.NodeCenterAnchored);
            var p2 = new SimplePort(new SimpleNode() {Layout = new RectD(new PointD(0, 100), new SizeD(1, 1))}, FreeNodePortLocationModel.NodeCenterAnchored);
            var edge = new SimpleEdge(p1, p2);
            var dummyLabel = new SimpleLabel(edge, labelTemplate.Text,
                FreeEdgeLabelModel.Instance.CreateDefaultParameter()) {
                  Style = labelTemplate.Style,
                  Tag = labelTemplate.Tag,
                  PreferredSize = labelTemplate.PreferredSize
                };

            DataObject dao = new DataObject();
            dao.SetData(typeof (ILabel), dummyLabel);
            listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
          }
          else if (node.Tag == "Port Container") {
            DataObject dao = new DataObject();
            dao.SetData(typeof(IPort), node.Ports.First());
            listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
          } else {
            // use as defaults for newly created nodes
            graphControl.Graph.NodeDefaults.Style = node.Style;
            graphControl.Graph.NodeDefaults.Size = node.Layout.ToSizeD();

            DataObject dao = new DataObject();
            //Initialize drag operation if we actually did hit something
            dao.SetData(typeof (INode), node);
            listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
          }
      }
    }

    /// <summary>
    /// Paint the style representation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void styleListBox_DrawItem(object sender, DrawItemEventArgs e) {
      ListBox listBox = (ListBox) sender;
      int i = e.Index;
      e.DrawBackground();
      INode node = listBox.Items[i] as INode;

      var bounds = e.Bounds;
      InsetsD insets = new InsetsD(2);

      var canvasControl = new GraphControl();
      canvasControl.Size = new Size(bounds.Width - (int) insets.HorizontalInsets,
                                    bounds.Height - (int) insets.VerticalInsets);
      Bitmap bm = new Bitmap(canvasControl.Size.Width, canvasControl.Size.Height);
      Graphics g = Graphics.FromImage(bm);
      canvasControl.HorizontalScrollBarPolicy = ScrollBarVisibility.Never;
      canvasControl.VerticalScrollBarPolicy = ScrollBarVisibility.Never;
      var dummyNode = canvasControl.Graph.CreateNode(node.Layout.ToRectD(), node.Style, node.Tag);
      foreach (var label in node.Labels) {
        canvasControl.Graph.AddLabel(dummyNode, label.Text, label.LayoutParameter, label.Style, label.PreferredSize, label.Tag);
      }
      foreach (var port in node.Ports) {
        canvasControl.Graph.AddPort(dummyNode, port.LocationParameter, port.Style, port.Tag);
      }
      canvasControl.ContentRect = new RectD(0,0,70,70);
      canvasControl.FitContent();
      e.DrawBackground();
      g.SmoothingMode = SmoothingMode.HighQuality;
      g.Clear(Color.White);

      ContextConfigurator cc = new ContextConfigurator(canvasControl.ContentRect);
      var renderContext = cc.CreateRenderContext(canvasControl, g);
      canvasControl.RenderContent(renderContext, g);
      var listGraphics = e.Graphics;
      var oldClip = listGraphics.Clip;
      listGraphics.IntersectClip(bounds);
      listGraphics.Clear(listBox.BackColor);
      listGraphics.DrawImage(bm, bounds.X + (int) insets.Left, bounds.Y + (int) insets.Top, bm.Width, bm.Height);
      listGraphics.Clip = oldClip;
      e.DrawFocusRectangle();

      canvasControl.Dispose();
      g.Dispose();
      bm.Dispose();

    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new DragAndDropForm());
    }

    
  }
}
