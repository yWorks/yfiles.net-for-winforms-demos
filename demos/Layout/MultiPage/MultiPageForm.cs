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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Demo.yFiles.Layout.MultiPage.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Algorithms;
using yWorks.Algorithms.Geometry;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.GraphML;
using yWorks.Layout;
using yWorks.Layout.Circular;
using yWorks.Layout.Hierarchic;
using yWorks.Layout.Multipage;
using yWorks.Layout.Organic;
using yWorks.Layout.Orthogonal;

namespace Demo.yFiles.Layout.MultiPage
{
  /// <summary>
  /// This demo application demonstrates how the result of a multi-page layout calculation
  ///	can be displayed in a graph control.
  /// </summary>
  /// <remarks>
  /// A multi-page layout splits a large graph into multiple pages with a fixed maximum width and height.
  /// Each of these pages is displayed by a different graph. 
  /// <see cref="MultiPageIGraphBuilder"/> is an adapter that helps to build the graphs, that can be displayed 
  /// in a <see cref="GraphControl"/>, from the layout graphs that are calculated by the multi-page layout.
  /// </remarks>
  public partial class MultiPageForm : Form
  {
    // list of available core layouts
    private Dictionary<string, ILayoutAlgorithm> coreLayouts;
    // the current core layout
    private ILayoutAlgorithm coreLayout = new HierarchicLayout();
    // the original graph that is the source for the multi-page layout
    private IGraph modelGraph;
    // the calculated page graphs
    private IGraph[] viewGraphs;
    // the currently selected page
    private int pageNumber;
    // visual creator for rendering the page bounds
    private PageBoundsVisualCreator pageBoundsVisualCreator;

    public MultiPageForm() {
      InitializeComponent();

      RegisterToolStripCommands();

      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    /// <summary>
    /// Called when the application has been loaded
    /// </summary>
    private void OnLoad(object sender, EventArgs e) {
      // show a notification because the multi-page layout takes some time
      ShowLoadingIndicator(true);
      InitializeCoreLayouts();
      InitializeInputMode();
      // load the original graph
      modelGraph = new DefaultGraph();
      // set a default node label style similar to those used for the visualization later,
      // so the node labels read from the GraphML file are assigned a correct size that considers the
      // paddings of the label style
      modelGraph.NodeDefaults.Labels.Style = DemoStyles.CreateDemoNodeLabelStyle(Themes.Palette21);
      GraphMLIOHandler ioHandler = new GraphMLIOHandler();
      ioHandler.Read(modelGraph, "Resources/pop-artists-small.graphml");
      // add the page bounds visual
      pageBoundsVisualCreator = new PageBoundsVisualCreator();
      graphControl.BackgroundGroup.AddChild(pageBoundsVisualCreator);
      // calculate the multi-page layout
      RunMultipageLayout();
    }

    private void InitializeInputMode() {
      // create the inputmode and disable selection and focus
      GraphViewerInputMode mode = new GraphViewerInputMode {
        ClickableItems = GraphItemTypes.Node,
        SelectableItems = GraphItemTypes.None,
        FocusableItems = GraphItemTypes.None,
      };
      // handle clicks on nodes
      mode.ItemClicked += (sender, e) => GotoReferencingNode((INode)e.Item);
	    // register the command bindings for the custom commands
	    mode.KeyboardInputMode.AddCommandBinding(PreviousPageCommand, PreviousPage_Executed, PreviousPage_CanExecute);
	    mode.KeyboardInputMode.AddCommandBinding(NextPageCommand, NextPage_Executed, NextPage_CanExecute);

      graphControl.InputMode = mode;
      originalGraphControl.InputMode = new GraphViewerInputMode()
      {ClickableItems = GraphItemTypes.None, SelectableItems = GraphItemTypes.None};
    }

    /// <summary>
    /// Creates the core layouts and populates the layouts box
    /// </summary>
    private void InitializeCoreLayouts() {
      coreLayouts = new Dictionary<string, ILayoutAlgorithm>();
      coreLayouts["Hierarchic"] = new HierarchicLayout { ConsiderNodeLabels = true, IntegratedEdgeLabeling = true, OrthogonalRouting = true };
      coreLayouts["Circular"] = new CircularLayout();
      coreLayouts["Compact Orthogonal"] = new CompactOrthogonalLayout();
      coreLayouts["Organic"] = new OrganicLayout { MinimumNodeDistance = 10, Deterministic = true };
      coreLayouts["Orthogonal"] = new OrthogonalLayout();
      string[] layouts = new string[coreLayouts.Keys.Count];
      coreLayouts.Keys.CopyTo(layouts, 0);
      coreLayoutComboBox.Items.AddRange(layouts);
      coreLayoutComboBox.SelectedIndex = 0;
    }

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      zoomInButtonOriginal.SetCommand(Commands.IncreaseZoom, originalGraphControl);
      zoomOutButtonOriginal.SetCommand(Commands.DecreaseZoom, originalGraphControl);
      fitContentButtonOriginal.SetCommand(Commands.FitContent, originalGraphControl);

      prevPageButton.SetCommand(PreviousPageCommand, graphControl);
      nextPageButton.SetCommand(NextPageCommand, graphControl);
    }

