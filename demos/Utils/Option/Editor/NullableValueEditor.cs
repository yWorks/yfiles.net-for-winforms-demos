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
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Support class for nullable value handlers
  /// </summary>
  internal class NullableUITypeEditor : UITypeEditor, IServiceProvider, IWindowsFormsEditorService
  {
    private UITypeEditor delegateEditor;
    private IServiceProvider coreProvider;
    private IWindowsFormsEditorService coreService;
    private I18NFactory i18NFactory = OptionHandler.FallBackI18NFactory;
    
    private bool NULL_VALUE_SELECTED;
    DropDownEditorControl ddc;

    #region UITypeEditor members

    public NullableUITypeEditor(UITypeEditor delegateEditor) {
      this.delegateEditor = delegateEditor;
      }

    /// <summary>
    /// This is the only method that does more than just calling the delegate functions
    /// </summary>
    /// <param name="context"></param>
    /// <param name="provider"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider,
                                     object value) {
      object retval;
      NULL_VALUE_SELECTED = false;
      coreProvider = provider;

      //tricky: Avoid passing VALUE_UNDEFINED to the delegateEditor (can lead to exception)
      // => map to null, if null is returned, we know that we canceled the editor
      // if OTOH, NULL_VALUE_SELECTED is set, we know that the button has been clicked.
      //this is really evil, but since we cannot override delegateEditor's EditValue
      //in a generic way, we use this work  around
      //todo: find a better solution!
      //todo: control this via editor attribute
      if (value == OptionItem.VALUE_UNDEFINED) {
        retval = delegateEditor.EditValue(context, this, null);
        if (NULL_VALUE_SELECTED) {
          retval = null;
        } else if (retval == null) {
          //editing has been canceled
          retval = OptionItem.VALUE_UNDEFINED;
        }
      } else {
        retval = delegateEditor.EditValue(context, this, value);
        if (NULL_VALUE_SELECTED) {
          retval = null;
        }
      }
      return retval;
                                     }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      if (context == null) {
        return delegateEditor.GetEditStyle(context);
      } else {
        return
          context.PropertyDescriptor.IsReadOnly
            ? UITypeEditorEditStyle.None
            : delegateEditor.GetEditStyle(context);
      }
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
      if (context == null) {
        return delegateEditor.GetPaintValueSupported(context);
      }

      PropertyModelView.OptionItemPropertyDescriptor desc =
        context.PropertyDescriptor as PropertyModelView.OptionItemPropertyDescriptor;
      if (desc != null) {
        object value = desc.GetValue(null);
        if (value == OptionItem.VALUE_UNDEFINED) {
          return true;
        }
      }
      return delegateEditor.GetPaintValueSupported(context);
    }

    public override bool IsDropDownResizable {
      get { return delegateEditor.IsDropDownResizable; }
    }

    public override void PaintValue(PaintValueEventArgs e) {
      object value = e.Value;
      if (value == OptionItem.VALUE_UNDEFINED) {
        Graphics g = e.Graphics;
        Font f = new Font("sansserif", e.Bounds.Height*0.7f, FontStyle.Bold,
                          GraphicsUnit.Pixel);
        g.DrawString("?", f, Brushes.Black, e.Bounds);
        base.PaintValue(e);
        return;
      }

      delegateEditor.PaintValue(e);
    }

    #endregion

    #region IServiceProvider Members

    public object GetService(Type serviceType) {
      object retval = coreProvider.GetService(serviceType);
      if (retval is IWindowsFormsEditorService) {
        coreService = (IWindowsFormsEditorService) retval;
        return this;
      }
      return retval;
    }

    #endregion

    #region IWindowsFormsEditorService Members

    public void CloseDropDown() {
      coreService.CloseDropDown();
      ddc.Dispose();
    }

    public void DropDownControl(Control control) {
      ddc = new DropDownEditorControl(this, control);
//      ddc.Dock = DockStyle.Fill;
      coreService.DropDownControl(ddc);
    }

    public DialogResult ShowDialog(Form dialog) {
      return coreService.ShowDialog(dialog);
    }

    #endregion

    #region drop dow control inner class

    /// <summary>
    /// The control that contains the dropped down editor.
    /// </summary>
    private class DropDownEditorControl : Control
    {
      #region private instance variables

      private Control currentControl;
      private FlowLayoutPanel basePane;
      private Button nullValueButton;
      private NullableUITypeEditor _enclosingInstance;

      #endregion

      #region constructors

      /// <summary>
      /// Creates a <strong>DropDownEditorControl</strong>.
      /// </summary>
      public DropDownEditorControl(NullableUITypeEditor enclosingInstance, Control control) {
        currentControl = null;
        _enclosingInstance = enclosingInstance;
        Visible = false;

        basePane = new FlowLayoutPanel();
        basePane.BackColor = control.BackColor;
        Controls.Add(basePane);
        nullValueButton = new Button();
        //todo: make this configurable
        nullValueButton.Text = enclosingInstance.i18NFactory.GetString(null, "OptionHandlerI18N_NO_VALUE");
        basePane.Controls.Add(nullValueButton);

        nullValueButton.Click += nullValueButton_Click;
        this.Component = control;
        nullValueButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        basePane.Dock = DockStyle.Fill;        
      }

      #endregion

      #region event handlers

      private void nullValueButton_Click(object sender, EventArgs e) {
        _enclosingInstance.NULL_VALUE_SELECTED = true;
        _enclosingInstance.CloseDropDown();
      }


      /// <summary>
      /// This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
      /// </summary>
      /// <remarks>
      /// Closes the form when the left button is clicked.
      /// </remarks>
      protected override void OnMouseDown(MouseEventArgs me) {
        _enclosingInstance.CloseDropDown();
        base.OnMouseDown(me);
      }

      /// <summary>
      /// Gets or sets the control displayed by the form.
      /// </summary>
      /// <value>A <see cref="Control"/> instance.</value>
      public Control Component {
        set {
          if (currentControl != null) {
            basePane.Controls.Remove(currentControl);
            currentControl = null;
          }
          if (value != null) {
            currentControl = value;
            basePane.Controls.Add(currentControl);
            nullValueButton.Width = currentControl.Width;
            Size = new Size(2 + currentControl.Width, 8 + currentControl.Height + nullValueButton.Height);
            basePane.Width = Width;
            basePane.Height = Height;
            currentControl.Location = new Point(2, nullValueButton.Height);
            currentControl.Visible = true;
          }
          Enabled = currentControl != null;
        }
      }


      protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
        base.SetBoundsCore(x, y, width, height, specified);
        this.Size = new Size(width, height);
        this.nullValueButton.Width = width-6;
        this.currentControl.Width = width-6;
        this.currentControl.Height = height - 8 - nullValueButton.Height - 4 ;
      }

      protected override void Dispose(bool disposing) {
        if (disposing) {
          // Be sure to unhook event handlers
          // to prevent "lapsed listener" leaks.
          nullValueButton.Click -= nullValueButton_Click;
          _enclosingInstance = null;
          nullValueButton.Dispose();
          basePane.Controls.Clear();
          basePane.Dispose();
        }
      }

      #endregion
    }

    #endregion
  }

  internal class UndefinedValueUITypeEditor : UITypeEditor
  {
    private UITypeEditor delegateEditor;


    public UndefinedValueUITypeEditor():this(null) {}

    public UndefinedValueUITypeEditor(UITypeEditor _delegate) {
      this.delegateEditor = _delegate;
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
      //todo: fix this for dialog editor
//            if (context == null)
//            {
//                return true;
//            }
      if(delegateEditor != null) {
        return delegateEditor.GetPaintValueSupported(context);
      }

      PropertyModelView.OptionItemPropertyDescriptor desc =
        context.PropertyDescriptor as PropertyModelView.OptionItemPropertyDescriptor;
      if (desc != null) {
        object value = desc.GetValue(null);
        if (value == OptionItem.VALUE_UNDEFINED) {
          return true;
        }
      }
      return false;
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      if(delegateEditor == null) {
        return UITypeEditorEditStyle.None;
      }
      if (context == null) {
        return delegateEditor.GetEditStyle(context);
      } else {
        return
          context.PropertyDescriptor.IsReadOnly
            ? UITypeEditorEditStyle.None
            : delegateEditor.GetEditStyle(context);
      }
    }

    public override bool IsDropDownResizable {
      get { return delegateEditor == null?false:delegateEditor.IsDropDownResizable; }
    }

    public override void PaintValue(PaintValueEventArgs e) {
      object value = e.Value;
      if (value == OptionItem.VALUE_UNDEFINED) {
        Graphics g = e.Graphics;
        Font f = new Font("sansserif", e.Bounds.Height*0.7f, FontStyle.Bold,
                          GraphicsUnit.Pixel);
        g.DrawString("?", f, Brushes.Black, e.Bounds);
        return;
      }
      else if(delegateEditor != null) {
        delegateEditor.PaintValue(e);
        return;
      }
    }

    /// <summary>
    /// This is the only method that does more than just calling the delegate functions
    /// </summary>
    /// <param name="context"></param>
    /// <param name="provider"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider,
                                     object value) {
      if(delegateEditor == null) {
        return value;
      }
      object retval;

      //tricky: Avoid passing VALUE_UNDEFINED to the delegateEditor (can lead to exception)
      // => map to null, if null is returned, we know that we canceled the editor
      // if OTOH, NULL_VALUE_SELECTED is set, we know that the button has been clicked.
      //this is really evil, but since we cannot override delegateEditor's EditValue
      //in a generic way, we use this work  around
      //todo: find a better solution!
      //todo: control this via editor attribute
      if (value == OptionItem.VALUE_UNDEFINED) {
        retval = delegateEditor.EditValue(context, provider, null);
        if (retval == null) {
          //editing has been canceled
          retval = OptionItem.VALUE_UNDEFINED;
        }
      } else {
        retval = delegateEditor.EditValue(context, provider, value);        
      }
      return retval;
    }
  }
}