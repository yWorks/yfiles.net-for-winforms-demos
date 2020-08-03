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
using System.IO;
using System.Windows.Forms;
using Tutorial.CustomStyles.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// This demo shows how to create and use a relatively simple, non-interactive custom style
  /// for nodes, labels, edges, and ports, as well as a custom arrow.
  /// </summary>
  public partial class StyleTutorialForm : Form
  {

    ////////////////////////////////////////////////////
    //////////////// New in this sample ////////////////
    ////////////////////////////////////////////////////

    /// <summary>
    /// Adds and configures the <see cref="BridgeManager"/>.
    /// </summary>
    private void ConfigureBridges() {
      // create a new BridgeManager
      // The graph item styles are responsible for both 
      // providing obstacles and drawing bridges,
      // the bridge manager collects the obstacles and
      // updates given edge paths to add bridges.
      // see MySimpleEdgeStyle
      var bridgeManager = new BridgeManager();

      // Convenience class that just queries all model item
      GraphObstacleProvider provider = new GraphObstacleProvider();

      // Register an IObstacleProvider, bridgeManager will query all registered obstacle providers
      // to determine if a bridge must be created
      bridgeManager.AddObstacleProvider(provider);
      // Bind the bridge manager to the GraphControl
      bridgeManager.CanvasControl = graphControl;
    }

    ////////////////////////////////////////////////////

    /// <summary>
    /// Sets a custom NodeStyle instance as a template for newly created
    /// nodes in the graph.
    /// </summary>
    protected void InitializeGraph() {
      IGraph graph = graphControl.Graph;

      // Create a new style and use it as default node style
      graph.NodeDefaults.Style = new MySimpleNodeStyle();
      // Create a new style and use it as default edge style
      graph.EdgeDefaults.Style = new MySimpleEdgeStyle();

      graph.NodeDefaults.Size = new SizeD(50, 50);

      // Create some graph elements with the above defined styles.
      CreateSampleGraph();
    }

    #region Constructor

    public StyleTutorialForm() {
      InitializeComponent();
      graphControl.FileOperationsEnabled = true;
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // initialize the graph
      InitializeGraph();

      // initialize the input mode
      graphControl.InputMode = CreateEditorMode();

      graphControl.FitGraphBounds();

      RegisterCommands();

      //////////////// New in this sample ////////////////
      // Configure the BridgeManager
      ConfigureBridges();
    }

    #endregion

    #region Command Registration

    private void RegisterCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
      openButton.SetCommand(Commands.Open, graphControl);
      saveButton.SetCommand(Commands.SaveAs, graphControl);
    }

    /// <summary>
    /// Creates the default input mode for the GraphControl,
    /// a <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <returns>a new GraphEditorInputMode instance</returns>
    private IInputMode CreateEditorMode() {
      GraphEditorInputMode mode = new GraphEditorInputMode
                                    {
                                      //We enable label editing
                                      AllowEditLabel = true
                                    };
      return mode;
    }


    #endregion

    #region Graph creation
    /// <summary>
    /// Creates the initial sample graph.
    /// </summary>
    private void CreateSampleGraph() {
      IGraph graph = graphControl.Graph;
      INode node0 = graph.CreateNode(new RectD(-100, -50, 30, 30));
      INode node1 = graph.CreateNode(new RectD(100, -50, 30, 30));
      IEdge edge0 = graph.CreateEdge(node0, node1);
      INode node2 = graph.CreateNode(new RectD(-100, 50, 30, 30));
      INode node3 = graph.CreateNode(new RectD(100, 50, 30, 30));
      IEdge edge2 = graph.CreateEdge(node2, node3);
      INode node4 = graph.CreateNode(new RectD(-50, -100, 30, 30));
      INode node5 = graph.CreateNode(new RectD(-50, 100, 30, 30));
      IEdge edge3 = graph.CreateEdge(node4, node5);
      INode node6 = graph.CreateNode(new RectD(50, -100, 30, 30));
      INode node7 = graph.CreateNode(new RectD(50, 100, 30, 30));
      IEdge edge4 = graph.CreateEdge(node6, node7);
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
