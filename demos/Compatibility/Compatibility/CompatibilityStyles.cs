/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.2.
 ** Copyright (c) 2000-2019 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;

namespace yWorks.Graph.Styles
{
  
  /// <summary>
  /// An abstract base class that makes it possible to easily implement a custom
  /// <see cref="INodeStyle"/>.
  /// </summary>
  /// <remarks>
  /// The only method that needs to be implemented by subclasses is <see cref="Paint"/>.
  /// </remarks>
  public abstract class SimpleAbstractNodeStyle : NodeStyleBase<IVisual>
  {
    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return new NodeStyleVisual(this, node);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      var v = oldVisual as NodeStyleVisual;
      if (v == null) {
        return CreateVisual(context, node);
      }
      v.style = this;
      v.node = node;
      return v;
    }

    protected abstract void Paint(INode node, Graphics graphics, IRenderContext renderContext);

    protected override RectD GetBounds(ICanvasContext context, INode node) { return GetBounds(node, context); }
    protected virtual RectD GetBounds(INode node, ICanvasContext context) { return base.GetBounds(context, node); }

    protected override bool IsVisible(ICanvasContext context, RectD rectangle, INode node) { return IsVisible(node, rectangle, context); }
    protected virtual bool IsVisible(INode node, RectD clip, ICanvasContext context) { return base.IsVisible(context, clip, node); }

