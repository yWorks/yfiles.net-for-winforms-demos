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

using yWorks.Controls;

namespace Demo.yFiles.ImageExport
{
  partial class ImageExportForm
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
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.exportButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.exportToFileButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.copyToClipboardButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.graphTabControl = new System.Windows.Forms.TabControl();
      this.graphTabPage = new System.Windows.Forms.TabPage();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.previewTabPage = new System.Windows.Forms.TabPage();
      this.previewCanvas = new yWorks.Controls.CanvasControl();
      this.moveViewportInputMode1 = new yWorks.Controls.Input.MoveViewportInputMode();
      this.description = new System.Windows.Forms.RichTextBox();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.zoom_1_1_Button = new System.Windows.Forms.ToolStripButton();
      this.toolStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.graphTabControl.SuspendLayout();
      this.graphTabPage.SuspendLayout();
      this.previewTabPage.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportButton,
            this.toolStripSeparator3,
            this.exportToFileButton,
            this.toolStripSeparator1,
            this.copyToClipboardButton,
            this.toolStripSeparator2,
            this.zoomInButton,
            this.zoomOutButton,
            this.zoom_1_1_Button,
            this.fitContentButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(724, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // exportButton
      // 
      this.exportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.exportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.exportButton.Name = "exportButton";
      this.exportButton.Size = new System.Drawing.Size(88, 20);
      this.exportButton.Text = "Export Preview";
      this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // exportToFileButton
      // 
      this.exportToFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.exportToFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.exportToFileButton.Name = "exportToFileButton";
      this.exportToFileButton.Size = new System.Drawing.Size(79, 20);
      this.exportToFileButton.Text = "Export to File";
      this.exportToFileButton.Click += new System.EventHandler(this.exportToFileButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // copyToClipboardButton
      // 
      this.copyToClipboardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.copyToClipboardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToClipboardButton.Name = "copyToClipboardButton";
      this.copyToClipboardButton.Size = new System.Drawing.Size(113, 20);
      this.copyToClipboardButton.Text = "Export to Clipboard";
      this.copyToClipboardButton.Click += new System.EventHandler(this.copyToClipboardButton_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.ImageExport.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.ImageExport.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.ImageExport.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
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
      this.splitContainer1.Size = new System.Drawing.Size(980, 564);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 1;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.graphTabControl);
      this.splitContainer2.Size = new System.Drawing.Size(724, 533);
      this.splitContainer2.SplitterDistance = 308;
      this.splitContainer2.TabIndex = 2;
      // 
      // graphTabControl
      // 
      this.graphTabControl.Controls.Add(this.graphTabPage);
      this.graphTabControl.Controls.Add(this.previewTabPage);
      this.graphTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphTabControl.Location = new System.Drawing.Point(0, 0);
      this.graphTabControl.Name = "graphTabControl";
      this.graphTabControl.SelectedIndex = 0;
      this.graphTabControl.Size = new System.Drawing.Size(412, 533);
      this.graphTabControl.TabIndex = 0;
      this.graphTabControl.SelectedIndexChanged += new System.EventHandler(this.graphTabControl_SelectedIndexChanged);
      // 
      // graphTabPage
      // 
      this.graphTabPage.BackColor = System.Drawing.SystemColors.ControlLight;
      this.graphTabPage.Controls.Add(this.graphControl);
      this.graphTabPage.Location = new System.Drawing.Point(4, 22);
      this.graphTabPage.Name = "graphTabPage";
      this.graphTabPage.Padding = new System.Windows.Forms.Padding(5);
      this.graphTabPage.Size = new System.Drawing.Size(404, 507);
      this.graphTabPage.TabIndex = 0;
      this.graphTabPage.Text = "Graph";
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(5, 5);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(394, 497);
      this.graphControl.TabIndex = 1;
      this.graphControl.Text = "Graph";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // previewTabPage
      // 
      this.previewTabPage.BackColor = System.Drawing.SystemColors.ControlLight;
      this.previewTabPage.Controls.Add(this.previewCanvas);
      this.previewTabPage.Location = new System.Drawing.Point(4, 22);
      this.previewTabPage.Name = "previewTabPage";
      this.previewTabPage.Padding = new System.Windows.Forms.Padding(5);
      this.previewTabPage.Size = new System.Drawing.Size(408, 507);
      this.previewTabPage.TabIndex = 1;
      this.previewTabPage.Text = "Export Preview";
      // 
      // previewCanvas
      // 
      this.previewCanvas.BackColor = System.Drawing.Color.WhiteSmoke;
      this.previewCanvas.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.previewCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.previewCanvas.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Always;
      this.previewCanvas.InputMode = this.moveViewportInputMode1;
      this.previewCanvas.Location = new System.Drawing.Point(5, 5);
      this.previewCanvas.Margin = new System.Windows.Forms.Padding(5);
      this.previewCanvas.Name = "previewCanvas";
      this.previewCanvas.Size = new System.Drawing.Size(398, 497);
      this.previewCanvas.TabIndex = 0;
      this.previewCanvas.Text = "previewControl";
      this.previewCanvas.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Always;
      viewportLimiter2.Bounds = null;
      this.previewCanvas.ViewportLimiter = viewportLimiter2;
      // 
      // moveViewportInputMode1
      // 
      this.moveViewportInputMode1.DragCursor = System.Windows.Forms.Cursors.Hand;
      // 
      // description
      // 
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.Size = new System.Drawing.Size(252, 564);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer2);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(724, 533);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(724, 564);
      this.toolStripContainer1.TabIndex = 2;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // zoom_1_1_Button
      // 
      this.zoom_1_1_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoom_1_1_Button.Image = global::Demo.yFiles.ImageExport.Properties.Resources.zoom_original3_16;
      this.zoom_1_1_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoom_1_1_Button.Name = "zoom_1_1_Button";
      this.zoom_1_1_Button.Size = new System.Drawing.Size(23, 20);
      this.zoom_1_1_Button.Text = "Zoom to 100%";
      // 
      // ImageExportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(980, 564);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.ImageExport.Properties.Resources.yIcon;
      this.Name = "ImageExportForm";
      this.Text = "Image Export Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.graphTabControl.ResumeLayout(false);
      this.graphTabPage.ResumeLayout(false);
      this.previewTabPage.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripButton exportButton;
    private System.Windows.Forms.TabControl graphTabControl;
    private System.Windows.Forms.TabPage graphTabPage;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.TabPage previewTabPage;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private CanvasControl previewCanvas;
    private yWorks.Controls.Input.MoveViewportInputMode moveViewportInputMode1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripButton exportToFileButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.ToolStripButton copyToClipboardButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton zoom_1_1_Button;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
  }
}

