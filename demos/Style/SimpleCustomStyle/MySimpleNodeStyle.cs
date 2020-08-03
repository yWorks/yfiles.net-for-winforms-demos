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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;
using yWorks.Utils;

namespace Demo.yFiles.Graph.SimpleCustomStyle
{
  /// <summary>
  /// A very simple implementation of an <see cref="INodeStyle"/>
  /// that uses the convenience class <see cref="NodeStyleBase{TVisual}"/>
  /// as base class.
  /// </summary>
  public class MySimpleNodeStyle : NodeStyleBase<VisualGroup>
  {

    /// <summary>
    /// Default color which is used if no Color is stored in the node's tag
    /// </summary>
    public static Color Color {
      get { return Color.FromArgb(200, 0, 130, 180); }
    }


    /// <summary>
    /// Overridden to take the connection lines to the label into account.
    /// </summary>
    /// <remarks>
    /// Otherwise label intersection lines might not be painted if the node is outside 
    /// of the clipping bounds.
    /// </remarks>
    protected override bool IsVisible(ICanvasContext context, RectD rectangle, INode node) {
      if (base.IsVisible(context, rectangle, node)) {
        return true;
      }
      // check for labels connection lines 
      rectangle = rectangle.GetEnlarged(10);
      foreach (var label in node.Labels) {
        if (rectangle.IntersectsLine(node.Layout.GetCenter(), label.GetLayout().GetCenter())) {
          return true;
        }
      }
      return false;
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

      // Check if a Color is stored in the node's tag
      Color color = Color;
      if (node.Tag is Color) {
        color = (Color) node.Tag;
      }

      // create the actual node visualization
      var ballVisual = new BallVisual();
      ballVisual.Update(layout.Size, color);
      group.Add(ballVisual);

      // Draw 'edges' connecting nodes with their labels
      var labelsGroup = new VisualGroup();
      RenderLabelEdges(context, node, labelsGroup);
      group.Add(labelsGroup);

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

      // Check if a Color is stored in the node's tag
      Color color = Color;
      if (node.Tag is Color) {
        color = (Color) node.Tag;
      }
      // update the actual node visualization with the current size and color
      var ballVisual = (BallVisual) group.Children[1];
      ballVisual.Update(layout.Size, color);

      // Update 'edges' connecting nodes with their labels
      RenderLabelEdges(context, node, (VisualGroup)group.Children[2]);

      // update the translation
      group.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      return group;

    }

    /// <summary>
    /// Draws the edge-like connectors from a node to its labels
    /// </summary>
    private void RenderLabelEdges(IRenderContext context, INode node, VisualGroup container) {
      int count = 0;
      if (node.Labels.Count > 0) {
        // Create a SimpleEdge which will be used as a dummy for the rendering
        SimpleEdge simpleEdge = new SimpleEdge(null, null);
        // Assign the style
        simpleEdge.Style = new MySimpleEdgeStyle {PathThickness = 2};

        // Create a SimpleNode which provides the sourceport for the edge but won't be drawn itself
        SimpleNode sourceDummyNode = new SimpleNode {
          Layout = new RectD(0, 0, node.Layout.Width, node.Layout.Height),
          Style = node.Style
        };


        // Set sourceport to the port of the node using a dummy node that is located at the origin.
        simpleEdge.SourcePort = new SimplePort(sourceDummyNode, FreeNodePortLocationModel.NodeCenterAnchored);

        // Create a SimpleNode which provides the targetport for the edge but won't be drawn itself
        SimpleNode targetDummyNode = new SimpleNode();

        // Create port on targetDummynode for the label target
        targetDummyNode.Ports =
            new ListEnumerable<IPort>(new[]
            {new SimplePort(targetDummyNode, FreeNodePortLocationModel.NodeCenterAnchored)});
        simpleEdge.TargetPort = new SimplePort(targetDummyNode, FreeNodePortLocationModel.NodeCenterAnchored);

        var topLeft = node.Layout.GetTopLeft();
        var labelLocations = node.Labels.Select(l => l.GetLayout().GetCenter() - topLeft);

        // Render one edge for each label
        foreach (PointD labelLocation in labelLocations) {
          // move the dummy node to the location of the label
          targetDummyNode.Layout = new MutableRectangle(labelLocation, SizeD.Zero);

          // now create the visual using the style interface:
          IEdgeStyleRenderer renderer = simpleEdge.Style.Renderer;
          IVisualCreator creator = renderer.GetVisualCreator(simpleEdge, simpleEdge.Style);
          if (container.Children.Count > count) {
            container.Children[count] = creator.UpdateVisual(context, container.Children[count]);
          } else {
            container.Children.Add(creator.CreateVisual(context));
          }
          count++;
        }
      }
      // remove superfluous visuals
      while (container.Children.Count > count) {
        container.Children.RemoveAt(container.Children.Count - 1);
      }      
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
      DropShadow(dropShadow, Color.FromArgb(51, 0, 0, 0), Gaussian1DScaled(2, 9));
      return dropShadow;
    }

    /// <summary>
    /// Creates a Drop Shadow from a given bitmap.
    /// </summary>
    /// <param name="bitmap">The bitmap to create a drop shadow for.</param>
    /// <param name="color">The color of the shadow.</param>
    /// <param name="kernel">The kernel to use</param>
    private static void DropShadow(Bitmap bitmap, Color color, int[] kernel) {
      BitmapData data =
          bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
              PixelFormat.Format32bppArgb);

