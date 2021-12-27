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
using yWorks.Annotations;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.DataBinding;

namespace yWorks.DataBinding.Compatibility
{
  /// <summary>
  /// Support class for <see cref="GraphBuilder{TNode,TEdge,TGroup}"/> and
  /// <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/>.
  /// </summary>
  /// <remarks>
  /// Provides node and edge creators an ties the provided callbacks to them.
  ///
  /// The initialized <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}"/> serves provides factories
  /// for <see cref="NodeCreator{TDataItem}"/>s and <see cref="EdgeCreator{TDataItem}"/>s:
  /// <see cref="CreateEdgeCreator"/>, <see cref="CreateNodeCreator"/>, and <see cref="CreateGroupCreator"/>.
  /// The creators in delegate to the provided callbacks which are expected to delegate to this class's
  /// <see cref="CreateNode"/>, <see cref="UpdateNode"/>, <see cref="CreateEdge"/>, <see cref="UpdateEdge"/>,
  /// <see cref="CreateGroupNode"/>, and <see cref="UpdateGroupNode"/> methods.
  /// </remarks>
  /// <typeparam name="TNode">The type of business data which represents a node.</typeparam>
  /// <typeparam name="TGroup">The type of business data which represents a group node.</typeparam>
  /// <typeparam name="TEdge">The type of business data which represents an edge.</typeparam>
  internal sealed class GraphBuilderHelper<TNode, TGroup, TEdge>
  {
    #region Create and Update callbacks

    [CanBeNull]
    internal delegate INode CreateNodeCallback(IGraph graph, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject);

    internal delegate void UpdateNodeCallback(IGraph graph, INode node, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject);

    [CanBeNull]
    internal delegate INode CreateGroupNodeCallback(IGraph graph, [CanBeNull] object labelData, [CanBeNull] TGroup groupObject);

    internal delegate void UpdateGroupNodeCallback(IGraph graph, INode groupNode, [CanBeNull] object labelData, [CanBeNull] TGroup groupObject);

    [CanBeNull]
    internal delegate IEdge CreateEdgeCallback(IGraph graph, [CanBeNull] INode source, [CanBeNull] INode target, [CanBeNull] object labelData, [CanBeNull] TEdge edgeObject);

    internal delegate void UpdateEdgeCallback(IGraph graph, IEdge edge, [CanBeNull] object labelData, [CanBeNull] TEdge edgeObject);

    private readonly CreateNodeCallback builderCreateNode;
    private readonly UpdateNodeCallback builderUpdateNode;
    private readonly CreateGroupNodeCallback builderCreateGroupNode;
    private readonly UpdateGroupNodeCallback builderUpdateGroupNode;
    private readonly CreateEdgeCallback builderCreateEdge;
    private readonly UpdateEdgeCallback builderUpdateEdge;

    #endregion

    #region Label Provider fields

    [CanBeNull]
    internal Func<TNode, object> nodeLabelProvider;

    [CanBeNull]
    internal Func<TEdge, object> edgeLabelBinding;

    [CanBeNull]
    internal Func<TGroup, object> groupLabelProvider;

    [CanBeNull]
    internal EdgeLabelProvider<TNode> edgeLabelProvider;

    #endregion

    private readonly IGraph graph;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="graph">The graph to build the elements in.</param>
    /// <param name="createNode"></param>
    /// <param name="updateNode"></param>
    /// <param name="createGroupNode"></param>
    /// <param name="updateGroupNode"></param>
    /// <param name="createEdge"></param>
    /// <param name="updateEdge"></param>
    internal GraphBuilderHelper(
        IGraph graph,
        CreateNodeCallback createNode,
        UpdateNodeCallback updateNode,
        CreateGroupNodeCallback createGroupNode,
        UpdateGroupNodeCallback updateGroupNode,
        CreateEdgeCallback createEdge,
        UpdateEdgeCallback updateEdge) {
      this.graph = graph;
      this.builderCreateNode = createNode;
      this.builderUpdateNode = updateNode;
      this.builderCreateGroupNode = createGroupNode;
      this.builderUpdateGroupNode = updateGroupNode;
      this.builderCreateEdge = createEdge;
      this.builderUpdateEdge = updateEdge;
    }

