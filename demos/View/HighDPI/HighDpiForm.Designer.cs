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

using yWorks.Geometry;

namespace Demo.yFiles.HighDpi
{
  partial class HighDpiForm
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
      System.Windows.Forms.GroupBox groupBox1;
      System.Windows.Forms.Label label1;
      this.graphControl = new yWorks.Controls.GraphControl();
      this.panel1 = new System.Windows.Forms.Panel();
      this.description = new System.Windows.Forms.RichTextBox();
      this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
      this.panel2 = new System.Windows.Forms.Panel();
      this.scaleLabel1 = new System.Windows.Forms.Label();
      this.scaleFactor = new System.Windows.Forms.ComboBox();
      this.scaleLabel2 = new System.Windows.Forms.Label();
      groupBox1 = new System.Windows.Forms.GroupBox();
      label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
      this.graphControl.Size = new System.Drawing.Size(844, 568);
      this.graphControl.TabIndex = 1;
      this.graphControl.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.description);
      this.panel1.Controls.Add(this.graphOverviewControl);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(269, 568);
      this.panel1.TabIndex = 3;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 330);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(265, 234);
      this.description.TabIndex = 2;
      this.description.Text = "";
      // 
      // graphOverviewControl
      // 
      this.graphOverviewControl.AnimateScrollCommands = false;
      this.graphOverviewControl.AutoDrag = false;
      this.graphOverviewControl.BackColor = System.Drawing.Color.White;
      this.graphOverviewControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphOverviewControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.graphOverviewControl.GraphControl = null;
      this.graphOverviewControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      this.graphOverviewControl.Location = new System.Drawing.Point(0, 125);
      this.graphOverviewControl.MouseWheelBehavior = yWorks.Controls.MouseWheelBehaviors.None;
      this.graphOverviewControl.Name = "graphOverviewControl";
      this.graphOverviewControl.Size = new System.Drawing.Size(265, 205);
      this.graphOverviewControl.TabIndex = 1;
      this.graphOverviewControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      // 
      // panel2
      // 
      this.panel2.AutoSize = true;
      this.panel2.Controls.Add(groupBox1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 4, 4);
      this.panel2.Size = new System.Drawing.Size(265, 125);
      this.panel2.TabIndex = 3;
      // 
      // groupBox1
      // 
      groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      groupBox1.Controls.Add(this.scaleLabel2);
      groupBox1.Controls.Add(this.scaleLabel1);
      groupBox1.Controls.Add(this.scaleFactor);
      groupBox1.Controls.Add(label1);
      groupBox1.Location = new System.Drawing.Point(10, 10);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(248, 108);
      groupBox1.TabIndex = 3;
      groupBox1.TabStop = false;
      groupBox1.Text = "Scaling settings";
      // 
      // scaleLabel1
      // 
      this.scaleLabel1.AutoSize = true;
      this.scaleLabel1.Location = new System.Drawing.Point(6, 18);
      this.scaleLabel1.Name = "scaleLabel1";
      this.scaleLabel1.Size = new System.Drawing.Size(81, 13);
      this.scaleLabel1.TabIndex = 0;
      this.scaleLabel1.Text = "Windows Scale";
      // 
      // scaleFactor
      // 
      this.scaleFactor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.scaleFactor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.scaleFactor.FormattingEnabled = true;
      this.scaleFactor.Items.AddRange(new object[] {
            "(Automatic)",
            "1.0",
            "1.25",
            "1.5",
            "1.75",
            "2",
            "2.5",
            "3",
            "4"});
      this.scaleFactor.Location = new System.Drawing.Point(9, 79);
      this.scaleFactor.Name = "scaleFactor";
      this.scaleFactor.Size = new System.Drawing.Size(233, 21);
      this.scaleFactor.TabIndex = 2;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new System.Drawing.Point(6, 63);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(111, 13);
      label1.TabIndex = 1;
      label1.Text = "Custom scaling factor:";
      // 
      // scaleLabel2
      // 
      this.scaleLabel2.AutoSize = true;
      this.scaleLabel2.Location = new System.Drawing.Point(6, 33);
      this.scaleLabel2.Name = "scaleLabel2";
      this.scaleLabel2.Size = new System.Drawing.Size(70, 13);
      this.scaleLabel2.TabIndex = 3;
      this.scaleLabel2.Text = "Control Scale";
      // 
      // HighDpiForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(844, 568);
      this.Controls.Add(this.graphControl);
      this.Controls.Add(this.panel1);
      this.Icon = global::Demo.yFiles.HighDpi.Properties.Resources.yIcon;
      this.Name = "HighDpiForm";
      this.Text = "High-DPI Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.Panel panel1;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label scaleLabel1;
    private System.Windows.Forms.ComboBox scaleFactor;
    private System.Windows.Forms.Label scaleLabel2;
  }
}