      try {
        int count1 = data.Width*data.Height;
        int count = data.Width*data.Height*4;
        byte[] src = new byte[count];
        byte[] src2 = new byte[count1];
        byte[] target = new byte[count1];
        Marshal.Copy(data.Scan0, src, 0, count);

        int c = 0;
        for (int i = 3; i < src.Length; i += 4, c++) {
          src2[c] = src[i];
        }


        ApplyKernelHorizontallyA(data, kernel, src2, target);
        ApplyKernelVerticallyA(data, kernel, target, src2);
        c = 0;
        byte b = color.B;
        byte r = color.R;
        byte g = color.G;
        byte a = color.A;
        for (int i = 0; i < src.Length - 4;) {
          src[i++] = b;
          src[i++] = g;
          src[i++] = r;
          src[i++] = (byte) ((src2[c++]*a)/255);
        }

        Marshal.Copy(src, 0, data.Scan0, count);
        return;
      } catch (SecurityException) {
        // ok - we cannot do that - lets just use the slow version of the code...
      } catch (MethodAccessException) {
        // ok - we cannot do that - lets just use the slow version of the code...
      } finally {
        bitmap.UnlockBits(data);
      }
      // slow version but no securitypermission required.
      {
        int count1 = bitmap.Width*bitmap.Height;
        byte[] src2 = new byte[count1];
        byte[] target = new byte[count1];

        for (int y = 0; y < bitmap.Height; y++) {
          int offset = y*bitmap.Width;
          for (int x = 0; x < bitmap.Width; x++) {
            Color pixel = bitmap.GetPixel(x, y);
            src2[x + offset] = pixel.A;
          }
        }

        ApplyKernelHorizontallyA(data, kernel, src2, target);
        ApplyKernelVerticallyA(data, kernel, target, src2);

        for (int y = 0; y < bitmap.Height; y++) {
          int offset = y*bitmap.Width;
          for (int x = 0; x < bitmap.Width; x++) {
            Color newPixel = Color.FromArgb((byte) ((src2[x + offset]*color.A)/255), color.R, color.G, color.B);
            bitmap.SetPixel(x, y, newPixel);
          }
        }
      }
    }

    private static void ApplyKernelHorizontallyA(BitmapData data, int[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length + 1)/2;
      int kend = kernel.Length - k2;
      int yend = data.Height;
      int xend = data.Width - kend;
      for (int y = 0; y < yend; y++) {
        int offset = y*data.Width;
        offset += k2;
        for (int x = k2; x < xend; x++) {
          int fa = 0;
          int k = 0;
          int p = offset - k2;
          for (int i = -k2; i < kend; i++) {
            fa += src[p++]*kernel[k++];
          }
          target[offset++] = (byte) Math.Min(255, Math.Max(fa/256, 0));
        }
      }
    }

    private static void ApplyKernelVerticallyA(BitmapData data, int[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length + 1)/2;
      int kend = kernel.Length - k2;
      int yend = data.Height - kend;
      int xend = data.Width;
      int deltaY = data.Width;
      int d1 = k2*deltaY;
      for (int x = 0; x < xend; x++) {
        int offset = d1 + x;
        for (int y = k2; y < yend; y++, offset += deltaY) {
          int srcOffset = offset;
          int fa = 0;
          int k = 0;
          int l = srcOffset - d1;
          for (int i = -k2; i < kend; i++, l += deltaY) {
            fa += src[l]*kernel[k++];
          }
          target[srcOffset] = (byte) Math.Min(255, Math.Max(fa/256, 0));
        }
      }
    }

    /// <summary>
    /// Creates a 1 dimensional gaussian kernel normalized to integer values between 0 and 255.
    /// </summary>
    public static int[] Gaussian1DScaled(double theta, int size) {
      int[] kernel = new int[size];
      double[] ktemp = new double[size];
      double sum = 0;
      for (int i = 0; i < size; ++i) {
        var val = gaussianDiscrete1D(theta, i - (size*.5d));
        ktemp[i] = val;
        sum += val;
      }
      for (int i = 0; i < kernel.Length; ++i) {
        kernel[i] = (int) Math.Round(256*(ktemp[i]/sum));
      }
      return kernel;
    }

    private static double gaussianDiscrete1D(double theta, double x) {
      double tmp = 1.0/(Math.Sqrt(2.0*Math.PI)*theta);
      double g = 0;
      int i = 0;
      for (double xSubPixel = x - 0.5; i < 11; i++, xSubPixel += 0.1) {
        g += tmp*Math.Pow(Math.E, -xSubPixel*xSubPixel/(2*theta*theta));
      }
      return g/11.0d;
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
      // expand bounds to include dropshadow
      bounds = new RectD(bounds.X, bounds.Y, bounds.Width + dropShadowOffset, bounds.Height + dropShadowOffset);

      // Add label centers to bounds in order to include label edges
      if (node.Labels.Count > 0) {
        foreach (var label in node.Labels) {
          bounds = RectD.Add(bounds, label.GetLayout().GetCenter());
        }
      }

      return bounds;
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
      if (outline == null)
        return (rectangle.Contains(node.Layout.ToRectD().TopLeft) &&
                rectangle.Contains(node.Layout.ToRectD().BottomRight));

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
