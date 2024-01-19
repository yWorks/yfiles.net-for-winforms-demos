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
using Demo.yFiles.Graph.GroupNodes.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Utils;

namespace Demo.yFiles.Graph.GroupNodes
{
  /// <summary>
  /// Shows the group and folder node visualization options offered by the
  /// <see cref="GroupNodeStyle"/> class.
  /// </summary>
  public partial class GroupNodeStyleDemo : Form
  {
    public GroupNodeStyleDemo() {
      InitializeComponent();
      InitializeFolding();
      InitializeInputMode();
      InitializeDefaultStyles();
      CreateSampleGraph();
      InitializeGridVisual();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    /// <summary>
    /// Initializes the visualization of the grid feature.
    /// </summary>
    private void InitializeGridVisual() {
      var grid = new GridVisualCreator(new GridInfo())
      {
          GridStyle = GridStyle.Lines,
          
          Pen = Pens.LightGray,
          VisibilityThreshold = 10,
      };
      graphControl.BackgroundGroup.AddChild(grid, CanvasObjectDescriptors.AlwaysDirtyInstance);
    }

    /// <summary>
    /// Enables folding and specifies that the label of the group node should
    /// also appear on the folder node.
    /// </summary>
    private void InitializeFolding() {
      var foldingManager = new FoldingManager(graphControl.Graph)
      {
        FolderNodeConverter = new DefaultFolderNodeConverter { CopyFirstLabel = true }
      };
      graphControl.Graph = foldingManager.CreateFoldingView().Graph;
    }

    /// <summary>
    /// Restricts user interaction to selecting, panning, and zooming.
    /// </summary>
    private void InitializeInputMode() {
      graphControl.FileOperationsEnabled = true;

      var geim = new GraphEditorInputMode
      {
        AllowCreateNode = false,
        AllowGroupingOperations = true,
        DeletableItems = GraphItemTypes.All & ~GraphItemTypes.Node,
        NavigationInputMode = { AutoGroupNodeAlignmentPolicy = NodeAlignmentPolicy.Center }
      };

      // provide a way to collapse group nodes or expand folder nodes even if their style does not
      // show an icon for collapsing or expanding
      geim.ItemLeftDoubleClicked += ((sender, args) => {
        if (args.Item is INode node) {
          if (Commands.ToggleExpansionState.CanExecute(node, graphControl)) {
            Commands.ToggleExpansionState.Execute(node, graphControl);
            graphControl.Selection.SetSelected(node, false);
            args.Handled = true;
          }
        }
      });

      graphControl.InputMode = geim;
    }

    /// <summary>
    /// Configures the default styles for new nodes, edges, and labels in the given graph.
    /// </summary>
    private void InitializeDefaultStyles() {
      DemoStyles.InitDemoStyles(
        graphControl.Graph,
        nodeTheme: Themes.Palette58,
        groupTheme: Themes.Palette58,
        edgeTheme: Themes.Palette58);
    }

    /// <summary>
    /// Creates a sample graph with several group and folder nodes.
    /// </summary>
    private void CreateSampleGraph() {
      var graph = graphControl.Graph;
      graph.Clear();

      var red = Themes.Palette59;
      var green = Themes.Palette53;
      var blue = Themes.Palette56;
      var orange = Themes.Palette51;

      // create a couple of GroupNodeStyle instances that demonstrate various tab configuration options
      // for tabs that are placed at the top of the respective node ...
      GroupNodeStyle[] stylesWithTabAtTop = {
          // style for red nodes
          new GroupNodeStyle {
            FolderIcon = GroupNodeStyleIconType.None,
            TabBrush = red.Fill,
            TabBackgroundBrush = red.Fill,
            // tab width 0 together with a leading or trailing tab position prevents corner rounding for
            // the "inner" corners of the tab stroke and the content area
            TabPosition = GroupNodeStyleTabPosition.TopLeading,
            TabWidth = 0.0,
            TabHeight = 24.0,
            Pen = new Pen(red.Stroke, 1)
          },
          // style for green nodes
          new GroupNodeStyle {
              CornerRadius = 0,
              GroupIcon = GroupNodeStyleIconType.TriangleDown,
              FolderIcon = GroupNodeStyleIconType.TriangleUp,
              IconPosition = GroupNodeStyleIconPosition.Leading,
              IconBackgroundShape = GroupNodeStyleIconBackgroundShape.Square,
              IconForegroundBrush = Brushes.White,
              TabBrush = green.Fill,
              TabPosition = GroupNodeStyleTabPosition.TopLeading,
              TabSlope = 0,
              Pen = new Pen(green.Stroke, 1)
          },
          // style for blue nodes
          new GroupNodeStyle {
              DrawShadow = true,
              GroupIcon = GroupNodeStyleIconType.ChevronDown,
              FolderIcon = GroupNodeStyleIconType.ChevronUp,
              IconForegroundBrush = blue.Stroke,
              IconPosition = GroupNodeStyleIconPosition.Trailing,
              TabPosition = GroupNodeStyleTabPosition.TopLeading,
              TabBrush = blue.Fill,
              TabBackgroundBrush = blue.Stroke,
              TabHeight = 22,
              TabSlope = 0.5,
              Pen = new Pen(blue.Stroke, 1)
          },
          // style for orange nodes
          new GroupNodeStyle {
              CornerRadius = 8,
              ContentAreaBrush = orange.EdgeLabelFill,
              DrawShadow = true,
              GroupIcon = GroupNodeStyleIconType.Minus,
              IconBackgroundBrush = orange.NodeLabelFill,
              IconForegroundBrush = orange.Stroke,
              IconBackgroundShape = GroupNodeStyleIconBackgroundShape.CircleSolid,
              TabBrush = orange.Fill,
              TabHeight = 22,
              TabInset = 8.0,
              Pen = new Pen(orange.Stroke, 1)
          }
      };

      var gold = Themes.Palette510;
      var gray = Themes.Palette58;
      var lightGreen = Themes.Palette54;
      var purple = Themes.Palette55;

      // ... and for tabs at different sides of the respective nodes
      GroupNodeStyle[] stylesWithTabAtMiscPositions = {
          // style for gold nodes
          new GroupNodeStyle {
              GroupIcon = GroupNodeStyleIconType.Minus, IconForegroundBrush = gold.Fill, TabBrush = gold.Fill, 
              ContentAreaBrush = Brushes.Transparent, RenderTransparentContentArea = true
          },
          // style for gray nodes
          new GroupNodeStyle {
              CornerRadius = 0,
              GroupIcon = GroupNodeStyleIconType.TriangleLeft,
              FolderIcon = GroupNodeStyleIconType.TriangleRight,
              IconPosition = GroupNodeStyleIconPosition.Leading,
              IconBackgroundShape = GroupNodeStyleIconBackgroundShape.Square,
              IconForegroundBrush = Brushes.White,
              TabBrush = gray.Fill,
              TabSlope = 0,
              TabPosition = GroupNodeStyleTabPosition.RightLeading,
              Pen = new Pen(gray.Stroke, 1)
          },
          // style for light-green nodes
          new GroupNodeStyle {
              DrawShadow = true,
              GroupIcon = GroupNodeStyleIconType.ChevronUp,
              FolderIcon = GroupNodeStyleIconType.ChevronDown,
              IconForegroundBrush = blue.Stroke,
              IconPosition = GroupNodeStyleIconPosition.Leading,
              TabPosition = GroupNodeStyleTabPosition.BottomTrailing,
              TabBrush = lightGreen.Fill,
              TabBackgroundBrush = green.Stroke,
              TabHeight = 22,
              TabSlope = 0.5,
              Pen = new Pen(lightGreen.Stroke, 1)
          },
          // style for purple nodes
          new GroupNodeStyle {
              CornerRadius = 8,
              ContentAreaBrush = purple.NodeLabelFill,
              DrawShadow = true,
              GroupIcon = GroupNodeStyleIconType.Minus,
              IconPosition = GroupNodeStyleIconPosition.Leading,
              IconBackgroundBrush = purple.NodeLabelFill,
              IconForegroundBrush = purple.Stroke,
              IconBackgroundShape = GroupNodeStyleIconBackgroundShape.CircleSolid,
              TabPosition = GroupNodeStyleTabPosition.Left,
              TabBrush = purple.Fill,
              TabHeight = 22,
              TabInset = 8.0,
              Pen = new Pen(purple.Stroke, 1)
          }
      };

      // create label styles that use the same color sets as the GroupNodeStyle instances created above
      DefaultLabelStyle[] labelStyles = {
          // style for red nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = red.NodeLabelFill
          },
          // style for green nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = green.Text
          },
          // style for blue nodes
          // this style uses centered horizontal text because of the sloped tab in the blue nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      Alignment = StringAlignment.Center,
                      LineAlignment = StringAlignment.Center,
                      Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = blue.NodeLabelFill
          },
          // style for orange nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = orange.NodeLabelFill
          }
      };

      DefaultLabelStyle[] labelStylesWithTabAtMiscPositions = {
          // style for gold nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = gold.NodeLabelFill
          },
          // style for gray nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false
          },
          // style for light-green nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = green.NodeLabelFill
          },
          // style for purple nodes
          new DefaultLabelStyle {
              StringFormat =
                  new StringFormat {
                      LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter
                  },
              ClipText = false,
              TextBrush = purple.NodeLabelFill
          }
      };

      string[] labelTexts = { "Red", "Green", "Blue", "Orange" };
      string[] labelTextsWithTabAtMiscPositions = { "Gold", "Gray", "Light green", "Purple" };

      // create one group node and one folder node for each of the above GroupNodeStyle instances
      CreateGroupAndFolderNodes(graph, stylesWithTabAtTop, labelStyles, labelTexts, 0, 0);
      CreateGroupAndFolderNodes(graph, stylesWithTabAtMiscPositions, labelStylesWithTabAtMiscPositions,
          labelTextsWithTabAtMiscPositions, 0, 425);

      // create a couple of child nodes for group nodes ...
      var nodes = graph.Nodes.ToArray();
      var p1c1 = CreateChildNode(graph, nodes[1], 20, 52);
      var p1c2 = CreateChildNode(graph, nodes[1], 80, 32);
      var p1c3 = CreateChildNode(graph, nodes[1], 60, 102);
      var p1c4 = CreateChildNode(graph, nodes[1], 140, 102);
      var p2c1 = CreateChildNode(graph, nodes[2], 43, 42);
      var p2c2 = CreateChildNode(graph, nodes[2], 133, 78);
      CreateChildNode(graph, nodes[8], 33, 33);
      var p8c2 = CreateChildNode(graph, nodes[8], 68, 103);
      var p8c3 = CreateChildNode(graph, nodes[8], 103, 33);
      var p9c1 = CreateChildNode(graph, nodes[9], 10, 10);
      var p9c2 = CreateChildNode(graph, nodes[9], 58, 42);
      var p9c3 = CreateChildNode(graph, nodes[9], 96, 94);
      CreateChildNode(graph, nodes[10], 43, 14);
      var pBc1 = CreateChildNode(graph, nodes[11], 34, 34);
      var pBc2 = CreateChildNode(graph, nodes[11], 128, 74);
      var pBc3 = CreateChildNode(graph, nodes[11], 138, 28);
      var pBc4 = CreateChildNode(graph, nodes[11], 50, 88);

      graph.CreateEdge(p1c1, p1c3);
      graph.CreateEdge(p1c3, p1c2);
      graph.CreateEdge(p1c3, p1c4);
      graph.CreateEdge(p2c2, p2c1);
      graph.CreateEdge(p8c2, p8c3);
      graph.CreateEdge(p9c1, p9c2);
      graph.CreateEdge(p9c2, p9c3);
      graph.CreateEdge(pBc1, pBc2);
      graph.CreateEdge(pBc3, pBc4);

      // ... and folder nodes
      CreateChildNode(graph, nodes[4], 68, 46);
      CreateChildNode(graph, nodes[4], 147, 82);
      CreateChildNode(graph, nodes[7], 55, 100);
      CreateChildNode(graph, nodes[12], 8, 26);
      CreateChildNode(graph, nodes[12], 87, 62);
      CreateChildNode(graph, nodes[13], 29, 85);
      CreateChildNode(graph, nodes[13], 59, 55);
      CreateChildNode(graph, nodes[13], 89, 25);
      CreateChildNode(graph, nodes[14], 8, 15);
      CreateChildNode(graph, nodes[14], 58, 15);
      CreateChildNode(graph, nodes[14], 108, 15);
      CreateChildNode(graph, nodes[14], 58, 55);
      CreateChildNode(graph, nodes[14], 108, 55);
      CreateChildNode(graph, nodes[14], 158, 55);
      CreateChildNode(graph, nodes[15], 55, 25);
      CreateChildNode(graph, nodes[15], 133, 25);
      CreateChildNode(graph, nodes[15], 55, 95);
      CreateChildNode(graph, nodes[15], 133, 95);
    }

    /// <summary>
    /// Creates a group node and a folder node for each of the given style instances.
    /// Additionally, this method will add one label to each created group or folder node.
    /// </summary>
    /// <param name="graph">The graph in which to create the new group and folder nodes.</param>
    /// <param name="nodeStyles">The style instances for which to create new group and folder nodes.</param>
    /// <param name="labelStyles">The style instances for the labels of the new group and folder nodes.</param>
    /// <param name="labelTexts">The texts for the labels of the new group and folder nodes.</param>
    /// <param name="x0">The top-left x-coordinate of the first node to create.</param>
    /// <param name="y0">The top-left x-coordinate of the first node to create.</param>
    static void CreateGroupAndFolderNodes(
        IGraph graph,
        GroupNodeStyle[] nodeStyles,
        DefaultLabelStyle[] labelStyles,
        string[] labelTexts,
        int x0,
        int y0
    ) {
      // place the labels of the group and folder nodes into the tab background of their visualizations
      // GroupNodeLabelModel's default parameter can be used to place labels into the tab area instead
      var tabBackgroundParameter = new GroupNodeLabelModel().CreateTabBackgroundParameter();

      var y = y0;
      var width = 200;
      var height = 150;
      for (var j = 0; j < 2; ++j) {
        var x = x0;
        var n = nodeStyles.Length;
        for (var i = 0; i < n; ++i) {
          var node = graph.CreateGroupNode(null, new RectD(x, y, width, height), nodeStyles[i]);
          graph.AddLabel(node, labelTexts[i] + (j + 1), tabBackgroundParameter, labelStyles[i]);
          if (j > 0) {
            CollapseLast(graph);
          }
          x += width + 100;
        }
        y += height + 25;
      }
    }

    /// <summary>
    /// Creates a child node for the given parent group node.
    /// The created node will be neither a group node nor a folder node.
    /// </summary>
    /// <param name="graph">The graph in which to create the new node.</param>
    /// <param name="parent">The parent node for the new node.</param>
    /// <param name="xOffset">The distance in x-direction from the new node's top left corner to the parent node's top left corner.</param>
    /// <param name="yOffset">The distance in y-direction from the new node's top left corner to the parent node's top left corner.</param>
    static INode CreateChildNode(IGraph graph, INode parent, int xOffset, int yOffset) {
      var nl = parent.Layout;
      var node = graph.CreateNode(new RectD(nl.X + xOffset, nl.Y + yOffset, 30, 30));
      graph.SetParent(node, parent);
      return node;
    }

    /// <summary>
    /// Collapses the last group node in the given graph.
    /// </summary>
    static void CollapseLast(IGraph graph) {
      graph.GetFoldingView().Collapse(graph.Nodes.Last());
    }

    /// <summary>
    /// Centers the sample graph in the visible area.
    /// </summary>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      graphControl.FitGraphBounds();
    }

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new GroupNodeStyleDemo());
    }

    #endregion
  }
}
