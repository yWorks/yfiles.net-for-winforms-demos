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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Demo.yFiles.Graph.Viewer.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.GraphML;

namespace Demo.yFiles.Graph.Viewer
{
  /// <summary>
  /// This demo shows how to display a graph with the GraphViewer component.
  /// </summary>
  public partial class GraphViewer : Form
  {

    private FoldingManager manager; 

    public GraphViewer() {
      InitializeComponent();
      EnableFolding();
      graphOverviewControl.GraphControl = graphControl;

      graphControl.FileOperationsEnabled = true;
      openButton.SetCommand(Commands.Open, graphControl);
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      descriptionTextBox.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    protected override void OnLoad(EventArgs e) {
      IGraph graph = graphControl.Graph;

      IMapperRegistry masterRegistry = graph.GetFoldingView().Manager.MasterGraph.MapperRegistry;
      masterRegistry.CreateWeakMapper<INode, string>("ToolTip");
      masterRegistry.CreateWeakMapper<INode, string>("Description");
      masterRegistry.CreateWeakMapper<INode, string>("Url");
      masterRegistry.CreateWeakMapper<IGraph, string>("GraphDescription");

      graphControl.CurrentItemChanged += OnCurrentItemChanged;
      
      InitializeHighlightStyles();

      InitializeInputMode();
      
      graphChooserBox.Items.AddRange(new[] { "computer-network", "movies", "family-tree", "hierarchy", "nesting", "social-network", "uml-diagram", "large-tree", });
      graphChooserBox.SelectedIndex = 0;

      graphControl.FitGraphBounds();
    }
    
    private void InitializeHighlightStyles() {
      // we want to create a non-default nice highlight styling
      // for the hover highlight, create semi transparent orange stroke first
      var orangePen = new Pen(Color.OrangeRed, 3);

      // now decorate the nodes and edges with custom hover highlight styles
      var decorator = graphControl.Graph.GetDecorator();

      // nodes should be given a rectangular orange rectangle highlight shape
      var highlightShape = new ShapeNodeStyle {
          Shape = ShapeNodeShape.RoundRectangle,
          Pen = orangePen,
          Brush = null
      };

      var nodeStyleHighlight = new NodeStyleDecorationInstaller {
          NodeStyle = highlightShape,
          // that should be slightly larger than the real node
          Margins = new InsetsD(5),
          // but have a fixed size in the view coordinates
          ZoomPolicy = StyleDecorationZoomPolicy.ViewCoordinates
      };

      // register it as the default implementation for all nodes
      decorator.NodeDecorator.HighlightDecorator.SetImplementation(nodeStyleHighlight);

      // a similar style for the edges, however cropped by the highlight's insets
      var dummyCroppingArrow = new Arrow {
          Type = ArrowType.None,
          CropLength = 5
      };
      var edgeStyle = new PolylineEdgeStyle {
          Pen = orangePen,
          TargetArrow = dummyCroppingArrow,
          SourceArrow = dummyCroppingArrow
      };
      var edgeStyleHighlight = new EdgeStyleDecorationInstaller {
          EdgeStyle = edgeStyle,
          ZoomPolicy = StyleDecorationZoomPolicy.ViewCoordinates
      };
      decorator.EdgeDecorator.HighlightDecorator.SetImplementation(edgeStyleHighlight);
    }

    private void InitializeInputMode() {
      // we have a viewer application, so we can use the GraphViewerInputMode
      // -enable support for: tooltips on nodes and edges
      // -clicking on nodes
      // -focusing (via keyboard navigation) of nodes
      // -no selection
      // -no marquee
      var graphViewerInputMode = new GraphViewerInputMode
      {
          ToolTipItems = GraphItemTypes.LabelOwner,
          ClickableItems = GraphItemTypes.Node,
          FocusableItems = GraphItemTypes.Node,
          SelectableItems = GraphItemTypes.None,
          MarqueeSelectableItems = GraphItemTypes.None
      };

      // we want to enable the user to collapse and expand groups interactively, even though we
      // are just a "viewer" application
      graphViewerInputMode.NavigationInputMode.AllowCollapseGroup = true;
      graphViewerInputMode.NavigationInputMode.AllowExpandGroup = true;
      // after expand/collapse/enter/exit operations - perform a fitContent operation to adjust
      // reachable area.
      graphViewerInputMode.NavigationInputMode.FitContentAfterGroupActions = true;
      // we don't have selection enabled and thus the commands should use the "currentItem"
      // property instead - this property is changed when clicking on items or navigating via
      // the keyboard.
      graphViewerInputMode.NavigationInputMode.UseCurrentItemForCommands = true;

      // we want to get reports of the mouse being hovered over nodes and edges
      // first enable queries
      graphViewerInputMode.ItemHoverInputMode.Enabled = true;
      // set the items to be reported
      graphViewerInputMode.ItemHoverInputMode.HoverItems = GraphItemTypes.Edge | GraphItemTypes.Node;
      // if there are other items (most importantly labels) in front of edges or nodes
      // they should be discarded, rather than be reported as "null"
      graphViewerInputMode.ItemHoverInputMode.DiscardInvalidItems = false;
      // whenever the currently hovered item changes call our method
      graphViewerInputMode.ItemHoverInputMode.HoveredItemChanged += OnHoveredItemChanged;

      // when the mouse hovers for a longer time over an item we may optionally display a
      // tooltip. Use this callback for querying the tooltip contents.
      graphViewerInputMode.QueryItemToolTip += OnQueryItemToolTip;

      // if we click on an item we want to perform a custom action, so register a callback
      graphViewerInputMode.ItemClicked += OnItemClicked;

      // also if someone clicked on an empty area we want to perform a custom group action
      graphViewerInputMode.ClickInputMode.Clicked += OnClickInputModeOnClicked;

      graphControl.InputMode = graphViewerInputMode;
    }

    private void OnHoveredItemChanged(object sender, HoveredItemChangedEventArgs e) {
      // we use the highlight manager of the GraphComponent to highlight related items
      var manager = graphControl.HighlightIndicatorManager;

      // first remove previous highlights
      manager.ClearHighlights();
      // then see where we are hovering over, now
      var newItem = e.Item;
      if (newItem != null) {
        // we highlight the item itself
        manager.AddHighlight(newItem);
        var node = newItem as INode;
        var edge = newItem as IEdge;
        if (node != null) {
          // and if it's a node, we highlight all adjacent edges, too
          foreach (var adjacentEdge in graphControl.Graph.EdgesAt(node)) {
            manager.AddHighlight(adjacentEdge);
          }
        } else if (edge != null) {
          // if it's an edge - we highlight the adjacent nodes
          manager.AddHighlight(edge.GetSourceNode());
          manager.AddHighlight(edge.GetTargetNode());
        }
      }
    }

    private void OnClickInputModeOnClicked(object sender, ClickEventArgs args) {
      if (!graphControl.GraphModelManager.HitElementsAt(args.Location).Any()) { // nothing hit
        if ((args.Modifiers & (Keys.Shift | Keys.Control)) == (Keys.Shift | Keys.Control)) {
          if (Commands.ExitGroup.CanExecute(null, graphControl) && !args.Handled) {
            Commands.ExitGroup.Execute(null, graphControl);
            args.Handled = true;
          }
        }
      }
    }

    /// <summary>
    /// Enable folding - change the GraphControls graph to a managed view
    /// that provides the actual collapse/expand state.
    /// </summary>
    private void EnableFolding() {
      // create the manager
      manager = new FoldingManager();
      // replace the displayed graph with a managed view
      graphControl.Graph = manager.CreateFoldingView().Graph;
      WrapGroupNodeStyles();
    }

    /// <summary>
    /// Change the default style for group nodes.
    /// </summary>
    /// <remarks>We use <see cref="CollapsibleNodeStyleDecorator"/> to wrap the
    /// <see cref="PanelNodeStyle"/> from the last demo, since we want to have nice
    /// +/- buttons for collapse/expand. Note that if you haven't defined
    /// a custom group node style, you don't have to do anything at all, since
    /// <see cref="FoldingManager"/> already
    /// provides such a decorated group node style by default.</remarks>
    private void WrapGroupNodeStyles() {
      //PanelNodeStyle is a nice style especially suited for group nodes
      PanelNodeStyle style = new PanelNodeStyle {Color = Color.LightBlue};

      //Wrap the style with CollapsibleNodeStyleDecorator
      graphControl.Graph.GroupNodeDefaults.Style = new CollapsibleNodeStyleDecorator(style);
    }

    private void OnCurrentItemChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
      var currentItem = graphControl.CurrentItem;
      if (currentItem is INode) {
        var node = (INode)currentItem;
        nodeDescriptionTextBlock.Text = DescriptionMapper[node] ?? string.Empty;
        nodeLabelTextBlock.Text = node.Labels.Count > 0 ? Regex.Replace(node.Labels[0].Text, "\r?\n", "\r\n") : string.Empty;
        var url = UrlMapper[node];
        if (url != null) {
          nodeUrlButton.Text = url;
          nodeUrlButton.Links.Clear();
          nodeUrlButton.Links.Add(0, url.Length, url);
          nodeUrlButton.Enabled = true;
        } else {
          nodeUrlButton.Text = "-";
          nodeUrlButton.Enabled = false;
        }
      }
      else {
        nodeDescriptionTextBlock.Text = "";
        nodeLabelTextBlock.Text = "";
        nodeUrlButton.Text = null;
        nodeUrlButton.Tag = null;
        nodeUrlButton.Enabled = false;
      }
    }

