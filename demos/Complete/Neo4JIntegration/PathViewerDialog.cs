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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Neo4j.Driver;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.DataBinding;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.Layout.Hierarchic;
using INode = yWorks.Graph.INode;
using INeo4jNode = Neo4j.Driver.INode;

namespace Neo4JIntegration
{
  /// <summary>
  /// The dialog which displays the result of the shortest path.
  /// </summary>
  public partial class PathViewerDialog : Form
  {
    private readonly List<INeo4jNode> nodes;
    private readonly List<IRelationship> edges;

    /// <summary>
    /// After the dialog has been loaded: create the graph.
    /// </summary>
    private async void OnLoaded(object source, EventArgs args) {
      var (graphBuilder, _, _) = Neo4JIntegrationDemo.CreateGraphBuilder(graphControl, graphControl.Graph, nodes, edges);

      // Build the graph
      graphBuilder.BuildGraph();

      graphControl.Center = PointD.Origin;

      // Layout the graph
      await graphControl.MorphLayout(new HierarchicLayout
      {
        IntegratedEdgeLabeling = true
      }, TimeSpan.FromSeconds(0.5), null);
    }

    /// <summary>
    /// Creates a new instance which displays a graph represented by the given nodes and edges.
    /// </summary>
    public PathViewerDialog(List<INeo4jNode> nodes, List<IRelationship> edges) {
      InitializeComponent();
      this.Load += OnLoaded;
      this.nodes = nodes;
      this.edges = edges;
    }

    private void graphControl_Resize(object sender, EventArgs e) {
        graphControl.FitGraphBounds();
    }
  }
}
