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

using yWorks.Geometry;

namespace Demo.yFiles.Graph.Events
{
  partial class GraphEventsForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEventsForm));
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
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
      this.loadGraphMLButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.toggleEditingButton = new System.Windows.Forms.ToolStripButton();
      this.orthogonalEditingButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.logGraphRenderEvents = new System.Windows.Forms.CheckBox();
      this.logNodeBoundsEvents = new System.Windows.Forms.CheckBox();
      this.logBendEvents = new System.Windows.Forms.CheckBox();
      this.logPortEvents = new System.Windows.Forms.CheckBox();
      this.logLabelEvents = new System.Windows.Forms.CheckBox();
      this.logEdgeEvents = new System.Windows.Forms.CheckBox();
      this.logNodeEvents = new System.Windows.Forms.CheckBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.logGraphControl = new System.Windows.Forms.CheckBox();
      this.logRenderEvents = new System.Windows.Forms.CheckBox();
      this.logViewportEvents = new System.Windows.Forms.CheckBox();
      this.logSelectionEvents = new System.Windows.Forms.CheckBox();
      this.logKeyEvents = new System.Windows.Forms.CheckBox();
      this.logMouseEvents = new System.Windows.Forms.CheckBox();
      this.logClipboard = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.logUndoEvents = new System.Windows.Forms.CheckBox();
      this.logClipboardEvents = new System.Windows.Forms.CheckBox();
      this.logMoveLabelModeEvents = new System.Windows.Forms.CheckBox();
      this.logItemHoverModeEvents = new System.Windows.Forms.CheckBox();
      this.logCreateEdgeModeEvents = new System.Windows.Forms.CheckBox();
      this.logCreateBendModeEvents = new System.Windows.Forms.CheckBox();
      this.logContextMenuModeEvents = new System.Windows.Forms.CheckBox();
      this.logTextEditorModeEvents = new System.Windows.Forms.CheckBox();
      this.logMouseHoverModeEvents = new System.Windows.Forms.CheckBox();
      this.logHandleModeEvents = new System.Windows.Forms.CheckBox();
      this.logMoveViewportModeEvents = new System.Windows.Forms.CheckBox();
      this.logMoveModeEvents = new System.Windows.Forms.CheckBox();
      this.logClickModeEvents = new System.Windows.Forms.CheckBox();
      this.logNavigationModeEvents = new System.Windows.Forms.CheckBox();
      this.logInputModeEvents = new System.Windows.Forms.CheckBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.messagePane = new System.Windows.Forms.ListBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.groupEvents = new System.Windows.Forms.CheckBox();
      this.clearLogButton = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.toolStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(602, 780);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(602, 835);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
      this.graphControl.Size = new System.Drawing.Size(602, 780);
      this.graphControl.TabIndex = 1;
      this.graphControl.Text = "graphControl";
      this.graphControl.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
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
      this.menuStrip.Size = new System.Drawing.Size(602, 24);
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
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.newToolStripMenuItem.Text = "New";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.openToolStripMenuItem.Text = "Open";
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.saveToolStripMenuItem.Text = "Save";
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(100, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
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
            this.loadGraphMLButton,
            this.toolStripSeparator3,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator12,
            this.toggleEditingButton,
            this.orthogonalEditingButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 24);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(602, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // loadGraphMLButton
      // 
      this.loadGraphMLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.loadGraphMLButton.Image = global::Demo.yFiles.Graph.Events.Properties.Resources.open_16;
      this.loadGraphMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.loadGraphMLButton.Name = "loadGraphMLButton";
      this.loadGraphMLButton.Size = new System.Drawing.Size(23, 20);
      this.loadGraphMLButton.Text = "Load GraphML";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.Events.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.Events.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.Events.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(6, 23);
      // 
      // toggleEditingButton
      // 
      this.toggleEditingButton.CheckOnClick = true;
      this.toggleEditingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toggleEditingButton.Image = ((System.Drawing.Image)(resources.GetObject("toggleEditingButton.Image")));
      this.toggleEditingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toggleEditingButton.Name = "toggleEditingButton";
      this.toggleEditingButton.Size = new System.Drawing.Size(87, 20);
      this.toggleEditingButton.Text = "Toggle Editing";
      this.toggleEditingButton.Click += new System.EventHandler(this.OnToggleEditingClicked);
      // 
      // orthogonalEditingButton
      // 
      this.orthogonalEditingButton.CheckOnClick = true;
      this.orthogonalEditingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.orthogonalEditingButton.Image = ((System.Drawing.Image)(resources.GetObject("orthogonalEditingButton.Image")));
      this.orthogonalEditingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.orthogonalEditingButton.Name = "orthogonalEditingButton";
      this.orthogonalEditingButton.Size = new System.Drawing.Size(112, 20);
      this.orthogonalEditingButton.Text = "Orthogonal Editing";
      this.orthogonalEditingButton.Click += new System.EventHandler(this.OnToggleOrthogonalEditingClicked);
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
      this.splitContainer1.Panel1MinSize = 180;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1176, 835);
      this.splitContainer1.SplitterDistance = 220;
      this.splitContainer1.TabIndex = 3;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 666);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(220, 169);
      this.description.TabIndex = 2;
      this.description.Text = "";
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.groupBox3);
      this.panel1.Controls.Add(this.groupBox2);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(220, 666);
      this.panel1.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(-2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(218, 27);
      this.label1.TabIndex = 13;
      this.label1.Text = "Event Log Options";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.logGraphRenderEvents);
      this.groupBox3.Controls.Add(this.logNodeBoundsEvents);
      this.groupBox3.Controls.Add(this.logBendEvents);
      this.groupBox3.Controls.Add(this.logPortEvents);
      this.groupBox3.Controls.Add(this.logLabelEvents);
      this.groupBox3.Controls.Add(this.logEdgeEvents);
      this.groupBox3.Controls.Add(this.logNodeEvents);
      this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox3.Location = new System.Drawing.Point(6, 497);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(210, 153);
      this.groupBox3.TabIndex = 7;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Graph Events";
      // 
      // logGraphRenderEvents
      // 
      this.logGraphRenderEvents.AutoSize = true;
      this.logGraphRenderEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logGraphRenderEvents.Location = new System.Drawing.Point(8, 130);
      this.logGraphRenderEvents.Name = "logGraphRenderEvents";
      this.logGraphRenderEvents.Size = new System.Drawing.Size(129, 17);
      this.logGraphRenderEvents.TabIndex = 6;
      this.logGraphRenderEvents.Text = "Graph Render Events";
      this.toolTip1.SetToolTip(this.logGraphRenderEvents, "Reports events that occur when the graph is rendered within a control.");
      this.logGraphRenderEvents.UseVisualStyleBackColor = true;
      this.logGraphRenderEvents.CheckedChanged += new System.EventHandler(this.OnLogGraphRenderEventsClicked);
      // 
      // logNodeBoundsEvents
      // 
      this.logNodeBoundsEvents.AutoSize = true;
      this.logNodeBoundsEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logNodeBoundsEvents.Location = new System.Drawing.Point(8, 112);
      this.logNodeBoundsEvents.Name = "logNodeBoundsEvents";
      this.logNodeBoundsEvents.Size = new System.Drawing.Size(164, 17);
      this.logNodeBoundsEvents.TabIndex = 5;
      this.logNodeBoundsEvents.Text = "Node Layout Changed Event";
      this.toolTip1.SetToolTip(this.logNodeBoundsEvents, "Dispatched when the layout of a node have changed.");
      this.logNodeBoundsEvents.UseVisualStyleBackColor = true;
      this.logNodeBoundsEvents.CheckedChanged += new System.EventHandler(this.OnLogNodeBoundsEventsClicked);
      // 
      // logBendEvents
      // 
      this.logBendEvents.AutoSize = true;
      this.logBendEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logBendEvents.Location = new System.Drawing.Point(8, 94);
      this.logBendEvents.Name = "logBendEvents";
      this.logBendEvents.Size = new System.Drawing.Size(87, 17);
      this.logBendEvents.TabIndex = 4;
      this.logBendEvents.Text = "Bend Events";
      this.toolTip1.SetToolTip(this.logBendEvents, "Dispatched when a bend is created, removed, or changed.");
      this.logBendEvents.UseVisualStyleBackColor = true;
      this.logBendEvents.CheckedChanged += new System.EventHandler(this.OnLogBendEventsClicked);
      // 
      // logPortEvents
      // 
      this.logPortEvents.AutoSize = true;
      this.logPortEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logPortEvents.Location = new System.Drawing.Point(8, 76);
      this.logPortEvents.Name = "logPortEvents";
      this.logPortEvents.Size = new System.Drawing.Size(81, 17);
      this.logPortEvents.TabIndex = 3;
      this.logPortEvents.Text = "Port Events";
      this.toolTip1.SetToolTip(this.logPortEvents, "Dispatched when a port is created, removed, or changed.");
      this.logPortEvents.UseVisualStyleBackColor = true;
      this.logPortEvents.CheckedChanged += new System.EventHandler(this.OnLogPortEventsClicked);
      // 
      // logLabelEvents
      // 
      this.logLabelEvents.AutoSize = true;
      this.logLabelEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logLabelEvents.Location = new System.Drawing.Point(8, 58);
      this.logLabelEvents.Name = "logLabelEvents";
      this.logLabelEvents.Size = new System.Drawing.Size(88, 17);
      this.logLabelEvents.TabIndex = 2;
      this.logLabelEvents.Text = "Label Events";
      this.toolTip1.SetToolTip(this.logLabelEvents, "Dispatched when a label is created, removed, or changed.");
      this.logLabelEvents.UseVisualStyleBackColor = true;
      this.logLabelEvents.CheckedChanged += new System.EventHandler(this.OnLogLabelEventsClicked);
      // 
      // logEdgeEvents
      // 
      this.logEdgeEvents.AutoSize = true;
      this.logEdgeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logEdgeEvents.Location = new System.Drawing.Point(8, 40);
      this.logEdgeEvents.Name = "logEdgeEvents";
      this.logEdgeEvents.Size = new System.Drawing.Size(87, 17);
      this.logEdgeEvents.TabIndex = 1;
      this.logEdgeEvents.Text = "Edge Events";
      this.toolTip1.SetToolTip(this.logEdgeEvents, "Dispatched when an edge is created, removed, or changed.");
      this.logEdgeEvents.UseVisualStyleBackColor = true;
      this.logEdgeEvents.CheckedChanged += new System.EventHandler(this.OnLogEdgeEventsClicked);
      // 
      // logNodeEvents
      // 
      this.logNodeEvents.AutoSize = true;
      this.logNodeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logNodeEvents.Location = new System.Drawing.Point(8, 22);
      this.logNodeEvents.Name = "logNodeEvents";
      this.logNodeEvents.Size = new System.Drawing.Size(88, 17);
      this.logNodeEvents.TabIndex = 0;
      this.logNodeEvents.Text = "Node Events";
      this.toolTip1.SetToolTip(this.logNodeEvents, "Dispatched when a node is created, removed, or changed.");
      this.logNodeEvents.UseVisualStyleBackColor = true;
      this.logNodeEvents.CheckedChanged += new System.EventHandler(this.OnLogNodeEventsClicked);
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.logGraphControl);
      this.groupBox2.Controls.Add(this.logRenderEvents);
      this.groupBox2.Controls.Add(this.logViewportEvents);
      this.groupBox2.Controls.Add(this.logSelectionEvents);
      this.groupBox2.Controls.Add(this.logKeyEvents);
      this.groupBox2.Controls.Add(this.logMouseEvents);
      this.groupBox2.Controls.Add(this.logClipboard);
      this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox2.Location = new System.Drawing.Point(1, 338);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(210, 153);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "GraphControl Events";
      // 
      // logGraphControl
      // 
      this.logGraphControl.AutoSize = true;
      this.logGraphControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logGraphControl.Location = new System.Drawing.Point(8, 130);
      this.logGraphControl.Name = "logGraphControl";
      this.logGraphControl.Size = new System.Drawing.Size(88, 17);
      this.logGraphControl.TabIndex = 6;
      this.logGraphControl.Text = "Other Events";
      this.toolTip1.SetToolTip(this.logGraphControl, "Logs changes of the Current Item, the complete graph or the input mode.");
      this.logGraphControl.UseVisualStyleBackColor = true;
      this.logGraphControl.CheckedChanged += new System.EventHandler(this.OnLogGraphControlClicked);
      // 
      // logRenderEvents
      // 
      this.logRenderEvents.AutoSize = true;
      this.logRenderEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logRenderEvents.Location = new System.Drawing.Point(8, 112);
      this.logRenderEvents.Name = "logRenderEvents";
      this.logRenderEvents.Size = new System.Drawing.Size(97, 17);
      this.logRenderEvents.TabIndex = 5;
      this.logRenderEvents.Text = "Render Events";
      this.toolTip1.SetToolTip(this.logRenderEvents, "Dispatched when the GraphControl is rendered.");
      this.logRenderEvents.UseVisualStyleBackColor = true;
      this.logRenderEvents.CheckedChanged += new System.EventHandler(this.OnLogRenderEventsClicked);
      // 
      // logViewportEvents
      // 
      this.logViewportEvents.AutoSize = true;
      this.logViewportEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logViewportEvents.Location = new System.Drawing.Point(8, 94);
      this.logViewportEvents.Name = "logViewportEvents";
      this.logViewportEvents.Size = new System.Drawing.Size(103, 17);
      this.logViewportEvents.TabIndex = 4;
      this.logViewportEvents.Text = "Viewport Events";
      this.toolTip1.SetToolTip(this.logViewportEvents, "Report changes of the view port and zoom level.");
      this.logViewportEvents.UseVisualStyleBackColor = true;
      this.logViewportEvents.CheckedChanged += new System.EventHandler(this.OnLogViewportEventsClicked);
      // 
      // logSelectionEvents
      // 
      this.logSelectionEvents.AutoSize = true;
      this.logSelectionEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logSelectionEvents.Location = new System.Drawing.Point(8, 76);
      this.logSelectionEvents.Name = "logSelectionEvents";
      this.logSelectionEvents.Size = new System.Drawing.Size(106, 17);
      this.logSelectionEvents.TabIndex = 3;
      this.logSelectionEvents.Text = "Selection Events";
      this.toolTip1.SetToolTip(this.logSelectionEvents, "Dispatched by GraphControl.Selection when graph items are selected or deselected." +
        "");
      this.logSelectionEvents.UseVisualStyleBackColor = true;
      this.logSelectionEvents.CheckedChanged += new System.EventHandler(this.OnLogSelectionEventsClicked);
      // 
      // logKeyEvents
      // 
      this.logKeyEvents.AutoSize = true;
      this.logKeyEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logKeyEvents.Location = new System.Drawing.Point(8, 58);
      this.logKeyEvents.Name = "logKeyEvents";
      this.logKeyEvents.Size = new System.Drawing.Size(80, 17);
      this.logKeyEvents.TabIndex = 2;
      this.logKeyEvents.Text = "Key Events";
      this.toolTip1.SetToolTip(this.logKeyEvents, "Dispatched when a key is pressed or released.");
      this.logKeyEvents.UseVisualStyleBackColor = true;
      this.logKeyEvents.CheckedChanged += new System.EventHandler(this.OnLogKeyEventsClicked);
      // 
      // logMouseEvents
      // 
      this.logMouseEvents.AutoSize = true;
      this.logMouseEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logMouseEvents.Location = new System.Drawing.Point(8, 40);
      this.logMouseEvents.Name = "logMouseEvents";
      this.logMouseEvents.Size = new System.Drawing.Size(94, 17);
      this.logMouseEvents.TabIndex = 1;
      this.logMouseEvents.Text = "Mouse Events";
      this.toolTip1.SetToolTip(this.logMouseEvents, "Dispatched when the mouse is moved or mouse buttons are pressed.");
      this.logMouseEvents.UseVisualStyleBackColor = true;
      this.logMouseEvents.CheckedChanged += new System.EventHandler(this.OnLogMouseEventsClicked);
      // 
      // logClipboard
      // 
      this.logClipboard.AutoSize = true;
      this.logClipboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logClipboard.Location = new System.Drawing.Point(8, 22);
      this.logClipboard.Name = "logClipboard";
      this.logClipboard.Size = new System.Drawing.Size(106, 17);
      this.logClipboard.TabIndex = 0;
      this.logClipboard.Text = "Clipboard Events";
      this.toolTip1.SetToolTip(this.logClipboard, "Logs actions in the clipboard and during duplication");
      this.logClipboard.UseVisualStyleBackColor = true;
      this.logClipboard.CheckedChanged += new System.EventHandler(this.OnLogClipboardCopierEventsClicked);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.logUndoEvents);
      this.groupBox1.Controls.Add(this.logClipboardEvents);
      this.groupBox1.Controls.Add(this.logMoveLabelModeEvents);
      this.groupBox1.Controls.Add(this.logItemHoverModeEvents);
      this.groupBox1.Controls.Add(this.logCreateEdgeModeEvents);
      this.groupBox1.Controls.Add(this.logCreateBendModeEvents);
      this.groupBox1.Controls.Add(this.logContextMenuModeEvents);
      this.groupBox1.Controls.Add(this.logTextEditorModeEvents);
      this.groupBox1.Controls.Add(this.logMouseHoverModeEvents);
      this.groupBox1.Controls.Add(this.logHandleModeEvents);
      this.groupBox1.Controls.Add(this.logMoveViewportModeEvents);
      this.groupBox1.Controls.Add(this.logMoveModeEvents);
      this.groupBox1.Controls.Add(this.logClickModeEvents);
      this.groupBox1.Controls.Add(this.logNavigationModeEvents);
      this.groupBox1.Controls.Add(this.logInputModeEvents);
      this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(5, 30);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(210, 302);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Input Mode Events";
      // 
      // logUndoEvents
      // 
      this.logUndoEvents.AutoSize = true;
      this.logUndoEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logUndoEvents.Location = new System.Drawing.Point(8, 274);
      this.logUndoEvents.Name = "logUndoEvents";
      this.logUndoEvents.Size = new System.Drawing.Size(88, 17);
      this.logUndoEvents.TabIndex = 14;
      this.logUndoEvents.Text = "Undo Events";
      this.toolTip1.SetToolTip(this.logUndoEvents, "Events dispatched by GraphEditorInputMode when an operation was undone or redone." +
        "");
      this.logUndoEvents.UseVisualStyleBackColor = true;
      this.logUndoEvents.CheckedChanged += new System.EventHandler(this.OnLogUndoEventsClicked);
      // 
      // logClipboardEvents
      // 
      this.logClipboardEvents.AutoSize = true;
      this.logClipboardEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logClipboardEvents.Location = new System.Drawing.Point(8, 256);
      this.logClipboardEvents.Name = "logClipboardEvents";
      this.logClipboardEvents.Size = new System.Drawing.Size(106, 17);
      this.logClipboardEvents.TabIndex = 13;
      this.logClipboardEvents.Text = "Clipboard Events";
      this.toolTip1.SetToolTip(this.logClipboardEvents, "Events dispatched by GraphEditorInputMode upon clipboard operations.");
      this.logClipboardEvents.UseVisualStyleBackColor = true;
      this.logClipboardEvents.CheckedChanged += new System.EventHandler(this.OnLogClipboardEventsClicked);
      // 
      // logMoveLabelModeEvents
      // 
      this.logMoveLabelModeEvents.AutoSize = true;
      this.logMoveLabelModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logMoveLabelModeEvents.Location = new System.Drawing.Point(8, 238);
      this.logMoveLabelModeEvents.Name = "logMoveLabelModeEvents";
      this.logMoveLabelModeEvents.Size = new System.Drawing.Size(118, 17);
      this.logMoveLabelModeEvents.TabIndex = 12;
      this.logMoveLabelModeEvents.Text = "Move Label Events";
      this.toolTip1.SetToolTip(this.logMoveLabelModeEvents, "Events dispatched by MoveLabelInputMode when a label was moved.");
      this.logMoveLabelModeEvents.UseVisualStyleBackColor = true;
      this.logMoveLabelModeEvents.CheckedChanged += new System.EventHandler(this.OnLogMoveLabelModeEventsClicked);
      // 
      // logItemHoverModeEvents
      // 
      this.logItemHoverModeEvents.AutoSize = true;
      this.logItemHoverModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logItemHoverModeEvents.Location = new System.Drawing.Point(8, 220);
      this.logItemHoverModeEvents.Name = "logItemHoverModeEvents";
      this.logItemHoverModeEvents.Size = new System.Drawing.Size(114, 17);
      this.logItemHoverModeEvents.TabIndex = 11;
      this.logItemHoverModeEvents.Text = "Item Hover Events";
      this.toolTip1.SetToolTip(this.logItemHoverModeEvents, "Events dispatched by ItemHoverInputMode when the mouse enters or leaves an item.");
      this.logItemHoverModeEvents.UseVisualStyleBackColor = true;
      this.logItemHoverModeEvents.CheckedChanged += new System.EventHandler(this.OnLogItemHoverModeEventsClicked);
      // 
      // logCreateEdgeModeEvents
      // 
      this.logCreateEdgeModeEvents.AutoSize = true;
      this.logCreateEdgeModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logCreateEdgeModeEvents.Location = new System.Drawing.Point(8, 202);
      this.logCreateEdgeModeEvents.Name = "logCreateEdgeModeEvents";
      this.logCreateEdgeModeEvents.Size = new System.Drawing.Size(121, 17);
      this.logCreateEdgeModeEvents.TabIndex = 10;
      this.logCreateEdgeModeEvents.Text = "Create Edge Events";
      this.toolTip1.SetToolTip(this.logCreateEdgeModeEvents, "Events dispatched by CreateEdgeInputMode during edge creation.");
      this.logCreateEdgeModeEvents.UseVisualStyleBackColor = true;
      this.logCreateEdgeModeEvents.CheckedChanged += new System.EventHandler(this.OnLogCreateEdgeModeEventsClicked);
      // 
      // logCreateBendModeEvents
      // 
      this.logCreateBendModeEvents.AutoSize = true;
      this.logCreateBendModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logCreateBendModeEvents.Location = new System.Drawing.Point(8, 184);
      this.logCreateBendModeEvents.Name = "logCreateBendModeEvents";
      this.logCreateBendModeEvents.Size = new System.Drawing.Size(121, 17);
      this.logCreateBendModeEvents.TabIndex = 9;
      this.logCreateBendModeEvents.Text = "Create Bend Events";
      this.toolTip1.SetToolTip(this.logCreateBendModeEvents, "Events dispatched by CreateBendInputMode during bend creation.");
      this.logCreateBendModeEvents.UseVisualStyleBackColor = true;
      this.logCreateBendModeEvents.CheckedChanged += new System.EventHandler(this.OnLogCreateBendModeEventsClicked);
      // 
      // logContextMenuModeEvents
      // 
      this.logContextMenuModeEvents.AutoSize = true;
      this.logContextMenuModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logContextMenuModeEvents.Location = new System.Drawing.Point(8, 166);
      this.logContextMenuModeEvents.Name = "logContextMenuModeEvents";
      this.logContextMenuModeEvents.Size = new System.Drawing.Size(128, 17);
      this.logContextMenuModeEvents.TabIndex = 8;
      this.logContextMenuModeEvents.Text = "Context Menu Events";
      this.toolTip1.SetToolTip(this.logContextMenuModeEvents, "Events dispatched by ContextMenuInputMode.");
      this.logContextMenuModeEvents.UseVisualStyleBackColor = true;
      this.logContextMenuModeEvents.CheckedChanged += new System.EventHandler(this.OnLogContextMenuModeEventsClicked);
      // 
      // logTextEditorModeEvents
      // 
      this.logTextEditorModeEvents.AutoSize = true;
      this.logTextEditorModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logTextEditorModeEvents.Location = new System.Drawing.Point(8, 148);
      this.logTextEditorModeEvents.Name = "logTextEditorModeEvents";
      this.logTextEditorModeEvents.Size = new System.Drawing.Size(113, 17);
      this.logTextEditorModeEvents.TabIndex = 7;
      this.logTextEditorModeEvents.Text = "Text Editor Events";
      this.toolTip1.SetToolTip(this.logTextEditorModeEvents, "Events dispatched by TextEditorInputMode during Label Editing.");
      this.logTextEditorModeEvents.UseVisualStyleBackColor = true;
      this.logTextEditorModeEvents.CheckedChanged += new System.EventHandler(this.OnLogTextEditorModeEventsClicked);
      // 
      // logMouseHoverModeEvents
      // 
      this.logMouseHoverModeEvents.AutoSize = true;
      this.logMouseHoverModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logMouseHoverModeEvents.Location = new System.Drawing.Point(8, 130);
      this.logMouseHoverModeEvents.Name = "logMouseHoverModeEvents";
      this.logMouseHoverModeEvents.Size = new System.Drawing.Size(126, 17);
      this.logMouseHoverModeEvents.TabIndex = 6;
      this.logMouseHoverModeEvents.Text = "Mouse Hover Events";
      this.toolTip1.SetToolTip(this.logMouseHoverModeEvents, "Events dispatched by MouseHoverInputMode when an item was hovered for a certain a" +
        "mount of time (e.g. to show a ToolTip).");
      this.logMouseHoverModeEvents.UseVisualStyleBackColor = true;
      this.logMouseHoverModeEvents.CheckedChanged += new System.EventHandler(this.OnLogMouseHoverModeEventsClicked);
      // 
      // logHandleModeEvents
      // 
      this.logHandleModeEvents.AutoSize = true;
      this.logHandleModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logHandleModeEvents.Location = new System.Drawing.Point(8, 112);
      this.logHandleModeEvents.Name = "logHandleModeEvents";
      this.logHandleModeEvents.Size = new System.Drawing.Size(126, 17);
      this.logHandleModeEvents.TabIndex = 5;
      this.logHandleModeEvents.Text = "Handle Move Events";
      this.toolTip1.SetToolTip(this.logHandleModeEvents, "Events dispatched by HandleInputMode when a node was resized or another handle wa" +
        "s moved.");
      this.logHandleModeEvents.UseVisualStyleBackColor = true;
      this.logHandleModeEvents.CheckedChanged += new System.EventHandler(this.OnLogHandleModeEventsClicked);
      // 
      // logMoveViewportModeEvents
      // 
      this.logMoveViewportModeEvents.AutoSize = true;
      this.logMoveViewportModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logMoveViewportModeEvents.Location = new System.Drawing.Point(8, 94);
      this.logMoveViewportModeEvents.Name = "logMoveViewportModeEvents";
      this.logMoveViewportModeEvents.Size = new System.Drawing.Size(133, 17);
      this.logMoveViewportModeEvents.TabIndex = 4;
      this.logMoveViewportModeEvents.Text = "Move Viewport Events";
      this.toolTip1.SetToolTip(this.logMoveViewportModeEvents, "Events dispatched by MoveViewportInputMode when the graph was panned or zoomed.");
      this.logMoveViewportModeEvents.UseVisualStyleBackColor = true;
      this.logMoveViewportModeEvents.CheckedChanged += new System.EventHandler(this.OnLogMoveViewportModeEventsClicked);
      // 
      // logMoveModeEvents
      // 
      this.logMoveModeEvents.AutoSize = true;
      this.logMoveModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logMoveModeEvents.Location = new System.Drawing.Point(8, 76);
      this.logMoveModeEvents.Name = "logMoveModeEvents";
      this.logMoveModeEvents.Size = new System.Drawing.Size(89, 17);
      this.logMoveModeEvents.TabIndex = 3;
      this.logMoveModeEvents.Text = "Move Events";
      this.toolTip1.SetToolTip(this.logMoveModeEvents, "Events dispatched by MoveInputMode when an item was moved.");
      this.logMoveModeEvents.UseVisualStyleBackColor = true;
      this.logMoveModeEvents.CheckedChanged += new System.EventHandler(this.OnLogMoveModeEventsClicked);
      // 
      // logClickModeEvents
      // 
      this.logClickModeEvents.AutoSize = true;
      this.logClickModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logClickModeEvents.Location = new System.Drawing.Point(8, 58);
      this.logClickModeEvents.Name = "logClickModeEvents";
      this.logClickModeEvents.Size = new System.Drawing.Size(85, 17);
      this.logClickModeEvents.TabIndex = 2;
      this.logClickModeEvents.Text = "Click Events";
      this.toolTip1.SetToolTip(this.logClickModeEvents, "Events dispatched by ClickInputMode.");
      this.logClickModeEvents.UseVisualStyleBackColor = true;
      this.logClickModeEvents.CheckedChanged += new System.EventHandler(this.OnLogClickModeEventsClicked);
      // 
      // logNavigationModeEvents
      // 
      this.logNavigationModeEvents.AutoSize = true;
      this.logNavigationModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logNavigationModeEvents.Location = new System.Drawing.Point(8, 40);
      this.logNavigationModeEvents.Name = "logNavigationModeEvents";
      this.logNavigationModeEvents.Size = new System.Drawing.Size(113, 17);
      this.logNavigationModeEvents.TabIndex = 1;
      this.logNavigationModeEvents.Text = "Navigation Events";
      this.toolTip1.SetToolTip(this.logNavigationModeEvents, "Events dispatched by NavigationInputMode when a group node was collapsed, expande" +
        "d, entered or exited.");
      this.logNavigationModeEvents.UseVisualStyleBackColor = true;
      this.logNavigationModeEvents.CheckedChanged += new System.EventHandler(this.OnLogNavigationModeEventsClicked);
      // 
      // logInputModeEvents
      // 
      this.logInputModeEvents.AutoSize = true;
      this.logInputModeEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logInputModeEvents.Location = new System.Drawing.Point(8, 22);
      this.logInputModeEvents.Name = "logInputModeEvents";
      this.logInputModeEvents.Size = new System.Drawing.Size(126, 17);
      this.logInputModeEvents.TabIndex = 0;
      this.logInputModeEvents.Text = "Viewer/Editor Events";
      this.toolTip1.SetToolTip(this.logInputModeEvents, "Events dispatched by GraphViewerInputMode or GraphEditorInputMode.");
      this.logInputModeEvents.UseVisualStyleBackColor = true;
      this.logInputModeEvents.CheckedChanged += new System.EventHandler(this.OnLogInputModeEventsClicked);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.toolStripContainer);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.messagePane);
      this.splitContainer2.Panel2.Controls.Add(this.panel2);
      this.splitContainer2.Size = new System.Drawing.Size(952, 835);
      this.splitContainer2.SplitterDistance = 602;
      this.splitContainer2.TabIndex = 2;
      // 
      // messagePane
      // 
      this.messagePane.Dock = System.Windows.Forms.DockStyle.Fill;
      this.messagePane.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
      this.messagePane.FormattingEnabled = true;
      this.messagePane.ItemHeight = 50;
      this.messagePane.Location = new System.Drawing.Point(0, 42);
      this.messagePane.Name = "messagePane";
      this.messagePane.SelectionMode = System.Windows.Forms.SelectionMode.None;
      this.messagePane.Size = new System.Drawing.Size(346, 793);
      this.messagePane.TabIndex = 2;
      this.messagePane.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.MessagePaneDrawItem);
      this.messagePane.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.MessagePaneMeasureItem);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label2);
      this.panel2.Controls.Add(this.groupEvents);
      this.panel2.Controls.Add(this.clearLogButton);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(346, 42);
      this.panel2.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(3, 12);
      this.label2.Margin = new System.Windows.Forms.Padding(3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 16);
      this.label2.TabIndex = 0;
      this.label2.Text = "Event Log";
      // 
      // groupEvents
      // 
      this.groupEvents.AutoSize = true;
      this.groupEvents.Checked = true;
      this.groupEvents.CheckState = System.Windows.Forms.CheckState.Checked;
      this.groupEvents.Dock = System.Windows.Forms.DockStyle.Right;
      this.groupEvents.Location = new System.Drawing.Point(130, 12);
      this.groupEvents.Name = "groupEvents";
      this.groupEvents.Size = new System.Drawing.Size(132, 17);
      this.groupEvents.TabIndex = 1;
      this.groupEvents.Text = "Group identical events";
      this.groupEvents.UseVisualStyleBackColor = true;
      // 
      // clearLogButton
      // 
      this.clearLogButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.clearLogButton.Location = new System.Drawing.Point(268, 9);
      this.clearLogButton.Name = "clearLogButton";
      this.clearLogButton.Size = new System.Drawing.Size(75, 23);
      this.clearLogButton.TabIndex = 2;
      this.clearLogButton.Text = "Clear";
      this.clearLogButton.UseVisualStyleBackColor = true;
      this.clearLogButton.Click += new System.EventHandler(this.ClearButtonClick);
      // 
      // GraphEventsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1176, 835);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.Events.Properties.Resources.yIcon;
      this.MainMenuStrip = this.menuStrip;
      this.Name = "GraphEventsForm";
      this.Text = "Graph Events Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
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
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton loadGraphMLButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.RichTextBox description;
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
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.ToolStripButton orthogonalEditingButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox logContextMenuModeEvents;
    private System.Windows.Forms.CheckBox logTextEditorModeEvents;
    private System.Windows.Forms.CheckBox logMouseHoverModeEvents;
    private System.Windows.Forms.CheckBox logHandleModeEvents;
    private System.Windows.Forms.CheckBox logMoveViewportModeEvents;
    private System.Windows.Forms.CheckBox logMoveModeEvents;
    private System.Windows.Forms.CheckBox logClickModeEvents;
    private System.Windows.Forms.CheckBox logNavigationModeEvents;
    private System.Windows.Forms.CheckBox logInputModeEvents;
    private System.Windows.Forms.CheckBox logUndoEvents;
    private System.Windows.Forms.CheckBox logClipboardEvents;
    private System.Windows.Forms.CheckBox logMoveLabelModeEvents;
    private System.Windows.Forms.CheckBox logItemHoverModeEvents;
    private System.Windows.Forms.CheckBox logCreateEdgeModeEvents;
    private System.Windows.Forms.CheckBox logCreateBendModeEvents;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox logGraphControl;
    private System.Windows.Forms.CheckBox logRenderEvents;
    private System.Windows.Forms.CheckBox logViewportEvents;
    private System.Windows.Forms.CheckBox logSelectionEvents;
    private System.Windows.Forms.CheckBox logKeyEvents;
    private System.Windows.Forms.CheckBox logMouseEvents;
    private System.Windows.Forms.CheckBox logClipboard;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.CheckBox logGraphRenderEvents;
    private System.Windows.Forms.CheckBox logNodeBoundsEvents;
    private System.Windows.Forms.CheckBox logBendEvents;
    private System.Windows.Forms.CheckBox logPortEvents;
    private System.Windows.Forms.CheckBox logLabelEvents;
    private System.Windows.Forms.CheckBox logEdgeEvents;
    private System.Windows.Forms.CheckBox logNodeEvents;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox groupEvents;
    private System.Windows.Forms.Button clearLogButton;
    private System.Windows.Forms.ListBox messagePane;
    private System.Windows.Forms.ToolStripButton toggleEditingButton;
  }
}

