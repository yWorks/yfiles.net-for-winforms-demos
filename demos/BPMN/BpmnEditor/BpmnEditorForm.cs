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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Demo.yFiles.Graph.Bpmn.BpmnDi;
using Demo.yFiles.Graph.Bpmn.Layout;
using Demo.yFiles.Graph.Bpmn.Styles;
using Demo.yFiles.Graph.BpmnEditor;
using Demo.yFiles.Graph.Bpmn.Editor.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Bpmn;
using ContentAlignment = System.Drawing.ContentAlignment;
using EdgeType = Demo.yFiles.Graph.Bpmn.EdgeType;

namespace Demo.yFiles.Graph.Bpmn.Editor
{
  /// <summary>
  /// This demo shows how an editor for business process diagrams can be created using yFiles WPF.
  /// </summary>
  /// <remarks>
  /// The visualization and business logic is based on the BPMN 2.0 specification but isn't meant to
  /// implement all aspects of the specification but to demonstrate what techniques offered by
  /// yFiles WPF can be used to create such an editor:
  /// <list type="bullet">
  ///   <item>
  ///     <description>Custom NodeStyles</description>
  ///   </item>
  ///   <item>
  ///     <description>Custom EdgeStyle with custom Arrows</description>
  ///   </item>
  ///   <item>
  ///     <description>
  ///     Usage of group node insets: Group nodes make use of the <see cref="INodeInsetsProvider" /> interface to define
  ///     what insets they want to have. These insets are used e.g. during the layout.
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>
  ///     Node creation via Drag'n'Drop: Like in the DragNDrop demo it is shown how a drag'n'drop mechanism can be used by
  ///     the user to generate nodes with different default styles.
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>
  ///     Usage of a PortCandidateProvider: The BPMN specification regulates what type of relations are allowed between what
  ///     type of diagram elements. How the creation of an edge as well as the relation of one of its ports can be restricted
  ///     to follow this specification is demonstrated using PortCandidateProvider.
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>
  ///     Usage of Tables: This demo showcases how table nodes can be used for visualization and interaction. It is
  ///     demonstrated how the layout can be made aware of the table nodes.
  ///     </description>
  ///   </item>
  ///    <item>
  ///     <description>
  ///     Usage of custom import formats: This demo provides a function to import files using the BPMN Diagram Interchange file
  ///     format into the graph interface.
  ///     </description>
  ///   </item>
  ///   <item>
  ///     <description>
  ///     Usage of Folding: Like the folding demo, this demo shows, how folding can be used to hide sub-graphs in a folded group node.
  ///     It also provides a function, to showcase the sub-graph in a separate window.
  ///     </description>
  ///   </item>
  /// </list>
  /// </remarks>
  public partial class BpmnEditorForm : Form
  {
    private TableEditorInputMode tableEditorInputMode;
    private FoldingManager foldingManager;
    
    #region Initialization

