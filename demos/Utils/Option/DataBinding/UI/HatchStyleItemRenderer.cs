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

using System;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Demo.yFiles.Option.DataBinding.UI
{
  /// <summary>
  /// Renderer implemetation for <see cref="HatchBrush"/> types
  /// </summary>
  public class HatchStyleItemRenderer : IItemRenderer<HatchStyle>
  {
    private Size preferredIconSize = new Size(20, 17);

    #region IItemRenderer<DashStyle> Members

    ///<inheritdoc/>
    public virtual void PaintGridCellValue(HatchStyle style, Rectangle bounds, Graphics g, PaintValueEventArgs e) {
      HatchBrush myBrush = new HatchBrush(style, Color.Black, Color.White);      
      g.FillRectangle(myBrush, bounds);
    }

    ///<inheritdoc/>
    public virtual void PaintListCellValue(HatchStyle style, Rectangle bounds, Graphics g, DrawItemEventArgs e) {
        Pen myPen = new Pen(Color.Black, 1.0f);
      Rectangle icon = new Rectangle(bounds.Location.X+2, bounds.Location.Y+2, PreferredIconSize.Width, bounds.Height-4);
      
      HatchBrush myBrush = new HatchBrush(style, Color.Black, Color.White);
      g.FillRectangle(myBrush, icon);
      g.DrawRectangle(myPen, icon);
      Rectangle stringRect = new Rectangle(icon.Right + 6, icon.Top, e.Bounds.Right - icon.Right - 6, icon.Height);
        e.Graphics.DrawString(style.ToString(), e.Font, Brushes.Black, stringRect, StringFormat.GenericDefault);
    }

    ///<inheritdoc/>
    public Size PreferredIconSize {
      get { return preferredIconSize; }
      set { preferredIconSize = value; }
    }

    ///<inheritdoc/>
    public int PreferredDropDownWidth {
      get {
        int minWidth = 0;
        foreach (string s in Enum.GetNames(typeof (HatchStyle))) {
          minWidth = s.Length*12;
        }
        return minWidth + 3 + preferredIconSize.Width+12;
      }
    }

    #endregion
  }
}