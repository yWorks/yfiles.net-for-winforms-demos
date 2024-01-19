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

namespace Demo.yFiles.GraphEditor.UI
{
  partial class ApplyEdgePropertiesDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplyEdgePropertiesDialog));
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.labelCheckBox = new System.Windows.Forms.CheckBox();
      this.styleCheckBox = new System.Windows.Forms.CheckBox();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.flowLayoutPanel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.flowLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AccessibleDescription = null;
      this.flowLayoutPanel1.AccessibleName = null;
      resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.BackgroundImage = null;
      this.flowLayoutPanel1.Controls.Add(this.groupBox2);
      this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
      this.flowLayoutPanel1.Font = null;
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      // 
      // groupBox2
      // 
      this.groupBox2.AccessibleDescription = null;
      this.groupBox2.AccessibleName = null;
      resources.ApplyResources(this.groupBox2, "groupBox2");
      this.groupBox2.BackgroundImage = null;
      this.groupBox2.Controls.Add(this.labelCheckBox);
      this.groupBox2.Controls.Add(this.styleCheckBox);
      this.groupBox2.Font = null;
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      // 
      // modelCheckBox
      // 
      this.labelCheckBox.AccessibleDescription = null;
      this.labelCheckBox.AccessibleName = null;
      resources.ApplyResources(this.labelCheckBox, "modelCheckBox");
      this.labelCheckBox.BackgroundImage = null;
      this.labelCheckBox.Font = null;
      this.labelCheckBox.Name = "labelCheckBox";
      this.labelCheckBox.UseVisualStyleBackColor = true;
      this.labelCheckBox.CheckedChanged += new System.EventHandler(this.modelCheckBox_CheckedChanged);
      // 
      // styleCheckBox
      // 
      this.styleCheckBox.AccessibleDescription = null;
      this.styleCheckBox.AccessibleName = null;
      resources.ApplyResources(this.styleCheckBox, "styleCheckBox");
      this.styleCheckBox.BackgroundImage = null;
      this.styleCheckBox.Font = null;
      this.styleCheckBox.Name = "styleCheckBox";
      this.styleCheckBox.UseVisualStyleBackColor = true;
      this.styleCheckBox.CheckedChanged += new System.EventHandler(this.styleCheckBox_CheckedChanged);
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.AccessibleDescription = null;
      this.flowLayoutPanel2.AccessibleName = null;
      resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
      this.flowLayoutPanel2.BackgroundImage = null;
      this.flowLayoutPanel2.Controls.Add(this.okButton);
      this.flowLayoutPanel2.Controls.Add(this.cancelButton);
      this.flowLayoutPanel2.Font = null;
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      // 
      // okButton
      // 
      this.okButton.AccessibleDescription = null;
      this.okButton.AccessibleName = null;
      resources.ApplyResources(this.okButton, "okButton");
      this.okButton.BackgroundImage = null;
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.Font = null;
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.AccessibleDescription = null;
      this.cancelButton.AccessibleName = null;
      resources.ApplyResources(this.cancelButton, "cancelButton");
      this.cancelButton.BackgroundImage = null;
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Font = null;
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // ApplyEdgePropertiesDialog
      // 
      this.AcceptButton = this.okButton;
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.CancelButton = this.cancelButton;
      this.Controls.Add(this.flowLayoutPanel1);
      this.Font = null;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = null;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ApplyEdgePropertiesDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.flowLayoutPanel2.ResumeLayout(false);
      this.flowLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox labelCheckBox;
    private System.Windows.Forms.CheckBox styleCheckBox;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;

  }
}