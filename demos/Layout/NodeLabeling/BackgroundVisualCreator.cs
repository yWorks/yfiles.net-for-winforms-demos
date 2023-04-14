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

using System.Drawing;
using System.Globalization;
using System.IO;
using yWorks.Controls;

namespace Demo.yFiles.Layout.NodeLabeling
{
  /// <summary>
  /// Simple paintable that just displays an image in the background of the canvas.
  /// </summary>
  internal class BackgroundVisualCreator : IVisualCreator
  {
    private static readonly PointF[] usMap;

    static BackgroundVisualCreator() {
      usMap = GetUSMap();
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
        g.FillPolygon(new SolidBrush(Color.FromArgb(0xCC, 0xCC, 0xCC)), usMap);
      }
    }

    private static PointF[] GetUSMap() {
      var lines = File.ReadAllLines("Resources/us_map.txt");
      PointF[] coordinates = new PointF[lines.Length];
      for (int i = 0; i < lines.Length; i++) {
        var point = lines[i].Split(' ');
        var c1 = float.Parse(point[0], CultureInfo.InvariantCulture);
        var c2 = float.Parse(point[1], CultureInfo.InvariantCulture);
        coordinates[i] = new PointF(c1, c2);
      }
      return coordinates;
    }
  }
}