    #region Create and Update Methods

    /// <summary>
    /// Creates an edge in the given graph.
    /// </summary>
    /// <remarks>
    /// Raises the <see cref="EdgeCreated"/> event.
    /// </remarks>
    /// <param name="graph">The graph to create the edge in.</param>
    /// <param name="source">The source node of the edge.</param>
    /// <param name="target">The target node of the edge.</param>
    /// <param name="labelData">The data object to create a label from.</param>
    /// <param name="edgeObject">The data object which is represented by the edge.</param>
    /// <returns>The newly created edge.</returns>
    [CanBeNull]
    public IEdge CreateEdge(IGraph graph, [CanBeNull] INode source, [CanBeNull] INode target,
        [CanBeNull] object labelData, [CanBeNull] TEdge edgeObject) {
      if (source == null || target == null) {
        // early exit if source or target node doesn't exist
        return null;
      }
      var edge = graph.CreateEdge(source, target, graph.EdgeDefaults.GetStyleInstance(), edgeObject);
      if (labelData != null) {
        graph.AddLabel(edge, labelData.ToString(), null, null, null, labelData);
      }
      this.OnEdgeCreated(edge, edgeObject);
      return edge;
    }

    /// <summary>
    /// Creates a group node in the given graph.
    /// </summary>
    /// <remarks>
    /// Raises the <see cref="GroupCreated"/> event.
    /// </remarks>
    /// <param name="graph">The graph to create the node in.</param>
    /// <param name="labelData">The data object to create a label from.</param>
    /// <param name="groupObject">The data object which is represented by the node.</param>
    /// <returns>The newly created edge.</returns>
    public INode CreateGroupNode(IGraph graph, [CanBeNull] object labelData, [CanBeNull] TGroup groupObject) {
      var nodeDefaults = graph.GroupNodeDefaults;
      var layout = new RectD(PointD.Origin, nodeDefaults.Size);
      var groupNode = graph.CreateGroupNode(null, layout, nodeDefaults.GetStyleInstance(), groupObject);
      if (labelData != null) {
        this.graph.AddLabel(groupNode, labelData.ToString(), null, null, null, labelData);
      }
      this.OnGroupCreated(groupNode, groupObject);
      return groupNode;
    }

    /// <summary>
    /// Creates a node in the given graph.
    /// </summary>
    /// <remarks>
    /// Raises the <see cref="NodeCreated"/> event.
    /// </remarks>
    /// <param name="graph">The graph to create the node in.</param>
    /// <param name="parent">The parent node of the node.</param>
    /// <param name="labelData">The data object to create a label from.</param>
    /// <param name="nodeObject">The data object which is represented by the node.</param>
    /// <returns>The newly created node.</returns>
    public INode CreateNode(IGraph graph, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject) {
      try {
        var node = graph.CreateNode(parent, null, graph.NodeDefaults.GetStyleInstance(), nodeObject);
        if (labelData != null) {
          this.graph.AddLabel(node, labelData.ToString(), null, null, null, labelData);
        }
        this.OnNodeCreated(node, nodeObject);
        return node;
      } catch (Exception e) {
        if (e.Message == "No node created!") {
          // This usually only happens when the GraphBuilder is used on a foldingView
          throw new InvalidOperationException(
              "Could not create node. When folding is used, make sure to use the master graph in the GraphBuilder."
          );
        }
        throw e;
      }
    }

    /// <summary>
    /// Updates an edge in the given graph.
    /// </summary>
    /// <remarks>
    /// Assumes that the edge already exists in the graph.
    /// Renews the tag and updates the edge's labels.
    /// Raises the <see cref="EdgeUpdated"/> event.
    /// </remarks>
    /// <param name="graph">The graph to create the edge in.</param>
    /// <param name="edge">The edge to update.</param>
    /// <param name="labelData">The data object to create a label from.</param>
    /// <param name="edgeObject">The data object which is represented by the edge.</param>
    public void UpdateEdge(IGraph graph, IEdge edge, [CanBeNull] object labelData, [CanBeNull] TEdge edgeObject) {
      if (edge.Tag != (object) edgeObject) {
        edge.Tag = edgeObject;
      }
      UpdateLabels(graph, graph.EdgeDefaults.Labels, edge, labelData);
      this.OnEdgeUpdated(edge, edgeObject);
    }

