/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.5.
 ** Copyright (c) 2000-2023 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using yWorks.Annotations;
using yWorks.Graph;
using IEnumerable = System.Collections.IEnumerable;

namespace yWorks.DataBinding.Compatibility
{
  /// <summary>
  /// Populates a graph from custom data where objects corresponding to nodes have a parent-child relationship.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This class can be used when the data specifies a collection of
  /// nodes, each of which knows its child nodes, and—optionally—a collection of groups. The
  /// properties <see cref="NodesSource"/> and <see cref="GroupsSource"/> define
  /// the source collections from which nodes and groups will be created.
  /// </para>
  /// <para>
  /// Generally, using the <see cref="TreeBuilder{TNode,TGroup}"/> class consists
  /// of a few basic steps:
  /// </para>
  /// <list type="number">
  /// <item>
  /// Set up the <see cref="Graph"/> with the proper defaults for items
  /// (<see cref="IGraph.NodeDefaults"/>, <see cref="IGraph.GroupNodeDefaults"/>, <see cref="IGraph.EdgeDefaults"/>)
  /// </item>
  /// <item>Create a <see cref="TreeBuilder{TNode,TGroup}"/>.</item>
  /// <item>
  /// Set the items sources.
  /// At the very least the <see cref="NodesSource"/> is needed. Note that the <see cref="NodesSource"/>
  /// does not have to contain all nodes, as nodes that are implicitly specified through the <see cref="ChildProvider"/>
  /// are automatically added to the graph as well.
  /// If the items in the nodes collection are grouped somehow, then also set the <see cref="GroupsSource"/>
  /// property.
  /// </item>
  /// <item>
  /// Set up the bindings so that a graph structure can actually be created from the items sources.
  /// This involves setting up the <see cref="ChildProvider"/> property so that edges can be created.
  /// If the node objects don't actually contain their children objects, but instead identifiers
  /// of other node objects, then <see cref="ChildProvider"/> would return those identifiers and
  /// <see cref="NodeIdProvider"/> must be set to return that identifier when given a node object.
  /// </item>
  /// <item>
  /// If <see cref="GroupsSource"/> is set, then you also need to set the <see cref="GroupProvider"/> property
  /// to enable mapping nodes to groups. Just like with a node's children,
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
  /// know their neighbors, or <see cref="GraphBuilder{TNode,TEdge,TGroup}"/> which is
  /// a more general approach to creating arbitrary graphs.
  /// </para>
  /// </remarks>
  /// <y.note>
  /// This class serves as a convenient way to create trees or forests and has some limitations:
  /// <list type="bullet">
  /// <item>When populating the graph for the first time it will be cleared of all existing items.</item>
  /// <item>When using a <see cref="NodeIdProvider"/>, all nodes have to exist in the <see cref="NodesSource"/>. Nodes cannot be created on demand from IDs only.</item>
  /// <item>Elements manually created on the graph in between calls to <see cref="UpdateGraph"/> may not be preserved.</item>
  /// </list>
  /// <para>
  /// If updates get too complex it's often better to write the code interfacing with the graph by hand
  /// instead of relying on <see cref="TreeBuilder{TNode,TGroup}"/>.
  /// </para>
  /// </y.note>
  /// <typeparam name="TNode">The type of object nodes are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// <typeparam name="TGroup">The type of object group nodes are created from. This type must implement <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> properly.</typeparam>
  /// 
  /// <seealso cref="GraphBuilder{TNode,TEdge,TGroup}"/>
  /// <seealso cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/>
  public class TreeBuilder<TNode, TGroup>
  {
    private readonly MyAdjacentNodesGraphBuilder graphBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="TreeBuilder{TNode,TGroup}"/> class that
    /// operates on the given graph.
    /// </summary>
    /// <remarks>
    /// The <paramref name="graph"/> will be <see cref="GraphExtensions.Clear">cleared</see> and re-built 
    /// from the data in <see cref="NodesSource"/> and <see cref="GroupsSource"/>
    /// when <see cref="BuildGraph"/> is called.
    /// </remarks>
    public TreeBuilder([CanBeNull] IGraph graph = null) {
      graphBuilder = new MyAdjacentNodesGraphBuilder(this, graph);
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
    /// <see cref="ChildProvider"/>. In this case it suffices to include all root nodes.
    /// </remarks>
    [CanBeNull]
    public IEnumerable<TNode> NodesSource {
      get { return graphBuilder.NodesSource; }
      set { graphBuilder.NodesSource = value; }
    }

    /// <summary>
    /// Gets or sets the objects to be represented as group nodes of the <see cref="Graph"/>.
    /// </summary>
    [CanBeNull]
    public IEnumerable<TGroup> GroupsSource {
      get { return graphBuilder.GroupsSource; }
      set { graphBuilder.GroupsSource = value; }
    }

    /// <summary>
    /// Gets or sets a delegate that maps node objects to their identifier.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a node to its identifier. This is needed
    /// when <see cref="ChildProvider">children</see> are represented only by an identifier
    /// of nodes instead of pointing directly to the respective node objects.
    /// </remarks>
    /// <y.warning>
    /// The identifiers returned by the delegate must be stable and not change over time.
    /// Otherwise the <see cref="UpdateGraph">update mechanism</see> cannot determine
    /// whether nodes have been added or updated. For the same reason this property
    /// must not be changed after having built the graph once.
    /// </y.warning>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="ChildProvider"/>
    [CanBeNull]
    public Func<TNode, object> NodeIdProvider {
      get { return graphBuilder.NodeIdProvider; }
      set { graphBuilder.NodeIdProvider = value; }
    }

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
      get { return graphBuilder.NodeLabelProvider; }
      set { graphBuilder.NodeLabelProvider = value; }
    }

