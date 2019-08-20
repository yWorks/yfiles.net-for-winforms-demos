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
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Layout;
using yWorks.Layout.Circular;
using yWorks.Layout.Hierarchic;
using yWorks.Layout.Organic;
using yWorks.Layout.Orthogonal;
using yWorks.Layout.Router;
using yWorks.Layout.Router.Polyline;
using yWorks.Layout.Tree;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using Demo.yFiles.Layout.BasicLayout.Properties;

namespace Demo.yFiles.Layout.BasicLayout
{

  ///<summary>
  /// This class shows how to use important layout and routing algorithms
  /// to calculate coordinates for a graph. 
  /// Featured layout and routing styles are Organic, Orthogonal (Router), 
  /// Orthogonal (Layout), Circular and Hierarchical.
  ///</summary>
  public partial class BasicLayoutDemo : Form
  {
    private readonly Dictionary<string, ILayoutAlgorithm> layouts = new Dictionary<string, ILayoutAlgorithm>();

    public Dictionary<string, ILayoutAlgorithm> Layouts {
      get { return layouts; }
    }

    private ILayoutAlgorithm currentLayout;

    public ILayoutAlgorithm CurrentLayout {
      get { return currentLayout; }
      set {
        currentLayout = value;
        if (currentLayout != null) {
          ApplyLayout();
        }
      }
    }

    private async void ApplyLayout() {
      // launch the layout in a separate thread and animate the result
      try {
        await graphControl.MorphLayout(currentLayout, TimeSpan.FromSeconds(1));
      } catch (Exception e) {
        MessageBox.Show(this, "Layout did not complete successfully.\n" + e.Message);
      }
    }

    public BasicLayoutDemo() {
      InitializeComponent();
      try {
        description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      } catch (MissingMethodException) {
        // Workaround for https://github.com/microsoft/msbuild/issues/4581
        description.Text = "The description is not available with this version of .NET Core.";
      }
      RegisterToolStripCommands();
    }

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      loadGraphMLButton.SetCommand(Commands.Open, graphControl);
      saveGraphMLButton.SetCommand(Commands.SaveAs, graphControl);
      printButton.SetCommand(Commands.Print, graphControl);

      cutButton.SetCommand(Commands.Cut, graphControl);
      copyButton.SetCommand(Commands.Copy, graphControl);
      pasteButton.SetCommand(Commands.Paste, graphControl);
      deleteButton.SetCommand(Commands.Delete, graphControl);

      undoButton.SetCommand(Commands.Undo, graphControl);
      redoButton.SetCommand(Commands.Redo, graphControl);

      groupButton.SetCommand(Commands.GroupSelection, graphControl);
      ungroupButton.SetCommand(Commands.UngroupSelection, graphControl);
    }

    private void SetupLayouts() {
      //using hierarchical layout style
      HierarchicLayout hierarchicLayout = new HierarchicLayout();
      hierarchicLayout.EdgeLayoutDescriptor.RoutingStyle = new yWorks.Layout.Hierarchic.RoutingStyle(
        yWorks.Layout.Hierarchic.EdgeRoutingStyle.Orthogonal);

      currentLayout = hierarchicLayout;
      layouts.Add("Hierarchic", hierarchicLayout);

      //using organic layout style
      OrganicLayout organic = new OrganicLayout
                                       {
                                         QualityTimeRatio = 1.0,
                                         NodeOverlapsAllowed = false,
                                         NodeEdgeOverlapAvoided = true,
                                         MinimumNodeDistance = 10,
                                         PreferredEdgeLength = 50,
                                       };
      layouts.Add("Organic", organic);

      //using orthogonal layout style
      OrthogonalLayout orthogonal = new OrthogonalLayout
                                        {
                                          GridSpacing = 15,
                                          OptimizePerceivedBends = true
                                        };
      layouts.Add("Orthogonal", orthogonal);

      //using circular layout style
      CircularLayout circular = new CircularLayout();
      circular.BalloonLayout.MinimumEdgeLength = 50;
      circular.BalloonLayout.CompactnessFactor = 0.1;
      layouts.Add("Circular", circular);

      // a tree layout algorithm
      TreeLayout treeLayout = new TreeLayout { ConsiderNodeLabels = true };
      treeLayout.AppendStage(new TreeReductionStage()
      {
        NonTreeEdgeRouter = new OrganicEdgeRouter(),
        NonTreeEdgeSelectionKey = OrganicEdgeRouter.AffectedEdgesDpKey,
      });
      layouts.Add("Tree", treeLayout);

      //using Polyline Router
      var polylineRouter = new EdgeRouter
      {
        Grid = new Grid(0, 0, 10),
        PolylineRouting = true,
        Rerouting = true
      };
      polylineRouter.DefaultEdgeLayoutDescriptor.PenaltySettings.BendPenalty = 3;
      polylineRouter.DefaultEdgeLayoutDescriptor.PenaltySettings.EdgeCrossingPenalty = 5;
      layouts.Add("Polyline Edge Router", polylineRouter);
    }

    private void Demo_Load(object sender, EventArgs e) {
      SetupLayouts();
      layoutComboBox.Items.AddRange(layouts.Keys.ToArray());
      InitializeGraph();
      InitializeInputModes();

      layoutComboBox.SelectedIndex = 0;
    }

    /// <summary>
    /// Creates a small graph and various layouts to it.
    /// </summary>
    private void InitializeGraph() {
      IGraph graph = graphControl.Graph;

      graphControl.FileOperationsEnabled = true;
      graphControl.NavigationCommandsEnabled = true;
      //Create graph
      graph.NodeDefaults.Size = new SizeD(50, 30);

      // set the style as the default for all new nodes
      graph.NodeDefaults.Style = graph.NodeDefaults.Style = new BevelNodeStyle
      {
        Color = Color.FromArgb(0xFF, 0xFF, 0x8C, 0x00),
        Inset = 2,
        Radius = 5,
      };

      // set the style as the default for all new edges
      graph.EdgeDefaults.Style = new PolylineEdgeStyle {TargetArrow = Arrows.Default};

      // set the style as the default for all new node labels
      graph.NodeDefaults.Labels.Style = new DefaultLabelStyle();
      PanelNodeStyle groupNodeStyle = new PanelNodeStyle
      {
        Color = Color.FromArgb(255, 135, 206, 250),
        LabelInsetsColor = Color.FromArgb(255, 173, 216, 230),
        Insets = new InsetsD(10, 10, 10, 20)
      };
      graph.GroupNodeDefaults.Style = groupNodeStyle;

      ReadSampleGraph();

      // enable undo...
      graph.SetUndoEngineEnabled(true);
    }

    private void InitializeInputModes() {
      graphControl.InputMode = new GraphEditorInputMode {AllowGroupingOperations = true};
    }

    private void ReadSampleGraph() {
      graphControl.ImportFromGraphML("Resources\\sample-flat.graphml");
      int i = 0;
      foreach (var node in graphControl.Graph.Nodes) {
        graphControl.Graph.AddLabel(node, (++i).ToString());
      }
    }

    private void layoutComboBox_SelectedValueChanged(object sender, EventArgs e) {
      CurrentLayout = layouts[(string)layoutComboBox.SelectedItem];
    }

    private void OnRunButtonClicked(object sender, EventArgs e) {
      CurrentLayout = layouts[(string)layoutComboBox.SelectedItem];
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new BasicLayoutDemo());
    }
  }
}
