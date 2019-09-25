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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.yFiles.Graph.OrgChart.Properties;
using yWorks.Algorithms;
using yWorks.Algorithms.Geometry;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Tree;
using yWorks.Utils;

namespace Demo.yFiles.Graph.OrgChart
{
  /// <summary>
  /// Interaction logic for OrgChartForm.xaml
  /// </summary>
  public partial class OrgChartForm
  {
    private GraphViewerInputMode editMode;

    // Ranges that specify which style to take at which zoom level
    private OrgChartNodeStyle.DoubleRange overviewRange;
    private OrgChartNodeStyle.DoubleRange intermediateRange;
    private OrgChartNodeStyle.DoubleRange detailRange;

    public OrgChartForm() {
      InitializeComponent();
      hiddenNodesSet = new HashSet<INode>();
      RegisterToolStripCommands();
      FormLoaded();
    }


    /// <summary>
    /// The command that can be used by the buttons to show the parent node.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand ShowParentCommand = Commands.CreateCommand("Show Parent");

    /// <summary>
    /// The command that can be used by the buttons to hide the parent node.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand HideParentCommand = Commands.CreateCommand("Hide Parent");

    /// <summary>
    /// The command that can be used by the buttons to show the child nodes.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand ShowChildrenCommand = Commands.CreateCommand("Show Children");

    /// <summary>
    /// The command that can be used by the buttons to hide the child nodes.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand HideChildrenCommand = Commands.CreateCommand("Hide Children");

    /// <summary>
    /// The command that can be used by the buttons to expand all collapsed nodes.
    /// </summary>
    public static readonly ICommand ShowAllCommand = Commands.CreateCommand("Show All");

    /// <summary>
    /// Used by the predicate function to determine which nodes should not be shown.
    /// </summary>
    private readonly HashSet<INode> hiddenNodesSet;

    /// <summary>
    /// The filtered graph instance that hides nodes from the to create smaller graphs for easier navigation.
    /// </summary>
    private FilteredGraphWrapper filteredGraphWrapper;

    private void FormLoaded() {

      // load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      InitializeGraph();

      // build the object model from the XML
      var employeeRoots = TreeBuilder.BuildEmployeesFromXml();
      BuildGraph(employeeRoots);
      BuildTree(employeeRoots);
      InitializeInputModes();
      InitializeToolTips();

      graphControl.CurrentItemChanged += graphControl_CurrentItemChanged;
			
			// disable selection, focus and highlight painting
      GraphControl.SelectionIndicatorManager.Enabled = false;
      GraphControl.FocusIndicatorManager.Enabled = false;
      GraphControl.HighlightIndicatorManager.Enabled = false;

      // we wrap the graph instance by a filtered graph wrapper
      filteredGraphWrapper = new FilteredGraphWrapper(GraphControl.Graph, ShouldShowNode, edge => true);
      GraphControl.Graph = filteredGraphWrapper;

      graphOverviewControl.GraphControl = graphControl;

      // now calculate the initial layout
      DoLayout();

      GraphControl.FitGraphBounds();
      LimitViewport();
    }

    /// <summary>
    /// Defer this until the form has its initial correct size...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InitialSizeChanged(object sender, EventArgs e) {
      this.SizeChanged -= InitialSizeChanged;
      GraphControl.FitGraphBounds();
    }


    /// <summary>
    /// Called when an item has been double clicked.
    /// </summary>
    /// <param name="o">The source of the event.</param>
    /// <param name="itemClickedEventArgs">The event argument instance containing the event data.</param>
    private async void OnItemDoubleClicked(object o, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      graphControl.CurrentItem = itemClickedEventArgs.Item;
      await ZoomToCurrentItem();
    }

