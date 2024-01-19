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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{ 
    /// <summary>
    /// The <strong>IWindowsFormsEditorService</strong> that allows you to
    /// drop dialog and UI type editors for a <see cref="GenericValueEditor"/>.
    /// </summary>
    class GenericValueEditorService : IServiceProvider, IWindowsFormsEditorService
    {

        /// <summary>
        /// The control that uses this service.
        /// </summary>
        private GenericValueEditor editor;

        /// <summary>
        /// A control that holds the dropped editors.
        /// </summary>
        private GenericValueDropDownForm genericValueDropDownForm;

        /// <summary>
        /// Indicates whether we are currently closing the drop-down form.
        /// </summary>
        private bool closingDropDown;

        /// <summary>
        /// Creates the editor service.
        /// </summary>
        /// <param name="editor">The cell editor.</param>
        public GenericValueEditorService(GenericValueEditor editor)
        {
            this.editor = editor;
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type <paramref name="serviceType"/>.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IWindowsFormsEditorService))
                return this;
            return null;
        }

        /// <summary>
        /// Drops the editor control.
        /// </summary>
        /// <param name="ctl">The control to drop.</param>
        public void DropDownControl(Control ctl)
        {

            if (genericValueDropDownForm == null)
                genericValueDropDownForm = new GenericValueDropDownForm(this);

            genericValueDropDownForm.Visible = false;
            genericValueDropDownForm.Component = ctl;

            Rectangle editorBounds = editor.Bounds;

            Size size = genericValueDropDownForm.Size;

            // location of the form
            Point location
                = new Point(editorBounds.Right - size.Width,
                          editorBounds.Bottom + 1);
            // location in screen coordinate
            location = editor.Parent.PointToScreen(location);

            // check the form is in the screen working area
            Rectangle screenWorkingArea = Screen.FromControl(editor).WorkingArea;

            location.X = Math.Min(screenWorkingArea.Right - size.Width,
                                  Math.Max(screenWorkingArea.X, location.X));

            if (size.Height + location.Y + editor.TextBox.Height > screenWorkingArea.Bottom)
                location.Y = location.Y - size.Height - editorBounds.Height - 1;

            genericValueDropDownForm.SetBounds(location.X, location.Y, size.Width, size.Height);
            genericValueDropDownForm.Visible = true;
            ctl.Focus();

            editor.SelectTextBox();
            // wait for the end of the editing

            while (genericValueDropDownForm.Visible)
            {
                Application.DoEvents();
                MsgWaitForMultipleObjects(0, 0, true, 250, 255);
            }

            // editing is done or aborted

        }

        /// <summary>
        /// Hides the drop-down editor.
        /// </summary>
        public void HideForm()
        {
            if (genericValueDropDownForm != null && genericValueDropDownForm.Visible)
                genericValueDropDownForm.Visible = false;
        }

        /// <summary>
        /// Closes the dropped editor.
        /// </summary>
        public virtual void CloseDropDown()
        {
            if (closingDropDown)
                return;
            try
            {
                closingDropDown = true;
                if (genericValueDropDownForm != null && genericValueDropDownForm.Visible)
                {
                    genericValueDropDownForm.Component = null;
                    genericValueDropDownForm.Visible = false;

                    if (editor.TextBox.Visible)
                        editor.TextBox.Focus();
                }
            }
            finally
            {
                closingDropDown = false;
            }
        }

        /// <summary>
        /// Opens a dialog editor.
        /// </summary>
        /// <param name="dialog">The dialog to open.</param>
        public DialogResult ShowDialog(Form dialog)
        {
            dialog.ShowDialog(editor);
            return dialog.DialogResult;
        }

        /// <summary>
        /// Is Called when the SystemColorsChanged event is received
        /// by the GenericValueEditor.
        /// </summary>
        public void SystemColorsChanged()
        {
            if (genericValueDropDownForm != null)
                genericValueDropDownForm.SystemColorChanged();
        }

        [DllImport("user32.dll")]
        public static extern int MsgWaitForMultipleObjects(
            int nCount,		// number of handles in array
            int pHandles,	// object-handle array
            bool bWaitAll,	// wait option
            int dwMilliseconds,	// time-out interval
            int dwWakeMask	// input-event type
            );
    }    
}
