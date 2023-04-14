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

using System.Configuration;
using System.Windows.Forms;
using yWorks.Geometry;

namespace Demo.yFiles.Layout.PortCandidateDemo
{
  partial class PortCandidateDemo
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.nodeStyleListBox = new System.Windows.Forms.ListBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fitGraphBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.runButton = new System.Windows.Forms.ToolStripButton();
      this.openButton = new System.Windows.Forms.ToolStripButton();
      this.saveButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.SuspendLayout();
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
      this.splitContainer1.Panel1.Controls.Add(this.description);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
      this.splitContainer1.Size = new System.Drawing.Size(868, 601);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 0;
      // 
      // description
      // 
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.Size = new System.Drawing.Size(252, 601);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.AutoScroll = true;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.nodeStyleListBox);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(612, 546);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(612, 601);
      this.toolStripContainer1.TabIndex = 7;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.Location = new System.Drawing.Point(100, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(512, 546);
      this.graphControl.TabIndex = 6;
      this.graphControl.Text = "graphControl1";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // nodeStyleListBox
      // 
      this.nodeStyleListBox.Dock = System.Windows.Forms.DockStyle.Left;
      this.nodeStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.nodeStyleListBox.FormattingEnabled = true;
      this.nodeStyleListBox.IntegralHeight = false;
      this.nodeStyleListBox.Location = new System.Drawing.Point(0, 0);
      this.nodeStyleListBox.Name = "nodeStyleListBox";
      this.nodeStyleListBox.Size = new System.Drawing.Size(100, 546);
      this.nodeStyleListBox.TabIndex = 0;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(612, 24);
      this.menuStrip1.TabIndex = 8;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator9,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // openFileToolStripMenuItem
      // 
      this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
      this.openFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.openFileToolStripMenuItem.Text = "Open";
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.saveAsToolStripMenuItem.Text = "Save as...";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(149, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.fitGraphBoundsToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // zoomInToolStripMenuItem
      // 
      this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
      this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
      this.zoomInToolStripMenuItem.Text = "Zoom in";
      // 
      // zoomOutToolStripMenuItem
      // 
      this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
      this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
      this.zoomOutToolStripMenuItem.Text = "Zoom out";
      // 
      // fitGraphBoundsToolStripMenuItem
      // 
      this.fitGraphBoundsToolStripMenuItem.Name = "fitGraphBoundsToolStripMenuItem";
      this.fitGraphBoundsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
      this.fitGraphBoundsToolStripMenuItem.Text = "Fit graph bounds";
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.saveButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator11,
            this.runButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 24);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(612, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 3;
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 23);
      // 
      // runButton
      // 
      this.runButton.Image = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.layout_hierarchic_16;
      this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(63, 20);
      this.runButton.Text = "Layout";
      this.runButton.ToolTipText = "Run layout";
      this.runButton.Click += new System.EventHandler(this.OnRunButtonClicked);
      // 
      // openButton
      // 
      this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openButton.Image = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.open_16;
      this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(23, 20);
      this.openButton.Text = "Open";
      // 
      // saveButton
      // 
      this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveButton.Image = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.save_16;
      this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(23, 20);
      this.saveButton.Text = "Save As...";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // PortCandidateDemo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(868, 601);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Layout.PortCandidateDemo.Properties.Resources.yIcon;
      this.Name = "PortCandidateDemo";
      this.Text = "Port Candidate Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.Demo_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton runButton;
    private ListBox nodeStyleListBox;
    private ToolStripContainer toolStripContainer1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem openFileToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem zoomInToolStripMenuItem;
    private ToolStripMenuItem zoomOutToolStripMenuItem;
    private ToolStripMenuItem fitGraphBoundsToolStripMenuItem;
    private ToolStripButton openButton;
    private ToolStripButton saveButton;
    private ToolStripSeparator toolStripSeparator1;


  }
}
