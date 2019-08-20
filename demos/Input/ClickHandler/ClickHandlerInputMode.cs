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

using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Graph;

namespace Demo.yFiles.Graph.Input.ClickHandler
{
  /// <summary>
  /// <see cref="IInputMode"/> implementation that works closely with <see cref="IEnhancedClickHandler"/>
  /// to allow cursor changes and handling hovering over a zone managed by the click handler.
  /// </summary>
  public class ClickHandlerHoverInputMode : InputModeBase
  {
    private IEnhancedClickHandler currentClickHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClickHandlerHoverInputMode"/> class.
    /// </summary>
    public ClickHandlerHoverInputMode() {
      // Allow this input mode to run concurrently with other input modes
      Exclusive = false;
    }

    /// <summary>
    /// Sets a new <see cref="IEnhancedClickHandler"/> that's currently hovered.
    /// </summary>
    /// <remarks>This also updates the cursor.</remarks>
    private void SetCurrentClickHandler(IEnhancedClickHandler newClickHandler) {
      currentClickHandler = newClickHandler;
      // Update cursor
      if (Controller != null) {
        if (currentClickHandler != null) {
          Controller.PreferredCursor = currentClickHandler.GetCursor(InputModeContext, InputModeContext.CanvasControl.LastEventLocation);
        } else if (Controller.PreferredCursor != null) {
          Controller.PreferredCursor = null;
        }
      }
      // Repaint to allow for custom rendering when a click handler is hovered
      if (InputModeContext != null && InputModeContext.CanvasControl != null) {
        InputModeContext.CanvasControl.Invalidate();
      }
    }

    public override void Install(IInputModeContext context, ConcurrencyController controller) {
      base.Install(context, controller);
      InputModeContext.CanvasControl.Mouse2DMoved += OnMouseMoved;
    }

    public override void Uninstall(IInputModeContext context) {
      InputModeContext.CanvasControl.Mouse2DMoved -= OnMouseMoved;
      currentClickHandler = null;
      base.Uninstall(context);
    }

    private void OnMouseMoved(object sender, Mouse2DEventArgs e) {
      var location = e.Location;
      // Find items at the current location that have our special IClickHandler
      var hitTester = InputModeContext.Lookup<IHitTester<IModelItem>>();
      var hits = hitTester.EnumerateHits(InputModeContext, location);
      foreach (var hit in hits) {
        var clickHandler = hit.Lookup<IClickHandler>() as IEnhancedClickHandler;
        if (clickHandler != null) {
          // If we hit that IClickHandler, then store the current one (which updates the cursor as well)
          if (clickHandler.HitTestable.IsHit(InputModeContext, location)) {
            SetCurrentClickHandler(clickHandler);
            return;
          }
        }
      }
      // No IClickHandler hit, reset the property (and the cursor).
      SetCurrentClickHandler(null);
    }
  }
}