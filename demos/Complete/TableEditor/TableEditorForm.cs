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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.yFiles.Graph.TableEditor.Properties;
using Demo.yFiles.Graph.TableEditor.Style;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Hierarchic;

//using Rectangle = yWorks.Geometry.MutableRectangle;

namespace Demo.yFiles.Graph.TableEditor
{
  /// <summary>
  /// Interaction logic for <c>TableNodeStyleWindow.xaml</c>.
  /// </summary>
  /// <remarks>This demo configures an instance of <see cref="TableEditorInputMode"/> that is used to interactively modify the tables, as well as several child modes of <see cref="GraphEditorInputMode"/> that
  /// handle context menus and tool tips. Additionally, it shows how to perform a hierarchic layout that automatically respects the table structure. Please see the demo description for further information.</remarks>
  public partial class TableEditorForm : Form
  {
    #region Properties

    private TableEditorInputMode tableEditorInputMode;
    private GraphEditorInputMode graphEditorInputMode;

    /// <summary>
    /// The default style for normal group nodes
    /// </summary>
    private readonly ShapeNodeStyle defaultGroupNodeStyle = new ShapeNodeStyle()
    {
      Shape = ShapeNodeShape.RoundRectangle,
      Brush = Brushes.Transparent,
      Pen = new Pen(Brushes.Black, 1) { DashStyle = DashStyle.DashDot }
    };

    /// <summary>
    /// The default style for normal nodes
    /// </summary>
    private readonly ShinyPlateNodeStyle defaultNormalNodeStyle = new ShinyPlateNodeStyle {
      Radius = 0,
      Brush = Brushes.Orange
    };

    /// <summary>
    /// The default size for normal nodes
    /// </summary>
    private readonly SizeD defaultNodeSize = new SizeD(80, 50);
    
    public IGraph Graph {
      get { return graphControl.Graph; }
    }

    #endregion

    #region Initialization & Configuration

    public TableEditorForm() {
      InitializeComponent();
      PopulateDnDList();
      ConfigureInputModes();
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeGraph"/>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      try {
        description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      } catch (MissingMethodException) {
        // Workaround for https://github.com/microsoft/msbuild/issues/4581
        description.Text = "The description is not available with this version of .NET Core.";
      }
      // initialize the graph
      InitializeGraph();

      RegisterMenuCommands();
      RegisterButtonCommands();
    }

    /// <summary>
    /// Configure the main input mode.
    /// </summary>
    /// <remarks>Creates a <see cref="GraphEditorInputMode"/> instance.</remarks>
    private void ConfigureInputModes() {
      graphEditorInputMode = new TableGraphEditorInputMode
                               {
                                 //We want orthogonal edge editing/creation
                                 OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext { Enabled = true },
                                 //Activate drag 'n' drop from the style palette
                                 NodeDropInputMode = new MyNodeDropInputMode
                                                       {
                                                         ShowPreview = true,
                                                         Enabled = true,
                                                         //We identify the group nodes during a drag by either a custom tag or if they have a table associated.
                                                         IsGroupNodePredicate =
                                                           draggedNode => draggedNode.Lookup<ITable>() != null || (string) draggedNode.Tag == "GroupNode"
                                                       },
                                 //But disable node creation on click
                                 AllowCreateNode = false
                               };
      //Register custom reparent handler that prevents reparenting of table nodes (i.e. they may only appear on root level)
      graphEditorInputMode.ReparentNodeHandler = new MyReparentHandler(graphEditorInputMode.ReparentNodeHandler);
      ConfigureTableEditing();

      graphControl.InputMode = graphEditorInputMode;
    }

