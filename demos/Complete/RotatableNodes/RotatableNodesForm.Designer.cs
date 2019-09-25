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

namespace Demo.yFiles.Complete.RotatableNodes
{
  partial class RotatableNodesForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.openButton = new System.Windows.Forms.ToolStripButton();
      this.saveButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.resetZoomButton = new System.Windows.Forms.ToolStripButton();
      this.cutButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.copyButton = new System.Windows.Forms.ToolStripButton();
      this.pasteButton = new System.Windows.Forms.ToolStripButton();
      this.deleteButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.snappingButton = new System.Windows.Forms.ToolStripButton();
      this.orthogonalEdgesButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.groupButton = new System.Windows.Forms.ToolStripButton();
      this.ungroupButton = new System.Windows.Forms.ToolStripButton();
      this.enterGroupButton = new System.Windows.Forms.ToolStripButton();
      this.exitGroupButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.sampleBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.layoutBox = new System.Windows.Forms.ToolStripComboBox();
      this.layoutButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
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
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(678, 605);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(678, 640);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
      // 
      // graphControl
      // 
      this.graphControl.AnimateScrollCommands = false;
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(678, 605);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 1;
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.saveButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.resetZoomButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.cutButton,
            this.copyButton,
            this.pasteButton,
            this.deleteButton,
            this.toolStripSeparator3,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator4,
            this.snappingButton,
            this.orthogonalEdgesButton,
            this.toolStripSeparator5,
            this.groupButton,
            this.ungroupButton,
            this.enterGroupButton,
            this.exitGroupButton,
            this.toolStripSeparator6,
            this.sampleBox,
            this.toolStripSeparator7,
            this.layoutBox,
            this.layoutButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(678, 35);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // openButton
      // 
      this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.open_16;
      this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(23, 24);
      this.openButton.Text = "Open GraphML";
      // 
      // saveButton
      // 
      this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.save_16;
      this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(23, 24);
      this.saveButton.Text = "Save GraphML";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 24);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 24);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 24);
      this.fitContentButton.ToolTipText = "Fit Content into View";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
      // 
      // resetZoomButton
      // 
      this.resetZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.resetZoomButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.zoom_original3_16;
      this.resetZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.resetZoomButton.Name = "resetZoomButton";
      this.resetZoomButton.Size = new System.Drawing.Size(23, 24);
      this.resetZoomButton.Text = "Original Size";
      // 
      // cutButton
      // 
      this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.cut2_16;
      this.cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutButton.Name = "cutButton";
      this.cutButton.Size = new System.Drawing.Size(23, 24);
      this.cutButton.Text = "Cut";
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
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer);
      this.splitContainer1.Size = new System.Drawing.Size(934, 640);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 2;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 640);
      this.description.TabIndex = 3;
      this.description.Text = "";
      // 
      // copyButton
      // 
      this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.copy_16;
      this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyButton.Name = "copyButton";
      this.copyButton.Size = new System.Drawing.Size(23, 24);
      this.copyButton.Text = "Copy";
      // 
      // pasteButton
      // 
      this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.paste_16;
      this.pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteButton.Name = "pasteButton";
      this.pasteButton.Size = new System.Drawing.Size(23, 24);
      this.pasteButton.Text = "Paste";
      // 
      // deleteButton
      // 
      this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.deleteButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.delete3_16;
      this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new System.Drawing.Size(23, 24);
      this.deleteButton.Text = "Delete";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 24);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.redo_16;
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 24);
      this.redoButton.Text = "Redo";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
      // 
      // snappingButton
      // 
      this.snappingButton.CheckOnClick = true;
      this.snappingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.snappingButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.snap_16;
      this.snappingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.snappingButton.Name = "snappingButton";
      this.snappingButton.Size = new System.Drawing.Size(23, 24);
      this.snappingButton.ToolTipText = "Snapping";
      this.snappingButton.Click += new System.EventHandler(this.SnappingButtonClick);
      // 
      // orthogonalEdgesButton
      // 
      this.orthogonalEdgesButton.Checked = true;
      this.orthogonalEdgesButton.CheckOnClick = true;
      this.orthogonalEdgesButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.orthogonalEdgesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.orthogonalEdgesButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.orthogonal_editing_16;
      this.orthogonalEdgesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.orthogonalEdgesButton.Name = "orthogonalEdgesButton";
      this.orthogonalEdgesButton.Size = new System.Drawing.Size(23, 24);
      this.orthogonalEdgesButton.ToolTipText = "Orthogonal Edges";
      this.orthogonalEdgesButton.Click += new System.EventHandler(this.OrthogonalEditingButtonClick);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
      // 
      // groupButton
      // 
      this.groupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.groupButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.group_16;
      this.groupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.groupButton.Name = "groupButton";
      this.groupButton.Size = new System.Drawing.Size(23, 24);
      this.groupButton.Text = "Group Selection";
      // 
      // ungroupButton
      // 
      this.ungroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ungroupButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.ungroup_16;
      this.ungroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ungroupButton.Name = "ungroupButton";
      this.ungroupButton.Size = new System.Drawing.Size(23, 24);
      this.ungroupButton.Text = "Ungroup Selection";
      // 
      // enterGroupButton
      // 
      this.enterGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.enterGroupButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.enter_group_16;
      this.enterGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.enterGroupButton.Name = "enterGroupButton";
      this.enterGroupButton.Size = new System.Drawing.Size(23, 24);
      this.enterGroupButton.Text = "Enter Group";
      // 
      // exitGroupButton
      // 
      this.exitGroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.exitGroupButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.exit_group_16;
      this.exitGroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.exitGroupButton.Name = "exitGroupButton";
      this.exitGroupButton.Size = new System.Drawing.Size(23, 24);
      this.exitGroupButton.Text = "Exit Group";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
      // 
      // sampleBox
      // 
      this.sampleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.sampleBox.Items.AddRange(new object[] {
            "Sample: Sine Wave",
            "Sample: Circle"});
      this.sampleBox.Name = "sampleBox";
      this.sampleBox.Size = new System.Drawing.Size(121, 27);
      this.sampleBox.ToolTipText = "Load Sample Graph";
      this.sampleBox.SelectedIndexChanged += new System.EventHandler(this.GraphChooserBoxSelectedIndexChanged);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
      // 
      // layoutBox
      // 
      this.layoutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.layoutBox.Items.AddRange(new object[] {
            "Layout: Hierarchic",
            "Layout: Organic",
            "Layout: Orthogonal",
            "Layout: Circular",
            "Layout: Tree",
            "Layout: Balloon",
            "Layout: Radial",
            "Routing: Polyline",
            "Routing: Organic"});
      this.layoutBox.Name = "layoutBox";
      this.layoutBox.Size = new System.Drawing.Size(121, 23);
      this.layoutBox.ToolTipText = "Layout or Routing Algorithm";
      this.layoutBox.SelectedIndexChanged += new System.EventHandler(this.LayoutChooserBoxSelectedIndexChanged);
      // 
      // layoutButton
      // 
      this.layoutButton.Image = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.reload_16;
      this.layoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.layoutButton.Name = "layoutButton";
      this.layoutButton.Size = new System.Drawing.Size(63, 20);
      this.layoutButton.Text = "Layout";
      this.layoutButton.ToolTipText = "Apply Layout";
      this.layoutButton.Click += new System.EventHandler(this.OnLayoutClick);
      // 
      // RotatableNodesForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(934, 640);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Complete.RotatableNodes.Properties.Resources.yFiles;
      this.Name = "RotatableNodesForm";
      this.Text = "Rotatable Nodes Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
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
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripButton openButton;
    private System.Windows.Forms.ToolStripButton saveButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton resetZoomButton;
    private ToolStripButton cutButton;
    private ToolStripButton copyButton;
    private ToolStripButton pasteButton;
    private ToolStripButton deleteButton;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton undoButton;
    private ToolStripButton redoButton;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton snappingButton;
    private ToolStripButton orthogonalEdgesButton;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton groupButton;
    private ToolStripButton ungroupButton;
    private ToolStripButton enterGroupButton;
    private ToolStripButton exitGroupButton;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripComboBox sampleBox;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripComboBox layoutBox;
    private ToolStripButton layoutButton;
  }
}

