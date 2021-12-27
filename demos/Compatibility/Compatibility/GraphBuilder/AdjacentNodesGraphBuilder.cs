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
using yWorks.Annotations;
using yWorks.Graph;
using yWorks.Graph.DataBinding;
using IEnumerable = System.Collections.IEnumerable;

namespace yWorks.DataBinding.Compatibility
{
  /// <summary>
  /// Populates a graph from custom data where objects corresponding to nodes know their neighbors.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This class can be used when the data specifies a collection of
  /// nodes in which each node knows its direct neighbors, and—optionally—a collection of groups. The
  /// properties <see cref="NodesSource"/> and <see cref="GroupsSource"/> define
  /// the source collections from which nodes and groups will be created.
  /// </para>
  /// <para>
  /// Generally, using the <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/> class consists
  /// of a few basic steps:
  /// </para>
  /// <list type="number">
  /// <item>
  /// Set up the <see cref="Graph"/> with the proper defaults for items
  /// (<see cref="IGraph.NodeDefaults"/>, <see cref="IGraph.GroupNodeDefaults"/>, <see cref="IGraph.EdgeDefaults"/>)
  /// </item>
  /// <item>Create an <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/>.</item>
  /// <item>
  /// Set the items sources.
  /// At the very least the <see cref="NodesSource"/> is needed. Note that the <see cref="NodesSource"/>
  /// does not have to contain all nodes, as nodes that are implicitly specified through the <see cref="PredecessorProvider"/>
  /// and <see cref="SuccessorProvider"/> are automatically added to the graph as well.
  /// If the items in the nodes collection are grouped somehow, then also set the <see cref="GroupsSource"/>
  /// property.
  /// </item>
  /// <item>
  /// Set up the bindings so that a graph structure can actually be created from the items sources.
  /// This involves at least setting up either the <see cref="PredecessorProvider"/> or <see cref="SuccessorProvider"/>
  /// property so that edges can be created. If the node objects don't actually contain their neighboring node objects,
  /// but instead identifiers of other node objects, then <see cref="PredecessorProvider"/>
  /// and <see cref="SuccessorProvider"/> would return those identifiers and <see cref="NodeIdProvider"/>
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
  /// <see cref="TreeBuilder{TNode,TGroup}"/>, where the graph is a tree and node objects
  /// know their children, or <see cref="GraphBuilder{TNode,TEdge,TGroup}"/> which is
  /// a more general approach to creating arbitrary graphs.
  /// </para>
  /// </remarks>
  /// <y.note>
  /// This class serves as a convenient way to create general graphs and has some limitations:
  /// <list type="bullet">
  /// <item>When populating the graph for the first time it will be cleared of all existing items.</item>
  /// <item>When using a <see cref="NodeIdProvider"/>, all nodes have to exist in the <see cref="NodesSource"/>. Nodes cannot be created on demand from IDs only.</item>
  /// <item>Elements manually created on the graph in between calls to <see cref="UpdateGraph"/> may not be preserved.</item>
  /// </list>
  /// <para>
  /// If updates get too complex it's often better to write the code interfacing with the graph by hand
  /// instead of relying on <see cref="GraphBuilder{TNode,TGroup,TEdge}"/>.
  /// </para>
  /// </y.note>
  /// <typeparam name="TNode">The type of object nodes are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// <typeparam name="TGroup">The type of object group nodes are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// 
  /// <seealso cref="GraphBuilder{TNode,TEdge,TGroup}"/>
  /// <seealso cref="TreeBuilder{TNode,TGroup}"/>
  public class AdjacentNodesGraphBuilder<TNode, TGroup>
  {
    private readonly GraphBuilderHelper<TNode, TGroup, TNode> graphBuilderHelper;
    private readonly AdjacencyGraphBuilder graphBuilder;
    private readonly AdjacencyNodesSource<TNode> builderNodesSource;
    private readonly AdjacencyNodesSource<TGroup> builderGroupsSource;
    private readonly EdgeCreator<TNode> builderEdgeCreator;
    private readonly IGraph mirrorGraph;
    private readonly Dictionary<INode, INode> nodeToMirrorNode;