    /// <summary>
    /// Configures table editing specific parts.
    /// </summary>
    private void ConfigureTableEditing() {
      //Create a new TEIM instance which also allows drag and drop
      tableEditorInputMode = new TableEditorInputMode
                               {
                                 //Enable drag & drop
                                 StripeDropInputMode = {Enabled = true},
                                 //Maximal level for both reparent and drag and drop is 2
                                 ReparentStripeHandler =
                                   new ReparentStripeHandler {MaxColumnLevel = 2, MaxRowLevel = 2},
                                 //Set the priority higher than for the handle input mode so that handles win if both gestures are possible
                                 Priority = graphEditorInputMode.HandleInputMode.Priority + 1
                               };
      //Add to GEIM
      graphEditorInputMode.Add(tableEditorInputMode);

      //Tooltip and context menu stuff for tables
      graphEditorInputMode.ContextMenuItems = GraphItemTypes.Node;
      graphEditorInputMode.PopulateItemContextMenu += graphEditorInputMode_PopulateItemContextMenu;
      graphEditorInputMode.PopulateItemContextMenu += graphEditorInputMode_PopulateNodeContextMenu;
      graphEditorInputMode.MouseHoverInputMode.QueryToolTip += MouseHoverInputMode_QueryToolTip;


      // we don't provide candidates for the pool nodes, so tell the input mode not to create edges, if
      // there aren't any candidates. That way, we can start marquee selection inside pool nodes
      graphEditorInputMode.CreateEdgeInputMode.UseHitItemsCandidatesOnly = true;
      graphEditorInputMode.AllowGroupingOperations = true;
    }

    /// <summary>
    /// Event handler for tool tips over a stripe header
    /// </summary>
    /// <remarks>We show only tool tips for stripe headers in this demo.</remarks>
    private void MouseHoverInputMode_QueryToolTip(object sender, ToolTipQueryEventArgs e) {
      if (!e.Handled) {
        StripeSubregion stripe = GetStripe(e.QueryLocation);
        if (stripe != null) {
          e.ToolTip = stripe.Stripe.ToString();
          e.Handled = true;
        }
      }
    }

    /// <summary>
    /// Event handler for the context menu for stripe header
    /// </summary>
    /// <remarks>We show only a simple context menu that demonstrates the <see cref="TableEditorInputMode.InsertChild"/> convenience method.</remarks>
    private void graphEditorInputMode_PopulateItemContextMenu(object sender,
                                                              PopulateItemContextMenuEventArgs<IModelItem> e) {
      if (!e.Handled) {
        var stripe = GetStripe(e.QueryLocation);
        if (stripe != null) {
          var deleteItem = new ToolStripMenuItem { Text = "Delete " + stripe.Stripe };
          deleteItem.SetCommand(Commands.Delete, stripe.Stripe, graphControl);
          e.Menu.Items.Add(deleteItem);
          var insertBeforeItem = new ToolStripMenuItem { Text = "Insert new stripe before " + stripe.Stripe };
          insertBeforeItem.Click += delegate {
                             IStripe parent = stripe.Stripe.GetParentStripe();
                             int index = stripe.Stripe.GetIndex();
                             tableEditorInputMode.InsertChild(parent, index);
                           };
          e.Menu.Items.Add(insertBeforeItem);
          var insertAfterItem = new ToolStripMenuItem { Text = "Insert new stripe after " + stripe.Stripe };
          insertAfterItem.Click += delegate {
                              IStripe parent = stripe.Stripe.GetParentStripe();
                              int index = stripe.Stripe.GetIndex();
                              tableEditorInputMode.InsertChild(parent, index + 1);
                            };
          e.Menu.Items.Add(insertAfterItem);
          e.Handled = true;
        }
      }
    }

    /// <summary>
    /// Event handler for the context menu other hits on a node.
    /// </summary>
    /// <remarks>We show only a dummy context menu to demonstrate the basic principle.</remarks>
    private void graphEditorInputMode_PopulateNodeContextMenu(object sender,
                                                              PopulateItemContextMenuEventArgs<IModelItem> e) {
      if (!e.Handled) {
        IModelItem tableNode =
          graphEditorInputMode.FindItems(e.QueryLocation, new[] {GraphItemTypes.Node}, (item) => item.Lookup<ITable>() != null).FirstOrDefault();
        if (tableNode != null) {
          var cutItem = new ToolStripMenuItem
          {
            Text = "ContextMenu for " + tableNode
          };
          e.Menu.Items.Add(cutItem);
          e.Handled = true;
        }
      }
    }