    private void OnItemClicked(object sender, ItemClickedEventArgs<IModelItem> e) {
      if (e.Item is INode) {
        graphControl.CurrentItem = e.Item;
        if ((ModifierKeys & (Keys.Shift | Keys.Control)) == (Keys.Shift | Keys.Control)) {
          if (Commands.EnterGroup.CanExecute(e.Item, graphControl)) {
            Commands.EnterGroup.Execute(e.Item, graphControl);
            e.Handled = true;
          }
        } else if ((ModifierKeys & Keys.Shift) == Keys.Shift) {
          var url = UrlMapper[(INode)e.Item];
          var descriptionWindow = new DescriptionForm();
          if (url != null) {
            descriptionWindow.Uri = url;
            descriptionWindow.Description = DescriptionMapper[(INode)e.Item];
            descriptionWindow.ShowDialog(this);
            e.Handled = true;
          }
        }
      }
    }

    private void OnQueryItemToolTip(object sender, QueryItemToolTipEventArgs<IModelItem> queryItemToolTipEventArgs) {
      if (queryItemToolTipEventArgs.Item is INode && !queryItemToolTipEventArgs.Handled) {
        INode node = (INode)queryItemToolTipEventArgs.Item;
        IMapper<INode, string> descriptionMapper = DescriptionMapper;
        var toolTip = ToolTipMapper[node] ?? (descriptionMapper != null ? descriptionMapper[node] : null);
        if (toolTip != null) {
          queryItemToolTipEventArgs.ToolTip = toolTip;
          queryItemToolTipEventArgs.Handled = true;
        }
      }
    }