    protected override bool IsHit(IInputModeContext context, PointD location, INode node) { return IsHit(node, location, context); }
    protected virtual bool IsHit(INode node, PointD p, ICanvasContext canvasContext) {
      return base.IsHit(
              canvasContext is IInputModeContext ? (IInputModeContext) canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext), 
              p, 
              node);
    }

    protected override bool IsInBox(IInputModeContext context, RectD rectangle, INode node) { return IsInBox(node, rectangle, context); }
    protected virtual bool IsInBox(INode node, RectD box, ICanvasContext canvasContext) {
      return IsInBox(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext), 
        box, 
        node);
    }

    private class NodeStyleVisual : IVisual
    {
      public SimpleAbstractNodeStyle style;
      public INode node;

      public NodeStyleVisual(SimpleAbstractNodeStyle style, INode node) {
        this.style = style;
        this.node = node;
      }

      public void Paint(IRenderContext context, Graphics g) {
        style.Paint(node, g, context);
      }
    }
  }
  
  /// <summary>
  /// An abstract base class that makes it possible to easily implement a custom
  /// <see cref="IEdgeStyle"/>.
  /// </summary>
  /// <remarks>
  /// The only method that needs to be implemented by subclasses is <see cref="Paint"/>.
  /// </remarks>
  public abstract class SimpleAbstractEdgeStyle : EdgeStyleBase<IVisual>
  {

    protected override IVisual CreateVisual(IRenderContext context, IEdge edge) {
      return new EdgeStyleVisual(this, edge);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, IEdge edge) {
      var v = oldVisual as EdgeStyleVisual;
      if (v == null) {
        return CreateVisual(context, edge);
      }
      v.style = this;
      v.edge = edge;
      return v;
    }

    protected abstract void Paint(IEdge edge, Graphics graphics, IRenderContext renderContext);

    protected override RectD GetBounds(ICanvasContext context, IEdge edge) { return GetBounds(edge, context); }
    protected virtual RectD GetBounds(IEdge edge, ICanvasContext context) { return base.GetBounds(context, edge); }

    protected override bool IsVisible(ICanvasContext context, RectD rectangle, IEdge edge) { return IsVisible(edge, rectangle, context); }
    protected virtual bool IsVisible(IEdge edge, RectD clip, ICanvasContext context) { return base.IsVisible(context, clip, edge); }

    protected override bool IsInBox(IInputModeContext context, RectD rectangle, IEdge edge) { return IsInBox(edge, rectangle, context); }
    protected virtual bool IsInBox(IEdge edge, RectD box, ICanvasContext canvasContext) {
      return base.IsInBox(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext),
        box, 
        edge);
    }

    protected override bool IsHit(IInputModeContext context, PointD location, IEdge edge) { return IsHit(edge, location, context); }
    protected virtual bool IsHit(IEdge edge, PointD p, ICanvasContext canvasContext) {
      return base.IsHit(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext),
        p,
        edge);
    }

    protected virtual void CropPath(IEdge edge, IArrow sourceArrow, IArrow targetArrow, ref GeneralPath pathToCrop) {
      pathToCrop = base.CropPath(edge, sourceArrow, targetArrow, pathToCrop);
    }

    protected virtual void RenderArrows(IRenderContext context, Graphics graphics, IEdge edge, GeneralPath edgePath, IArrow sourceArrow, IArrow targetArrow) {
      if (targetArrow != Arrows.None && targetArrow != null) {
        Tangent? targetArrowAnchor = GetTargetArrowAnchor(edgePath, targetArrow);
        if (targetArrowAnchor != null) {
          var anchorPoint = targetArrowAnchor.Value.Point;
          var arrowDirection = targetArrowAnchor.Value.Vector;
          IVisualCreator visualCreator = targetArrow.GetVisualCreator(edge, false, anchorPoint, arrowDirection);
          visualCreator.CreateVisual(context).Paint(context, graphics);
        }
      }
      if (sourceArrow != Arrows.None && sourceArrow != null) {
        Tangent? sourceArrowAnchor = GetSourceArrowAnchor(edgePath, sourceArrow);
        if (sourceArrowAnchor != null) {
          var anchorPoint = sourceArrowAnchor.Value.Point;
          var arrowDirection = sourceArrowAnchor.Value.Vector;
          var visualCreator = sourceArrow.GetVisualCreator(edge, true, anchorPoint, arrowDirection);
          visualCreator.CreateVisual(context).Paint(context, graphics);
        }
      }
    }

    private class EdgeStyleVisual : IVisual
    {
      public SimpleAbstractEdgeStyle style;
      public IEdge edge;

      public EdgeStyleVisual(SimpleAbstractEdgeStyle style, IEdge edge) {
        this.style = style;
        this.edge = edge;
      }

      public void Paint(IRenderContext context, Graphics g) {
        style.Paint(edge, g, context);
      }
    }
  }
  
  /// <summary>
  /// An abstract base class that makes it possible to easily implement a custom
  /// <see cref="ILabelStyle"/>.
  /// </summary>
  /// <remarks>
  /// Only <see cref="Paint"/> and <see cref="LabelStyleBase{TVisual}.GetPreferredSize"/> need to be implemented
  /// by subclasses.
  /// </remarks>
  public abstract class SimpleAbstractLabelStyle : LabelStyleBase<IVisual>
  {

    protected override IVisual CreateVisual(IRenderContext context, ILabel label) {
      return new LabelStyleVisual(this, label);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, ILabel label) {
      var v = oldVisual as LabelStyleVisual;
      if (v == null) {
        return CreateVisual(context, label);
      }
      v.style = this;
      v.label = label;
      return v;
    }

    protected abstract void Paint(ILabel label, Graphics graphics, IRenderContext renderContext);

    protected override RectD GetBounds(ICanvasContext context, ILabel label) { return GetBounds(label, context); }
    protected virtual RectD GetBounds(ILabel label, ICanvasContext context) { return base.GetBounds(context, label); }

    protected override bool IsVisible(ICanvasContext context, RectD rectangle, ILabel label) { return IsVisible(label, rectangle, context); }
    protected virtual bool IsVisible(ILabel label, RectD clip, ICanvasContext context) { return base.IsVisible(context, clip, label); }

    protected override bool IsHit(IInputModeContext context, PointD location, ILabel label) { return IsHit(label, location, context); }
    protected virtual bool IsHit(ILabel label, PointD p, ICanvasContext canvasContext) {
      return base.IsHit(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext),
        p,
        label);
    }

    protected override bool IsInBox(IInputModeContext context, RectD rectangle, ILabel label) { return IsInBox(label, rectangle, context); }
    protected virtual bool IsInBox(ILabel label, RectD box, ICanvasContext canvasContext) {
      return base.IsInBox(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext),
        box,
        label);
    }

    private class LabelStyleVisual : IVisual
    {
      public SimpleAbstractLabelStyle style;
      public ILabel label;

      public LabelStyleVisual(SimpleAbstractLabelStyle style, ILabel label) {
        this.style = style;
        this.label = label;
      }

      public void Paint(IRenderContext context, Graphics g) {
        style.Paint(label, g, context);
      }
    }
  }
  
  /// <summary>
  /// An abstract base class that makes it possible to easily implement a custom
  /// <see cref="IPortStyle"/>.
  /// </summary>
  /// <remarks>
  /// Only <see cref="Paint"/> and <see cref="GetBounds(IPort,ICanvasContext)"/> need to be implemented
  /// by subclasses.
  /// </remarks>
  public abstract class SimpleAbstractPortStyle : PortStyleBase<IVisual>
  {
    protected override IVisual CreateVisual(IRenderContext context, IPort port) {
      return new PortStyleVisual(this, port);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, IPort port) {
      var v = oldVisual as PortStyleVisual;
      if (v == null) {
        return CreateVisual(context, port);
      }
      v.style = this;
      v.port = port;
      return v;
    }

    protected abstract void Paint(IPort port, Graphics graphics, IRenderContext renderContext);

    protected abstract RectD GetBounds(IPort port, ICanvasContext context);

    protected override RectD GetBounds(ICanvasContext context, IPort port) { return GetBounds(port, context); }

    protected override bool IsVisible(ICanvasContext context, RectD rectangle, IPort port) { return IsVisible(port, rectangle, context); }
    protected virtual bool IsVisible(IPort port, RectD clip, ICanvasContext context) { return base.IsVisible(context, clip, port); }

    protected override bool IsHit(IInputModeContext context, PointD location, IPort port) { return IsHit(port, location, context); }
    protected virtual bool IsHit(IPort port, PointD p, ICanvasContext canvasContext) {
      return base.IsHit(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext),
        p,
        port);
    }

    protected override bool IsInBox(IInputModeContext context, RectD rectangle, IPort port) { return IsInBox(port, rectangle, context); }
    protected virtual bool IsInBox(IPort port, RectD box, ICanvasContext canvasContext) {
      return base.IsInBox(
        canvasContext is IInputModeContext ? (IInputModeContext)canvasContext : Contexts.CreateInputModeContext(canvasContext.CanvasControl, null, canvasContext),
        box,
        port);
    }

    private class PortStyleVisual : IVisual
    {
      public SimpleAbstractPortStyle style;
      public IPort port;

      public PortStyleVisual(SimpleAbstractPortStyle style, IPort port) {
        this.style = style;
        this.port = port;
      }

      public void Paint(IRenderContext context, Graphics g) {
        style.Paint(port, g, context);
      }
    }
  }


  /// <summary>
  /// Utility class that offers convenience methods for working with <see cref="Color"/>
  /// instances and applies effects to <see cref="Bitmap"/>s.
  /// </summary>
  public class ImageSupport {
    /// <summary>
    /// Create a color from HSB values.
    /// </summary>
    public static Color FromHSB(double hue, double saturation, double brightness, double alpha) {
      int r = 0, g = 0, b = 0;
      if (saturation == 0) {
        r = g = b = (int)(brightness * 255.0f + 0.5f);
      } else {
        double h = (hue - Math.Floor(hue)) * 6.0f;
        double f = h - Math.Floor(h);
        double p = brightness * (1.0f - saturation);
        double q = brightness * (1.0f - saturation * f);
        double t = brightness * (1.0f - (saturation * (1.0f - f)));
        switch ((int)h) {
          case 0:
            r = (int)(brightness * 255.0f + 0.5f);
            g = (int)(t * 255.0f + 0.5f);
            b = (int)(p * 255.0f + 0.5f);
            break;
          case 1:
            r = (int)(q * 255.0f + 0.5f);
            g = (int)(brightness * 255.0f + 0.5f);
            b = (int)(p * 255.0f + 0.5f);
            break;
          case 2:
            r = (int)(p * 255.0f + 0.5f);
            g = (int)(brightness * 255.0f + 0.5f);
            b = (int)(t * 255.0f + 0.5f);
            break;
          case 3:
            r = (int)(p * 255.0f + 0.5f);
            g = (int)(q * 255.0f + 0.5f);
            b = (int)(brightness * 255.0f + 0.5f);
            break;
          case 4:
            r = (int)(t * 255.0f + 0.5f);
            g = (int)(p * 255.0f + 0.5f);
            b = (int)(brightness * 255.0f + 0.5f);
            break;
          case 5:
            r = (int)(brightness * 255.0f + 0.5f);
            g = (int)(p * 255.0f + 0.5f);
            b = (int)(q * 255.0f + 0.5f);
            break;
        }
      }
      return Color.FromArgb((int) (alpha*255), r, g, b);

    }

    /// <summary>
    /// Converts an RGB color value to HSB values
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="hue">The hue.</param>
    /// <param name="saturation">The saturation.</param>
    /// <param name="brightness">The brightness.</param>
    /// <param name="alpha">The alpha.</param>
    public static void ToHSB(Color color, out double hue, out double saturation, out double brightness, out double alpha) {
      double r = color.R/255.0d;
      double g = color.G/255.0d;
      double b = color.B/255.0d;
      double a = color.A/255.0d;
      double max = Math.Max(b, Math.Max(r, g));
      double min = Math.Min(b, Math.Min(r, g));

      if (max == min) {
        hue = 0;
      } else if (max == r && g >= b) {
        hue = 1/6.0d * (g - b)/(max - min);
      } else if (max == r && g < b) {
        hue = 1/6.0d*(g - b)/(max - min) + 1;
      } else if (max == g) {
        hue = 1/6.0d*(b - r)/(max - min) + 2/6.0d;
      } else {//if (max == b) {
        hue = 1/6.0d*(r - g)/(max - min) + 4/6.0d;
      }

      double l = (max + min)*0.5d;
      if (max == min) {
        saturation = 0;
      } else if (l <= 0.5) {
        saturation = (max - min)/(2*l);
      } else {
        saturation = (max - min)/(2 - 2*l);
      }
      saturation = Math.Min(1, Math.Max(0, saturation));
      hue = Math.Min(1, Math.Max(0, hue));
      brightness = Math.Min(1, Math.Max(0, max));
      alpha = Math.Min(1, Math.Max(0, a));
    }

    internal static void InnerGlow(Bitmap bitmap, double theta, int size, Color color) {

      BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                                        PixelFormat.Format32bppArgb);

      int count1 = data.Width * data.Height;
      int count = data.Width * data.Height * 4;
      byte[] src = new byte[count];
      byte[] src2 = new byte[count1];
      byte[] target = new byte[count1];
      Marshal.Copy(data.Scan0, src, 0, count);

      int c = 0;
      for (int i = 3; i < src.Length; i += 4, c++) {
        src2[c] = (byte)(255 - src[i]);
      }

      int[] kernel = Gaussian1DScaled(theta, size);

      ApplyKernelHorizontallyA(data, kernel, src2, target);
      ApplyKernelVerticallyA(data, kernel, target, src2);

      c = 0;
      byte b = color.B;
      byte r = color.R;
      byte g = color.G;
      for (int i = 0; i < src.Length - 3; ) {
        src[i++] = b;
        src[i++] = g;
        src[i++] = r;
        src[i] = Math.Min(src[i], src2[c++]);// (byte)(((src[i]) * src2[c++]) / 255);
        i++;
      }

      Marshal.Copy(src, 0, data.Scan0, count);
      bitmap.UnlockBits(data);
    }

    internal static void OuterGlow(Bitmap bitmap, double theta, int size, Color color) {
      BitmapData data =
        bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                        PixelFormat.Format32bppArgb);

      int count1 = data.Width * data.Height;
      int count = data.Width * data.Height * 4;
      byte[] src = new byte[count];
      byte[] src2 = new byte[count1];
      byte[] target = new byte[count1];
      Marshal.Copy(data.Scan0, src, 0, count);

      int c = 0;
      for (int i = 3; i < src.Length; i += 4, c++) {
        src2[c] = src[i];
      }

      int[] kernel = Gaussian1DScaled(theta, size);

      ApplyKernelHorizontallyA(data, kernel, src2, target);
      ApplyKernelVerticallyA(data, kernel, target, src2);
      c = 0;
      byte b = color.B;
      byte r = color.R;
      byte g = color.G;
      for (int i = 0; i < src.Length - 3; ) {
        src[i++] = b;
        src[i++] = g;
        src[i++] = r;
        src[i] = (byte)(((255 - src[i]) * src2[c++]) / 255);
        i++;
      }

      Marshal.Copy(src, 0, data.Scan0, count);
      bitmap.UnlockBits(data);
    }

    /// <summary>
    /// Creates a Drop Shadow from a given bitmap.
    /// </summary>
    /// <param name="bitmap">The bitmap to create a drop shadow for.</param>
    /// <param name="color">The color of the shadow.</param>
    /// <param name="kernel">The kernel to use</param>
    /// <see cref="Gaussian1D"/>
    public static void DropShadow(Bitmap bitmap, Color color, int[] kernel) {
      BitmapData data =
        bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                        PixelFormat.Format32bppArgb);

      try {
        int count1 = data.Width * data.Height;
        int count = data.Width * data.Height * 4;
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
        for (int i = 0; i < src.Length - 4; ) {
          src[i++] = b;
          src[i++] = g;
          src[i++] = r;
          src[i++] = (byte)((src2[c++] * a) / 255);
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


    internal static void GaussianBlur(Bitmap bitmap, double theta, int size) {
      int[] intkernel = Gaussian1DScaled(theta, size);
      GaussianBlur(bitmap, intkernel);
    }

    internal static void GaussianBlur(Bitmap bitmap, int[] intkernel) {
      BitmapData data =
        bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                        PixelFormat.Format32bppArgb);
      try {
        int count = data.Width * data.Height * 4;
        byte[] src = new byte[count];
        byte[] target = new byte[count];
        Marshal.Copy(data.Scan0, src, 0, count);

        ApplyKernelHorizontallyARGB(data, intkernel, src, target);
        ApplyKernelVerticallyARGB(data, intkernel, target, src);


        Marshal.Copy(src, 0, data.Scan0, count);
        return;
      } catch (SecurityException) {
        // ignore and do it by hand instead.
      } catch (MethodAccessException) {
        // ignore and do it by hand instead.
      } finally {
        bitmap.UnlockBits(data);
      }
      {
        int count1 = bitmap.Width * bitmap.Height * 4;
        byte[] src = new byte[count1];
        byte[] target = new byte[count1];

        for (int y = 0; y < bitmap.Height; y++) {
          int offset = y * bitmap.Width * 4;
          for (int x = 0; x < bitmap.Width; x++) {
            Color pixel = bitmap.GetPixel(x, y);
            int c = x*4 + offset;
            src[c++] = pixel.A;
            src[c++] = pixel.R;
            src[c++] = pixel.G;
            src[c] = pixel.B;
          }
        }

        ApplyKernelHorizontallyARGB(data, intkernel, src, target);
        ApplyKernelVerticallyARGB(data, intkernel, target, src);

        for (int y = 0; y < bitmap.Height; y++) {
          int offset = y * bitmap.Width * 4;
          for (int x = 0; x < bitmap.Width; x++) {
            int c = x * 4 + offset;
            Color newPixel = Color.FromArgb(src[c++], src[c++], src[c++], src[c]);
            bitmap.SetPixel(x, y, newPixel);
          }
        }
      }
    }

    internal static void ApplyKernel(Bitmap bitmap, float[][] kernel, bool normalize) {

      if (normalize) {
        float sum = 0;
        for (int i = 0; i < kernel.Length; i++) {
          float[] floats = kernel[i];
          for (int j = 0; j < floats.Length; j++) {
            sum += floats[j];
          }
        }
        if (sum != 0) {
          for (int i = 0; i < kernel.Length; i++) {
            float[] floats = kernel[i];
            for (int j = 0; j < floats.Length; j++) {
              floats[j] /= sum;
            }
          }
        }
      }

      BitmapData data =
        bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite,
                        PixelFormat.Format32bppArgb);

      int count = data.Width*data.Height*4;
      byte[] src = new byte[count];
      byte[] target = new byte[count];
      Marshal.Copy(data.Scan0, src, 0, count);

      ApplyKernelARGB(data, kernel, src, target);

      Marshal.Copy(target, 0, data.Scan0, count);
      bitmap.UnlockBits(data);
    }

    private static void ApplyKernelHorizontallyARGB(BitmapData data, float[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length+1) / 2;
      int kend = kernel.Length - k2;
      int yend = data.Height - kend;
      int xend = data.Width - kend;
      for (int y = k2; y < yend; y++) {
        int offset = y*data.Width*4;
        offset += k2*4;
        for (int x = k2; x < xend; x++) {
          float fa = 0;
          float fr = 0;
          float fg = 0;
          float fb = 0;
          int k = 0;
          int p = offset -k2*4;
          for (int i = -k2; i < kend; i++) {
            float kvalue = kernel[k++];
            fa += src[p++] * kvalue;
            fr += src[p++] * kvalue;
            fg += src[p++] * kvalue;
            fb += src[p++] * kvalue;
          }
          fa = 0;

          target[offset++] = (byte)Math.Min(255, Math.Max(fa, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(fr, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(fg, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(fb, 0));
        }
      }
    }

    private static void ApplyKernelHorizontallyARGB(BitmapData data, int[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length+1) / 2;
      int kend = kernel.Length - k2;
      int yend = data.Height - kend;
      int xend = data.Width - kend;
      for (int y = k2; y < yend; y++) {
        int offset = y*data.Width*4;
        offset += k2*4;
        for (int x = k2; x < xend; x++) {
          int fa = 0;
          int fr = 0;
          int fg = 0;
          int fb = 0;
          int k = 0;
          int p = offset -k2*4;
          for (int i = -k2; i < kend; i++) {
            int kvalue = kernel[k++];
            fa += src[p++] * kvalue;
            fr += src[p++] * kvalue;
            fg += src[p++] * kvalue;
            fb += src[p++] * kvalue;
          }

          target[offset++] = (byte)Math.Min(255, Math.Max(fa/256, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(fr/256, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(fg/256, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(fb/256, 0));
        }
      }
    }

    private static void ApplyKernelHorizontallyA(BitmapData data, int[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length+1) / 2;
      int kend = kernel.Length - k2;
      int yend = data.Height;
      int xend = data.Width - kend;
      for (int y = 0; y < yend; y++) {
        int offset = y*data.Width;
        offset += k2;
        for (int x = k2; x < xend; x++) {
          int fa = 0;
          int k = 0;
          int p = offset -k2;
          for (int i = -k2; i < kend; i++) {
            fa += src[p++] * kernel[k++];
          }
          target[offset++] = (byte)Math.Min(255, Math.Max(fa/256, 0));
        }
      }
    }

    internal static void ApplyKernelARGB(BitmapData data, float[][] kernel, byte[] src, byte[] target) {
      int kernelLength = kernel.Length;
      if (kernelLength == 1) {
        ApplyKernelHorizontallyARGB(data, kernel[0], src, target);
        return;
      } else {
        bool allOne = true;
        for (int i = 0; i < kernelLength; i++) {
          float[] floats = kernel[i];
          if (floats.Length != 1) {
            allOne = false;
            break;
          }
        }
        if (allOne) {
          float[] kernel2 = new float[kernelLength];
          for (int i = 0; i < kernelLength; i++) {
            float[] floats = kernel[i];
            kernel2[i] = floats[0];
          }
          ApplyKernelVerticallyARGB(data, kernel2, src, target);
          return;
        }
      }
      int k2x = (kernelLength+1) / 2;
      int kernelLength2 = kernel[0].Length;
      int k2y = (kernelLength2+1) / 2;
      int kendx = kernelLength - k2x;
      int kendy = kernelLength2 - k2y;
      int yend = data.Height - kendy;
      int xend = data.Width - kendx;
      int yspan = data.Width * 4;
      int delta = k2y * yspan + k2x *4;
      for (int y = k2y; y < yend; y++) {
        int offset = y*yspan + k2x * 4;
        for (int x = k2x; x < xend; x++) {
          int srcOffset = offset - delta;
          float a = 0;
          float r = 0;
          float g = 0;
          float b = 0;
          for (int i = 0; i < kernelLength; i++, srcOffset += yspan) {
            float[] kernelx = kernel[i];
            int srcOffset2 = srcOffset;
            for (int j = 0; j < kernelLength2; j++) {
              float kvalue = kernelx[j];
              r += src[srcOffset2++]*kvalue;
              g += src[srcOffset2++]*kvalue;
              b += src[srcOffset2++]*kvalue;
              a += src[srcOffset2++] * kvalue;
            }
          }
          target[offset++] = (byte) Math.Min(255, Math.Max(r, 0));
          target[offset++] = (byte) Math.Min(255, Math.Max(g, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(b, 0));
          target[offset++] = (byte)Math.Min(255, Math.Max(a, 0));
        }
      }
    }

    private static void ApplyKernelVerticallyARGB(BitmapData data, float[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length + 1) / 2;
      int kend = kernel.Length - k2;
      int yend = data.Height - kend;
      int xend = data.Width - kend;
      int deltaY = data.Width*4;
      int offset = k2 * 4 + k2 * deltaY;
      for (int x = k2; x < xend; x++, offset += 4) {
        for (int y = k2; y < yend; y++, offset += deltaY) {
          int srcOffset = offset ;
          float fa = 0;
          float fr = 0;
          float fg = 0;
          float fb = 0;
          int k = 0;
          for (int i = -k2; i < kend; i++) {
            int offsety = i*deltaY;
            float kvalue = kernel[k++];
            fa += src[srcOffset + offsety++]*kvalue;
            fr += src[srcOffset + offsety++]*kvalue;
            fg += src[srcOffset + offsety++]*kvalue;
            fb += src[srcOffset + offsety]*kvalue;
          }
          target[srcOffset++] = (byte)Math.Min(255, Math.Max(fa, 0));
          target[srcOffset++] = (byte) Math.Min(255, Math.Max(fr, 0));
          target[srcOffset++] = (byte) Math.Min(255, Math.Max(fg, 0));
          target[srcOffset] = (byte) Math.Min(255, Math.Max(fb, 0));
        }
      }
    }

    private static void ApplyKernelVerticallyARGB(BitmapData data, int[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length + 1) / 2;
      int kend = kernel.Length - k2;
      int yend = data.Height - kend;
      int xend = data.Width - kend;
      int deltaY = data.Width*4;
      for (int x = k2; x < xend; x++) {
        int offset = x * 4 + k2 * deltaY;
        for (int y = k2; y < yend; y++, offset += deltaY) {
          int srcOffset = offset ;
          int fa = 0;
          int fr = 0;
          int fg = 0;
          int fb = 0;
          int k = 0;
          for (int i = -k2; i < kend; i++) {
            int offsety = i*deltaY;
            int kvalue = kernel[k++];
            fa += src[srcOffset + offsety++]*kvalue;
            fr += src[srcOffset + offsety++]*kvalue;
            fg += src[srcOffset + offsety++]*kvalue;
            fb += src[srcOffset + offsety]*kvalue;
          }
          target[srcOffset++] = (byte)Math.Min(255, Math.Max(fa/256, 0));
          target[srcOffset++] = (byte) Math.Min(255, Math.Max(fr/256, 0));
          target[srcOffset++] = (byte) Math.Min(255, Math.Max(fg/256, 0));
          target[srcOffset] = (byte) Math.Min(255, Math.Max(fb/256, 0));
        }
      }
    }

    private static void ApplyKernelVerticallyA(BitmapData data, int[] kernel, byte[] src, byte[] target) {
      int k2 = (kernel.Length + 1) / 2;
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
    /// Creates a 1 dimensional gaussian kernel.
    /// </summary>
    public static float[] Gaussian1D(double theta, int size) {
      float[] kernel = new float[size];
      float sum = 0;
      for (int i = 0; i < size; ++i) {
        float val = (float) gaussianDiscrete1D(theta, i - (size*.5d));
        kernel[i] = val;
        sum += val;
      }
      for (int i = 0; i < kernel.Length; ++i) {
        kernel[i] /= sum;
      }
      return kernel;
    }

    /// <summary>
    /// Creates a 1 dimensional gaussian kernel normalized to integer values between 0 and 255.
    /// </summary>
    public static int[] Gaussian1DScaled(double theta, int size) {
      int[] kernel = new int[size];
      double[] ktemp = new double[size];
      double sum = 0;
      for (int i = 0; i < size; ++i) {
        var val = gaussianDiscrete1D(theta, i - (size * .5d));
        ktemp[i] = val;
        sum += val;
      }
      for (int i = 0; i < kernel.Length; ++i) {
        kernel[i] = (int)Math.Round(256 * (ktemp[i] / sum));
      }
      return kernel;
    }

    private static double gaussianDiscrete1D(double theta, double x) {
      double tmp = 1.0 / (Math.Sqrt(2.0 * Math.PI) * theta);
      double g = 0;
      int i = 0;
      for (double xSubPixel = x - 0.5; i < 11; i++, xSubPixel += 0.1) {
        g += tmp * Math.Pow(Math.E, -xSubPixel * xSubPixel / (2 * theta * theta));
      }
      return g / 11.0d;
    }

    ///<summary>
    /// Mixes two colors using the provided ratio.
    ///</summary>
    public static Color Mix(Color color0, Color color1, double ratio) {
      double iratio = 1 - ratio;
      double a = color0.A * ratio + color1.A * iratio;
      double r = color0.R * ratio + color1.R * iratio;
      double g = color0.G * ratio + color1.G * iratio;
      double b = color0.B * ratio + color1.B * iratio;
      return
        Color.FromArgb((int) Math.Round(a), (int) Math.Round(r),
                       (int) Math.Round(g), (int) Math.Round(b));
    }
  }

}
