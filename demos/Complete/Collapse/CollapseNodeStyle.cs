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
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Geometry;
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
  public class CollapseNodeStyle : NodeStyleBase<IVisual>
  {
    private readonly INodeStyle collapsedBackground;
    private readonly INodeStyle expandedBackground;

    public CollapseNodeStyle() {
      collapsedBackground = DemoStyles.CreateDemoNodeStyle(Themes.Palette401);
      expandedBackground = DemoStyles.CreateDemoNodeStyle(Themes.Palette403);
    }

    public CollapsedState CollapsedState { get; set; }

    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      var state = CollapsedState;
      var background = CollapsedState.Collapsed == state ? collapsedBackground : expandedBackground;
      return new CollapseNodeVisual {
          background = background.Renderer.GetVisualCreator(node, background).CreateVisual(context),
          center = node.Layout.GetCenter(),
          state = state
      };
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      var cv = oldVisual as CollapseNodeVisual;
      if (cv != null) {
        var state = CollapsedState;
        var background = CollapsedState.Collapsed == state ? collapsedBackground : expandedBackground;
        cv.background = background.Renderer.GetVisualCreator(node, background).UpdateVisual(context, cv.background);
        cv.center = node.Layout.GetCenter();
        cv.state = state;
        return cv;
      }
      return CreateVisual(context, node);
    }

    private sealed class CollapseNodeVisual : IVisual
    {
      internal IVisual background;
      internal PointD center;
      internal CollapsedState state;
      private readonly Brush brush;

      internal CollapseNodeVisual() {
        brush = new SolidBrush(Color.FromArgb(0x99, 0x99, 0x99));
      }

      public void Paint(IRenderContext context, Graphics graphics) {
        var bg = background;
        if (bg != null) {
          bg.Paint(context, graphics);
        }

        PaintStateSymbol(graphics);
      }

      private void PaintStateSymbol(Graphics graphics) {
        var x = (float) center.X;
        var y = (float) center.Y;
        if (CollapsedState.Collapsed == state) {
          // draw plus sign
          graphics.FillRectangle(Brushes.White, x - 9, y - 3, 18, 6);
          graphics.FillRectangle(Brushes.White, x - 3, y - 9, 6, 18);
          graphics.FillRectangle(brush, x - 8, y - 2, 16, 4);
          graphics.FillRectangle(brush, x - 2, y - 8, 4, 16);
        } else if (CollapsedState.Expanded == state) {
          // draw minus sign
          graphics.FillRectangle(Brushes.White, x - 9, y - 3, 18, 6);
          graphics.FillRectangle(brush, x - 8, y - 2, 16, 4);
        }
      }
    }
  }

  /// <summary>
  /// An implementation of <see cref="NodeStyleBase" /> which visualizes 
  /// the leaf nodes in the tree.
  /// </summary>
  public class LeafNodeStyle : NodeStyleBase<IVisual>
  {
    private readonly INodeStyle wrapped;

    public LeafNodeStyle() {
      wrapped = DemoStyles.CreateDemoNodeStyle(Themes.Palette25);
    }

    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return wrapped.Renderer.GetVisualCreator(node, wrapped).CreateVisual(context);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      return wrapped.Renderer.GetVisualCreator(node, wrapped).UpdateVisual(context, oldVisual);
    }
  }
}
