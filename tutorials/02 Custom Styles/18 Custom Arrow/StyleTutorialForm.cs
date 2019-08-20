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
using System.IO;
using System.Windows.Forms;
using Tutorial.CustomStyles.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// This demo shows how to create simple, non-interactive custom styles
  /// for nodes, labels, ports, and edges with custom arrows.
  /// </summary>
  public partial class StyleTutorialForm : Form
  {

    #region Constructor

    public StyleTutorialForm() {
      InitializeComponent();
      graphControl.FileOperationsEnabled = true;
      try {
        description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      } catch (MissingMethodException) {
        // Workaround for https://github.com/microsoft/msbuild/issues/4581
        description.Text = "The description is not available with this version of .NET Core.";
      }

      // initialize the graph
      InitializeGraph();
      // initialize the input mode
      graphControl.InputMode = new GraphEditorInputMode();
      graphControl.FitGraphBounds();

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
      // Set the thickness of edges to 3
      graph.EdgeDefaults.Style = new MySimpleEdgeStyle { PathThickness = 3 };
      graph.EdgeDefaults.ShareStyleInstance = true;
      graph.EdgeDefaults.Labels.LayoutParameter = new EdgeSegmentLabelModel{Distance = 15}.CreateDefaultParameter();

      graph.NodeDefaults.Labels.Style =
        graph.EdgeDefaults.Labels.Style = new MySimpleLabelStyle();
      graph.NodeDefaults.Labels.ShareStyleInstance = true;
      graph.EdgeDefaults.Labels.ShareStyleInstance = true;
      graph.NodeDefaults.Labels.LayoutParameter = ExteriorLabelModel.NorthEast;

      // Create some graph elements with the above defined styles.
      CreateSampleGraph();
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

    #endregion

    #region Graph creation 

    /// <summary>
    /// Creates the initial sample graph.
    /// </summary>
    private void CreateSampleGraph() {
      IGraph graph = graphControl.Graph;
      INode node0 = graph.CreateNode(new RectD(180, 40, 30, 30));
      INode node1 = graph.CreateNode(new RectD(260, 50, 30, 30));
      INode node2 = graph.CreateNode(new RectD(284, 200, 30, 30));
      INode node3 = graph.CreateNode(new RectD(350, 40, 30, 30));
      IEdge edge0 = graph.CreateEdge(node1, node2);
      // Add some bends
      graph.AddBend(edge0, new PointD(350, 130));
      graph.AddBend(edge0, new PointD(230, 170));
      graph.CreateEdge(node1, node0);
      graph.CreateEdge(node1, node3);
      ILabel label0 = graph.AddLabel(edge0, "Edge Label");
      ILabel label1 = graph.AddLabel(node1, "Node Label");
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
