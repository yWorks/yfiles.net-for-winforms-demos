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

using yWorks.Geometry;

namespace Demo.yFiles.Graph.Folding
{
  partial class FoldingForm
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
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.showModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.separateDummyEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excludeDummyEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mergeDummyEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mergeUndirectedDummyEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.expandGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.collapseGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.enterGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.loadGraphMLButton = new System.Windows.Forms.ToolStripButton();
      this.saveGraphMLButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.enterGroupButton = new System.Windows.Forms.ToolStripButton();
      this.exitGroupButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.showContentsButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.expandGroupButton = new System.Windows.Forms.ToolStripButton();
      this.collapseGroupButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.toolStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(752, 674);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(752, 729);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
      this.graphControl.Size = new System.Drawing.Size(752, 674);
      this.graphControl.TabIndex = 1;
      this.graphControl.Text = "graphControl";
      this.graphControl.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
      // 
      // menuStrip
      // 
      this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.groupingToolStripMenuItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(752, 24);
      this.menuStrip.TabIndex = 1;
      this.menuStrip.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.openToolStripMenuItem.Text = "Open";
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.saveToolStripMenuItem.Text = "Save";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(100, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showModelToolStripMenuItem,
            this.separateDummyEdgesToolStripMenuItem,
            this.excludeDummyEdgesToolStripMenuItem,
            this.mergeDummyEdgesToolStripMenuItem,
            this.mergeUndirectedDummyEdgesToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // showModelToolStripMenuItem
      // 
      this.showModelToolStripMenuItem.Name = "showModelToolStripMenuItem";
      this.showModelToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
      this.showModelToolStripMenuItem.Text = "Show Model";
      this.showModelToolStripMenuItem.Click += new System.EventHandler(this.showModelToolStripMenuItem_Click);
      // 
      // separateDummyEdgesToolStripMenuItem
      // 
      this.separateDummyEdgesToolStripMenuItem.Name = "separateDummyEdgesToolStripMenuItem";
      this.separateDummyEdgesToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
      this.separateDummyEdgesToolStripMenuItem.Text = "Separate Dummy Edges";
      this.separateDummyEdgesToolStripMenuItem.Click += new System.EventHandler(this.separateFoldingEdgesToolStripMenuItem_Click);
      // 
      // excludeDummyEdgesToolStripMenuItem
      // 
      this.excludeDummyEdgesToolStripMenuItem.Name = "excludeDummyEdgesToolStripMenuItem";
      this.excludeDummyEdgesToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
      this.excludeDummyEdgesToolStripMenuItem.Text = "Exclude Dummy Edges";
      this.excludeDummyEdgesToolStripMenuItem.Click += new System.EventHandler(this.excludeFoldingEdgesToolStripMenuItem_Click);
      // 
      // mergeDummyEdgesToolStripMenuItem
      // 
      this.mergeDummyEdgesToolStripMenuItem.Name = "mergeDummyEdgesToolStripMenuItem";
      this.mergeDummyEdgesToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
      this.mergeDummyEdgesToolStripMenuItem.Text = "Merge Dummy Edges";
      this.mergeDummyEdgesToolStripMenuItem.Click += new System.EventHandler(this.mergeFoldingEdgesToolStripMenuItem_Click);
      // 
      // mergeUndirectedDummyEdgesToolStripMenuItem
      // 
      this.mergeUndirectedDummyEdgesToolStripMenuItem.Name = "mergeUndirectedDummyEdgesToolStripMenuItem";
      this.mergeUndirectedDummyEdgesToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
      this.mergeUndirectedDummyEdgesToolStripMenuItem.Text = "Merge Undirected Dummy Edges";
      this.mergeUndirectedDummyEdgesToolStripMenuItem.Click += new System.EventHandler(this.mergeUndirectedFoldingEdgesToolStripMenuItem_Click);
      // 
      // groupingToolStripMenuItem
      // 
      this.groupingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandGroupToolStripMenuItem,
            this.collapseGroupToolStripMenuItem,
            this.toolStripSeparator6,
            this.enterGroupToolStripMenuItem,
            this.exitGroupToolStripMenuItem});
      this.groupingToolStripMenuItem.Name = "groupingToolStripMenuItem";
      this.groupingToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
      this.groupingToolStripMenuItem.Text = "Grouping";
      // 
      // expandGroupToolStripMenuItem
      // 
      this.expandGroupToolStripMenuItem.Name = "expandGroupToolStripMenuItem";
      this.expandGroupToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.expandGroupToolStripMenuItem.Text = "Expand Group";
      // 
      // collapseGroupToolStripMenuItem
      // 
      this.collapseGroupToolStripMenuItem.Name = "collapseGroupToolStripMenuItem";
      this.collapseGroupToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.collapseGroupToolStripMenuItem.Text = "Collapse Group";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(152, 6);
      // 
      // enterGroupToolStripMenuItem
      // 
      this.enterGroupToolStripMenuItem.Name = "enterGroupToolStripMenuItem";
      this.enterGroupToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.enterGroupToolStripMenuItem.Text = "Enter Group";
      // 
      // exitGroupToolStripMenuItem
      // 
      this.exitGroupToolStripMenuItem.Name = "exitGroupToolStripMenuItem";
      this.exitGroupToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
      this.exitGroupToolStripMenuItem.Text = "Exit Group";
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGraphMLButton,
            this.saveGraphMLButton,
            this.toolStripSeparator2,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.enterGroupButton,
            this.exitGroupButton,
            this.toolStripSeparator3,
            this.showContentsButton,
            this.toolStripSeparator4,
            this.expandGroupButton,
            this.collapseGroupButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 24);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(752, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // loadGraphMLButton
      // 
      this.loadGraphMLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.loadGraphMLButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.open_16;
      this.loadGraphMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.loadGraphMLButton.Name = "loadGraphMLButton";
      this.loadGraphMLButton.Size = new System.Drawing.Size(23, 20);
      this.loadGraphMLButton.Text = "Load GraphML";
      // 
      // saveGraphMLButton
      // 
      this.saveGraphMLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveGraphMLButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.save_16;
      this.saveGraphMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveGraphMLButton.Name = "saveGraphMLButton";
      this.saveGraphMLButton.Size = new System.Drawing.Size(23, 20);
      this.saveGraphMLButton.Text = "Save GraphML";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // enterGroupButton
      // 
      this.enterGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.enterGroupButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.enter_group_16;
      this.enterGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.enterGroupButton.Name = "enterGroupButton";
      this.enterGroupButton.Size = new System.Drawing.Size(23, 20);
      this.enterGroupButton.Text = "Enter Group";
      // 
      // exitGroupButton
      // 
      this.exitGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.exitGroupButton.Image = global::Demo.yFiles.Graph.Folding.Properties.Resources.exit_group_16;
      this.exitGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.exitGroupButton.Name = "exitGroupButton";
      this.exitGroupButton.Size = new System.Drawing.Size(23, 20);
      this.exitGroupButton.Text = "Exit Group";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // showContentsButton
      // 
      this.showContentsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.showContentsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.showContentsButton.Name = "showContentsButton";
      this.showContentsButton.Size = new System.Drawing.Size(127, 20);
      this.showContentsButton.Text = "Show Group Contents";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
      // 
      // expandGroupButton
      // 
      this.expandGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.expandGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.expandGroupButton.Name = "expandGroupButton";
      this.expandGroupButton.Size = new System.Drawing.Size(85, 20);
      this.expandGroupButton.Text = "Expand Group";
      // 
      // collapseGroupButton
      // 
      this.collapseGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.collapseGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.collapseGroupButton.Name = "collapseGroupButton";
      this.collapseGroupButton.Size = new System.Drawing.Size(92, 20);
      this.collapseGroupButton.Text = "Collapse Group";
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
      this.splitContainer1.Panel1.Controls.Add(this.richTextBox1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer);
      this.splitContainer1.Size = new System.Drawing.Size(1008, 729);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 3;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Location = new System.Drawing.Point(0, 0);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(252, 729);
      this.richTextBox1.TabIndex = 2;
      this.richTextBox1.Text = "";
      // 
      // FoldingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1008, 729);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.Folding.Properties.Resources.yIcon;
      this.MainMenuStrip = this.menuStrip;
      this.Name = "FoldingForm";
      this.Text = "Folding Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton enterGroupButton;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton saveGraphMLButton;
    private System.Windows.Forms.ToolStripButton exitGroupButton;
    private System.Windows.Forms.ToolStripButton loadGraphMLButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton showContentsButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showModelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem separateDummyEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem excludeDummyEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mergeDummyEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mergeUndirectedDummyEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton expandGroupButton;
    private System.Windows.Forms.ToolStripButton collapseGroupButton;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem groupingToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem expandGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem collapseGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripMenuItem enterGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitGroupToolStripMenuItem;
  }
}

