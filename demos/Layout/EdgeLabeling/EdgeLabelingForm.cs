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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Labeling;

namespace Demo.yFiles.Layout.EdgeLabeling
{
  /// <summary>
  /// This demo shows how to use a labeling algorithm to place edge labels with different label models. 
  /// It demonstrates how to wrap multiple existing label models using <see cref="CompositeLabelModel"/>.
  /// </summary>
  public partial class EdgeLabelingForm : Form
  {

    #region private fields

    // Optionhandler for labeling options
    private OptionHandler handler;

    /// <summary>
    /// The Option editor control
    /// </summary>
    private EditorControl editorControl;

    #endregion

    #region constructor

    public EdgeLabelingForm() {
      InitializeComponent();
      // load description
      description.LoadFile(new MemoryStream(EdgeLabeling.description), RichTextBoxStreamType.RichText);

      // set commands to buttons and menu items
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitToSizeButton.SetCommand(Commands.FitContent, graphControl);
      fitGraphBoundsToolStripMenuItem.SetCommand(Commands.FitContent, graphControl);
      openFileToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveAsToolStripMenuItem.SetCommand(Commands.SaveAs, graphControl);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the option handler
    /// </summary>
    private OptionHandler Handler {
      get { return handler; }
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeGraph"/>
    protected virtual async void OnLoad(object src, EventArgs e) {
      // initialize the graph
      InitializeGraph();
      // initialize the styles
      InitializeStyles();
      // initialize the input mode
      InitializeInputModes();
      // setup the option handler
      SetupOptions();

      // do initial label placement
      UpdateEdgeLabels();
      await DoLabelPlacement();
      graphControl.FitGraphBounds();
    }

    /// <summary>
    /// Initializes a <see cref="GraphEditorInputMode"/> and registers
    /// the result as the <see cref="CanvasControl.InputMode"/>.
    /// </summary>
    protected virtual void InitializeInputModes() {
      var graphEditorInputMode = new GraphEditorInputMode
                                   {
                                     // initialize orthogonal edge editing
                                     OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext()
                                   };
      // add a label when an edge has been created
      graphEditorInputMode.CreateEdgeInputMode.EdgeCreated += (o, args) => graphControl.Graph.AddLabel(args.Item, "Label");
      graphControl.InputMode = graphEditorInputMode;
      // switch on orthogonal edge editing
      SetOrthogonalEdgeEditing(toggleOrthogonalEdgesButton.Checked);
    }

    private void InitializeStyles() {
      graphControl.Graph.NodeDefaults.Style = new ShinyPlateNodeStyle { Brush = Brushes.Orange, DrawShadow = false };
      graphControl.Graph.NodeDefaults.Size = new SizeD(60, 30);
    }

    /// <summary>
    /// Create the initial sample graph
    /// </summary>
    private void InitializeGraph() {
      graphControl.ImportFromGraphML("Resources\\orthogonal.graphml");
      var graph = graphControl.Graph;
    }

    #endregion

    /// <summary>
    /// Does the label placement using the generic labeling algorithm. Before this, the model of the labels is
    /// set according to the option handlers settings.
    /// </summary>
    private async Task DoLabelPlacement() {
      if (runningLayout) {
        return;
      }
      
      SetRunningLayout(true);
      UpdateEdgeLabels();
      //configure and run the layout algorithm
      var labelingAlgorithm = new GenericLabeling
                                          {
                                            OptimizationStrategy = OptimizationStrategy.Balanced,
                                            PlaceEdgeLabels = true,
                                            PlaceNodeLabels = false,
                                            ProfitModel = new ExtendedLabelCandidateProfitModel(),
                                            CustomProfitModelRatio = 0.01,
                                          };


      var layoutData = new LabelingData {
        EdgeLabelPreferredPlacement = {
          Constant = new PreferredPlacementDescriptor {
            PlaceAlongEdge = LabelPlacements.AtCenter
          }
        }
      };
      await graphControl.MorphLayout(labelingAlgorithm, TimeSpan.FromMilliseconds(500), layoutData);
      SetRunningLayout(false);
    }

    private void SetRunningLayout(bool running) {
      runningLayout = running;
      editorControl.Enabled = !running;
      toolBar.Enabled = !running;
    }

    private bool runningLayout;

    /// <summary>
    /// Updates the labels' properties as specified in the option handler
    /// </summary>
    private void UpdateEdgeLabels() {

      // get a label model for the current option handler settings
      CompositeLabelModel labelModel = GetCurrentEdgeLabelModel();

      // set label model as default
      graphControl.Graph.EdgeDefaults.Labels.LayoutParameter = labelModel.CreateDefaultParameter();

      // set autoflip according to the option handler settings
      bool autoflip = (bool)handler[LABEL_AUTOFLIP].Value;
      ((DefaultLabelStyle) graphControl.Graph.EdgeDefaults.Labels.Style).AutoFlip = autoflip;

      // set label model and autoflip for each label in the graph
      foreach(var label in graphControl.Graph.Labels) {
        if (label.Owner is IEdge) {
          graphControl.Graph.SetLabelLayoutParameter(label, labelModel.FindBestParameter(label, labelModel, label.GetLayout()));
          if (label.Style is DefaultLabelStyle) {
            ((DefaultLabelStyle) label.Style).AutoFlip = autoflip;
          }
        }
      }
    }

    /// <summary>
    /// Gets a label model with the properties as specified in the option handler.
    /// </summary>
    /// <returns>a composite edge label model containing the selected model and 
    /// the same label model, 90 degrees rotated, if 90 degrees deviation is enabled.</returns>
    private CompositeLabelModel GetCurrentEdgeLabelModel() {
      double angle = ToRadians((double) handler[LABEL_ANGLE].Value);

      // initialize a composite label model containing the specified label model
      CompositeLabelModel compositeLabelModel = new CompositeLabelModel { 
        LabelModels = {
          GetEdgeLabelModel((bool)handler[LABEL_AUTOROTATION].Value, (double)handler[LABEL_EDGE_TO_LABEL_DIST].Value, angle, angle)}};

      // if enabled, add a 90 degrees rotated label model to the composite
      if ((bool) handler[LABEL_ALLOW_90_DEGREE_DEVIATION].Value) {
        //add model that creates label candidates for the alternative angle
        double rotatedAngle = (angle + Math.PI*0.5)%(2.0*Math.PI);
        compositeLabelModel.LabelModels.Add(GetEdgeLabelModel((bool) handler[LABEL_AUTOROTATION].Value,
                                                      (double) handler[LABEL_EDGE_TO_LABEL_DIST].Value, rotatedAngle, angle));
      }
      return compositeLabelModel;
    }

    /// <summary>
    /// Creates a label model with the given parameters
    /// </summary>
    private static ILabelModel GetEdgeLabelModel(bool autoRotationEnabled, double distance, double angle, double optAngle) {
      return new DescriptorWrapperLabelModel(
          new EdgePathLabelModel
          {
            Angle = angle,
            Distance = distance,
            AutoRotation = autoRotationEnabled,
            SideOfEdge = distance < 0 ? EdgeSides.LeftOfEdge : distance > 0 ? EdgeSides.RightOfEdge : EdgeSides.OnEdge
          })
      {
        Descriptor = new LabelCandidateDescriptor() { Profit = angle == optAngle ? 1.0 : 0.5 }
      };
    }


    private static double ToRadians(double d) {
      // calculates radians from degrees
      return d*(Math.PI/180);
    }

    /// <summary>
    /// Switches orthogonal edge creation and editing on/off.
    /// </summary>
    /// <param name="orthogonal"><see langword="true"/> if edges should be edited and created orthogonally.</param>
    private void SetOrthogonalEdgeEditing(bool orthogonal) {
      var graphEditorInputMode = (GraphEditorInputMode)graphControl.InputMode;
      graphEditorInputMode.OrthogonalEdgeEditingContext.Enabled = orthogonal;
    }

    #region option handler

    private void SetupOptions() {
      SetupHandler();
      
      // create the EditorControl
      DefaultEditorFactory tableEditorFactory = new DefaultEditorFactory();
      //tableEditorFactory.ToolbarVisible = false;
      editorControl = tableEditorFactory.CreateControl(Handler, true, true);
      editorControl.Dock = DockStyle.Fill;
      editorControl.Padding = new Padding(5);

      //order is important here...
      propertiesPanel.Controls.Add(editorControl);
      propertiesPanel.Controls.SetChildIndex(editorControl, 0);
      propertiesPanel.PerformLayout();
    }


    /// <summary>
    /// Initializes the option handler for the export
    /// </summary>
    private void SetupHandler() {
      handler = new OptionHandler(EDGE_LABELING);
      OptionGroup currentGroup = handler;
      currentGroup.AddDouble(LABEL_ANGLE, 0.0d).PropertyChanged += HandlerPropertyChanged;
      currentGroup.AddBool(LABEL_AUTOFLIP, true).PropertyChanged += HandlerPropertyChanged;
      var autoRotationItem = currentGroup.AddBool(LABEL_AUTOROTATION, true);
      autoRotationItem.PropertyChanged += HandlerPropertyChanged;
      currentGroup.AddBool(LABEL_ALLOW_90_DEGREE_DEVIATION, true).PropertyChanged += HandlerPropertyChanged;
      currentGroup.AddDouble(LABEL_EDGE_TO_LABEL_DIST, 0.0d).PropertyChanged += HandlerPropertyChanged;

      // localization
      var rm =
        new ResourceManager("Demo.yFiles.Layout.EdgeLabeling.EdgeLabeling",
                            Assembly.GetExecutingAssembly());
      var rmf = new ResourceManagerI18NFactory();
      rmf.AddResourceManager(Handler.Name, rm);
      Handler.I18nFactory = rmf;

    }

    private async void HandlerPropertyChanged(object sender, PropertyChangedEventArgs e) {
      await DoLabelPlacement();
    }

    #endregion

    #region static members

    private const string EDGE_LABELING = "EDGE_LABELING";
    private const string LABEL_ANGLE = "LABEL_ANGLE";
    private const string LABEL_AUTOFLIP = "LABEL_AUTOFLIP";
    private const string LABEL_AUTOROTATION = "LABEL_AUTOROTATION";
    private const string LABEL_ALLOW_90_DEGREE_DEVIATION = "LABEL_ALLOW_90_DEGREE_DEVIATION";
    private const string LABEL_EDGE_TO_LABEL_DIST = "LABEL_EDGE_TO_LABEL_DIST";

    #endregion

    #region UI handlers

    /// <summary>
    /// Handles the OnClick event of the PlaceLabelsButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void PlaceLabelsButton_OnClick(object sender, EventArgs e) {
      DoLabelPlacement();
    }

    /// <summary>
    /// Callback that loads the "orthogonal.graphml" sample into the graph control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private async void Orthogonal_OnClick(object sender, EventArgs e) {
      graphControl.ImportFromGraphML("Resources/orthogonal.graphml");
      await DoLabelPlacement();
    }

    /// <summary>
    /// Callback that loads the "organic.graphml" sample into the graph control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private async void Organic_OnClick(object sender, EventArgs e) {
      graphControl.ImportFromGraphML("Resources/organic.graphml");
      await DoLabelPlacement();
    }

    /// <summary>
    /// Callback that toggles the state of the orthogonal edge editing.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void OrthogonalEdgesButtonClick(object sender, EventArgs e) {
      SetOrthogonalEdgeEditing(toggleOrthogonalEdgesButton.Checked);
    }

    #endregion

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new EdgeLabelingForm());
    }
    
    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    #endregion
  }
}
