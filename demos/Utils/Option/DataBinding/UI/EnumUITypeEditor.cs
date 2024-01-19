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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Demo.yFiles.Option.DataBinding.UI
{
  /// <summary>
  /// Custom <see cref="UITypeEditor"/> for enumeration values that allows to set a custom <see cref="IItemRenderer{E}"/>.
  /// </summary>
  /// <typeparam name="E">The type of the base enumeration</typeparam>
  public class EnumUITypeEditor<E> : UITypeEditor
  {
    private IItemRenderer<E> renderer;

    /// <summary>
    /// Get or set te preferred size of the drop down for <see cref="UITypeEditorEditStyle.DropDown"/>.
    /// </summary>
    public Size DropDownSize {
      get { return dropDownSize; }
      set { dropDownSize = value; }
    }

    private Size dropDownSize = new Size(100, 90);

    /// <summary>
    /// Get or set a custom <see cref="IItemRenderer{E}"/> for visual representation of the enum values.
    /// </summary>
    public IItemRenderer<E> Renderer {
      get { return renderer; }
      set { renderer = value; }
    }

    /// <summary>
    /// Gets the edit style for this instance.
    /// </summary>
    /// <param name="context">The current context.</param>
    /// <returns><see cref="UITypeEditorEditStyle.DropDown"/> if <see cref="Renderer"/> has been set,
    /// <see cref="UITypeEditorEditStyle.None"/> otherwise.</returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      if (Renderer != null) {
        return UITypeEditorEditStyle.DropDown;
      }
      return UITypeEditorEditStyle.None;
    }

    /// <summary>
    /// Returns whether the DropDown can be resized.
    /// </summary>
    /// <value>Always returns <see langword="false"/>.</value>
    public override bool IsDropDownResizable {
      get { return false; }
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
      if (provider != null) {
        IWindowsFormsEditorService editorService =
          provider.GetService(
            typeof (IWindowsFormsEditorService))
          as IWindowsFormsEditorService;
        if (editorService != null && Renderer != null) {
          EnumDropDownControl<E> selectionControl =
            new EnumDropDownControl<E>(
              editorService, Renderer, DropDownSize);

          editorService.DropDownControl(selectionControl);
          value = selectionControl.SelectedItem;
        }
      }
      return value;
    }

    /// <inheritdoc/>
    /// <remarks>Paints the grid cell value <see langword="true"/> iff <see cref="Renderer"/> is set.</remarks>
    public override void PaintValue(PaintValueEventArgs e) {
      if (Renderer != null) {
        Renderer.PaintGridCellValue((E) e.Value, e.Bounds, e.Graphics, e);
      }
    }

    /// <inheritdoc/>
    /// <returns><see langword="true"/> iff <see cref="Renderer"/> is set.</returns>
    public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
      return Renderer != null;
    }
  }
}