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

using System.Windows.Forms;
using Demo.yFiles.Graph.Collapse.Properties;
using yWorks.Geometry;

namespace Demo.yFiles.Graph.Collapse
{
  partial class GraphCollapseForm
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
      yWorks.Controls.ViewportLimiter viewportLimiter1 = new yWorks.Controls.ViewportLimiter();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
      this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.FitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.layoutComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(503, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "FileMenuStrip";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
      this.toolStripMenuItem1.Text = "File";
      // 
      // ExitMenuItem
      // 
      this.ExitMenuItem.Name = "ExitMenuItem";
      this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
      this.ExitMenuItem.Text = "Exit";
      this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomInButton,
            this.ZoomOutButton,
            this.FitContentButton,
            this.toolStripSeparator1,
            this.layoutComboBox});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(503, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // ZoomInButton
      // 
      this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomInButton.Image = global::Demo.yFiles.Graph.Collapse.Properties.Resources.plus_16;
      this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomInButton.Name = "ZoomInButton";
      this.ZoomInButton.Size = new System.Drawing.Size(23, 20);
      this.ZoomInButton.ToolTipText = "Zoom In";
      // 
      // ZoomOutButton
      // 
      this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomOutButton.Image = global::Demo.yFiles.Graph.Collapse.Properties.Resources.minus_16;
      this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomOutButton.Name = "ZoomOutButton";
      this.ZoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.ZoomOutButton.ToolTipText = "Zoom Out";
      // 
      // FitContentButton
      // 
      this.FitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.FitContentButton.Image = global::Demo.yFiles.Graph.Collapse.Properties.Resources.fit_16;
      this.FitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.FitContentButton.Name = "FitContentButton";
      this.FitContentButton.Size = new System.Drawing.Size(23, 20);
      this.FitContentButton.ToolTipText = "Fit Content into View";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // layoutComboBox
      // 
      this.layoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.layoutComboBox.Name = "layoutComboBox";
      this.layoutComboBox.Size = new System.Drawing.Size(121, 23);
      this.layoutComboBox.SelectedIndexChanged += new System.EventHandler(this.layoutComboBox_SelectedIndexChanged);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 55);
      this.graphControl.Margin = new System.Windows.Forms.Padding(5);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(503, 618);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.descriptionTextBox);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.graphControl);
      this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
      this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
      this.splitContainer1.Size = new System.Drawing.Size(759, 673);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 3;
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.descriptionTextBox.Location = new System.Drawing.Point(0, 0);
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.Size = new System.Drawing.Size(252, 673);
      this.descriptionTextBox.TabIndex = 0;
      this.descriptionTextBox.Text = "";
      // 
      // GraphCollapseForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(759, 673);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.Collapse.Properties.Resources.yIcon;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "GraphCollapseForm";
      this.Text = "yFiles Graph Collapsing Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton ZoomInButton;
    private System.Windows.Forms.ToolStripButton ZoomOutButton;
    private System.Windows.Forms.ToolStripButton FitContentButton;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripComboBox layoutComboBox;
    private SplitContainer splitContainer1;
    private RichTextBox descriptionTextBox;
  }
}

