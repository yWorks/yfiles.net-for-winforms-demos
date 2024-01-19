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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.yFiles.Graph.ZOrder.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.ZOrder
{
  /// <summary>
  /// This demo customizes editing gestures to keep the z-order of nodes consistent.
  /// </summary>
  public partial class ZOrderForm
  {
    /// <summary>
    /// Automatically generated by Visual Studio.
    /// Wires up the UI components and adds a 
    /// <see cref="GraphControl"/> to the form.
    /// </summary>
    public ZOrderForm() {
      InitializeComponent();
      graphOverviewControl.GraphControl = graphControl;
      graphControl.FileOperationsEnabled = true;
      RegisterToolStripCommands();
      RegisterMenuItemCommands();

      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    #region Command registration

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      loadGraphMLButton.SetCommand(Commands.Open, graphControl);
      saveGraphMLButton.SetCommand(Commands.SaveAs, graphControl);

      cutButton.SetCommand(Commands.Cut, graphControl);
      copyButton.SetCommand(Commands.Copy, graphControl);
      pasteButton.SetCommand(Commands.Paste, graphControl);
      deleteButton.SetCommand(Commands.Delete, graphControl);

      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);

      zoom11ToolStripMenuItem.SetCommand(Commands.Zoom, 1.0d, graphControl);

      groupButton.SetCommand(Commands.GroupSelection, graphControl);
      ungroupButton.SetCommand(Commands.UngroupSelection, graphControl);

      // z-Order commands
      raiseButton.SetCommand(ZOrderSupport.RaiseCommand, graphControl);
      lowerButton.SetCommand(ZOrderSupport.LowerCommand, graphControl);
      toFrontButton.SetCommand(ZOrderSupport.ToFrontCommand, graphControl);
      toBackButton.SetCommand(ZOrderSupport.ToBackCommand, graphControl);

    }

    private void RegisterMenuItemCommands() {
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentToolStripMenuItem.SetCommand(Commands.FitContent, graphControl);

      openToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveToolStripMenuItem.SetCommand(Commands.SaveAs, graphControl);

      cutToolStripMenuItem.SetCommand(Commands.Cut, graphControl);
      copyToolStripMenuItem.SetCommand(Commands.Copy, graphControl);
      pasteToolStripMenuItem.SetCommand(Commands.Paste, graphControl);
      deleteToolStripMenuItem.SetCommand(Commands.Delete, graphControl);
      undoToolStripMenuItem.SetCommand(Commands.Undo, graphControl);
      redoToolStripMenuItem.SetCommand(Commands.Redo, graphControl);

      groupSelectionToolStripMenuItem.SetCommand(Commands.GroupSelection, graphControl);
      ungroupSelectionToolStripMenuItem.SetCommand(Commands.UngroupSelection, graphControl);
      expandGroupToolStripMenuItem.SetCommand(Commands.ExpandGroup, graphControl);
      collapseGroupToolStripMenuItem.SetCommand(Commands.CollapseGroup, graphControl);
      enterGroupToolStripMenuItem.SetCommand(Commands.EnterGroup, graphControl);
      exitGroupToolStripMenuItem.SetCommand(Commands.ExitGroup, graphControl);
    }

    #endregion


    /// <summary>
    /// Initializes the graph, z-order support and the input mode.
    /// </summary>
    /// <seealso cref="InitializeGraph"/>
    protected virtual void OnLoaded(object source, EventArgs e) {
      // initialize the graph
      InitializeGraph();

      // initialize consistent z-order support
      var zOrderSupport = new ZOrderSupport(graphControl);

      EnableGroupingOperations();

      ConfigureInputMode(zOrderSupport);

      LoadGraph();
    }

    private void ConfigureInputMode(ZOrderSupport zOrderSupport) {
      var geim = graphControl.InputMode as GraphEditorInputMode;
      geim.FocusableItems = GraphItemTypes.None;
      // prevent interactive label changes since they display the z-index in this demo
      geim.SelectableItems = GraphItemTypes.Node | GraphItemTypes.Edge;
      geim.AllowEditLabel = false;
      geim.AllowAddLabel = false;
      geim.AllowEditLabelOnDoubleClick = false;

      
      // update node labels showing the node's z-index
      geim.NodeCreated += (sender, args) => {
        var node = args.Item;
        UpdateLabel(node, zOrderSupport.GetZOrder(node));
      };
      
      geim.ElementsPasted += (sender, args) => {
        foreach (var node in graphControl.Graph.Nodes) {
          UpdateLabel(node, zOrderSupport.GetZOrder(node));
        }
      };

      geim.ElementsDuplicated += (sender, args) => {
        foreach (var node in graphControl.Graph.Nodes) {
          UpdateLabel(node, zOrderSupport.GetZOrder(node));
        }
      };

      zOrderSupport.ZOrderChanged += (sender, args) => {
        if (args.Node is INode) {
          UpdateLabel(args.Node as INode, args.NewZOrder);
        }
      };
    }

    /// <summary>
    /// Updates the label text to show the current z-index of the node.
    /// </summary>
    /// <param name="node">The node whose z-index has changed.</param>
    /// <param name="zIndex">The new z-index.</param>
    private void UpdateLabel(INode node, int zIndex) {
      var graph = graphControl.Graph.Contains(node)
          ? graphControl.Graph
          : graphControl.Graph.GetFoldingView().Manager.MasterGraph;

      if (node.Labels.Any(label => label.Tag is ShowZOrderFlag)) {
        graph.SetLabelText(
            node.Labels.First(label => label.Tag is ShowZOrderFlag),
            "Level: " + zIndex
        );
      } else {
        graph.AddLabel(node, "Level: " + zIndex, tag: ShowZOrderFlag.Instance);
      }
    }

    /// <summary>
    /// Initializes folding and sets default styles.
    /// </summary>
    protected virtual void InitializeGraph() {
      // Enable folding
      IFoldingView view = new FoldingManager().CreateFoldingView();
      graphControl.Graph = view.Graph;

      // Get the master graph instance and enable undoability support.
      view.Manager.MasterGraph.SetUndoEngineEnabled(true);
      // add undo support for expand/collapse operations
      view.EnqueueNavigationalUndoUnits = true;

      // copy first label of collapsed group node
      view.Manager.FolderNodeConverter = new DefaultFolderNodeConverter() { CopyFirstLabel = true };

      DemoStyles.InitDemoStyles(graphControl.Graph, foldingEnabled: true);
    }

    /// <summary>
    /// Enables interactive grouping operations.
    /// </summary>
    private void EnableGroupingOperations() {
      var geim = graphControl.InputMode as GraphEditorInputMode;
      if (geim != null) {
        geim.AllowGroupingOperations = true;
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private void newButton_Click(object sender, EventArgs e) {
      graphControl.Graph.Clear();
    }
    
    /// <summary>
    /// Loads the graph from the 'Resources' folder.
    /// </summary>
    private void LoadGraph() {
      string fileName = Path.Combine("Resources", "zOrderGraph.graphml");
      graphControl.Graph.Clear();
      using (StreamReader reader = new StreamReader(fileName)) {
        var ioHandler = graphControl.GraphMLIOHandler;
        ioHandler.Read(graphControl.Graph, reader);
      }
      var zOrderSupport = graphControl.Graph.Lookup<ZOrderSupport>();
      foreach (var node in graphControl.Graph.Nodes) {
        UpdateLabel(node, zOrderSupport.GetZOrder(node));
      }
      graphControl.FitGraphBounds();
      
      // clear undo-queue
      graphControl.Graph.GetUndoEngine().Clear();
    }

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ZOrderForm());
    }

    #endregion
  }

  /// <summary>
  /// Class used as flagging tag for node labels that display their owners z-order. 
  /// </summary>
  public class ShowZOrderFlag  {
    public static readonly ShowZOrderFlag Instance = new ShowZOrderFlag();
  }
}

