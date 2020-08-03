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
using System.Drawing.Drawing2D;
using yWorks.Controls;

namespace Demo.yFiles.Layout.NodeLabeling
{
  /// <summary>
  /// Simple paintable that just displays an image in the background of the canvas.
  /// </summary>
  internal class BackgroundVisualCreator : IVisualCreator
  {
    private static readonly Image image;

    static BackgroundVisualCreator() {
      image = Image.FromFile("Resources/us_map.png");
    }

    public IVisual CreateVisual(IRenderContext context) {
      return new ImageVisual();
    }

    public IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      if (oldVisual is ImageVisual) {
        return oldVisual;
      }
      return CreateVisual(context);
    }

    private class ImageVisual : IVisual
    {
      public void Paint(IRenderContext context, Graphics g) {
        var old = g.InterpolationMode;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.DrawImage(image, new Rectangle(15, 5, 645, 399));
        g.InterpolationMode = old;
      }
    }
  }
}
