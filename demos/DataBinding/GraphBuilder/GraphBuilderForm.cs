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
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Demo.yFiles.DataBinding.GraphBuilder.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Hierarchic;

namespace Demo.yFiles.DataBinding.GraphBuilder
{
  /// <summary>
  /// Shows how to augment <see cref="IClickHandler"/> with additional functionality.
  /// </summary>
  /// <remarks>
  /// See the description.rtf file or run the application to find out about what this application demonstrates.
  /// </remarks>
  public partial class GraphBuilderForm : Form
  {
    #region Initialization

    public GraphBuilderForm() {
      InitializeComponent();
      RegisterToolStripCommands();
      exampleComboBox.DataSource = new[] {"Organization", "Classes"};
      exampleComboBox.SelectedIndex = -1;
      exampleComboBox.SelectedIndexChanged += new System.EventHandler(this.ExampleChanged);

      // Initialize input mode
      graphControl.InputMode = CreateInputMode();

      InitializeGraphDefaults();

      // Load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    #region Command registration

    private void RegisterToolStripCommands() {
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    #endregion

    private void GraphBuilderForm_Load(object sender, EventArgs e) {
      // change the combo box's index to trigger initial graph loading
      exampleComboBox.SelectedIndex = 1;
    }

    /// <summary>
    /// Creates a viewer input mode.
    /// </summary>
    private static IInputMode CreateInputMode() {
      var gvim = new GraphViewerInputMode {
        SelectableItems = GraphItemTypes.None,
        FocusableItems = GraphItemTypes.None
      };
      return gvim;
    }

    #endregion

    #region Graph Creation

    /// <summary>
    /// Sets default styles for the graph.
    /// </summary>
    public void InitializeGraphDefaults() {
      var graph = graphControl.Graph;
      graph.NodeDefaults.Style = new ShapeNodeStyle {
        Shape = ShapeNodeShape.RoundRectangle,
        Brush = new SolidBrush(Color.FromArgb(255, 255, 237, 204)),
        Pen = Pens.DarkOrange
      };
      graph.NodeDefaults.Labels.Style = new DefaultLabelStyle {
        Font = new Font("Arial", 13, FontStyle.Regular, GraphicsUnit.Pixel)
      };
      graph.NodeDefaults.Labels.LayoutParameter = InteriorLabelModel.Center;

      graph.GroupNodeDefaults.Style = new PanelNodeStyle {
        Color = Color.FromArgb(127, 225, 242, 253),
        LabelInsetsColor = Color.AntiqueWhite,
        Insets = new InsetsD(5,20,5,5)
      };
      graph.GroupNodeDefaults.Labels.Style = new DefaultLabelStyle {
        Font = new Font("Arial", 32, FontStyle.Bold, GraphicsUnit.Pixel),
        TextBrush = Brushes.DarkGray
      };
      graph.GroupNodeDefaults.Labels.LayoutParameter =
          FreeNodeLabelModel.Instance.CreateParameter(
            new PointD(0, 0), new PointD(5, 5), new PointD(0, 0), new PointD(0, 0), 0);

      graph.EdgeDefaults.Style = new PolylineEdgeStyle { SmoothingLength = 20 };
      graph.EdgeDefaults.Labels.Style = new DefaultLabelStyle {
        BackgroundPen = Pens.LightSkyBlue,
        BackgroundBrush = new SolidBrush(Color.FromArgb(225, 242, 253))
      };
      graph.EdgeDefaults.Labels.LayoutParameter = new EdgeSegmentLabelModel(0,0,0,false, EdgeSides.OnEdge).CreateDefaultParameter();
    }

    private void ExampleChanged(object sender, EventArgs args) {
      switch (exampleComboBox.SelectedIndex) {
        case 0:
          CreateOrganizationGraph();
          break;
        case 1:
          CreateClassesGraph();
          break;
      }
    }

    public void CreateOrganizationGraph() {
      var data = XDocument.Load("Resources/model.xml").Root;
      var graph = graphControl.Graph;

      if (data != null) {
        var builder = new TreeBuilder<XElement, XElement>(graph) {
          // Nodes, edges, and groups are obtained from XML elements in the model
          NodesSource = data.XPathSelectElements("//employee"),
          GroupsSource = data.XPathSelectElements("//businessunit"),
          // Mapping nodes to groups is done via an attribute on the employee.
          GroupProvider =
              e => data.XPathSelectElement(string.Format("//businessunit[@name='{0}']",
                e.Attribute("businessUnit").Value)),
          // Group nesting is determined by nesting of the XML elements
          ParentGroupProvider = e => e.Parent,
          // As is the hierarchy of employees
          ChildProvider = e => e.XPathSelectElements("./employee"),
          // Add descriptive labels to edges and groups. Nodes get two labels instead, which is handled in an event below.
          EdgeLabelProvider = (source, target) => target.Attribute("name").Value,
          GroupLabelProvider = e => e.Attribute("name").Value
        };

        // A label model with some space to the node's border
        var nodeLabelModel = new InteriorLabelModel { Insets = new InsetsD(5) };

        builder.NodeCreated += (sender, e) => {
          // Add employee name and position as labels
          var l1 = e.Graph.AddLabel(e.Item, e.SourceObject.Attribute("name").Value,
            nodeLabelModel.CreateParameter(InteriorLabelModel.Position.NorthWest));
          var l2 = e.Graph.AddLabel(e.Item, e.SourceObject.Attribute("position").Value,
            nodeLabelModel.CreateParameter(InteriorLabelModel.Position.SouthWest));

          // Determine optimal node size
          var bestSize = new SizeD(
            Math.Max(l1.PreferredSize.Width, l2.PreferredSize.Width) + 10,
            l1.PreferredSize.Height + l2.PreferredSize.Height + 12);
          // Set node to that size. Location is irrelevant here, since we're running a layout anyway
          e.Graph.SetNodeLayout(e.Item, new RectD(PointD.Origin, bestSize));
        };
        graph.EdgeDefaults.Style = new PolylineEdgeStyle{SmoothingLength = 20};

        builder.BuildGraph();
        graphControl.MorphLayout(new HierarchicLayout{IntegratedEdgeLabeling = true}, TimeSpan.FromSeconds(1));
        
      }
    }

    public void CreateClassesGraph() {
      var data = XDocument.Load("Resources/classesmodel.xml").Root;
      var graph = graphControl.Graph;

      if (data != null) {
        var builder = new GraphBuilder<XElement, XElement,XElement>(graph) {
          // Nodes and edges are obtained from XML elements in the model
          NodesSource = data.XPathSelectElements("//class"),
          EdgesSource = data.XPathSelectElements("//class"),
          // Nodes are grouped by their parent
          GroupProvider = e => e.Parent,
          // Group nesting is determined by nesting of the XML elements
          ParentGroupProvider = e => e.Parent,
          // edges are drawn for classes with an "extends" attribute
          // between the class itself
          SourceNodeProvider = e => e,
          // and the class which is provided by the "extends" attribute
          TargetNodeProvider = e => {
            var att = e.Attribute("extends");
            return att != null ? data.XPathSelectElement(string.Format("//class[@name='{0}']", att.Value)) : null;},
          // the node label is either ClassName, interface ClassName, or ClassName extends OtherClass
          NodeLabelProvider = e=> {
            var name = e.Attribute("name").Value;
            var isInterface = e.Attribute("type").Value == "interface";
            var extends = e.Attribute("extends");
            return (isInterface ? "interface " : "") + name + (extends != null ? " extends " + extends.Value : "");
          },
          // edge label "Source extends Target"
          EdgeLabelProvider = e => e.Attribute("name").Value + " extends " + data.XPathSelectElement(string.Format("//class[@name='{0}']", e.Attribute("extends").Value)).Attribute("name").Value
        };

        // use edges with arrowhead
        graph.EdgeDefaults.Style = new PolylineEdgeStyle{SmoothingLength = 20, TargetArrow = Arrows.Short};
        // use different label models for the groups
        graph.IsGroupNodeChanged += SetLabelParameterForGroups;

        builder.NodeCreated += (sender, e) => {
          var node = e.Item;
          var l1 = node.Labels[0];
          // Determine optimal node size
          var bestSize = new SizeD(Math.Max(node.Layout.Width, l1.PreferredSize.Width + 10), Math.Max(node.Layout.Height, l1.PreferredSize.Height + 12));
          // Set node to that size. Location is irrelevant here, since we're running a layout anyway
          e.Graph.SetNodeLayout(node, new RectD(PointD.Origin, bestSize));
        };

        builder.BuildGraph();
        graph.IsGroupNodeChanged -= SetLabelParameterForGroups;
        graphControl.MorphLayout(
            new HierarchicLayout {
              LayoutOrientation = LayoutOrientation.BottomToTop,
              IntegratedEdgeLabeling = true,
              MinimumLayerDistance = 30,
              ConsiderNodeLabels = false
            }, TimeSpan.FromSeconds(1));
      }
    }

    private void SetLabelParameterForGroups(object sender, NodeEventArgs args) {
      ((IGraph)sender).SetLabelLayoutParameter(args.Item.Labels[0], InteriorLabelModel.NorthWest);
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
      Application.Run(new GraphBuilderForm());
    }

    #endregion
  }
}
