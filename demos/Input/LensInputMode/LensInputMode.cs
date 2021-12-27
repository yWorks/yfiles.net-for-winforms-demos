/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.4.
 ** Copyright (c) 2000-2021 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;

namespace Demo.yFiles.Graph.Input.LensInputMode
{
  /// <summary>
  /// A specialized <see cref="IInputMode"/> that displays the currently hovered-over
  /// part of the graph in some kind of magnifying glass.
  /// </summary>
  public class LensInputMode : InputModeBase
  {
    /// <summary>
    /// The <see cref="IVisual"/> that displays the magnifying <see cref="GraphControl"/>.
    /// </summary>
    private sealed class LensVisual : IVisual, IDisposable
    {
      private readonly LensInputMode inputMode;
      private readonly GraphControl lensGraphControl;
      private readonly Pen outlinePen = new Pen(Brushes.Gray, 2);
      private readonly GraphicsPath clippingPath;
      private Bitmap bitmap;
      private Region region;
      private Size currentSize;
      private bool disposed;

      public LensVisual(LensInputMode inputMode) {
        this.inputMode = inputMode;
        var canvasControl = ((GraphControl) inputMode.InputModeContext.CanvasControl);
        lensGraphControl = new GraphControl
        {
          Width = inputMode.Size.Width,
          Height = inputMode.Size.Height,

          Graph = canvasControl.Graph,
          Selection = canvasControl.Selection,
          Projection = canvasControl.Projection,
          
          Zoom = inputMode.ZoomFactor * canvasControl.Zoom,
          HorizontalScrollBarPolicy = ScrollBarVisibility.Never,
          VerticalScrollBarPolicy = ScrollBarVisibility.Never,

          // This is only necessary to show handles in the zoomed graph. Remove if not needed
          InputMode = new GraphEditorInputMode(),
        };
        bitmap = new Bitmap(inputMode.Size.Width, inputMode.Size.Height);
        clippingPath = new GraphicsPath();
        clippingPath.AddEllipse(new Rectangle(new Point(), inputMode.Size));
        region = new Region(clippingPath);
        currentSize = inputMode.Size;
      }

      public void Paint([NotNull] IRenderContext context, [NotNull] Graphics g) {
        if (!inputMode.mouseInside || !inputMode.Enabled) {
          return;
        }

        if (currentSize != inputMode.Size) {
          bitmap?.Dispose();
          bitmap = new Bitmap(inputMode.Size.Width, inputMode.Size.Height);

          // Update GraphControl size
          lensGraphControl.Width = inputMode.Size.Width;
          lensGraphControl.Height = inputMode.Size.Height;

          // Update clipping region
          clippingPath.Reset();
          clippingPath.AddEllipse(new Rectangle(new Point(), inputMode.Size));
          region?.Dispose();
          region = new Region(clippingPath);

          currentSize = inputMode.Size;
        }
        // Update the GraphControl with all relevant information
        var canvasControl = context.CanvasControl as GraphControl;

        lensGraphControl.Center = canvasControl.LastEventLocation;
        lensGraphControl.Zoom = inputMode.ZoomFactor * context.Zoom;
        lensGraphControl.Projection = context.Projection;

        // Draw to the bitmap
        lensGraphControl.DrawToBitmap(bitmap, new Rectangle(0, 0, inputMode.Size.Width, inputMode.Size.Height));
        // Draw the bitmap to the screen
        var drawingLocation = context.ToViewCoordinates(canvasControl.LastEventLocation).ToFloorPoint();
        var state = g.Save();
        g.Transform = context.ViewTransform;
        g.TranslateTransform(drawingLocation.X, drawingLocation.Y);
        g.Clip = region;
        g.DrawImageUnscaled(bitmap, new Point());
        g.ResetClip();
        g.DrawEllipse(outlinePen, new Rectangle(new Point(), inputMode.Size));
        g.Restore(state);
      }

      private void Dispose(bool disposing) {
        if (!disposed) {
          if (disposing) {
            bitmap?.Dispose();
            clippingPath?.Dispose();
            region?.Dispose();
            outlinePen?.Dispose();
          }
          disposed = true;
        }
      }

      public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
      }
    }

    /// <summary>
    /// The <see cref="ICanvasObjectGroup"/> containing the lens graph control.
    /// </summary>
    private ICanvasObjectGroup lensGroup;
    private LensVisual lensVisual;
    
    /// <summary>
    /// Indicates whether the mouse is inside the <see cref="CanvasControl"/>.
    /// If not, the magnifying <see cref="GraphControl"/> is not painted in the <see cref="LensVisual"/>.
    /// </summary>
    private bool mouseInside;

    private Size size = new Size(250, 250);
    private double zoomFactor = 3;

    /// <summary>
    /// The size of the "magnifying glass".
    /// </summary>
    public Size Size {
      get { return size; }
      set {
        size = value;
        InputModeContext?.CanvasControl?.Invalidate();
      }
    }

    /// <summary>
    /// The zoom factor used for magnifying the graph.
    /// </summary>
    public double ZoomFactor {
      get { return zoomFactor; }
      set {
        zoomFactor = value;
        InputModeContext?.CanvasControl?.Invalidate();
      }
    }

    /// <summary>
    /// Installs the <see cref="LensInputMode"/> by adding the <see cref="LensVisual"/>
    /// to the <see cref="lensGroup"/> and registering the necessary mouse event handlers.
    /// </summary>
    public override void Install(IInputModeContext context, ConcurrencyController controller) {
      base.Install(context, controller);

      var canvasControl = context.CanvasControl as GraphControl;

      lensGroup = canvasControl.RootGroup.AddGroup();
      lensGroup.Above(canvasControl.InputModeGroup);
      lensVisual = new LensVisual(this);
      lensGroup.AddChild(lensVisual, CanvasObjectDescriptors.Visual);

      canvasControl.Mouse2DMoved += UpdateLensLocation;
      canvasControl.Mouse2DDragged += UpdateLensLocation;

      canvasControl.Mouse2DEntered += UpdateLensVisibility;
      canvasControl.Mouse2DExited += UpdateLensVisibility;
    }

    /// <summary>
    /// Uninstalls the <see cref="LensInputMode"/> by removing the <see cref="lensGroup"/>
    /// and unregistering the various mouse event handlers.
    /// </summary>
    /// <param name="context"></param>
    public override void Uninstall(IInputModeContext context) {
      var canvasControl = context.CanvasControl;

      lensGroup.Remove();
      lensVisual.Dispose();

      canvasControl.Mouse2DMoved -= UpdateLensLocation;
      canvasControl.Mouse2DDragged -= UpdateLensLocation;

      canvasControl.Mouse2DEntered -= UpdateLensVisibility;
      canvasControl.Mouse2DExited -= UpdateLensVisibility;

      base.Uninstall(context);
    }

    private void UpdateLensLocation(object sender, Mouse2DEventArgs args) {
      InputModeContext.CanvasControl.Invalidate();
    }

    private void UpdateLensVisibility(object sender, Mouse2DEventArgs args) {
      mouseInside = args.EventType == Mouse2DEventTypes.Entered;
      InputModeContext.CanvasControl.Invalidate();
    }
  }
}