    #region custom commands

    /// <summary>
    /// Command for switching to the next page
    /// </summary>
    public static readonly ICommand PreviousPageCommand = Commands.CreateCommand("Previous Page");

    /// <summary>
    /// Command for switching to the previous page
    /// </summary>
    public static readonly ICommand NextPageCommand = Commands.CreateCommand("Next Page");

    private void PreviousPage_CanExecute(object sender, CanExecuteCommandEventArgs e) {
      e.CanExecute = IsPageNumberValid(pageNumber-1);
      e.Handled = true;
    }

    private void NextPage_CanExecute(object sender, CanExecuteCommandEventArgs e) {
      e.CanExecute = IsPageNumberValid(pageNumber + 1);
      e.Handled = true;
    }

    private void PreviousPage_Executed(object sender, ExecutedCommandEventArgs e) {
      SetPageNumber(pageNumber-1, null);
    }

    private void NextPage_Executed(object sender, ExecutedCommandEventArgs e) {
      SetPageNumber(pageNumber + 1, null);
    }

    #endregion

    private void RunMultipageLayout() {

      // parse the pageWidth and pageHeight parameters
      double pageWidth;
      double pageHeight;

      if (!Double.TryParse(pageWidthTextBox.Text, out pageWidth)) {
        pageWidth = 800;
      }
      if (!Double.TryParse(pageHeightTextBox.Text, out pageHeight)) {
        pageHeight = 800;
      }

      // get the core layout
      string coreLayoutKey = coreLayoutComboBox.SelectedItem as string;
      if (coreLayoutKey != null && coreLayouts.ContainsKey(coreLayoutKey)) {
        coreLayout = coreLayouts[coreLayoutKey];
      } else {
        coreLayout = coreLayouts["Hierarchic"];
      }

      // a data provider for the node, edge, and label IDs:
      // this data provider returns the node/edge/label instances themselves

      var multiPageLayoutData = new MultiPageLayoutData
      {
        NodeIds = {Delegate = node => node},
        EdgeIds = {Delegate = edge => edge},
        NodeLabelIds = {Delegate = label => label},
        EdgeLabelIds = {Delegate = label => label},
        AbortHandler = abortHandler = new AbortHandler()
      };

      // apply the multi page layout
      // multiPageLayout contains a list with the single page graphs
      MultiPageLayout multiPageLayout = new MultiPageLayout(coreLayout)
      {
        MaximumPageSize = new YDimension(pageWidth, pageHeight),
        LayoutCallback = new DelegateLayoutCallback(result => BeginInvoke(new Action(() => {
          ApplyLayoutResult(result, pageWidth, pageHeight);
          abortHandler = null;
          ShowLoadingIndicator(false);
          // force to update the command state
          CommandManager.InvalidateRequerySuggested();
        })))
      };

      // execute layout in thread to prevent ui blocking
      new Thread(() => {
        try {
          modelGraph.ApplyLayout(multiPageLayout, multiPageLayoutData);
        } catch (AlgorithmAbortedException) {
          // layout was aborted
          BeginInvoke(new Action(() => {
            // reset abortHandler and loading indicator in the view thread
            abortHandler = null;
            ShowLoadingIndicator(false);
          }));
        }
      }).Start();
    }