    /// <summary>
    /// Updates a group node in the given graph.
    /// </summary>
    /// <remarks>
    /// Assumes that the node already exists in the graph.
    /// Renews the tag and updates the node's labels.
    /// Raises the <see cref="GroupUpdated"/> event.
    /// </remarks>
    /// <param name="graph">The graph to create the node in.</param>
    /// <param name="groupNode">The node to update.</param>
    /// <param name="labelData">The data object to create a label from.</param>
    /// <param name="groupObject">The data object which is represented by the node.</param>
    public void UpdateGroupNode(IGraph graph, INode groupNode, [CanBeNull] object labelData, [CanBeNull] TGroup groupObject) {
      if (groupNode.Tag != (object) groupObject) {
        groupNode.Tag = groupObject;
      }
      UpdateLabels(graph, graph.NodeDefaults.Labels, groupNode, labelData);
      this.OnGroupUpdated(groupNode, groupObject);
    }

    /// <summary>
    /// Updates a node in the given graph.
    /// </summary>
    /// <remarks>
    /// Assumes that the node already exists in the graph.
    /// Renews the tag and updates the node's labels.
    /// Raises the <see cref="NodeUpdated"/> event.
    /// </remarks>
    /// <param name="graph">The graph to create the node in.</param>
    /// <param name="node">The node to update.</param>
    /// <param name="parent">The (new) parent of the node to update.</param>
    /// <param name="labelData">The data object to create a label from.</param>
    /// <param name="nodeObject">The data object which is represented by the node.</param>
    public void UpdateNode(IGraph graph, INode node, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject) {
      if (node.Tag != (object) nodeObject) {
        node.Tag = nodeObject;
      }
      UpdateLabels(graph, graph.NodeDefaults.Labels, node, labelData);
      if (graph.GetParent(node) != parent) {
        graph.SetParent(node, parent);
      }
      this.OnNodeUpdated(node, nodeObject);
    }

    /// <summary>
    /// Updates the labels of a given owner.
    /// </summary>
    /// <param name="graph">The graph the owner belongs to.</param>
    /// <param name="labelDefaults">The defaults to create the labels with.</param>
    /// <param name="item">The owner of the label.</param>
    /// <param name="labelData">The data to create the labels from.</param>
    private static void UpdateLabels(IGraph graph, ILabelDefaults labelDefaults, ILabelOwner item, object labelData) {
      var labels = item.Labels;
      if (labelData == null) {
        while (labels.Count > 0) {
          graph.Remove(labels[labels.Count - 1]);
        }
      } else if (labels.Count == 0) {
        var layoutParameter = labelDefaults.GetLayoutParameterInstance(item);
        var labelStyle = labelDefaults.GetStyleInstance(item);
        graph.AddLabel(item, labelData.ToString(), layoutParameter, labelStyle, null, labelData);
      } else if (labels.Count > 0) {
        var label = labels[0];
        if (label.Text != labelData.ToString()) {
          graph.SetLabelText(label, labelData.ToString());
        }
        if (label.Tag != labelData) {
          label.Tag = labelData;
        }
      }
    }

    #endregion

    #region Custom NodeCreator and EdgeCreator

    /// <summary>
    /// Returns a <see cref="NodeCreator{TDataItem}"/> which works on this instance.
    /// </summary>
    public NodeCreator<TNode> CreateNodeCreator() {
      return new GraphBuilderNodeCreator(this);
    }

    /// <summary>
    /// Returns a <see cref="NodeCreator{TDataItem}"/> which works on this instance.
    /// </summary>
    public NodeCreator<TGroup> CreateGroupCreator() {
      return new GraphBuilderGroupCreator(this);
    }

