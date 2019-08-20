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
using System.Drawing.Drawing2D;
using yWorks.Controls;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Collapse
{
  /// <summary>
  /// An implementation of <see cref="NodeStyleBase" /> which 
  /// visualizes the inner nodes in the tree.
  /// </summary>
  /// <remarks>The drawing of the plus/minus sign depends on the content 
  /// of the node's tag.</remarks>
  public class CollapseNodeStyle : NodeStyleBase<VisualGroup>
  {
    private readonly Color collapsedColor2 = Color.FromArgb(255, 255, 153, 0);
    private readonly Color collapsedColor1 = Color.FromArgb(255, 255, 204, 0);
    private readonly Color expandedColor1 = Color.FromArgb(255, 204, 255, 255);
    private readonly Color expandedColor2 = Color.FromArgb(255, 153, 204, 255);

    public CollapsedState CollapsedState { get; set; }

    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var layout = node.Layout;

      // Get the background colors dependent on the collapse state
      var backgroundColor1 = CollapsedState == CollapsedState.Collapsed ? collapsedColor1 : expandedColor1;
      var backgroundColor2 = CollapsedState == CollapsedState.Collapsed ? collapsedColor2 : expandedColor2;

      var background = new RectangleVisual(0, 0, layout.Width, layout.Height) {
        Brush = new LinearGradientBrush(new PointF(0, 0), new PointF(0, (float) layout.Height), backgroundColor1, backgroundColor2),
        Pen = Pens.LightGray
      };
      var symbol = new SymbolVisual(new Point((int) (layout.Width / 2), (int) (layout.Height / 2)), CollapsedState);

      return new VisualGroup {
        Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y),
        Children = {
          background, symbol
        }
      };
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldGroup, INode node) {
      if (oldGroup == null || oldGroup.Children.Count != 3) {
        return CreateVisual(context, node);
      }

      // Update the transform if the node has moved
      var layout = node.Layout;
      if (oldGroup.Transform.OffsetX != (float) layout.X || oldGroup.Transform.OffsetY != (float) layout.Y) {
        oldGroup.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      }

      // Update the symbol and background colors if the collapsed state changed
      var symbol = (SymbolVisual) oldGroup.Children[2];
      if (symbol.collapsedState != CollapsedState) {
        symbol.collapsedState = CollapsedState;

        var backgroundColor1 = CollapsedState == CollapsedState.Collapsed ? collapsedColor1 : expandedColor1;
        var backgroundColor2 = CollapsedState == CollapsedState.Collapsed ? collapsedColor2 : expandedColor2;

        var background = (ShapeVisual) oldGroup.Children[0];
        ((LinearGradientBrush) background.Brush).LinearColors = new[] { backgroundColor1, backgroundColor2 };
      }

      return oldGroup;
    }

    private class SymbolVisual : IVisual
    {
      private Point center;
      internal CollapsedState collapsedState;

      public SymbolVisual(Point center, CollapsedState collapsedState) {
        this.center = center;
        this.collapsedState = collapsedState;
      }

      public void Paint(IRenderContext context, Graphics g) {
        if (collapsedState == CollapsedState.Collapsed) {
          // draw plus sign
          g.FillRectangle(Brushes.LightGray, center.X - 10, center.Y - 4, 20, 8);
          g.FillRectangle(Brushes.LightGray, center.X - 4, center.Y - 10, 8, 20);
          g.FillRectangle(Brushes.White, center.X - 9, center.Y - 3, 18, 6);
          g.FillRectangle(Brushes.White, center.X - 3, center.Y - 9, 6, 18);
          g.FillRectangle(Brushes.LightGray, center.X - 8, center.Y - 2, 16, 4);
          g.FillRectangle(Brushes.LightGray, center.X - 2, center.Y - 8, 4, 16);
        } else if (collapsedState == CollapsedState.Expanded) {
          // draw minus sign
          g.FillRectangle(Brushes.LightGray, center.X - 10, center.Y - 4, 20, 8);
          g.FillRectangle(Brushes.White, center.X - 9, center.Y - 3, 18, 6);
          g.FillRectangle(Brushes.LightGray, center.X - 8, center.Y - 2, 16, 4);
        }

      }
    }
  }

  /// <summary>
  /// An implementation of <see cref="NodeStyleBase" /> which visualizes 
  /// the leaf nodes in the tree.
  /// </summary>
  public class LeafNodeStyle : NodeStyleBase<RectangleVisual> {

    private readonly Color backgroundColor1 = Color.FromArgb(255, 204, 255, 153);
    private readonly Color backgroundColor2 = Color.FromArgb(255, 153, 204, 51);

    protected override RectangleVisual CreateVisual(IRenderContext context, INode node) {
      var layout = node.Layout;
      return new RectangleVisual(0, 0, layout.Width, layout.Height) {
        Brush = new LinearGradientBrush(new PointF(0, 0), new PointF(0, (float) layout.Height), backgroundColor1, backgroundColor2),
        Pen = Pens.LightGray,
        Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y)
      };
    }

    protected override RectangleVisual UpdateVisual(IRenderContext context, RectangleVisual rect, INode node) {
      if (rect == null) {
        return CreateVisual(context, node);
      }
      // in this demo only the position can change, so this is the only thing we have to update
      var layout = node.Layout;
      if (rect.Transform.OffsetX != (float) layout.X || rect.Transform.OffsetY != (float) layout.Y) {
        rect.Transform = new Matrix(1, 0, 0, 1, (float) layout.X, (float) layout.Y);
      }
      return rect;
    }
  }
}