    private Provider<TNode, IEnumerable<TNode>> successorsProvider;
    private Provider<TNode, IEnumerable<TNode>> predecessorsProvider;
    private Provider<TNode, IEnumerable<object>> successorsIdProvider;
    private Provider<TNode, IEnumerable<object>> predecessorsIdProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/> class that
    /// operates on the given graph.
    /// </summary>
    /// <remarks>
    /// The <paramref name="graph"/> will be <see cref="GraphExtensions.Clear">cleared</see> and re-built 
    /// from the data in <see cref="NodesSource"/> and <see cref="GroupsSource"/>
    /// when <see cref="BuildGraph"/> is called.
    /// </remarks>
    public AdjacentNodesGraphBuilder([CanBeNull] IGraph graph = null) {
      graphBuilderHelper = new GraphBuilderHelper<TNode, TGroup, TNode>(
          graph ?? new DefaultGraph(),
          this.CreateNodeAndMirrorNode,
          this.UpdateNodeAndCreateMirrorNode,
          this.CreateGroupNode,
          this.UpdateGroupNode,
          this.CreateEdgeAndMirrorEdge,
          this.UpdateEdgeAndCreateMirrorEdge);

      graphBuilderHelper.EdgeCreated += (sender, args) => {
        var eventArgs = new GraphBuilderItemEventArgs<IEdge, object>(args.Graph, args.Item, args.SourceObject);
        foreach (var eventHandler in edgeCreatedHandler) {
          eventHandler(sender, eventArgs);
        }
      };
      graphBuilderHelper.EdgeUpdated += (sender, args) => {
        var eventArgs = new GraphBuilderItemEventArgs<IEdge, object>(args.Graph, args.Item, args.SourceObject);
        foreach (var eventHandler in edgeUpdatedHandler) {
          eventHandler(sender, eventArgs);
        }
      };
      
      this.graphBuilder = new AdjacencyGraphBuilder(graph);
      this.builderNodesSource = this.graphBuilder.CreateNodesSource<TNode>(Enumerable.Empty<TNode>());
      this.builderNodesSource.NodeCreator = this.graphBuilderHelper.CreateNodeCreator();

      this.builderEdgeCreator = this.graphBuilderHelper.CreateEdgeCreator(true);

      this.builderNodesSource.AddSuccessorsSource(
          dataItem => (this.successorsProvider != null ? this.successorsProvider(dataItem): null),
          this.builderNodesSource, 
          this.builderEdgeCreator
          );
      this.builderNodesSource.AddPredecessorsSource(
          dataItem => (this.predecessorsProvider != null ? this.predecessorsProvider(dataItem) : null),
          this.builderNodesSource,
          this.builderEdgeCreator
      );

      this.builderNodesSource.AddSuccessorIds(
          dataItem => this.successorsIdProvider != null ? this.successorsIdProvider(dataItem) : null,
          this.builderEdgeCreator
      );
      this.builderNodesSource.AddPredecessorIds(
          dataItem => this.predecessorsIdProvider != null ? this.predecessorsIdProvider(dataItem) : null,
          this.builderEdgeCreator
      );

      this.builderGroupsSource = this.graphBuilder.CreateGroupNodesSource<TGroup>(Enumerable.Empty<TGroup>());
      this.builderGroupsSource.NodeCreator = this.graphBuilderHelper.CreateGroupCreator();

      this.mirrorGraph = new DefaultGraph();
      this.nodeToMirrorNode = new Dictionary<INode, INode>();
    }

    /// <summary>
    /// Gets the <see cref="IGraph">graph</see> used by this class.
    /// </summary>
    [NotNull]
    public IGraph Graph {
      get { return graphBuilder.Graph; }
    }

    /// <summary>
    /// Gets or sets the objects to be represented as nodes of the <see cref="Graph"/>.
    /// </summary>
    /// <remarks>
    /// Note that it is not necessary to include all nodes in this property, if they can be reached via the 
    /// <see cref="PredecessorProvider"/> or <see cref="SuccessorProvider"/>. In this case it suffices to include all root nodes.
    /// </remarks>
    [CanBeNull]
    public IEnumerable<TNode> NodesSource { get; set; }

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
    /// when <see cref="PredecessorProvider">predecessors</see> or
    /// <see cref="SuccessorProvider">successors</see> are represented only by an identifier
    /// of nodes instead of pointing directly to the respective node objects.
    /// </remarks>
    /// <y.warning>
    /// The identifiers returned by the delegate must be stable and not change over time.
    /// Otherwise the <see cref="UpdateGraph">update mechanism</see> cannot determine
    /// whether nodes have been added or updated. For the same reason this property
    /// must not be changed after having built the graph once.
    /// </y.warning>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="PredecessorProvider"/>
    /// <seealso cref="SuccessorProvider"/>
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
    /// Gets or sets a delegate that maps an edge, represented by its source and target node object, to a label.
    /// </summary>
    /// <remarks>
    /// This maps the source and target node objects to an object that represents the label
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
    [CanBeNull]
    public EdgeLabelProvider<TNode> EdgeLabelProvider {
      get { return graphBuilderHelper.edgeLabelProvider; }
      set { graphBuilderHelper.edgeLabelProvider = value; }    }

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
      Initialize();
      this.Graph.Clear();
      var graph = graphBuilder.BuildGraph();
      Cleanup();
      return graph;
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
      Initialize();
      graphBuilder.UpdateGraph();
      Cleanup();
    }

