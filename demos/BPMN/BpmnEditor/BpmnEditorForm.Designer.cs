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
using yWorks.Geometry;

namespace Demo.yFiles.Graph.Bpmn.Editor
{
  partial class BpmnEditorForm
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.nodeStyleListBox = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.panel2 = new System.Windows.Forms.Panel();
      this.editorControl = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.newButton = new System.Windows.Forms.ToolStripButton();
      this.openButton = new System.Windows.Forms.ToolStripButton();
      this.saveButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitToSizeButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.runButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.sampleGraphDiComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.sampleGraphComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fitToGraphBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.activitiesStyleListBox = new System.Windows.Forms.ListBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
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
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
      this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
      this.splitContainer1.Size = new System.Drawing.Size(1264, 775);
      this.splitContainer1.SplitterDistance = 300;
      this.splitContainer1.TabIndex = 0;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(300, 775);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 55);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer2.Size = new System.Drawing.Size(960, 720);
      this.splitContainer2.SplitterDistance = 175;
      this.splitContainer2.TabIndex = 2;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.nodeStyleListBox);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(175, 720);
      this.panel1.TabIndex = 1;
      // 
      // nodeStyleListBox
      // 
      this.nodeStyleListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nodeStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.nodeStyleListBox.FormattingEnabled = true;
      this.nodeStyleListBox.IntegralHeight = false;
      this.nodeStyleListBox.Location = new System.Drawing.Point(0, 27);
      this.nodeStyleListBox.Name = "nodeStyleListBox";
      this.nodeStyleListBox.Size = new System.Drawing.Size(175, 693);
      this.nodeStyleListBox.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.label1.Size = new System.Drawing.Size(175, 27);
      this.label1.TabIndex = 1;
      this.label1.Text = "BPMN Styles";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.graphControl);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.panel2);
      this.splitContainer3.Size = new System.Drawing.Size(781, 720);
      this.splitContainer3.SplitterDistance = 575;
      this.splitContainer3.TabIndex = 1;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(575, 720);
      this.graphControl.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.editorControl);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(202, 720);
      this.panel2.TabIndex = 0;
      // 
      // editorControl
      // 
      this.editorControl.BackColor = System.Drawing.SystemColors.Window;
      this.editorControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.editorControl.Location = new System.Drawing.Point(0, 27);
      this.editorControl.Name = "editorControl";
      this.editorControl.Size = new System.Drawing.Size(202, 693);
      this.editorControl.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label2.Dock = System.Windows.Forms.DockStyle.Top;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Name = "label2";
      this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.label2.Size = new System.Drawing.Size(202, 27);
      this.label2.TabIndex = 0;
      this.label2.Text = "Selection Properties";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.openButton,
            this.saveButton,
            this.toolStripSeparator6,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitToSizeButton,
            this.toolStripSeparator5,
            this.runButton,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.sampleGraphDiComboBox,
            this.toolStripLabel1,
            this.sampleGraphComboBox});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(960, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // newButton
      // 
      this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.new_document_16;
      this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newButton.Name = "newButton";
      this.newButton.Size = new System.Drawing.Size(23, 20);
      this.newButton.Text = "toolStripButton2";
      this.newButton.ToolTipText = "New document";
      // 
      // openButton
      // 
      this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.open_16;
      this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(23, 20);
      this.openButton.Text = "toolStripButton3";
      this.openButton.ToolTipText = "Open";
      // 
      // saveButton
      // 
      this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.save_16;
      this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(23, 20);
      this.saveButton.Text = "toolStripButton1";
      this.saveButton.ToolTipText = "Save As";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 23);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 20);
      this.undoButton.Text = "toolStripButton1";
      this.undoButton.ToolTipText = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.redo_16;
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 20);
      this.redoButton.Text = "toolStripButton2";
      this.redoButton.ToolTipText = "Redo";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "toolStripButton1";
      this.zoomInButton.ToolTipText = "Zoom in";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "toolStripButton1";
      this.zoomOutButton.ToolTipText = "Zoom out";
      // 
      // fitToSizeButton
      // 
      this.fitToSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitToSizeButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.fit_16;
      this.fitToSizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitToSizeButton.Name = "fitToSizeButton";
      this.fitToSizeButton.Size = new System.Drawing.Size(23, 20);
      this.fitToSizeButton.Text = "toolStripButton1";
      this.fitToSizeButton.ToolTipText = "Fit to graph bounds";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
      // 
      // runButton
      // 
      this.runButton.Image = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.reload_16;
      this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(63, 20);
      this.runButton.Text = "Layout";
      this.runButton.Click += new System.EventHandler(this.OnLayoutClick);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(114, 20);
      this.toolStripLabel2.Text = "Diagram (BPMN DI):";
      // 
      // sampleGraphDiComboBox
      // 
      this.sampleGraphDiComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.sampleGraphDiComboBox.Name = "sampleGraphDiComboBox";
      this.sampleGraphDiComboBox.Size = new System.Drawing.Size(175, 23);
      this.sampleGraphDiComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSampleDiGraphChanged);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(115, 20);
      this.toolStripLabel1.Text = "Diagram (GraphML):";
      // 
      // sampleGraphComboBox
      // 
      this.sampleGraphComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.sampleGraphComboBox.Name = "sampleGraphComboBox";
      this.sampleGraphComboBox.Size = new System.Drawing.Size(175, 23);
      this.sampleGraphComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSampleGraphChanged);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(960, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.newToolStripMenuItem.Text = "New";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.openToolStripMenuItem.Text = "Open";
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.saveAsToolStripMenuItem.Text = "Save as...";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(118, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitMenuItemClick);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.fitToGraphBoundsToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // zoomInToolStripMenuItem
      // 
      this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
      this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.zoomInToolStripMenuItem.Text = "Zoom in";
      // 
      // zoomOutToolStripMenuItem
      // 
      this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
      this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.zoomOutToolStripMenuItem.Text = "Zoom out";
      // 
      // fitToGraphBoundsToolStripMenuItem
      // 
      this.fitToGraphBoundsToolStripMenuItem.Name = "fitToGraphBoundsToolStripMenuItem";
      this.fitToGraphBoundsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.fitToGraphBoundsToolStripMenuItem.Text = "Fit to graph bounds";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(118, 6);
      // 
      // activitiesStyleListBox
      // 
      this.activitiesStyleListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.activitiesStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.activitiesStyleListBox.FormattingEnabled = true;
      this.activitiesStyleListBox.IntegralHeight = false;
      this.activitiesStyleListBox.Location = new System.Drawing.Point(0, 0);
      this.activitiesStyleListBox.MultiColumn = true;
      this.activitiesStyleListBox.Name = "activitiesStyleListBox";
      this.activitiesStyleListBox.Size = new System.Drawing.Size(175, 720);
      this.activitiesStyleListBox.TabIndex = 0;
      // 
      // BpmnEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1264, 775);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.Bpmn.Editor.Properties.Resources.yIcon;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "BpmnEditorForm";
      this.Text = "Demo.yFiles.Graph.Bpmn.Editor";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoaded);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitToSizeButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton runButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox sampleGraphComboBox;
    private System.Windows.Forms.ListBox nodeStyleListBox;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fitToGraphBoundsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private Panel panel1;
    private Label label1;
    private ToolStripButton undoButton;
    private ToolStripButton redoButton;
    private ToolStripSeparator toolStripSeparator5;
    private ListBox activitiesStyleListBox;
    private SplitContainer splitContainer3;
    private Panel panel2;
    private Label label2;
    private Panel editorControl;
    private ToolStripButton newButton;
    private ToolStripButton openButton;
    private ToolStripButton saveButton;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem newToolStripMenuItem;
    private ToolStripLabel toolStripLabel2;
    private ToolStripComboBox sampleGraphDiComboBox;
  }
}
