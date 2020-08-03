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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.GraphML;
using yWorks.Utils;

namespace Demo.yFiles.Graph.ComponentDragAndDrop
{
  /// <summary>
  /// A demo that shows how to make space for a subgraph that ia dragged and dropped from a palette
  /// onto the canvas.
  /// </summary>
  public partial class ComponentDragAndDropForm
  {
    /// <summary>
    /// Performs layout and animation during the drag and drop operation.
    /// </summary>
    private ClearAreaLayoutHelper layoutHelper;

    /// <summary>
    /// Gets the currently registered IGraph instance from the GraphControl.
    /// </summary>
    private IGraph Graph {
      get { return graphControl.Graph; }
    }

    /// <summary>
    /// Components that should not be modified by the layout.
    /// </summary>
    private readonly DictionaryMapper<INode, object> graphComponents;


    #region Initialization

    /// <summary>
    /// Wires up the UI components.
    /// </summary>
    public ComponentDragAndDropForm() {
      InitializeComponent();
      
      // load description
      description.LoadFile(new MemoryStream(Properties.Resources.description), RichTextBoxStreamType.RichText);

      // set commands to buttons and menu items
      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl); 
      zoomFitButton.SetCommand(Commands.FitContent, graphControl);
      
      graphComponents = new DictionaryMapper<INode, object>();
    }

    /// <summary>
    /// Initializes the graph and the input mode.
    /// </summary>
    private void OnLoad(object sender, EventArgs e) {
      InitializeInputModes();
      InitializeGraph();
      InitializePalette();
    }

    /// <summary>
    /// Registers the <see cref="GraphEditorInputMode"/> as the <see cref="CanvasControl.InputMode"/>
    /// and initializes the input mode for dropping components.
    /// </summary>
    private void InitializeInputModes() {
      // create a GraphEditorInputMode instance
      var editMode = new GraphEditorInputMode();
      graphControl.InputMode = editMode;

      // add the input mode to drop components
      var graphDropInputMode = new ComponentDropInputMode();
      graphDropInputMode.DragEntered += OnDragStarting;
      graphDropInputMode.DragOver += OnDragged;
      graphDropInputMode.ItemCreated += OnDragFinished;
      graphDropInputMode.DragLeft += OnDragCanceled;
      editMode.Add(graphDropInputMode);
    }

    /// <summary>
    /// Enables undo/redo support and initializes the default styles.
    /// </summary>
    protected virtual void InitializeGraph() {
      graphControl.Graph.SetUndoEngineEnabled(true);
      
      Graph.NodeDefaults.Style = new ShapeNodeStyle {
          Shape = ShapeNodeShape.Ellipse,
          Brush = Brushes.DarkGray,
          Pen = null
      };

      Graph.EdgeDefaults.Style = new PolylineEdgeStyle {
          Pen = new Pen(Brushes.DarkGray, 5)
      };
    }

    /// <summary>
    /// Populates the palette with the graphs stored in the resources folder.
    /// </summary>
    private void InitializePalette() {
      //Handle list item drawing
      paletteListBox.DrawItem += OnDrawItem;

      // register for the mouse down event to initiate the drag operation
      paletteListBox.MouseDown += OnMouseDown;
      
      // populate the palette 
      var ioHandler = new GraphMLIOHandler();
      foreach (var file in Directory.GetFiles("Resources").Where(f => f.EndsWith("graphml"))) {
        var graph = new DefaultGraph();
        ioHandler.Read(graph, file);
        paletteListBox.Items.Add(graph);
      }
    }

    #endregion

    #region Dragging the component

    /// <summary>
    /// The component is upon to be moved or resized.
    /// </summary>
    private void OnDragStarting(object sender, InputModeEventArgs e) {
      if (sender is ComponentDropInputMode graphDropInputMode) {
        var component = graphDropInputMode.DropData as IGraph;
        layoutHelper = new ClearAreaLayoutHelper(graphControl, component, keepComponentsButton.Checked ? graphComponents : null);
        layoutHelper.InitializeLayout();
      }
    }

