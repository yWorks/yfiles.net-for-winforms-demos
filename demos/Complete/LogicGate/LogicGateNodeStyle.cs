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
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Layout.LogicGate
{
  /// <summary>
  /// A very simple implementation of an <see cref="INodeStyle"/> that displays logic gates.
  /// </summary>
  public class LogicGateNodeStyle : NodeStyleBase<IVisual>
  {
    // node fill
    private static readonly Color FillColor = Color.WhiteSmoke;
    private static readonly Color OutlineColor = Color.Black;

    private static readonly GeneralPath AndOutlinePath, OrOutlinePath, NandOutlinePath, NorOutlinePath, NotOutlinePath;

    /// <summary>
    /// Gets or sets the type of the logic gate.
    /// </summary>
    /// <value>
    /// The type of the logic gate this style should represent.
    /// </value>
    public LogicGateType GateType { get; set; }
    
    static LogicGateNodeStyle() {
      // path for AND nodes
      AndOutlinePath = new GeneralPath();
      AndOutlinePath.MoveTo(0.6, 0);
      AndOutlinePath.LineTo(0.1, 0);
      AndOutlinePath.LineTo(0.1, 1);
      AndOutlinePath.LineTo(0.6, 1);
      AndOutlinePath.QuadTo(0.8, 1.0, 0.8, 0.5);
      AndOutlinePath.QuadTo(0.8, 0.0, 0.6, 0);

      // path for OR nodes
      OrOutlinePath = new GeneralPath();
      OrOutlinePath.MoveTo(0.6, 0);
      OrOutlinePath.LineTo(0.1, 0);
      OrOutlinePath.QuadTo(0.3, 0.5, 0.1, 1);
      OrOutlinePath.LineTo(0.6, 1);
      OrOutlinePath.QuadTo(0.8, 1.0, 0.8, 0.5);
      OrOutlinePath.QuadTo(0.8, 0.0, 0.6, 0);

      // path for NAND nodes
      NandOutlinePath = new GeneralPath();
      NandOutlinePath.MoveTo(0.6, 0);
      NandOutlinePath.LineTo(0.1, 0);
      NandOutlinePath.LineTo(0.1, 1);
      NandOutlinePath.LineTo(0.6, 1);
      NandOutlinePath.QuadTo(0.8, 1.0, 0.8, 0.5);
      NandOutlinePath.QuadTo(0.8, 0.0, 0.6, 0);
      NandOutlinePath.AppendEllipse(new RectD(0.8, 0.4, 0.1, 0.2), false);

      // path for NOR nodes
      NorOutlinePath = new GeneralPath();
      NorOutlinePath.MoveTo(0.6, 0);
      NorOutlinePath.LineTo(0.1, 0);
      NorOutlinePath.QuadTo(0.3, 0.5, 0.1, 1);
      NorOutlinePath.LineTo(0.6, 1);
      NorOutlinePath.QuadTo(0.8, 1.0, 0.8, 0.5);
      NorOutlinePath.QuadTo(0.8, 0.0, 0.6, 0);
      NorOutlinePath.AppendEllipse(new RectD(0.8, 0.4, 0.1, 0.2), false);

      // path for NOT nodes
      NotOutlinePath = new GeneralPath();
      NotOutlinePath.MoveTo(0.8, 0.5);
      NotOutlinePath.LineTo(0.1, 0);
      NotOutlinePath.LineTo(0.1, 1);
      NotOutlinePath.LineTo(0.8, 0.5);
      NotOutlinePath.AppendEllipse(new RectD(0.8, 0.4, 0.1, 0.2), false);
    }

    #region Rendering

    protected override GeneralPath GetOutline(INode node) {
      var layout = node.Layout;
      return GetNodeOutlinePath().CreateGeneralPath(new Matrix2D(layout.Width, 0, 0, layout.Height, layout.X, layout.Y));
    }

    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      return new LogicGateVisual(node, GateType, GetNodeOutlinePath());
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      var lgv = oldVisual as LogicGateVisual;
      if (lgv == null) {
        return CreateVisual(context, node);
      }
      lgv.node = node;
      if (lgv.gateType != GateType) {
        lgv.gateType = GateType;
        lgv.outlinePath = GetNodeOutlinePath();
      }
      return oldVisual;
    }

    private class LogicGateVisual : IVisual
    {
      public INode node;
      public LogicGateType gateType;
      public GeneralPath outlinePath;

      public LogicGateVisual(INode node, LogicGateType gateType, GeneralPath outlinePath) {
        this.node = node;
        this.gateType = gateType;
        this.outlinePath = outlinePath;
      }

      public void Paint(IRenderContext context, Graphics graphics) {
        // paint path
        float x = (float) node.Layout.X;
        float y = (float) node.Layout.Y;
        float w = (float) node.Layout.Width;
        float h = (float) node.Layout.Height;
        LogicGateType type = gateType;

        var outlinePen = new Pen(OutlineColor, 3f);
        if (type == LogicGateType.Not) {
          graphics.DrawLine(outlinePen, x, y + 0.5f*h, x + 0.1f*w, y + 0.5f*h);
        } else {
          // in port lines
          graphics.DrawLine(outlinePen, x, y + 5, x + 0.3f*w, y + 5);
          graphics.DrawLine(outlinePen, x, y + 25, x + 0.3f*w, y + 25);
        }

        var outline = outlinePath;
        outline.Fill(graphics, new Matrix2D(w, 0, 0, h, x, y), new SolidBrush(FillColor));
        outline.Draw(graphics, new Matrix2D(w, 0, 0, h, x, y), new Pen(new SolidBrush(OutlineColor), 2));

        if (type == LogicGateType.And || type == LogicGateType.Or) {
          graphics.DrawLine(outlinePen, x + 0.8f*w, y + 0.5f*h, x + w, y + 0.5f*h);
        } else {
          graphics.DrawLine(outlinePen, x + 0.9f*w, y + 0.5f*h, x + w, y + 0.5f*h);
        }
      }
    }

    protected override bool IsHit(IInputModeContext context, PointD location, INode node) {
      return node.Layout.ToRectD().GetEnlarged(context.HitTestRadius).Contains(location);
    }

    #endregion

    private GeneralPath GetNodeOutlinePath() {
      switch (GateType) {
        default:
        case LogicGateType.And:
          return AndOutlinePath;
        case LogicGateType.Nand:
          return NandOutlinePath;
        case LogicGateType.Nor:
          return NorOutlinePath;
        case LogicGateType.Not:
          return NotOutlinePath;
        case LogicGateType.Or:
          return OrOutlinePath;
      }
    }
  }
}