    /// <summary>
    /// Gets or sets a delegate that maps node objects to their child nodes.
    /// </summary>
    /// <remarks>
    /// This maps an object that represents a node to a set of other objects 
    /// that represent its child nodes.
    /// <para>
    /// If a <see cref="NodeIdProvider"/> is set, the returned objects must be the IDs of node objects
    /// instead of the node objects themselves.
    /// </para>
    /// </remarks>
    /// <seealso cref="NodesSource"/>
    /// <seealso cref="NodeIdProvider"/>
    [CanBeNull]
    public Func<TNode, IEnumerable> ChildProvider {
      get { return graphBuilder.SuccessorProvider; }
      set { graphBuilder.SuccessorProvider = value; }
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
    public Func<TNode, object> GroupProvider {
      get { return graphBuilder.GroupProvider; }
      set { graphBuilder.GroupProvider = value; }
    }

    /// <summary>
    /// Gets or sets a delegate that maps a node object representing the edge's target node to a label.
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
    [CanBeNull]
    public EdgeLabelProvider<TNode> EdgeLabelProvider {
      get { return graphBuilder.EdgeLabelProvider; }
      set { graphBuilder.EdgeLabelProvider = value; }
    }

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
    public Func<TGroup, object> GroupIdProvider {
      get { return graphBuilder.GroupIdProvider; }
      set { graphBuilder.GroupIdProvider = value; }
    }

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
      get { return graphBuilder.GroupLabelProvider; }
      set { graphBuilder.GroupLabelProvider = value; }
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
    public Func<TGroup, object> ParentGroupProvider {
      get { return graphBuilder.ParentGroupProvider; }
      set { graphBuilder.ParentGroupProvider = value; }
    }

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
      if (NodesSource == null || ChildProvider == null) {
        throw new InvalidOperationException("The NodesSource and ChildProvider properties must be set before calling BuildGraph.");
      }
      return graphBuilder.BuildGraph();
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
      graphBuilder.UpdateGraph();
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
      return graphBuilder.CreateNodeBase(graph, parent, labelData, nodeObject);
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
    [NotNull]
    protected virtual void UpdateNode([NotNull] IGraph graph, [NotNull] INode node, [CanBeNull] INode parent, [CanBeNull] object labelData, [CanBeNull] TNode nodeObject) {
      graphBuilder.UpdateNodeBase(graph, node, parent, labelData, nodeObject);
    }

    /// <summary>
    /// Creates an edge from the given <paramref name="source"/>, <paramref name="target"/>, and <paramref name="labelData"/>.
    /// </summary>
    /// <remarks>
    /// This method is called for every edge that is created either when <see cref="BuildGraph">building the graph</see>,
    /// or when new items appear in the <see cref="ChildProvider"/> when <see cref="UpdateGraph">updating it</see>.
    /// <para>
    /// The default behavior is to create the edge and create a label from <paramref name="labelData"/>,
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
    /// <returns>The created edge.</returns>
    /// <y.expert/>
    [NotNull]
    protected virtual IEdge CreateEdge([NotNull] IGraph graph, [NotNull] INode source, [NotNull] INode target, [CanBeNull] object labelData) {
      return graphBuilder.CreateEdgeBase(graph, source, target, labelData);
    }

    /// <summary>
    /// Updates an existing edge when the <see cref="UpdateGraph">graph is updated</see>.
    /// </summary>
    /// <remarks>
    /// This method is called during <see cref="UpdateGraph">updating the graph</see> for every edge that already
    /// exists in the graph where its corresponding source and target node objects also still exist.
    /// <para>
    /// Customizing how edges are updated is usually easier by adding an event handler to the <see cref="EdgeUpdated"/>
    /// event than by overriding this method.
    /// </para>
    /// </remarks>
    /// <param name="graph">The edge's containing graph.</param>
    /// <param name="edge">The edge to update.</param>
    /// <param name="labelData">The optional label data of the edge if an <see cref="NodeLabelProvider"/> is specified.</param>
    /// <y.expert/>
    protected virtual void UpdateEdge([NotNull] IGraph graph, [NotNull] IEdge edge, [CanBeNull] object labelData) {
      graphBuilder.UpdateEdgeBase(graph, edge, labelData);
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
      return graphBuilder.CreateGroupNodeBase(graph, labelData, groupObject);
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
      graphBuilder.UpdateGroupNodeBase(graph, groupNode, labelData, groupObject);
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
      add { graphBuilder.NodeCreated += value; }
      remove { graphBuilder.NodeCreated -= value; }
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
      add { graphBuilder.NodeUpdated += value; }
      remove { graphBuilder.NodeUpdated -= value; }
    }

    /// <summary>
    /// Occurs when an edge has been created.
    /// </summary>
    /// <remarks>
    /// This event can be used to further customize the created edge.
    /// <para>
    /// New edges are created either in response to calling <see cref="BuildGraph"/>,
    /// or in response to calling <see cref="UpdateGraph"/> when there are new items in
    /// <see cref="ChildProvider"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgeUpdated"/>
    public event EventHandler<GraphBuilderItemEventArgs<IEdge, object>> EdgeCreated {
      add { graphBuilder.EdgeCreated += value; }
      remove { graphBuilder.EdgeCreated -= value; }
    }

    /// <summary>
    /// Occurs when an edge has been updated.
    /// </summary>
    /// <remarks>
    /// This event can be used to update customizations added in an event handler for <see cref="EdgeCreated"/>.
    /// <para>
    /// Edges are updated in response to calling <see cref="UpdateGraph"/> for
    /// items that haven't been added anew in <see cref="ChildProvider"/> since the last
    /// call to <see cref="BuildGraph"/> or <see cref="UpdateGraph"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="EdgeCreated"/>
    public event EventHandler<GraphBuilderItemEventArgs<IEdge, object>> EdgeUpdated {
      add { graphBuilder.EdgeUpdated += value; }
      remove { graphBuilder.EdgeUpdated -= value; }
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
      add { graphBuilder.GroupNodeCreated += value; }
      remove { graphBuilder.GroupNodeCreated -= value; }
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
      add { graphBuilder.GroupNodeUpdated += value; }
      remove { graphBuilder.GroupNodeUpdated -= value; }
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
      return graphBuilder.GetSourceObject(item);
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
      return graphBuilder.GetNode(nodeObject);
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
      return graphBuilder.GetGroup(groupObject);
    }

    /// <summary>
    /// A <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/> that provides the additional 
    /// <see cref="TreeBuilder{TNode,TGroup}.EdgeLabelProvider" /> and makes protected methods work with the decorator
    /// pattern.
    /// </summary>
    /// <remarks>
    /// Protected methods work in the following way: Each overridden protected method delegates to
    /// the corresponding method of the <see cref="TreeBuilder{TNode,TGroup}"/> instance to allow users to override
    /// it. Then, that method calls a new method of this class that provides access to the original
    /// base implementation.
    /// </remarks>
    private sealed class MyAdjacentNodesGraphBuilder : AdjacentNodesGraphBuilder<TNode, TGroup>
    {
      private readonly TreeBuilder<TNode, TGroup> TreeBuilder;

      public MyAdjacentNodesGraphBuilder(TreeBuilder<TNode, TGroup> simpleTreeBuilder, IGraph graph)
        : base(graph) {
        this.TreeBuilder = simpleTreeBuilder;
      }

      protected override INode CreateNode(IGraph graph, INode parent, object labelData, TNode nodeObject) {
        return TreeBuilder.CreateNode(graph, parent, labelData, nodeObject);
      }

      public INode CreateNodeBase(IGraph graph, INode parent, object labelData, TNode data) {
        return base.CreateNode(graph, parent, labelData, data);
      }

      protected override IEdge CreateEdge(IGraph graph, INode source, INode target, object labelData) {
        return TreeBuilder.CreateEdge(graph, source, target, labelData);
      }

      public IEdge CreateEdgeBase(IGraph graph, INode sourceNode, INode targetNode, object labelData) {
        return base.CreateEdge(graph, sourceNode, targetNode, labelData);
      }

      protected override INode CreateGroupNode(IGraph graph, object labelData, TGroup groupObject) {
        return TreeBuilder.CreateGroupNode(graph, labelData, groupObject);
      }

      public INode CreateGroupNodeBase(IGraph graph, object labelData, TGroup data) {
        return base.CreateGroupNode(graph, labelData, data);
      }

      protected override void UpdateNode(IGraph graph, INode node, INode parent, object labelData, TNode nodeObject) {
        TreeBuilder.UpdateNode(graph, node, parent, labelData, nodeObject);
      }

      public void UpdateNodeBase(IGraph graph, INode node, INode parent, object labelData, TNode data) {
        base.UpdateNode(graph, node, parent, labelData, data);
      }

      protected override void UpdateEdge(IGraph graph, IEdge edge, object labelData) {
        TreeBuilder.UpdateEdge(graph, edge, labelData);
      }

      public void UpdateEdgeBase(IGraph graph, IEdge edge, object labelData) {
        base.UpdateEdge(graph, edge, labelData);
      }

      protected override void UpdateGroupNode(IGraph graph, INode groupNode, object labelData, TGroup groupObject) {
        TreeBuilder.UpdateGroupNode(graph, groupNode, labelData, groupObject);
      }

      public void UpdateGroupNodeBase(IGraph graph, INode groupNode, object labelData, TGroup data) {
        base.UpdateGroupNode(graph, groupNode, labelData, data);
      }
    }
  }
}
