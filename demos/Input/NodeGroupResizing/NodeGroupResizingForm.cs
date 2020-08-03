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
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.NodeGroupResizing.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.NodeGroupResizing
{
  /// <summary>
  /// Shows how to reshape a group of nodes as one unit.
  /// </summary>
  /// <remarks>
  /// A custom <see cref="IInputMode"/> implementation is used to create handles for the unit and custom
  /// <see cref="IReshapeHandler"/> implementations include the logic to either
  /// <see cref="NodeGroupResizing.ResizeMode.Resize">move and resize</see> or just
  /// <see cref="NodeGroupResizing.ResizeMode.Scale">move</see> the nodes.
  /// </remarks>
  public partial class NodeGroupResizingForm
  {
    public NodeGroupResizingForm() {
      InitializeComponent();
      RegisterToolStripCommands();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);
      cutButton.SetCommand(Commands.Cut, graphControl);
      copyButton.SetCommand(Commands.Copy, graphControl);
      pasteButton.SetCommand(Commands.Paste, graphControl);
      deleteButton.SetCommand(Commands.Delete, graphControl);
      groupButton.SetCommand(Commands.GroupSelection, graphControl);
      ungroupButton.SetCommand(Commands.UngroupSelection, graphControl);
    }

    private NodeGroupResizingInputMode nodeGroupResizingInputMode;

    private GraphEditorInputMode graphEditorInputMode;

    private void OnLoaded(object sender, EventArgs e) {
      // enable undo engine
      graphControl.Graph.SetUndoEngineEnabled(true);

      graphEditorInputMode = new GraphEditorInputMode();

      // prepare orthogonal edge editing
      graphEditorInputMode.OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext { Enabled = false };

      // allow grouping operations
      graphEditorInputMode.AllowGroupingOperations = true;

      // prepare snapping
      graphEditorInputMode.SnapContext = new GraphSnapContext { Enabled = false };

      graphControl.InputMode = graphEditorInputMode;

      // set minimum and maximum sizes for all non-group nodes (group nodes should be able to grow larger so they can
      // contain arbitrary numbers of nodes)
      var sizeConstraintProvider = new NodeSizeConstraintProvider(new SizeD(10, 10), new SizeD(100, 100));
      graphControl.Graph
                  .GetDecorator()
                  .NodeDecorator
                  .SizeConstraintProviderDecorator
                  .SetFactory(node => !graphControl.Graph.IsGroupNode(node), node => sizeConstraintProvider);

      // add a custom input mode to the GraphEditorInputMode that shows a single set of handles when multiple nodes are selected
      nodeGroupResizingInputMode = new NodeGroupResizingInputMode {
          Margins = new InsetsD(10), 
          Mode = NodeGroupResizing.ResizeMode.Resize
      };
      graphEditorInputMode.Add(nodeGroupResizingInputMode);

      // load sample graph
      graphControl.ImportFromGraphML("Resources/sampleGraph.graphml");
      graphControl.FitContent();
      graphControl.Graph.GetUndoEngine().Clear();

      // set style defaults
      graphControl.Graph.NodeDefaults.Style = new ShinyPlateNodeStyle { Brush = Brushes.Orange };
      Color groupNodeColor = Color.FromArgb(255, 214, 229, 248);
      var groupNodeDefaults = graphControl.Graph.GroupNodeDefaults;
      groupNodeDefaults.Style = new PanelNodeStyle {
          Color = groupNodeColor, 
          Insets = new InsetsD(5, 18, 5, 5), 
          LabelInsetsColor = groupNodeColor,
      };
      groupNodeDefaults.Labels.Style = new DefaultLabelStyle { StringFormat  = {Alignment = StringAlignment.Far} };
      groupNodeDefaults.Labels.LayoutParameter = InteriorStretchLabelModel.North;

      cmbResizeMode.SelectedIndex = 0;
    }

    private void SnappingButtonClick(object sender, EventArgs e) {
      graphEditorInputMode.SnapContext.Enabled = snappingButton.Checked;
    }

    private void OrthogonalEditingButtonClick(object sender, EventArgs e) {
      graphEditorInputMode.OrthogonalEdgeEditingContext.Enabled = orthogonalButton.Checked;
    }

    private void ResizeModeSelectionChanged(object sender, EventArgs e) {
      if (nodeGroupResizingInputMode != null) {
        nodeGroupResizingInputMode.Mode = cmbResizeMode.SelectedIndex == 0
            ? NodeGroupResizing.ResizeMode.Resize
            : NodeGroupResizing.ResizeMode.Scale;
      }
    }

    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new NodeGroupResizingForm());
    }

    #endregion
  }
}
