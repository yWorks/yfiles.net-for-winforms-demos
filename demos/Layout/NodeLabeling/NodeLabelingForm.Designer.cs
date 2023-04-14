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
using System.Windows.Forms.Design;

namespace Demo.yFiles.Layout.NodeLabeling
{
  partial class NodeLabelingForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitGraphBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.FitContentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.placeLabelsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.labelModelComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.sizeLabel = new System.Windows.Forms.ToolStripLabel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.description = new System.Windows.Forms.RichTextBox();
            this.sizeNumericUpDown = new Demo.yFiles.Layout.NodeLabeling.ToolStripNumberControl();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1236, 988);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(1236, 1063);
            this.toolStripContainer.TabIndex = 1;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip1);
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
            this.graphControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphControl.Name = "graphControl";
            this.graphControl.Size = new System.Drawing.Size(1236, 988);
            this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.graphControl.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1236, 33);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 34);
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
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(251, 34);
            this.zoomInToolStripMenuItem.Text = "Zoom in";
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(251, 34);
            this.zoomOutToolStripMenuItem.Text = "Zoom out";
            // 
            // fitGraphBoundsToolStripMenuItem
            // 
            this.fitGraphBoundsToolStripMenuItem.Name = "fitGraphBoundsToolStripMenuItem";
            this.fitGraphBoundsToolStripMenuItem.Size = new System.Drawing.Size(251, 34);
            this.fitGraphBoundsToolStripMenuItem.Text = "Fit graph bounds";
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomInButton,
            this.ZoomOutButton,
            this.FitContentButton,
            this.toolStripSeparator1,
            this.deleteButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.labelModelComboBox,
            this.sizeLabel,
            this.sizeNumericUpDown,
            this.toolStripSeparator3,
            this.placeLabelsButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 33);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
            this.toolStrip.Size = new System.Drawing.Size(1236, 42);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 0;
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = global::Demo.yFiles.Layout.NodeLabeling.Properties.Resources.plus_16;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(34, 29);
            this.ZoomInButton.Text = "Zoom In";
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = global::Demo.yFiles.Layout.NodeLabeling.Properties.Resources.minus_16;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(34, 29);
            this.ZoomOutButton.Text = "Zoom Out";
            // 
            // FitContentButton
            // 
            this.FitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitContentButton.Image = global::Demo.yFiles.Layout.NodeLabeling.Properties.Resources.fit_16;
            this.FitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitContentButton.Name = "FitContentButton";
            this.FitContentButton.Size = new System.Drawing.Size(34, 29);
            this.FitContentButton.Text = "Fit Content into View";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::Demo.yFiles.Layout.NodeLabeling.Properties.Resources.delete3_16;
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(34, 29);
            this.deleteButton.Text = "Delete Label";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // placeLabelsButton
            // 
            this.placeLabelsButton.Image = global::Demo.yFiles.Layout.NodeLabeling.Properties.Resources.reload_16;
            this.placeLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.placeLabelsButton.Name = "placeLabelsButton";
            this.placeLabelsButton.Size = new System.Drawing.Size(134, 29);
            this.placeLabelsButton.Text = "Place Labels";
            this.placeLabelsButton.Click += new System.EventHandler(this.PlaceLabelsButton_OnClick);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(104, 29);
            this.toolStripLabel1.Text = "LabelModel";
            // 
            // labelModelComboBox
            // 
            this.labelModelComboBox.Name = "labelModelComboBox";
            this.labelModelComboBox.Size = new System.Drawing.Size(121, 34);
            this.labelModelComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.labelModelComboBox.SelectedIndexChanged += new System.EventHandler(this.PlaceLabelsButton_OnClick);
            // 
            // sizeLabel
            // 
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(93, 29);
            this.sizeLabel.Text = "Label Size:";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "graphml";
            this.saveFileDialog.Filter = "GraphML Files|*.graphml|XML Files|*.xml|All files|*.*";
            this.saveFileDialog.Title = "Save GraphML file...";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "graphml";
            this.openFileDialog.Filter = "GraphML Files|*.graphml|XML Files|*.xml|All files|*.*";
            this.openFileDialog.Title = "Load GraphML file...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.description);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1494, 1063);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 2;
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Window;
            this.description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.description.Location = new System.Drawing.Point(0, 0);
            this.description.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Size = new System.Drawing.Size(252, 1063);
            this.description.TabIndex = 5;
            this.description.Text = "";
            // 
            // sizeNumericUpDown
            // 
            this.sizeNumericUpDown.Name = "sizeNumericUpDown";
            this.sizeNumericUpDown.Size = new System.Drawing.Size(66, 29);
            this.sizeNumericUpDown.Text = "8";
            this.sizeNumericUpDown.ValueChanged += new System.EventHandler(this.PlaceLabelsButton_OnClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // NodeLabelingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 1063);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::Demo.yFiles.Layout.NodeLabeling.Properties.Resources.yIcon;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NodeLabelingForm";
            this.Text = "Node Labeling Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnLoad);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton ZoomInButton;
    private System.Windows.Forms.ToolStripButton ZoomOutButton;
    private System.Windows.Forms.ToolStripButton FitContentButton;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton placeLabelsButton;
    private ToolStripButton deleteButton;
    private ToolStripSeparator toolStripSeparator2;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem zoomInToolStripMenuItem;
    private ToolStripMenuItem zoomOutToolStripMenuItem;
    private ToolStripMenuItem fitGraphBoundsToolStripMenuItem;
    private ToolStripLabel toolStripLabel1;
    private ToolStripComboBox labelModelComboBox;
    private ToolStripLabel sizeLabel;
    private ToolStripNumberControl sizeNumericUpDown;
    private ToolStripSeparator toolStripSeparator3;
  }
}
