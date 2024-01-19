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
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{
    /// <summary>
    /// The form that contains the dropped down editor.
    /// </summary>
    class GenericValueDropDownForm : Form
    {

        /// <summary>
        /// Currently dropped control.
        /// </summary>
        private Control currentControl;

        /// <summary>
        /// The service that dropped this form.
        /// </summary>
        private IWindowsFormsEditorService service;

        /// <summary>
        /// Creates a <strong>GenericValueDropDownForm</strong>.
        /// </summary>
        /// <param name="service">The service that drops this form.</param>
        public GenericValueDropDownForm(IWindowsFormsEditorService service)
        {
            StartPosition = FormStartPosition.Manual;
            currentControl = null;
            ShowInTaskbar = false;
            ControlBox = false;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = "";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Visible = false;
            this.service = service;
        }

        internal void SystemColorChanged()
        {
            OnSystemColorsChanged(EventArgs.Empty);
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnMouseDown">Control.OnMouseDown</see>.
        /// </summary>
        /// <remarks>
        /// Closes the form when the left button is clicked.
        /// </remarks>
        protected override void OnMouseDown(MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Left)
                service.CloseDropDown();
            base.OnMouseDown(me);
        }

        /// <summary>
        /// This member overrides <see cref="Form.OnClosed">Form.OnClosed</see>.
        /// </summary>
        protected override void OnClosed(EventArgs args)
        {
            if (Visible)
                service.CloseDropDown();
            base.OnClosed(args);
        }

        /// <summary>
        /// This member overrides <see cref="Form.OnDeactivate">Form.OnDeactivate</see>.
        /// </summary>
        protected override void OnDeactivate(EventArgs args)
        {
            if (Visible)
                service.CloseDropDown();
            base.OnDeactivate(args);
        }

        /// <summary>
        /// Gets or sets the control displayed by the form.
        /// </summary>
        /// <value>A <see cref="Control"/> instance.</value>
        public Control Component
        {
            get
            {
                return currentControl;
            }
            set
            {
                if (currentControl != null)
                {
                    Controls.Remove(currentControl);
                    currentControl = null;
                }
                if (value != null)
                {
                    currentControl = value;
                    Controls.Add(currentControl);
                    Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                    currentControl.Location = new Point(0, 0);
                    currentControl.Visible = true;
                    currentControl.Resize += new EventHandler(OnCurrentControlResize);
                }
                Enabled = currentControl != null;
            }
        }

        /// <summary>
        /// Invoked when the dropped control is resized.
        /// This resizes the form and realigns it.
        /// </summary>
        private void OnCurrentControlResize(object o, EventArgs e)
        {
            int width;
            if (currentControl != null)
            {
                width = Width;
                Size = new Size(2 + currentControl.Width, 2 + currentControl.Height);
                Left -= Width - width;
            }
        }

        /// <summary>
        /// Invoked when the form is resized.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (currentControl != null)
            {
                currentControl.SetBounds(0, 0, width - 2, height - 2);
                width = currentControl.Width;
                height = currentControl.Height;
                if (height == 0 && currentControl is ListBox)
                {
                    height = ((ListBox)currentControl).ItemHeight;
                    currentControl.Height = height;
                }
                width = width + 2;
                height = height + 2;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

    } 
}
