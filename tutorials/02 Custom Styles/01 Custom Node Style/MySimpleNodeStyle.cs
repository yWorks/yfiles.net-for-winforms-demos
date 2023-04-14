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
using System.Drawing;
using System.Drawing.Drawing2D;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// A very simple implementation of an <see cref="INodeStyle"/>
  /// that uses the convenience class <see cref="NodeStyleBase{TVisual}"/>
  /// as base class.
  /// </summary>
  public class MySimpleNodeStyle : NodeStyleBase<IVisual>
  {

    /// <summary>
    /// Default color
    /// </summary>
    public static Color Color { get { return Color.FromArgb(200, 0, 130, 180); } }

    /// <summary>
    /// Creates the <see cref="IVisual"/> which renders the node.
    /// </summary>
    /// <remarks>
    /// An <see cref="IVisual"/> is an object which knows how to render its visualization
    /// by implementing a <see cref="IVisual.Paint"/> method.
    /// 
    /// The advantage of this approach over directly implementing a paint method
    /// is that the visual might keep some pre-calculated state which doesn't have
    /// to be calculated for each rendering.
    /// 
    /// In this step we don't make use of this, yet. But we will in later steps.
    /// </remarks>
    /// <param name="context">The current render context. The render context provides
    /// additional information like the <see cref="CanvasControl"/> where the node is rendered.</param>
    /// <param name="node">The node to render</param>
    /// <returns>A visual which renders the given node.</returns>
    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return new BallVisual(node);
    }

    /// <summary>
    /// Simple implementation of <see cref="IVisual"/> which renders the node.
    /// </summary>
    private class BallVisual : IVisual
    {
      private readonly INode node;

      /// <summary>
      /// Creates a new instance for the given node.
      /// </summary>
      /// <param name="node"></param>
      public BallVisual(INode node) {
        this.node = node;
      }

      /// <summary>
      /// Renders the node in the given graphics context.
      /// </summary>
      /// <param name="context">The render context.</param>
      /// <param name="g">The graphics context.</param>
      public void Paint(IRenderContext context, Graphics g) {
        var layout = node.Layout;

        Color color = Color;

        // min needed for reflection effect calculation
        double min = Math.Min(layout.Width, layout.Height);

        LinearGradientBrush brush = new LinearGradientBrush(layout.GetTopLeft().ToPoint(),
            new PointF((float) (layout.X + (layout.Width/3)),
                (float) (layout.Y + (layout.Height/3))),

            Color.FromArgb((byte) Math.Max(0, color.A - 50),
                (byte) Math.Min(255, color.R*1.7),
                (byte) Math.Min(255, color.G*1.7),
                (byte) Math.Min(255, color.B*1.7)),
            Color.FromArgb((byte) Math.Max(0, color.A - 50),
                (byte) Math.Min(255, color.R*1.4),
                (byte) Math.Min(255, color.G*1.4),
                (byte) Math.Min(255, color.B*1.4))) {
                  WrapMode = WrapMode.TileFlipXY
                };

        // Draw main ellipse
        g.FillEllipse(brush, (float) layout.X, (float) layout.Y, (float) layout.Width, (float) layout.Height);

        // Draw reflections
        g.FillEllipse(Brushes.AliceBlue, (float) (layout.X + layout.Width/4.9f), (float) (layout.Y + layout.Height/4.9f), (float) min/7f, (float) min/7f);
        g.FillEllipse(Brushes.White, (float) (layout.X + layout.Width*0.2f), (float) (layout.Y + layout.Height*0.2f), (float) min*0.1f, (float) min*0.1f);

        // Create moon-shaped reflection
        GraphicsPath reflection3 = new GraphicsPath();
        PointF startPoint = new PointF((float) (layout.X + layout.Width/2.5), (float) (layout.Y + layout.Height/10*9));
        PointF endPoint = new PointF((float) (layout.X + layout.Width/10*9), (float) (layout.Y + layout.Height/2.5));
        PointF ctrlPoint1 = new PointF(startPoint.X + (endPoint.X - startPoint.X)/2, (float) (layout.Y + layout.Height));
        PointF ctrlPoint2 = new PointF((float) (layout.X + layout.Width), startPoint.Y + (endPoint.Y - startPoint.Y)/2);
        PointF ctrlPoint3 = new PointF(ctrlPoint1.X, (float) (ctrlPoint1.Y - layout.Height/10));
        PointF ctrlPoint4 = new PointF((float) (ctrlPoint2.X - layout.Width/10), ctrlPoint2.Y);

        reflection3.AddBezier(startPoint, ctrlPoint1, ctrlPoint2, endPoint);
        reflection3.AddBezier(endPoint, ctrlPoint4, ctrlPoint3, startPoint);

        g.FillPath(Brushes.AliceBlue, reflection3);
      }
    }
  }
}
