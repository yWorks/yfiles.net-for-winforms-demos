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

using System;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Demo.yFiles.Option.DataBinding.UI
{
  /// <summary>
  /// Renderer implemetation for <see cref="DashStyle"/> types
  /// </summary>
  public class DashStyleItemRenderer : IItemRenderer<DashStyle>
  {
    private Size preferredIconSize = new Size(20, 15);

    #region IItemRenderer<DashStyle> Members

    ///<inheritdoc/>
    public virtual void PaintGridCellValue(DashStyle style, Rectangle bounds, Graphics g, PaintValueEventArgs e) {
      if (style != DashStyle.Custom) {
        Pen myPen = new Pen(Color.Black, 2.0f);
        myPen.DashStyle = style;
        g.DrawLine(myPen, bounds.Left, bounds.Top + bounds.Height/2, bounds.Right, bounds.Top + bounds.Height/2);
      }
    }

    ///<inheritdoc/>
    public virtual void PaintListCellValue(DashStyle style, Rectangle bounds, Graphics g, DrawItemEventArgs e) {
      if (style != System.Drawing.Drawing2D.DashStyle.Custom) {
        Pen myPen = new Pen(Color.Black, 2.0f);
        myPen.DashStyle = style;
        e.Graphics.DrawLine(myPen, bounds.Left + 3, bounds.Top + bounds.Height/2, bounds.Right - 3,
                            bounds.Top + bounds.Height/2);
      } else {
        e.Graphics.DrawString(style.ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
      }
    }

    ///<inheritdoc/>
    public Size PreferredIconSize {
      get { return preferredIconSize; }
      set { preferredIconSize = value; }
    }

    ///<inheritdoc/>
    public int PreferredDropDownWidth {
      get { return 100; }
    }

    #endregion
  }
}