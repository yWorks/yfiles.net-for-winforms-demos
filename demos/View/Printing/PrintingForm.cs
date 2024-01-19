/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.6.
 ** Copyright (c) 2000-2024 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using Demo.yFiles.Printing.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Utils;

namespace Demo.yFiles.Printing
{
  /// <summary>
  /// This demo shows how to use and customize printing functionality.
  /// </summary>
  /// <remarks>For more details, see the description file or run the application.</remarks>
  public partial class PrintingForm : Form
  {
    #region private fields

    // Optionhandler for print options
    private OptionHandler handler;
    // region that gets printed
    private MutableRectangle exportRect;
    // printer and page settings used for printing
    private PrinterSettings printerSettings;
    private PageSettings pageSettings;
    // printable representation of the graph control
    private CanvasPrintDocument printDocument;

    #endregion

    #region Constructors

    /// <summary>
    /// Instantiates class <see cref="PrintingForm" />
    /// </summary>
    public PrintingForm() {
      InitializeComponent();

      InitializeInputModes();
      InitializeGraph();
      RegisterCommands();

      SetupOptions();
      InitializePrinting();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    #endregion

    #region Properties

    private OptionHandler Handler {
      get { return handler; }
    }

    #endregion

    #region Initialization

    private void InitializeInputModes() {
      // Create a GraphEditorInputMode instance
      var editMode = new GraphEditorInputMode();

      // and install the edit mode into the canvas.
      graphControl.InputMode = editMode;

      // create the model for the export rectangle 
      exportRect = new MutableRectangle(0, 0, 100, 100);
      // visualize it
      new RectangleIndicatorInstaller(exportRect, RectangleIndicatorInstaller.SelectionTemplateKey)
        .AddCanvasObject(graphControl.CanvasContext, graphControl.BackgroundGroup, exportRect);

      AddExportRectInputModes(editMode);
    }

    /// <summary>
    /// Adds the view modes that handle the resizing and movement of the export rectangle.
    /// </summary>
    /// <param name="inputMode"></param>
    private void AddExportRectInputModes(MultiplexingInputMode inputMode){
      // create handles for interactively resizing the export rectangle
      var rectangleHandles = new RectangleReshapeHandleProvider(exportRect) {MinimumSize = new SizeD(1, 1)};

      // create a mode that deals with the handles
      var exportHandleInputMode = new HandleInputMode{Priority = 1};

      // add it to the graph editor mode
      inputMode.Add(exportHandleInputMode);

      // now the handles
      var inputModeContext = Contexts.CreateInputModeContext(exportHandleInputMode);
      exportHandleInputMode.Handles = new DefaultObservableCollection<IHandle>
                                        {
                                          rectangleHandles.GetHandle(inputModeContext, HandlePositions.NorthEast),
                                          rectangleHandles.GetHandle(inputModeContext, HandlePositions.NorthWest),
                                          rectangleHandles.GetHandle(inputModeContext, HandlePositions.SouthEast),
                                          rectangleHandles.GetHandle(inputModeContext, HandlePositions.SouthWest),
                                        };

      // create a mode that allows for dragging the export rectangle at the sides
      var moveInputMode = new MoveInputMode
                            {
                              PositionHandler = new ExportRectanglePositionHandler(exportRect),
                              HitTestable = HitTestables.Create(
                                (context, location) => {
                                  var path = new GeneralPath(5);
                                  path.AppendRectangle(exportRect, false);
                                  return path.PathContains(location, context.HitTestRadius + 3 * context.Zoom);
                                }),
                              Priority = 41
                            };

      // add it to the edit mode
      inputMode.Add(moveInputMode);
    }

    private void InitializeGraph() {
      IGraph graph = graphControl.Graph;
      // initialize defaults
      DemoStyles.InitDemoStyles(graph);

      // create sample graph
      graph.AddLabel(graph.CreateNode(new RectD(15, 15, 50, 30)), "Node");
      INode node = graph.CreateNode(new PointD(90, 30));
      graph.CreateEdge(node, graph.CreateNode(new PointD(90, 90)));

      graphControl.FitGraphBounds();
      // initially set the export rect to enclose part of the graph's contents
      exportRect.Reshape(graphControl.ContentRect);

      graph.CreateEdge(node, graph.CreateNode(new PointD(200, 30)));

      graphControl.FitGraphBounds();

    }

    private void RegisterCommands() {
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    private void InitializePrinting() {
      // create new printer settings
      printerSettings = new PrinterSettings();
      // create new page settings
      pageSettings = new PageSettings(printerSettings);
      // create new canvas print document
      printDocument = new CanvasPrintDocument
      {
        PrinterSettings = printerSettings,
        DefaultPageSettings = pageSettings,
      };
    }

    #endregion

    #region export option handler

    private void SetupOptions() {
      // create the options
      SetupHandler();
      // create the control to visualize them
      AddEditorControlToForm();
    }

    private void AddEditorControlToForm() {
      // add a new editor control for the option handler
      var editorControl = new TableEditorFactory(){ToolbarVisible = false}.CreateControl(Handler, true, true);
      splitContainer2.Panel1.Controls.Add(editorControl);
      splitContainer2.Panel1.Controls.SetChildIndex(editorControl, 0);
      editorControl.Dock = DockStyle.Fill;
    }


    /// <summary>
    /// Initializes the option handler for the export
    /// </summary>
    private void SetupHandler() {
      handler = new OptionHandler(PRINTING);

      OptionGroup currentGroup = handler.AddGroup(OUTPUT);
      currentGroup.AddBool(HIDE_DECORATIONS, true);
      currentGroup.AddBool(EXPORT_RECTANGLE, true);

      currentGroup = handler.AddGroup(DOCUMENT_SETTINGS);

      var item = currentGroup.AddDouble(SCALE, 1.0);
      currentGroup.AddBool(CENTER_CONTENT, false);
      currentGroup.AddBool(PAGE_MARK_PRINTING, false);
      currentGroup.AddBool(SCALE_DOWN_TO_FIT_PAGE, false);
      currentGroup.AddBool(SCALE_UP_TO_FIT_PAGE, false);

      // localization
      var rm =
        new ResourceManager("Demo.yFiles.Printing.Printing",
                            Assembly.GetExecutingAssembly());
      var rmf = new ResourceManagerI18NFactory();
      rmf.AddResourceManager(Handler.Name, rm);
      Handler.I18nFactory = rmf;
    }

    #endregion

    #region eventhandlers
    
    private void SetupPrintDocumentOptions() {
      GraphControl control = graphControl;
      // check if the rectangular region or the whole viewport should be printed
      bool useRect = (bool)handler.GetValue(OUTPUT, EXPORT_RECTANGLE);
      RectD bounds = useRect ? exportRect.ToRectD() : control.Viewport;

      // check whether decorations (selection, handles, ...) should be hidden
      bool hide = (bool)handler.GetValue(OUTPUT, HIDE_DECORATIONS);
      if (hide) {
        // if so, create a new graphcontrol with the same graph
        control = new GraphControl { Graph = graphControl.Graph, Projection = graphControl.Projection };
      }

      // read CanvasPrintDocument options
      printDocument.Scale = (double)Handler.GetValue(DOCUMENT_SETTINGS, SCALE);
      printDocument.CenterContent = (bool)Handler.GetValue(DOCUMENT_SETTINGS, CENTER_CONTENT);
      printDocument.PageMarkPrinting = (bool)Handler.GetValue(DOCUMENT_SETTINGS, PAGE_MARK_PRINTING);
      printDocument.ScaleDownToFitPage = (bool)Handler.GetValue(DOCUMENT_SETTINGS, SCALE_DOWN_TO_FIT_PAGE);
      printDocument.ScaleUpToFitPage = (bool)Handler.GetValue(DOCUMENT_SETTINGS, SCALE_UP_TO_FIT_PAGE);
      // set GraphControl
      printDocument.Canvas = control;
      // set print area
      printDocument.PrintRectangle = bounds;
      printDocument.Projection = graphControl.Projection;
    }

    private void printPreviewButton_Click(object sender, EventArgs e) {
      SetupPrintDocumentOptions();
      // show new PrintPreviewDialog
      PrintPreviewDialog dialog = new PrintPreviewDialog { Document = printDocument };
      DialogResult result = dialog.ShowDialog(this);
      if (result == DialogResult.Cancel || result == DialogResult.Abort || result == DialogResult.No) {
        return;
      }
      // print
      printDocument.Print();
    }

    private void printWithDialogButton_Click(object sender, EventArgs e) {
      SetupPrintDocumentOptions();
      // show new PrintDialog
      printDocument.DefaultPageSettings.Landscape = true;
      PrintDialog printDialog = new PrintDialog
      {
        AllowCurrentPage = false,
        AllowSomePages = false,
        PrinterSettings = printerSettings,
        UseEXDialog = false,
        Document = printDocument,
        AllowPrintToFile = false,
      };
      DialogResult result = printDialog.ShowDialog(this);
      if (result == DialogResult.Cancel || result == DialogResult.Abort || result == DialogResult.No) {
        return;
      }
      // print
      printDocument.Print();
    }

    private void pageSetupButton_Click(object sender, EventArgs e) {
      SetupPrintDocumentOptions();
      // show new PageSetupDialog
      PageSetupDialog setupDialog = new PageSetupDialog
      {
        AllowMargins = true,
        AllowOrientation = true,
        AllowPaper = true,
        AllowPrinter = true,
        PrinterSettings = printerSettings,
        PageSettings = pageSettings,
        Document = printDocument,
      };
      //We just want to setup the page settings here, so we don't react to the actual dialog result
      setupDialog.ShowDialog(this);
    }

    #endregion

    #region static members

    private const string PRINTING = "PRINTING";

    private const string OUTPUT = "OUTPUT";
    private const string HIDE_DECORATIONS = "HIDE_DECORATIONS";
    private const string EXPORT_RECTANGLE = "EXPORT_RECTANGLE";

    private const string DOCUMENT_SETTINGS = "DOCUMENT_SETTINGS";
    private const string SCALE = "SCALE";
    private const string CENTER_CONTENT = "CENTER_CONTENT";
    private const string PAGE_MARK_PRINTING = "PAGE_MARK_PRINTING";
    private const string SCALE_DOWN_TO_FIT_PAGE = "SCALE_DOWN_TO_FIT_PAGE";
    private const string SCALE_UP_TO_FIT_PAGE = "SCALE_UP_TO_FIT_PAGE";

    #endregion

    #region Startup

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new PrintingForm());
    }

    #endregion

    /// <summary>
    /// Helper method that allows for reusing this window in other applications.
    /// </summary>
    /// <param name="graphControl">The graph control.</param>
    public void ShowGraph(GraphControl graphControl) {
      this.graphControl.Graph = graphControl.Graph;
      this.graphControl.Selection.Clear(); // or possibly: = graphControl.Selection;
      this.graphControl.ContentRect = graphControl.ContentRect.GetEnlarged(20);
      
      // show all of the contents
      this.graphControl.FitContent();
      
      // or possibly the same viewport
//      this.graphControl.Zoom = graphControl.Zoom;
//      this.graphControl.ViewPoint = graphControl.ViewPoint;

      var inputMode = new MultiplexingInputMode();

      // set the whole content rect as the export rectangle
      exportRect.Reshape(graphControl.ContentRect);
      // or possibly just the visible viewport
      //exportRect.Set(graphControl.Viewport);

      AddExportRectInputModes(inputMode);
      this.graphControl.InputMode = inputMode;
    }
  }
}