    /// <summary>
    /// Applies the result of the multipage layout using a <see cref="MultiPageIGraphBuilder"/>.
    /// </summary>
    private void ApplyLayoutResult(MultiPageLayoutResult multiPageLayout, double pageWidth, double pageHeight)
    {
      // use the MultiPageGraphBuilder to create a list of IGraph instances that represent the single pages
      MultiPageIGraphBuilder builder = new MultiPageIGraphBuilder(multiPageLayout) {
        // assign custom template node styles for the auxiliary nodes introduced by the multipage layout.
        // also set the label style and model parameter. 
        // if nothing is specified, the values of the original graph are copied.
        NormalNodeDefaults = {
          Style = DemoStyles.CreateDemoNodeStyle(Themes.Palette21),
          Labels = {
            Style = DemoStyles.CreateDemoNodeLabelStyle(Themes.Palette21),
            LayoutParameter = InteriorLabelModel.Center
          }
        },
        ConnectorNodeDefaults = {
          Style = DemoStyles.CreateDemoNodeStyle(Themes.Palette23),
          Labels = {
            Style = DemoStyles.CreateDemoNodeLabelStyle(Themes.Palette23),
            LayoutParameter = InteriorLabelModel.Center
          }
        },
        ProxyNodeDefaults = {
          Style = DemoStyles.CreateDemoNodeStyle(Themes.Palette25),
          Labels = {
            Style = DemoStyles.CreateDemoNodeLabelStyle(Themes.Palette25),
            LayoutParameter = InteriorLabelModel.Center
          }
        },
        ProxyReferenceNodeDefaults = {
          Style = DemoStyles.CreateDemoNodeStyle(Themes.Palette14),
          Labels = {
            Style = DemoStyles.CreateDemoNodeLabelStyle(Themes.Palette14),
            LayoutParameter = InteriorLabelModel.Center
          }
        },
        NormalEdgeDefaults = {
          Style =DemoStyles.CreateDemoEdgeStyle(Themes.Palette21) 
        },
        ConnectorEdgeDefaults = {
          Style = DemoStyles.CreateDemoEdgeStyle(Themes.Palette23)
        },
        ProxyEdgeDefaults = {
          Style = DemoStyles.CreateDemoEdgeStyle(Themes.Palette25)
        },
        ProxyReferenceEdgeDefaults = {
          Style = DemoStyles.CreateDemoEdgeStyle(Themes.Palette14)
        }
      };


      // create the graphs
      viewGraphs = builder.CreateViewGraphs();
      SetPageNumber(0, null);

      // set the new page bounds
      pageBoundsVisualCreator.PageWidth = pageWidth;
      pageBoundsVisualCreator.PageHeight = pageHeight;
      ShowLoadingIndicator(false);
      graphControl.Focus();
    }

    private void SetPageNumber(int newPageNumber, INode targetNode) {
      graphControl.HighlightIndicatorManager.ClearHighlights();
      if (newPageNumber < 0) {
        newPageNumber = 0;
      } else if (newPageNumber > viewGraphs.Length - 1) {
        newPageNumber = viewGraphs.Length - 1;
      }
      graphControl.FocusIndicatorManager.FocusedItem = null;
      pageNumber = newPageNumber;
      pageNumberTextBox.Text = (pageNumber+1).ToString();
      graphControl.Graph = GetCurrentGraph();
      // update the content bounds
      graphControl.UpdateContentRect();
      // set the page center
      pageBoundsVisualCreator.Center = graphControl.ContentRect.Center;

      // place target node under mouse cursor
      if (targetNode != null && graphControl.Graph.Contains(targetNode)) {
        PointD controlCenter = graphControl.ToWorldCoordinates(new PointD(graphControl.Width * 0.5, graphControl.Height * 0.5));
        graphControl.ZoomTo(targetNode.Layout.GetCenter() - (graphControl.LastEventLocation - controlCenter), graphControl.Zoom);
        graphControl.HighlightIndicatorManager.AddHighlight(targetNode);
      } else {
        graphControl.FitGraphBounds();
      }
    }

    /// <summary>
    /// "Jump" to a referencing node of a clicked auxiliary multi-page node.
    /// </summary>
    /// <param name="viewNode">The multi page node that has been clicked</param>
    private void GotoReferencingNode(INode viewNode) {
      IGraph graph = GetCurrentGraph();
      // get the ID of the referencing node
      MultiPageIGraphBuilder.NodeData nodeData = graph.MapperRegistry.GetMapper<INode, MultiPageIGraphBuilder.NodeData>(MultiPageIGraphBuilder.MapperKeyNodeData)[viewNode];
      if (nodeData != null && nodeData.IsReferenceNode) {
        var referencedNode = nodeData.ReferencedNode;
        if (referencedNode != null) {
          int targetPage = GetPageNumber(referencedNode);
          // open the page and center on the referencing node
          if (IsPageNumberValid(targetPage)) {
            SetPageNumber(targetPage, referencedNode);
          }
        }
      }
    }

