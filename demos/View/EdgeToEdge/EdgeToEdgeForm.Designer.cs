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

using yWorks.Controls;
using yWorks.Geometry;

namespace Demo.yFiles.Graph.EdgeToEdge
{
  partial class EdgeToEdgeForm
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
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoom11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fitContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ungroupSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.expandGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.collapseGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.enterGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.newButton = new System.Windows.Forms.ToolStripButton();
      this.loadGraphMLButton = new System.Windows.Forms.ToolStripButton();
      this.saveGraphMLButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.printButton = new System.Windows.Forms.ToolStripButton();
      this.exportImageButton = new System.Windows.Forms.ToolStripButton();
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
      this.groupButton = new System.Windows.Forms.ToolStripButton();
      this.ungroupButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.snappingButton = new System.Windows.Forms.ToolStripButton();
      this.orthogonalEditingButton = new System.Windows.Forms.ToolStripButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer1);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(844, 528);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(844, 583);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
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
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.graphControl);
      this.splitContainer1.Size = new System.Drawing.Size(844, 528);
      this.splitContainer1.SplitterDistance = 300;
      this.splitContainer1.TabIndex = 3;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 237);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(300, 291);
      this.description.TabIndex = 2;
      this.description.Text = "";
      // 
      // graphOverviewControl
      // 
      this.graphOverviewControl.AutoDrag = false;
      this.graphOverviewControl.MouseWheelBehavior = MouseWheelBehaviors.None;
      this.graphOverviewControl.BackColor = System.Drawing.Color.White;
      this.graphOverviewControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphOverviewControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphOverviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphOverviewControl.Graph = null;
      this.graphOverviewControl.GraphControl = null;
      this.graphOverviewControl.GraphVisualCreator = null;
      this.graphOverviewControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      this.graphOverviewControl.Location = new System.Drawing.Point(0, 0);
      this.graphOverviewControl.Name = "graphOverviewControl";
      this.graphOverviewControl.Size = new System.Drawing.Size(296, 233);
      this.graphOverviewControl.TabIndex = 1;
      this.graphOverviewControl.Text = "graphOverviewControl";
      this.graphOverviewControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      viewportLimiter1.Bounds = null;
      this.graphOverviewControl.ViewportLimiter = viewportLimiter1;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
      this.graphControl.Size = new System.Drawing.Size(540, 528);
      this.graphControl.TabIndex = 1;
      this.graphControl.Text = "graphControl";
      this.graphControl.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
      viewportLimiter2.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter2;
      // 
      // menuStrip
      // 
      this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.groupingToolStripMenuItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(844, 24);
      this.menuStrip.TabIndex = 1;
      this.menuStrip.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator10,
            this.printToolStripMenuItem,
            this.exportImageToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
      this.newToolStripMenuItem.Text = "New";
      this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
      this.openToolStripMenuItem.Text = "Open";
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
      this.saveToolStripMenuItem.Text = "Save";
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(140, 6);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
      this.printToolStripMenuItem.Text = "Print";
      // 
      // exportImageToolStripMenuItem
      // 
      this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
      this.exportImageToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
      this.exportImageToolStripMenuItem.Text = "Export Image";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator6,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator7,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.cutToolStripMenuItem.Text = "Cut";
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.pasteToolStripMenuItem.Text = "Paste";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(104, 6);
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.deleteToolStripMenuItem.Text = "Delete";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(104, 6);
      // 
      // undoToolStripMenuItem
      // 
      this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
      this.undoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.undoToolStripMenuItem.Text = "Undo";
      // 
      // redoToolStripMenuItem
      // 
      this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
      this.redoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.redoToolStripMenuItem.Text = "Redo";
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.zoom11ToolStripMenuItem,
            this.fitContentToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // zoomInToolStripMenuItem
      // 
      this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
      this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.zoomInToolStripMenuItem.Text = "Zoom in";
      // 
      // zoomOutToolStripMenuItem
      // 
      this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
      this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.zoomOutToolStripMenuItem.Text = "Zoom out";
      // 
      // zoom11ToolStripMenuItem
      // 
      this.zoom11ToolStripMenuItem.Name = "zoom11ToolStripMenuItem";
      this.zoom11ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.zoom11ToolStripMenuItem.Text = "Zoom 1:1";
      // 
      // fitContentToolStripMenuItem
      // 
      this.fitContentToolStripMenuItem.Name = "fitContentToolStripMenuItem";
      this.fitContentToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.fitContentToolStripMenuItem.Text = "Fit Content";
      // 
      // groupingToolStripMenuItem
      // 
      this.groupingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupSelectionToolStripMenuItem,
            this.ungroupSelectionToolStripMenuItem,
            this.toolStripSeparator8,
            this.expandGroupToolStripMenuItem,
            this.collapseGroupToolStripMenuItem,
            this.toolStripSeparator9,
            this.enterGroupToolStripMenuItem,
            this.exitGroupToolStripMenuItem});
      this.groupingToolStripMenuItem.Name = "groupingToolStripMenuItem";
      this.groupingToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
      this.groupingToolStripMenuItem.Text = "Grouping";
      // 
      // groupSelectionToolStripMenuItem
      // 
      this.groupSelectionToolStripMenuItem.Name = "groupSelectionToolStripMenuItem";
      this.groupSelectionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.groupSelectionToolStripMenuItem.Text = "Group Selection";
      // 
      // ungroupSelectionToolStripMenuItem
      // 
      this.ungroupSelectionToolStripMenuItem.Name = "ungroupSelectionToolStripMenuItem";
      this.ungroupSelectionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.ungroupSelectionToolStripMenuItem.Text = "Ungroup Selection";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(169, 6);
      // 
      // expandGroupToolStripMenuItem
      // 
      this.expandGroupToolStripMenuItem.Name = "expandGroupToolStripMenuItem";
      this.expandGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.expandGroupToolStripMenuItem.Text = "Expand Group";
      // 
      // collapseGroupToolStripMenuItem
      // 
      this.collapseGroupToolStripMenuItem.Name = "collapseGroupToolStripMenuItem";
      this.collapseGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.collapseGroupToolStripMenuItem.Text = "Collapse Group";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(169, 6);
      // 
      // enterGroupToolStripMenuItem
      // 
      this.enterGroupToolStripMenuItem.Name = "enterGroupToolStripMenuItem";
      this.enterGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.enterGroupToolStripMenuItem.Text = "Enter Group";
      // 
      // exitGroupToolStripMenuItem
      // 
      this.exitGroupToolStripMenuItem.Name = "exitGroupToolStripMenuItem";
      this.exitGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.exitGroupToolStripMenuItem.Text = "Exit Group";
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.loadGraphMLButton,
            this.saveGraphMLButton,
            this.toolStripSeparator3,
            this.printButton,
            this.exportImageButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator4,
            this.cutButton,
            this.copyButton,
            this.pasteButton,
            this.deleteButton,
            this.toolStripSeparator2,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator11,
            this.groupButton,
            this.ungroupButton,
            this.toolStripSeparator12,
            this.snappingButton,
            this.orthogonalEditingButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 24);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(844, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // newButton
      // 
      this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.new_document_16;
      this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newButton.Name = "newButton";
      this.newButton.Size = new System.Drawing.Size(23, 20);
      this.newButton.Text = "&New";
      this.newButton.Click += new System.EventHandler(this.newButton_Click);
      // 
      // loadGraphMLButton
      // 
      this.loadGraphMLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.loadGraphMLButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.open_16;
      this.loadGraphMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.loadGraphMLButton.Name = "loadGraphMLButton";
      this.loadGraphMLButton.Size = new System.Drawing.Size(23, 20);
      this.loadGraphMLButton.Text = "Load GraphML";
      // 
      // saveGraphMLButton
      // 
      this.saveGraphMLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveGraphMLButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.save_16;
      this.saveGraphMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveGraphMLButton.Name = "saveGraphMLButton";
      this.saveGraphMLButton.Size = new System.Drawing.Size(23, 20);
      this.saveGraphMLButton.Text = "Save GraphML";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // printButton
      // 
      this.printButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.printButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.print_16;
      this.printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(23, 20);
      this.printButton.Text = "Print ...";
      // 
      // exportImageButton
      // 
      this.exportImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.exportImageButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.export_image_16;
      this.exportImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.exportImageButton.Name = "exportImageButton";
      this.exportImageButton.Size = new System.Drawing.Size(23, 20);
      this.exportImageButton.Text = "Export Image";
      this.exportImageButton.Click += new System.EventHandler(this.exportImageButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 20);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.redo_16;
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 20);
      this.redoButton.Text = "Redo";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
      // 
      // cutButton
      // 
      this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.cut2_16;
      this.cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutButton.Name = "cutButton";
      this.cutButton.Size = new System.Drawing.Size(23, 20);
      this.cutButton.Text = "C&ut";
      // 
      // copyButton
      // 
      this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.copy_16;
      this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyButton.Name = "copyButton";
      this.copyButton.Size = new System.Drawing.Size(23, 20);
      this.copyButton.Text = "&Copy";
      // 
      // pasteButton
      // 
      this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.paste_16;
      this.pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteButton.Name = "pasteButton";
      this.pasteButton.Size = new System.Drawing.Size(23, 20);
      this.pasteButton.Text = "&Paste";
      // 
      // deleteButton
      // 
      this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.deleteButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.delete2_16;
      this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new System.Drawing.Size(23, 20);
      this.deleteButton.Text = "Delete";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 23);
      // 
      // groupButton
      // 
      this.groupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.groupButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.group_16;
      this.groupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.groupButton.Name = "groupButton";
      this.groupButton.Size = new System.Drawing.Size(23, 20);
      this.groupButton.Text = "Group Selection";
      // 
      // ungroupButton
      // 
      this.ungroupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ungroupButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.ungroup_16;
      this.ungroupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ungroupButton.Name = "ungroupButton";
      this.ungroupButton.Size = new System.Drawing.Size(23, 20);
      this.ungroupButton.Text = "Ungroup Selection";
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(6, 23);
      // 
      // snappingButton
      // 
      this.snappingButton.CheckOnClick = true;
      this.snappingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.snappingButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.snap_16;
      this.snappingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.snappingButton.Name = "snappingButton";
      this.snappingButton.Size = new System.Drawing.Size(23, 20);
      this.snappingButton.Text = "Snapping";
      this.snappingButton.Click += new System.EventHandler(this.snappingButton_Click);
      // 
      // orthogonalEditingButton
      // 
      this.orthogonalEditingButton.CheckOnClick = true;
      this.orthogonalEditingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.orthogonalEditingButton.Image = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.orthogonal_editing_16;
      this.orthogonalEditingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.orthogonalEditingButton.Name = "orthogonalEditingButton";
      this.orthogonalEditingButton.Size = new System.Drawing.Size(23, 20);
      this.orthogonalEditingButton.Text = "Orthogonal Edges";
      this.orthogonalEditingButton.Click += new System.EventHandler(this.orthogonalEditingButton_Click);
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.graphOverviewControl);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(300, 237);
      this.panel1.TabIndex = 3;
      // 
      // EdgeToEdgeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(844, 583);
      this.Controls.Add(this.toolStripContainer);
      this.MainMenuStrip = this.menuStrip;
      this.Icon = global::Demo.yFiles.Graph.EdgeToEdge.Properties.Resources.yIcon;
      this.Name = "EdgeToEdgeForm";
      this.Text = "Edge To Edge Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton printButton;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton saveGraphMLButton;
    private System.Windows.Forms.ToolStripButton exportImageButton;
    private System.Windows.Forms.ToolStripButton loadGraphMLButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripButton newButton;
    private System.Windows.Forms.ToolStripButton cutButton;
    private System.Windows.Forms.ToolStripButton copyButton;
    private System.Windows.Forms.ToolStripButton pasteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.RichTextBox description;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoom11ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fitContentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem groupingToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem groupSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ungroupSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripMenuItem expandGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem collapseGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripMenuItem enterGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.ToolStripButton deleteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripButton groupButton;
    private System.Windows.Forms.ToolStripButton ungroupButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private System.Windows.Forms.ToolStripButton snappingButton;
    private System.Windows.Forms.ToolStripButton orthogonalEditingButton;
    private System.Windows.Forms.Panel panel1;
  }
}

