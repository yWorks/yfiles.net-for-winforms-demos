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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Demo.yFiles.ImageExport.Properties;
using Demo.yFiles.Option.Constraint;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Utils;

namespace Demo.yFiles.ImageExport
{
  /// <summary>
  /// This demo shows how to export a graph or part of it to different 
  /// pixel and vector format images like JPEG, PNG, TIFF, GIF, BMP or EMF.
  /// </summary>
  /// <remarks>For more details, see the description file or run the application.</remarks>
  public partial class ImageExportForm : Form
  {
    #region private fields

    // Optionhandler for export options
    private OptionHandler handler;
    // Currently focused CanvasControl for which the zoom buttons work
    private CanvasControl focusedCanvas;
    // region that gets exported
    private MutableRectangle exportRect;
    #endregion

    #region Constructors

    public ImageExportForm() {
      InitializeComponent();

      InitializeInputModes();
      InitializeGraph();

      SetupOptions();

      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // Make the commands use the currently focused CanvasControl
      zoomOutButton.Click += (sender, args) => Commands.DecreaseZoom.Execute(null, GetFocusedCanvas());
      zoomInButton.Click += (sender, args) => Commands.IncreaseZoom.Execute(null, GetFocusedCanvas());
      fitContentButton.Click += (sender, args) => Commands.FitContent.Execute(null, GetFocusedCanvas());
      zoom_1_1_Button.Click += (sender, args) => GetFocusedCanvas().Zoom = 1;
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
      // create handles for interactivel resizing the export rectangle
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
                                  return path.PathContains(location, context.HitTestRadius);
                                }),
                              Priority = 41
                            };

      // add it to the edit mode
      inputMode.Add(moveInputMode);
    }

    private void InitializeGraph() {
      IGraph graph = graphControl.Graph;
      // initialize defaults
      graph.NodeDefaults.Style = new ShinyPlateNodeStyle { Brush = Brushes.DarkOrange };
      graph.EdgeDefaults.Style = new PolylineEdgeStyle {TargetArrow = Arrows.Default};

      // create sample graph
      graph.AddLabel(graph.CreateNode(new PointD(30, 30)), "Node");
      INode node = graph.CreateNode(new PointD(90, 30));
      graph.CreateEdge(node, graph.CreateNode(new PointD(90, 90)));

      graphControl.FitGraphBounds();
      // initially set the export rect to enclose part of the graph's contents
      exportRect.Reshape(graphControl.ContentRect);

      graph.CreateEdge(node, graph.CreateNode(new PointD(200, 30)));

      graphControl.FitGraphBounds();

    }

    private CanvasControl GetFocusedCanvas() {
      return focusedCanvas ?? graphControl;
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
      var editorControl = new TableEditorFactory(){ToolbarVisible = false }.CreateControl(Handler, true, true);
      splitContainer2.Panel1.Controls.Add(editorControl);
      splitContainer2.Panel1.Controls.SetChildIndex(editorControl, 0);
      editorControl.Dock = DockStyle.Fill;
    }

    /// <summary>
    /// Initializes the option handler for the export
    /// </summary>
    private void SetupHandler() {
      handler = new OptionHandler(IMAGE_EXPORT);
      handler.PropertyChanged += handler_PropertyChanged;
      OptionGroup currentGroup = handler.AddGroup(OUTPUT);
      OptionItem formatItem = currentGroup.AddList(FORMAT, Formats.Keys, FORMAT_JPG);
      currentGroup.AddBool(HIDE_DECORATIONS, true);
      currentGroup.AddBool(EXPORT_RECTANGLE, true);
      currentGroup = handler.AddGroup(BOUNDS);


      OptionItem sizeItem = currentGroup.AddList(SIZE, SizeModes, USE_ORIGINAL_SIZE);
      IOptionItem widthItem = currentGroup.AddInt(WIDTH, DefaultWidth, 1, int.MaxValue);
      IOptionItem heightItem = currentGroup.AddInt(HEIGHT, DefaultHeight, 1, Int32.MaxValue);

      currentGroup.AddDouble(SCALE, DefaultScale);
      currentGroup.AddDouble(ZOOM, DefaultZoom);

      currentGroup = handler.AddGroup(GRAPHICS);

      currentGroup.AddGeneric(SMOOTHING, SimpleSmoothingMode.HighSpeed).SetAttribute(
        OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      currentGroup.AddGeneric(TEXTRENDERING, TextRenderingHint.SystemDefault).SetAttribute(
        OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      currentGroup.AddGeneric(INTERPOLATION, InterpolationMode.Invalid).SetAttribute(
        OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      currentGroup.AddGeneric(PIXELOFFSET, PixelOffsetMode.Invalid).SetAttribute(
        OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);

      currentGroup = handler.AddGroup(MARGINS);

      currentGroup.AddInt(LEFT_MARGIN, DefaultMargins);
      currentGroup.AddInt(RIGHT_MARGIN, DefaultMargins);
      currentGroup.AddInt(TOP_MARGIN, DefaultMargins);
      currentGroup.AddInt(BOTTOM_MARGIN, DefaultMargins);

      currentGroup = handler.AddGroup(JPG);
      DoubleOptionItem qualityItem = currentGroup.AddDouble(QUALITY, DefaultQuality);

      currentGroup = handler.AddGroup(TIFF);
      OptionItem colorDepthItem = currentGroup.AddList(COLOR_DEPTH, ColorDepthList.Keys, DefaultDepth);
      OptionItem compressionItem = currentGroup.AddList(COMPRESSION,
                                                        Compressions.Keys, DefaultCompression);


      currentGroup = handler.AddGroup(PNG);
      BoolOptionItem transparentPNGItem = currentGroup.AddBool(TRANSPARENT, false);

      currentGroup = handler.AddGroup(EMF);
      BoolOptionItem transparentEMFItem = currentGroup.AddBool(TRANSPARENT, false);

      
      var cm = new ConstraintManager(Handler);
      cm.SetEnabledOnValueEquals(sizeItem, SPECIFY_WIDTH, widthItem);
      cm.SetEnabledOnValueEquals(sizeItem, SPECIFY_HEIGHT, heightItem);

      cm.SetEnabledOnValueEquals(formatItem, FORMAT_EMF, transparentEMFItem);
      cm.SetEnabledOnValueEquals(formatItem, FORMAT_PNG, transparentPNGItem);

      cm.SetEnabledOnValueEquals(formatItem, FORMAT_JPG, qualityItem);
      cm.SetEnabledOnValueEquals(formatItem, FORMAT_TIFF, colorDepthItem);
      cm.SetEnabledOnValueEquals(formatItem, FORMAT_TIFF, compressionItem);

      // localization
      var rm =
        new ResourceManager("Demo.yFiles.ImageExport.ImageExport",
                            Assembly.GetExecutingAssembly());
      var rmf = new ResourceManagerI18NFactory();
      rmf.AddResourceManager(Handler.Name, rm);
      Handler.I18nFactory = rmf;
    }

    /// <summary>
    /// Replaces the <see cref="SmoothingMode"/> enum.
    /// </summary>
    /// <remarks>
    /// Simplifies the choice by leaving out equivalent modes. Also has no <see cref="SmoothingMode.Invalid"/>
    /// mode which would cause the rendering to break.
    /// </remarks>
    private enum SimpleSmoothingMode
    {
      HighSpeed = SmoothingMode.HighSpeed,
      HighQuality = SmoothingMode.HighQuality
    }

    #endregion

    #region ImageExport

    private void ImageExportToPreview() {

      Image image = ImageExport();

      ImageVisual imageVisual = new ImageVisual(image);

      if (previewCanvas.RootGroup.First != null) {
        previewCanvas.RootGroup.First.Remove();
      }
      previewCanvas.RootGroup.AddChild(imageVisual, CanvasObjectDescriptors.Visual);
      previewCanvas.ContentRect = exportRect.ToRectD().GetEnlarged(20);
    }

    private class ImageVisual : IVisual
    {
      private readonly Image image;

      public ImageVisual(Image image) {
        this.image = image;
      }

      public void Paint(IRenderContext context, Graphics g) {
        if (image != null) {
          g.DrawImage(image, 0, 0);
        }
      }
    }

    [CanBeNull]
    private Image ImageExport() {
      GraphControl control = graphControl;
      // check if the reclangular region or the whole viewport should be exported
      bool useRect = (bool) handler.GetValue(OUTPUT, EXPORT_RECTANGLE);
      RectD bounds = useRect ? exportRect.ToRectD() : control.Viewport;

      // get the format
      string formatChoice = (string) Handler.GetValue(OUTPUT, FORMAT);
      string format = Formats[formatChoice];
      // check whether decorations (selection, handles, ...) should be hidden
      bool hide = (bool) handler.GetValue(OUTPUT, HIDE_DECORATIONS);
      if (hide) {
        // if so, create a new graphcontrol whith the same graph
        control = new GraphControl {Graph = graphControl.Graph};
      }
      IImageExporter exporter;
      ContextConfigurator config = GetConfig(bounds, !useRect);
      
      if (format.Equals("EMF")) {
        var transparentBackground = (bool)Handler.GetValue(EMF, TRANSPARENT);
        // create the exporter
        EmfImageExporter emfImageExporter = new EmfImageExporter(config);
        emfImageExporter.FillBackground = !transparentBackground;
        exporter = emfImageExporter;
        var memoryStream = new MemoryStream();
        exporter.Export(control, memoryStream);
        // reset the stream
        memoryStream.Position = 0;
        // and read back the metafile for display
        return new Metafile(memoryStream);
      } else {
        // create the exporter
        PixelImageExporter pixelImageExport = new PixelImageExporter(config);
        AddExportParameters(pixelImageExport, format);
        pixelImageExport.OutputFormat = format;
        // check if the format is transparent PNG
        var transparentBackground = (bool)Handler.GetValue(PNG, TRANSPARENT);
        if ((!format.Equals("image/png") || !transparentBackground)) {
          // if not, set the background color
          pixelImageExport.BackColor = control.BackColor;
        }
        exporter = pixelImageExport;
        var memoryStream = new MemoryStream();
        try {
          exporter.Export(control, memoryStream);

          // reset the stream
          memoryStream.Position = 0;
          // and read back the image for display
          return new Bitmap(memoryStream);
        } catch (IOException exception) {
          MessageBox.Show(exception.Message, "I/O Error", MessageBoxButtons.OK);
          return null;
        }
      }
    }

    private void ImageExportToFile() {

      GraphControl control = graphControl;
      // check if the rectangular region or the whole viewport should be exported
      bool useRect = (bool)handler.GetValue(OUTPUT, EXPORT_RECTANGLE);
      RectD bounds = useRect ? exportRect.ToRectD() : control.Viewport;

      // get the format
      string formatChoice = (string)Handler.GetValue(OUTPUT, FORMAT);
      string format = Formats[formatChoice];
      // check whether decorations (selection, handles, ...) should be hidden
      bool hide = (bool)handler.GetValue(OUTPUT, HIDE_DECORATIONS);
      if (hide) {
        // if so, create a new graphcontrol whith the same graph
        control = new GraphControl { Graph = graphControl.Graph };
      }
      IImageExporter exporter;
      ContextConfigurator config = GetConfig(bounds, !useRect);

      if (format.Equals("EMF")) {
        var transparentBackground = (bool)Handler.GetValue(EMF, TRANSPARENT);
        // create the exporter
        EmfImageExporter emfImageExporter = new EmfImageExporter(config);
        emfImageExporter.FillBackground = !transparentBackground;
        exporter = emfImageExporter;
        saveFileDialog.Reset();
        saveFileDialog.Filter = "EMF files (*.emf)|*.emf";
        if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
          FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
          exporter.Export(control, stream);
          stream.Flush();
          stream.Close();
        }
      } else {
        // create the exporter
        PixelImageExporter pixelImageExport = new PixelImageExporter(config);
        AddExportParameters(pixelImageExport, format);
        pixelImageExport.OutputFormat = format;
        // check if the format is transparent PNG
        var transparentBackground = (bool)Handler.GetValue(PNG, TRANSPARENT);
        if ((!format.Equals("image/png") || !transparentBackground)) {
          // if not, set the background color
          pixelImageExport.BackColor = control.BackColor;
        }
        exporter = pixelImageExport;

        saveFileDialog.Reset();
        saveFileDialog.Filter = format + "|*." + format.Substring(6);
        if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
          FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
          try {
            exporter.Export(control, stream);
          } catch (IOException exception) {
            MessageBox.Show(exception.Message, "I/O Error", MessageBoxButtons.OK);
          } finally {
            stream.Flush();
            stream.Close();
          }
        }
      }
    }

    /// <summary>
    /// Gets the export configuration
    /// </summary>
    private ContextConfigurator GetConfig(RectD worldBounds, bool useViewport) {
      ContextConfigurator config = new ContextConfigurator(worldBounds);
      SetScale(config, useViewport);
      // get the margins
      int leftMargin = (int) Handler.GetValue(MARGINS, LEFT_MARGIN);
      int rightMargin = (int) Handler.GetValue(MARGINS, RIGHT_MARGIN);
      int topMargin = (int) Handler.GetValue(MARGINS, TOP_MARGIN);
      int bottomMargin = (int) Handler.GetValue(MARGINS, BOTTOM_MARGIN);
      config.Margins = new InsetsD(leftMargin, topMargin, rightMargin, bottomMargin);
      return config;
    }

    /// <summary>
    /// Gets the various export parameters for the chosen format
    /// </summary>
    private void AddExportParameters(PixelImageExporter pixelImageExport, string format) {
      if (format.Equals("image/jpeg")) {
        // get jpeg quality
        pixelImageExport.Quality = (double) Handler.GetValue(JPG, QUALITY);
      } else if (format.Equals("image/tiff")) {
        // get tiff color depth
        int colorDepthChoice = (int) Handler.GetValue(TIFF, COLOR_DEPTH);
        pixelImageExport.ColorDepth = ColorDepthList[colorDepthChoice];
        // get tiff compression
        string compressionChoice = (string) Handler.GetValue(TIFF, COMPRESSION);
        pixelImageExport.Compression = !compressionChoice.Equals(COMPRESSION_NONE);
      }
      pixelImageExport.SmoothingMode = (SmoothingMode) Handler.GetValue(GRAPHICS, SMOOTHING);
      pixelImageExport.TextRenderingHint = (TextRenderingHint) Handler.GetValue(GRAPHICS, TEXTRENDERING);
      pixelImageExport.PixelOffsetMode = (PixelOffsetMode) Handler.GetValue(GRAPHICS, PIXELOFFSET);
      pixelImageExport.InterpolationMode = (InterpolationMode) Handler.GetValue(GRAPHICS, INTERPOLATION);
    }

    /// <summary>
    /// Sets the export scale
    /// </summary>
    private void SetScale(ContextConfigurator config, bool useViewport) {
      // get the scale value specified in the options
      double userScale = (double) Handler.GetValue(BOUNDS, SCALE);
      // consider the zoom level
      if (useViewport) {
        userScale *= graphControl.Zoom;
      }
      // look if a fixed size has been specified
      string sizeChoice = (string) Handler.GetValue(BOUNDS, SIZE);
      switch (sizeChoice) {
        case (SPECIFY_WIDTH):
          int newWidth = (int) Handler.GetValue(BOUNDS, WIDTH);
          config.Scale = config.CalculateScaleForWidth((int) (userScale*newWidth));
          break;
        case (SPECIFY_HEIGHT):
          int newHeight = (int) Handler.GetValue(BOUNDS, HEIGHT);
          config.Scale = config.CalculateScaleForHeight((int) (userScale*newHeight));
          break;
        default:
          config.Scale = (float) userScale;
          break;
      }
    }

    #endregion

    #region eventhandlers

    private void exportButton_Click(object sender, EventArgs e) {
      // select the export tab, export gets triggered automatically when tab is selected
      if (graphTabControl.SelectedIndex != 1) {
        graphTabControl.SelectedIndex = 1;
      } else {
        // already displayed - update export
        ImageExportToPreview();
      }
    }

    private void exportToFileButton_Click(object sender, EventArgs e) {
      ImageExportToFile();
    }

    private void graphTabControl_SelectedIndexChanged(object sender, EventArgs e) {
      if (handler != null) {
        if (((TabControl) sender).SelectedIndex == 1) {
          // if preview tab has been selected, trigger export
          ImageExportToPreview();
          int i = ((TabControl)sender).SelectedIndex;
          focusedCanvas = i == 1 ? previewCanvas : graphControl;
          focusedCanvas.Select();
        }
      }
    }

    /// <summary>
    /// Called when a value in the option handler is changed
    /// </summary>
    void handler_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
      ImageExportToPreview();
    }

    private void copyToClipboardButton_Click(object sender, EventArgs e) {
      //Clipboard.SetDataObject(ImageExport());
      var image = ImageExport();
      if (image is Metafile) {
        // these does not work in many cases - maybe this will be fixed some day
        // Clipboard.SetImage(image);
        // Clipboard.SetData(DataFormats.EnhancedMetafile, ((Metafile)image));
        
        // meanwhile use a native code approach from MSDN
        ClipboardMetafileHelper.PutEnhMetafileOnClipboard(Handle, ((Metafile)image));
      } else {
        // This does not work either for all kinds of applications - it does for some, however
        //Clipboard.SetImage(image);

        // instead convert the image to DIB and store that in the clipboard
        byte[] bmpBytes;
        using (MemoryStream bmpStream = new MemoryStream()) {
          image.Save(bmpStream, ImageFormat.Bmp);
          bmpBytes = bmpStream.GetBuffer();
        }
        // 14 bytes into the stream - the DIB starts
        MemoryStream dibStream = new MemoryStream();
        dibStream.Write(bmpBytes, 14, bmpBytes.Length - 14);
        dibStream.Position = 0;

        // store the stream in the clipboard as Device Independent Bitmap
        DataObject dataObject = new DataObject();
        dataObject.SetData(DataFormats.Dib, true, dibStream);
        // also we add the image the ordinary way
        dataObject.SetImage(image);
        Clipboard.SetDataObject(dataObject, false);
      }
    }

    #endregion

    #region static members

    private const string IMAGE_EXPORT = "IMAGE_EXPORT";
    private const string BOUNDS = "BOUNDS";
    private const string SIZE = "SIZE";
    private const string USE_ORIGINAL_SIZE = "USE_ORIGINAL_SIZE";
    private const string SPECIFY_WIDTH = "SPECIFY_WIDTH";
    private const string SPECIFY_HEIGHT = "SPECIFY_HEIGHT";
    private const string WIDTH = "WIDTH";
    private const string HEIGHT = "HEIGHT";
    private const string MARGINS = "MARGINS";
    private const string LEFT_MARGIN = "LEFT_MARGIN";
    private const string RIGHT_MARGIN = "RIGHT_MARGIN";
    private const string TOP_MARGIN = "TOP_MARGIN";
    private const string BOTTOM_MARGIN = "BOTTOM_MARGIN";
    private const string TRANSPARENT = "TRANSPARENT";
    private const string SCALE = "SCALE";
    private const string ZOOM = "ZOOM";
    private const string JPG = "JPG";
    private const string EMF = "EMF";
    private const string PNG = "PNG";
    private const string QUALITY = "QUALITY";
    private const string TIFF = "TIFF";
    private const string COLOR_DEPTH = "COLOR_DEPTH";
    private const string COMPRESSION = "COMPRESSION";
    private const string COMPRESSION_NONE = "COMPRESSION_NONE";
    private const string COMPRESSION_LZW = "COMPRESSION_LZW";
    private const string OUTPUT = "OUTPUT";
    private const string FORMAT = "FORMAT";
    private const string FORMAT_JPG = "FORMAT_JPG";
    private const string FORMAT_PNG = "FORMAT_PNG";
    private const string FORMAT_TIFF = "FORMAT_TIFF";
    private const string FORMAT_GIF = "FORMAT_GIF";
    private const string FORMAT_BMP = "FORMAT_BMP";
    private const string FORMAT_EMF = "FORMAT_EMF";
    private const string HIDE_DECORATIONS = "HIDE_DECORATIONS";
    private const string EXPORT_RECTANGLE = "EXPORT_RECTANGLE";

    private const string GRAPHICS = "GRAPHICS";
    private const string SMOOTHING = "SMOOTHING";
    private const string INTERPOLATION = "INTERPOLATION";
    private const string PIXELOFFSET = "PIXELOFFSET";
    private const string TEXTRENDERING = "TEXTRENDERING";


    private const int DefaultWidth = 500;
    private const int DefaultHeight = 500;
    private const int DefaultMargins = 0;
    private const double DefaultScale = 1.0;
    private const double DefaultZoom = 1.0;
    private const double DefaultQuality = 0.75;
    private const int DefaultDepth = 24;
    private const string DefaultCompression = COMPRESSION_LZW;

    private static readonly List<string> SizeModes = new List<string>();

    private static readonly Dictionary<int, PixelImageExporter.ColorDepthValue> ColorDepthList =
      new Dictionary<int, PixelImageExporter.ColorDepthValue>();

    private static readonly Dictionary<string, EncoderValue> Compressions =
      new Dictionary<string, EncoderValue>();

    private static readonly Dictionary<string, string> Formats =
      new Dictionary<string, string>();

    static ImageExportForm() {
      SizeModes.Add(USE_ORIGINAL_SIZE);
      SizeModes.Add(SPECIFY_WIDTH);
      SizeModes.Add(SPECIFY_HEIGHT);

      ColorDepthList.Add(24, PixelImageExporter.ColorDepthValue.ColorDepth24bpp);
      ColorDepthList.Add(32, PixelImageExporter.ColorDepthValue.ColorDepth32bpp);

      Compressions.Add(COMPRESSION_NONE, EncoderValue.CompressionNone);
      Compressions.Add(COMPRESSION_LZW, EncoderValue.CompressionLZW);

      Formats.Add(FORMAT_JPG, "image/jpeg");
      Formats.Add(FORMAT_TIFF, "image/tiff");
      Formats.Add(FORMAT_GIF, "image/gif");
      Formats.Add(FORMAT_PNG, "image/png");
      Formats.Add(FORMAT_BMP, "image/bmp");
      Formats.Add(FORMAT_EMF, "EMF");
    }

    #endregion

    #region Startup

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ImageExportForm());
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


      // create mode which supports moving the viewport by pressing ctrl
      var moveViewportInputMode = new MoveViewportInputMode
                                    {
                                      PressedRecognizer =
                                          (source, args) =>
                                          MouseEventRecognizers.LeftPressed(source, args) &&
                                          (ModifierKeys & Keys.Control) == Keys.Control
                                    };

      inputMode.Add(moveViewportInputMode);

      AddExportRectInputModes(inputMode);
      this.graphControl.InputMode = inputMode;
    }
  }
}