    /// <summary>
    /// Returns a <see cref="NodeCreator{TDataItem}"/> which works on this instance.
    /// </summary>
    public EdgeCreator<TEdge> CreateEdgeCreator(bool labelDataFromSourceAndTarget = false) {
      return new GraphBuilderEdgeCreator(this, labelDataFromSourceAndTarget);
    }

    /// <summary>
    /// A custom <see cref="NodeCreator{TDataItem}"/> which delegates to this instance's
    /// <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}.builderCreateNode"/> and
    /// <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}.builderUpdateNode"/> callbacks.
    /// </summary>
    class GraphBuilderNodeCreator : NodeCreator<TNode>
    {
      private readonly GraphBuilderHelper<TNode, TGroup, TEdge> helper;

      internal GraphBuilderNodeCreator(GraphBuilderHelper<TNode, TGroup, TEdge> helper) {
        this.helper = helper;
      }

      public override INode CreateNode(IGraph graph, INode parent, TNode dataItem) {
        var labelData = this.GetLabelData(dataItem);
        var nodeObject = this.GetNodeObject(dataItem);
        return this.helper.builderCreateNode(graph, parent, labelData, nodeObject);
      }

      public override void UpdateNode(IGraph graph, INode node, INode parent, TNode dataItem) {
        var labelData = this.GetLabelData(dataItem);
        var nodeObject = this.GetNodeObject(dataItem);
        this.helper.builderUpdateNode(graph, node, parent, labelData, nodeObject);
      }

      private TNode GetNodeObject(TNode dataItem) {
        if (this.TagProvider != null) {
          return (TNode) this.TagProvider(dataItem);
        }
        return dataItem;
      }

      private object GetLabelData(TNode dataItem) {
        return this.helper.nodeLabelProvider != null
            ? this.helper.nodeLabelProvider(dataItem)
            : null;
      }
    }


    /// <summary>
    /// A custom <see cref="NodeCreator{TDataItem}"/> for groups which delegates to this instance's
    /// <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}.builderCreateGroupNode"/> and
    /// <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}.builderUpdateGroupNode"/> callbacks.
    /// </summary>
    class GraphBuilderGroupCreator : NodeCreator<TGroup>
    {
      private readonly GraphBuilderHelper<TNode, TGroup, TEdge> helper;

      internal GraphBuilderGroupCreator(GraphBuilderHelper<TNode, TGroup, TEdge> helper) {
        this.helper = helper;
      }

      public override INode CreateNode(IGraph graph, INode parent, TGroup dataItem) {
        var labelData = this.GetLabelData(dataItem);
        var nodeObject = this.GetNodeObject(dataItem);
        var node = this.helper.builderCreateGroupNode(graph, labelData, nodeObject);
        graph.SetParent(node, parent);
        return node;
      }

      public override void UpdateNode(IGraph graph, INode node, INode parent, TGroup dataItem) {
        var labelData = this.GetLabelData(dataItem);
        var nodeObject = this.GetNodeObject(dataItem);
        this.helper.builderUpdateGroupNode(graph, node, labelData, nodeObject);
        if (graph.GetParent(node) != parent) {
          graph.SetParent(node, parent);
        }
      }

      private TGroup GetNodeObject(TGroup dataItem) {
        if (this.TagProvider != null) {
          return (TGroup) this.TagProvider(dataItem);
        }
        return dataItem;
      }

      private object GetLabelData(TGroup dataItem) {
        return this.helper.groupLabelProvider != null
            ? this.helper.groupLabelProvider(dataItem)
            : null;
      }
    }

    /// <summary>
    /// A custom <see cref="EdgeCreator{TDataItem}"/> which delegates to this instance's
    /// <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}.builderCreateEdge"/> and 
    /// <see cref="GraphBuilderHelper{TNode,TGroup,TEdge}.builderUpdateEdge"/> callbacks.
    /// </summary>
    class GraphBuilderEdgeCreator : EdgeCreator<TEdge>
    {
      private readonly GraphBuilderHelper<TNode, TGroup, TEdge> helper;
      private readonly bool labelDataFromSourceAndTarget;

