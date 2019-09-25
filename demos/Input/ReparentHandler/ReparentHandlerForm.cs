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
using Demo.yFiles.Graph.Input.ReparentHandler.Properties;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.ReparentHandler
{
  /// <summary>
  /// Shows how to customize the reparent gesture in a grouped graph
  /// by implementing a custom <see cref="IReparentNodeHandler"/>.
  /// </summary>
  public partial class ReparentHandlerForm : Form
  {
    #region Initialization

    public ReparentHandlerForm() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    private void FormLoaded(object sender, EventArgs e) {
      IGraph graph = graphControl.Graph;

      // Create a default editor input mode and configure it
      GraphEditorInputMode graphEditorInputMode = new GraphEditorInputMode
      {
        AllowGroupingOperations = true,
        AllowGroupSelection = false,
        AllowClipboardOperations = false
      };

      // Assign the custom reparent handler of this demo
      graphEditorInputMode.ReparentNodeHandler = new DemoReparentNodeHandler();

      // Just for user convenience: disable node and edge creation,
      graphEditorInputMode.AllowCreateEdge = false;
      graphEditorInputMode.AllowCreateNode = false;
      // disable deleting items
      graphEditorInputMode.DeletableItems = GraphItemTypes.None;
      // disable the grouping command (Ctrl+G)
      graphEditorInputMode.AllowGroupSelection = false;
      // and enable the undo feature.
      graph.SetUndoEngineEnabled(true);

      // Finally, set the input mode to the graph control.
      graphControl.InputMode = graphEditorInputMode;

      CreateSampleGraph(graph);
      // reset the Undo queue so the initial graph creation cannot be undone
      graph.GetUndoEngine().Clear();
    }

    #endregion

    /// <summary>
    /// Customized variant of the default <see cref="ReparentNodeHandler"/> that
    /// determines the possible reparenting operations based on the node's tag.
    /// </summary>
    public class DemoReparentNodeHandler : ReparentNodeHandler
    {
      /// <summary>
      /// In general, this method determines whether the current gesture that
      /// can be determined through the context is a reparent gesture. In this
      /// case, it returns true, if the base implementation returns true or if the
      /// current node is green.
      /// </summary>
      /// <param name="context">The context that provides information about the
      /// user input.</param>
      /// <param name="node">The node that will possibly be reparented.</param>
      /// <returns>Whether this is a reparenting gesture.</returns>
      public override bool IsReparentGesture(IInputModeContext context, INode node) {
        return base.IsReparentGesture(context, node) || Color.Green.Equals(node.Tag);
      }

      /// <summary>
      /// In general, this method determines whether the user may detach the
      /// given node from its current parent in order to reparent it. In this case,
      /// it returns false for red nodes.
      /// </summary>
      /// <param name="context">The context that provides information about the
      /// user input.</param>
      /// <param name="node">The node that is about to be detached from its
      /// current parent.</param>
      /// <returns>Whether the node may be detached and reparented.</returns>
      public override bool ShouldReparent(IInputModeContext context, INode node) {
        return !Color.Firebrick.Equals(node.Tag);
      }

      /// <summary>
      /// In general, this method determines whether the provided node
      /// may be reparented to the given <paramref name="newParent">new parent
      /// </paramref>.
      /// </summary>
      /// <param name="context">The context that provides information about the
      /// user input.</param>
      /// <param name="node">The node that will be reparented.</param>
      /// <param name="newParent">The potential new parent.</param>
      /// <returns>Whether <paramref name="newParent"/> is a valid new parent
      /// for <paramref name="node"/>.</returns>
      public override bool IsValidParent(IInputModeContext context, INode node, INode newParent) {
        // Obtain the tag from the designated child
        object nodeTag = node.Tag;
        // and from the designated parent.
        object parentTag = newParent == null ? null : newParent.Tag;
        if (nodeTag == null) {
          // Newly created nodes or nodes without a tag in general can be reparented freely
          return true;
        }
        // Otherwise allow nodes to be moved only if their tags are the same color
        if (nodeTag is Color && parentTag is Color) {
          return nodeTag.Equals(parentTag);
        }
        // Finally, if there is no new parent, this is ok, too
        return newParent == null;
      }
    }

    #region Sample Graph Creation

    /// <summary>
    /// Creates the sample graph of this demo.
    /// </summary>
    private static void CreateSampleGraph(IGraph graph) {
      // Create some group nodes
      var group1 = CreateGroupNode(graph, 100, 100, Color.RoyalBlue, "Only Blue Children");
      var group2 = CreateGroupNode(graph, 160, 130, Color.RoyalBlue, "Only Blue Children");
      var greenGroup = CreateGroupNode(graph, 100, 350, Color.Green, "Only Green Children");
      CreateGroupNode(graph, 400, 350, Color.Green, "Only Green Children");

      // And some regular nodes
      INode blueNode = graph.CreateNode(new RectD(110, 130, 30, 30), new ShinyPlateNodeStyle {Brush = Brushes.RoyalBlue}, Color.RoyalBlue);
      INode greenNode = graph.CreateNode(new RectD(130, 380, 30, 30), new ShinyPlateNodeStyle {Brush = Brushes.Green}, Color.Green);
      graph.CreateNode(new RectD(400, 100, 30, 30), new ShinyPlateNodeStyle {Brush = Brushes.Firebrick}, Color.Firebrick);
      graph.CreateNode(new RectD(500, 100, 30, 30), new ShinyPlateNodeStyle {Brush = Brushes.Green}, Color.Green);
      graph.CreateNode(new RectD(400, 200, 30, 30), new ShinyPlateNodeStyle {Brush = Brushes.RoyalBlue}, Color.RoyalBlue);
      graph.CreateNode(new RectD(500, 200, 30, 30), new ShinyPlateNodeStyle {Brush = Brushes.Firebrick}, Color.Firebrick);

      // Add some initial children to the groups
      graph.GroupNodes(group1, new[] { blueNode, group2 });
      graph.GroupNodes(greenGroup, new[] {greenNode});

      // Ensure that the outer blue group completely contains its inner group 
      graph.SetNodeLayout(group1, new RectD(100, 100, 200, 150));

      // Uncomment the following line to adjust the layout of the outer blue group automatically
//       graph.AdjustGroupNodeLayout(group1);
    }

    /// <summary>
    /// Creates a group node for the sample graph with a specific styling.
    /// </summary>
    private static INode CreateGroupNode(IGraph graph, double x, double y, Color fillColor, string labelText) {
      var groupNode = graph.CreateGroupNode();
      graph.SetStyle(groupNode, new PanelNodeStyle {Color = fillColor, LabelInsetsColor = fillColor, Insets = new InsetsD(5, 20, 5, 5)});
      graph.SetNodeLayout(groupNode, new RectD(x, y, 130, 100));

      // The label style and placement
      var labelStyle = new DefaultLabelStyle {
        BackgroundBrush = new SolidBrush(fillColor),
        TextBrush = Brushes.White
      };
      var labelModel = new InteriorStretchLabelModel();
      labelModel.Insets = new InsetsD(5, 2, 5, 4);
      var modelParameter = labelModel.CreateParameter(InteriorStretchLabelModel.Position.North);
      graph.AddLabel(groupNode, labelText, modelParameter, labelStyle);

      groupNode.Tag = fillColor;
      return groupNode;
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
      Application.Run(new ReparentHandlerForm());
    }

    #endregion

  }
}
