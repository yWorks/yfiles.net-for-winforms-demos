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
using System.Linq;
using yWorks.Annotations;
using yWorks.Graph;
using yWorks.Graph.DataBinding;

namespace yWorks.DataBinding.Compatibility
{
  /// <summary>
  /// Populates a graph from custom data.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This class can be used when the data specifies a collection of
  /// nodes, a collection of edges, and—optionally—a collection of groups. The
  /// properties
  /// <see cref="NodesSource"/>,  <see cref="GroupsSource"/>, and  <see cref="EdgesSource"/>
  /// define the source collections from which nodes, groups, and edges will be created.
  /// </para>
  /// <para>
  /// Generally, using the <see cref="GraphBuilder{TNode,TGroup,TEdge}"/> class consists
  /// of a few basic steps:
  /// </para>
  /// <list type="number">
  /// <item>
  /// Set up the <see cref="Graph"/> with the proper defaults for items
  /// (<see cref="IGraph.NodeDefaults"/>, <see cref="IGraph.GroupNodeDefaults"/>, <see cref="IGraph.EdgeDefaults"/>)
  /// </item>
  /// <item>Create a <see cref="GraphBuilder{TNode,TGroup,TEdge}"/>.</item>
  /// <item>
  /// Set the items sources.
  /// At the very least the <see cref="NodesSource"/> (unless using <see cref="LazyNodeDefinition"/>)
  /// and <see cref="EdgesSource"/> are needed. If
  /// the items in the nodes collection are grouped somehow, then also set the <see cref="GroupsSource"/>
  /// property.
  /// </item>
  /// <item>
  /// Set up the bindings so that a graph structure can actually be created from the items sources.
  /// This involves at least setting up the <see cref="SourceNodeProvider"/> and <see cref="TargetNodeProvider"/>
  /// properties so that edges can be created. If the edge objects don't actually contain the node objects
  /// as source and target, but instead an identifier of the node objects, then <see cref="SourceNodeProvider"/>
  /// and <see cref="TargetNodeProvider"/> would return those identifiers and <see cref="NodeIdProvider"/>
  /// must be set to return that identifier when given a node object.
  /// </item>
  /// <item>
  /// If <see cref="GroupsSource"/> is set, then you also need to set the <see cref="GroupProvider"/> property
  /// to enable mapping nodes to groups. Just like with edges and their source and target nodes,
  /// if the node object only contains an identifier for a group node
  /// and not the actual group object, then return the identifier in the <see cref="GroupProvider"/> and set
  /// up the <see cref="GroupIdProvider"/> to map group node objects to their identifiers. If group nodes can nest,
  /// you also need the <see cref="ParentGroupProvider"/>.
  /// </item>
  /// <item>
  /// You can also easily create labels for nodes, groups, and edges by using the <see cref="NodeLabelProvider"/>,
  /// <see cref="GroupLabelProvider"/>, and <see cref="EdgeLabelProvider"/> properties.
  /// </item>
  /// <item>
  /// Call <see cref="BuildGraph"/> to populate the graph. You can apply a layout algorithm
  /// afterward to make the graph look nice.
  /// </item>
  /// <item>
  /// If your items or collections change later, call <see cref="UpdateGraph"/> to make those changes visible
  /// in the graph.
  /// </item>
  /// </list>
  /// <para>
  /// You can further customize how nodes, groups, and edges are created by adding
  /// event handlers to the various events and modifying the items there. This can
  /// be used to modify items in ways that are not directly supported by the available
  /// bindings or defaults. This includes scenarios such as the following:
  /// </para>
  /// <list type="bullet">
  /// <item>Setting node positions or adding bends to edges. Often a layout is applied to the graph after building it, so these things are only rarely needed.</item>
  /// <item>Modifying individual items, such as setting a different style for every nodes, depending on the bound object.</item>
  /// <item>Adding more than one label for an item, as the <see cref="NodeLabelProvider"/> and <see cref="EdgeLabelProvider"/> will only create a single label, or adding labels to group nodes.</item>
  /// </list>
  /// <para>
  /// There are creation and update events for all three types of items, which allows
  /// separate customization  when nodes, groups, and edges are created or updated. For
  /// completely static graphs where <see cref="UpdateGraph"/> is not needed, the
  /// update events can be safely ignored.
  /// </para>
  /// <para>
  /// Depending on how the source data is laid out, you may also consider using
  /// <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/>, where node objects
  /// know their neighbors, or <see cref="TreeBuilder{TNode,TGroup}"/> where the
  /// graph is a tree and node objects know their children. Both of those other
  /// graph builders make edges implicit through the relationships between nodes
  /// and thus have no <see cref="EdgesSource"/>.
  /// </para>
  /// </remarks>
  /// <y.note>
  /// This class serves as a convenient way to create general graphs and has some limitations:
  /// <list type="bullet">
  /// <item>When populating the graph for the first time it will be cleared of all existing items.</item>
  /// <item>Elements manually created on the graph in between calls to <see cref="UpdateGraph"/> may not be preserved.</item>
  /// <item>Edge objects in <see cref="EdgesSource"/> cannot change their source or target node. <see cref="SourceNodeProvider"/> and <see cref="TargetNodeProvider"/> are only used during edge creation.</item>
  /// </list>
  /// <para>
  /// If updates get too complex it's often better to write the code interfacing with the graph by hand
  /// instead of relying on <see cref="GraphBuilder{TNode,TGroup,TEdge}"/>.
  /// </para>
  /// </y.note>
  /// <y.note>
  /// <see cref="GroupProvider"/> may also return objects or IDs corresponding to normal nodes instead of items from <see cref="GroupsSource"/> (and
  /// in fact, <see cref="GroupsSource"/> does not have to be set at all). This enables convenient use of group nodes when any node can also become
  /// a group node at a later time. However, the feature comes with a few caveats:
  /// <list type="bullet">
  /// <item>Nodes are converted to group nodes automatically as soon as another node has that node as a parent via <see cref="GroupProvider"/>.
  /// The node's style is automatically changed to the graph's default style for group nodes. Group nodes created this way will never be changed
  /// back to normal nodes, so the result on the graph after several changes in data and calls to <see cref="UpdateGraph"/> can be different than
  /// a fresh <see cref="BuildGraph"/> with the same data.</item>
  /// <item><see cref="LazyNodeDefinition"/> is not supported for <see cref="GroupProvider"/>.</item>
  /// <item>If <see cref="GroupProvider"/> references a node that will never exist, the resulting graph may have wrong parent/child relationships from
  /// what the backing data specifies. Those can in some cases be corrected by calling <see cref="UpdateGraph"/> once more.</item>
  /// </list>
  /// </y.note>
  /// <typeparam name="TNode">The type of object nodes are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// <typeparam name="TEdge">The type of object edges are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// <typeparam name="TGroup">The type of object group nodes are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// 
  /// <seealso cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/>
  /// <seealso cref="TreeBuilder{TNode,TGroup}"/>
  public class GraphBuilder<TNode, TEdge, TGroup>
  {
    // the GraphBuilder to delegate to
    private readonly GraphBuilder graphBuilder;
    private readonly NodesSource<TNode> builderNodesSource;
    private readonly NodesSource<TGroup> builderGroupsSource;
    private readonly EdgesSource<TEdge> builderEdgesSource;
    