      public GraphBuilderEdgeCreator(GraphBuilderHelper<TNode, TGroup, TEdge> helper, bool labelDataFromSourceAndTarget) {
        this.helper = helper;
        this.labelDataFromSourceAndTarget = labelDataFromSourceAndTarget;
      }

      public override IEdge CreateEdge(IGraph graph, INode source, INode target, TEdge dataItem) {
        var labelData = this.GetLabelData(dataItem, source, target);
        var edgeObject = this.GetEdgeObject(dataItem);
        var edge = this.helper.builderCreateEdge(graph, source, target, labelData, edgeObject);
        if (edge == null) {
          throw new InvalidOperationException("An edge must be created by createEdge.");
        }
        return edge;
      }

      public override void UpdateEdge(IGraph graph, IEdge edge, TEdge dataItem) {
        var labelData = this.GetLabelData(dataItem, edge.GetSourceNode(), edge.GetTargetNode());
        var edgeObject = this.GetEdgeObject(dataItem);
        this.helper.builderUpdateEdge(graph, edge, labelData, edgeObject);
      }


      private TEdge GetEdgeObject(TEdge dataItem) {
        if (this.TagProvider != null) {
          return (TEdge) this.TagProvider(dataItem);
        }
        return dataItem;
      }

      private object GetLabelData(TEdge dataItem, INode source, INode target) {
        if (this.labelDataFromSourceAndTarget) {
          return this.helper.edgeLabelProvider != null
              ? this.helper.edgeLabelProvider((TNode) source.Tag, (TNode) target.Tag)
              : null;
        } else {
          return this.helper.edgeLabelBinding != null
              ? this.helper.edgeLabelBinding(dataItem)
              : null;
        }
      }
    }

    #endregion

    #region Create and Update Events

    public event EventHandler<GraphBuilderItemEventArgs<INode, TNode>> NodeCreated;

    public event EventHandler<GraphBuilderItemEventArgs<INode, TNode>> NodeUpdated;

    public event EventHandler<GraphBuilderItemEventArgs<INode, TGroup>> GroupCreated;

    public event EventHandler<GraphBuilderItemEventArgs<INode, TGroup>> GroupUpdated;

    public event EventHandler<GraphBuilderItemEventArgs<IEdge, TEdge>> EdgeCreated;

    public event EventHandler<GraphBuilderItemEventArgs<IEdge, TEdge>> EdgeUpdated;


    private void OnNodeCreated(INode node, TNode dataItem) {
      if (this.NodeCreated != null) {
        NodeCreated(this, new GraphBuilderItemEventArgs<INode, TNode>(this.graph, node, dataItem));
      }
    }

    private void OnNodeUpdated(INode node, TNode dataItem) {
      if (this.NodeUpdated != null) {
        NodeUpdated(this, new GraphBuilderItemEventArgs<INode, TNode>(this.graph, node, dataItem));
      }
    }

    private void OnGroupCreated(INode groupNode, TGroup dataItem) {
      if (this.GroupCreated != null) {
        GroupCreated(this, new GraphBuilderItemEventArgs<INode, TGroup>(this.graph, groupNode, dataItem));
      }
    }

    private void OnGroupUpdated(INode groupNode, TGroup dataItem) {
      if (this.GroupUpdated != null) {
        GroupUpdated(this, new GraphBuilderItemEventArgs<INode, TGroup>(this.graph, groupNode, dataItem));
      }
    }

    private void OnEdgeCreated(IEdge edge, TEdge dataItem) {
      if (this.EdgeCreated != null) {
        EdgeCreated(this, new GraphBuilderItemEventArgs<IEdge, TEdge>(this.graph, edge, dataItem));
      }
    }

    private void OnEdgeUpdated(IEdge edge, TEdge dataItem) {
      if (this.EdgeUpdated != null) {
        EdgeUpdated(this, new GraphBuilderItemEventArgs<IEdge, TEdge>(this.graph, edge, dataItem));
      }
    }

    #endregion
  }
}
