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
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.ContextMenu.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.ContextMenu
{
  /// <summary>
  /// Shows how to implement a dynamic context menu for the nodes and for the
  /// background of a <see cref="GraphControl"/>.
  /// </summary>
  /// <remarks>
  /// See the description.rtf file or run the application to find out about what this application demonstrates.
  /// </remarks>
  public partial class ContextMenuForm : Form
  {
    /// <summary>
    /// Registers the callback method that populates the dynamic context menu, 
    /// when needed.
    /// </summary>
    /// <remarks>
    /// Note that this is the only place that interfaces with the Context Menu
    /// API. The remainder of the implementation shows how this can be
    /// customized, but for simple scenarios, all that needs to be done is shown
    /// in this method.
    /// </remarks>
    private void RegisterContextMenuCallback() {
      graphEditorInputMode.ContextMenuItems = GraphItemTypes.Node;

      // Simple implementations with static context menus could just assign 
      // a static ContextMenu instance here.
      // Instead, use a dynamic context menu:
      graphEditorInputMode.PopulateItemContextMenu += OnPopulateItemContextMenu;
    }

    /// <summary>
    /// Fills the context menu with menu items based on the clicked node.
    /// </summary>
    private void OnPopulateItemContextMenu(object sender, PopulateItemContextMenuEventArgs<IModelItem> e) {
      // first update the selection
      INode node = e.Item as INode;
      // if the cursor is over a node select it, else clear selection
      UpdateSelection(node);

      // Create the context menu items
      if (graphControl.Selection.SelectedNodes.Count > 0) {
        // at least one node is selected
        e.Menu.Items.Add(Commands.Cut, graphControl);
        e.Menu.Items.Add(Commands.Copy, graphControl);
        e.Menu.Items.Add(Commands.Paste, e.QueryLocation, graphControl);
        e.Menu.Items.Add(Commands.Delete, graphControl);
      } else {
        // no node has been hit
        var selectAllItem = new ToolStripMenuItem("Select all");
        selectAllItem.Click += (o, args) => { graphEditorInputMode.SelectAll(); };
        e.Menu.Items.Add(selectAllItem);
        e.Menu.Items.Add(Commands.Paste, e.QueryLocation, graphControl);
      }
      // open the menu
      e.ShowMenu = true;
      // mark the event as handled
      e.Handled = true;
    }

    /// <summary>
    /// Updates the node selection state when the context menu is opened on a node.
    /// </summary>
    /// <param name="node">The node or <see langword="null"/>.</param>
    private void UpdateSelection(INode node) {
      // see if no node was hit
      if (node == null) {
        // clear the whole selection
        graphControl.Selection.Clear();
      } else {
        // see if the node was selected, already
        if (!graphControl.Selection.SelectedNodes.IsSelected(node)) {
          // no - clear the remaining selection
          graphControl.Selection.Clear();
          // select the node
          graphControl.Selection.SelectedNodes.SetSelected(node, true);
          // also update the current item
          graphControl.CurrentItem = node;
        }
      }
    }

    private readonly GraphEditorInputMode graphEditorInputMode;

    #region Initialization

    public ContextMenuForm() {
      InitializeComponent();

      // initialize input mode
      graphEditorInputMode = new GraphEditorInputMode();
      graphControl.InputMode = graphEditorInputMode;
      RegisterContextMenuCallback();

      // load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // Set a nicer node style and create the sample graph
      graphControl.Graph.NodeDefaults.Style = new ShinyPlateNodeStyle{Brush = Brushes.Orange};
      CreateSampleGraph(graphControl.Graph);


    }


    #endregion

    #region Sample Graph Creation

    private void CreateSampleGraph(IGraph graph) {
      graph.AddLabel(graph.CreateNode(new PointD(100, 100)), "1");
      graph.AddLabel(graph.CreateNode(new PointD(200, 100)), "2");
      graph.AddLabel(graph.CreateNode(new PointD(300, 100)), "3");
    }

    #endregion
    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ContextMenuForm());
    }

    #endregion

  }
}
