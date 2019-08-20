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
using yWorks.Geometry;

namespace Demo.yFiles.Layout.AbortHandler
{
  partial class AbortHandlerForm
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
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.cutButton = new System.Windows.Forms.ToolStripButton();
      this.copyButton = new System.Windows.Forms.ToolStripButton();
      this.pasteButton = new System.Windows.Forms.ToolStripButton();
      this.deleteButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.layoutComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.stopDuration = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.cancelDuration = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
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
      this.splitContainer1.Panel2.Controls.Add(this.graphControl);
      this.splitContainer1.Panel2.Controls.Add(this.toolStrip);
      this.splitContainer1.Size = new System.Drawing.Size(1203, 601);
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
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FitContentViewMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.Location = new System.Drawing.Point(0, 33);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(947, 568);
      this.graphControl.TabIndex = 6;
      this.graphControl.Text = "graphControl1";
      this.graphControl.ViewPoint = new yWorks.Geometry.PointD(15D, -20D);
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // toolStrip
      // 
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator4,
            this.cutButton,
            this.copyButton,
            this.pasteButton,
            this.deleteButton,
            this.toolStripSeparator2,
            this.layoutComboBox,
            this.toolStripLabel1,
            this.stopDuration,
            this.toolStripLabel3,
            this.cancelDuration,
            this.toolStripSeparator11,
            this.toolStripButton1});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(947, 33);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 3;
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 22);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.redo_16;
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 22);
      this.redoButton.Text = "Redo";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // cutButton
      // 
      this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.cut2_16;
      this.cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutButton.Name = "cutButton";
      this.cutButton.Size = new System.Drawing.Size(23, 22);
      this.cutButton.Text = "C&ut";
      // 
      // copyButton
      // 
      this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.copy_16;
      this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyButton.Name = "copyButton";
      this.copyButton.Size = new System.Drawing.Size(23, 22);
      this.copyButton.Text = "&Copy";
      // 
      // pasteButton
      // 
      this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.paste_16;
      this.pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteButton.Name = "pasteButton";
      this.pasteButton.Size = new System.Drawing.Size(23, 22);
      this.pasteButton.Text = "&Paste";
      // 
      // deleteButton
      // 
      this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.deleteButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.delete2_16;
      this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new System.Drawing.Size(23, 22);
      this.deleteButton.Text = "Delete";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 22);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 22);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
      // 
      // layouterComboBox
      // 
      this.layoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.layoutComboBox.Items.AddRange(new object[] {
            "Hierarchic",
            "Organic",
            "Orthogonal"});
      this.layoutComboBox.Name = "layoutComboBox";
      this.layoutComboBox.Size = new System.Drawing.Size(125, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(78, 22);
      this.toolStripLabel1.Text = "Stop Seconds";
      // 
      // stopDuration
      // 
      this.stopDuration.AutoSize = false;
      this.stopDuration.Name = "stopDuration";
      this.stopDuration.Size = new System.Drawing.Size(30, 25);
      this.stopDuration.Text = "0";
      this.stopDuration.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(90, 22);
      this.toolStripLabel3.Text = "Cancel Seconds";
      // 
      // cancelDuration
      // 
      this.cancelDuration.AutoSize = false;
      this.cancelDuration.Name = "cancelDuration";
      this.cancelDuration.Size = new System.Drawing.Size(30, 25);
      this.cancelDuration.Text = "0";
      this.cancelDuration.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.layout_hierarchic_16;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(63, 22);
      this.toolStripButton1.Text = "Layout";
      this.toolStripButton1.Click += new System.EventHandler(this.OnLayoutButtonClicked);
      // 
      // AbortHandlerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1203, 601);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Layout.AbortHandler.Properties.Resources.yIcon;
      this.Name = "AbortHandlerForm";
      this.Text = "AbortHandler Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton cutButton;
    private System.Windows.Forms.ToolStripButton copyButton;
    private System.Windows.Forms.ToolStripButton pasteButton;
    private System.Windows.Forms.ToolStripButton deleteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private yWorks.Controls.GraphControl graphControl;
    private ToolStripComboBox layoutComboBox;
    private ToolStripLabel toolStripLabel1;
    private ToolStripTextBox stopDuration;
    private ToolStripLabel toolStripLabel3;
    private ToolStripTextBox cancelDuration;
    private ToolStripButton toolStripButton1;


  }
}