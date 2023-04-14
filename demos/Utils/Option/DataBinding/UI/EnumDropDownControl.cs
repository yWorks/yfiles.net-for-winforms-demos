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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Demo.yFiles.Option.DataBinding.UI
{
  internal partial class EnumDropDownControl<E> : UserControl
  {
    public IItemRenderer<E> Renderer {
      get { return renderer; }
    }

    private IItemRenderer<E> renderer;


    public E SelectedItem {
      get { return values[listBox1.SelectedIndex]; }
    }

    private readonly IWindowsFormsEditorService editorService;
    private readonly E[] values;

    public EnumDropDownControl(IWindowsFormsEditorService editorService, IItemRenderer<E> renderer, Size dropDownSize) {
      this.editorService = editorService;
      this.renderer = renderer;
      InitializeComponent();
      values =  InitializeValues();
      listBox1.DataSource = values;
      // Set the DrawMode property to draw fixed sized items.
     if (Renderer != null) {
       listBox1.DrawMode = DrawMode.OwnerDrawFixed;      
       listBox1.ItemHeight = Renderer.PreferredIconSize.Height;
       this.Width = Renderer.PreferredDropDownWidth;
//       listBox1.Size = dropDownSize;
       listBox1.Dock = DockStyle.Fill;
      }
    }

    protected virtual E[] InitializeValues() {
      return (E[]) Enum.GetValues(typeof (E));
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing) {
        this.listBox1.MouseClick -= listBox1_MouseClick;
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    private void listBox1_MouseClick(object sender, MouseEventArgs e) {
      this.Invalidate(false);
      this.editorService.CloseDropDown();
    }

    private void listBox1_DrawItem(object sender, DrawItemEventArgs e) {
      if(Renderer != null) {
        // Draw the background of the ListBox control for each item.
        e.DrawBackground();
        renderer.PaintListCellValue(values[e.Index], e.Bounds, e.Graphics, e);
        e.DrawFocusRectangle();
      }
    }
  }
}
