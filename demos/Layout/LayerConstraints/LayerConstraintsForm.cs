/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.4.
 ** Copyright (c) 2000-2021 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.yFiles.Layout.LayerConstraints.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout.Hierarchic;

namespace Demo.yFiles.Layout.LayerConstraints
{
  /// <summary>
  /// This demo shows how to use layer constraints with the <see cref="HierarchicLayout"/> to
  /// restrict the node layering.
  /// </summary>
  public partial class LayerConstraintsForm : Form
  {
    
    /// <summary>
    /// Initializes a new instance of the <see cref="LayerConstraintsForm"/> class.
    /// </summary>
    public LayerConstraintsForm() {
      InitializeComponent();
      RegisterToolStripCommands();
    }

    #region Initialization

    /// <summary>
    /// The default style
    /// </summary>
    private readonly INodeStyle defaultStyle = new ConstraintsNodeStyle();

    /// <summary>
    /// Used to create new <see cref="LayerConstraintsInfo"/> objects with random weight that are randomly enabled/disabled.
    /// </summary>
    private readonly Random rand = new Random();

    /// <summary>
    /// Registers the tool strip commands.
    /// </summary>
    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitGraphBounds, graphControl);

      loadGraphMLMenuItem.SetCommand(Commands.Open, graphControl);
      saveGraphMLMenuItem.SetCommand(Commands.SaveAs, graphControl);
      increaseZoomToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      decreaseZoomToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitGraphBoundsToolStripMenuItem.SetCommand(Commands.FitGraphBounds, graphControl);
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeInputModes"/>
    /// <seealso cref="InitializeGraph"/>
    private async void OnLoaded(object sender, EventArgs e) {
      // load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // initialize the graph
      await InitializeGraph();

      // initialize the input mode
      InitializeInputModes();
    }

    /// <summary>
    /// Initializes the graph instance setting default styles
    /// and creating a small sample graph.
    /// </summary>
    protected virtual async Task InitializeGraph() {
      IGraph graph = graphControl.Graph;

      // set the style as the default for all new nodes
      graph.NodeDefaults.Style = defaultStyle;
      graph.NodeDefaults.Size = new SizeD(90, 65);

      // create a simple label style
      DefaultLabelStyle labelStyle = new DefaultLabelStyle
      {
        BackgroundBrush = Brushes.White,
        AutoFlip = true
      };

      // set the style as the default for all new node labels
      graph.NodeDefaults.Labels.Style = labelStyle;
      graph.EdgeDefaults.Labels.Style = labelStyle;

      graph.EdgeDefaults.Labels.LayoutParameter = new EdgeSegmentLabelModel().CreateDefaultParameter();

      // create the graph and perform a layout operation
      CreateNewGraph();
      await DoLayout();
    }

    /// <summary>
    /// Calls <see cref="CreateEditorMode"/> and registers
    /// the result as the <see cref="CanvasControl.InputMode"/>.
    /// </summary>
    protected virtual void InitializeInputModes() {
      graphControl.InputMode = CreateEditorMode();
    }

    /// <summary>
    /// Creates the default input mode for the <see cref="GraphControl"/>,
    /// a <see cref="GraphEditorInputMode"/>.
    /// </summary>
    /// <remarks>
    /// The control uses a custom node creation callback that creates business objects for newly
    /// created nodes.
    /// </remarks>
    /// <returns>a new <see cref="GraphEditorInputMode"/> instance</returns>
    protected virtual IInputMode CreateEditorMode() {
      var mode = new GraphEditorInputMode
                   {
                     NodeCreator = CreateNodeCallback,
                     // disable node resizing
                     ShowHandleItems = GraphItemTypes.Bend | GraphItemTypes.Edge
                   };
      // only allow numeric values to be specified as label text
      mode.ValidateLabelText += delegate(object sender, LabelTextValidatingEventArgs labelTextValidatingEventArgs) {
        labelTextValidatingEventArgs.NewText = labelTextValidatingEventArgs.NewText.Trim();
        if (labelTextValidatingEventArgs.NewText.Length == 0) {
          return;
        }
        double result;
        if (
          !Double.TryParse(labelTextValidatingEventArgs.NewText, NumberStyles.Float,
                           Thread.CurrentThread.CurrentUICulture, out result)) {
          // only allow numbers between 0 and 100
          if (result <= 100 && result >= 0) {
            labelTextValidatingEventArgs.Cancel = true;
          }
        }
      };

	    // register command bindings
	    mode.KeyboardInputMode.AddCommandBinding(DecreaseLayerCommand, DecreaseLayerExecuted, CanExecuteDecreaseLayer);
	    mode.KeyboardInputMode.AddCommandBinding(IncreaseLayerCommand, IncreaseLayerExecuted, CanExecuteIncreaseLayer);
	    mode.KeyboardInputMode.AddCommandBinding(ToggleConstraintsStateCommand, ToggleConstraintsStateExecuted, CanExecuteToggleConstraintsState);
			mode.KeyboardInputMode.AddCommandBinding(InvalidateControlCommand, delegate(object sender,
	                                                                                           ExecutedCommandEventArgs e) {
		    var gc = sender as GraphControl;
		    if (gc != null) {
			    gc.Invalidate();
			    e.Handled = true;
		    }
	    }, delegate(object sender, CanExecuteCommandEventArgs e) {
		    var gc = sender as GraphControl;
		    e.CanExecute = gc != null;
				e.Handled = true;
	    });
      return mode;
    }

