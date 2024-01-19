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

namespace Demo.yFiles.Layout.PreferredLabelPlacement
{
  partial class PreferredLabelPlacementForm
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
      this.outerSplitContainer = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.innerSplitContainer = new System.Windows.Forms.SplitContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolBar = new System.Windows.Forms.ToolStrip();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitToSizeButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.layoutComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.refreshLayoutButton = new System.Windows.Forms.ToolStripButton();
      this.propertiesPanel = new System.Windows.Forms.Panel();
      this.label5 = new System.Windows.Forms.Label();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.outerSplitContainer.Panel1.SuspendLayout();
      this.outerSplitContainer.Panel2.SuspendLayout();
      this.outerSplitContainer.SuspendLayout();
      this.innerSplitContainer.Panel1.SuspendLayout();
      this.innerSplitContainer.Panel2.SuspendLayout();
      this.innerSplitContainer.SuspendLayout();
      this.panel1.SuspendLayout();
      this.toolBar.SuspendLayout();
      this.propertiesPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
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
      this.innerSplitContainer.Size = new System.Drawing.Size(705, 501);
      this.innerSplitContainer.SplitterDistance = 330;
      this.innerSplitContainer.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.graphControl);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(405, 501);
      this.panel1.TabIndex = 0;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(405, 501);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl1";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // toolBar
      // 
      this.toolBar.Dock = System.Windows.Forms.DockStyle.None;
      this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator2,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitToSizeButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.layoutComboBox,
            this.refreshLayoutButton});
      this.toolBar.Location = new System.Drawing.Point(3, 0);
      this.toolBar.Name = "toolBar";
      this.toolBar.Size = new System.Drawing.Size(370, 25);
      this.toolBar.TabIndex = 1;
      this.toolBar.Text = "toolStrip1";
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 22);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.redo_16;
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
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 22);
      this.zoomInButton.Text = "toolStripButton1";
      this.zoomInButton.ToolTipText = "Zoom in";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
      this.zoomOutButton.Text = "toolStripButton1";
      this.zoomOutButton.ToolTipText = "Zoom out";
      // 
      // fitToSizeButton
      // 
      this.fitToSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitToSizeButton.Image = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.fit_16;
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
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(46, 22);
      this.toolStripLabel1.Text = "Layout:";
      // 
      // layoutComboBox
      // 
      this.layoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.layoutComboBox.Name = "layoutComboBox";
      this.layoutComboBox.Size = new System.Drawing.Size(160, 25);
      // 
      // refreshLayoutButton
      // 
      this.refreshLayoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.refreshLayoutButton.Image = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.reload_16;
      this.refreshLayoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshLayoutButton.Name = "refreshLayoutButton";
      this.refreshLayoutButton.Size = new System.Drawing.Size(23, 22);
      this.refreshLayoutButton.Text = "Refresh Layout";
      this.refreshLayoutButton.Click += new System.EventHandler(this.OnDoLayoutButtonClicked);
      // 
      // propertiesPanel
      // 
      this.propertiesPanel.Controls.Add(this.label5);
      this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
      this.propertiesPanel.Name = "propertiesPanel";
      this.propertiesPanel.Size = new System.Drawing.Size(296, 501);
      this.propertiesPanel.TabIndex = 0;
      // 
      // label5
      // 
      this.label5.Dock = System.Windows.Forms.DockStyle.Top;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(0, 0);
      this.label5.Name = "label5";
      this.label5.Padding = new System.Windows.Forms.Padding(5);
      this.label5.Size = new System.Drawing.Size(296, 23);
      this.label5.TabIndex = 9;
      this.label5.Text = "Edge Label Properties";
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.innerSplitContainer);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(705, 501);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(705, 526);
      this.toolStripContainer1.TabIndex = 1;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolBar);
      // 
      // PreferredLabelPlacementForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(961, 526);
      this.Controls.Add(this.outerSplitContainer);
      this.Icon = global::Demo.yFiles.Layout.PreferredLabelPlacement.PreferredLabelPlacement.yFiles;
      this.Name = "PreferredLabelPlacementForm";
      this.Text = "Preferred Label Placement Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoad);
      this.outerSplitContainer.Panel1.ResumeLayout(false);
      this.outerSplitContainer.Panel2.ResumeLayout(false);
      this.outerSplitContainer.ResumeLayout(false);
      this.innerSplitContainer.Panel1.ResumeLayout(false);
      this.innerSplitContainer.Panel2.ResumeLayout(false);
      this.innerSplitContainer.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.toolBar.ResumeLayout(false);
      this.toolBar.PerformLayout();
      this.propertiesPanel.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer outerSplitContainer;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer innerSplitContainer;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStrip toolBar;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitToSizeButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.Panel propertiesPanel;
    private System.Windows.Forms.RichTextBox description;
    private ToolStripButton undoButton;
    private ToolStripButton redoButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripLabel toolStripLabel1;
    private ToolStripComboBox layoutComboBox;
    private ToolStripButton refreshLayoutButton;
    private Label label5;
    private ToolStripContainer toolStripContainer1;
  }
}