    /// <summary>
    /// Helper method that uses <see cref="TableEditorInputMode.FindStripe"/>
    /// to retrieve a stripe at a certain location.
    /// </summary>
    private StripeSubregion GetStripe(PointD location) {
      return tableEditorInputMode.FindStripe(location, StripeTypes.All, StripeSubregionTypes.Header);
    }

    private void InitializeGraph() {
      Graph.NodeDefaults.Style = defaultNormalNodeStyle;
      Graph.NodeDefaults.Size = defaultNodeSize;
      Graph.GroupNodeDefaults.Style = defaultGroupNodeStyle;
      Graph.GroupNodeDefaults.Size = defaultNodeSize;

      //We load a sample graph
      graphControl.ImportFromGraphML("Resources\\sample.graphml");
      graphControl.FitGraphBounds();

      //Configure Undo...
      //Enable general undo support
      graphControl.Graph.SetUndoEngineEnabled(true);
      //Use the undo support from the graph also for all future table instances
      Table.InstallStaticUndoSupport(Graph);

      // provide no candidates for edge creation at pool nodes - this effectively disables
      // edge creations for those nodes
      Graph.GetDecorator().NodeDecorator.PortCandidateProviderDecorator.SetImplementation(
        node => node.Lookup<ITable>() != null, PortCandidateProviders.NoCandidates);

      // customize marquee selection handling for pool nodes
      Graph.GetDecorator().NodeDecorator.MarqueeTestableDecorator.SetFactory(
        node => node.Lookup<ITable>() != null, node => new PoolNodeMarqueeTestable(node.Layout));
    }

    private class PoolNodeMarqueeTestable : IMarqueeTestable {
      private readonly IRectangle rectangle;

      public PoolNodeMarqueeTestable(IRectangle rectangle) {
        this.rectangle = rectangle;
      }

      public bool IsInBox(IInputModeContext context, RectD rectangle) {
        return rectangle.Contains(rectangle.GetTopLeft()) && rectangle.Contains(rectangle.GetBottomRight());
      }
    }

    #endregion

    #region Command Registration

    private void RegisterMenuCommands() {
      // File menu
      openToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveAsToolStripMenuItem.SetCommand(Commands.Save, graphControl);
      // Edit menu
      undoToolStripMenuItem.SetCommand(Commands.Undo, graphControl);
      redoToolStripMenuItem.SetCommand(Commands.Redo, graphControl);
      cutToolStripMenuItem.SetCommand(Commands.Cut, graphControl);
      copyToolStripMenuItem.SetCommand(Commands.Copy, graphControl);
      pasteToolStripMenuItem.SetCommand(Commands.Paste, graphControl);
      deleteToolStripMenuItem.SetCommand(Commands.Delete, graphControl);

      // View menu
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitToSizeToolStripMenuItem.SetCommand(Commands.FitGraphBounds, graphControl);
    }