    /// <summary>
    /// Updates the details view if a node is selected
    /// </summary>
    private void graphControl_CurrentItemChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
      Employee employee = graphControl.CurrentItem != null ? graphControl.CurrentItem.Tag as Employee : null;
      if (employee != null) {
        NameLabel.Text = employee.Name;
        PositionLabel.Text = employee.Position;
        PhoneLabel.Text = employee.Phone;
        FaxLabel.Text = employee.Fax;
        EmailLabel.Text = employee.Email;
        BusinessUnitLabel.Text = employee.BusinessUnit;
        switch (employee.Status) {
          case EmployeeStatus.Present:
            StatusLabel.Text = "Present";
            StatusLabel.BackColor = Color.Green;
            break;
          case EmployeeStatus.Unavailable:
            StatusLabel.Text = "Unavailable";
            StatusLabel.BackColor = Color.Red;
            break;
          case EmployeeStatus.Travel:
            StatusLabel.Text = "Travel";
            StatusLabel.BackColor = Color.Purple;
            break;
          default:
            StatusLabel.Text = "";
            StatusLabel.BackColor = Color.White;
            break;
        }
        SelectTreeNode(employee, structureTreeView.Nodes[0]);
      } else {
        NameLabel.Text = "";
        PositionLabel.Text = "";
        PhoneLabel.Text = "";
        FaxLabel.Text = "";
        EmailLabel.Text = "";
        BusinessUnitLabel.Text = "";
        StatusLabel.Text = "";
        StatusLabel.BackColor = Color.White;
      }
    }

    private bool SelectTreeNode(Employee employee, TreeNode root) {
      TreeNode treeNode = root;
      if (treeNode.Tag == employee) {
        structureTreeView.SelectedNode = treeNode;
        return true;
      } else {
        for (TreeNode child = treeNode.FirstNode; child != null; child = child.NextNode) {
          if (SelectTreeNode(employee, child)) {
            return true;
          }
        }
      }
      return false;
    }


    private void InitializeGraph() {
      // create new nodestyle that delegates to other 
      // styles for different zoom ranges
      var nodeStyle = new OrgChartNodeStyle();

      // specify the three different zoom ranges
      overviewRange = new OrgChartNodeStyle.DoubleRange(0, 0.3);
      intermediateRange = new OrgChartNodeStyle.DoubleRange(0.3, 0.8);
      detailRange = new OrgChartNodeStyle.DoubleRange(0.8, Double.PositiveInfinity);

      // set different node styles for the zoom ranges
      nodeStyle.SetNodeStyle(overviewRange, new OverviewNodeStyle());
      nodeStyle.SetNodeStyle(intermediateRange, new IntermediateNodeStyle());

      DetailNodeStyle detailNodeStyle = new DetailNodeStyle();
      // add buttons to detail node style
      ButtonDecoratorNodeStyle buttonDecorator = new ButtonDecoratorNodeStyle();
      buttonDecorator.DecoratedStyle = detailNodeStyle;
      AddDecoratorButtons(buttonDecorator);

      nodeStyle.SetNodeStyle(detailRange, buttonDecorator);

      graphControl.Graph.NodeDefaults.Style = nodeStyle;
      graphControl.Graph.NodeDefaults.Size = new SizeD(250, 100);

      graphControl.Graph.EdgeDefaults.Style = new PolylineEdgeStyle { SmoothingLength = 10 };
    }

    /// <summary>
    /// Adds the buttons to the <see cref="ButtonDecoratorNodeStyle" />.
    /// </summary>
    private void AddDecoratorButtons(ButtonDecoratorNodeStyle buttonDecorator) {
      int buttonSize = 14;
      // show parent button
      var b1 = new Button {
                   Command = ShowParentCommand,
                   CanExecuteHandler = CanExecuteShowParent,
                   CommandParameter = Button.UseNodeParameter,
                   CommandTarget = Button.UseCanvasControlTarget,
                   // set a label as the button visualization
                   Visualization = new SimpleLabel(null, "", FreeNodeLabelModel.Instance.CreateParameter(
                     new PointD(1, 1),
                     new PointD(-32, -32),
                     PointD.Origin,
                     PointD.Origin, 0)) {
                       // style the label
                       Style = new ButtonLabelStyle {
                         BackgroundColor = Color.Black,
                         ForegroundColor = Color.White,
                         Icon = ButtonLabelStyle.ButtonIcon.ShowParent
                       },
                       PreferredSize = new SizeD(buttonSize, buttonSize)
                     }
                 };
      // set ButtonLabelStyle.Button so the style knows its owner
      ((ButtonLabelStyle) b1.Visualization.Style).Button = b1;
      buttonDecorator.Buttons.Add(b1);

      // hide parent button
      var b2 = new Button {
                   Command = HideParentCommand,
                   CanExecuteHandler = CanExecuteHideParent,
                   CommandParameter = Button.UseNodeParameter,
                   CommandTarget = Button.UseCanvasControlTarget,
                   // set a label as the button visualization
                   Visualization = new SimpleLabel(null, "", FreeNodeLabelModel.Instance.CreateParameter(
                     new PointD(1, 1),
                     new PointD(-17, -32),
                     PointD.Origin, PointD.Origin, 0)) {
                       // style the label
                       Style = new ButtonLabelStyle {
                         BackgroundColor = Color.Black,
                         ForegroundColor = Color.White,
                         Icon = ButtonLabelStyle.ButtonIcon.HideParent
                       },
                       PreferredSize = new SizeD(buttonSize, buttonSize)
                     }
                 };
      // set ButtonLabelStyle.Button so the style knows it's owner
      ((ButtonLabelStyle)b2.Visualization.Style).Button = b2;
      buttonDecorator.Buttons.Add(b2);

      // show children button
      var b3 = new Button {
                   Command = ShowChildrenCommand,
                   CanExecuteHandler = CanExecuteShowChildren,
                   CommandParameter = Button.UseNodeParameter,
                   CommandTarget = Button.UseCanvasControlTarget,
                   // set a label as the button visualization
                   Visualization = new SimpleLabel(null, "", FreeNodeLabelModel.Instance.CreateParameter(
                     new PointD(1, 1),
                     new PointD(-32, -17),
                     PointD.Origin, PointD.Origin, 0)) {
                       // style the label
                       Style = new ButtonLabelStyle {
                         BackgroundColor = Color.Black,
                         ForegroundColor = Color.White,
                         Icon = ButtonLabelStyle.ButtonIcon.ShowChildren
                       },
                       PreferredSize = new SizeD(buttonSize, buttonSize)
                     }
                 };
      // set ButtonLabelStyle.Button so the style knows it's owner
      ((ButtonLabelStyle)b3.Visualization.Style).Button = b3;
      buttonDecorator.Buttons.Add(b3);

      // hide children button
      var b4 = new Button {
                   Command = HideChildrenCommand,
                   CanExecuteHandler = CanExecuteHideChildren,
                   CommandParameter = Button.UseNodeParameter,
                   CommandTarget = Button.UseCanvasControlTarget,
                   // set a label as the button visualization
                   Visualization = new SimpleLabel(null, "", FreeNodeLabelModel.Instance.CreateParameter(
                     new PointD(1, 1),
                     new PointD(-17, -17),
                     PointD.Origin, PointD.Origin, 0)) {
                       // style the label
                       Style = new ButtonLabelStyle {
                         BackgroundColor = Color.Black,
                         ForegroundColor = Color.White,
                         Icon = ButtonLabelStyle.ButtonIcon.HideChildren
                       },
                       PreferredSize = new SizeD(buttonSize, buttonSize)
                     }
                 };
      // set ButtonLabelStyle.Button so the style knows it's owner
      ((ButtonLabelStyle)b4.Visualization.Style).Button = b4;
      buttonDecorator.Buttons.Add(b4);
    }
    
    private void InitializeInputModes() {
      editMode = new GraphViewerInputMode
                   {
                     ClickableItems = GraphItemTypes.Node,
                     SelectableItems = GraphItemTypes.None,
                     MarqueeSelectableItems = GraphItemTypes.None,
                     ToolTipItems = GraphItemTypes.None,
                     FocusableItems = GraphItemTypes.Node,
                   };
      editMode.ItemDoubleClicked += OnItemDoubleClicked;

      // add key bindings
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Add, Keys.None, Commands.IncreaseZoom);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Subtract, Keys.None, Commands.DecreaseZoom);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Enter, Keys.None, Commands.ZoomToCurrentItem);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Left, Keys.Control, Commands.ScrollPageLeft);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Right, Keys.Control, Commands.ScrollPageRight);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Up, Keys.Control, Commands.ScrollPageUp);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Down, Keys.Control, Commands.ScrollPageDown);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.PageUp, Keys.Control, ShowParentCommand);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.PageDown, Keys.Control, HideParentCommand);
      editMode.KeyboardInputMode.AddKeyBinding(Keys.Add, Keys.Control, ShowChildrenCommand);
	    editMode.KeyboardInputMode.AddKeyBinding(Keys.Subtract, Keys.Control, HideChildrenCommand);

	    // register command bindings
	    editMode.KeyboardInputMode.AddCommandBinding(HideChildrenCommand, HideChildrenExecuted, CanExecuteHideChildren);
	    editMode.KeyboardInputMode.AddCommandBinding(ShowChildrenCommand, ShowChildrenExecuted, CanExecuteShowChildren);
	    editMode.KeyboardInputMode.AddCommandBinding(HideParentCommand, HideParentExecuted, CanExecuteHideParent);
	    editMode.KeyboardInputMode.AddCommandBinding(ShowParentCommand, ShowParentExecuted, CanExecuteShowParent);
	    editMode.KeyboardInputMode.AddCommandBinding(ShowAllCommand, ShowAllExecuted, CanExecuteShowAll);


      graphControl.InputMode = editMode;
    }

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);
      zoomToCurrentItemButton.SetCommand(Commands.ZoomToCurrentItem, graphControl);

      showParentButton.SetCommand(ShowParentCommand, graphControl.CurrentItem, graphControl);
      hideParentButton.SetCommand(HideParentCommand, graphControl.CurrentItem, graphControl);
      showChildrenButton.SetCommand(ShowChildrenCommand, graphControl.CurrentItem, graphControl);
      hideChildrenButton.SetCommand(HideChildrenCommand, graphControl.CurrentItem, graphControl);
      showAllButton.SetCommand(ShowAllCommand, graphControl);

    }

    private void BuildGraph(IEnumerable<Employee> employees) {
      foreach(Employee employee in employees) {
        INode node = graphControl.Graph.CreateNode(PointD.Origin, tag: employee);
        if (employee.SubOrdinates.Count > 0) {
          AddSubOrdinates(node, employee.SubOrdinates);
        }
      }
    }

    private void AddSubOrdinates(INode parentNode, IEnumerable<Employee> employees) {
      foreach (Employee employee in employees) {
        INode childNode = graphControl.Graph.CreateNode(PointD.Origin, tag: employee);
        graphControl.Graph.CreateEdge(parentNode, childNode);
        if (employee.SubOrdinates.Count > 0) {
          AddSubOrdinates(childNode, employee.SubOrdinates);
        }
      }
    }

    /// <summary>
    /// The predicate used for the FilterGraphWrapper
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private bool ShouldShowNode(INode obj) {
      return !hiddenNodesSet.Contains(obj);
    }

    private void LimitViewport() {
      GraphControl.UpdateContentRect();
      ViewportLimiter limiter = GraphControl.ViewportLimiter;
      limiter.HonorBothDimensions = false;
      limiter.Bounds = GraphControl.ContentRect.GetEnlarged(100);
    }

    /// <summary>
    /// Gets the GraphControl instance used in the form.
    /// </summary>
    public GraphControl GraphControl {
      get { return graphControl; }
    }

    #region Tree Layout Configuration and initial execution

    /// <summary>
    /// Does a tree layout of the graph.
    /// The layout and assistant attributes from the business data of the employees are used to
    /// guide the the layout.
    /// </summary>
    public void DoLayout() {
      IGraph tree = graphControl.Graph;
      var layoutData = CreateLayoutData(tree, null);
      tree.ApplyLayout(new BendDuplicatorStage(new TreeLayout()), layoutData);
    }

    private static LayoutData CreateLayoutData(IGraph tree, INode centerNode) {
      var data = new TreeLayoutData
      {
        NodePlacers =
        {
          Delegate = delegate(INode node) {
            var employee = node.Tag as Employee;
            if (tree.OutDegree(node) == 0 || employee == null) {
              return null;
            }
            var layout = employee.Layout;
            switch (layout) {
              case EmployeeLayout.RightHanging:
                return new AssistantNodePlacer
                {
                  ChildNodePlacer = new DefaultNodePlacer(ChildPlacement.VerticalToRight, RootAlignment.LeadingOnBus, 30, 30) { RoutingStyle = RoutingStyle.ForkAtRoot }
                };
              case EmployeeLayout.LeftHanging:
                return new AssistantNodePlacer
                {
                  ChildNodePlacer = new DefaultNodePlacer(ChildPlacement.VerticalToLeft, RootAlignment.LeadingOnBus, 30, 30) { RoutingStyle = RoutingStyle.ForkAtRoot }
                };
              case EmployeeLayout.BothHanging:
                return new AssistantNodePlacer
                {
                  ChildNodePlacer = new LeftRightNodePlacer() { PlaceLastOnBottom = false }
                };
              default:
                return new AssistantNodePlacer
                {
                  ChildNodePlacer = new DefaultNodePlacer(ChildPlacement.HorizontalDownward, RootAlignment.Median, 30, 30)
                };
            }
          }
        },
        AssistantNodes =
        {
          Delegate = delegate(INode node) {
            var employee = node.Tag as Employee;
            return employee != null && employee.Assistant;
          }
        }
      };

      return data.CombineWith(new FixNodeLayoutData { FixedNodes = { Item = centerNode } });
    }

    #endregion

    #region Command Binding Helper methods

    /// <summary>
    /// Helper method that determines whether the <see cref="ShowParentCommand"/> can be executed.
    /// </summary>
    public void CanExecuteShowChildren(object sender, CanExecuteCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout && filteredGraphWrapper != null) {
        e.CanExecute = filteredGraphWrapper.OutDegree(node) != filteredGraphWrapper.WrappedGraph.OutDegree(node);
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="ShowChildrenCommand"/>
    /// </summary>
    public async void ShowChildrenExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout) {
        int count = hiddenNodesSet.Count;
        foreach (var childEdge in filteredGraphWrapper.WrappedGraph.OutEdgesAt(node)) {
          var child = childEdge.GetTargetNode();
          if (hiddenNodesSet.Remove(child)) {
            filteredGraphWrapper.WrappedGraph.SetNodeCenter(child, node.Layout.GetCenter());
            filteredGraphWrapper.WrappedGraph.ClearBends(childEdge);
          }
        }
        await RefreshLayout(count, node);
      }
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="ShowParentCommand"/> can be executed.
    /// </summary>
    private void CanExecuteShowParent(object sender, CanExecuteCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout && filteredGraphWrapper != null) {
        e.CanExecute = filteredGraphWrapper.InDegree(node) == 0 && filteredGraphWrapper.WrappedGraph.InDegree(node) > 0;
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="ShowParentCommand"/>
    /// </summary>
    private async void ShowParentExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout) {
        int count = hiddenNodesSet.Count;
        foreach (var parentEdge in filteredGraphWrapper.WrappedGraph.InEdgesAt(node)) {
          var parent = parentEdge.GetSourceNode();
          if (hiddenNodesSet.Remove(parent)) {
            filteredGraphWrapper.WrappedGraph.SetNodeCenter(parent, node.Layout.GetCenter());
            filteredGraphWrapper.WrappedGraph.ClearBends(parentEdge);
          }
        }
        await RefreshLayout(count, node);
      }
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="HideParentCommand"/> can be executed.
    /// </summary>
    private void CanExecuteHideParent(object sender, CanExecuteCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout && filteredGraphWrapper != null) {
        e.CanExecute = filteredGraphWrapper.InDegree(node) > 0;
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="HideParentCommand"/>
    /// </summary>
    private async void HideParentExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout) {
        int count = hiddenNodesSet.Count;

        foreach (var testNode in filteredGraphWrapper.WrappedGraph.Nodes) {
          if (testNode != node && filteredGraphWrapper.Contains(testNode) &&
              filteredGraphWrapper.InDegree(testNode) == 0) {
            // this is a root node - remove it and all children unless 
            HideAllExcept(testNode, node);
          }
        }
        await RefreshLayout(count, node);
      }
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="HideChildrenCommand"/> can be executed.
    /// </summary>
    private void CanExecuteHideChildren(object sender, CanExecuteCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout && filteredGraphWrapper != null) {
        e.CanExecute = filteredGraphWrapper.OutDegree(node) > 0;
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="HideChildrenCommand"/>
    /// </summary>
    private async void HideChildrenExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = (e.Parameter ?? graphControl.CurrentItem) as INode;
      if (node != null && !doingLayout) {
        int count = hiddenNodesSet.Count;
        foreach (var child in filteredGraphWrapper.Successors(node)) {
          HideAllExcept(child, node);
        }
        await RefreshLayout(count, node);
      }
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="ShowParentCommand"/> can be executed.
    /// </summary>
    private void CanExecuteShowAll(object sender, CanExecuteCommandEventArgs e) {
      e.CanExecute =  filteredGraphWrapper != null && hiddenNodesSet.Count != 0 && !doingLayout;
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="ShowAllCommand"/>
    /// </summary>
    private async void ShowAllExecuted(object sender, ExecutedCommandEventArgs e) {
      if (!doingLayout) {
        hiddenNodesSet.Clear();
        await RefreshLayout(-1, graphControl.CurrentItem as INode);
      }
    }

    #endregion

    /// <summary>
    /// Help method that hides all nodes and its descendants except for a given node
    /// </summary>
    private void HideAllExcept(INode nodeToHide, INode exceptNode) {
      hiddenNodesSet.Add(nodeToHide);
      foreach (var child in filteredGraphWrapper.WrappedGraph.Successors(nodeToHide)) {
        if (exceptNode != child) {
          HideAllExcept(child, exceptNode);
        }
      }
    }

    // indicates whether a layout is calculated at the moment
    private bool doingLayout;

    /// <summary>
    /// Helper method that refreshes the layout after children or parent nodes have been added
    /// or removed.
    /// </summary>
    private async Task RefreshLayout(int count, INode centerNode) {
      if (doingLayout) {
        return;
      }
      doingLayout = true;
      if (count != hiddenNodesSet.Count) {
        // tell our filter to refresh the graph
        filteredGraphWrapper.NodePredicateChanged();
        // the commands CanExecute state might have changed - suggest a requery.
        CommandManager.InvalidateRequerySuggested();

        // now layout the graph in animated fashion
        IGraph tree = graphControl.Graph;


        // configure the layout data
        var layoutData = CreateLayoutData(tree, centerNode);

        // create the layout algorithm (with a stage that fixes the center node in the coordinate system
        var layout = new BendDuplicatorStage(new FixNodeLayoutStage(new TreeLayout()));

        // configure a LayoutExecutor
        var executor = new LayoutExecutor(graphControl, layout)
        {
          AnimateViewport = centerNode == null,
          EasedAnimation = true,
          RunInThread = true,
          UpdateContentRect = true,
          LayoutData = layoutData,
          Duration = TimeSpan.FromMilliseconds(500)
        };
        await executor.Start();
        doingLayout = false;
        LimitViewport();
      }
    }

    private async Task ZoomToCurrentItem() {
      var currentItem = GraphControl.CurrentItem as INode;
      // visible current item
      if (GraphControl.Graph.Contains(currentItem)) {
	      Commands.ZoomToCurrentItem.Execute(null, GraphControl);
      } else {
        // see if it can be made visible
        if (filteredGraphWrapper.WrappedGraph.Nodes.Contains(currentItem)) {
          // uhide all nodes...
          hiddenNodesSet.Clear();
          // except the node to be displayed and all its descendants
          foreach (var testNode in filteredGraphWrapper.WrappedGraph.Nodes) {
            if (testNode != currentItem && filteredGraphWrapper.WrappedGraph.InDegree(testNode) == 0) {
              HideAllExcept(testNode, currentItem);
            }
          }
          // reset the layout to make the animation nicer
          foreach (var n in filteredGraphWrapper.Nodes) {
            filteredGraphWrapper.SetNodeCenter(n, PointD.Origin);
          }
          foreach (var edge in filteredGraphWrapper.Edges) {
            filteredGraphWrapper.ClearBends(edge);
          }
          await RefreshLayout(-1, null);
        }
      }
    }

    private void InitializeToolTips() {
      editMode.ToolTipItems = GraphItemTypes.Node;
      editMode.QueryItemToolTip +=
        delegate(object src, QueryItemToolTipEventArgs<IModelItem> eventArgs) {
          if (eventArgs.Handled) {
            // A tooltip has already been assigned -> nothing to do.
            return;
          }
          INode hitNode = eventArgs.Item as INode;
          if (hitNode == null) {
            return;
          }
          Employee employee = hitNode.Tag as Employee;
          if (employee == null) {
            return;
          }
          if (overviewRange.IsInRange(graphControl.Zoom) || intermediateRange.IsInRange(graphControl.Zoom)) {
            // show name, position and status for low details
            eventArgs.ToolTip = employee.Name + "\n" + employee.Position + "\nStatus " + employee.Status;
          } else if (detailRange.IsInRange(graphControl.Zoom)) {
            // show all data for detail level
            eventArgs.ToolTip = "Name\t\t"+employee.Name + 
              "\nPosition\t\t" + employee.Position + 
              "\nPhone\t\t" + employee.Phone + 
              "\nFax\t\t" + employee.Fax + 
              "\nEmail\t\t"+ employee.Email +
              "\nBusiness Unit\t" + employee.BusinessUnit + 
              "\nStatus\t\t" + employee.Status;
          }
        };
    }

    #region TreeView related

    /// <summary>
    /// Builds the tree for the structure view
    /// </summary>
    private void BuildTree(IEnumerable<Employee> employees) {
      List<TreeNode> children = new List<TreeNode>();
      foreach (Employee employee in employees) {
        TreeNode node = new TreeNode(employee.Name, GetChildren(employee))
                          {
                            // store employee in TreeNode tag
                            Tag = employee,
                          };
        children.Add(node);
      }
      structureTreeView.Nodes.AddRange(children.ToArray());
      structureTreeView.ExpandAll();
    }

    /// <summary>
    /// Returns an array of TreeNodes which contain the children of employee
    /// </summary>
    private TreeNode[] GetChildren(Employee employee) {
      List<TreeNode> children = new List<TreeNode>();
      foreach (Employee childEmployee in employee.SubOrdinates) {
        TreeNode node = new TreeNode(childEmployee.Name, GetChildren(childEmployee))
                          {
                            // store employee in TreeNode tag
                            Tag = childEmployee
                          }; 
        children.Add(node);
      }
      return children.ToArray();
    }

    private void structureTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
      // get the selected employee
      Employee employee = structureTreeView.SelectedNode.Tag as Employee;

      // get the correspondent node in the graph
      INode selectedNode = filteredGraphWrapper.WrappedGraph.Nodes.FirstOrDefault(node => node.Tag == employee);
      if (selectedNode != null && graphControl.Graph.Contains(selectedNode)) {
        // select the node in the GraphControl
        GraphControl.CurrentItem = selectedNode;
        graphControl.EnsureVisible(selectedNode.Layout.ToRectD());
      }
    }

    private async void structureTreeView_MouseDoubleClick(object sender, MouseEventArgs e) {
      await ZoomToTreeItem();
    }

    private async void structureTreeView_KeyPress(object sender, KeyPressEventArgs e) {
      if (e.KeyChar == '\r') {
        await ZoomToTreeItem();
      }
    }

    /// <summary>
    /// Selects the employee and zooms in on it.
    /// </summary>
    private async Task ZoomToTreeItem() {
      // get the selected employee
      Employee selectedEmployee = structureTreeView.SelectedNode.Tag as Employee;
      // get the correspondent node in the graph
      INode selectedNode = filteredGraphWrapper.WrappedGraph.Nodes.FirstOrDefault(node => node.Tag == selectedEmployee);
      if (selectedNode != null) {
        graphControl.CurrentItem = selectedNode;
        await ZoomToCurrentItem();
      }
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
      Application.Run(new OrgChartForm());
    }

    #endregion
   }

  /// <summary>
  /// LayoutStage that duplicates bends that share a common bus.
  /// </summary>
  class BendDuplicatorStage : LayoutStageBase
  {

    public BendDuplicatorStage()
      : base() {
    }

    public BendDuplicatorStage(ILayoutAlgorithm coreLayouter)
      : base(coreLayouter) {
    }

    public override void ApplyLayout(LayoutGraph graph) {

      ApplyLayoutCore(graph);

      foreach (Node n in graph.Nodes) {
        foreach (Edge e in n.OutEdges) {
          bool lastSegmentOverlap = false;
          IEdgeLayout er = graph.GetLayout(e);
          if (er.PointCount() > 0) {
            // last bend point
            YPoint bendPoint = er.GetPoint(er.PointCount() - 1);

            IEnumerator<Edge> ecc = n.OutEdges.GetEnumerator();
          loop: while (ecc.MoveNext()) {
              Edge eccEdge = ecc.Current;
              if (eccEdge != e) {
                YPointPath path = graph.GetPath(eccEdge);
                for (ILineSegmentCursor lc = path.LineSegments(); lc.Ok; lc.Next()) {
                  LineSegment seg = lc.LineSegment;
                  if (seg.Contains(bendPoint)) {
                    lastSegmentOverlap = true;
                    goto loop;
                  }
                }
              }
            }
          }


          YList points = graph.GetPointList(e);
          for (ListCell c = points.FirstCell; c != null; c = c.Succ()) {
            YPoint p = (YPoint)c.Info;
            if (c.Succ() == null && !lastSegmentOverlap) {
              break;
            }

            YPoint p0 = (YPoint)(c.Pred() == null ? graph.GetSourcePointAbs(e) : c.Pred().Info);
            YPoint p2;
            if (Math.Abs(p0.X - p.X) < 0.01) {
              p2 = new YPoint(p.X, p.Y - 0.001);
            } else {
              p2 = new YPoint(p.X - 0.001, p.Y);
            }

            points.InsertBefore(p2, c);
          }
          graph.SetPoints(e, points);
        }
      }
    }
  }
}
