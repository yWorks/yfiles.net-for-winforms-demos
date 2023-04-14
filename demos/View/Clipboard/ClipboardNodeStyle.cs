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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Demo.yFiles.Toolkit;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Clipboard
{
  /// <summary>
  /// NodeStyle to visualize the value property of a business object and provide a handle to edit it.
  /// </summary>
  public class ClipboardNodeStyle : NodeStyleBase<IVisual>
  {
    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return new ClipboardNodeVisual(node);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      var cnv = oldVisual as ClipboardNodeVisual;
      if (cnv == null) {
        return CreateVisual(context, node);
      }
      cnv.node = node;
      return cnv;
    }

    private sealed class ClipboardNodeVisual : IVisual
    {
      internal INode node;

      public ClipboardNodeVisual(INode node) {
        this.node = node;
      }

      public void Paint(IRenderContext context, Graphics graphics) {
        var layout = node.Layout;

        // Draw a rounded rectangle
        var palette = Themes.Palette31;
        var path = NewRoundRectanglePath(layout, 3.5);

        // paint lower part
        graphics.FillPath(palette.Fill, path);

        // paint upper part
        var oldClip = graphics.Clip;
        graphics.IntersectClip(new RectangleF(
          (float) layout.X, (float) layout.Y, (float) layout.Width, (float) (layout.Height * 0.5)));
        graphics.FillPath(palette.NodeLabelFill, path);
        graphics.Clip = oldClip;

        // paint border
        try {
          graphics.DrawPath(new Pen(palette.Stroke, 1.5f), path);
        } catch {
          // ignore
        }

        ClipboardBusinessObject businessObject = node.Tag as ClipboardBusinessObject;
        if (businessObject != null) {
          // Draw a line to visualize the business object's value
          graphics.DrawLine(Pens.Black, (float) (layout.X + 10), (float) (layout.Y + (layout.Height*3/4)),
              (float) (layout.X + 10 + (layout.Width - 20)*businessObject.Value),
              (float) (layout.Y + (layout.Height*3/4)));
          // Display the business object's name
          graphics.DrawString(businessObject.Name, new Font("Arial", 8), Brushes.Black, (float) (layout.X + 10),
              (float) layout.Y + 10);
        }
      }

      private static GraphicsPath NewRoundRectanglePath(IRectangle layout, double radius) {
        var x = layout.X;
        var y = layout.Y;
        var width = layout.Width;
        var height = layout.Height;

        var arcX = Math.Min(width * 0.5, radius);
        var arcY = Math.Min(height * 0.5, radius);
      
        var bezierArcApproximation = 0.552284749830794; // (Math.Sqrt(2) - 1) * 4 / 3
        var cx = (1 - bezierArcApproximation) * arcX;
        var cy = (1 - bezierArcApproximation) * arcY;

        var path = new GeneralPath();
        path.MoveTo(x, y + arcY);
        path.CubicTo(x, y + cy, x + cx, y, x + arcX, y);
        path.LineTo(x + width - arcX, y);
        path.CubicTo(x + width - cx, y, x + width, y + cy, x + width, y + arcY);
        path.LineTo(x + width, y + height - arcY);
        path.CubicTo(x + width, y + height - cy, x + width - cx, y + height, x + width - arcX, y + height);
        path.LineTo(x + arcX, y + height);
        path.CubicTo(x + cx, y + height, x, y + height - cy, x, y + height - arcY);
        path.Close();
        return path.CreatePath();
      }
    }


    /// <summary>
    /// Provides a custom handle for modifying the business object's value
    /// </summary>
    protected override object Lookup(INode node, Type type) {
      if (type == typeof (IHandleProvider)) {
        return new MyHandleProvider(node);
      } else if (type == typeof(INodeSizeConstraintProvider)) {
        return new NodeSizeConstraintProvider(new SizeD(120, 60), SizeD.Infinite);
      } else {
        return base.Lookup(node, type);
      }
    }

    /// <summary>
    /// Custom handle provider
    /// </summary>
    private sealed class MyHandleProvider : IHandleProvider
    {
      private readonly INode node;

      public MyHandleProvider(INode node) {
        this.node = node;
      }

      public IEnumerable<IHandle> GetHandles(IInputModeContext inputModeContext) {
        return new List<IHandle> {new DataHandle(node)};
      }

      /// <summary>
      /// Handle for modifying the value of the business object
      /// </summary>
      private sealed class DataHandle : IHandle, IPoint
      {
        private readonly INode node;
        private double originalValue;

        public DataHandle(INode node) {
          this.node = node;
        }

        public IPoint Location {
          get { return this; }
        }

        public void InitializeDrag(IInputModeContext inputModeContext) {
          // remember the original value so the drag can be cancelled correctly
          ClipboardBusinessObject clipboardBusinessObject = node.Tag as ClipboardBusinessObject;
          if (clipboardBusinessObject != null) {
            originalValue = clipboardBusinessObject.Value;
          }
        }

        public void HandleMove(IInputModeContext inputModeContext, PointD originalLocation, PointD newLocation) {
          double minVal = node.Layout.X + 10;
          double maxVal = node.Layout.X + node.Layout.Width -10;
          double currentVal = newLocation.X;
          double ratio = ((currentVal-minVal) / (maxVal-minVal));
          var clipboardBusinessObject = node.Tag as ClipboardBusinessObject;
          if (clipboardBusinessObject != null) {
            clipboardBusinessObject.Value = ratio;
          }
        }

        public void CancelDrag(IInputModeContext inputModeContext, PointD originalLocation) {
          // restore original value
          var clipboardBusinessObject = node.Tag as ClipboardBusinessObject;
          if (clipboardBusinessObject != null) {
            clipboardBusinessObject.Value = originalValue;
          }
        }

        public void DragFinished(IInputModeContext inputModeContext, PointD originalLocation, PointD newLocation) {
          // the value is already set.
          // we create an Undo unit, though, to make the edit undoable
          var clipboardBusinessObject = node.Tag as ClipboardBusinessObject;
          if (clipboardBusinessObject != null && clipboardBusinessObject.Value != originalValue) {
            var undoEngine = inputModeContext.GetGraph().GetUndoEngine();
            if (undoEngine != null) {
              undoEngine.AddUnit(new BusinessValueUndoUnit(clipboardBusinessObject, originalValue));
            }
          }
        }

        public void HandleClick([NotNull] ClickEventArgs eventArgs) {
        }

        public HandleTypes Type {
          get { return HandleTypes.Resize | HandleTypes.Variant2; }
        }

        public Cursor Cursor {
          get { return Cursors.SizeWE; }
        }

        double IPoint.X {
          get {
            // calculate the X position of the handle
            ClipboardBusinessObject businessObject = node.Tag as ClipboardBusinessObject;
            if (businessObject != null) {
              return (node.Layout.X + 10 + (node.Layout.Width - 20)*businessObject.Value);
            }
            return 0;
          }
        }

        double IPoint.Y {
          // Y position doesn't depend on business object
          get { return node.Layout.Y + node.Layout.Height*0.75d; }
        }
      }

      /// <summary>
      /// Custom <see cref="IUndoUnit"/> to make value changes undoable.
      /// </summary>
      private sealed class BusinessValueUndoUnit : UndoUnitBase
      {
        private readonly ClipboardBusinessObject obj;
        private readonly double oldValue;
        private double newValue;

        /// <summary>
        /// Creates an instance for the given business object which restores the given <paramref name="oldValue"/> upon undo.
        /// </summary>
        /// <param name="obj">The business object to handle.</param>
        /// <param name="oldValue">The original value which will be restored upon undo.</param>
        public BusinessValueUndoUnit(ClipboardBusinessObject obj, double oldValue) : base("Change Value") {
          this.obj = obj;
          this.oldValue = oldValue;
        }

        /// <summary>
        /// Undo: remember the current value for redo and restore the old value.
        /// </summary>
        public override void Undo() {
          this.newValue = obj.Value;
          obj.Value = oldValue;
        }

        /// <summary>
        /// Redo: restore the value the business object had before undo.
        /// </summary>
        public override void Redo() {
          obj.Value = newValue;
        }
      }
    }
  }
}
