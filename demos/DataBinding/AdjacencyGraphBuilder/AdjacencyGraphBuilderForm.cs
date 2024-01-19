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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Demo.yFiles.DataBinding.AdjacencyGraphBuilder.Properties;
using Demo.yFiles.Toolkit;
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
    public AdjacencyGraphBuilderForm() {
      InitializeComponent();
      InitializeGraphDefaults();
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

    private async void AdjacencyGraphBuilderModelChanged(object sender, EventArgs e) {
      BuildGraph(graphSourceComboBox.SelectedIndex);

      // Perform an animated layout of the organization chart graph when the window is loaded.
      await graphControl.MorphLayout(new HierarchicLayout {
          EdgeLayoutDescriptor = new EdgeLayoutDescriptor {MinimumLength = 50},
          LayoutOrientation =
              graphSourceComboBox.SelectedIndex <= 3 ? LayoutOrientation.TopToBottom : LayoutOrientation.BottomToTop,
          IntegratedEdgeLabeling = true
      }, TimeSpan.FromSeconds(2));
    }

    private void BuildGraph(int index) {
      var configurationName = graphSourceComboBox.Items[index].ToString();
      var dataProvider = XDocument.Load("Resources/model.xml");

      if ("Organization with Predecessor"== configurationName) {
        BuildOrganization(dataProvider, false, false);
      } else if ("Organization with Successors" == configurationName) {
        BuildOrganization(dataProvider, true, false);
      } else if ("Organization with Predecessor Id" == configurationName) {
        BuildOrganization(dataProvider, false, true);
      } else if ("Organization with Successors Ids" == configurationName) {
        BuildOrganization(dataProvider, true, true);
      }
    }

    /// <summary>
    /// Extract Data provider from the given XML and create and configure a graph builder and build the graph.
    /// </summary>
    /// <param name="xmlDoc">The XML.</param>
    /// <param name="useSuccessor"></param>
    /// <param name="useIds"></param>
    private void BuildOrganization(XDocument xmlDoc, bool useSuccessor, bool useIds) {
      // extract employees, positions, and business units as enumerables
      var employees = xmlDoc.Descendants().Where(p => p.Name.LocalName == "employee");
      var positions = employees.Select(employee => employee.Attributes("position").First().Value).Distinct();
      var businessunits = xmlDoc.Descendants().Where(p => p.Name.LocalName == "businessunit");
      graphControl.Graph.Clear();
      var adjacentNodesGraphBuilder = new yWorks.Graph.DataBinding.AdjacencyGraphBuilder(graphControl.Graph);

      // first node collection: employees

      // create a nodes source which creates nodes from the given employees
      var nodesSource = adjacentNodesGraphBuilder.CreateNodesSource(employees);
      // nodes are grouped in business units
      nodesSource.ParentIdProvider = employee => {
        return employee.Attributes("businessUnit").First().Value;
      };
      // adjust the size so the node labels fit
      nodesSource.NodeCreator.LayoutProvider = element => {
        var width = 5 + 7 * Math.Max(element.Attributes("name").First().Value.Length, element.Attributes("position").First().Value.Length);
        return new RectD(0, 0, width, 40);
      };

      // set label provider
      var nodeNameLabels = nodesSource.NodeCreator.CreateLabelBinding(element => element.Attributes("name").First().Value);
      nodeNameLabels.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(5, 5, 5, 5)}.CreateParameter(InteriorStretchLabelModel.Position.Center);
      var nodeNameLabelStyle = DemoStyles.CreateDemoNodeLabelStyle();
      nodeNameLabelStyle.Insets = InsetsD.Empty;
      nodeNameLabelStyle.StringFormat = new StringFormat {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Near
      };
      nodeNameLabels.Defaults.Style = nodeNameLabelStyle;
      var nodePositionLabels = nodesSource.NodeCreator.CreateLabelBinding(element => element.Attributes("position").First().Value);
      nodePositionLabels.Defaults.LayoutParameter = new InteriorStretchLabelModel() {Insets = new InsetsD(5, 20, 5, 5)}.CreateParameter(InteriorStretchLabelModel.Position.Center);

      // second nodes collections: positions

      // create nodes source for positions with different style and size
      var positionsSource = adjacentNodesGraphBuilder.CreateNodesSource(positions);
      positionsSource.NodeCreator.Defaults.Size = new SizeD(100, 60);
      positionsSource.NodeCreator.Defaults.Style = DemoStyles.CreateDemoNodeStyle(Themes.PaletteGreen);
      var positionLabelCreator = positionsSource.NodeCreator.CreateLabelBinding(position => position.ToString());
      positionLabelCreator.Defaults.Style = DemoStyles.CreateDemoNodeLabelStyle(Themes.PaletteGreen);
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
      var groupLabels = groupNodesSource.NodeCreator.CreateLabelBinding(element => element.Attribute("name").Value);
      groupLabels.Defaults.LayoutParameter = InteriorLabelModel.NorthWest;

      // prepare edge creation
      EdgeCreator<XElement> edgeCreator = new EdgeCreator<XElement> { Defaults = graphControl.Graph.EdgeDefaults };
      var edgeLabels = edgeCreator.CreateLabelBinding(element => element.Attributes("name").First().Value);
      edgeLabels.Defaults = graphControl.Graph.EdgeDefaults.Labels;

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
      nodesSource.AddSuccessorIds(employee => new[] { employee.Attribute("position").Value },
          new EdgeCreator<XElement> { Defaults = graphControl.Graph.EdgeDefaults });

      adjacentNodesGraphBuilder.BuildGraph();
    }

    private void InitializeGraphDefaults() {
      var graph = graphControl.Graph;
      
      // initialize demo styles
      DemoStyles.InitDemoStyles(graph);
      // remove insets of demo node label styles 
      ((DefaultLabelStyle) graph.NodeDefaults.Labels.Style).Insets = InsetsD.Empty;
      // set insets and bigger text size for demo group node label styles 
      var groupLabelStyle = (DefaultLabelStyle) graph.GroupNodeDefaults.Labels.Style;
      groupLabelStyle.Insets = new InsetsD(2);
      groupLabelStyle.Font = new Font(groupLabelStyle.Font.FontFamily, 18);
      // increase tab height of GroupNodeStyle so the increased group node labels fit into the header
      ((GroupNodeStyle) graph.GroupNodeDefaults.Style).TabHeight = 28;
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
}
