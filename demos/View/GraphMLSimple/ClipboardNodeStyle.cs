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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.IO.GraphML.Simple
{

  /// <summary>
  /// NodeStyle to visualize the value property of a business object and provide a handle to edit it.
  /// </summary>
  /// <remarks>This NodeStyle and it's business object are copied from Demo.yFiles.Graph.Clipboard 
  /// to show how a custom style and a business object get serialized in GraphML.</remarks>
  public class ClipboardNodeStyle : NodeStyleBase<IVisual>
  {
    private const double roundingFactor = 0.1;

    private class ClipboardNodeStyleVisual : IVisual
    {

      private const double roundingFactor = 0.1;

      public INode Node { get; set; }

      public void Paint(IRenderContext renderContext, Graphics graphics) {
        var node = Node;
        var layout = node.Layout;

        // Draw a rounded rectangle
        double roundingX = layout.Width*roundingFactor;
        double roundingY = layout.Height*roundingFactor;

        ClipboardBusinessObject businessObject = node.Tag as ClipboardBusinessObject;

        GraphicsPath upperPath = new GraphicsPath();
        GraphicsPath lowerPath = new GraphicsPath();
        upperPath.AddBezier((float) layout.X, (float) (layout.Y + roundingY), (float) layout.X,
            (float) (layout.Y + (roundingY/2)),
            (float) (layout.X + (roundingX/2)), (float) layout.Y, (float) (layout.X + roundingX), (float) layout.Y);
        upperPath.AddLine((float) (layout.X + roundingX), (float) layout.Y,
            (float) (layout.X + layout.Width - roundingX), (float) layout.Y);

        upperPath.AddBezier((float) (layout.X + layout.Width - roundingX), (float) (layout.Y),
            (float) (layout.X + layout.Width - (roundingX/2)), (float) (layout.Y),
            (float) (layout.X + layout.Width), (float) (layout.Y + (roundingY/2)), (float) (layout.X + layout.Width),
            (float) (layout.Y + roundingY));
        upperPath.AddLine((float) (layout.X + layout.Width), (float) (layout.Y + roundingY),
            (float) (layout.X + layout.Width), (float) (layout.Y + layout.Height/2));

        upperPath.AddLine((float) (layout.X + layout.Width), (float) (layout.Y + layout.Height/2), (float) (layout.X),
            (float) (layout.Y + layout.Height/2));
        upperPath.AddLine((float) (layout.X), (float) (layout.Y + layout.Height/2), (float) layout.X,
            (float) (layout.Y + roundingY));


        lowerPath.AddBezier((float) (layout.X + layout.Width), (float) (layout.Y + layout.Height - roundingY),
            (float) (layout.X + layout.Width), (float) (layout.Y + layout.Height - (roundingY/2)),
            (float) (layout.X + layout.Width - (roundingX/2)), (float) (layout.Y + layout.Height),
            (float) (layout.X + layout.Width - roundingX), (float) (layout.Y + layout.Height));
        lowerPath.AddLine((float) (layout.X + layout.Width - roundingX), (float) (layout.Y + layout.Height),
            (float) (layout.X + roundingX), (float) (layout.Y + layout.Height));

        lowerPath.AddBezier((float) (layout.X + roundingX), (float) (layout.Y + layout.Height),
            (float) (layout.X + (roundingX/2)), (float) (layout.Y + layout.Height),
            (float) (layout.X), (float) (layout.Y + layout.Height - (roundingY/2)), (float) (layout.X),
            (float) (layout.Y + layout.Height - roundingY));
        lowerPath.AddLine((float) layout.X, (float) (layout.Y + layout.Height - roundingY), (float) (layout.X),
            (float) (layout.Y + (layout.Height/2)));
        lowerPath.AddLine((float) layout.X, (float) (layout.Y + (layout.Height/2)), (float) (layout.X + layout.Width),
            (float) (layout.Y + (layout.Height/2)));
        lowerPath.AddLine((float) (layout.X + layout.Width), (float) (layout.Y + (layout.Height/2)),
            (float) (layout.X + layout.Width), (float) (layout.Y + layout.Height - roundingY));

        graphics.FillPath(
            new LinearGradientBrush(
                new RectangleF((float) layout.X, (float) layout.Y, (float) layout.Width, (float) layout.Height),
                Mix(Color.SkyBlue, Color.White, 0.5), Mix(Color.DodgerBlue, Color.White, 0.5),
                90f), upperPath);
        graphics.FillPath(
            new LinearGradientBrush(
                new RectangleF((float) layout.X, (float) layout.Y, (float) layout.Width, (float) layout.Height),
                Color.SkyBlue, Color.DodgerBlue, 90f), lowerPath);


        if (businessObject != null) {
          // Draw a line to vizualize the business object's value
          graphics.DrawLine(Pens.Black, (float) (layout.X + 10), (float) (layout.Y + (layout.Height*3/4)),
              (float) (layout.X + 10 + (layout.Width - 20)*businessObject.Value),
              (float) (layout.Y + (layout.Height*3/4)));
          // Display the business object's name
          graphics.DrawString(businessObject.Name, new Font("Arial", 8), Brushes.Black, (float) (layout.X + 10),
              (float) layout.Y + 10);
        }
      }

      ///<summary>
      /// Mixes two colors using the provided ratio.
      ///</summary>
      private static Color Mix(Color color0, Color color1, double ratio) {
        double iratio = 1 - ratio;
        double a = color0.A*ratio + color1.A*iratio;
        double r = color0.R*ratio + color1.R*iratio;
        double g = color0.G*ratio + color1.G*iratio;
        double b = color0.B*ratio + color1.B*iratio;
        return
            Color.FromArgb((int) Math.Round(a), (int) Math.Round(r),
                (int) Math.Round(g), (int) Math.Round(b));
      }
    }

    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return new ClipboardNodeStyleVisual {Node = node};
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      var cnsv = oldVisual as ClipboardNodeStyleVisual;
      if (cnsv == null) {
        return CreateVisual(context, node);
      }
      cnsv.Node = node;
      return cnsv;
    }

    /// <summary>
    /// Provides a custom handle for modifing the business object's value
    /// </summary>
    protected override object Lookup(INode node, Type type) {
      if (type == typeof(IHandleProvider)) {
        return new MyHandleProvider(node);
      } else {
        return base.Lookup(node, type);
      }
    }

    /// <summary>
    /// Custom handle provider
    /// </summary>
    internal class MyHandleProvider : IHandleProvider
    {
      private readonly INode node;

      public MyHandleProvider(INode node) {
        this.node = node;
      }

      public IEnumerable<IHandle> GetHandles(IInputModeContext inputModeContext) {
        return new List<IHandle> { new DataHandle(node) };
      }

      /// <summary>
      /// Handle for modifying the value of the business object
      /// </summary>
      internal class DataHandle : IHandle, IPoint
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
          double maxVal = node.Layout.X + node.Layout.Width - 10;
          double currentVal = newLocation.X;
          double ratio = ((currentVal - minVal) / (maxVal - minVal));
          ClipboardBusinessObject clipboardBusinessObject = node.Tag as ClipboardBusinessObject;
          if (clipboardBusinessObject != null) {
            double oldValue = clipboardBusinessObject.Value;
            clipboardBusinessObject.Value = ratio;
          }
        }

        public void CancelDrag(IInputModeContext inputModeContext, PointD originalLocation) {
          // restore original value
          ClipboardBusinessObject clipboardBusinessObject = node.Tag as ClipboardBusinessObject;
          if (clipboardBusinessObject != null) {
            clipboardBusinessObject.Value = originalValue;
          }
        }

        public void DragFinished(IInputModeContext inputModeContext, PointD originalLocation, PointD newLocation) {
          // Do nothing
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
              return (node.Layout.X + 10 + (node.Layout.Width - 20) * businessObject.Value);
            }
            return 0;
          }
        }

        double IPoint.Y {
          // Y position doesn't depend on business object
          get { return node.Layout.Y + node.Layout.Height * 0.75d; }
        }
      }
    }
  }

  /// <summary>
  /// Example business object with just two properties to show the 
  /// GraphML serialization of an object in the node's tag.
  /// </summary>
  public class ClipboardBusinessObject : INotifyPropertyChanged
  {
    private string name;
    private double value = 0.5d;

    public string Name {
      get { return name; }
      set {
        if (name != value) {
          name = value;
          if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs("Name"));
          }
        }
      }
    }

    public double Value {
      get { return value; }
      set {
        if (value < 0) {
          this.value = 0;
        } else if (value > 1) {
          this.value = 1;
        } else {
          this.value = value;
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
