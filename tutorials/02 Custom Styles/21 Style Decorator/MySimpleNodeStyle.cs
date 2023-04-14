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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using yWorks.Controls;
using yWorks.Controls.Input;
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
  /// <remarks>
  /// In this tutorial step, the label edge rendering code was removed from this
  /// style and moved to the new class <see cref="MyNodeStyleDecorator"/> in order
  /// to demonstrate wrapping of existing styles.
  /// </remarks>
  public class MySimpleNodeStyle : NodeStyleBase<VisualGroup>
  {

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
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var layout = node.Layout.ToRectD();

      // Draw a drop shadow
      var group = new VisualGroup();
      // Only draw the shadow if the zoomlevel is > 0.3
      if (context.Zoom > 0.3) {
        group.Children.Add(new ImageVisual {
          Image = CreateShadow(layout.Size),
          Rectangle = new RectD(-(layout.Width*0.5d) + dropShadowOffset, -(layout.Height*0.5d) + dropShadowOffset, layout.Width*2,
                layout.Height*2)
        });
      } else {
        group.Add(null);
      }

      // create the actual node visualization
      var ballVisual = new BallVisual();
      ballVisual.Update(layout.Size, GetNodeColor(node));
      group.Add(ballVisual);

      // now translate all to the final location
      group.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      return group;
    }

    /// <summary>
    /// Takes the old Visual and tries to update it.
    /// </summary>
    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup group, INode node) {
      var layout = node.Layout.ToRectD();

      // Update the drop shadow
      // Only draw the shadow if the zoomlevel is > 0.3
      var dropShadowVisual = group.Children[0] as ImageVisual;
      if (context.Zoom > 0.3) {
        if (dropShadowVisual != null) {
          if (dropShadowVisual.Rectangle.Width != layout.Width * 2 || dropShadowVisual.Rectangle.Height != layout.Height * 2) {
            dropShadowVisual.Image = CreateShadow(layout.Size);
          }
          // if the drop shadow already exists only update its bounds
          dropShadowVisual.Rectangle = new RectD(-(layout.Width*0.5d) + dropShadowOffset, -(layout.Height*0.5d) + dropShadowOffset, layout.Width*2, layout.Height*2);
        } else {
          // otherwise create a new one
          group.Children[0] = new ImageVisual {
            Image = CreateShadow(layout.Size),
            Rectangle = new RectD(-(layout.Width*0.5d) + dropShadowOffset, -(layout.Height*0.5d) + dropShadowOffset, layout.Width*2, layout.Height*2)
          };
        }
      } else if (dropShadowVisual != null) {
        // if there is a drop shadow which shouldn't be drawn anymore: remove it
        group.Children[0] = null;
      }
      // update the actual node visualization with the current size and color
      var ballVisual = (BallVisual) group.Children[1];
      ballVisual.Update(layout.Size, GetNodeColor(node));

      // update the translation
      group.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      return group;
    }


    /// <summary>
    /// Simple implementation of <see cref="IVisual"/> which renders the node.
    /// </summary>
    private class BallVisual : IVisual
    {
      private SizeD size;
      private Color color;
      private Brush brush;

      /// <summary>
      /// Sets color and size.
      /// </summary>
      /// <remarks>
      /// Creates a new gradient brush
      /// if and only if a change in color or size made this necessary.
      /// </remarks>
      /// <param name="size"></param>
      /// <param name="color"></param>
      public void Update(SizeD size, Color color) {
        if (!size.Equals(this.size) || !color.Equals(this.color)) {
          // color or size have changed: update the gradient brush
          brush = new LinearGradientBrush(new Point(),
              new PointF((float) (size.Width/3),
                  (float) (size.Height/3)),

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
        }
        this.size = size;
        this.color = color;
      }

      /// <summary>
      /// Renders the node in the given graphics context.
      /// </summary>
      /// <param name="context">The render context.</param>
      /// <param name="g">The graphics context.</param>
      public void Paint(IRenderContext context, Graphics g) {
        // min needed for reflection effect calculation
        double min = Math.Min(size.Width, size.Height);

        // Draw main ellipse
        g.FillEllipse(brush, 0, 0, (float) size.Width, (float) size.Height);

        // Draw reflections
        g.FillEllipse(Brushes.AliceBlue, (float) ( size.Width/4.9f), (float) ( size.Height/4.9f), (float) min/7f, (float) min/7f);
        g.FillEllipse(Brushes.White, (float) ( size.Width*0.2f), (float) ( size.Height*0.2f), (float) min*0.1f, (float) min*0.1f);

        // Create moon-shaped reflection
        GraphicsPath reflection3 = new GraphicsPath();
        PointF startPoint = new PointF((float) ( size.Width/2.5), (float) ( size.Height/10*9));
        PointF endPoint = new PointF((float) ( size.Width/10*9), (float) ( size.Height/2.5));
        PointF ctrlPoint1 = new PointF(startPoint.X + (endPoint.X - startPoint.X)/2, (float) ( size.Height));
        PointF ctrlPoint2 = new PointF((float) ( size.Width), startPoint.Y + (endPoint.Y - startPoint.Y)/2);
        PointF ctrlPoint3 = new PointF(ctrlPoint1.X, (float) (ctrlPoint1.Y - size.Height/10));
        PointF ctrlPoint4 = new PointF((float) (ctrlPoint2.X - size.Width/10), ctrlPoint2.Y);

        reflection3.AddBezier(startPoint, ctrlPoint1, ctrlPoint2, endPoint);
        reflection3.AddBezier(endPoint, ctrlPoint4, ctrlPoint3, startPoint);

        g.FillPath(Brushes.AliceBlue, reflection3);
      }
    }

    #region Drop Shadow

    private const int dropShadowOffset = 4;

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

    #region Painting Helper Methods

    /// <summary>
    /// Gets the outline of the node, an ellipse in this case
    /// </summary>
    /// <remarks>
    /// This allows for correct edge path intersection calculation, among others.
    /// </remarks>
    protected override GeneralPath GetOutline(INode node) {
      var rect = node.Layout.ToRectD();
      var outline = new GeneralPath();
      outline.AppendEllipse(rect, false);
      return outline;
    }

    /// <summary>
    /// Get the bounding box of the node
    /// </summary>
    /// <remarks>
    /// This is used for bounding box calculations and includes the visual shadow.
    /// </remarks>
    protected override RectD GetBounds(ICanvasContext context, INode node) {
      RectD bounds = node.Layout.ToRectD();
      // expand bounds to include drop shadow
      return bounds + new InsetsD(0, 0, 3, 3);
    }

    /// <summary>
    /// Hit test which considers HitTestRadius specified in canvasContext
    /// </summary>
    /// <returns>True if p is inside node.</returns>
    protected override bool IsHit(IInputModeContext context, PointD location, INode node) {
      return GeomUtilities.EllipseContains(node.Layout.ToRectD(), location, context.HitTestRadius);
    }

    /// <summary>
    /// Checks if a node is inside a certain box. Considers HitTestRadius.
    /// </summary>
    /// <returns>True if the box intersects the elliptical shape of the node. Also true if box lies completely inside node.</returns>
    protected override bool IsInBox(IInputModeContext context, RectD rectangle, INode node) {
      // early exit if not even the bounds are contained in the box
      if (!base.IsInBox(context, rectangle, node)) {
        return false;
      }

      double eps = context.HitTestRadius;

      var outline = GetOutline(node);
      if (outline == null) return false;

      if (outline.Intersects(rectangle, eps)) {
        return true;
      }
      if (outline.PathContains(rectangle.TopLeft, eps) && outline.PathContains(rectangle.BottomRight, eps)) {
        return true;
      }
      return (rectangle.Contains(node.Layout.ToRectD().TopLeft) && rectangle.Contains(node.Layout.ToRectD().BottomRight));
    }

    /// <summary>
    /// Exact geometric check whether a point p lies inside the node. This is important for intersection calculation, among others.
    /// </summary>
    protected override bool IsInside(INode node, PointD point) {
      return GeomUtilities.EllipseContains(node.Layout.ToRectD(), point, 0);
    }

    #endregion

  }
}
