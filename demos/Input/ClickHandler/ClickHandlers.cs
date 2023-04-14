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
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.ClickHandler
{
  public interface IEnhancedClickHandler : IClickHandler
  {
    Cursor GetCursor(IInputModeContext context, PointD location);
  }

  public class GrowShrinkButtonNodeStyleDecorator : NodeStyleBase<VisualGroup>
  {
    private const double MinNodeHeight = 40;
    private const double MaxNodeHeight = 150;
    private const double GrowShrinkStep = 10;
    private const double ButtonRadius = ButtonDiameter / 2;
    private const double ButtonDiameter = 25;
    private const double ButtonDistance = 5;

    public GrowShrinkButtonNodeStyleDecorator(INodeStyle wrappedStyle) {
      if (wrappedStyle == null) {
        throw new ArgumentNullException("wrappedStyle");
      }
      WrappedStyle = wrappedStyle;
    }

    public INodeStyle WrappedStyle { get; private set; }

    protected override object Lookup(INode node, Type type) {
      // Return the custom click handler when asked for it
      if (type == typeof(IClickHandler)) {
        return new GrowShrinkButtonClickHandler(node);
      }
      return base.Lookup(node, type);
    }

    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      // Determine which button was hit, if any
      var hitButton = GetHitButton(context, node, context.CanvasControl.LastEventLocation);
      var group = new VisualGroup {
        Children = {
          WrappedStyle.Renderer.GetVisualCreator(node, WrappedStyle).CreateVisual(context),
          new GrowShrinkButtonVisual(node, hitButton)
        }
      };
      return group;
    }

    private static bool CanShrink(INode node) {
      return node.Layout.Height > MinNodeHeight;
    }

    private static bool CanGrow(INode node) {
      return node.Layout.Height < MaxNodeHeight;
    }

    private static PointD GetAnchor(INode node) {
      var layout = node.Layout;
      var anchor = new PointD(layout.GetCenter().X, layout.Y + 20);
      return anchor;
    }

    private static ButtonType GetHitButton(ICanvasContext context, INode node, PointD location) {
      // Get anchor point near the top of the node
      var anchor = GetAnchor(node);
      var upAnchor = anchor - new PointD(ButtonRadius + ButtonDistance / 2, 0);
      var downAnchor = anchor + new PointD(ButtonRadius + ButtonDistance / 2, 0);
      var circleRadius = ButtonRadius + context.HitTestRadius;
      if (CanShrink(node) && upAnchor.DistanceTo(location) < circleRadius) {
        return ButtonType.Shrink;
      }
      if (CanGrow(node) && downAnchor.DistanceTo(location) < circleRadius) {
        return ButtonType.Grow;
      }
      return ButtonType.None;
    }

    /// <summary>
    /// Enumeration for determining which button has been hit.
    /// </summary>
    enum ButtonType
    {
      None, Shrink, Grow
    }

    /// <summary>
    /// Click handler implementation that handles resizing the node upon click and provides a suitable cursor
    /// for <see cref="ClickHandlerHoverInputMode"/>.
    /// </summary>
    class GrowShrinkButtonClickHandler : IEnhancedClickHandler, IHitTestable
    {
      private readonly INode node;

      public GrowShrinkButtonClickHandler(INode node) {
        this.node = node;
      }

      public Cursor GetCursor(IInputModeContext context, PointD location) {
        var hoveredButton = GetHitButton(context, node, location);
        if (hoveredButton == ButtonType.Shrink) {
          return Cursors.PanNorth;
        }
        if (hoveredButton == ButtonType.Grow) {
          return Cursors.PanSouth;
        }
        return null;
      }

      public IHitTestable HitTestable {
        get {
          // Just return ourselves here, to ease implementation.
          return this;
        }
      }

      public void OnClicked(IInputModeContext context, PointD location) {
        var clickedButton = GetHitButton(context, node, location);
        var graph = ((GraphControl) context.CanvasControl).Graph;
        double newHeight = node.Layout.Height;
        if (clickedButton == ButtonType.Shrink) {
          newHeight -= GrowShrinkStep;
        } else if (clickedButton == ButtonType.Grow) {
          newHeight += GrowShrinkStep;
        }
        var newLayout = new RectD(node.Layout.X, node.Layout.Y, node.Layout.Width, Math.Min(MaxNodeHeight, Math.Max(MinNodeHeight, newHeight)));
        graph.SetNodeLayout(node, newLayout);
      }

      public bool IsHit(IInputModeContext context, PointD location) {
        var whichButton = GetHitButton(context, node, location);
        return whichButton != ButtonType.None;
      }
    }

    /// <summary>
    /// Visual that draws the buttons and can highlight the currently hovered button.
    /// </summary>
    class GrowShrinkButtonVisual : IVisual
    {
      private readonly INode node;
      private readonly ButtonType hoveredButton;

      private static readonly Pen disabledPen = new Pen(Color.FromArgb(128, Color.LightGray));
      private static readonly Brush disabledBrush = new SolidBrush(Color.FromArgb(128, Color.DimGray));

      public GrowShrinkButtonVisual(INode node, ButtonType hoveredButton) {
        this.node = node;
        this.hoveredButton = hoveredButton;
      }

      public void Paint(IRenderContext context, Graphics g) {
        // Get anchor point near the top of the node
        var anchor = GetAnchor(node);

        // Shrink button
        var canShrink = CanShrink(node);
        g.FillEllipse(canShrink ? Brushes.DarkSlateGray : disabledBrush, (float) (anchor.X - ButtonDistance / 2 - ButtonDiameter), (float) (anchor.Y - ButtonRadius), (float) ButtonDiameter, (float) ButtonDiameter);
        var up = new GraphicsPath();
        up.AddPolygon(new PointF[] {
          anchor - new PointD(5, 0), anchor - new PointD(15, 6), anchor - new PointD(25, 0),
          anchor - new PointD(25, -4), anchor - new PointD(15, 2), anchor - new PointD(5, -4)
        });
        if (hoveredButton == ButtonType.Shrink) {
          g.FillPath(Brushes.White, up);
        }
        g.DrawPath(canShrink ? Pens.White : disabledPen, up);

        // Grow button
        var canGrow = CanGrow(node);
        g.FillEllipse(canGrow ? Brushes.DarkSlateGray : disabledBrush, (float) (anchor.X + ButtonDistance / 2), (float) (anchor.Y - ButtonRadius), (float) ButtonDiameter, (float) ButtonDiameter);
        var down = new GraphicsPath();
        down.AddPolygon(new PointF[] {
          anchor + new PointD(5, 0), anchor + new PointD(15, 6), anchor + new PointD(25, 0),
          anchor + new PointD(25, -4), anchor + new PointD(15, 2), anchor + new PointD(5, -4)
        });
        if (hoveredButton == ButtonType.Grow) {
          g.FillPath(Brushes.White, down);
        }
        g.DrawPath(canGrow ? Pens.White : disabledPen, down);
      }
    }
  }
}