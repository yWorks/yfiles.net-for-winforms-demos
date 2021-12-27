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

using yWorks.Controls;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.BezierEdgeStyle
{
  /// <summary>
  /// Custom <see cref="OverviewGraphVisualCreator"/> that renders Bézier edges with curves.
  /// </summary>
  public class BezierOverviewVisualCreator : OverviewGraphVisualCreator
  {
    /// <summary>
    /// Shared style for all Bézier edges, just without arrows.
    /// </summary>
    private readonly IEdgeStyle bezierEdgeStyle = new yWorks.Graph.Styles.BezierEdgeStyle();

    public BezierOverviewVisualCreator(IGraph graph) : base(graph) { }

    protected override IEdgeStyle GetEdgeStyle(IEdge edge) {
      if (edge.Style is yWorks.Graph.Styles.BezierEdgeStyle) {
        return bezierEdgeStyle;
      }
      return base.GetEdgeStyle(edge);
    }
  }
}