    private void Initialize() {
      if (NodesSource == null || SuccessorProvider == null && PredecessorProvider == null) {
        throw new InvalidOperationException("The NodesSource and SuccessorProvider or PredecessorProvider properties must be set before calling BuildGraph.");
      }
      this.InitializeProviders();
      this.PrepareData();
    }
    
    private void InitializeProviders() {
      var predecessorsProvider = this.PredecessorProvider != null ? item => this.PredecessorProvider(item) : (Provider<TNode, IEnumerable>) null;
      var successorsProvider = this.SuccessorProvider != null ? item => this.SuccessorProvider(item) : (Provider<TNode, IEnumerable>) null;

      this.predecessorsProvider = null;
      this.successorsProvider = null;
      this.predecessorsIdProvider = null;
      this.successorsIdProvider = null;

      if (this.NodeIdProvider != null) {
        if (predecessorsProvider != null) {
          this.predecessorsIdProvider = dataItem => 
              this.EliminateDuplicateEdges<object>(dataItem, predecessorsProvider(dataItem), false);
        }
        if (successorsProvider != null) {
          this.successorsIdProvider = dataItem => 
              this.EliminateDuplicateEdges<object>(dataItem, successorsProvider(dataItem), false);
        }
      } else {
        if (predecessorsProvider != null) {
          this.predecessorsProvider = dataItem => 
              this.EliminateDuplicateEdges<TNode>(dataItem, predecessorsProvider(dataItem), false);
        }
        if (successorsProvider != null) {
          this.successorsProvider = dataItem => 
              this.EliminateDuplicateEdges<TNode>(dataItem, successorsProvider(dataItem), false);
        }
      }

      this.builderNodesSource.IdProvider = this.NodeIdProvider != null ? item => this.NodeIdProvider(item) : (IdProvider<TNode>) null;
      this.builderGroupsSource.IdProvider = this.GroupIdProvider != null ? item => this.GroupIdProvider(item) : (IdProvider<TGroup>) null;

      this.builderEdgeCreator.TagProvider = (item => null);

      this.builderNodesSource.ParentIdProvider = this.GroupProvider != null ? item => this.GroupProvider(item) : (Provider<TNode, object>) null;
      this.builderGroupsSource.ParentIdProvider = this.ParentGroupProvider != null ? item => this.ParentGroupProvider(item) : (Provider<TGroup, object>) null;
    }

    private void PrepareData() {
      this.graphBuilder.SetData(this.builderNodesSource, this.NodesSource);
      if (this.GroupsSource != null) {
        this.graphBuilder.SetData(this.builderGroupsSource, this.GroupsSource);
      }
    }
    
    private IEnumerable<TTarget> EliminateDuplicateEdges<TTarget>(TNode thisDataItem, IEnumerable neighborCollection, bool neighborIsSuccessor) {
      var set = new HashSet<object>();
      var distinctCollection = new List<TTarget>();
      foreach (var neighbor in neighborCollection) {
        if (!set.Contains(neighbor) &&
            !this.IsDuplicate(thisDataItem, this.MaybeResolveId(neighbor), neighborIsSuccessor)
            ) {
          set.Add(neighbor);
          distinctCollection.Add((TTarget) neighbor);
        }
      }
      return distinctCollection;
    }
    
