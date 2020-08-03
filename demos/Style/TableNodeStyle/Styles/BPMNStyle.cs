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
using System.Linq;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using MatrixOrder = System.Drawing.Drawing2D.MatrixOrder;

namespace Demo.yFiles.Graph.TableNodeStyle.Style
{
  /// <summary>
  /// Abstract base class for stripe styles that provide a BPMN like visualization.
  /// </summary>
  public abstract class BPMNStyle : StripeStyleBase<GeneralPathVisual>
  {
    public StripeDescriptor StripeDescriptor { get; set; }

    private double wedgeHeight = 10;

    [DefaultValue(10)]
    public double WedgeHeight {
      get { return wedgeHeight; }
      set { wedgeHeight = value; }
    }

    private double wedgeWidth = 10;

    [DefaultValue(10)]
    public double WedgeWidth {
      get { return wedgeWidth; }
      set { wedgeWidth = value; }
    }

    protected override GeneralPathVisual CreateVisual(IRenderContext context, IStripe stripe) {
      IRectangle layout = stripe.Layout.ToRectD();
      GeneralPath outline = CreatePath(stripe, layout);
      Brush brush = StripeDescriptor.BackgroundBrush;
      if (!(brush is SolidBrush)) {
        brush = NormalizeBrush(brush, layout);
      }
      return new MyPathVisual(outline) {
        Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y),
        Brush = brush,
        Pen = new Pen(StripeDescriptor.BorderBrush, StripeDescriptor.BorderThickness),
        Size = layout.GetSize()
      };
    }

    protected override GeneralPathVisual UpdateVisual(IRenderContext context, GeneralPathVisual oldVisual, IStripe stripe) {
      var path = oldVisual as MyPathVisual;
      if (path == null) {
        return CreateVisual(context, stripe);
      }
      IRectangle layout = stripe.Layout.ToRectD();
      if (path.Size != layout.GetSize()) {
        // the size has changed: need to recreate the path
        path.Path = CreatePath(stripe, layout);
      }
      path.Brush = StripeDescriptor.BackgroundBrush is SolidBrush
          ? StripeDescriptor.BackgroundBrush
          : NormalizeBrush(StripeDescriptor.BackgroundBrush, layout);
      path.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      return path;
    }

    private class MyPathVisual : GeneralPathVisual
    {
      public MyPathVisual(GeneralPath path) : base(path) {}

      public SizeD Size { get; set; }
    }

    private static Brush NormalizeBrush(Brush brush, IRectangle layout) {
      if (brush is LinearGradientBrush) {
        LinearGradientBrush lgb = (LinearGradientBrush) brush.Clone();
        lgb.MultiplyTransform(
            new Matrix(Math.Max(0.1f, (float) layout.Width), 0, 0, Math.Max(0.1f, (float) layout.Height),
                (float) layout.X, (float) layout.Y), MatrixOrder.Append);
        brush = lgb;
      } else if (brush is PathGradientBrush) {
        PathGradientBrush pgb = (PathGradientBrush) brush.Clone();
        pgb.MultiplyTransform(
            new Matrix(Math.Max(0.1f, (float) layout.Width), 0, 0, Math.Max(0.1f, (float) layout.Height),
                (float) layout.X, (float) layout.Y), MatrixOrder.Append);
        brush = pgb;
      } else if (brush is TextureBrush) {
        TextureBrush tb = (TextureBrush) brush.Clone();
        tb.MultiplyTransform(
            new Matrix(Math.Max(0.1f, (float) layout.Width), 0, 0, Math.Max(0.1f, (float) layout.Height),
                (float) layout.X, (float) layout.Y), MatrixOrder.Append);
        brush = tb;
      }
      return brush;
    }

    protected abstract GeneralPath CreatePath(IStripe stripe, IRectangle layout);

  }

  /// <summary>
  /// Custom style that provides a BPMN like visualization for rows
  /// </summary>
  public class BPMNRowStyle : BPMNStyle
  {
    protected override GeneralPath CreatePath(IStripe stripe, IRectangle layout) {
      var row = (IRow)stripe;
      GeneralPath outline = new GeneralPath();
      outline.MoveTo(0, 0);
      outline.LineTo(0, layout.Height);
      outline.LineTo(layout.Width, layout.Height);
      if (IsFirst(row)) {
        outline.LineTo(layout.Width, 2*WedgeHeight);
        outline.LineTo(layout.Width + WedgeWidth, WedgeHeight);
        outline.LineTo(layout.Width, 0);
        outline.Close();
      } else {
        outline.LineTo(layout.Width, 0);
      }
      return outline;
    }

    private bool IsFirst(IRow row) {
      ITable t = row.Table;
      return t != null && t.RootRow.ChildRows.First() == row;
    }
  }

  /// <summary>
  /// Custom style that provides a BPMN like visualization for columns
  /// </summary>
  public class BPMNColumnStyle : BPMNStyle
  {
    protected override GeneralPath CreatePath(IStripe stripe, IRectangle layout) {
      var column = (IColumn)stripe;
      GeneralPath outline = new GeneralPath();
      //Left border:
      outline.MoveTo(0, 0);
      outline.LineTo(layout.Width, 0);
      outline.LineTo(layout.Width + WedgeWidth, WedgeHeight);
      outline.LineTo(layout.Width, 2 * WedgeHeight);
      outline.LineTo(layout.Width, layout.Height);
      outline.LineTo(0, layout.Height);

      if (IsFirst(column)) {
        outline.Close();
      } else {
        outline.LineTo(0, layout.Height);
        outline.LineTo(0, 2 * WedgeHeight);
        outline.LineTo(WedgeWidth, WedgeHeight);
        outline.Close();
      }

      return outline;
    }

    private bool IsFirst(IColumn col) {
      ITable t = col.Table;
      return t != null && t.RootColumn.ChildColumns.First() == col;
    }
  }

}