    /// <summary>
    /// Gets the number of the page a node is contained in.
    /// </summary>
    /// <param name="node">The node to get the page number for.</param>
    /// <returns>The page number, or -1 if the node is not part of any page.</returns>
    private int GetPageNumber(INode node) {
      MultiPageIGraphBuilder.NodeData nodeData =
        GetCurrentGraph().MapperRegistry.GetMapper<INode, MultiPageIGraphBuilder.NodeData>(
          MultiPageIGraphBuilder.MapperKeyNodeData)[node];
      return nodeData != null ? nodeData.PageNumber : -1;
    }

    #region UI event handlers

    private void RunLayout_Click(object sender, EventArgs e) {
      // calculate a new layout
      ShowLoadingIndicator(true);
      RunMultipageLayout();
    }

    private void ShowLoadingIndicator(bool visible) {
      if (visible) {
        stopRequested = false;
        stopButton.Text = "Stop";
        loadingIndicator.Visible = true;
      } else {
        loadingIndicator.Visible = false;
      }
    }

    private bool stopRequested;
    private AbortHandler abortHandler;

    private void StopLayoutButtonClick(object sender, EventArgs e) {
      if (stopRequested) {
        abortHandler.Cancel();
      } else {
        stopRequested = true;
        stopButton.Text = "Abort Immediately";
        abortHandler.Stop();
      }
    }
    
    private bool inputGraphLoaded;

    /// <summary>
    /// Displays the original graph
    /// </summary>
    private void ShowInputGraph_Click(object sender, EventArgs e) {
      if (((CheckBox)sender).Checked) {
        showInputGraphButton.Text = "Hide Input Graph";
        graphControlSplitContainer.IsSplitterFixed = false;
        graphControlSplitContainer.SplitterDistance = (int)(graphControlSplitContainer.Width*0.5);
        if (!inputGraphLoaded) {
          // load the graph
          inputGraphLoaded = true;
          originalGraphControl.ImportFromGraphML("Resources/pop-artists-small.graphml");
        }
        originalGraphControl.FitGraphBounds();
        graphControl.FitGraphBounds();
      } else {
        graphControlSplitContainer.SplitterDistance = 0;
        showInputGraphButton.Text = "Show Input Graph";
        graphControlSplitContainer.IsSplitterFixed = true;
        graphControl.FitGraphBounds();
      }
    }

    /// <summary>
    /// Sets the page number.
    /// </summary>
    private void PageNumberTextBox_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {
        int pageNo;
        if (Int32.TryParse(pageNumberTextBox.Text, out pageNo)) {
          SetPageNumber(pageNo-1, null);
        } else {
          pageNumberTextBox.Focus();
        }
      }
    }

    #endregion

    #region utility methods

    /// <summary>
    /// Returns the currently selected page graph.
    /// </summary>
    private IGraph GetCurrentGraph() {
      return viewGraphs[pageNumber];
    }

    /// <summary>
    /// Checks if the given page number is valid.
    /// </summary>
    private bool IsPageNumberValid(int pageNo) {
      return pageNo >= 0 && viewGraphs != null && pageNo < viewGraphs.Length;
    }

    #endregion    

    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MultiPageForm());
    }

    #endregion


  }

  /// <summary>
  /// This class renders the page size
  /// </summary>
  internal class PageBoundsVisualCreator : IVisualCreator
  {
    private const double Margin = 8;

    public PageBoundsVisualCreator() {
      this.PageWidth = 0;
      this.PageHeight = 0;
    }

    /// <summary>
    /// The page Width
    /// </summary>
    public double PageWidth { get; set; }

    /// <summary>
    /// The Page Height
    /// </summary>
    public double PageHeight { get; set; }

    /// <summary>
    /// The Page Center
    /// </summary>
    public PointD Center { get; set; }

    private MutableRectangle rectangle;

    public IVisual CreateVisual(IRenderContext context) {
      rectangle = new MutableRectangle(RectD.FromCenter(Center, new SizeD(PageWidth + Margin, PageHeight + Margin)));
      var rectVisual = new RectangleVisual(rectangle) {Pen = new Pen(Color.Black, 1) {DashStyle = DashStyle.Dash}};
      return rectVisual;
    }

    public IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      rectangle.Reshape(RectD.FromCenter(Center, new SizeD(PageWidth + Margin, PageHeight + Margin)));
      return oldVisual;
    }
  }

  internal delegate void LayoutCallbackDelegate(MultiPageLayoutResult layout);

  class DelegateLayoutCallback : ILayoutCallback
  {

    private LayoutCallbackDelegate layoutDelegate;

    public DelegateLayoutCallback(LayoutCallbackDelegate layoutDelegate) {
      this.layoutDelegate = layoutDelegate;
    }

    public void LayoutDone(MultiPageLayoutResult result) {
      layoutDelegate(result);
    }
  }
}
