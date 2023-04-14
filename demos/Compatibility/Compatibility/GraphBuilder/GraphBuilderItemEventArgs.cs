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

using yWorks.Graph;
using yWorks.Utils;

namespace yWorks.DataBinding.Compatibility
{
  /// <summary>
  /// Event arguments for item events in <see cref="GraphBuilder{TNode,TEdge,TGroup}"/>,
  /// <see cref="AdjacentNodesGraphBuilder{TNode,TGroup}"/>, and <see cref="TreeBuilder{TNode,TGroup}"/>.
  /// </summary>
  /// <typeparam name="TItem">The type of the item contained in the argument.</typeparam>
  /// <typeparam name="TObject">The type of object that the item was created from.</typeparam>
  public class GraphBuilderItemEventArgs<TItem, TObject> : ItemEventArgs<TItem>
  {
    /// <summary>
    /// Gets the graph that can be used to modify the <see cref="ItemEventArgs{T}.Item"/>.
    /// </summary>
    public IGraph Graph { get; private set; }

    /// <summary>
    /// Gets the object the <see cref="ItemEventArgs{T}.Item"/> has been created from.
    /// </summary>
    public TObject SourceObject { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="GraphBuilderItemEventArgs{TItem,TObject}"/>
    /// class with the given graph, item, and source object.
    /// </summary>
    /// <param name="graph">The graph that can be used to modify <paramref name="item"/>.</param>
    /// <param name="item">The item created from <paramref name="sourceObject"/>.</param>
    /// <param name="sourceObject">The object <paramref name="item"/> was created from.</param>
    public GraphBuilderItemEventArgs(IGraph graph, TItem item, TObject sourceObject)
        : base(item) {
      Graph = graph;
      SourceObject = sourceObject;
    }
  }

  /// <summary>
  /// A delegate that maps an edge represented by its source and target node object to a label.
  /// </summary>
  /// <typeparam name="TNode">The type of the node objects.</typeparam>
  /// <param name="source">The source node object.</param>
  /// <param name="target">The target node object.</param>
  /// <returns>The label data to use for the edge between <paramref name="source"/> and <paramref name="target"/>.</returns>
  public delegate object EdgeLabelProvider<TNode>(TNode source, TNode target);
}
