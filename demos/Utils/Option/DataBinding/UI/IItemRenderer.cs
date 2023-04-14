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
using System.Drawing.Design;
using System.Windows.Forms;

namespace Demo.yFiles.Option.DataBinding.UI
{
  /// <summary>
  /// Common interface for classes that can render value representations in list cells as well as in property grid cells.
  /// </summary>
  /// <typeparam name="E">The type of the item to render</typeparam>
  public interface IItemRenderer<E>
  {
    /// <summary>
    /// Render a value representation of <paramref name="value"/> in a property grid cell
    /// </summary>
    /// <param name="value">The value to render</param>
    /// <param name="bounds">The bounds of the painting area</param>
    /// <param name="g">The graphics context to paint on</param>
    /// <param name="e">Additional event args needed to work correctly in a grid cell context</param>
    void PaintGridCellValue(E value, Rectangle bounds, Graphics g, PaintValueEventArgs e);

    /// <summary>
    /// Render a value representation of <paramref name="value"/> in a list cell
    /// </summary>
    /// <param name="value">The value to render</param>
    /// <param name="bounds">The bounds of the painting area</param>
    /// <param name="g">The graphics context to paint on</param>
    /// <param name="e">Additional event args needed to work correctly in a list cell context</param>
    void PaintListCellValue(E value, Rectangle bounds, Graphics g, DrawItemEventArgs e);

    /// <summary>
    /// Gets or sets the preferred size of the icon represetation
    /// </summary>
    Size PreferredIconSize { get; set; }

    /// <summary>
    /// Gets the preferred size of the drop down for combo list boxes
    /// </summary>
    int PreferredDropDownWidth { get; }
  }
}