    public BpmnEditorForm() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      newButton.SetCommand(Commands.New, graphControl);
      openButton.Click += OpenFile;
      saveButton.SetCommand(Commands.SaveAs, graphControl);
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitToSizeButton.SetCommand(Commands.FitGraphBounds, graphControl.FitContentViewMargins, graphControl);
      fitToGraphBoundsToolStripMenuItem.SetCommand(Commands.FitGraphBounds, graphControl.FitContentViewMargins, graphControl);

      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);
      newToolStripMenuItem.SetCommand(Commands.New, graphControl);
      openToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveAsToolStripMenuItem.SetCommand(Commands.SaveAs, graphControl);
    }

    private void OnLoaded(object sender, EventArgs e) {
      splitContainer3.Panel2MinSize = 200;
      InitializeStylePanel();
      InitializeGraph();
      InitializeInputModes();
      InitializeContextMenu();
      InitializePropertyPanel();
      sampleGraphComboBox.Items.AddRange(new[]
                                           {
                                             "Business",
                                             "Collaboration",
                                             "Different Exception Flows",
                                             "Expanded Subprocess",
                                             "Lanes Segment",
                                             "Lanes with Information Systems",
                                             "Matrix Lanes",
                                             "Process Normal Flow",
                                             "Project Application",
                                             "Simple BPMN Model",
                                             "Vertical Swimlanes"
                                           });
      sampleGraphComboBox.SelectedIndex = 0;
      sampleGraphDiComboBox.Items.AddRange(new[]
                                             {
                                               "Choreography",
                                               "Collaboration",
                                               "Collapsed SubProcess",
                                               "Different Exception Flows",
                                               "Label Styles",
                                               "Lanes with Information Systems",
                                               "Multiple Diagrams",
                                               "Process Normal Flow",
                                               "Project Application",
                                               "Simple BPMN Model",
                                               "SubProcess Hierarchy",
                                               "Vertical Swimlanes"
                                             });
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl,
    /// a <see cref="GraphEditorInputMode" />.
    /// </summary>
    /// <remarks>
    /// It will enable the drag'n'drop behavior and set the <see cref="tableEditorInputMode" />.
    /// </remarks>
    protected virtual void InitializeInputModes() {
      GraphEditorInputMode geim = new GraphEditorInputMode
      {
        // enable grouping operations
        AllowGroupingOperations = true,
        // We want orthogonal edge creation and editing
        OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext(),
        // dropping nodes from the palette
        NodeDropInputMode = new MyNodeDropInputMode
        {
          Enabled = true,
          // We identify the group nodes during a drag by either a custom tag or if they have a table associated.
          IsGroupNodePredicate =
            draggedNode => draggedNode.Lookup<ITable>() != null || (string) draggedNode.Tag == "GroupNode"
        },
        // don't allow node creation (except for context menu and drag'n'drop)
        AllowCreateNode = false,
        // Alter the ClickHitTestOrder so ports are tested before nodes
        ClickHitTestOrder = new[]
        {
          GraphItemTypes.Bend, GraphItemTypes.EdgeLabel, GraphItemTypes.Edge,
          GraphItemTypes.Port, GraphItemTypes.Node, GraphItemTypes.NodeLabel
        },
        // Alter the DoubleClickHitTestOrder so labels are tested first
        DoubleClickHitTestOrder = new[]
        {
            GraphItemTypes.Label, GraphItemTypes.All
        },
        // Enable snapping
        SnapContext = new GraphSnapContext
        {
          EdgeToEdgeDistance = 10,
          NodeToEdgeDistance = 15,
          NodeToNodeDistance = 20,
          SnapBendsToSnapLines = true
        },
        // tables shall not become child nodes but reparenting for other nodes is always enabled
        ReparentNodeHandler =
          new NoTableReparentNodeHandler { ReparentRecognizer = EventRecognizers.Always },
        // we use a default MoveViewportInputMode that allows us to drag the viewport without pressing 'SHIFT'
        MoveViewportInputMode = new MoveViewportInputMode(),
        // disable marquee selection so the MoveViewportInputMode can work without modifiers
        MarqueeSelectionInputMode = { Enabled = false },  
        // AutoRemoving empty labels destroys choreographys, if the Labels are empty
        AutoRemoveEmptyLabels = false,
        NavigationInputMode = {
            AutoGroupNodeAlignmentPolicy = NodeAlignmentPolicy.BottomCenter
        }
      };
      // increase the priority value of the MoveViewportInputMode so other input modes are still preferred.
      geim.MoveViewportInputMode.Priority = 110;

      // Create a new TEIM instance which also allows drag and drop
      tableEditorInputMode = new TableEditorInputMode
      {
        // Enable drag & drop of stripes
        StripeDropInputMode = { Enabled = true },
        // Maximal level for both reparent and drag and drop is 2
        ReparentStripeHandler = new ReparentStripeHandler { MaxColumnLevel = 2, MaxRowLevel = 2 },
        Priority = geim.HandleInputMode.Priority + 1
      };
      // Add to GEIM - we set the priority higher than for the handle input mode so that handles win if both gestures are possible
      geim.Add(tableEditorInputMode);

      // add our context menu
      geim.PopulateItemContextMenu += OnPopulateItemContextMenu;
      
      // add double click event handler
      geim.ItemLeftDoubleClicked += OnLeftDoubleClick;

      // register the command binding for the 'New' command
      geim.KeyboardInputMode.AddCommandBinding(Commands.New, OnNewExecuted);

      graphControl.InputMode = geim;
    }

    /// <summary>
    /// Event, if there was a double click on an Item
    /// </summary>
    private void OnLeftDoubleClick(object sender, ItemClickedEventArgs<IModelItem> e) {
      if (e.Handled) {
        return;
      }

      var label = e.Item as ILabel;
      if (label != null) {
        ((GraphEditorInputMode) graphControl.InputMode).EditLabel(label);
        e.Handled = true;
        return;
      }
      
      var node = e.Item as INode;
      if (node != null && node.Labels.Count > 0) {
        ((GraphEditorInputMode) graphControl.InputMode).EditLabel(node.Labels.First());
        e.Handled = true;
        return;
      }

      var port = e.Item as IPort;
      if (port != null && port.Labels.Count > 0) {
        ((GraphEditorInputMode) graphControl.InputMode).EditLabel(port.Labels.First());
        e.Handled = true;
        return;
      }
      
      var edge = e.Item as IEdge;
      if (edge != null && edge.Labels.Count > 0) {
        ((GraphEditorInputMode) graphControl.InputMode).EditLabel(edge.Labels.First());
        e.Handled = true;
        return;
      }
      e.Handled = false;
    }

    private void OnNewExecuted(object sender, ExecutedCommandEventArgs e) {
      // clear the whole graph, not only the current view
      var foldingView = graphControl.Graph.GetFoldingView();
      foldingView.LocalRoot = null;
      foldingView.Manager.MasterGraph.Clear();
      graphControl.UpdateContentRect();
      UndoEngine engine = graphControl.Graph.GetUndoEngine();
      if (engine != null) {
        engine.Clear();
      }
    }

    /// <summary>
    /// Initializes support for the property editor panel.
    /// </summary>
    /// <remarks>
    /// The contents of that panel are re-created each time the selection changes, based on the then-current selection.
    /// </remarks>
    private void InitializePropertyPanel() {
      var geim = (GraphEditorInputMode) graphControl.InputMode;
      geim.MultiSelectionFinished += UpdatePropertyPanel;
    }

    private static Label CreateHintLabel(string text) {
      return new Label {
                 Text = text,
                 TextAlign = ContentAlignment.MiddleLeft,
                 Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Italic, GraphicsUnit.Pixel),
                 Padding = new Padding(5,5,0,0),
                 Height = 50,
                 Width = 150
      };
    }

    private static object GetStyle(IModelItem item) {
      var node = item as INode;
      if (node != null) {
        return node.Style;
      }
      var edge = item as IEdge;
      if (edge != null) {
        return edge.Style;
      }
      var port = item as IPort;
      if (port != null) {
        return port.Style;
      }
      return null;
    }

    /// <summary>
    /// Initializes the graph defaults.
    /// </summary>
    private void InitializeGraph() {
      foldingManager = new FoldingManager();
      var foldingView = foldingManager.CreateFoldingView();
      var graph = graphControl.Graph = foldingView.Graph;

      // Ports should not be removed when an attached edge is deleted
      graph.NodeDefaults.Ports.AutoCleanUp = false;
      // Folding edges should use existing ports
      ((DefaultFoldingEdgeConverter) foldingManager.FoldingEdgeConverter).ReuseMasterPorts = true;
      ((DefaultFoldingEdgeConverter) foldingManager.FoldingEdgeConverter).ReuseFolderNodePorts = true;

      // Set default styles and label model parameter
      //foldingView.GroupedGraph.GroupNodeDefaults.Style = new GroupNodeStyle();
      graph.EdgeDefaults.Style = new BpmnEdgeStyle { Type = EdgeType.SequenceFlow };
      graph.EdgeDefaults.ShareStyleInstance = false;
      graph.EdgeDefaults.Labels.LayoutParameter = new EdgeSegmentLabelModel(10, 0, 0, true, EdgeSides.AboveEdge).CreateDefaultParameter();
      // For nodes we use a CompositeLabelModel that combines label placements inside and outside of the node
      var compositeLabelModel = new CompositeLabelModel();
      compositeLabelModel.LabelModels.Add(new InteriorLabelModel());
      compositeLabelModel.LabelModels.Add(new ExteriorLabelModel { Insets = new InsetsD(10) });
      graph.NodeDefaults.Labels.LayoutParameter = compositeLabelModel.CreateDefaultParameter();
      graph.NodeDefaults.Labels.Style = BpmnLabelStyle.NewDefaultInstance();

      // use a specialized port candidate provider
      foldingManager.MasterGraph.GetDecorator().NodeDecorator.PortCandidateProviderDecorator.SetFactory(
          node => (node.Style is BpmnNodeStyle || node.Style is GroupNodeStyle),
          node => new BpmnPortCandidateProvider(node));
      // Pools only have a dynamic PortCandidate
      foldingManager.MasterGraph.GetDecorator().NodeDecorator.PortCandidateProviderDecorator.SetFactory(
          node => (node.Style is PoolNodeStyle),
          node => PortCandidateProviders.FromCandidates(new DefaultPortCandidate(node) { Validity = PortCandidateValidity.Dynamic }));

      // allow reconnecting of edges
      foldingManager.MasterGraph.GetDecorator()
        .EdgeDecorator.EdgeReconnectionPortCandidateProviderDecorator.SetImplementation(
          EdgeReconnectionPortCandidateProviders.AllNodeCandidates);

      // enable undo operations
      foldingManager.MasterGraph.SetUndoEngineEnabled(true);
      //Use the undo support from the graph also for all future table instances
      Table.InstallStaticUndoSupport(foldingManager.MasterGraph);
    }

    /// <summary>
    /// Sets up context menu support for graph items.
    /// </summary>
    private void InitializeContextMenu() {
      var geim = (GraphEditorInputMode) graphControl.InputMode;
      // open context menu when port, node or edge is clicked
      geim.ContextMenuItems = GraphItemTypes.Node | GraphItemTypes.Edge | GraphItemTypes.Port;
      geim.PopulateItemContextMenu += OnPopulateItemContextMenu;
    }

    /// <summary>
    /// Populates the context menu for the given situation.
    /// </summary>
    private void OnPopulateItemContextMenu(object sender, PopulateItemContextMenuEventArgs<IModelItem> e) {
      if (e.Handled) {
        return;
      }

      var items = e.Menu.Items;

      var node = e.Item as INode;
      if (node != null) {
        // If it is not a text annotation itself...
        var textAnnotationStyle = node.Style as AnnotationNodeStyle;
        if (textAnnotationStyle == null) {  
          // ... offer to add a text annotation node
          AddMenuItem(items, "Add text annotation", (o, args) => {
            var annotationNode = graphControl.Graph.CreateNode(new PointD(node.Layout.X, node.Layout.Y-50));
            graphControl.Graph.SetStyle(annotationNode, new AnnotationNodeStyle());
            // including a connecting edge
            graphControl.Graph.CreateEdge(node, annotationNode, new BpmnEdgeStyle{Type = EdgeType.Association});
            var newLabel = graphControl.Graph.AddLabel(annotationNode, "", 
                new InteriorStretchLabelModel{Insets = new InsetsD(3, 3, 3, 3)}.CreateParameter(InteriorStretchLabelModel.Position.Center));
            // and start to edit the label
            ((GraphEditorInputMode) graphControl.InputMode).EditLabel(newLabel);
          });
        } else {
          // If it is a text annotation node, allow toggling the direction
          AddMenuItem(items, "Toggle direction", (o, args) => {
            textAnnotationStyle.Left = !textAnnotationStyle.Left;
          });
        }
        
       // If it is an Choreography node...
        var choreographyNodeStyle = node.Style as ChoreographyNodeStyle;
        if (choreographyNodeStyle != null) {
          items.Add(new ToolStripSeparator());
          // ... check if a participant was right-clicked
          var participant = choreographyNodeStyle.GetParticipant(node, e.QueryLocation);
          if (participant != null) {
            // and if so, offer to remove it
            AddMenuItem(items, "Remove participant", (o, args) => {
              if (!choreographyNodeStyle.TopParticipants.Remove(participant)) {
                choreographyNodeStyle.BottomParticipants.Remove(participant);
              }
              graphControl.Focus();
            });
            // or toggle its Multi-Instance flag
            AddMenuItem(items, "Toggle Participant Multi-Instance", (o, args) => {
              participant.MultiInstance = !participant.MultiInstance;
              graphControl.Focus();
            });
            // or edit its Label
            AddMenuItem(items, "Edit Label", (o, args) => {
              var parameter = choreographyNodeStyle.GetParticipantParameters(participant);
              var label = GetLabelFromParameter(node.Labels, parameter);
              if (label == null) {
                parameter = choreographyNodeStyle.GetParticipantParameters(participant);
                label = graphControl.Graph.AddLabel(node, "");
                graphControl.Graph.SetLabelLayoutParameter(label, parameter);
                graphControl.Graph.SetStyle(label, BpmnLabelStyle.NewDefaultInstance());
              }
              ((GraphEditorInputMode) graphControl.InputMode).EditLabel(label);
            });
          } else {
            // if no participant was clicked, a new one can be added to the top or bottom participants
            AddMenuItem(items, "Add Participant at Top", (o, args) => {
              participant = new Participant();
              choreographyNodeStyle.TopParticipants.Add(participant);
              var parameter = choreographyNodeStyle.GetParticipantParameters(participant);
              var newLabel = graphControl.Graph.AddLabel(node, "");
              graphControl.Graph.SetLabelLayoutParameter(newLabel, parameter);
              graphControl.Graph.SetStyle(newLabel, BpmnLabelStyle.NewDefaultInstance());
              ((GraphEditorInputMode) graphControl.InputMode).EditLabel(newLabel);
            });
            AddMenuItem(items, "Add Participant at Bottom", (o, args) => {
              participant = new Participant();
              choreographyNodeStyle.BottomParticipants.Add(participant);
              var parameter = choreographyNodeStyle.GetParticipantParameters(participant);
              var newLabel = graphControl.Graph.AddLabel(node, "");
              graphControl.Graph.SetLabelLayoutParameter(newLabel, parameter);
              graphControl.Graph.SetStyle(newLabel, BpmnLabelStyle.NewDefaultInstance());
              ((GraphEditorInputMode) graphControl.InputMode).EditLabel(newLabel);
            });
            AddMenuItem(items, "Edit Label", (o, args) => {
              ILabel taskNameBandLabel = null;
              if (node.Labels.Count <= 0 || GetLabelFromParameter(node.Labels, ChoreographyLabelModel.TaskNameBand) == null) {
                var parameter = ChoreographyLabelModel.TaskNameBand;
                taskNameBandLabel = graphControl.Graph.AddLabel(node, "");
                graphControl.Graph.SetLabelLayoutParameter(taskNameBandLabel, parameter);
                graphControl.Graph.SetStyle(taskNameBandLabel, BpmnLabelStyle.NewDefaultInstance());
              } else {
                taskNameBandLabel = GetLabelFromParameter(node.Labels, ChoreographyLabelModel.TaskNameBand);
              }
              ((GraphEditorInputMode) graphControl.InputMode).EditLabel(taskNameBandLabel);
            });
          }
        }

        // If it is an Activity node...
        var activityNodeStyle = node.Style as ActivityNodeStyle;
        if (activityNodeStyle != null) {
          items.Add(new ToolStripSeparator());
          // allow to add a Boundary Event as port that uses an EventPortStyle
          AddMenuItem(items, "Add Boundary Event", (o, args) => {
            graphControl.Graph.AddPort(node, FreeNodePortLocationModel.NodeBottomAnchored, new EventPortStyle());
            graphControl.Focus();
          });
        }

        // If a row of a pool node has been hit...
        var stripeDescriptor = tableEditorInputMode.FindStripe(e.QueryLocation, StripeTypes.All,
            StripeSubregionTypes.Header);
        if (stripeDescriptor != null) {
          // add the insert before menu item
          AddMenuItem(items, "Insert new lane before " + stripeDescriptor.Stripe, delegate {
            IStripe parent = stripeDescriptor.Stripe.GetParentStripe();
            int index = stripeDescriptor.Stripe.GetIndex();
            tableEditorInputMode.InsertChild(parent, index);
          });
          // add the insert after menu item
          AddMenuItem(items, "Insert new lane after " + stripeDescriptor.Stripe, delegate {
            IStripe parent = stripeDescriptor.Stripe.GetParentStripe();
            int index = stripeDescriptor.Stripe.GetIndex();
            tableEditorInputMode.InsertChild(parent, index + 1);
          });
          // add the delete menu item
          AddMenuItem(items, "Delete lane", delegate { tableEditorInputMode.DeleteStripe(stripeDescriptor.Stripe); });
        }
        if (stripeDescriptor != null && stripeDescriptor.Stripe is IRow) {
          // ... allow to increase or decrease the row header size
          var stripe = (IRow) stripeDescriptor.Stripe;
          var insets = stripe.Insets;
          var defaultInsets = stripe.Table.RowDefaults.Insets;

          items.Add(new ToolStripSeparator());

          if (insets.Left > defaultInsets.Left) {
            AddMenuItem(items, "Reduce header size", (o, args) => {
              // by reducing the header size of one of the rows, the size of the table insets might change
              var insetsBefore = stripe.Table.GetAccumulatedInsets();
              stripe.Table.SetStripeInsets(stripe,
                  new InsetsD(insets.Left - defaultInsets.Left, insets.Top, insets.Right, insets.Bottom));
              var insetsAfter = stripe.Table.GetAccumulatedInsets();
              // if the table insets have changed, the bounds of the pool node have to be adjusted as well
              var diff = insetsBefore.Left - insetsAfter.Left;
              graphControl.Graph.SetNodeLayout(node,
                  new RectD(node.Layout.X + diff, node.Layout.Y, node.Layout.Width - diff, node.Layout.Height));
            });
          }
          AddMenuItem(items, "Increase header size", (o, args) => {
            var insetsBefore = stripe.Table.GetAccumulatedInsets();
            stripe.Table.SetStripeInsets(stripe,
                new InsetsD(insets.Left + defaultInsets.Left, insets.Top, insets.Right, insets.Bottom));
            var insetsAfter = stripe.Table.GetAccumulatedInsets();
            var diff = insetsBefore.Left - insetsAfter.Left;
            graphControl.Graph.SetNodeLayout(node,
                new RectD(node.Layout.X + diff, node.Layout.Y, node.Layout.Width - diff, node.Layout.Height));
          });
        }

        // we don't want to be queried again if there are more items at this location
        e.Handled = true;
      }
      var edge = e.Item as IEdge;
      if (edge != null) {
        // For edges a label with a Message Icon may be added
        var modelParameter = new EdgeSegmentLabelModel(0, 0, 0, false, EdgeSides.OnEdge).CreateDefaultParameter();
        AddMenuItem(items, "Add Initiating Message Icon Label", (o, args) => {
          graphControl.Graph.AddLabel(edge, "", modelParameter, MessageLabelStyle.InitiatingStyle(), new SizeD(20, 14));
          graphControl.Focus();
        });
        AddMenuItem(items, "Add Response Message Icon Label", (o, args) => {
          graphControl.Graph.AddLabel(edge, "", modelParameter, MessageLabelStyle.ResponseStyle(), new SizeD(20, 14));
          graphControl.Focus();
        });

        // we don't want to be queried again if there are more items at this location
        e.Handled = true;
      }
    }
    
    

    /// <summary>
    /// Helper method that retrieves the label to the corresponding choreography participant
    /// </summary>
    /// <param name="nodeLabels"></param> All labels of this node
    /// <param name="parameter"></param> Parameter of this taskNameBand
    private static ILabel GetLabelFromParameter(IEnumerable<ILabel> nodeLabels, ILabelModelParameter parameter) {
      
        foreach (var label in nodeLabels) {
          if (ChoreographyLabelModel.GetEqualParameters(label.LayoutParameter, parameter)) {
            return label;
          }
        }

        return null;
    }

    private static void AddMenuItem(IList items, string header, EventHandler clickHandler) {
      var addAnnotationItem = new ToolStripMenuItem(header);
      addAnnotationItem.Click += clickHandler;
      items.Add(addAnnotationItem);
    }

    /// <summary>
    /// Initializes the panel from where BPMN elements can be dragged onto the graph.
    /// </summary>
    private void InitializeStylePanel() {
      // Create a new Graph in which the palette nodes live
      IGraph nodeContainer = new DefaultGraph();

      // Create the sample node for the pool
      var verticalPoolNodeStyle = new PoolNodeStyle();
      var verticalPoolNode = nodeContainer.CreateNode(PointD.Origin, verticalPoolNodeStyle);
      var verticalPoolTable = GetTable(verticalPoolNodeStyle);
      verticalPoolTable.ColumnDefaults.Insets = new InsetsD();
      verticalPoolTable.Insets = new InsetsD();
      verticalPoolTable.CreateGrid(1, 1);
      //Use twice the default width for this sample column (looks nicer in the preview)
      verticalPoolTable.SetSize(verticalPoolTable.RootColumn.ChildColumns.First(), verticalPoolTable.RootColumn.ChildColumns.First().GetActualSize() * 2);
      nodeContainer.SetNodeLayout(verticalPoolNode, verticalPoolTable.Layout.ToRectD());
      verticalPoolTable.AddLabel(verticalPoolTable.RootRow.ChildRows.First(), "Vertical Pool");

      var rowPoolNodeStyle = new PoolNodeStyle();
      var rowNode = nodeContainer.CreateNode(PointD.Origin, rowPoolNodeStyle);
      var rowTable = GetTable(rowPoolNodeStyle);

      var rowSampleRow = rowTable.CreateRow(100d);
      var rowSampleColumn = rowTable.CreateColumn(200d);
      rowTable.SetStyle(rowSampleColumn, VoidStripeStyle.Instance);
      rowTable.SetStripeInsets(rowSampleColumn, new InsetsD());
      rowTable.Insets = new InsetsD();
      rowTable.AddLabel(rowSampleRow, "Row");
      nodeContainer.SetNodeLayout(rowNode, rowTable.Layout.ToRectD());
      // Set the first row as tag so the NodeDragControl knows that a row and not a complete pool node shall be dragged
      rowNode.Tag = rowTable.RootRow.ChildRows.First();

      // Add BPMN sample nodes
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(60, 40)), new ActivityNodeStyle(), "GroupNode");
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(30, 30)), new GatewayNodeStyle());
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(30, 30)), new EventNodeStyle());
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(80, 20)), new AnnotationNodeStyle());
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(30, 50)), new DataObjectNodeStyle());
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(40, 40)), new DataStoreNodeStyle());
      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(80, 60)), new GroupNodeStyle(), "GroupNode");

      // Add a Choreography node with 2 participants
      var choreographyNodeStyle = new ChoreographyNodeStyle();
      choreographyNodeStyle.TopParticipants.Add(new Participant());
      choreographyNodeStyle.BottomParticipants.Add(new Participant());
      var choreographyNode = nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(80, 90)), choreographyNodeStyle,
          "GroupNode");
      nodeContainer.AddLabel(choreographyNode, "Participant 1", ChoreographyLabelModel.Instance.CreateParticipantParameter(true, 0));
      nodeContainer.AddLabel(choreographyNode, "Participant 2", ChoreographyLabelModel.Instance.CreateParticipantParameter(false, 0));

      nodeContainer.CreateNode(new RectD(PointD.Origin, new SizeD(50, 50)),
          new ConversationNodeStyle { Type = ConversationType.Conversation });
      nodeStyleListBox.ItemHeight = 80;
      nodeStyleListBox.ColumnWidth = 150;
      foreach (var node in nodeContainer.Nodes) {
        nodeStyleListBox.Items.Add(node);
      }

      //Handle list item drawing
      nodeStyleListBox.DrawItem += NodeStyleListBox_DrawItem;

      // register for the mouse down event to initiate the drag operation
      nodeStyleListBox.MouseDown += NodeStyleListBox_MouseDown;
      nodeStyleListBox.SizeChanged += (obj, args) => nodeStyleListBox.Invalidate();
    }

    /// <summary>
    /// Paint the node style representation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NodeStyleListBox_DrawItem(object sender, DrawItemEventArgs e) {
      ListBox listBox = (ListBox) sender;
      int i = e.Index;
      INode node = (INode) listBox.Items[i];

      Rectangle bounds = e.Bounds;
      Rectangle insets = Rectangle.FromLTRB(5, 5, 5, 5);

      Graphics g = e.Graphics;
      SmoothingMode oldMode = g.SmoothingMode;
      Region oldClip = g.Clip;

      // in .net 3.5 there are repaint issues - none of the below seems to help, there
      // are still sometimes background rendering artefacts left over.
      g.IntersectClip(bounds);
      g.FillRegion(new SolidBrush(e.BackColor), g.Clip);
      g.Clear(e.BackColor);
      e.DrawBackground();
      var w = bounds.Width - insets.Left - insets.Right;
      var h = bounds.Height - insets.Top - insets.Bottom;
      var sx = (float) ((w)/node.Layout.Width);
      var sy = (float) ((h)/node.Layout.Height);


      var transform = g.Transform;
      g.SmoothingMode = SmoothingMode.HighQuality;
      var layout = node.Layout.ToRectD();

      g.TranslateTransform(bounds.X + insets.Left, bounds.Y + insets.Top);
      if (sx > 0 && sy > 0 && (sx <= 1 || sy <= 1)) {
        var scale = Math.Min(sx, sy);
        g.ScaleTransform(scale, scale);
        g.TranslateTransform(w/(2*scale), h/(2*scale));
        g.TranslateTransform((float) (-layout.GetCenter().X), (float) (-layout.GetCenter().Y));

      } else {
        g.TranslateTransform(w/2f, h/2f);
        g.TranslateTransform((float) (-layout.GetCenter().X), (float) (-layout.GetCenter().Y));
      }
      //Get the renderer from the style, this requires the dummy node instance.
      var renderContext = new RenderContext(g, null) {ViewTransform = g.Transform, WorldTransform = g.Transform};
      node.Style.Renderer.GetVisualCreator(node, node.Style).CreateVisual(renderContext).Paint(renderContext, g);
      foreach (var label in node.Labels) {
        label.Style.Renderer.GetVisualCreator(label, label.Style).CreateVisual(renderContext).Paint(renderContext, g);
      }

      g.Transform = transform;
      g.SmoothingMode = oldMode;


      g.Clip = oldClip;
      e.DrawFocusRectangle();
    }

    /// <summary>
    /// Handles the MouseDown event of the nodeStyleListBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
    private void NodeStyleListBox_MouseDown(object sender, MouseEventArgs e) {
      ListBox listBox = (ListBox)sender;
      if (e.Button == MouseButtons.Left) {
        int indexOfItemUnderMouseToDrag = listBox.IndexFromPoint(e.X, e.Y);
        if (indexOfItemUnderMouseToDrag < 0) return;
        var node = (INode)listBox.Items[indexOfItemUnderMouseToDrag];
        // Get the index of the item the mouse is below.
        if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {
          DataObject dao = new DataObject();
          if (node.Tag is IStripe) {
            //If the dummy node has a stripe as its tag, we use the stripe directly
            //This allows StripeDropInputMode to take over
            dao.SetData(typeof(IStripe), node.Tag);
          } else {
            //Initialize drag operation if we actually did hit something
            dao.SetData(typeof (INode),
                        new SimpleNode {Layout = node.Layout, Style = (INodeStyle) node.Style.Clone(), Tag = node.Tag});
          }
          listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
        }
      } else if (e.Button == MouseButtons.Right) {
        // select clicked item and show context menu
        listBox.SelectedIndex = listBox.IndexFromPoint(e.X, e.Y);
        if (nodeStyleListBox.ContextMenuStrip != null) {
          nodeStyleListBox.ContextMenuStrip.Show();
        }
      }
    }

    private static ITable GetTable(PoolNodeStyle poolNodeStyle) {
      return poolNodeStyle.TableNodeStyle.Table;
    }

    #endregion

    private void UpdatePropertyPanel(object sender, SelectionEventArgs<IModelItem> e) {
      editorControl.Controls.Clear();
      FlowLayoutPanel panel = new FlowLayoutPanel {
        AutoSize = false,
        Dock = DockStyle.Fill
      };

      editorControl.Controls.Add(panel);
      panel.SuspendLayout();

      var selection = graphControl.Selection;
      if (selection.Count == 0) {
        panel.Controls.Add(CreateHintLabel("(No selection)"));
        return;
      }
      if (selection.Count > 1) {
        panel.Controls.Add(CreateHintLabel("(More than one item selected)"));
        return;
      }

      var item = selection.First();

      var style = GetStyle(item);
      if (style == null) {
        panel.Controls.Add(CreateHintLabel("(No properties to change)"));
        return;
      }

      // Get all boolean and enum properties
      var properties = style.GetType().GetProperties();
      var filteredProperties = properties.Where(p => p.PropertyType == typeof (bool) || p.PropertyType.IsEnum).OrderBy(p => p.PropertyType.IsEnum).ThenBy(p => p.Name).ToList();

      if (filteredProperties.Count == 0) {
        panel.Controls.Add(CreateHintLabel("(No properties to change)"));
        return;
      }

      for (int i = 0; i < filteredProperties.Count; i++) {
        var p = filteredProperties[i];

        // CheckBox for boolean properties
        if (p.PropertyType == typeof (bool)) {
          var checkBox = new CheckBox {
            Text = p.Name,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font(FontFamily.GenericSansSerif, 10.5f, FontStyle.Regular, GraphicsUnit.Pixel),
            Padding = new Padding(2),
            Height = 20,
            Width = 150,
            Checked = (bool) p.GetValue(style, null)
          };
          EventHandler handler = delegate {
            var node = item as INode;
            if (node != null) {
              var newStyle = node.Style.Clone();
              p.SetValue(newStyle, checkBox.Checked, null);
              graphControl.Graph.SetStyle(node, (INodeStyle) newStyle);
            }
            var edge = item as IEdge;
            if (edge != null) {
              var newStyle = edge.Style.Clone();
              p.SetValue(newStyle, checkBox.Checked, null);
              graphControl.Graph.SetStyle(edge, (IEdgeStyle) newStyle);
            }
            var port = item as IPort;
            if (port != null) {
              var newStyle = port.Style.Clone();
              p.SetValue(newStyle, checkBox.Checked, null);
              graphControl.Graph.SetStyle(port, (IPortStyle) newStyle);
            }
            graphControl.Invalidate();
          };
          checkBox.CheckedChanged += handler;
          panel.Controls.Add(checkBox);
        }

        // Label and ComboBox for enum properties
        if (p.PropertyType.IsEnum) {
          var label = new Label {Text = p.Name, TextAlign = ContentAlignment.MiddleLeft,
                 Font = new Font(FontFamily.GenericSansSerif, 10.5f, FontStyle.Bold, GraphicsUnit.Pixel),
                 Padding = new Padding(2),
                 Height = 20,
                 Width = 150};
                 

          panel.Controls.Add(label);
          var comboBox = new ComboBox {
            Margin = new Padding(5, 0, 0, 0),
            Width = 180
          };
          foreach(var enumVal in Enum.GetValues(p.PropertyType)) {
            comboBox.Items.Add(enumVal);
          }

          comboBox.SelectedItem = p.GetValue(style, null);
          comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
          comboBox.SelectedValueChanged += delegate {
            var node = item as INode;
            if (node != null) {
              var newStyle = node.Style.Clone();
              p.SetValue(newStyle, comboBox.SelectedItem, null);
              graphControl.Graph.SetStyle(node, (INodeStyle) newStyle);
            }
            var edge = item as IEdge;
            if (edge != null) {
              var newStyle = edge.Style.Clone();
              p.SetValue(newStyle, comboBox.SelectedItem, null);
              graphControl.Graph.SetStyle(edge, (IEdgeStyle) newStyle);
            }
            var port = item as IPort;
            if (port != null) {
              var newStyle = port.Style.Clone();
              p.SetValue(newStyle, comboBox.SelectedItem, null);
              graphControl.Graph.SetStyle(port, (IPortStyle) newStyle);
            }
            graphControl.Invalidate();
          };
          panel.Controls.Add(comboBox);
        }
      }
      panel.ResumeLayout();
    }

    /// <summary>
    /// Exits the application on menu item click.
    /// </summary>
    private void OnExitMenuItemClick(object sender, EventArgs e) {
      Application.Exit();
    }

    /// <summary>
    /// Called when the selection of the graph selection combo box is changed.
    /// </summary>
    private void OnSampleGraphChanged(object sender, EventArgs e) {
      var selection = sampleGraphComboBox.SelectedItem as string;
      if (selection != null) {
        // modify selection string to match file
        var name = selection.ToLower().Replace(' ', '_') + ".graphml";
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            @"Resources\BpmnSamples");
        var file = Path.Combine(path, name);
        graphControl.ImportFromGraphML(file);
        UpdatePropertyPanel(null, null);
        sampleGraphDiComboBox.SelectedItem = null;
      }
    }
    
    /// <summary>
    /// Called when the selection of the bpmn graph selection combo box is changed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">
    /// The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event
    /// data.
    /// </param>
    private void OnSampleDiGraphChanged(object sender, EventArgs e) {
      var selection = sampleGraphDiComboBox.SelectedItem as string;
      if (selection != null) {
        // modify selection string to match file
        var name = selection.ToLower().Replace(' ', '_') + ".bpmn";
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            @"Resources\BpmnDiSamples");
        var file = Path.Combine(path, name);
        var parser = new BpmnDiParser();
        parser.Load(graphControl.Graph, file, SelectDiagram);
        graphControl.FitGraphBounds();
        UpdatePropertyPanel(null, null);
        sampleGraphComboBox.SelectedItem = null;
      }
    }

    /// <summary>
    /// Event callback to handle the automatic layout.
    /// </summary>
    private async void OnLayoutClick(object sender, EventArgs e) {
      // Create a new BpmnLayout using a Left-To-Right layout orientation
      var bpmnLayout = new BpmnLayout { LayoutOrientation = yWorks.Layout.Bpmn.LayoutOrientation.LeftToRight };

      //We use Layout executor convenience method that already sets up the whole layout pipeline correctly
      var layoutExecutor = new LayoutExecutor(graphControl, bpmnLayout)
      {
        // Set the duration of the animation
        Duration = TimeSpan.FromSeconds(1),
        // The viewport shall be animated as well
        AnimateViewport = true,
        // The content rect shall be adjusted to the new layout
        UpdateContentRect = true,
        TableLayoutConfigurator = { HorizontalLayout = true, FromSketch = true },
        // the BpmnLayoutData provides information about the BPMN node and edge types to the layout algorithm.
        LayoutData = new BpmnLayoutData()
        {
          CompactMessageFlowLayering = false,
          StartNodesFirst = true
        }
      };
      await layoutExecutor.Start();
    }

    /// <summary>
    /// Custom <see cref="NodeDropInputMode" /> that disallows creating a table node inside of a group node
    /// (especially inside of another table node)
    /// </summary>
    private sealed class MyNodeDropInputMode : NodeDropInputMode
    {
      protected override IModelItem GetDropTarget(PointD dragLocation) {
        //Ok, this node has a table associated -> disallow dragging it into a group node.
        if (DraggedItem.Lookup<ITable>() != null) {
          return null;
        }
        return base.GetDropTarget(dragLocation);
      }
    }

    /// <summary>
    /// Custom <see cref="NodeDropInputMode" /> that disallows creating a table node inside of a group node and
    /// enlarges the group node if a new node is dropped outside of its bounds.
    /// </summary>
    private sealed class ContentViewerNodeDropInputMode : NodeDropInputMode
    {
      protected override INode CreateNode(IInputModeContext context, IGraph graph, INode node, IModelItem dropTarget, RectD layout) {
        var node1 = base.CreateNode(context, graph, node, dropTarget, layout);

        // Enlarges the group node, if a new node is dropped outside the bounds of the master group node
        var localRoot = context.GetGraph().GetFoldingView().LocalRoot;
        var view = context.GetGraph().GetFoldingView();
        view.Manager.MasterGraph.GetGroupingSupport().EnlargeGroupNode(context, localRoot, true);
        
        return node1;
      }

      protected override IModelItem GetDropTarget(PointD dragLocation) {
        
        //Ok, this node has a table associated -> disallow dragging it into a group node.
        if (DraggedItem.Lookup<ITable>() != null) {
          return null;
        }
        
        return base.GetDropTarget(dragLocation);
      }
    }

    /// <summary>
    /// Custom <see cref="ReparentNodeHandler" /> that disallows reparenting a table node.
    /// </summary>
    private sealed class NoTableReparentNodeHandler : ReparentNodeHandler
    {
      public override bool IsValidParent(IInputModeContext context, INode node, INode newParent) {
        // table nodes shall not become child nodes
        return node.Lookup(typeof (ITable)) == null && base.IsValidParent(context, node, newParent);
      }
    }

    /// <summary>
    /// Custom Open File Dialog for .bpmn and GraphML files.
    /// </summary>
    private void OpenFile(object sender, EventArgs e) {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "All graphs|*.bpmn;*.graphml|BPMN files (*.bpmn)|*.bpmn|GraphML files (*.graphml)|*.graphml|XML files (*.xml)|*.xml|All files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK) {
        var fileName = openFileDialog.FileName;
        if (Path.GetExtension(openFileDialog.FileName) == ".bpmn") {
          var parser = new BpmnDiParser();
          parser.Load(graphControl.Graph, fileName, SelectDiagram);
        } else {
          graphControl.GraphMLIOHandler.Read(graphControl.Graph,fileName);
        }
        graphControl.FitGraphBounds();
      }
    }

    /// <summary>
    /// Lets the user select a diagram from a list of possible diagrams
    /// </summary>
    /// <param name="topLevelDiagrams">Supplied list of possible diagrams.</param>
    /// <returns>The selected Diagram</returns>
    private string SelectDiagram(IEnumerable<string> topLevelDiagrams) {
      if (!topLevelDiagrams.Any()) {
        return null;
      }
      string diaToLoad = null;
      if (topLevelDiagrams.Count() == 1) {
        diaToLoad = topLevelDiagrams.First();
      } else {
        var chooser = new DiagramChooserForm();
        foreach (var diagram in topLevelDiagrams) {
          chooser.listDiagrams.Items.Add(diagram);
        }
        // TODO: Pass owner form
        if (chooser.ShowDialog() == DialogResult.OK && chooser.listDiagrams.SelectedIndex != -1) {
          return (string) chooser.listDiagrams.SelectedItem;
        }
      }
      return diaToLoad;
    }

    #region Main
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new BpmnEditorForm());
    }
    #endregion
  }
}
