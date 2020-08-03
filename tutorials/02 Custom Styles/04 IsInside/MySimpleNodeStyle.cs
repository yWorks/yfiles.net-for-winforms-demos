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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// A very simple implementation of an <see cref="INodeStyle"/>
  /// that uses the convenience class <see cref="NodeStyleBase{TVisual}"/>
  /// as the base class.
  /// </summary>
  public class MySimpleNodeStyle : NodeStyleBase<IVisual>
  {

    ////////////////////////////////////////////////////
    //////////////// New in this sample ////////////////
    ////////////////////////////////////////////////////

    /// <summary>
    /// Gets the outline of the node, an ellipse in this case
    /// </summary>
    /// <remarks>
    /// This allows for correct edge path intersection calculation, among others.
    /// </remarks>
    protected override GeneralPath GetOutline(INode node) {
//      return base.GetOutline(node);
      var rect = node.Layout.ToRectD();
      var outline = new GeneralPath();
      outline.AppendEllipse(rect, false);
      return outline;
    }

    /// <summary>
    /// Exact geometric check whether the given point lies inside the node. This is important for intersection calculation, among others.
    /// </summary>
    protected override bool IsInside(INode node, PointD point) {
//      return base.IsInside(node, point);
      return GeomUtilities.EllipseContains(node.Layout.ToRectD(), point, 0);
    }

    ////////////////////////////////////////////////////

    public MySimpleNodeStyle() {
      NodeColor = Color.FromArgb(0xc8, 0x00, 0x82, 0xb4);
    }

    /// <summary>
    /// Gets or sets the fill color of the node.
    /// </summary>
    [DefaultValue(typeof(Color), "#C80082B4")]
    public Color NodeColor { get; set; }

    /// <summary>
    /// Determines the color to use for filling the node.
    /// </summary>
    /// <remarks>
    /// This implementation uses the <see cref="NodeColor"/> property unless
    /// the <see cref="ITagOwner.Tag"/> of the <see cref="INode"/> is of type <see cref="Color"/>, 
    /// in which case that color overrides this style's setting.
    /// </remarks>
    /// <param name="node">The node to determine the color for.</param>
    /// <returns>The color for filling the node.</returns>
    protected virtual Color GetNodeColor(INode node) {
      // the color can be obtained from the "business data" that can be associated with
      // each node, or use the value from this instance.
      return node.Tag is Color ? (Color)node.Tag : NodeColor;
    }

    /// <summary>
    /// Creates the <see cref="IVisual"/> which renders the node.
    /// </summary>
    /// <param name="context">The current render context. The render context provides
    /// additional information like the <see cref="CanvasControl"/> where the node is rendered.</param>
    /// <param name="node">The node to render</param>
    /// <returns>A visual which renders the given node.</returns>
    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return new BallVisual(node, GetNodeColor(node));
    }

    /// <summary>
    /// Simple implementation of <see cref="IVisual"/> which renders the node.
    /// </summary>
    private class BallVisual : IVisual
    {
      private readonly INode node;
      private readonly Color color;

      /// <summary>
      /// Creates a new instance for the given node.
      /// </summary>
      /// <param name="node"></param>
      /// <param name="color"></param>
      public BallVisual(INode node, Color color) {
        this.node = node;
        this.color = color;
      }

      /// <summary>
      /// Renders the node in the given graphics context.
      /// </summary>
      /// <param name="context">The render context.</param>
      /// <param name="g">The graphics context.</param>
      public void Paint(IRenderContext context, Graphics g) {
        var layout = node.Layout;

        DrawShadow(context, layout.ToRectD(), g);

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

      #region Drop Shadow

      private const int dropShadowOffset = 4;

      /// <summary>
      /// Draws the drop shadow for the style
      /// </summary>
      private void DrawShadow(IRenderContext renderContext, RectD layout, Graphics g) {
        // Only draw the shadow if the zoomlevel is > 0.3
        if (renderContext.Zoom > 0.3) {
          g.DrawImage(CreateShadow(layout.Size), (float) (layout.X - (layout.Width*0.5d) + dropShadowOffset),
              (float) (layout.Y - (layout.Height*0.5d) + dropShadowOffset), (float) (layout.Width*2),
              (float) (layout.Height*2));
        }
      }

      private Bitmap CreateShadow(SizeD size) {
        var width = Math.Max((int) size.Width, 8);
        var height = Math.Max((int) size.Height, 8);
        var dropShadow = new Bitmap(width * 2, height * 2, PixelFormat.Format32bppArgb);
        using (Graphics graphics = Graphics.FromImage(dropShadow)) {
          graphics.FillEllipse(Brushes.Black, width/2, height/2, width, height);
        }

        // Calculate the blur
        DropShadowSupport.DropShadow(dropShadow, Color.FromArgb(51, 0, 0, 0), DropShadowSupport.Gaussian1DScaled(2, 9));
        return dropShadow;
      }

      #endregion
    }
  }
}