    private readonly GraphBuilderHelper<TNode, TGroup, TEdge> graphBuilderHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GraphBuilder{TNode,TGroup,TEdge}"/> class that
    /// operates on the given graph.
    /// </summary>
    /// <remarks>
    /// The <paramref name="graph"/> will be <see cref="GraphExtensions.Clear">cleared</see> and re-built 
    /// from the data in <see cref="NodesSource"/>, <see cref="GroupsSource"/>, and <see cref="EdgesSource"/>
    /// when <see cref="BuildGraph"/> is called.
    /// </remarks>
    public GraphBuilder([CanBeNull] IGraph graph = null) {
      graphBuilderHelper = new GraphBuilderHelper<TNode, TGroup, TEdge>(
          graph ?? new DefaultGraph(),
          this.CreateNode,
          this.UpdateNode,
          this.CreateGroupNode,
          this.UpdateGroupNode,
          this.CreateEdge,
          this.UpdateEdge
          );
      this.graphBuilder = new GraphBuilder(graph);
      this.builderNodesSource = this.graphBuilder.CreateNodesSource<TNode>(Enumerable.Empty<TNode>());
      this.builderNodesSource.NodeCreator = this.graphBuilderHelper.CreateNodeCreator();

      this.builderGroupsSource = this.graphBuilder.CreateGroupNodesSource<TGroup>(Enumerable.Empty<TGroup>());
      this.builderGroupsSource.NodeCreator = this.graphBuilderHelper.CreateGroupCreator();

      this.builderEdgesSource = this.graphBuilder.CreateEdgesSource<TEdge>(Enumerable.Empty<TEdge>(),
          item => this.SourceNodeProvider != null ? this.SourceNodeProvider(item) : (Provider<TEdge, object>) null, 
          item => this.TargetNodeProvider != null ?  this.TargetNodeProvider(item) : (Provider<TEdge, object>) null
      );
      this.builderEdgesSource.EdgeCreator = this.graphBuilderHelper.CreateEdgeCreator();
    }

