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

using System.Drawing;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.GraphEditor.Styles
{
  /// <summary>
  /// Basic implementation of port style. Renders a port as a circle.
  /// </summary>
  public class CirclePortStyle : PortStyleBase<IVisual>
  {
    private const double Width = 6;
    private const double Height = 6;
    private readonly Pen pen = new Pen(Brushes.Black);

    protected override IVisual CreateVisual(IRenderContext context, IPort port) {
      var ellipse = new EllipseVisual {Pen = pen};
      ellipse.Rect.Reshape(GetBounds(context, port));
      return ellipse;
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, IPort port) {
      var ellipse = oldVisual as EllipseVisual;
      if (ellipse == null) {
        return CreateVisual(context, port);
      }
      ellipse.Rect.Reshape(GetBounds(context, port));
      return oldVisual;
    }

    protected override RectD GetBounds(ICanvasContext context, IPort port) {
      var location = port.GetLocation();
      return new RectD(location.X - Width/2, location.Y - Height/2, Width, Height);
    }

    private class EllipseVisual : IVisual
    {

      public Pen Pen { get; set; }

      public MutableRectangle Rect {
        get { return rect; }
      }

      private readonly MutableRectangle rect = new MutableRectangle();

      public void Paint(IRenderContext context, Graphics g) {
        if (Pen != null) {
          g.DrawEllipse(Pen, (float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height);
        }
      }
    }
  }
}