    /// <summary>
    /// The component is currently be moved or resized.
    /// For each drag a new layout is calculated and applied if the previous one is completed.
    /// </summary>
    private void OnDragged(object sender, InputModeEventArgs e) {
      if (sender is ComponentDropInputMode graphDropInputMode) {
        layoutHelper.Location = graphDropInputMode.MousePosition.ToPointD();
        layoutHelper.RunLayout();
      }
    }

    /// <summary>
    /// Dragging the component has been canceled.
    /// The state before the gesture must be restored.
    /// </summary>
    private void OnDragCanceled(object sender, InputModeEventArgs e) {
      if (sender is ComponentDropInputMode) {
        layoutHelper.CancelLayout();
      }
    }

    /// <summary>
    /// The component has been dropped.
    /// We execute the layout to the final state.
    /// </summary>
    private void OnDragFinished(object sender, ItemEventArgs<IGraph> itemEventArgs) {
      if (sender is ComponentDropInputMode graphDropInputMode) {
        layoutHelper.Location = graphDropInputMode.DropLocation;
        layoutHelper.Component = itemEventArgs.Item;
        // specify the dropped nodes as a single component
        var componentId = new object();
        foreach (var node in itemEventArgs.Item.Nodes) {
          graphComponents[node] = componentId;
        }
        layoutHelper.FinishLayout();
      }
    }

    #endregion

    #region Palette event handling

    /// <summary>
    /// Starts drag and drop operation.
    /// </summary>
    private void OnMouseDown(object sender, MouseEventArgs e) {
      if (sender is ListBox listBox) {
        int itemIndex = listBox.IndexFromPoint(e.X, e.Y);
        if (itemIndex != ListBox.NoMatches &&
            listBox.Items[itemIndex] is IGraph graph) {
          var dao = new DataObject();
          dao.SetData(typeof(IGraph), graph);
          paletteListBox.DoDragDrop(dao, DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Move);
        }
      }
    }

    /// <summary>
    /// Paint <see cref="IGraph">component</see> as elements of the palette.
    /// </summary>
    private static void OnDrawItem(object sender, DrawItemEventArgs e) {
      if (sender is ListBox listBox && listBox.Items[e.Index] is IGraph component) {
        var bounds = e.Bounds;
        InsetsD insets = new InsetsD(10);

        // create a GraphControl that shows the component
        var componentControl = new GraphControl {
            Size = new Size(bounds.Width - (int) insets.HorizontalInsets, bounds.Height - (int) insets.VerticalInsets),
            HorizontalScrollBarPolicy = ScrollBarVisibility.Never,
            VerticalScrollBarPolicy = ScrollBarVisibility.Never,
            Graph = component
        };
        componentControl.FitGraphBounds();

        // create a bitmap with the same size as the GraphControl
        Bitmap bm = new Bitmap(componentControl.Size.Width, componentControl.Size.Height);
        Graphics g = Graphics.FromImage(bm);
        e.DrawBackground();
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.Clear(Color.White);

        // render the content of the GraphControl into the bitmap
        ContextConfigurator cc = new ContextConfigurator(componentControl.ContentRect);
        var renderContext = cc.CreateRenderContext(componentControl, g);
        componentControl.RenderContent(renderContext, g);

        // render the image as an element of the palette
        var listGraphics = e.Graphics;
        var oldClip = listGraphics.Clip;
        listGraphics.IntersectClip(bounds);
        listGraphics.Clear(listBox.BackColor);
        listGraphics.DrawImage(bm, bounds.X + (int) insets.Left, bounds.Y + (int) insets.Top, bm.Width, bm.Height);
        listGraphics.Clip = oldClip;
        e.DrawFocusRectangle();

        componentControl.Dispose();
        g.Dispose();
        bm.Dispose();
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
      Application.Run(new ComponentDragAndDropForm());
    }

    #endregion
  }
}
