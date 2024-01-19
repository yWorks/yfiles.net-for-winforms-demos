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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using Pen = System.Drawing.Pen;

namespace Demo.yFiles.Complete.RotatableNodes
{
  /// <summary>
  /// An extension of <see cref="OrientedRectangleIndicatorInstaller"/> that uses the rotated layout 
  /// of nodes using a <see cref="RotatableNodeStyleDecorator"/>.
  /// </summary>
  /// <remarks>
  /// The indicator will be rotated to fit the rotated bounds of the node.
  /// </remarks>
  public class RotatableNodeIndicatorInstaller : OrientedRectangleIndicatorInstaller
  {
    /// <summary>
    /// A <see cref="ResourceKey"/> that will be used to find the <see cref="TemplateVisual"/>
    /// for drawing the selection indicator.
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    public static readonly ResourceKey NodeSelectionTemplateKey =
      new ComponentResourceKey(typeof(LabelPositionHandler), "NodeSelectionTemplateKey");

    static RotatableNodeIndicatorInstaller()
    {
      var r = ApplicationResources.Instance;
      r[NodeSelectionTemplateKey] = new RotatableNodeIndicatorTemplate();
    }

    /// <summary>
    /// Create a new instance with the specified template key and no fixed node layout.
    /// </summary>
    /// <param name="templateKey">The key to lookup the visualization template.</param>
    public RotatableNodeIndicatorInstaller(ResourceKey templateKey) : base(null, templateKey) { }

    /// <summary>
    /// Returns the rotated layout of the specified node.
    /// </summary>
    protected override IOrientedRectangle GetRectangle(object item) {
      var node = item as INode;
      var styleWrapper = node.Style as RotatableNodeStyleDecorator;
      if (styleWrapper != null) {
        return styleWrapper.GetRotatedLayout(node);
      }
      return new OrientedRectangle(node.Layout);
    }
  }

  public class RotatableNodeIndicatorTemplate : TemplateVisual {
    private readonly Pen pen = new Pen(new HatchBrush(HatchStyle.Percent50, Color.White, Color.Black), 3);

    public override void Paint(IRenderContext ctx, Graphics g) {
      var scale = ctx.Scale;
      var x = (float)(Math.Floor(X * scale) / scale);
      var y = (float)(Math.Floor(Y * scale) / scale);
      var x2 = (float)(Math.Ceiling(Bounds.MaxX * scale) / scale);
      var y2 = (float)(Math.Ceiling(Bounds.MaxY * scale) / scale);
      var cx = x + (x2 - x) / 2;
      var cy = y + (y2 - y) / 2;

      g.DrawRectangle(pen, x, y, x2 - x, y2 - y);
      g.DrawEllipse(Pens.Black, cx - 4, cy - 4, 8, 8);
      g.DrawLine(pen, cx, y, cx, y - 20);
    }
  }
}
