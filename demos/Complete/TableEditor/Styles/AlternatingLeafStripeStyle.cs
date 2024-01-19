/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.6.
 ** Copyright (c) 2000-2024 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Windows.Markup;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using Rectangle = System.Drawing.Rectangle;

[assembly: XmlnsDefinition("Demo.yFiles.Graph.TableEditor", "Demo.yFiles.Graph.TableEditor.Style")]

namespace Demo.yFiles.Graph.TableEditor.Style
{
  /// <summary>
  /// Custom stripe style that alternates the visualizations for the leaf nodes and uses a different style for all parent stripes.
  /// </summary>
  public class AlternatingLeafStripeStyle : StripeStyleBase<IVisual>
  {
    /// <summary>
    /// Visualization for all leaf stripes that have an even index
    /// </summary>
    public StripeDescriptor EvenLeafDescriptor { get; set; }

    /// <summary>
    /// Visualization for all stripes that are not leafs
    /// </summary>
    public StripeDescriptor ParentDescriptor { get; set; }

    /// <summary>
    /// Visualization for all leaf stripes that have an odd index
    /// </summary>
    public StripeDescriptor OddLeafDescriptor { get; set; }

    private class AlternatingLeafStyleVisual : IVisual
    {

      public IStripe Stripe { get; set; }
      public StripeDescriptor EvenLeafDescriptor { get; set; }
      public StripeDescriptor ParentDescriptor { get; set; }
      public StripeDescriptor OddLeafDescriptor { get; set; }

      public void Paint(IRenderContext context, Graphics graphics) {
        var node = Stripe;
        var stripe = node.Lookup<IStripe>();
        IRectangle layout = node.Layout.ToRectD();
        if (stripe != null) {
          InsetsD stripeInsets;

          if (stripe is IColumn) {
            var col = (IColumn) stripe;
            stripeInsets = new InsetsD(0, col.GetActualInsets().Top, 0, col.GetActualInsets().Bottom);
          } else {
            var row = (IRow) stripe;
            stripeInsets = new InsetsD(row.GetActualInsets().Left, 0, row.GetActualInsets().Right, 0);
          }

          StripeDescriptor descriptor;

          if (stripe.GetChildStripes().Any()) {
            //Parent stripe - use the parent descriptor
            descriptor = ParentDescriptor;

          } else {
            int index;
            if (stripe is IColumn) {
              var col = (IColumn) stripe;
              //Get all leaf columns
              var leafs = col.Table.RootColumn.GetLeaves().ToList();
              //Determine the index
              index = leafs.FindIndex((curr) => col == curr);
              //Use the correct descriptor
              descriptor = index%2 == 0 ? EvenLeafDescriptor : OddLeafDescriptor;
            } else {
              var row = (IRow) stripe;
              var leafs = row.Table.RootRow.GetLeaves().ToList();
              index = leafs.FindIndex((curr) => row == curr);
              descriptor = index%2 == 0 ? EvenLeafDescriptor : OddLeafDescriptor;
            }
          }

          graphics.FillRectangle(descriptor.BackgroundBrush,
              new RectangleF((float) layout.X, (float) node.Layout.Y, (float) layout.Width, (float) layout.Height));
          //Draw the insets
          if (stripeInsets.Left > 0) {
            graphics.FillRectangle(descriptor.InsetBrush,
                new RectangleF((float) layout.X, (float) node.Layout.Y, (float) stripeInsets.Left, (float) layout.Height));
          }
          if (stripeInsets.Top > 0) {
            graphics.FillRectangle(descriptor.InsetBrush,
                new RectangleF((float) layout.X, (float) layout.Y, (float) layout.Width, (float) stripeInsets.Top));
          }
          if (stripeInsets.Right > 0) {
            graphics.FillRectangle(descriptor.InsetBrush,
                new RectangleF((float) (layout.GetMaxX() - stripeInsets.Right), (float) layout.Y,
                    (float) stripeInsets.Right, (float) layout.Height));
          }
          if (stripeInsets.Bottom > 0) {
            graphics.FillRectangle(descriptor.InsetBrush,
                new RectangleF((float) layout.X, (float) (layout.GetMaxY() - stripeInsets.Bottom), (float) layout.Width,
                    (float) stripeInsets.Bottom));
          }
          graphics.DrawRectangle(new Pen(descriptor.BorderBrush, descriptor.BorderThickness),
              new Rectangle((int) layout.X, (int) layout.Y, (int) layout.Width, (int) layout.Height));

        }
      }
    }

    protected override IVisual CreateVisual(IRenderContext context, IStripe stripe) {
      return new AlternatingLeafStyleVisual {
        EvenLeafDescriptor = EvenLeafDescriptor,
        OddLeafDescriptor = OddLeafDescriptor,
        ParentDescriptor = ParentDescriptor,
        Stripe = stripe
      };
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, IStripe stripe) {
      var alsv = oldVisual as AlternatingLeafStyleVisual;
      if (alsv == null) {
        return CreateVisual(context, stripe);
      }
      alsv.EvenLeafDescriptor = EvenLeafDescriptor;
      alsv.OddLeafDescriptor = OddLeafDescriptor;
      alsv.ParentDescriptor = ParentDescriptor;
      alsv.Stripe = stripe;
      return alsv;
    }
  }
}