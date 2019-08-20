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

namespace Demo.yFiles.GraphEditor.UI
{
  partial class ApplyNodePropertiesDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplyNodePropertiesDialog));
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupNodesCheckBox = new System.Windows.Forms.CheckBox();
      this.normalNodesCheckBox = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.labelConfigurationCheckbox = new System.Windows.Forms.CheckBox();
      this.styleCheckBox = new System.Windows.Forms.CheckBox();
      this.geometryCheckBox = new System.Windows.Forms.CheckBox();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.flowLayoutPanel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.flowLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.groupBox1);
      this.flowLayoutPanel1.Controls.Add(this.groupBox2);
      resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.groupNodesCheckBox);
      this.groupBox1.Controls.Add(this.normalNodesCheckBox);
      resources.ApplyResources(this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      // 
      // groupNodesCheckBox
      // 
      resources.ApplyResources(this.groupNodesCheckBox, "groupNodesCheckBox");
      this.groupNodesCheckBox.Name = "groupNodesCheckBox";
      this.groupNodesCheckBox.UseVisualStyleBackColor = true;
      this.groupNodesCheckBox.CheckedChanged += new System.EventHandler(this.edgeLabelsCheckBox_CheckedChanged);
      // 
      // normalNodesCheckBox
      // 
      resources.ApplyResources(this.normalNodesCheckBox, "normalNodesCheckBox");
      this.normalNodesCheckBox.Name = "normalNodesCheckBox";
      this.normalNodesCheckBox.UseVisualStyleBackColor = true;
      this.normalNodesCheckBox.CheckedChanged += new System.EventHandler(this.nodeLabelsCheckBox_CheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.labelConfigurationCheckbox);
      this.groupBox2.Controls.Add(this.styleCheckBox);
      this.groupBox2.Controls.Add(this.geometryCheckBox);
      resources.ApplyResources(this.groupBox2, "groupBox2");
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      // 
      // labelConfigurationCheckbox
      // 
      resources.ApplyResources(this.labelConfigurationCheckbox, "labelConfigurationCheckbox");
      this.labelConfigurationCheckbox.Name = "labelConfigurationCheckbox";
      this.labelConfigurationCheckbox.UseVisualStyleBackColor = true;
      this.labelConfigurationCheckbox.CheckedChanged += new System.EventHandler(this.labelConfigurationCheckbox_CheckedChanged);
      // 
      // styleCheckBox
      // 
      resources.ApplyResources(this.styleCheckBox, "styleCheckBox");
      this.styleCheckBox.Name = "styleCheckBox";
      this.styleCheckBox.UseVisualStyleBackColor = true;
      this.styleCheckBox.CheckedChanged += new System.EventHandler(this.styleCheckBox_CheckedChanged);
      // 
      // geometryCheckBox
      // 
      resources.ApplyResources(this.geometryCheckBox, "geometryCheckBox");
      this.geometryCheckBox.Name = "geometryCheckBox";
      this.geometryCheckBox.UseVisualStyleBackColor = true;
      this.geometryCheckBox.CheckedChanged += new System.EventHandler(this.modelCheckBox_CheckedChanged);
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.Controls.Add(this.okButton);
      this.flowLayoutPanel2.Controls.Add(this.cancelButton);
      resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      // 
      // okButton
      // 
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      resources.ApplyResources(this.okButton, "okButton");
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      resources.ApplyResources(this.cancelButton, "cancelButton");
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // ApplyNodePropertiesDialog
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.flowLayoutPanel2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ApplyNodePropertiesDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.flowLayoutPanel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.flowLayoutPanel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox groupNodesCheckBox;
    private System.Windows.Forms.CheckBox normalNodesCheckBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox geometryCheckBox;
    private System.Windows.Forms.CheckBox styleCheckBox;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.CheckBox labelConfigurationCheckbox;

  }
}