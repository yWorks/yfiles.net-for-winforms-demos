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


namespace Neo4JIntegration
{
  partial class ShortestPathDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortestPathDialog));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.toComboBox = new System.Windows.Forms.ComboBox();
      this.showPathButton = new System.Windows.Forms.Button();
      this.fromControl = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.fromControl)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 51);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(30, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "From";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 145);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(20, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "To";
      // 
      // toComboBox
      // 
      this.toComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.toComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.toComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.toComboBox.FormattingEnabled = true;
      this.toComboBox.ItemHeight = 80;
      this.toComboBox.Location = new System.Drawing.Point(49, 108);
      this.toComboBox.Name = "toComboBox";
      this.toComboBox.Size = new System.Drawing.Size(270, 86);
      this.toComboBox.TabIndex = 3;
      this.toComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawComboBoxItem);
      // 
      // showPathButton
      // 
      this.showPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.showPathButton.Location = new System.Drawing.Point(203, 200);
      this.showPathButton.Name = "showPathButton";
      this.showPathButton.Size = new System.Drawing.Size(116, 23);
      this.showPathButton.TabIndex = 4;
      this.showPathButton.Text = "Show Shortest Path";
      this.showPathButton.UseVisualStyleBackColor = true;
      this.showPathButton.Click += new System.EventHandler(this.ShowPath);
      // 
      // fromControl
      // 
      this.fromControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.fromControl.Location = new System.Drawing.Point(49, 12);
      this.fromControl.Name = "fromControl";
      this.fromControl.Size = new System.Drawing.Size(270, 90);
      this.fromControl.TabIndex = 5;
      this.fromControl.TabStop = false;
      // 
      // ShortestPathDialog
      // 
      this.AcceptButton = this.showPathButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(332, 235);
      this.Controls.Add(this.fromControl);
      this.Controls.Add(this.showPathButton);
      this.Controls.Add(this.toComboBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(1200, 274);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(348, 274);
      this.Name = "ShortestPathDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Find Shortest Path";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShortestPathDialog_FormClosed);
      ((System.ComponentModel.ISupportInitialize)(this.fromControl)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox toComboBox;
    private System.Windows.Forms.Button showPathButton;
    private System.Windows.Forms.PictureBox fromControl;
  }
}
