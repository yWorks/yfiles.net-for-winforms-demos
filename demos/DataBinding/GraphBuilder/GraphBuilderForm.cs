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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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
  /// Interaction logic for GraphBuilderForm
  /// </summary>
  public partial class GraphBuilderForm
  {
    public GraphBuilderForm() {
      InitializeComponent();
      RegisterToolStripCommands();
      // Load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    private void RegisterToolStripCommands() {
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    private void GraphBuilderWindow_OnLoaded(object sender, EventArgs e) {
      // create the graph
      var dataProvider = XDocument.Load("Resources/model.xml");
      var graphBuilder = CreateOrganizationBuilder(dataProvider);
      var newGraph = graphBuilder.Graph;

      // add some insets to group nodes
      newGraph.GetDecorator().NodeDecorator.InsetsProviderDecorator.SetImplementation(newGraph.IsGroupNode, new GroupNodeInsetsProvider());

      graphControl.Graph = newGraph;

      // Perform an animated layout of the organization chart graph when the window is loaded.
      graphControl.MorphLayout(new HierarchicLayout
      {
          EdgeLayoutDescriptor = new EdgeLayoutDescriptor { MinimumLength = 50 },
          LayoutOrientation = LayoutOrientation.TopToBottom,
      }, TimeSpan.FromSeconds(2));
    }

    #region graphbuilder
    /// <summary>
    /// Create a GraphBuilder which uses an enumerable as source.
    /// </summary>
    /// <param name="dataProvider">The XML to create the data from.</param>
    /// <returns>A configured graph builder</returns>
    private yWorks.Graph.DataBinding.GraphBuilder CreateOrganizationBuilder(XDocument dataProvider) {
      // extract the data into enumerables.
      var employees = dataProvider.Descendants().Where(p => p.Name.LocalName == "employee");
      var businessunits = dataProvider.Descendants().Where(p => p.Name.LocalName == "businessunit");

      // create the GraphBuilder to configure
      var graphBuilder = new yWorks.Graph.DataBinding.GraphBuilder();

      // configure tne nodes source to use the employees enumerable
      var nodesSource = graphBuilder.CreateNodesSource(employees);
      // group by business units
      nodesSource.ParentIdProvider = employee => employee.Attribute("businessUnit").Value;
      var nodeBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, 1), Color.FromArgb(255, 165, 0), Color.FromArgb(255, 237, 204));
      // choose the node size so that the labels fit
      nodesSource.NodeCreator.LayoutProvider = element => {
        var width = 7 * Math.Max(element.Attribute("name").Value.Length, element.Attribute("position").Value.Length);
        return new RectD(0, 0, width, 40);
      };
      nodesSource.NodeCreator.Defaults.Style = new ShapeNodeStyle {
          Pen = Pens.DarkOrange,
          Brush = nodeBrush,
          Shape = ShapeNodeShape.RoundRectangle
      };
      // take the name attribute as node name
      var nodeNameLabels = nodesSource.NodeCreator.CreateLabelBinding(element => element.Attribute("name").Value);
      nodeNameLabels.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(0, 0, 0, 10)}.CreateParameter(InteriorStretchLabelModel.Position.Center);
      var nodePositionLabels = nodesSource.NodeCreator.CreateLabelBinding(element => element.Attribute("position").Value);
      nodePositionLabels.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(0, 10, 0, -5)}.CreateParameter(InteriorStretchLabelModel.Position.Center);
      
      // create the group nodes from the business unit's enumerable
      var groupNodesSource = graphBuilder.CreateGroupNodesSource(businessunits, (businessunit) => businessunit.Attribute("name").Value);
      groupNodesSource.ParentIdProvider = businessUnit => {
        var parentUnit = (businessUnit.Parent);
        if (parentUnit.Name.LocalName.Equals("businessunit")) {
          return businessUnit.Parent.Attribute("name").Value;
        }
        return null;
      };
      groupNodesSource.NodeCreator.Defaults.Size = new SizeD(50, 50);
      var groupNodeBrush = new LinearGradientBrush(new PointF(0.5f, 0), new PointF(0.5f, 1), Color.FromArgb(225, 242, 253), Color.LightSkyBlue);
      groupNodesSource.NodeCreator.Defaults.Style = new ShapeNodeStyle() {
          Pen = Pens.LightSkyBlue,
          Brush = groupNodeBrush
      };
      var groupLabels = groupNodesSource.NodeCreator.CreateLabelBinding(element => element.Attribute("name").Value);
      groupLabels.Defaults.Style = new DefaultLabelStyle() {
          TextBrush = Brushes.DarkGray,
          Font = new Font("Arial", 24)
      };
      groupLabels.Defaults.LayoutParameter = InteriorLabelModel.NorthWest;

      // create the edges from an element's parent XML node to the element itself
      var edgesSource = graphBuilder.CreateEdgesSource(employees, element => element.Parent, element => element);
      edgesSource.EdgeCreator.Defaults.Style = new PolylineEdgeStyle() {SmoothingLength = 20};
      var edgeLabels = edgesSource.EdgeCreator.CreateLabelBinding(element => element.Attribute("position").Value);
      edgeLabels.Defaults.Style = new DefaultLabelStyle() {
        BackgroundBrush = new SolidBrush(Color.FromArgb(225, 242, 253)),
        BackgroundPen = Pens.LightSkyBlue,
          Insets = new InsetsD(2),
          Font = new Font("Arial", 8)
      };
      edgeLabels.Defaults.LayoutParameter = new EdgePathLabelModel() { AutoRotation = false}.CreateDefaultParameter();

      graphBuilder.BuildGraph();
      return graphBuilder;
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


  sealed class GroupNodeInsetsProvider : INodeInsetsProvider {
    public InsetsD GetInsets(INode node) {
      return new InsetsD(5, 20, 5, 5);
    }
  }
}
