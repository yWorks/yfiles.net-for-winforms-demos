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

using System.Windows.Forms;

namespace Demo.yFiles.Algorithms.ShortestPath
{
  partial class ShortestPathForm
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
      this.components = new System.ComponentModel.Container();
      yWorks.Controls.ViewportLimiter viewportLimiter1 = new yWorks.Controls.ViewportLimiter();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.newGraphButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.layoutComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.applyLayoutButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.markAsSourceButton = new System.Windows.Forms.ToolStripButton();
      this.markAsTargetButton = new System.Windows.Forms.ToolStripButton();
      this.directedComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.setLabelValueButton = new System.Windows.Forms.ToolStripButton();
      this.deleteLabelsButton = new System.Windows.Forms.ToolStripButton();
      this.nodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.markAsSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.markAsTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.nodeContextMenuStrip.SuspendLayout();
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
      this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
      this.splitContainer1.Panel1.Controls.Add(this.descriptionTextBox);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
      this.splitContainer1.Size = new System.Drawing.Size(1095, 658);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 2;
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.descriptionTextBox.Location = new System.Drawing.Point(0, 0);
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ReadOnly = true;
      this.descriptionTextBox.Size = new System.Drawing.Size(252, 658);
      this.descriptionTextBox.TabIndex = 0;
      this.descriptionTextBox.Text = "";
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(839, 627);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(839, 658);
      this.toolStripContainer1.TabIndex = 2;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FitContentViewMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(839, 627);
      this.graphControl.TabIndex = 0;
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGraphButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.layoutComboBox,
            this.applyLayoutButton,
            this.toolStripSeparator4,
            this.markAsSourceButton,
            this.markAsTargetButton,
            this.directedComboBox,
            this.toolStripSeparator3,
            this.setLabelValueButton,
            this.deleteLabelsButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(839, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // newGraphButton
      // 
      this.newGraphButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newGraphButton.Image = global::Demo.yFiles.Algorithms.ShortestPath.Properties.Resources.new_document_16;
      this.newGraphButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newGraphButton.Name = "newGraphButton";
      this.newGraphButton.Size = new System.Drawing.Size(23, 20);
      this.newGraphButton.Text = "New Graph";
      this.newGraphButton.Click += new System.EventHandler(this.newGraphButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Algorithms.ShortestPath.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Algorithms.ShortestPath.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Algorithms.ShortestPath.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // layoutComboBox
      // 
      this.layoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.layoutComboBox.Name = "layoutComboBox";
      this.layoutComboBox.Size = new System.Drawing.Size(121, 23);
      this.layoutComboBox.ToolTipText = "Select a Layout Algorithm";
      // 
      // applyLayoutButton
      // 
      this.applyLayoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.applyLayoutButton.Image = global::Demo.yFiles.Algorithms.ShortestPath.Properties.Resources.reload_16;
      this.applyLayoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.applyLayoutButton.Name = "applyLayoutButton";
      this.applyLayoutButton.Size = new System.Drawing.Size(23, 20);
      this.applyLayoutButton.Text = "Run Layout";
      this.applyLayoutButton.ToolTipText = "Run Layout";
      this.applyLayoutButton.Click += new System.EventHandler(this.applyLayoutButton_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
      // 
      // markAsSourceButton
      // 
      this.markAsSourceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.markAsSourceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.markAsSourceButton.Name = "markAsSourceButton";
      this.markAsSourceButton.Size = new System.Drawing.Size(91, 20);
      this.markAsSourceButton.Text = "Mark as Source";
      this.markAsSourceButton.ToolTipText = "Mark Selected Nodes as Source";
      this.markAsSourceButton.Click += new System.EventHandler(this.markAsSourceButton_Click);
      // 
      // markAsTargetButton
      // 
      this.markAsTargetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.markAsTargetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.markAsTargetButton.Name = "markAsTargetButton";
      this.markAsTargetButton.Size = new System.Drawing.Size(89, 20);
      this.markAsTargetButton.Text = "Mark as Target";
      this.markAsTargetButton.ToolTipText = "Mark Selected Nodes as Target";
      this.markAsTargetButton.Click += new System.EventHandler(this.markAsTargetButton_Click);
      // 
      // directedComboBox
      // 
      this.directedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.directedComboBox.Items.AddRange(new object[] {
            "Directed Edges",
            "Undirected Edges"});
      this.directedComboBox.Name = "directedComboBox";
      this.directedComboBox.Size = new System.Drawing.Size(121, 23);
      this.directedComboBox.SelectedIndexChanged += new System.EventHandler(this.directedComboBox_SelectedIndexChanged);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // setLabelValueButton
      // 
      this.setLabelValueButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.setLabelValueButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.setLabelValueButton.Name = "setLabelValueButton";
      this.setLabelValueButton.Size = new System.Drawing.Size(84, 20);
      this.setLabelValueButton.Text = "Edit All Labels";
      this.setLabelValueButton.Click += new System.EventHandler(this.setLabelValueButton_Click);
      // 
      // deleteLabelsButton
      // 
      this.deleteLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.deleteLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.deleteLabelsButton.Name = "deleteLabelsButton";
      this.deleteLabelsButton.Size = new System.Drawing.Size(97, 20);
      this.deleteLabelsButton.Text = "Delete All Labels";
      this.deleteLabelsButton.Click += new System.EventHandler(this.deleteLabelsButton_Click);
      // 
      // nodeContextMenuStrip
      // 
      this.nodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsSourceMenuItem,
            this.markAsTargetMenuItem});
      this.nodeContextMenuStrip.Name = "nodeContextMenuStrip";
      this.nodeContextMenuStrip.Size = new System.Drawing.Size(157, 48);
      // 
      // markAsSourceMenuItem
      // 
      this.markAsSourceMenuItem.Name = "markAsSourceMenuItem";
      this.markAsSourceMenuItem.Size = new System.Drawing.Size(156, 22);
      this.markAsSourceMenuItem.Text = "Mark As Source";
      // 
      // markAsTargetMenuItem
      // 
      this.markAsTargetMenuItem.Name = "markAsTargetMenuItem";
      this.markAsTargetMenuItem.Size = new System.Drawing.Size(156, 22);
      this.markAsTargetMenuItem.Text = "Mark as target";
      // 
      // ShortestPathForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1095, 658);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Algorithms.ShortestPath.Properties.Resources.yIcon;
      this.Name = "ShortestPathForm";
      this.Text = "Shortest Path Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.ShortestPathForm_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.nodeContextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox descriptionTextBox;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ContextMenuStrip nodeContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem markAsSourceMenuItem;
    private System.Windows.Forms.ToolStripMenuItem markAsTargetMenuItem;
    private ToolStripContainer toolStripContainer1;
    private ToolStrip toolStrip1;
    private ToolStripButton newGraphButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton zoomInButton;
    private ToolStripButton zoomOutButton;
    private ToolStripButton fitContentButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripComboBox layoutComboBox;
    private ToolStripButton applyLayoutButton;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton markAsSourceButton;
    private ToolStripButton markAsTargetButton;
    private ToolStripComboBox directedComboBox;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton setLabelValueButton;
    private ToolStripButton deleteLabelsButton;
  }
}