    private IMapper<INode, string> DescriptionMapper {
      get { return graphControl.Graph.MapperRegistry.GetMapper<INode, string>("Description"); }
    }
    private IMapper<INode, string> ToolTipMapper {
      get { return graphControl.Graph.MapperRegistry.GetMapper<INode, string>("ToolTip"); }
    }
    private IMapper<INode, string> UrlMapper {
      get { return graphControl.Graph.MapperRegistry.GetMapper<INode, string>("Url"); }
    }


    private void ReadSampleGraph() {
      string fileName = string.Format("Resources" + Path.DirectorySeparatorChar + "{0}.graphml", graphChooserBox.SelectedItem.ToString());
      DictionaryMapper<IGraph, string> descriptionMapper = new DictionaryMapper<IGraph, string>();
      graphControl.Graph.Clear();
      var ioHandler = new GraphMLIOHandler();
      ioHandler.AddRegistryInputMapper<INode, string>("Description");
      ioHandler.AddRegistryInputMapper<INode, string>("ToolTip");
      ioHandler.AddRegistryInputMapper<INode, string>("Url");
      ioHandler.AddInputMapper<IGraph, string>("GraphDescription", descriptionMapper);
      ioHandler.Read(graphControl.Graph, fileName);
      graphDescriptionTextBlock.Text = descriptionMapper[graphControl.Graph.GetFoldingView().Manager.MasterGraph] ?? string.Empty;
      graphControl.FitGraphBounds(new InsetsD(10));
    }

    private void UpdateButtons() {
      nextButton.Enabled = graphChooserBox.SelectedIndex < graphChooserBox.Items.Count - 1;
      previousButton.Enabled = graphChooserBox.SelectedIndex > 0;
    }

    private void previousButton_Click(object sender, EventArgs e) {
      graphChooserBox.SelectedIndex--;
      UpdateButtons();
    }

    private void nextButton_Click(object sender, EventArgs e) {
      graphChooserBox.SelectedIndex++;
      UpdateButtons();
    }

    private void graphChooserBox_SelectedIndexChanged(object sender, EventArgs e) {
      ReadSampleGraph();
      UpdateButtons();
    }

    private void nodeUrlButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      // Open the link...
      Process.Start(new ProcessStartInfo {FileName = e.Link.LinkData.ToString(), UseShellExecute = true});
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new GraphViewer());
    }
  }
}