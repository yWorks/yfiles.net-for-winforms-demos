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



using System.Drawing;
using System.Drawing.Drawing2D;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Complete.LargeGraphAggregation
{
  /// <summary>
  /// An implementation of <see cref="NodeStyleBase{TVisual}"/>
  /// for nodes which represent Aggregates.
  /// </summary>
  public class AggregationNodeStyle : NodeStyleBase<VisualGroup>
  {
    /// <summary>
    /// Creates the rendering visual.
    /// </summary>
    /// <remarks>
    /// An ellipse with a + or - symbol.
    /// </remarks>
    /// <param name="context">The render context.</param>
    /// <param name="node">The node.to render.</param>
    /// <returns>A visual representation.</returns>
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var vg = new AggregationVisual();
      var info = (AggregationNodeInfo)node.Tag;
      vg.HasNode = info.Aggregate.Node != null;
      vg.IsAggregated = info.IsAggregated;

      // draw a grey ellipse
      // if the aggregate represents a node draw a solid border. Draw a dashed border otherwise.
      var pen = info.Aggregate.Node == null
          ? new Pen(Brushes.LightGray) { DashStyle = DashStyle.Dash, DashPattern = new[] { 2f, 2f } }
          : Pens.LightGray;
      vg.Add(new EllipseVisual(new RectD(PointD.Origin, node.Layout.GetSize())) {
          Pen = pen,
          Brush = new SolidBrush(info.IsAggregated
              ? Color.FromArgb(0x11, 0x6c, 0x91, 0xbf)
              : Color.FromArgb(0x09, 0x6c, 0x91, 0xbf))
      });
      // draw a + if aggregated, - otherwise
      GeneralPath gp = new GeneralPath();
      if (info.IsAggregated) {
        gp.MoveTo(0, -4);
        gp.LineTo(0, 4);
        gp.MoveTo(-4, 0);
        gp.LineTo(4, 0);
      } else {
        gp.MoveTo(-4,0);
        gp.LineTo(4,0);
      }
      GeneralPathVisual path = new GeneralPathVisual(gp);
      path.Transform = new Matrix(1,0,0,1, (float) (node.Layout.Width * 0.5), (float) (node.Layout.Height * 0.5));
      path.Pen = new Pen(Brushes.DimGray){Width = 1.5f};
      vg.Add(path);
      vg.Transform = new Matrix(1, 0, 0, 1, (float) node.Layout.X, (float) node.Layout.Y);
      return vg;
    }

    /// <summary>
    /// Updates the visual.
    /// </summary>
    /// <remarks>
    /// Updates the layout if necessary and if nothing else changed.
    /// If something changed: create a completely new visual.
    /// </remarks>
    /// <param name="context">The render context.</param>
    /// <param name="oldVisual">The visual to update.</param>
    /// <param name="node">The node to render.</param>
    /// <returns>The updated visual (might be a new reference).</returns>
    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldVisual, INode node) {
      var vg = oldVisual as AggregationVisual;
      var info = node.Tag as AggregationNodeInfo;
      if (vg == null || info == null || vg.HasNode != (info.Aggregate.Node != null) || vg.IsAggregated != info.IsAggregated) {
        // need a full redraw
        return CreateVisual(context, node);
      }
      // Update the transform if the node has moved
      var layout = node.Layout;
      if (oldVisual.Transform.OffsetX != (float) layout.X || oldVisual.Transform.OffsetY != (float) layout.Y) {
        oldVisual.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      }
      // Update the size if it has changed
      var ellipseVisual = (EllipseVisual) vg.Children[0];
      ellipseVisual.Bounds = new RectD(PointD.Origin, layout.GetSize());
      // Update the position of the expand/collapse icon if the size of the node has changed
      var pathVisual = (GeneralPathVisual) vg.Children[1];
      pathVisual.Transform = new Matrix(1,0,0,1, (float) (layout.Width * 0.5), (float) (layout.Height * 0.5));
      return oldVisual;
    }

    /// <summary>
    /// Returns the outline of the node.
    /// </summary>
    /// <remarks>
    /// Overriding this method will yield proper hit tests and edge intersection tests.
    /// </remarks>
    /// <param name="node"></param>
    /// <returns></returns>
    protected override GeneralPath GetOutline(INode node) {
      var path = new GeneralPath();
      path.AppendEllipse(node.Layout, false);
      return path;
    }

    /// <summary>
    /// A visual group which keeps the current data state when rendered.
    /// </summary>
    private class AggregationVisual : VisualGroup
    {
      public bool IsAggregated { get; set; }
      public bool HasNode { get; set; }
    }
  }
}
