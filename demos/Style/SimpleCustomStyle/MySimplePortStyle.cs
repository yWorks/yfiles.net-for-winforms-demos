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

using System.Drawing;
using System.Drawing.Drawing2D;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.SimpleCustomStyle
{
  /// <summary>
  /// This class is an example of a custom port style based on the <see cref="PortStyleBase{TVisual}"/> class.
  /// The port is rendered as a circle.
  /// </summary>
  public class MySimplePortStyle : PortStyleBase<EllipseVisual> {


    // the size of the port rendering - immutable
    private const int Width = 4;
    private const int Height = 4;
    private readonly Pen ellipsePen = new Pen(Color.FromArgb(80, 255, 255, 255));

    protected override EllipseVisual CreateVisual(IRenderContext context, IPort port) {
      // create the ellipse
      var visual = new EllipseVisual(0, 0, Width, Height) { Pen = ellipsePen };

      // and arrange it
      var transform = new Matrix();
      var topLeft = port.GetLocation() + new PointD(Width*0.5, Height*0.5);
      transform.Translate((float) topLeft.X, (float) topLeft.Y);
      visual.Transform = transform;
      return visual;
    }

    protected override EllipseVisual UpdateVisual(IRenderContext context, EllipseVisual oldVisual, IPort port) {
      // arrange the old ellipse
      var transform = new Matrix();
      var topLeft = port.GetLocation() + new PointD(Width*0.5, Height*0.5);
      transform.Translate((float) topLeft.X, (float) topLeft.Y);
      oldVisual.Transform = transform;
      return oldVisual;
    }

    /// <summary>
    /// Calculates the bounds of this port.
    /// </summary>
    /// <remarks>
    /// These are also used for arranging the visual, hit testing, visibility testing, and marquee box tests.
    /// </remarks>
    protected override RectD GetBounds(ICanvasContext context, IPort port) {
      return RectD.FromCenter(port.GetLocation(), new SizeD(Width, Height));
    }
  }
}