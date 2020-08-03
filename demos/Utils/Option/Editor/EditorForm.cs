/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Windows.Forms;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Convenience class that wraps an <see cref="EditorControl"/> instance and adds some buttons.
  /// </summary>
  public class EditorForm : Form
  {

    private System.Windows.Forms.Button resetButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button adoptButton;
    private System.Windows.Forms.Button applyButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.FlowLayoutPanel buttonLayoutPanel;
    private System.Windows.Forms.Panel editorLayoutPanel;
    private EditorControl editorControl;

    //we can be only be created by factory methods, not directly
    internal EditorForm(EditorControl editorControl) {
      this.editorControl = editorControl;
      InitializeComponent();
      this.Text = editorControl.Title;
    }

    internal EditorForm(EditorControl editorControl, bool resettable) {
      this.editorControl = editorControl;
      this.showResetButton = resettable;
      InitializeComponent();
      this.Text = editorControl.Title;
    }

    /// <summary>
    /// Get the <see cref="EditorControl"/> that is wrapped by this form.
    /// </summary>
    public virtual EditorControl EditorControl {
      get { return editorControl; }
      //entry point for factory methods
      internal set { editorControl = value; }
    }

    /// <summary>
    /// Return whether the Editor is in auto commit state
    /// </summary>
    public bool IsAutoCommit {
      get { return EditorControl.IsAutoCommit; }
      internal set { EditorControl.IsAutoCommit = value; }
    }    
    
    /// <summary>
    /// Return whether the Editor should show a reset button.
    /// </summary>
    /// <remarks>Default is <see langword="true"/></remarks>
    public bool ShowResetButton {
      get { return showResetButton; }
      set { showResetButton = value; }
    }

    /// <summary>
    /// Return whether the Editor is in auto adopt state
    /// </summary>
    public bool IsAutoAdopt {
      get { return EditorControl.IsAutoAdopt; }
      internal set { EditorControl.IsAutoAdopt = value; }
    }

    #region private helper methods

    private void CreateButtons() {
      this.buttonLayoutPanel.SuspendLayout();
      this.buttonLayoutPanel.WrapContents = false;
      this.buttonLayoutPanel.FlowDirection = FlowDirection.RightToLeft;
      // 
      // flowLayoutPanel1
      // 
//      this.buttonLayoutPanel.Location =
//        new Point(0, 
//        EditorControl.ClientRectangle.Bottom +
////        EditorControl.Margin.Vertical + 
//        buttonLayoutPanel.Margin.Top);


      // 
      // applyButton, only when view has autocommit disabled
      // 
      if (!IsAutoCommit) {
        this.applyButton = new System.Windows.Forms.Button();
        this.applyButton.Name = "applyButton";
        this.applyButton.Size = new System.Drawing.Size(75, 23);
        this.applyButton.AutoSize = true;
        this.applyButton.TabIndex = 6;
//        this.applyButton.Margin = new Padding(0);
        this.applyButton.Text = EditorControl.I18NFactory.GetString(null, "OptionHandlerI18N_APPLYBUTTON_TEXT");
        this.applyButton.UseVisualStyleBackColor = true;
        this.applyButton.Click += new System.EventHandler(this.applyButton_Click);        
        this.buttonLayoutPanel.Controls.Add(this.applyButton);
      }

      // 
      // adoptButton, only when view has autoadopt disabled
      // 
      if (!IsAutoAdopt) {
        this.adoptButton = new System.Windows.Forms.Button();
        this.adoptButton.Name = "adoptButton";
        this.adoptButton.Size = new System.Drawing.Size(75, 23);
        this.adoptButton.TabIndex = 7;
        this.adoptButton.AutoSize = true;
        this.adoptButton.Text = EditorControl.I18NFactory.GetString(null, "OptionHandlerI18N_ADOPTBUTTON_TEXT");
        this.adoptButton.UseVisualStyleBackColor = true;
        this.adoptButton.Click += new System.EventHandler(this.adoptButton_Click);
        this.buttonLayoutPanel.Controls.Add(this.adoptButton);
      }

      // 
      // cancelButton, always exists
      // 

      this.cancelButton = new System.Windows.Forms.Button();
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 8;
      this.cancelButton.AutoSize = true;
//      this.cancelButton.Margin = new Padding(0);
      this.cancelButton.Text = EditorControl.I18NFactory.GetString(null, "OptionHandlerI18N_CANCELBUTTON_TEXT");
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.buttonLayoutPanel.Controls.Add(this.cancelButton);

      // 
      // resetButton, always exists
      // 
      if (ShowResetButton) {
        this.resetButton = new System.Windows.Forms.Button();
        this.resetButton.Name = "resetButton";
        this.resetButton.Size = new System.Drawing.Size(75, 23);
        this.resetButton.TabIndex = 9;
        this.resetButton.AutoSize = true;
//        this.resetButton.Margin = new Padding(0);
        this.resetButton.Text = EditorControl.I18NFactory.GetString(null, "OptionHandlerI18N_RESETBUTTON_TEXT");
        this.resetButton.UseVisualStyleBackColor = true;
        this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
        this.buttonLayoutPanel.Controls.Add(this.resetButton);

        this.buttonLayoutPanel.ResumeLayout(false);
      }

      // 
      // okButton, always exists...
      // 
      this.okButton = new System.Windows.Forms.Button();
      this.okButton.Name = "okButton";
      //todo: make size flexible
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 5;
//      this.okButton.Margin = new Padding(0);
      this.okButton.AutoSize = false;
      this.okButton.Text = EditorControl.I18NFactory.GetString(null, "OptionHandlerI18N_OKBUTTON_TEXT");
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      this.okButton.DialogResult = DialogResult.OK;

      this.AcceptButton = okButton;
      this.CancelButton = cancelButton;
      this.buttonLayoutPanel.Controls.Add(this.okButton);
      buttonLayoutPanel.PerformLayout();
    }

    #endregion

    #region event listeners

    private void okButton_Click(object sender, EventArgs e) {
      OnValuesCommited(sender, new EventArgs());
      this.Dispose();
    }

    private void applyButton_Click(object sender, EventArgs e) {
      OnValuesCommited(sender, new EventArgs());
    }

    private void adoptButton_Click(object sender, EventArgs e) {
      OnValuesAdopted(sender, new EventArgs());
    }

    /// <summary>
    /// This method is called when values have been adopted in the form.
    /// </summary>
    /// <param name="form"></param>
    /// <param name="args"></param>
    protected virtual void OnValuesAdopted(object form, EventArgs args) {
      //delegate operation to active view
      EditorControl.AdoptValues();
      if(ValuesAdopted != null) {
        ValuesAdopted(form, args);
      }
    }

    /// <summary>
    /// This method is called when values have been commited by the form.
    /// </summary>
    /// <param name="form"></param>
    /// <param name="args"></param>
    protected virtual void OnValuesCommited(object form, EventArgs args) {
      //delegate operation to active view
      EditorControl.CommitValues();
      if (ValuesCommited != null) {
        ValuesCommited(form, args);
      }
    }

    private void cancelButton_Click(object sender, EventArgs e) {
      //don't commit
      this.Dispose();
    }

    private void resetButton_Click(object sender, EventArgs e) {
      EditorControl.ResetValues();
//      MessageBox.Show("Help not implemented", "Help not implemented", MessageBoxButtons.OK,
//                      MessageBoxIcon.Exclamation);
    }

    #endregion

    /// <summary>
    /// This events is fired when values have been adopted by the form.
    /// </summary>
    public event EventHandler ValuesAdopted;

    /// <summary>
    /// This events is fired when values have been commited by the form.
    /// </summary>
    public event EventHandler ValuesCommited;

    private void InitializeComponent() {
      this.buttonLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.editorLayoutPanel = new System.Windows.Forms.Panel();
      this.buttonLayoutPanel.SuspendLayout();
      this.editorLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      this.Padding = new Padding(6);
      CreateButtons();
      this.buttonLayoutPanel.AutoSize = false;
      this.editorControl.AutoSize = false;
      this.editorControl.Name = "editorControl1";
      this.editorControl.TabIndex = 0;
      Padding buttonMargin = buttonLayoutPanel.Controls[0].Margin;
      buttonLayoutPanel.Controls[buttonLayoutPanel.Controls.Count - 1].Margin = new Padding(0, buttonMargin.Top, buttonMargin.Right, buttonMargin.Bottom);
      buttonLayoutPanel.Controls[0].Margin = new Padding(buttonMargin.Left, buttonMargin.Top, 0, buttonMargin.Bottom);

      this.editorControl.Padding = new Padding(0);
      this.editorControl.Location = new System.Drawing.Point(editorControl.Margin.Left+this.Padding.Left,
        this.Padding.Top + editorControl.Margin.Top);

      // 
      // flowLayoutPanel1
      // 
      this.buttonLayoutPanel.AutoSize = true;
//      this.editorControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.buttonLayoutPanel.Padding = new Padding(0);
      this.buttonLayoutPanel.Margin = new Padding(0);
      this.buttonLayoutPanel.Size = new System.Drawing.Size(buttonLayoutPanel.PreferredSize.Width,
        buttonLayoutPanel.PreferredSize.Height);

      
      this.buttonLayoutPanel.TabIndex = 2;
      this.buttonLayoutPanel.Name = "flowLayoutPanel1";
      
      int preferredWidth =
        Math.Max(buttonLayoutPanel.PreferredSize.Width,
                 editorControl.PreferredSize.Width);

      this.editorControl.Size = new System.Drawing.Size(preferredWidth,
        editorControl.PreferredSize.Height);
      this.buttonLayoutPanel.Location = new Point(editorControl.Right-buttonLayoutPanel.Width,
                                                  this.Padding.Bottom+editorControl.Bottom+editorControl.Margin.Bottom+buttonLayoutPanel.Margin.Top);

      preferredWidth = Math.Max(buttonLayoutPanel.Width + buttonLayoutPanel.Margin.Horizontal,
                                editorControl.Width + editorControl.Margin.Horizontal)+this.Padding.Horizontal;
      int preferredHeight = buttonLayoutPanel.Bottom + buttonLayoutPanel.Margin.Bottom+this.Padding.Bottom;

      this.ClientSize = new Size(preferredWidth, preferredHeight);
      this.MinimumSize = this.Size;
      this.editorControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
      this.buttonLayoutPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.editorControl);
      this.Controls.Add(this.buttonLayoutPanel);
      //todo: use real name
      this.Name = "Form1";
      this.Text = "Form1";
      this.buttonLayoutPanel.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }


    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private bool showResetButton = true;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
//            Console.WriteLine("Disposing editor form");
      if (disposing && (components != null)) {
        components.Dispose();
      }
      this.editorControl = null;
      base.Dispose(disposing);
    }
  }
}