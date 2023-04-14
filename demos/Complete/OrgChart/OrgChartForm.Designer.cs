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

namespace Demo.yFiles.Graph.OrgChart
{
  partial class OrgChartForm : Form
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
      yWorks.Controls.ViewportLimiter viewportLimiter3 = new yWorks.Controls.ViewportLimiter();
      yWorks.Controls.ViewportLimiter viewportLimiter4 = new yWorks.Controls.ViewportLimiter();
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.zoomToCurrentItemButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.hideParentButton = new System.Windows.Forms.ToolStripButton();
      this.showParentButton = new System.Windows.Forms.ToolStripButton();
      this.hideChildrenButton = new System.Windows.Forms.ToolStripButton();
      this.showChildrenButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.showAllButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.StatusLabel = new System.Windows.Forms.Label();
      this.BusinessUnitLabel = new System.Windows.Forms.Label();
      this.EmailLabel = new System.Windows.Forms.Label();
      this.FaxLabel = new System.Windows.Forms.Label();
      this.PhoneLabel = new System.Windows.Forms.Label();
      this.PositionLabel = new System.Windows.Forms.Label();
      this.NameLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.structureTreeView = new System.Windows.Forms.TreeView();
      this.label9 = new System.Windows.Forms.Label();
      this.panel4 = new System.Windows.Forms.Panel();
      this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
      this.label8 = new System.Windows.Forms.Label();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel4.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(525, 624);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(525, 655);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(525, 624);
      this.graphControl.TabIndex = 2;
      this.graphControl.Text = "graphControl";
      this.graphControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      viewportLimiter3.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter3;
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.zoomToCurrentItemButton,
            this.toolStripSeparator11,
            this.hideParentButton,
            this.showParentButton,
            this.hideChildrenButton,
            this.showChildrenButton,
            this.toolStripSeparator1,
            this.showAllButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(525, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.OrgChart.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.OrgChart.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.OrgChart.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // zoomToCurrentItemButton
      // 
      this.zoomToCurrentItemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomToCurrentItemButton.Image = global::Demo.yFiles.Graph.OrgChart.Properties.Resources.usericon_female1_16;
      this.zoomToCurrentItemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomToCurrentItemButton.Name = "zoomToCurrentItemButton";
      this.zoomToCurrentItemButton.Size = new System.Drawing.Size(23, 20);
      this.zoomToCurrentItemButton.Text = "Zoom to current item";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 23);
      // 
      // hideParentButton
      // 
      this.hideParentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.hideParentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.hideParentButton.Name = "hideParentButton";
      this.hideParentButton.Size = new System.Drawing.Size(73, 20);
      this.hideParentButton.Text = "Hide Parent";
      // 
      // showParentButton
      // 
      this.showParentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.showParentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.showParentButton.Name = "showParentButton";
      this.showParentButton.Size = new System.Drawing.Size(77, 20);
      this.showParentButton.Text = "Show Parent";
      // 
      // hideChildrenButton
      // 
      this.hideChildrenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.hideChildrenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.hideChildrenButton.Name = "hideChildrenButton";
      this.hideChildrenButton.Size = new System.Drawing.Size(84, 20);
      this.hideChildrenButton.Text = "Hide Children";
      // 
      // showChildrenButton
      // 
      this.showChildrenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.showChildrenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.showChildrenButton.Name = "showChildrenButton";
      this.showChildrenButton.Size = new System.Drawing.Size(88, 20);
      this.showChildrenButton.Text = "Show Children";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // showAllButton
      // 
      this.showAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.showAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.showAllButton.Name = "showAllButton";
      this.showAllButton.Size = new System.Drawing.Size(57, 20);
      this.showAllButton.Text = "Show All";
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
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1068, 655);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 3;
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Controls.Add(this.panel3);
      this.panel1.Controls.Add(this.panel4);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(252, 655);
      this.panel1.TabIndex = 4;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.tableLayoutPanel1);
      this.panel3.Controls.Add(this.label10);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 484);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(248, 167);
      this.panel3.TabIndex = 6;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.14286F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.85714F));
      this.tableLayoutPanel1.Controls.Add(this.StatusLabel, 1, 6);
      this.tableLayoutPanel1.Controls.Add(this.BusinessUnitLabel, 1, 5);
      this.tableLayoutPanel1.Controls.Add(this.EmailLabel, 1, 4);
      this.tableLayoutPanel1.Controls.Add(this.FaxLabel, 1, 3);
      this.tableLayoutPanel1.Controls.Add(this.PhoneLabel, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.PositionLabel, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.NameLabel, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 23);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 8;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(248, 144);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // StatusLabel
      // 
      this.StatusLabel.AutoSize = true;
      this.StatusLabel.Location = new System.Drawing.Point(82, 120);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(10, 13);
      this.StatusLabel.TabIndex = 13;
      this.StatusLabel.Text = "-";
      // 
      // BusinessUnitLabel
      // 
      this.BusinessUnitLabel.AutoSize = true;
      this.BusinessUnitLabel.Location = new System.Drawing.Point(82, 100);
      this.BusinessUnitLabel.Name = "BusinessUnitLabel";
      this.BusinessUnitLabel.Size = new System.Drawing.Size(10, 13);
      this.BusinessUnitLabel.TabIndex = 12;
      this.BusinessUnitLabel.Text = "-";
      // 
      // EmailLabel
      // 
      this.EmailLabel.AutoSize = true;
      this.EmailLabel.Location = new System.Drawing.Point(82, 80);
      this.EmailLabel.Name = "EmailLabel";
      this.EmailLabel.Size = new System.Drawing.Size(10, 13);
      this.EmailLabel.TabIndex = 11;
      this.EmailLabel.Text = "-";
      // 
      // FaxLabel
      // 
      this.FaxLabel.AutoSize = true;
      this.FaxLabel.Location = new System.Drawing.Point(82, 60);
      this.FaxLabel.Name = "FaxLabel";
      this.FaxLabel.Size = new System.Drawing.Size(10, 13);
      this.FaxLabel.TabIndex = 10;
      this.FaxLabel.Text = "-";
      // 
      // PhoneLabel
      // 
      this.PhoneLabel.AutoSize = true;
      this.PhoneLabel.Location = new System.Drawing.Point(82, 40);
      this.PhoneLabel.Name = "PhoneLabel";
      this.PhoneLabel.Size = new System.Drawing.Size(10, 13);
      this.PhoneLabel.TabIndex = 9;
      this.PhoneLabel.Text = "-";
      // 
      // PositionLabel
      // 
      this.PositionLabel.AutoSize = true;
      this.PositionLabel.Location = new System.Drawing.Point(82, 20);
      this.PositionLabel.Name = "PositionLabel";
      this.PositionLabel.Size = new System.Drawing.Size(10, 13);
      this.PositionLabel.TabIndex = 8;
      this.PositionLabel.Text = "-";
      // 
      // NameLabel
      // 
      this.NameLabel.AutoSize = true;
      this.NameLabel.Location = new System.Drawing.Point(82, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(10, 13);
      this.NameLabel.TabIndex = 7;
      this.NameLabel.Text = "-";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Name";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 40);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(38, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Phone";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 20);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(44, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Position";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 60);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(24, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Fax";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(3, 80);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(32, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Email";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(3, 100);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(71, 13);
      this.label6.TabIndex = 5;
      this.label6.Text = "Business Unit";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(3, 120);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(37, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Status";
      // 
      // label10
      // 
      this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label10.Dock = System.Windows.Forms.DockStyle.Top;
      this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(0, 0);
      this.label10.Name = "label10";
      this.label10.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.label10.Size = new System.Drawing.Size(248, 23);
      this.label10.TabIndex = 7;
      this.label10.Text = "Details";
      this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.structureTreeView);
      this.panel2.Controls.Add(this.label9);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 161);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(248, 490);
      this.panel2.TabIndex = 7;
      // 
      // structureTreeView
      // 
      this.structureTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.structureTreeView.HideSelection = false;
      this.structureTreeView.Location = new System.Drawing.Point(0, 23);
      this.structureTreeView.Name = "structureTreeView";
      this.structureTreeView.Size = new System.Drawing.Size(248, 467);
      this.structureTreeView.TabIndex = 0;
      this.structureTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.structureTreeView_AfterSelect);
      this.structureTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.structureTreeView_KeyPress);
      this.structureTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.structureTreeView_MouseDoubleClick);
      // 
      // label9
      // 
      this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label9.Dock = System.Windows.Forms.DockStyle.Top;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(0, 0);
      this.label9.Name = "label9";
      this.label9.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.label9.Size = new System.Drawing.Size(248, 23);
      this.label9.TabIndex = 5;
      this.label9.Text = "Structure";
      this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.graphOverviewControl);
      this.panel4.Controls.Add(this.label8);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel4.Location = new System.Drawing.Point(0, 0);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(248, 161);
      this.panel4.TabIndex = 7;
      // 
      // graphOverviewControl
      // 
      this.graphOverviewControl.AutoDrag = false;
      this.graphOverviewControl.BackColor = System.Drawing.Color.White;
      this.graphOverviewControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphOverviewControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphOverviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphOverviewControl.Graph = null;
      this.graphOverviewControl.GraphControl = null;
      this.graphOverviewControl.GraphVisualCreator = null;
      this.graphOverviewControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      this.graphOverviewControl.Location = new System.Drawing.Point(0, 23);
      this.graphOverviewControl.Name = "graphOverviewControl";
      this.graphOverviewControl.Size = new System.Drawing.Size(248, 138);
      this.graphOverviewControl.TabIndex = 3;
      this.graphOverviewControl.Text = "graphOverviewControl";
      this.graphOverviewControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      viewportLimiter4.Bounds = null;
      this.graphOverviewControl.ViewportLimiter = viewportLimiter4;
      // 
      // label8
      // 
      this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label8.Dock = System.Windows.Forms.DockStyle.Top;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(0, 0);
      this.label8.Name = "label8";
      this.label8.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.label8.Size = new System.Drawing.Size(248, 23);
      this.label8.TabIndex = 4;
      this.label8.Text = "Overview";
      this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
      this.splitContainer2.Panel2.Controls.Add(this.description);
      this.splitContainer2.Size = new System.Drawing.Size(812, 655);
      this.splitContainer2.SplitterDistance = 525;
      this.splitContainer2.TabIndex = 0;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(283, 655);
      this.description.TabIndex = 3;
      this.description.Text = "";
      // 
      // OrgChartForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1068, 655);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.OrgChart.Properties.Resources.yIcon;
      this.Name = "OrgChartForm";
      this.Text = "OrgChart Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.SizeChanged += new System.EventHandler(this.InitialSizeChanged);
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private ToolStripButton hideParentButton;
    private ToolStripButton showParentButton;
    private ToolStripButton hideChildrenButton;
    private ToolStripButton showChildrenButton;
    private ToolStripButton zoomToCurrentItemButton;
    private SplitContainer splitContainer2;
    private yWorks.Controls.GraphControl graphControl;
    private RichTextBox description;
    private TableLayoutPanel tableLayoutPanel1;
    private Label StatusLabel;
    private Label BusinessUnitLabel;
    private Label EmailLabel;
    private Label FaxLabel;
    private Label PhoneLabel;
    private Label PositionLabel;
    private Label NameLabel;
    private Label label1;
    private Label label3;
    private Label label2;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton showAllButton;
    private TreeView structureTreeView;
    private Panel panel1;
    private Label label8;
    private Label label9;
    private Panel panel2;
    private Panel panel3;
    private Label label10;
    private Panel panel4;
  }
}

