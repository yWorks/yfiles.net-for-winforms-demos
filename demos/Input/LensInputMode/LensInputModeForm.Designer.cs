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

using yWorks.Geometry;

namespace Demo.yFiles.Graph.Input.LensInputMode
{
  partial class LensInputModeForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LensInputModeForm));
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.GraphControl = new yWorks.Controls.GraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.lensZoom = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.lensSize = new System.Windows.Forms.ToolStripTextBox();
      this.toggleProjection = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
      this.splitContainer.Panel1.SuspendLayout();
      this.splitContainer.Panel2.SuspendLayout();
      this.splitContainer.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer
      // 
      this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer.Location = new System.Drawing.Point(0, 0);
      this.splitContainer.Name = "splitContainer";
      // 
      // splitContainer.Panel1
      // 
      this.splitContainer.Panel1.Controls.Add(this.description);
      // 
      // splitContainer.Panel2
      // 
      this.splitContainer.Panel2.Controls.Add(this.GraphControl);
      this.splitContainer.Panel2.Controls.Add(this.toolStrip1);
      this.splitContainer.Size = new System.Drawing.Size(659, 447);
      this.splitContainer.SplitterDistance = 252;
      this.splitContainer.TabIndex = 0;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 447);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // GraphControl
      // 
      this.GraphControl.BackColor = System.Drawing.Color.White;
      this.GraphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.GraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GraphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.GraphControl.Location = new System.Drawing.Point(0, 25);
      this.GraphControl.Name = "GraphControl";
      this.GraphControl.Size = new System.Drawing.Size(403, 422);
      this.GraphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.GraphControl.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.lensZoom,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.lensSize,
            this.toggleProjection});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(403, 25);
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 22);
      this.zoomInButton.Text = "toolStripButton1";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
      this.zoomOutButton.Text = "toolStripButton2";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = ((System.Drawing.Image)(resources.GetObject("fitContentButton.Image")));
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 22);
      this.fitContentButton.Text = "toolStripButton3";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 22);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 22);
      this.redoButton.Text = "Redo";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
      this.toolStripLabel1.Text = "Zoom: ";
      // 
      // lensZoom
      // 
      this.lensZoom.Name = "lensZoom";
      this.lensZoom.Size = new System.Drawing.Size(40, 25);
      this.lensZoom.Text = "3";
      this.lensZoom.TextChanged += new System.EventHandler(this.ZoomPresetChanged);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(33, 22);
      this.toolStripLabel2.Text = "Size: ";
      // 
      // lensSize
      // 
      this.lensSize.Name = "lensSize";
      this.lensSize.Size = new System.Drawing.Size(60, 25);
      this.lensSize.Text = "200";
      this.lensSize.TextChanged += new System.EventHandler(this.SizePresetChanged);
      // 
      // toggleProjection
      // 
      this.toggleProjection.CheckOnClick = true;
      this.toggleProjection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toggleProjection.Image = ((System.Drawing.Image)(resources.GetObject("toggleProjection.Image")));
      this.toggleProjection.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toggleProjection.Name = "toggleProjection";
      this.toggleProjection.Size = new System.Drawing.Size(155, 19);
      this.toggleProjection.Text = "Toggle Isometric projection";
      this.toggleProjection.Click += new System.EventHandler(this.ToggleProjection);
      // 
      // LensInputModeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(659, 447);
      this.Controls.Add(this.splitContainer);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "LensInputModeForm";
      this.Text = "LensInputMode Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnWindowLoaded);
      this.splitContainer.Panel1.ResumeLayout(false);
      this.splitContainer.Panel2.ResumeLayout(false);
      this.splitContainer.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
      this.splitContainer.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.RichTextBox description;
    private yWorks.Controls.GraphControl GraphControl;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox lensZoom;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripTextBox lensSize;
    private System.Windows.Forms.ToolStripButton toggleProjection;
  }
}
