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

using System.Drawing;
using System.Linq;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.TableNodeStyle.Style
{
  /// <summary>
  /// Simple style that provides alternating visualizations for even and odd stripes
  /// </summary>
  public class AlternatingStripeStyle : StripeStyleBase<VisualGroup>
  {
    public StripeDescriptor EvenStripeDescriptor { get; set; }
    public StripeDescriptor OddStripeDescriptor { get; set; }

    protected override VisualGroup CreateVisual(IRenderContext context, IStripe stripe) {
      IRectangle layout = stripe.Layout.ToRectD();
      InsetsD stripeInsets;
      var group = new VisualGroup();
      int index;
      if (stripe is IColumn) {
        var col = (IColumn) stripe;
        stripeInsets = new InsetsD(0, col.GetActualInsets().Top, 0, col.GetActualInsets().Bottom);
        index = col.ParentColumn.ChildColumns.ToList().FindIndex(curr => col == curr);
      } else {
        var row = (IRow) stripe;
        stripeInsets = new InsetsD(row.GetActualInsets().Left, 0, row.GetActualInsets().Right, 0);
        index = row.ParentRow.ChildRows.ToList().FindIndex((curr) => row == curr);
      }
      StripeDescriptor descriptor = index%2 == 0 ? EvenStripeDescriptor : OddStripeDescriptor;
      group.Add(new RectangleVisual(layout.X, layout.Y, layout.Width, layout.Height) { Brush = descriptor.BackgroundBrush, Pen = new Pen(descriptor.BorderBrush, descriptor.BorderThickness)});

      //Draw the insets
      if (stripeInsets.Left > 0) {
        group.Add(new RectangleVisual(layout.X, layout.Y, stripeInsets.Left, layout.Height) { Brush = descriptor.InsetBrush });
      }
      if (stripeInsets.Top > 0) {
        group.Add(new RectangleVisual(layout.X, layout.Y, layout.Width, stripeInsets.Top) { Brush = descriptor.InsetBrush });
      }
      if (stripeInsets.Right > 0) {
        group.Add(new RectangleVisual(layout.GetMaxX() - stripeInsets.Right, layout.Y, stripeInsets.Right, layout.Height) { Brush = descriptor.InsetBrush});
      }
      if (stripeInsets.Bottom > 0) {
        group.Add(new RectangleVisual(layout.X, layout.GetMaxY() - stripeInsets.Bottom, layout.Width, stripeInsets.Bottom) {Brush = descriptor.InsetBrush});
      }
      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup group, IStripe stripe) {
      IRectangle layout = stripe.Layout.ToRectD();
      InsetsD stripeInsets;
      int index;
      if (stripe is IColumn) {
        var col = (IColumn) stripe;
        stripeInsets = new InsetsD(0, col.GetActualInsets().Top, 0, col.GetActualInsets().Bottom);
        index = col.ParentColumn.ChildColumns.ToList().FindIndex((curr) => col == curr);
      } else {
        var row = (IRow) stripe;
        stripeInsets = new InsetsD(row.GetActualInsets().Left, 0, row.GetActualInsets().Right, 0);
        index = row.ParentRow.ChildRows.ToList().FindIndex((curr) => row == curr);
      }
      StripeDescriptor descriptor = index%2 == 0 ? EvenStripeDescriptor : OddStripeDescriptor;
      var rect = (RectangleVisual) group.Children[0];
      rect.Rectangle = layout;
      rect.Brush = descriptor.BackgroundBrush;
      if (rect.Pen.Brush != descriptor.BorderBrush) {
        rect.Pen = new Pen(descriptor.BorderBrush, descriptor.BorderThickness);
      }

      int count = 1;
      //Draw the insets
      if (stripeInsets.Left > 0) {
        if (count >= group.Children.Count) {
          group.Add(new RectangleVisual(layout.X, layout.Y, stripeInsets.Left, layout.Height) { Brush = descriptor.InsetBrush });
        } else {
          rect = (RectangleVisual) group.Children[count];
          rect.Rectangle = new RectD(layout.X, layout.Y, stripeInsets.Left, layout.Height);
        }
        count++;
      }
      if (stripeInsets.Top > 0) {
        if (count >= group.Children.Count) {
          group.Add(new RectangleVisual(layout.X, layout.Y, layout.Width, stripeInsets.Top) { Brush = descriptor.InsetBrush });
        } else {
          rect = (RectangleVisual) group.Children[count];
          rect.Rectangle = new RectD(layout.X, layout.Y, layout.Width, stripeInsets.Top);
        }
        count++;
      }
      if (stripeInsets.Right > 0) {
        if (count >= group.Children.Count) {
          group.Add(new RectangleVisual(layout.GetMaxX() - stripeInsets.Right, layout.Y, stripeInsets.Right, layout.Height) { Brush = descriptor.InsetBrush });
        } else {
          rect = (RectangleVisual) group.Children[count];
          rect.Rectangle = new RectD(layout.GetMaxX() - stripeInsets.Right, layout.Y, stripeInsets.Right, layout.Height);
        }
        count++;
      }
      if (stripeInsets.Bottom > 0) {
        if (count >= group.Children.Count) {
          group.Add(new RectangleVisual(layout.X, layout.GetMaxY() - stripeInsets.Bottom, layout.Width, stripeInsets.Bottom) { Brush = descriptor.InsetBrush });
        } else {
          rect = (RectangleVisual) group.Children[count];
          rect.Rectangle = new RectD(layout.X, layout.GetMaxY() - stripeInsets.Bottom, layout.Width, stripeInsets.Bottom);
        }
        count++;
      }
      while (group.Children.Count > count) {
        group.Children.RemoveAt(group.Children.Count - 1);
      }
      return group;
    }
  }
}