    private void RegisterButtonCommands() {
      loadGraphMLButton.SetCommand(Commands.Open, graphControl);
      saveGraphMLButton.SetCommand(Commands.Save, graphControl);
      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);
      cutButton.SetCommand(Commands.Cut, graphControl);
      copyButton.SetCommand(Commands.Copy, graphControl);
      pasteButton.SetCommand(Commands.Paste, graphControl);
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitToSizeButton.SetCommand(Commands.FitGraphBounds, graphControl);
    }

    #endregion

    #region Event handler

    /// <summary>
    /// Perform a hierarchic layout that also configures the tables
    /// </summary>
    /// <remarks>Table support is automatically enabled in <see cref="LayoutExecutor"/>. The layout will:
    /// <list type="bullet">
    /// <item>
    /// Arrange all leaf nodes in a hierarchic layout inside their respective table cells
    /// </item>
    /// <item>Resize all table cells to encompass their child nodes. Optionally, <see cref="TableLayoutConfigurator.Compaction"/> allows to shrink table cells, other wise, table cells
    /// can only grow.</item>
    /// </list>
    /// </remarks>
    private void LayoutButton_Click(object sender, EventArgs e) {
        var hl = new HierarchicLayout
                    {
                      ComponentLayoutEnabled = false,
                      LayoutOrientation = LayoutOrientation.LeftToRight,
                      OrthogonalRouting = true,
                      RecursiveGroupLayering = false
                    };
        ((SimplexNodePlacer) hl.NodePlacer).BarycenterMode = true;

        //We use Layout executor convenience method that already sets up the whole layout pipeline correctly
        var layoutExecutor = new LayoutExecutor(graphControl, hl)
                               {
                                 //Table layout is enabled by default already...
                                 ConfigureTableLayout = true,
                                 Duration = TimeSpan.FromMilliseconds(500),
                                 AnimateViewport = true,
                                 UpdateContentRect = true,
                                 TargetBoundsInsets = graphEditorInputMode.ContentRectMargins,
                                 RunInThread = true,
                                 //Table cells may only grow by an automatic layout.
                                 TableLayoutConfigurator = {Compaction = false}
                               };
        layoutExecutor.Start();
    }

    /// <summary>
    /// Exit the demo
    /// </summary>
    private void ExitMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    #endregion

    #region drag and drop

    /// <summary>
    /// Creates rows, columns and tables programmatically to populate the list view where the items
    /// are dragged from.
    /// </summary>
    private void PopulateDnDList() {
      //Dummy table that serves to hold only a sample row
      ITable rowSampleTable = new Table();
      //Dummy table that serves to hold only a sample column
      ITable columnSampleTable = new Table();

      //Configure the defaults for the row sample table
      //We use a stripe control style and pass the style specific instance b a custom messenger object (e.g. StripeDescriptor)
      var descriptor = new StripeDescriptor {
        BackgroundBrush = new SolidBrush(Color.FromArgb(255, 171, 200, 226)),
        InsetBrush = new SolidBrush(Color.FromArgb(255, 240, 248, 255))
      };
      rowSampleTable.RowDefaults.Style = new AlternatingLeafStripeStyle {
        EvenLeafDescriptor = descriptor,
        OddLeafDescriptor = descriptor,
        ParentDescriptor = descriptor
      };

      //Create the sample row
      var rowSampleRow = rowSampleTable.CreateRow();
      //Create an invisible sample column in this table so that we will see something.
      var rowSampleColumn = rowSampleTable.CreateColumn(200d);
      rowSampleTable.SetStyle(rowSampleColumn, VoidStripeStyle.Instance);
      //The sample row uses empty insets
      rowSampleTable.SetStripeInsets(rowSampleColumn, new InsetsD());
      rowSampleTable.AddLabel(rowSampleRow, "Row");


      var columnSampleRow = columnSampleTable.CreateRow(200);
      columnSampleTable.SetStyle(columnSampleRow, VoidStripeStyle.Instance);
      descriptor = new StripeDescriptor {
        BackgroundBrush = new SolidBrush(Color.FromArgb(255, 171, 200, 226)),
        InsetBrush = new SolidBrush(Color.FromArgb(255, 240, 248, 255))
      };
      var columnSampleColumn = columnSampleTable.CreateColumn(200d);
      columnSampleTable.SetStyle(columnSampleColumn, new AlternatingLeafStripeStyle {
        EvenLeafDescriptor = descriptor,
        OddLeafDescriptor = descriptor,
        ParentDescriptor = descriptor
      });
      columnSampleTable.SetStripeInsets(columnSampleRow, new InsetsD());
      columnSampleTable.AddLabel(columnSampleColumn, "Column");

      //Table for a complete sample table node
      Table sampleTable = new Table() {Insets = new InsetsD(0, 30, 0, 0)};
      //Configure the defaults for the row sample table
      sampleTable.ColumnDefaults.MinimumSize = sampleTable.RowDefaults.MinimumSize = 50;

      //Setup defaults for the complete sample table
      //We use a custom style that alternates the stripe colors and uses a special style for all parent stripes.
      sampleTable.RowDefaults.Style = new AlternatingLeafStripeStyle {
        EvenLeafDescriptor = new StripeDescriptor {
          BackgroundBrush = new SolidBrush(Color.FromArgb(255, 196, 215, 237)),
          InsetBrush = new SolidBrush(Color.FromArgb(255, 196, 215, 237))
        },
        OddLeafDescriptor = new StripeDescriptor {
          BackgroundBrush = new SolidBrush(Color.FromArgb(255, 171, 200, 226)),
          InsetBrush = new SolidBrush(Color.FromArgb(255, 171, 200, 226))
        },
        ParentDescriptor = new StripeDescriptor {
          BackgroundBrush = new SolidBrush(Color.FromArgb(255, 113, 146, 178)),
          InsetBrush = new SolidBrush(Color.FromArgb(255, 113, 146, 178))
        }
      };

      //The style for the columns is simpler, we use a node control style that only paints the header insets.
      descriptor = new StripeDescriptor {
        BackgroundBrush = Brushes.Transparent,
        InsetBrush = new SolidBrush(Color.FromArgb(255, 113, 146, 178))
      };
      sampleTable.ColumnDefaults.Style = columnSampleTable.ColumnDefaults.Style = new AlternatingLeafStripeStyle {
        EvenLeafDescriptor = descriptor,
        OddLeafDescriptor = descriptor,
        ParentDescriptor = descriptor
      };

      //Create a row and a column in the sample table
      sampleTable.CreateGrid(1, 1);
      //Use twice the default width for this sample column (looks nicer in the preview...)
      sampleTable.SetSize(sampleTable.Columns.First(), sampleTable.Columns.First().GetActualSize() * 2);
      //Bind the table to a dummy node which is used for drag & drop
      //Binding the table is performed through a TableNodeStyle instance.
      //Among other things, this also makes the table instance available in the node's lookup (use INode.Lookup<ITable>()...)
      SimpleNode tableNode = new SimpleNode
      {
        Style = new yWorks.Graph.Styles.TableNodeStyle(sampleTable)
          {
            TableRenderingOrder = TableRenderingOrder.RowsFirst,
            BackgroundStyle = new ShapeNodeStyle { Brush = new SolidBrush(Color.FromArgb(255, 236, 245, 255)) }
          },
        Layout = new MutableRectangle(sampleTable.Layout)
      };

      //Add the sample node for the table
      styleListBox.Items.Add(tableNode);

      //Add sample rows and columns
      //We use dummy nodes to hold the associated stripe instances - this makes the style panel easier to use
      SimpleNode columnSampleNode = new SimpleNode {
        Style = new yWorks.Graph.Styles.TableNodeStyle(columnSampleTable),
        Layout = new MutableRectangle(columnSampleTable.Layout),
        Tag = columnSampleTable.RootColumn.ChildColumns.First()
      };

      styleListBox.Items.Add(columnSampleNode);

      //Add sample rows and columns
      //We use dummy nodes to hold the associated stripe instances - this makes the style panel easier to use
      SimpleNode rowSampleNode = new SimpleNode {
        Style = new yWorks.Graph.Styles.TableNodeStyle(rowSampleTable),
        Layout = new MutableRectangle(rowSampleTable.Layout),
        Tag = rowSampleTable.RootRow.ChildRows.First()
      };
      styleListBox.Items.Add(rowSampleNode);

      //Add normal sample leaf and group nodes
      SimpleNode normalNode = new SimpleNode {
        Style = defaultNormalNodeStyle,
        Layout = new MutableRectangle(PointD.Origin, defaultNodeSize),
      };
      styleListBox.Items.Add(normalNode);

      SimpleNode groupNode = new SimpleNode {
        Style = defaultGroupNodeStyle,
        Layout = new MutableRectangle(PointD.Origin, defaultNodeSize),
        //We set a custom tag that identifies this node as group node.
        Tag = "GroupNode"
      };
      styleListBox.Items.Add(groupNode);
    }

    #region Event handlers & painting

    private void nodeStyleListBox_MouseDown(object sender, MouseEventArgs e) {
      ListBox listBox = (ListBox)sender;
      if (e.Button == MouseButtons.Left) {
        int indexOfItemUnderMouseToDrag = listBox.IndexFromPoint(e.X, e.Y);
        // Get the index of the item the mouse is below.
        if (indexOfItemUnderMouseToDrag != ListBox.NoMatches) {
          var node = (INode)listBox.Items[indexOfItemUnderMouseToDrag];
          DataObject dao = new DataObject();

          if (node.Tag is IStripe) {
            //If the dummy node has a stripe as its tag, we use the stripe directly
            //This allows StripeDropInputMode to take over
            dao.SetData(typeof(IStripe), node.Tag);
            listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
          } else {
            //Initialize drag operation if we actually did hit something
            var copyNode = new SimpleNode{ Labels = node.Labels, Layout = node.Layout, Ports = node.Ports, Style = (INodeStyle) node.Style.Clone(), Tag = node.Tag};
            dao.SetData(typeof (INode), copyNode);
            listBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
          }
        }
      } else if (e.Button == MouseButtons.Right) {
        // select clicked item and show context menu
        listBox.SelectedIndex = listBox.IndexFromPoint(e.X, e.Y);
        styleListBox.ContextMenuStrip.Show();
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

      var sx = (float)((bounds.Width - insets.Left - insets.Right) / node.Layout.Width);
      var sy = (float)((bounds.Height - insets.Top - insets.Bottom) / node.Layout.Height);

      if (sx > 0 && sy > 0) {
        var transform = g.Transform;
        g.SmoothingMode = SmoothingMode.HighQuality;

        g.TranslateTransform((float)(bounds.X + insets.Left), (float)(bounds.Y + insets.Top));
        g.ScaleTransform(Math.Min(sx, sy), Math.Min(sx, sy));
        g.TranslateTransform((float)(-node.Layout.X), (float)(-node.Layout.Y));

        //Get the renderer from the style, this requires the dummy node instance.
        var ctx = new RenderContext(g, null) { ViewTransform = g.Transform, WorldTransform = g.Transform };
        node.Style.Renderer.GetVisualCreator(node, node.Style).CreateVisual(ctx).Paint(ctx, g);

        g.Transform = transform;
        g.SmoothingMode = oldMode;
      }

      g.Clip = oldClip;
      e.DrawFocusRectangle();
    }

    #endregion

    #endregion

    #region Utilities

    /// <summary>
    /// Custom <see cref="NodeDropInputMode"/> that disallows creating a table node inside of a group node (especially inside of another table node)
    /// </summary>
    private class MyNodeDropInputMode : NodeDropInputMode
    {
      protected override IModelItem GetDropTarget(PointD location) {
        //Ok, this node has a table associated - disallow dragging into a group node.
        var draggedNode = DraggedItem;
        if (draggedNode != null && draggedNode.Lookup<ITable>() != null) {
          return null;
        }
        return base.GetDropTarget(location);
      }
    }

    /// <summary>
    /// Custom <see cref="IReparentNodeHandler"/> that disallows reparenting a table node
    /// </summary>
    private class MyReparentHandler : IReparentNodeHandler
    {
      private readonly IReparentNodeHandler coreHandler;

      public MyReparentHandler(IReparentNodeHandler coreHandler) {
        this.coreHandler = coreHandler;
      }

      public bool IsReparentGesture(IInputModeContext context, INode node) {
        return coreHandler.IsReparentGesture(context, node);
      }

      public bool ShouldReparent(IInputModeContext context, INode node) {
        //Ok, this node has a table associated - disallow dragging into a group node.
        if (node.Lookup<ITable>() != null) {
          return false;
        }
        return coreHandler.ShouldReparent(context, node);
      }

      public bool IsValidParent(IInputModeContext context, INode node, INode newParent) {
        return coreHandler.IsValidParent(context, node, newParent);
      }

      public void Reparent(IInputModeContext context, INode node, INode newParent) {
        coreHandler.Reparent(context, node, newParent);
      }
    }

    public class TableGraphEditorInputMode : GraphEditorInputMode
    {
      /// <summary>
      /// Disallows the click selection of table nodes
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
      protected override bool ShouldClickSelect(IModelItem item) {
        if (item.Lookup<ITable>() != null) {
          return false;
        } else {
          return base.ShouldClickSelect(item);
        }
      }


    }

    #endregion

    #region Application start

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TableEditorForm());
    }

    #endregion
  }
}
