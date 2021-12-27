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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Demo.yFiles.DataBinding.AdjacencyGraphBuilder.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.DataBinding;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout;
using yWorks.Layout.Hierarchic;

namespace Demo.yFiles.DataBinding.AdjacencyGraphBuilder
{
  /// <summary>
  /// Interaction logic for AdjacencyGraphBuilderForm
  /// </summary>
  public partial class AdjacencyGraphBuilderForm : Form
  {
    private static readonly ILabelModelParameter edgeLabelLayoutParameter = FreeEdgeLabelModel.Instance.CreateDefaultParameter();

    public AdjacencyGraphBuilderForm() {
      InitializeComponent();
      RegisterToolStripCommands();
      // Load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    private void AdjacencyGraphBuilderWindow_OnLoaded(object sender, EventArgs e) {
      graphControl.InputMode = new MoveViewportInputMode();
      graphSourceComboBox.SelectedIndex = 0;
    }
    private void RegisterToolStripCommands() {
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitContent, graphControl);
    }

    private void AdjacencyGraphBuilderModelChanged(object sender, EventArgs e) {
      var newGraph = GetGraphBuilder(graphSourceComboBox.SelectedIndex).Graph;

      // add some insets to group nodes
      newGraph.GetDecorator().NodeDecorator.InsetsProviderDecorator.SetImplementation(newGraph.IsGroupNode, new GroupNodeInsetsProvider());

      graphControl.Graph = newGraph;

      // Perform an animated layout of the organization chart graph when the window is loaded.
      graphControl.MorphLayout(new HierarchicLayout {
          EdgeLayoutDescriptor = new EdgeLayoutDescriptor {MinimumLength = 50},
          LayoutOrientation =
              graphSourceComboBox.SelectedIndex <= 3 ? LayoutOrientation.TopToBottom : LayoutOrientation.BottomToTop,
          IntegratedEdgeLabeling = true
      }, TimeSpan.FromSeconds(2));
    }
    
    private yWorks.Graph.DataBinding.AdjacencyGraphBuilder GetGraphBuilder(int index) {
      var configurationName = graphSourceComboBox.Items[index].ToString();
      var dataProvider = XDocument.Load("Resources/model.xml");

      if ("Organization with Predecessor"== configurationName) {
        return CreateOrganizationBuilder(dataProvider, false, false);
      } else if ("Organization with Successors" == configurationName) {
        return CreateOrganizationBuilder(dataProvider, true, false);      
      } else if ("Organization with Predecessor Id" == configurationName) {
        return CreateOrganizationBuilder(dataProvider, false, true);
      } else if ("Organization with Successors Ids" == configurationName) {
        return CreateOrganizationBuilder(dataProvider, true, true);      
      }
      return null;
    }