    /// <summary>
    /// Callback that actually creates the node and its business object
    /// </summary>
    private INode CreateNodeCallback(IInputModeContext context, IGraph graph, PointD location, INode parent) {
      RectD newBounds = RectD.FromCenter(location, graph.NodeDefaults.Size);
      var node = graph.CreateNode(newBounds);
      node.Tag = new LayerConstraintsInfo
      {
        Value = rand.Next(0, 7),
        Constraints = rand.NextDouble() < 0.9
      };
      return node;
    }

    /// <summary>
    /// Calculates the weight of an edge by translating its (first) label into an int.
    /// It will return 0 if the label is not a correctly formatted double.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    private int GetEdgeWeight(IEdge edge) {
      // if edge has at least one label...
      if (edge.Labels.Count > 0) {
        // return its value
        return (int) Convert.ToDouble(edge.Labels[0].Text, CultureInfo.CurrentUICulture);
      }
      return 1;
    }

    #endregion

    #region Event handler implementation

    /// <summary>
    /// The command that can be used by the buttons to toggle the enabled state of the node.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="ExecutedCommandEventArgs.Parameter"/> as the <see cref="ExecutedCommandEventArgs"/>.
    /// </remarks>
    public static readonly ICommand ToggleConstraintsStateCommand = Commands.CreateCommand("Toogle Constraints Enabled");

    /// <summary>
    /// The command that can be used by the buttons to toggle the enabled state of the node.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand IncreaseLayerCommand = Commands.CreateCommand("Increase Layer");

    /// <summary>
    /// The command that can be used by the buttons to toggle the enabled state of the node.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand DecreaseLayerCommand = Commands.CreateCommand("Decrease Layer");

    /// <summary>
    /// The command that triggers invalidation of the graph control.
    /// </summary>
    public static readonly ICommand InvalidateControlCommand = Commands.CreateCommand("Invalidate");
    
    /// <summary>
    /// Helper method that determines whether the <see cref="ToggleConstraintsStateCommand"/> can be executed.
    /// </summary>
    /// <remarks>
    /// Always returns true.
    /// </remarks>
    private void CanExecuteToggleConstraintsState(object sender, CanExecuteCommandEventArgs e) {
      e.CanExecute = true;
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="ToggleConstraintsStateCommand"/>
    /// </summary>
    private void ToggleConstraintsStateExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = e.Parameter as INode;
      if(node != null) {
        var data = (LayerConstraintsInfo)node.Tag;
        data.ToggleState();
      }
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="IncreaseLayerCommand"/> can be executed.
    /// </summary>
    private void CanExecuteIncreaseLayer(object sender, CanExecuteCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (LayerConstraintsInfo)node.Tag;
        e.CanExecute = data.CanIncreaseValue();
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="DecreaseLayerCommand"/> can be executed.
    /// </summary>
    private void CanExecuteDecreaseLayer(object sender, CanExecuteCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (LayerConstraintsInfo)node.Tag;
        e.CanExecute = data.CanDecreaseValue();
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="IncreaseLayerCommand"/>
    /// </summary>
    private void IncreaseLayerExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (LayerConstraintsInfo)node.Tag;
        data.IncreaseValue();
      }
    }

    /// <summary>
    /// Handler for the <see cref="DecreaseLayerCommand"/>
    /// </summary>
    private void DecreaseLayerExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (LayerConstraintsInfo)node.Tag;
        data.DecreaseValue();
      }
    }


    /// <summary>
    /// Exits the demo.
    /// </summary>
    private void ExitMenuItemClick(object sender, EventArgs e) {
      Application.Exit();
    }

    /// <summary>
    /// Formats the current graph.
    /// </summary>
    private async void OnLayoutClick(object sender, EventArgs e) {
      await DoLayout();
    }

    /// <summary>
    /// Creates a new graph and formats it.
    /// </summary>
    private async void OnNewGraphClick(object sender, EventArgs e) {
      CreateNewGraph();
      await DoLayout();
    }

    /// <summary>
    /// Disables all constraints on the nodes.
    /// </summary>
    private void OnDisableConstraints(object sender, EventArgs e) {
      foreach (var node in graphControl.Graph.Nodes) {
        var data = node.Tag as LayerConstraintsInfo;
        if (data != null) {
          data.Constraints = false;
        }
      }
    }

    /// <summary>
    /// Enables all constraints on the nodes.
    /// </summary>
    private void OnEnableConstraints(object sender, EventArgs e) {
      foreach (var node in graphControl.Graph.Nodes) {
        var data = node.Tag as LayerConstraintsInfo;
        if (data != null) {
          data.Constraints = true;
        }
      }
    }

    #endregion

    #region Graph creation and layout

    #region Create a random graph

    /// <summary>
    /// Clears the existing graph and creates a new random graph
    /// </summary>
    private void CreateNewGraph() {
      // remove all nodes and edges from the graph
      graphControl.Graph.Clear();

      // create a new random graph
      new RandomGraphGenerator
        {
          AllowCycles = true,
          AllowMultipleEdges = false,
          AllowSelfLoops = false,
          EdgeCount = 25,
          NodeCount = 20,
          NodeCreator = graph => CreateNodeCallback(null, graph, PointD.Origin, null)
        }.Generate(graphControl.Graph);

      // center the graph to prevent the initial layout fading in from the top left corner
      graphControl.FitGraphBounds();
    }

    #endregion

    private async Task DoLayout() {
      // layout starting, disable button
      runButton.Enabled = false;

      // create a new layout algorithm
      var hl = new HierarchicLayout
                  {
                    OrthogonalRouting = true,
                    FromScratchLayeringStrategy = LayeringStrategy.HierarchicalTopmost,
                    IntegratedEdgeLabeling = true
                  };

      // and layout data for it
      var hlData = new HierarchicLayoutData {
        ConstraintIncrementalLayererAdditionalEdgeWeights = { Delegate = GetEdgeWeight }
      };

      // we provide the LayerConstraintData.Values directly as IComparable instead of using the Add* methods on LayerConstraintData
      var layerConstraintData = hlData.LayerConstraints;
      layerConstraintData.NodeComparables.Delegate = node => {  
        var data = node.Tag as LayerConstraintsInfo;
        if (data != null && data.Constraints) {
          // the node shall be constrained so we use its Value as comparable
          return data.Value;
        }
        // otherwise we don't add constraints for the node
        return null;
      };

      INode[] layerRep = new INode[8];

      // additionally enforce all nodes with a LayerConstraintInfo.Value of 0 or 7 to be placed at top/bottom
      // and register the value in the NodeComparables Mapper for all other constrained nodes
      foreach (var node in graphControl.Graph.Nodes) {
        var data = node.Tag as LayerConstraintsInfo;
        if (data != null && data.Constraints) {
          if (data.Value == 0) {
            // add constraint to put this node at the top
            layerConstraintData.PlaceAtTop(node);
          } else if (data.Value == 7) {
            // add constraint to put this node at the bottom
            layerConstraintData.PlaceAtBottom(node);
          } else {
            // for every node in between we record its value with the mapper, assuring that there
            // will be no layer with different values and monotonically growing values per layer
            layerConstraintData.NodeComparables.Mapper[node] = data.Value;
          }
        }
      }

      // perform the layout operation
      await graphControl.MorphLayout(hl, TimeSpan.FromSeconds(1), hlData);
      // code is executed once the layout operation is finished
      // enable button again
      runButton.Enabled = true;
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
      layerConstraintsForm = new LayerConstraintsForm();
      Application.Run(layerConstraintsForm);
    }

    private static LayerConstraintsForm layerConstraintsForm;

    /// <summary>
    /// Helper method to find the graph control that is bound to the currently running application from a static context
    /// </summary>
    /// <returns></returns>
    internal static GraphControl GetActiveGraphControl() {
      if (layerConstraintsForm != null) {
        return layerConstraintsForm.graphControl;
      }
      return null;
    }

    #endregion
  }
}
