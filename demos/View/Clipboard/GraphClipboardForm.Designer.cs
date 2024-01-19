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

using yWorks.Geometry;

namespace Demo.yFiles.Graph.Clipboard
{
  partial class GraphClipboardForm
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
      yWorks.Controls.ViewportLimiter viewportLimiter2 = new yWorks.Controls.ViewportLimiter();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteSpecialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.cutButton = new System.Windows.Forms.ToolStripButton();
      this.copyButton = new System.Windows.Forms.ToolStripButton();
      this.pasteButton = new System.Windows.Forms.ToolStripButton();
      this.pasteSpecialButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.graphControl2 = new yWorks.Controls.GraphControl();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tabControl.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(945, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "FileMenuStrip";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.SaveMenuItem,
            this.ExitMenuItem});
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
      this.toolStripMenuItem1.Text = "File";
      // 
      // OpenMenuItem
      // 
      this.OpenMenuItem.Name = "OpenMenuItem";
      this.OpenMenuItem.Size = new System.Drawing.Size(112, 22);
      this.OpenMenuItem.Text = "Open...";
      // 
      // SaveMenuItem
      // 
      this.SaveMenuItem.Name = "SaveMenuItem";
      this.SaveMenuItem.Size = new System.Drawing.Size(112, 22);
      this.SaveMenuItem.Text = "Save...";
      // 
      // ExitMenuItem
      // 
      this.ExitMenuItem.Name = "ExitMenuItem";
      this.ExitMenuItem.Size = new System.Drawing.Size(112, 22);
      this.ExitMenuItem.Text = "Exit";
      this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.pasteSpecialToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.cutToolStripMenuItem.Text = "Cut";
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.pasteToolStripMenuItem.Text = "Paste";
      // 
      // pasteSpecialToolStripMenuItem
      // 
      this.pasteSpecialToolStripMenuItem.Name = "pasteSpecialToolStripMenuItem";
      this.pasteSpecialToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.pasteSpecialToolStripMenuItem.Text = "Paste Special";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.cutButton,
            this.copyButton,
            this.pasteButton,
            this.pasteSpecialButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(945, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.ToolTipText = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.ToolTipText = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.ToolTipText = "Fit Content";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // cutButton
      // 
      this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutButton.Image = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.cut2_16;
      this.cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutButton.Name = "cutButton";
      this.cutButton.Size = new System.Drawing.Size(23, 20);
      this.cutButton.Text = "Cut";
      // 
      // copyButton
      // 
      this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyButton.Image = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.copy_16;
      this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyButton.Name = "copyButton";
      this.copyButton.Size = new System.Drawing.Size(23, 20);
      this.copyButton.Text = "Copy";
      // 
      // pasteButton
      // 
      this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteButton.Image = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.paste_16;
      this.pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteButton.Name = "pasteButton";
      this.pasteButton.Size = new System.Drawing.Size(23, 20);
      this.pasteButton.Text = "Paste";
      // 
      // pasteSpecialButton
      // 
      this.pasteSpecialButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.pasteSpecialButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteSpecialButton.Name = "pasteSpecialButton";
      this.pasteSpecialButton.Size = new System.Drawing.Size(79, 20);
      this.pasteSpecialButton.Text = "Paste Special";
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
      this.splitContainer1.Size = new System.Drawing.Size(1201, 585);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 3;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 585);
      this.description.TabIndex = 3;
      this.description.Text = "";
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.AutoScroll = true;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(945, 530);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(945, 585);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // tabControl
      // 
      this.tabControl.Controls.Add(this.tabPage1);
      this.tabControl.Controls.Add(this.tabPage2);
      this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl.Location = new System.Drawing.Point(0, 0);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(945, 530);
      this.tabControl.TabIndex = 1;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.graphControl);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(937, 504);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Graph 1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(3, 3);
      this.graphControl.Name = "graphControl";
      this.graphControl.NavigationCommandsEnabled = false;
      this.graphControl.Size = new System.Drawing.Size(931, 498);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl1";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.graphControl2);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(937, 504);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Graph 2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // graphControl2
      // 
      this.graphControl2.BackColor = System.Drawing.Color.White;
      this.graphControl2.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl2.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl2.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl2.Location = new System.Drawing.Point(3, 3);
      this.graphControl2.Name = "graphControl2";
      this.graphControl2.NavigationCommandsEnabled = false;
      this.graphControl2.Size = new System.Drawing.Size(931, 498);
      this.graphControl2.TabIndex = 0;
      this.graphControl2.Text = "graphControl2";
      viewportLimiter2.Bounds = null;
      this.graphControl2.ViewportLimiter = viewportLimiter2;
      // 
      // GraphClipboardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1201, 585);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.Clipboard.Properties.Resources.yIcon;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "GraphClipboardForm";
      this.Text = "Graph Clipboard Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tabControl.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
    private System.Windows.Forms.ToolStripMenuItem SaveMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton cutButton;
    private System.Windows.Forms.ToolStripButton copyButton;
    private System.Windows.Forms.ToolStripButton pasteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton pasteSpecialButton;
    private System.Windows.Forms.ToolStripMenuItem pasteSpecialToolStripMenuItem;
    private yWorks.Controls.GraphControl graphControl;
    private yWorks.Controls.GraphControl graphControl2;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
  }
}