    /// <summary>
    /// Gets the <see cref="IGraph">graph</see> used by this class.
    /// </summary>
    [NotNull]
    public IGraph Graph {
      get { return graphBuilder.Graph; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether or not to automatically create nodes
    /// for values returned from <see cref="SourceNodeProvider"/> and <see cref="TargetNodeProvider"/>
    /// that don't exist in <see cref="NodesSource"/>.
    /// </summary>
    /// <remarks>
    /// When this property is set to <see langword="false"/>, nodes in the graph are
    /// <em>only</em> created from <see cref="NodesSource"/>, and edge objects that result in
    /// source or target nodes not in <see cref="NodesSource"/> will have no edge created.
    /// <para>
    /// If this property is set to <see langword="true"/>, edges will always be created,
    /// and if <see cref="SourceNodeProvider"/> or <see cref="TargetNodeProvider"/>
    /// return values not in <see cref="NodesSource"/>, additional nodes are created as needed.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="EdgesSource"/>
    public bool LazyNodeDefinition { get; set; }

    /// <summary>
    /// Gets or sets the objects to be represented as nodes of the <see cref="Graph"/>.
    /// </summary>
    [CanBeNull]
    public IEnumerable<TNode> NodesSource { get; set; }

    /// <summary>
    /// Gets or sets the objects to be represented as edges of the <see cref="Graph"/>.
    /// </summary>
    [CanBeNull]
    public IEnumerable<TEdge> EdgesSource { get; set; }

    /// <summary>
    /// Gets or sets the objects to be represented as group nodes of the <see cref="Graph"/>.
    /// </summary>
    [CanBeNull]
    public IEnumerable<TGroup> GroupsSource { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps node objects to their identifier.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a node to its identifier. This is needed
    /// when <see cref="EdgesSource">edge objects</see> only contain an identifier
    /// to specify their source and target nodes instead of pointing directly to the
    /// respective node object.
    /// </remarks>
    /// <y.warning>
    /// The identifiers returned by the delegate must be stable and not change over time.
    /// Otherwise the <see cref="UpdateGraph">update mechanism</see> cannot determine
    /// whether nodes have been added or updated. For the same reason this property
    /// must not be changed after having built the graph once.
    /// </y.warning>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="SourceNodeProvider"/>
    /// <seealso cref="TargetNodeProvider"/>
    [CanBeNull]
    public Func<TNode, object> NodeIdProvider { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps a node object to a label.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a node to an object that represents the label
    /// for the node.
    /// <para>
    /// The resulting object will be converted into a string to be displayed as the label's text.
    /// If this is insufficient, a label can also be created directly in an event handler of
    /// the <see cref="NodeCreated"/> event.
    /// </para>
    /// <para>
    /// Returning <see langword="null"/> from the delegate will not create a label for that node.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    [CanBeNull]
    public Func<TNode, object> NodeLabelProvider {
      get { return graphBuilderHelper.nodeLabelProvider; }
      set { graphBuilderHelper.nodeLabelProvider = value; }
    }

    /// <summary>
    /// Gets or sets a delegate that maps node objects to their containing groups.
    /// </summary>
    /// <remarks>
    /// This maps an object <i>N</i> that represents a node to another object 
    /// <i>G</i> that specifies the containing group of <i>N</i>. If <i>G</i> is contained in 
    /// <see cref="GroupsSource"/>, then the node for <i>N</i> becomes a child node of the group
    /// for <i>G</i>.
    /// <para>
    /// If a <see cref="GroupIdProvider"/> is set, the returned object <i>G</i> must be the ID of the object
    /// that specifies the group instead of that object itself.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="GroupsSource"/>
    /// <seealso cref="GroupIdProvider"/>
    [CanBeNull]
    public Func<TNode, object> GroupProvider { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps an edge object to a label.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents an edge to an object that represents the label
    /// for the edge.
    /// <para>
    /// The resulting object will be converted into a string to be displayed as the label's text.
    /// If this is insufficient, a label can also be created directly in an event handler of
    /// the <see cref="EdgeCreated"/> event.
    /// </para>
    /// <para>
    /// Returning <see langword="null"/> from the delegate will not create a label for that edge.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgesSource"/>
    [CanBeNull]
    public Func<TEdge, object> EdgeLabelProvider {
      get { return graphBuilderHelper.edgeLabelBinding; }
      set { graphBuilderHelper.edgeLabelBinding = value; }    }

    /// <summary>
    /// Gets or sets a delegate that maps edge objects to their source node.
    /// </summary>
    /// <remarks>
    /// This maps an object <i>E</i> that represents an edge to another object 
    /// <i>N</i> that specifies the source node of <i>E</i>. This source node may be
    /// a group node from <see cref="GroupsSource"/> as well.
    /// <para>
    /// If a <see cref="NodeIdProvider"/> is set, the returned object <i>N</i> must be the ID of the object
    /// that specifies the node instead of that object itself. The same holds for <see cref="GroupIdProvider"/>
    /// when trying to specify a group node that way. If both a node and a group node share the same ID, the
    /// node takes precedence over the group node.
    /// </para>
    /// <para>
    /// If <see cref="LazyNodeDefinition"/> is <see langword="true"/>, the resulting node object
    /// does not have to exist in <see cref="NodesSource"/>; instead, nodes are created as needed.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="TargetNodeProvider"/>
    /// <seealso cref="NodeIdProvider"/>
    /// <seealso cref="LazyNodeDefinition"/>
    [CanBeNull]
    public Func<TEdge, object> SourceNodeProvider { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps edge objects to their target node.
    /// </summary>
    /// <remarks>
    /// This maps an object <i>E</i> that represents an edge to another object 
    /// <i>N</i> that specifies the target node of <i>E</i>. This target node may be
    /// a group node from <see cref="GroupsSource"/> as well.
    /// <para>
    /// If a <see cref="NodeIdProvider"/> is set, the returned object <i>N</i> must be the ID of the object
    /// that specifies the node instead of that object itself. The same holds for <see cref="GroupIdProvider"/>
    /// when trying to specify a group node that way. If both a node and a group node share the same ID, the
    /// node takes precedence over the group node.
    /// </para>
    /// <para>
    /// If <see cref="LazyNodeDefinition"/> is <see langword="true"/>, the resulting node object
    /// does not have to exist in <see cref="NodesSource"/>; instead, nodes are created as needed.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="SourceNodeProvider"/>
    /// <seealso cref="NodeIdProvider"/>
    /// <seealso cref="LazyNodeDefinition"/>
    [CanBeNull]
    public Func<TEdge, object> TargetNodeProvider { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps group objects to their identifier.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a group node to its identifier. This is needed
    /// when <see cref="NodesSource">node objects</see> only contain an identifier
    /// to specify the group they belong to instead of pointing directly to the
    /// respective group object. The same goes for the parent group in group objects.
    /// </remarks>
    /// <y.warning>
    /// The identifiers returned by the delegate must be stable and not change over time.
    /// Otherwise the <see cref="UpdateGraph">update mechanism</see> cannot determine
    /// whether groups have been added or updated. For the same reason this property
    /// must not be changed after having built the graph once.
    /// </y.warning>
    /// <seealso cref="GroupsSource"/>
    /// <seealso cref="GroupProvider"/>
    /// <seealso cref="ParentGroupProvider"/>
    [CanBeNull]
    public Func<TGroup, object> GroupIdProvider { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps a group object to a label.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a group node to an object that represents the label
    /// for the group node.
    /// <para>
    /// The resulting object will be converted into a string to be displayed as the label's text.
    /// If this is insufficient, a label can also be created directly in an event handler of
    /// the <see cref="GroupNodeCreated"/> event.
    /// </para>
    /// <para>
    /// Returning <see langword="null"/> from the delegate will not create a label for that group node.
    /// </para>
    /// </remarks>
    /// <seealso cref="GroupsSource"/>
    [CanBeNull]
    public Func<TGroup, object> GroupLabelProvider {
      get { return graphBuilderHelper.groupLabelProvider; }
      set { graphBuilderHelper.groupLabelProvider = value; }
    }


    /// <summary>
    /// Gets or sets a delegate that maps group objects to their containing groups.
    /// </summary>
    /// <remarks>
    /// This maps an object <i>G</i> that represents a group node to another object 
    /// <i>P</i> that specifies the containing group of <i>G</i>. If <i>P</i> is contained in 
    /// <see cref="GroupsSource"/>, then the group node for <i>G</i> becomes a child node of the group
    /// for <i>P</i>.
    /// <para>
    /// If a <see cref="GroupIdProvider"/> is set, the returned object <i>P</i> must be the ID of the object
    /// that specifies the group instead of that object itself.
    /// </para>
    /// </remarks>
    /// <seealso cref="GroupsSource"/>
    /// <seealso cref="GroupIdProvider"/>
    [CanBeNull]
    public Func<TGroup, object> ParentGroupProvider { get; set; }

    /// <summary>
    /// Populates the graph with items generated from the bound data.
    /// </summary>
    /// <remarks>
    /// The graph is cleared, and then new nodes, groups, and edges are created
    /// as defined by the source collections.
    /// </remarks>
    /// <returns>The created graph.</returns>
    /// <seealso cref="UpdateGraph"/>
    [NotNull]
    public virtual IGraph BuildGraph() {
      this.Graph.Clear();
      Initialize();
      return this.graphBuilder.BuildGraph();
    }

    /// <summary>
    /// Updates the graph after changes in the bound data.
    /// </summary>
    /// <remarks>
    /// In contrast to <see cref="BuildGraph"/>, the graph is not cleared.
    /// Instead, graph elements corresponding to objects that are still present
    /// in the source collections are kept, new graph elements are created for new
    /// objects in the collections, and obsolete ones are removed.
    /// </remarks>
    public virtual void UpdateGraph() {
      this.Initialize();
      graphBuilder.UpdateGraph();
    }

    private void Initialize() {
      if (NodesSource == null) {
        throw new InvalidOperationException("NodesSource must be set.");
      }

      if (EdgesSource != null && (SourceNodeProvider == null || TargetNodeProvider == null)) {
        throw new InvalidOperationException("Since EdgesSource is set, SourceNodeProvider and TargetNodeProvider must be set, too.");
      }

      if (LazyNodeDefinition && NodeIdProvider != null) {
        throw new InvalidOperationException("LazyNodeDefinition cannot be used with NodeIdProvider.");
      }
      this.InitializeProviders();
      this.PrepareData();
    }

    private void InitializeProviders() {
      this.builderNodesSource.IdProvider = this.NodeIdProvider != null ? item => this.NodeIdProvider(item) : (IdProvider<TNode>) null;
      this.builderGroupsSource.IdProvider = this.GroupIdProvider != null ? item => this.GroupIdProvider(item) : (IdProvider<TGroup>) null;

      this.builderNodesSource.ParentIdProvider = this.GroupProvider != null ? item => this.GroupProvider(item) : (Provider<TNode, object>) null;
      this.builderGroupsSource.ParentIdProvider = this.ParentGroupProvider != null ? item => this.ParentGroupProvider(item) : (Provider<TGroup, object>) null;
    }

    private void PrepareData() {
      var nodesSource = this.NodesSource;
      
      if (this.LazyNodeDefinition && this.EdgesSource != null) {
        var clonedNodesSource = new List<TNode>(nodesSource);
        foreach (var edgeDataItem in this.EdgesSource) {
          var sourceNodeDataItem = this.SourceNodeProvider(edgeDataItem);
          if (sourceNodeDataItem is TNode && !clonedNodesSource.Contains((TNode) sourceNodeDataItem)) {
            clonedNodesSource.Add((TNode) sourceNodeDataItem);
          }
          var targetNodeDataItem = this.TargetNodeProvider(edgeDataItem);
          if (targetNodeDataItem is TNode && !clonedNodesSource.Contains((TNode) targetNodeDataItem)) {
            clonedNodesSource.Add((TNode) targetNodeDataItem);
          }
        }
        nodesSource = clonedNodesSource;
      }

      this.graphBuilder.SetData(this.builderNodesSource, nodesSource);
      if (this.EdgesSource != null) {
        this.graphBuilder.SetData(this.builderEdgesSource, this.EdgesSource);
      }
      if (this.GroupsSource != null) {
        this.graphBuilder.SetData(this.builderGroupsSource, this.GroupsSource);
      }
    }

    /// <summary>
    /// Creates a node with the specified parent from the given <paramref name="nodeObject"/> and
    /// <paramref name="labelData"/>.
    /// </summary>
    /// <remarks>
    /// This method is called for every node that is created either when <see cref="BuildGraph">building the graph</see>,
    /// or when new items appear in the <see cref="NodesSource"/> when <see cref="UpdateGraph">updating it</see>.
    /// <para>
    /// The default behavior is to create the node with the given parent node, assign the <paramref name="nodeObject"/>
    /// to the node's <see cref="ITagOwner.Tag"/> property, and create a label from <paramref name="labelData"/>,
    /// if present.
    /// </para>
    /// <para>
    /// Customizing how nodes are created is usually easier by adding an event handler to the <see cref="NodeCreated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The graph in which to create the node.</param>
    /// <param name="parent">The node's parent node.</param>
    /// <param name="labelData">The optional label data of the node if an <see cref="NodeLabelProvider"/> is specified.</param>
    /// <param name="nodeObject">The object from <see cref="NodesSource"/> from which to create the node.</param>
    /// <returns>The created node.</returns>
    /// <y.expert/>
    [NotNull]
    protected virtual INode CreateNode([NotNull] IGraph graph, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject) {
      return graphBuilderHelper.CreateNode(graph, parent, labelData, nodeObject);
    }

    /// <summary>
    /// Updates an existing node when the <see cref="UpdateGraph">graph is updated</see>.
    /// </summary>
    /// <remarks>
    /// This method is called during <see cref="UpdateGraph">updating the graph</see> for every node that already
    /// exists in the graph where its corresponding object from <see cref="NodesSource"/> is also still present.
    /// <para>
    /// Customizing how nodes are updated is usually easier by adding an event handler to the <see cref="NodeUpdated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The node's containing graph.</param>
    /// <param name="node">The node to update.</param>
    /// <param name="parent">The node's parent node.</param>
    /// <param name="labelData">The optional label data of the node if an <see cref="NodeLabelProvider"/> is specified.</param>
    /// <param name="nodeObject">The object from <see cref="NodesSource"/> from which the node has been created.</param>
    /// <y.expert/>
    protected virtual void UpdateNode([NotNull] IGraph graph, [NotNull] INode node, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject) {
      graphBuilderHelper.UpdateNode(graph, node, parent, labelData, nodeObject);
    }

    /// <summary>
    /// Creates an edge from the given <paramref name="edgeObject"/> and <paramref name="labelData"/>.
    /// </summary>
    /// <remarks>
    /// This method is called for every edge that is created either when <see cref="BuildGraph">building the graph</see>,
    /// or when new items appear in the <see cref="EdgesSource"/> when <see cref="UpdateGraph">updating it</see>.
    /// <para>
    /// The default behavior is to create the edge, assign the <paramref name="edgeObject"/>
    /// to the edge's <see cref="ITagOwner.Tag"/> property, and create a label from <paramref name="labelData"/>,
    /// if present.
    /// </para>
    /// <para>
    /// Customizing how edges are created is usually easier by adding an event handler to the <see cref="EdgeCreated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The graph in which to create the edge.</param>
    /// <param name="source">The source node for the edge.</param>
    /// <param name="target">The target node for the edge.</param>
    /// <param name="labelData">The optional label data of the edge if an <see cref="EdgeLabelProvider"/> is specified.</param>
    /// <param name="edgeObject">The object from <see cref="EdgesSource"/> from which to create the edge.</param>
    /// <returns>The created edge.</returns>
    /// <y.expert/>
    [CanBeNull]
    protected virtual IEdge CreateEdge([NotNull] IGraph graph, [CanBeNull] INode source, [CanBeNull] INode target, [CanBeNull] object labelData, [CanBeNull] TEdge edgeObject) {
      return this.graphBuilderHelper.CreateEdge(graph, source, target, labelData, edgeObject);
    }

    /// <summary>
    /// Updates an existing edge when the <see cref="UpdateGraph">graph is updated</see>.
    /// </summary>
    /// <remarks>
    /// This method is called during <see cref="UpdateGraph">updating the graph</see> for every edge that already
    /// exists in the graph where its corresponding object from <see cref="EdgesSource"/> is also still present.
    /// <para>
    /// Customizing how edges are updated is usually easier by adding an event handler to the <see cref="EdgeUpdated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The edge's containing graph.</param>
    /// <param name="edge">The edge to update.</param>
    /// <param name="labelData">The optional label data of the edge if an <see cref="EdgeLabelProvider"/> is specified.</param>
    /// <param name="edgeObject">The object from <see cref="EdgesSource"/> from which the edge has been created.</param>
    /// <y.expert/>
    protected virtual void UpdateEdge([NotNull] IGraph graph, [NotNull] IEdge edge, [CanBeNull] object labelData, [CanBeNull] TEdge edgeObject) {
      graphBuilderHelper.UpdateEdge(graph, edge, labelData, edgeObject);
    }

    /// <summary>
    /// Creates a group node from the given <paramref name="groupObject"/> and <paramref name="labelData"/>.
    /// </summary>
    /// <remarks>
    /// This method is called for every group node that is created either when <see cref="BuildGraph">building the graph</see>,
    /// or when new items appear in the <see cref="GroupsSource"/> when <see cref="UpdateGraph">updating it</see>.
    /// <para>
    /// The default behavior is to create the group node, assign the <paramref name="groupObject"/>
    /// to the group node's <see cref="ITagOwner.Tag"/> property, and create a label from <paramref name="labelData"/>,
    /// if present.
    /// </para>
    /// <para>
    /// Customizing how group nodes are created is usually easier by adding an event handler to the <see cref="GroupNodeCreated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The graph in which to create the group node.</param>
    /// <param name="labelData">The optional label data of the group node if an <see cref="GroupLabelProvider"/> is specified.</param>
    /// <param name="groupObject">The object from <see cref="GroupsSource"/> from which to create the group node.</param>
    /// <returns>The created group node.</returns>
    /// <y.expert/>
    [NotNull]
    protected virtual INode CreateGroupNode([NotNull] IGraph graph, [CanBeNull] object labelData, [CanBeNull] TGroup groupObject) {
      return graphBuilderHelper.CreateGroupNode(graph, labelData, groupObject);
    }

    /// <summary>
    /// Updates an existing group node when the <see cref="UpdateGraph">graph is updated</see>.
    /// </summary>
    /// <remarks>
    /// This method is called during <see cref="UpdateGraph">updating the graph</see> for every group node that already
    /// exists in the graph where its corresponding object from <see cref="GroupsSource"/> is also still present.
    /// <para>
    /// Customizing how group nodes are updated is usually easier by adding an event handler to the <see cref="GroupNodeUpdated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The group node's containing graph.</param>
    /// <param name="groupNode">The group node to update.</param>
    /// <param name="labelData">The optional label data of the group node if an <see cref="GroupLabelProvider"/> is specified.</param>
    /// <param name="groupObject">The object from <see cref="GroupsSource"/> from which the group node has been created.</param>
    /// <y.expert/>
    protected virtual void UpdateGroupNode([NotNull] IGraph graph, [NotNull] INode groupNode, [CanBeNull] object labelData, [CanBeNull] TGroup groupObject) {
      graphBuilderHelper.UpdateGroupNode(graph, groupNode, labelData, groupObject);
    }

    /// <summary>
    /// Occurs when a node has been created.
    /// </summary>
    /// <remarks>
    /// This event can be used to further customize the created node.
    /// <para>
    /// New nodes are created either in response to calling <see cref="BuildGraph"/>,
    /// or in response to calling <see cref="UpdateGraph"/> when there are new items in
    /// <see cref="NodesSource"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodeUpdated"/>
    public event EventHandler<GraphBuilderItemEventArgs<INode, TNode>> NodeCreated {
      add { graphBuilderHelper.NodeCreated += value; }
      remove { graphBuilderHelper.NodeCreated -= value; }
    }

    /// <summary>
    /// Occurs when a node has been updated.
    /// </summary>
    /// <remarks>
    /// This event can be used to update customizations added in an event handler for <see cref="NodeCreated"/>.
    /// <para>
    /// Nodes are updated in response to calling <see cref="UpdateGraph"/> for
    /// items that haven't been added anew in <see cref="NodesSource"/> since the last
    /// call to <see cref="BuildGraph"/> or <see cref="UpdateGraph"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodeCreated"/>
    public event EventHandler<GraphBuilderItemEventArgs<INode, TNode>> NodeUpdated {
      add { graphBuilderHelper.NodeUpdated += value; }
      remove { graphBuilderHelper.NodeUpdated -= value; }
    }

    /// <summary>
    /// Occurs when an edge has been created.
    /// </summary>
    /// <remarks>
    /// This event can be used to further customize the created edge.
    /// <para>
    /// New edges are created either in response to calling <see cref="BuildGraph"/>,
    /// or in response to calling <see cref="UpdateGraph"/> when there are new items in
    /// <see cref="EdgesSource"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgeUpdated"/>
    public event EventHandler<GraphBuilderItemEventArgs<IEdge, TEdge>> EdgeCreated {
      add { graphBuilderHelper.EdgeCreated += value; }
      remove { graphBuilderHelper.EdgeCreated -= value; }
    }

    /// <summary>
    /// Occurs when an edge has been updated.
    /// </summary>
    /// <remarks>
    /// This event can be used to update customizations added in an event handler for <see cref="EdgeCreated"/>.
    /// <para>
    /// Edges are updated in response to calling <see cref="UpdateGraph"/> for
    /// items that haven't been added anew in <see cref="EdgesSource"/> since the last
    /// call to <see cref="BuildGraph"/> or <see cref="UpdateGraph"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgeCreated"/>
    public event EventHandler<GraphBuilderItemEventArgs<IEdge, TEdge>> EdgeUpdated {
      add { graphBuilderHelper.EdgeUpdated += value; }
      remove { graphBuilderHelper.EdgeUpdated -= value; }
    }

    /// <summary>
    /// Occurs when a group node has been created.
    /// </summary>
    /// <remarks>
    /// This event can be used to further customize the created group node.
    /// <para>
    /// New group nodes are created either in response to calling <see cref="BuildGraph"/>,
    /// or in response to calling <see cref="UpdateGraph"/> when there are new items in
    /// <see cref="GroupsSource"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="GroupNodeUpdated"/>
    public event EventHandler<GraphBuilderItemEventArgs<INode, TGroup>> GroupNodeCreated {
      add { graphBuilderHelper.GroupCreated += value; }
      remove { graphBuilderHelper.GroupCreated -= value; }
    }

    /// <summary>
    /// Occurs when a group node has been updated.
    /// </summary>
    /// <remarks>
    /// This event can be used to update customizations added in an event handler for <see cref="GroupNodeCreated"/>.
    /// <para>
    /// Group nodes are updated in response to calling <see cref="UpdateGraph"/> for
    /// items that haven't been added anew in <see cref="GroupsSource"/> since the last
    /// call to <see cref="BuildGraph"/> or <see cref="UpdateGraph"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="GroupNodeCreated"/>
    public event EventHandler<GraphBuilderItemEventArgs<INode, TGroup>> GroupNodeUpdated {
      add { graphBuilderHelper.GroupUpdated += value; }
      remove { graphBuilderHelper.GroupUpdated -= value; }
    }

    /// <summary>
    /// Retrieves the object from which a given item has been created.
    /// </summary>
    /// <param name="item">The item to get the object for.</param>
    /// <returns>The object from which the graph item has been created.</returns>
    /// <seealso cref="GetNode"/>
    /// <seealso cref="GetEdge"/>
    /// <seealso cref="GetGroup"/>
    [CanBeNull]
    public object GetSourceObject([NotNull] IModelItem item) {
      return item.Tag;
    }

    /// <summary>
    /// Retrieves the node associated with an object from the <see cref="NodesSource"/>.
    /// </summary>
    /// <param name="nodeObject">An object from the <see cref="NodesSource"/>.</param>
    /// <returns>
    /// The node associated with <paramref name="nodeObject"/>, or <see langword="null"/>
    /// in case there is no node associated with that object. This can happen if <paramref name="nodeObject"/>
    /// is new since the last call to <see cref="UpdateGraph"/>.
    /// </returns>
    /// <seealso cref="GetEdge"/>
    /// <seealso cref="GetGroup"/>
    /// <seealso cref="GetSourceObject"/>
    [CanBeNull]
    public INode GetNode([NotNull] TNode nodeObject) {
      return Graph.Nodes.FirstOrDefault(n => n.Tag == (object) nodeObject);
    }

    /// <summary>
    /// Retrieves the group node associated with an object from the <see cref="GroupsSource"/>.
    /// </summary>
    /// <param name="edgeObject">An object from the <see cref="GroupsSource"/>.</param>
    /// <returns>
    /// The group node associated with <paramref name="edgeObject"/>, or <see langword="null"/>
    /// in case there is no group node associated with that object. This can happen if <paramref name="edgeObject"/>
    /// is new since the last call to <see cref="UpdateGraph"/>.
    /// </returns>
    /// <seealso cref="GetNode"/>
    /// <seealso cref="GetGroup"/>
    /// <seealso cref="GetSourceObject"/>
    [CanBeNull]
    public IEdge GetEdge([NotNull] TEdge edgeObject) {
      return Graph.Edges.FirstOrDefault(e => e.Tag == (object) edgeObject);
    }

    /// <summary>
    /// Retrieves the group node associated with an object from the <see cref="GroupsSource"/>.
    /// </summary>
    /// <param name="groupObject">An object from the <see cref="GroupsSource"/>.</param>
    /// <returns>
    /// The group node associated with <paramref name="groupObject"/>, or <see langword="null"/>
    /// in case there is no group node associated with that object. This can happen if <paramref name="groupObject"/>
    /// is new since the last call to <see cref="UpdateGraph"/>.
    /// </returns>
    /// <seealso cref="GetNode"/>
    /// <seealso cref="GetEdge"/>
    /// <seealso cref="GetSourceObject"/>
    [CanBeNull]
    public INode GetGroup([NotNull] TGroup groupObject) {
      return Graph.Nodes.FirstOrDefault(n => n.Tag == (object) groupObject);
    }
  }
}
