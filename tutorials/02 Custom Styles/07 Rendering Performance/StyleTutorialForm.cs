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
using System.IO;
using System.Windows.Forms;
using Tutorial.CustomStyles.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;


namespace Tutorial.CustomStyles
{
  /// <summary>
  /// This demo shows how to create simple, non-interactive custom styles
  /// for nodes, labels, ports, and edges with custom arrows.
  /// </summary>
  public partial class StyleTutorialForm : Form
  {
    
    #region demo specific

    // The following class members exist only in this tutorial step in order to 
    // point out the difference in rendering performance

    private readonly Animator animator;

    

    private void ToggleFast(object sender, EventArgs e) {
      // switch UpdateVisual() implementation on/off
      var style = graphControl.Graph.NodeDefaults.Style as MySimpleNodeStyle;
      if (style != null) {
        style.HighPerformance = fastToggleButton.Checked;
      }
      fastToggleButton.Text = fastToggleButton.Checked ? "Fast" : "Slow";
    }
  
    private void AnimationStart_Click(object sender, EventArgs e) {
      StartAnimation();
    }

    private async void StartAnimation() {
      // animates the nodes in random fashion
      Random r = new Random(DateTime.Now.TimeOfDay.Milliseconds);
      await animator.Animate(Animations.CreateGraphAnimation(graphControl.Graph, Mappers.FromDelegate<INode, IRectangle>(node => new RectD(r.NextDouble() * 800, r.NextDouble() * 800, node.Layout.Width, node.Layout.Height)), null, null, null, TimeSpan.FromSeconds(5)));
    }

    #endregion

    #region Constructor

    public StyleTutorialForm() {
      InitializeComponent();
      graphControl.FileOperationsEnabled = true;
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // initialize the graph
      InitializeGraph();
      // initialize the input mode
      graphControl.InputMode = new GraphEditorInputMode();
      graphControl.FitGraphBounds();
      animator = new Animator(graphControl);

      RegisterCommands();
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Sets a custom style instance as a template for newly created
    /// elements in the graph.
    /// </summary>
    protected void InitializeGraph() {
      IGraph graph = graphControl.Graph;

      // Create some styles and use them as the defaults for this sample
      graph.NodeDefaults.Style = new MySimpleNodeStyle();
      graph.NodeDefaults.ShareStyleInstance = true;
      graph.EdgeDefaults.Labels.LayoutParameter = new EdgeSegmentLabelModel{Distance = 15}.CreateDefaultParameter();

      graph.NodeDefaults.Labels.Style =
          graph.EdgeDefaults.Labels.Style =
              new DefaultLabelStyle {BackgroundPen = Pens.Black, BackgroundBrush = Brushes.White};
      graph.NodeDefaults.Labels.ShareStyleInstance = true;
      graph.EdgeDefaults.Labels.ShareStyleInstance = true;
      graph.NodeDefaults.Labels.LayoutParameter = ExteriorLabelModel.North;

      // Create some graph elements with the above defined styles.
      CreateSampleGraph();
    }

    #endregion

    #region Command Registration

    private void RegisterCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    #endregion

    #region Graph creation 

    /// <summary>
    /// Creates the initial sample graph.
    /// </summary>
    private void CreateSampleGraph() {

      IGraph graph = graphControl.Graph;

      for (int i = 1; i <= 20; i++) {
        for (int j = 1; j <= 20; j++) {
          graph.CreateNode(new RectD(40 * i, 40 * j, 30, 30));
        }
      }
    }

    #endregion

    #region Standard Actions

    /// <summary>
    /// Callback action that is triggered when the user exits the application.
    /// </summary>
    protected virtual void ExitToolStripMenuItemClick(object sender, EventArgs e) {
      Application.Exit();
    }

    #endregion

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new StyleTutorialForm());
    }

    #endregion
    
  }
}