    /// <summary>
    /// Extract Data provider from the given XML and create and configure a graph builder.
    /// </summary>
    /// <param name="xmlDoc">The XML.</param>
    /// <param name="useSuccessor"></param>
    /// <param name="useIds"></param>
    /// <returns></returns>
    private yWorks.Graph.DataBinding.AdjacencyGraphBuilder CreateOrganizationBuilder(XDocument xmlDoc, bool useSuccessor, bool useIds) {
      // extract employees, positions, and business units as enumerables
      var employees = xmlDoc.Descendants().Where(p => p.Name.LocalName == "employee");
      var positions = employees.Select(employee => employee.Attributes("position").First().Value).Distinct();
      var businessunits = xmlDoc.Descendants().Where(p => p.Name.LocalName == "businessunit");
      var adjacentNodesGraphBuilder = new yWorks.Graph.DataBinding.AdjacencyGraphBuilder();

      // first node collection: employees

      // create a nodes source which creates nodes from the given employees
      var nodesSource = adjacentNodesGraphBuilder.CreateNodesSource(employees);
      // nodes are grouped in business units
      nodesSource.ParentIdProvider = employee => {
        return employee.Attributes("businessUnit").First().Value;
      };

      var nodeBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(0, 1), Color.FromArgb(255, 165, 0), Color.FromArgb(255, 237, 204));
      // adjust the size so the node labels fit
      nodesSource.NodeCreator.LayoutProvider = element => {
        var width = 5 + 7 * Math.Max(element.Attributes("name").First().Value.Length, element.Attributes("position").First().Value.Length);
        return new RectD(0, 0, width, 40);
      };
      nodesSource.NodeCreator.Defaults.Style = new ShapeNodeStyle() {
          Pen = Pens.DarkOrange,
          Brush = nodeBrush,
          Shape = ShapeNodeShape.RoundRectangle
      };
      // set label provider
      var nodeNameLabels = nodesSource.NodeCreator.CreateLabelBinding(element => element.Attributes("name").First().Value);
      nodeNameLabels.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(5, 5, 5, 10)}.CreateParameter(InteriorStretchLabelModel.Position.Center);
      var nodePositionLabels = nodesSource.NodeCreator.CreateLabelBinding(element => element.Attributes("position").First().Value);
      nodePositionLabels.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(5, 20, 5, 5)}.CreateParameter(InteriorStretchLabelModel.Position.Center);

      // second nodes collections: positions

      // create nodes source for positions with different style and size
      var positionsSource = adjacentNodesGraphBuilder.CreateNodesSource(positions);
      positionsSource.NodeCreator.Defaults.Size = new SizeD(100, 60);
      positionsSource.NodeCreator.Defaults.Style = new ShapeNodeStyle( ) {
          Pen = Pens.SeaGreen,
          Brush = Brushes.PaleGreen,
          Shape = ShapeNodeShape.RoundRectangle
      };
      var positionLabelCreator = positionsSource.NodeCreator.CreateLabelBinding(position => position.ToString());
      positionLabelCreator.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(5)}.CreateParameter(InteriorStretchLabelModel.Position.Center);

      // group node collections: business units

      var groupNodesSource = adjacentNodesGraphBuilder.CreateGroupNodesSource(businessunits, (businessunit) => businessunit.Attribute("name").Value);
      groupNodesSource.ParentIdProvider = businessUnit => {
        var parentUnit = (businessUnit.Parent);
        if (parentUnit.Name.LocalName.Equals("businessunit")) {
          return businessUnit.Parent.Attribute("name").Value;
        }
        return null;
      };
      groupNodesSource.NodeCreator.Defaults.Size = new SizeD(50, 50);

      var groupNodeBrush =  new LinearGradientBrush(new PointF(0.5f, 0), new PointF(0.5f, 1), Color.FromArgb(225, 242, 253), Color.LightSkyBlue);
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

      // prepare edge creation
      EdgeCreator<XElement> edgeCreator = new EdgeCreator<XElement> {
          Defaults = { Style = new PolylineEdgeStyle() { SmoothingLength = 20, TargetArrow = Arrows.Default } }
      };
      var edgeLabels = edgeCreator.CreateLabelBinding(element => element.Attributes("name").First().Value);
      edgeLabels.Defaults.Style = new DefaultLabelStyle() {
          BackgroundBrush = new SolidBrush(Color.FromArgb(225,242,253)),
          BackgroundPen = Pens.LightSkyBlue,
          Insets = new InsetsD(2),
          Font = new Font("Arial", 8)
      };
      edgeLabels.Defaults.LayoutParameter = edgeLabelLayoutParameter; 

      // configure the successor and predecessor sources
      // for this demo this depends on the chosen settings
      // we configure either successors or predecessors and choose whether we use IDs or the elements themselves to resolve the references
      if (useIds) {
        if (useSuccessor) {
          nodesSource.AddSuccessorIds(element => element.Elements(), edgeCreator);
        } else {
          nodesSource.AddPredecessorIds(element => {
            var parentElement = element.Parent;
            return parentElement != null && parentElement.Name.LocalName.Equals("employee")? new []{parentElement} : null;
          },edgeCreator);
        }
      } else {
        if (useSuccessor) {
          nodesSource.AddSuccessorsSource(element => element.Elements(), nodesSource, edgeCreator);
        } else {
          nodesSource.AddPredecessorsSource<XElement>(element => {
            var parentElement = element.Parent;
            return parentElement != null && parentElement.Name.LocalName.Equals("employee")? new []{parentElement} : null;
          }, nodesSource, edgeCreator);
        }
      }

      // either way: we create edges between the employee and his/her position
      nodesSource.AddSuccessorIds(employee => {
        return new[] {
            employee.Attribute("position").Value
        };
      }, new EdgeCreator<XElement>());

      adjacentNodesGraphBuilder.LabelAdded += (sender, args) => {
        var newLabel = true;
      }; 
      
      adjacentNodesGraphBuilder.BuildGraph();
      return adjacentNodesGraphBuilder;
    }
    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new AdjacencyGraphBuilderForm());
    }

    #endregion
  }
  sealed class GroupNodeInsetsProvider : INodeInsetsProvider {
    public InsetsD GetInsets(INode node) {
      return new InsetsD(5, 20, 5, 5);
    }
  }

}
