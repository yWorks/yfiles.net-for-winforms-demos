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

using System.Windows.Forms;

namespace Demo.yFiles.Layout.EdgeLabeling
{
  partial class EdgeLabelingForm
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
      yWorks.Controls.ViewportLimiter viewportLimiter2 = new yWorks.Controls.ViewportLimiter();
      this.outerSplitContainer = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.innerSplitContainer = new System.Windows.Forms.SplitContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolBar = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitToSizeButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toggleOrthogonalEdgesButton = new System.Windows.Forms.ToolStripButton();
      this.placeLabelsButton = new System.Windows.Forms.ToolStripButton();
      this.propertiesPanel = new System.Windows.Forms.Panel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fitGraphBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sampleGraphsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.orthogonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.organicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label1 = new System.Windows.Forms.Label();
      this.outerSplitContainer.Panel1.SuspendLayout();
      this.outerSplitContainer.Panel2.SuspendLayout();
      this.outerSplitContainer.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.innerSplitContainer.Panel1.SuspendLayout();
      this.innerSplitContainer.Panel2.SuspendLayout();
      this.innerSplitContainer.SuspendLayout();
      this.panel1.SuspendLayout();
      this.toolBar.SuspendLayout();
      this.propertiesPanel.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // outerSplitContainer
      // 
      this.outerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.outerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.outerSplitContainer.Location = new System.Drawing.Point(0, 0);
      this.outerSplitContainer.Name = "outerSplitContainer";
      // 
      // outerSplitContainer.Panel1
      // 
      this.outerSplitContainer.Panel1.Controls.Add(this.description);
      // 
      // outerSplitContainer.Panel2
      // 
      this.outerSplitContainer.Panel2.Controls.Add(this.toolStripContainer1);
      this.outerSplitContainer.Size = new System.Drawing.Size(961, 526);
      this.outerSplitContainer.SplitterDistance = 252;
      this.outerSplitContainer.TabIndex = 0;
      // 
      // description
      // 
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.Size = new System.Drawing.Size(252, 526);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.innerSplitContainer);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(705, 477);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(705, 526);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolBar);
      // 
      // innerSplitContainer
      // 
      this.innerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.innerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.innerSplitContainer.Location = new System.Drawing.Point(0, 0);
      this.innerSplitContainer.Name = "innerSplitContainer";
      // 
      // innerSplitContainer.Panel1
      // 
      this.innerSplitContainer.Panel1.Controls.Add(this.panel1);
      // 
      // innerSplitContainer.Panel2
      // 
      this.innerSplitContainer.Panel2.Controls.Add(this.propertiesPanel);
      this.innerSplitContainer.Size = new System.Drawing.Size(705, 477);
      this.innerSplitContainer.SplitterDistance = 380;
      this.innerSplitContainer.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.graphControl);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(380, 477);
      this.panel1.TabIndex = 0;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.FitContentViewMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(380, 477);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl1";
      viewportLimiter2.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter2;
      // 
      // toolBar
      // 
      this.toolBar.Dock = System.Windows.Forms.DockStyle.None;
      this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitToSizeButton,
            this.toolStripSeparator1,
            this.toggleOrthogonalEdgesButton,
            this.placeLabelsButton});
      this.toolBar.Location = new System.Drawing.Point(0, 24);
      this.toolBar.Name = "toolBar";
      this.toolBar.Size = new System.Drawing.Size(705, 25);
      this.toolBar.Stretch = true;
      this.toolBar.TabIndex = 1;
      this.toolBar.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.EdgeLabeling.EdgeLabeling.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 22);
      this.zoomInButton.Text = "toolStripButton1";
      this.zoomInButton.ToolTipText = "Zoom in";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.EdgeLabeling.EdgeLabeling.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
      this.zoomOutButton.Text = "toolStripButton1";
      this.zoomOutButton.ToolTipText = "Zoom out";
      // 
      // fitToSizeButton
      // 
      this.fitToSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitToSizeButton.Image = global::Demo.yFiles.Layout.EdgeLabeling.EdgeLabeling.fit_16;
      this.fitToSizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitToSizeButton.Name = "fitToSizeButton";
      this.fitToSizeButton.Size = new System.Drawing.Size(23, 22);
      this.fitToSizeButton.Text = "toolStripButton1";
      this.fitToSizeButton.ToolTipText = "Fit graph bounds";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toggleOrthogonalEdgesButton
      // 
      this.toggleOrthogonalEdgesButton.Checked = true;
      this.toggleOrthogonalEdgesButton.CheckOnClick = true;
      this.toggleOrthogonalEdgesButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toggleOrthogonalEdgesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toggleOrthogonalEdgesButton.Image = global::Demo.yFiles.Layout.EdgeLabeling.EdgeLabeling.orthogonal_editing_16;
      this.toggleOrthogonalEdgesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toggleOrthogonalEdgesButton.Name = "toggleOrthogonalEdgesButton";
      this.toggleOrthogonalEdgesButton.Size = new System.Drawing.Size(23, 22);
      this.toggleOrthogonalEdgesButton.Text = "toolStripButton1";
      this.toggleOrthogonalEdgesButton.ToolTipText = "Orthogonal Edges";
      this.toggleOrthogonalEdgesButton.Click += new System.EventHandler(this.OrthogonalEdgesButtonClick);
      // 
      // placeLabelsButton
      // 
      this.placeLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.placeLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.placeLabelsButton.Name = "placeLabelsButton";
      this.placeLabelsButton.Size = new System.Drawing.Size(75, 22);
      this.placeLabelsButton.Text = "Place Labels";
      this.placeLabelsButton.Click += new System.EventHandler(this.PlaceLabelsButton_OnClick);
      // 
      // propertiesPanel
      // 
      this.propertiesPanel.Controls.Add(this.label1);
      this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
      this.propertiesPanel.Name = "propertiesPanel";
      this.propertiesPanel.Size = new System.Drawing.Size(321, 477);
      this.propertiesPanel.TabIndex = 0;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.sampleGraphsToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(705, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // openFileToolStripMenuItem
      // 
      this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
      this.openFileToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.openFileToolStripMenuItem.Text = "Open";
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.saveAsToolStripMenuItem.Text = "Save as...";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(118, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
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
      // sampleGraphsToolStripMenuItem
      // 
      this.sampleGraphsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orthogonalToolStripMenuItem,
            this.organicToolStripMenuItem});
      this.sampleGraphsToolStripMenuItem.Name = "sampleGraphsToolStripMenuItem";
      this.sampleGraphsToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
      this.sampleGraphsToolStripMenuItem.Text = "Sample Graphs";
      // 
      // orthogonalToolStripMenuItem
      // 
      this.orthogonalToolStripMenuItem.Name = "orthogonalToolStripMenuItem";
      this.orthogonalToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.orthogonalToolStripMenuItem.Text = "Orthogonal";
      this.orthogonalToolStripMenuItem.Click += new System.EventHandler(this.Orthogonal_OnClick);
      // 
      // organicToolStripMenuItem
      // 
      this.organicToolStripMenuItem.Name = "organicToolStripMenuItem";
      this.organicToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.organicToolStripMenuItem.Text = "Organic";
      this.organicToolStripMenuItem.Click += new System.EventHandler(this.Organic_OnClick);
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(5);
      this.label1.Size = new System.Drawing.Size(321, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "Edge Label Properties";
      // 
      // EdgeLabelingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(961, 526);
      this.Controls.Add(this.outerSplitContainer);
      this.Icon = global::Demo.yFiles.Layout.EdgeLabeling.EdgeLabeling.yFiles;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "EdgeLabelingForm";
      this.Text = "EdgeLabeling Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoad);
      this.outerSplitContainer.Panel1.ResumeLayout(false);
      this.outerSplitContainer.Panel2.ResumeLayout(false);
      this.outerSplitContainer.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.innerSplitContainer.Panel1.ResumeLayout(false);
      this.innerSplitContainer.Panel2.ResumeLayout(false);
      this.innerSplitContainer.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.toolBar.ResumeLayout(false);
      this.toolBar.PerformLayout();
      this.propertiesPanel.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer outerSplitContainer;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sampleGraphsToolStripMenuItem;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer innerSplitContainer;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStrip toolBar;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitToSizeButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toggleOrthogonalEdgesButton;
    private System.Windows.Forms.ToolStripButton placeLabelsButton;
    private System.Windows.Forms.Panel propertiesPanel;
    private System.Windows.Forms.ToolStripMenuItem orthogonalToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem organicToolStripMenuItem;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fitGraphBoundsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private Label label1;
  }
}