    private bool IsDuplicate(TNode thisDataItem, TNode neighborDataItem, bool neighborIsSuccessor) {
      var thisNode = this.GetNode(thisDataItem);
      var neighborNode = this.GetNode(neighborDataItem);

      if (thisNode == null || neighborNode == null) {
        return false;
      }

      if (!nodeToMirrorNode.TryGetValue(thisNode, out thisNode) || nodeToMirrorNode.TryGetValue(neighborNode, out neighborNode)) {
        return false;
      }

      return (neighborIsSuccessor
              ? this.mirrorGraph.Successors(thisNode)
              : this.mirrorGraph.Predecessors(thisNode)
          ).Contains(neighborNode);
    }
    
    
    private TNode MaybeResolveId(object dataItemOrId) {
      return this.NodeIdProvider != null ? this.GetDataItemById(dataItemOrId) : (TNode) dataItemOrId;
    }

    private TNode GetDataItemById(object id) {
      foreach (var dataItem in this.NodesSource) {
        if (this.builderNodesSource.IdProvider(dataItem) == id) {
          return dataItem;
        }
      }
      return default(TNode);
    }

    private void Cleanup() {
      this.mirrorGraph.Clear();
      this.nodeToMirrorNode.Clear();
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
      return this.graphBuilderHelper.CreateNode(graph, parent, labelData, nodeObject);
    }

    /// <summary>
    /// Callback for the <see cref="graphBuilderHelper"/> for node creation.
    /// </summary>
    private INode CreateNodeAndMirrorNode(IGraph graph, INode parent, object labelData, TNode nodeObject) {
      var node = this.CreateNode(graph, parent, labelData, nodeObject);
      var mirrorNode = this.mirrorGraph.CreateNode();
      this.nodeToMirrorNode[node] = mirrorNode;
      return node;
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
      this.graphBuilderHelper.UpdateNode(graph, node, parent, labelData, nodeObject);
    }


    /// <summary>
    /// Callback for the <see cref="graphBuilderHelper"/> for node update.
    /// </summary>
    private void UpdateNodeAndCreateMirrorNode(IGraph graph, INode node, INode parent, object labelData, TNode nodeObject) {
      this.UpdateNode(graph, node, parent, labelData, nodeObject);
      var mirrorNode = this.mirrorGraph.CreateNode();
      this.nodeToMirrorNode[node] = mirrorNode;
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
      this.graphBuilderHelper.UpdateGroupNode(graph, groupNode, labelData, groupObject);
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
      return this.graphBuilderHelper.CreateGroupNode(graph, labelData, groupObject);
    }

