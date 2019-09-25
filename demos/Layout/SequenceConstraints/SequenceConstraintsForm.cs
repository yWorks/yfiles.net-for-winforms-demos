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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Demo.yFiles.Layout.SequenceConstraints.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout.Hierarchic;

[assembly: XmlnsDefinition("http://www.yworks.com/yfilesNET/5.0/demos/SequenceConstraintsWindow", "Demo.yFiles.Layout.SequenceConstraints")]
[assembly: XmlnsPrefix("http://www.yworks.com/yfilesNET/5.0/demos/SequenceConstraintsWindow", "demo")]

namespace Demo.yFiles.Layout.SequenceConstraints
{
  /// <summary>
  /// This demo shows how to use sequence constraints with the <see cref="HierarchicLayout"/> to
  /// restrict the node sequencing.
  /// </summary>
  public partial class SequenceConstraintsForm : Form
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SequenceConstraintsForm"/> class.
    /// </summary>
    public SequenceConstraintsForm() {
      InitializeComponent();
      RegisterToolStripCommands();
    }

    #region Initialization

    /// <summary>
    /// The default style
    /// </summary>
    private readonly INodeStyle defaultStyle = new ConstraintsNodeStyle();

    /// <summary>
    /// Used to create new <see cref="SequenceConstraintsInfo"/> objects with random weight that are randomly enabled/disabled.
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

      graph.EdgeDefaults.Labels.LayoutParameter = new EdgeSegmentLabelModel().CreateDefaultParameter();

      // create a simple label style
      DefaultLabelStyle labelStyle = new DefaultLabelStyle
      {
        BackgroundBrush = Brushes.White,
        AutoFlip = true
      };

      // set the style as the default for all new node labels
      graph.NodeDefaults.Labels.Style = labelStyle;

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
      var mode = new GraphEditorInputMode {
        NodeCreator = CreateNodeCallback,
        // disable node resizing
        ShowHandleItems = GraphItemTypes.Bend | GraphItemTypes.Edge,
        AllowEditLabelOnDoubleClick = false
      };

	    // register command bindings
	    mode.KeyboardInputMode.AddCommandBinding(DecreaseSequenceCommand, DecreaseSequenceExecuted, CanExecuteDecreaseSequence);
	    mode.KeyboardInputMode.AddCommandBinding(IncreaseSequenceCommand, IncreaseSequenceExecuted, CanExecuteIncreaseSequence);
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
      // create a new node
      var node = graph.CreateNode(newBounds);
      // set the node tag to a new random sequence constraints
      node.Tag = new SequenceConstraintsInfo {
        Value = rand.Next(0, 7),
        Constraints = rand.NextDouble() < 0.9
      };
      return node;
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
    public static readonly ICommand IncreaseSequenceCommand = Commands.CreateCommand("Move forward in sequence");

    /// <summary>
    /// The command that can be used by the buttons to toggle the enabled state of the node.
    /// </summary>
    /// <remarks>
    /// This command requires the corresponding <see cref="INode"/> as the <see cref="ExecutedCommandEventArgs.Parameter"/>.
    /// </remarks>
    public static readonly ICommand DecreaseSequenceCommand = Commands.CreateCommand("Move backward in sequence");

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
        var data = (SequenceConstraintsInfo)node.Tag;
        data.ToggleState();
      }
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="IncreaseSequenceCommand"/> can be executed.
    /// </summary>
    private void CanExecuteIncreaseSequence(object sender, CanExecuteCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (SequenceConstraintsInfo)node.Tag;
        e.CanExecute = data.CanIncreaseValue();
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Helper method that determines whether the <see cref="DecreaseSequenceCommand"/> can be executed.
    /// </summary>
    private void CanExecuteDecreaseSequence(object sender, CanExecuteCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (SequenceConstraintsInfo)node.Tag;
        e.CanExecute = data.CanDecreaseValue();
      } else {
        e.CanExecute = false;
      }
      e.Handled = true;
    }

    /// <summary>
    /// Handler for the <see cref="IncreaseSequenceCommand"/>
    /// </summary>
    private void IncreaseSequenceExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (SequenceConstraintsInfo)node.Tag;
        data.IncreaseValue();
      }
    }

    /// <summary>
    /// Handler for the <see cref="DecreaseSequenceCommand"/>
    /// </summary>
    private void DecreaseSequenceExecuted(object sender, ExecutedCommandEventArgs e) {
      var node = e.Parameter as INode;
      if (node != null) {
        var data = (SequenceConstraintsInfo)node.Tag;
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

    #endregion

    #region Graph creation and layout

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

    /// <summary>
    /// Performs the layout operation after applying all required constraints
    /// </summary>
    private async Task DoLayout() {
      // layout starting, disable button
      runButton.Enabled = false;

      // we want to use the hl
      var hl = new HierarchicLayout() { OrthogonalRouting = true };

      // we only configure sequence constraints, so we can just use a new SequenceConstraintData
      // if HierarchicLayout should be configured further, we could also use HierarchicLayoutData.SequenceConstraintData
      var scData = new SequenceConstraintData {
        // we provide the item Values directly as IComparable instead of using the Add* methods on SequenceConstraintData
        ItemComparables = {
          Delegate = item => {
            // get the constraints info for the item
            // Note that 'item' can be an INode or IEdge but we only use SequenceConstraintsInfo for nodes
            var data = (item).Tag as SequenceConstraintsInfo;
            if (data != null && data.Constraints) {
              // the item shall be constrained so we use its Value as comparable
              return data.Value;
            }
            // otherwise we don't add constraints for the item
            return null;
          }
        }
      };

      // additionally enforce all nodes with a SequenceConstraintInfo.Value of 0 or 7 to be placed at head/tail
      foreach (var node in graphControl.Graph.Nodes) {
        var data = node.Tag as SequenceConstraintsInfo;
        if (data != null && data.Constraints) {
          if (data.Value == 0) {
            // add constraint to put this node at the head 
            scData.PlaceAtHead(node);
          } else if (data.Value == 7) {
            // add constraint to put this node at the tail
            scData.PlaceAtTail(node);
          }
        }
      }

      // do the layout
      await graphControl.MorphLayout(hl, TimeSpan.FromSeconds(1), scData);

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
      sequenceConstraintsForm = new SequenceConstraintsForm();
      Application.Run(sequenceConstraintsForm);
    }

    private static SequenceConstraintsForm sequenceConstraintsForm;

    /// <summary>
    /// Helper method to find the graph control that is bound to the currently running application from a static context
    /// </summary>
    /// <returns></returns>
    internal static GraphControl GetActiveGraphControl() {
      if (sequenceConstraintsForm != null) {
        return sequenceConstraintsForm.graphControl;
      }
      return null;
    }

    #endregion
  }
}
