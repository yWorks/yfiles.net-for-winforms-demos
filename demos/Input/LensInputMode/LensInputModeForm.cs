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
using Demo.yFiles.Graph.Input.LensInputMode.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;

namespace Demo.yFiles.Graph.Input.LensInputMode
{
  /// <summary>
  /// Shows how to use a special LensInputMode to magnify the currently hovered over part of the graph.
  /// </summary>
  public partial class LensInputModeForm : Form
  {
    /// <summary>
    /// The <see cref="LensInputMode"/> displaying the "magnifying glass".
    /// </summary>
    private readonly LensInputMode lensInputMode = new LensInputMode();

    #region Initialization

    public LensInputModeForm() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      RegisterToolStripCommands();
    }

    private void OnWindowLoaded(object sender, EventArgs e) {
      // Set a nicer node style and create the sample graph
      DemoStyles.InitDemoStyles(GraphControl.Graph);

      var graphEditorInputMode = new GraphEditorInputMode();
      graphEditorInputMode.Add(lensInputMode);
      GraphControl.InputMode = graphEditorInputMode;

      InitializeGraph(GraphControl.Graph);
    }

    #endregion

    #region Sample Graph Creation

    /// <summary>
    /// Creates the sample graph.
    /// </summary>
    private void InitializeGraph(IGraph graph) {
      INode[] nodes = new INode[16];
      int count = 0;
      for (int i = 1; i < 5; i++) {
        nodes[count++] = graph.CreateNode(new PointD(50 + 40*i, 260));
        nodes[count++] = graph.CreateNode(new PointD(50 + 40*i, 40));
        nodes[count++] = graph.CreateNode(new PointD(40, 50 + 40*i));
        nodes[count++] = graph.CreateNode(new PointD(260, 50 + 40*i));
      }

      for (int i = 0; i < nodes.Length; i++) {
        graph.AddLabel(nodes[i], "" + i);
      }

      graph.CreateEdge(nodes[0], nodes[1]);

      graph.CreateEdge(nodes[5], nodes[4]);

      graph.CreateEdge(nodes[2], nodes[3]);

      graph.CreateEdge(nodes[7], nodes[6]);

      graph.CreateEdge(nodes[2 + 8], nodes[3 + 8]);
      graph.CreateEdge(nodes[7 + 8], nodes[6 + 8]);

      graph.CreateEdge(nodes[0 + 8], nodes[1 + 8]);
      graph.CreateEdge(nodes[5 + 8], nodes[4 + 8]);

      // enable undo...
      graph.SetUndoEngineEnabled(true);

      GraphControl.FitGraphBounds();
    }

    #endregion

    #region Event Handlers

    private void ZoomPresetChanged(object sender, EventArgs e) {
      if (double.TryParse(lensZoom.Text, out var zoom)) {
        lensInputMode.ZoomFactor = zoom;
      }
    }

    private void SizePresetChanged(object sender, EventArgs e) {
      if (int.TryParse(lensSize.Text, out var size)) {
        lensInputMode.Size = new Size(size, size);
      }
    }

    private void ToggleProjection(object sender, EventArgs e) {
      GraphControl.Projection = GraphControl.Projection.Equals(Projections.Identity) ? Projections.Isometric : Projections.Identity;
    }

    private void EnableProjection(object sender, EventArgs e) {
      GraphControl.Projection = Projections.Isometric;
    }

    #endregion

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, GraphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, GraphControl);
      fitContentButton.SetCommand(Commands.FitContent, GraphControl);
      undoButton.SetCommand(Commands.Undo, GraphControl);
      redoButton.SetCommand(Commands.Redo, GraphControl);
    }

    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new LensInputModeForm());
    }

    #endregion
  }
}