    /// <summary>
    /// Retrieves the object from which a given item has been created.
    /// </summary>
    /// <param name="item">The item to get the object for.</param>
    /// <returns>The object from which the graph item has been created.</returns>
    /// <seealso cref="GetNode"/>
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
    /// <seealso cref="GetGroup"/>
    /// <seealso cref="GetSourceObject"/>
    [CanBeNull]
    public INode GetNode([NotNull] TNode nodeObject) {
      return Graph.Nodes.FirstOrDefault(n => n.Tag == (object) nodeObject);
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
    /// <seealso cref="GetSourceObject"/>
    [CanBeNull]
    public INode GetGroup([NotNull] TGroup groupObject) {
      return Graph.Nodes.FirstOrDefault(n => n.Tag == (object) groupObject);
    }

    /// <summary>
    /// Gets or sets a delegate that maps node objects to their successors.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a node to a set of other objects 
    /// that represent its successor nodes, i.e. other nodes connected with an outgoing edge.
    /// <para>
    /// If a <see cref="NodeIdProvider"/> is set, the returned objects must be the IDs of node objects
    /// instead of the node objects themselves.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="PredecessorProvider"/>
    /// <seealso cref="NodeIdProvider"/>
    [CanBeNull]
    public Func<TNode, IEnumerable> SuccessorProvider { get; set; }

    /// <summary>
    /// Gets or sets a delegate that maps node objects to their predecessors.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a node to a set of other objects 
    /// that represent its predecessor nodes, i.e. other nodes connected with an incoming edge.
    /// <para>
    /// If a <see cref="NodeIdProvider"/> is set, the returned objects must be the IDs of node objects
    /// instead of the node objects themselves.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="SuccessorProvider"/>
    /// <seealso cref="NodeIdProvider"/>
    [CanBeNull]
    public Func<TNode, IEnumerable> PredecessorProvider { get; set; }

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
    /// <see cref="PredecessorProvider"/> or <see cref="SuccessorProvider"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgeUpdated"/>
    public event EventHandler<GraphBuilderItemEventArgs<IEdge, object>> EdgeCreated {
      add { edgeCreatedHandler.Add(value); }
      remove { edgeCreatedHandler.Remove(value); }
    }
    
    private List<EventHandler<GraphBuilderItemEventArgs<IEdge, object>>> edgeCreatedHandler 
        = new List<EventHandler<GraphBuilderItemEventArgs<IEdge, object>>>();

    /// <summary>
    /// Occurs when an edge has been updated.
    /// </summary>
    /// <remarks>
    /// This event can be used to update customizations added in an event handler for <see cref="EdgeCreated"/>.
    /// <para>
    /// Edges are updated in response to calling <see cref="UpdateGraph"/> for
    /// items that haven't been added anew in <see cref="PredecessorProvider"/> or <see cref="SuccessorProvider"/> since the last
    /// call to <see cref="BuildGraph"/> or <see cref="UpdateGraph"/>.
    /// </para>
    /// <para>
    /// Depending on how the source data is structured, this event can be raised during
    /// <see cref="BuildGraph"/>, or multiple times for the same edge during <see cref="UpdateGraph"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgeCreated"/>
    public event EventHandler<GraphBuilderItemEventArgs<IEdge, object>> EdgeUpdated {
      add { edgeUpdatedHandler.Add(value); }
      remove { edgeUpdatedHandler.Remove(value); }
    }

    private List<EventHandler<GraphBuilderItemEventArgs<IEdge, object>>> edgeUpdatedHandler 
        = new List<EventHandler<GraphBuilderItemEventArgs<IEdge, object>>>();
    
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
    /// Creates a new edge connecting the given nodes.
    /// </summary>
    /// <remarks>
    /// This class calls this method to create all new edges, and customers may
    /// override it to customize edge creation.
    /// </remarks>
    /// <param name="graph">The graph.</param>
    /// <param name="source">The source node of the edge.</param>
    /// <param name="target">The target node of the edge.</param>
    /// <param name="labelData">The optional label data of the edge if an <see cref="EdgeLabelProvider"/> is specified.</param>
    /// <returns>The created edge.</returns>
    /// <y.expert/>
    [NotNull]
    protected virtual IEdge CreateEdge([NotNull] IGraph graph, [NotNull] INode source, [NotNull] INode target, [CanBeNull] object labelData) {
      return this.graphBuilderHelper.CreateEdge(graph, source, target, labelData, default(TNode));
    }
    
    private IEdge CreateEdgeAndMirrorEdge(IGraph graph, INode source, INode target, object labelData, TNode edgeObject) {
      INode sourceMirrorNode;
      INode targetMirrorNode;
      if (this.nodeToMirrorNode.TryGetValue(source, out sourceMirrorNode) 
          && this.nodeToMirrorNode.TryGetValue(target, out targetMirrorNode)) {
        this.mirrorGraph.CreateEdge(sourceMirrorNode, targetMirrorNode);
      }
      return this.CreateEdge(graph, source, target, labelData);
    }

    /// <summary>
    /// Updates an existing edge connecting the given nodes when <see cref="GraphBuilder{TNode,TEdge,TGroup}.UpdateGraph"/> 
    /// is called and the edge should remain in the graph.
    /// </summary>
    /// <param name="graph">The graph.</param>
    /// <param name="edge">The edge to update.</param>
    /// <param name="labelData">The optional label data of the edge if an <see cref="EdgeLabelProvider"/> is specified.</param>
    /// <y.expert/>
    protected virtual void UpdateEdge([NotNull] IGraph graph, [NotNull] IEdge edge, [CanBeNull] object labelData) {
      this.graphBuilderHelper.UpdateEdge(graph, edge, labelData, default(TNode));
    }

    private void UpdateEdgeAndCreateMirrorEdge(IGraph graph, IEdge edge, object labelData, TNode edgeObject) {
      INode sourceMirrorNode;
      INode targetMirrorNode;
      if (this.nodeToMirrorNode.TryGetValue(edge.GetSourceNode(), out sourceMirrorNode) 
          && this.nodeToMirrorNode.TryGetValue(edge.GetTargetNode(), out targetMirrorNode)) {
        this.mirrorGraph.CreateEdge(sourceMirrorNode, targetMirrorNode);
      }
      this.UpdateEdge(graph, edge, labelData);
    }
  }
}
