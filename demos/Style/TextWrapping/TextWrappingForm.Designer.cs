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

using System.Windows.Forms;
using yWorks.Geometry;

namespace Demo.yFiles.Graph.TextWrapping
{
  partial class TextWrappingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextWrappingForm));
            this.graphControl = new yWorks.Controls.GraphControl();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.description = new System.Windows.Forms.RichTextBox();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.FitContentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphControl
            // 
            this.graphControl.BackColor = System.Drawing.Color.White;
            this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
            this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
            this.graphControl.Location = new System.Drawing.Point(0, 0);
            this.graphControl.Name = "graphControl";
            this.graphControl.Size = new System.Drawing.Size(558, 433);
            this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.graphControl.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(558, 433);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(558, 464);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
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
            this.splitContainer1.Size = new System.Drawing.Size(814, 464);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.TabIndex = 1;
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Window;
            this.description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.description.Location = new System.Drawing.Point(0, 0);
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Size = new System.Drawing.Size(252, 464);
            this.description.TabIndex = 5;
            this.description.Text = "";
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = global::Demo.yFiles.Graph.TextWrapping.Properties.Resources.plus_16;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(23, 20);
            this.ZoomInButton.ToolTipText = "Zoom In";
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = global::Demo.yFiles.Graph.TextWrapping.Properties.Resources.minus_16;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(23, 20);
            this.ZoomOutButton.ToolTipText = "Zoom Out";
            // 
            // FitContentButton
            // 
            this.FitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitContentButton.Image = global::Demo.yFiles.Graph.TextWrapping.Properties.Resources.fit_16;
            this.FitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitContentButton.Name = "FitContentButton";
            this.FitContentButton.Size = new System.Drawing.Size(23, 20);
            this.FitContentButton.ToolTipText = "Fit Content into View";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomInButton,
            this.ZoomOutButton,
            this.FitContentButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
            this.toolStrip1.Size = new System.Drawing.Size(558, 31);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TextWrappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 464);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::Demo.yFiles.Graph.TextWrapping.Properties.Resources.yIcon;
            this.Name = "TextWrappingForm";
            this.Text = "Text Wrapping Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private ToolStrip toolStrip1;
    private ToolStripButton ZoomInButton;
    private ToolStripButton ZoomOutButton;
    private ToolStripButton FitContentButton;
  }
}

