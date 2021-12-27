/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.4.
 ** Copyright (c) 2000-2021 by yWorks GmbH, Vor dem Kreuzberg 28,
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

namespace Demo.yFiles.Algorithms.GraphAnalysis
{
  partial class GraphAnalysisForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphAnalysisForm));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.textboxPanel = new System.Windows.Forms.Panel();
      this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.parallelEdgesLabel = new System.Windows.Forms.Label();
      this.selfLoopsLabel = new System.Windows.Forms.Label();
      this.treeLabel = new System.Windows.Forms.Label();
      this.planarLabel = new System.Windows.Forms.Label();
      this.stronglyConnectedLabel = new System.Windows.Forms.Label();
      this.biconnectedLabel = new System.Windows.Forms.Label();
      this.connectedLabel = new System.Windows.Forms.Label();
      this.bipartiteLabel = new System.Windows.Forms.Label();
      this.acyclicLabel = new System.Windows.Forms.Label();
      this.numberOfEdges = new System.Windows.Forms.Label();
      this.numberOfEdgesLabel = new System.Windows.Forms.Label();
      this.numberOfNodes = new System.Windows.Forms.Label();
      this.numberOfNodesLabel = new System.Windows.Forms.Label();
      this.graphInformationLabel = new System.Windows.Forms.Label();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.newGraphButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.previousButton = new System.Windows.Forms.ToolStripButton();
      this.sampleComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.nextButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.algorithmComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.generateEdgeLabelsButton = new System.Windows.Forms.ToolStripButton();
      this.removeEdgeLabelsButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.uniformEdgeWeightsComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.directionComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.layoutButton = new System.Windows.Forms.ToolStripButton();
      this.nodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.markAsSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.markAsTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.textboxPanel.SuspendLayout();
      this.panel1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.nodeContextMenuStrip.SuspendLayout();
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
      this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
      this.splitContainer1.Panel1.Controls.Add(this.textboxPanel);
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Window;
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
      this.splitContainer1.Panel2.ForeColor = System.Drawing.SystemColors.WindowText;
      this.splitContainer1.Size = new System.Drawing.Size(1351, 658);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 2;
      // 
      // textboxPanel
      // 
      this.textboxPanel.AutoScroll = true;
      this.textboxPanel.Controls.Add(this.descriptionTextBox);
      this.textboxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textboxPanel.Location = new System.Drawing.Point(0, 0);
      this.textboxPanel.Name = "textboxPanel";
      this.textboxPanel.Padding = new System.Windows.Forms.Padding(20);
      this.textboxPanel.Size = new System.Drawing.Size(252, 409);
      this.textboxPanel.TabIndex = 2;
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.descriptionTextBox.Location = new System.Drawing.Point(20, 20);
      this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ReadOnly = true;
      this.descriptionTextBox.Size = new System.Drawing.Size(212, 369);
      this.descriptionTextBox.TabIndex = 0;
      this.descriptionTextBox.Text = "description textbox content";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.parallelEdgesLabel);
      this.panel1.Controls.Add(this.selfLoopsLabel);
      this.panel1.Controls.Add(this.treeLabel);
      this.panel1.Controls.Add(this.planarLabel);
      this.panel1.Controls.Add(this.stronglyConnectedLabel);
      this.panel1.Controls.Add(this.biconnectedLabel);
      this.panel1.Controls.Add(this.connectedLabel);
      this.panel1.Controls.Add(this.bipartiteLabel);
      this.panel1.Controls.Add(this.acyclicLabel);
      this.panel1.Controls.Add(this.numberOfEdges);
      this.panel1.Controls.Add(this.numberOfEdgesLabel);
      this.panel1.Controls.Add(this.numberOfNodes);
      this.panel1.Controls.Add(this.numberOfNodesLabel);
      this.panel1.Controls.Add(this.graphInformationLabel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 409);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(252, 249);
      this.panel1.TabIndex = 1;
      // 
      // parallelEdgesLabel
      // 
      this.parallelEdgesLabel.AutoSize = true;
      this.parallelEdgesLabel.Location = new System.Drawing.Point(15, 214);
      this.parallelEdgesLabel.Name = "parallelEdgesLabel";
      this.parallelEdgesLabel.Size = new System.Drawing.Size(74, 13);
      this.parallelEdgesLabel.TabIndex = 13;
      this.parallelEdgesLabel.Text = "Parallel Edges";
      // 
      // selfLoopsLabel
      // 
      this.selfLoopsLabel.AutoSize = true;
      this.selfLoopsLabel.Location = new System.Drawing.Point(15, 197);
      this.selfLoopsLabel.Name = "selfLoopsLabel";
      this.selfLoopsLabel.Size = new System.Drawing.Size(57, 13);
      this.selfLoopsLabel.TabIndex = 12;
      this.selfLoopsLabel.Text = "Self Loops";
      // 
      // treeLabel
      // 
      this.treeLabel.AutoSize = true;
      this.treeLabel.Location = new System.Drawing.Point(15, 180);
      this.treeLabel.Name = "treeLabel";
      this.treeLabel.Size = new System.Drawing.Size(29, 13);
      this.treeLabel.TabIndex = 11;
      this.treeLabel.Text = "Tree";
      // 
      // planarLabel
      // 
      this.planarLabel.AutoSize = true;
      this.planarLabel.Location = new System.Drawing.Point(15, 163);
      this.planarLabel.Name = "planarLabel";
      this.planarLabel.Size = new System.Drawing.Size(37, 13);
      this.planarLabel.TabIndex = 10;
      this.planarLabel.Text = "Planar";
      // 
      // stronglyConnectedLabel
      // 
      this.stronglyConnectedLabel.AutoSize = true;
      this.stronglyConnectedLabel.Location = new System.Drawing.Point(15, 146);
      this.stronglyConnectedLabel.Name = "stronglyConnectedLabel";
      this.stronglyConnectedLabel.Size = new System.Drawing.Size(100, 13);
      this.stronglyConnectedLabel.TabIndex = 9;
      this.stronglyConnectedLabel.Text = "Strongly Connected";
      // 
      // biconnectedLabel
      // 
      this.biconnectedLabel.AutoSize = true;
      this.biconnectedLabel.Location = new System.Drawing.Point(15, 129);
      this.biconnectedLabel.Name = "biconnectedLabel";
      this.biconnectedLabel.Size = new System.Drawing.Size(67, 13);
      this.biconnectedLabel.TabIndex = 8;
      this.biconnectedLabel.Text = "Biconnected";
      // 
      // connectedLabel
      // 
      this.connectedLabel.AutoSize = true;
      this.connectedLabel.Location = new System.Drawing.Point(15, 112);
      this.connectedLabel.Name = "connectedLabel";
      this.connectedLabel.Size = new System.Drawing.Size(59, 13);
      this.connectedLabel.TabIndex = 7;
      this.connectedLabel.Text = "Connected";
      // 
      // bipartiteLabel
      // 
      this.bipartiteLabel.AutoSize = true;
      this.bipartiteLabel.Location = new System.Drawing.Point(15, 95);
      this.bipartiteLabel.Name = "bipartiteLabel";
      this.bipartiteLabel.Size = new System.Drawing.Size(45, 13);
      this.bipartiteLabel.TabIndex = 6;
      this.bipartiteLabel.Text = "Bipartite";
      // 
      // acyclicLabel
      // 
      this.acyclicLabel.AutoSize = true;
      this.acyclicLabel.Location = new System.Drawing.Point(15, 78);
      this.acyclicLabel.Name = "acyclicLabel";
      this.acyclicLabel.Size = new System.Drawing.Size(41, 13);
      this.acyclicLabel.TabIndex = 5;
      this.acyclicLabel.Text = "Acyclic";
      // 
      // numberOfEdges
      // 
      this.numberOfEdges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.numberOfEdges.AutoSize = true;
      this.numberOfEdges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numberOfEdges.Location = new System.Drawing.Point(154, 57);
      this.numberOfEdges.Name = "numberOfEdges";
      this.numberOfEdges.Size = new System.Drawing.Size(11, 13);
      this.numberOfEdges.TabIndex = 4;
      this.numberOfEdges.Text = "-";
      this.numberOfEdges.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // numberOfEdgesLabel
      // 
      this.numberOfEdgesLabel.AutoSize = true;
      this.numberOfEdgesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numberOfEdgesLabel.Location = new System.Drawing.Point(15, 57);
      this.numberOfEdgesLabel.Name = "numberOfEdgesLabel";
      this.numberOfEdgesLabel.Size = new System.Drawing.Size(104, 13);
      this.numberOfEdgesLabel.TabIndex = 3;
      this.numberOfEdgesLabel.Text = "Number of Edges";
      // 
      // numberOfNodes
      // 
      this.numberOfNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.numberOfNodes.AutoSize = true;
      this.numberOfNodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numberOfNodes.Location = new System.Drawing.Point(154, 40);
      this.numberOfNodes.Name = "numberOfNodes";
      this.numberOfNodes.Size = new System.Drawing.Size(11, 13);
      this.numberOfNodes.TabIndex = 2;
      this.numberOfNodes.Text = "-";
      this.numberOfNodes.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // numberOfNodesLabel
      // 
      this.numberOfNodesLabel.AutoSize = true;
      this.numberOfNodesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numberOfNodesLabel.Location = new System.Drawing.Point(15, 40);
      this.numberOfNodesLabel.Name = "numberOfNodesLabel";
      this.numberOfNodesLabel.Size = new System.Drawing.Size(105, 13);
      this.numberOfNodesLabel.TabIndex = 1;
      this.numberOfNodesLabel.Text = "Number of Nodes";
      // 
      // graphInformationLabel
      // 
      this.graphInformationLabel.AutoSize = true;
      this.graphInformationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.graphInformationLabel.Location = new System.Drawing.Point(12, 3);
      this.graphInformationLabel.Name = "graphInformationLabel";
      this.graphInformationLabel.Size = new System.Drawing.Size(127, 18);
      this.graphInformationLabel.TabIndex = 0;
      this.graphInformationLabel.Text = "Graph Information";
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1095, 623);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(1095, 658);
      this.toolStripContainer1.TabIndex = 2;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(1095, 623);
      this.graphControl.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGraphButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator3,
            this.previousButton,
            this.sampleComboBox,
            this.nextButton,
            this.toolStripSeparator4,
            this.algorithmComboBox,
            this.toolStripSeparator5,
            this.generateEdgeLabelsButton,
            this.removeEdgeLabelsButton,
            this.toolStripSeparator6,
            this.uniformEdgeWeightsComboBox,
            this.directionComboBox,
            this.toolStripSeparator7,
            this.layoutButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(1095, 35);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // newGraphButton
      // 
      this.newGraphButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newGraphButton.Image = ((System.Drawing.Image)(resources.GetObject("newGraphButton.Image")));
      this.newGraphButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newGraphButton.Name = "newGraphButton";
      this.newGraphButton.Size = new System.Drawing.Size(23, 24);
      this.newGraphButton.Text = "New Graph";
      this.newGraphButton.Click += new System.EventHandler(this.NewGraphButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 24);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 24);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = ((System.Drawing.Image)(resources.GetObject("fitContentButton.Image")));
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 24);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 24);
      this.undoButton.Text = "undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 24);
      this.redoButton.Text = "redo";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
      // 
      // previousButton
      // 
      this.previousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.previousButton.Image = ((System.Drawing.Image)(resources.GetObject("previousButton.Image")));
      this.previousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.previousButton.Name = "previousButton";
      this.previousButton.Size = new System.Drawing.Size(23, 24);
      this.previousButton.Text = "Previous sample";
      this.previousButton.Click += new System.EventHandler(this.PreviousSample);
      // 
      // sampleComboBox
      // 
      this.sampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.sampleComboBox.DropDownWidth = 250;
      this.sampleComboBox.Name = "sampleComboBox";
      this.sampleComboBox.Size = new System.Drawing.Size(250, 27);
      this.sampleComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSampleChanged);
      // 
      // nextButton
      // 
      this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.nextButton.Image = ((System.Drawing.Image)(resources.GetObject("nextButton.Image")));
      this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(23, 24);
      this.nextButton.Text = "Next sample";
      this.nextButton.Click += new System.EventHandler(this.NextSample);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
      // 
      // algorithmComboBox
      // 
      this.algorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.algorithmComboBox.DropDownWidth = 250;
      this.algorithmComboBox.Name = "algorithmComboBox";
      this.algorithmComboBox.Size = new System.Drawing.Size(250, 27);
      this.algorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.OnAlgorithmChanged);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
      // 
      // generateEdgeLabelsButton
      // 
      this.generateEdgeLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.generateEdgeLabelsButton.Image = ((System.Drawing.Image)(resources.GetObject("generateEdgeLabelsButton.Image")));
      this.generateEdgeLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.generateEdgeLabelsButton.Name = "generateEdgeLabelsButton";
      this.generateEdgeLabelsButton.Size = new System.Drawing.Size(123, 24);
      this.generateEdgeLabelsButton.Text = "Generate Edge Labels";
      this.generateEdgeLabelsButton.Click += new System.EventHandler(this.GenerateEdgeLabels);
      // 
      // removeEdgeLabelsButton
      // 
      this.removeEdgeLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.removeEdgeLabelsButton.Image = ((System.Drawing.Image)(resources.GetObject("removeEdgeLabelsButton.Image")));
      this.removeEdgeLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.removeEdgeLabelsButton.Name = "removeEdgeLabelsButton";
      this.removeEdgeLabelsButton.Size = new System.Drawing.Size(126, 24);
      this.removeEdgeLabelsButton.Text = "Delete All Edge Labels";
      this.removeEdgeLabelsButton.Click += new System.EventHandler(this.RemoveEdgeLabels);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
      // 
      // uniformEdgeWeightsComboBox
      // 
      this.uniformEdgeWeightsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.uniformEdgeWeightsComboBox.DropDownWidth = 180;
      this.uniformEdgeWeightsComboBox.Items.AddRange(new object[] {
            "Uniform Edge Weights",
            "Non-uniform Edge Weights"});
      this.uniformEdgeWeightsComboBox.Name = "uniformEdgeWeightsComboBox";
      this.uniformEdgeWeightsComboBox.Size = new System.Drawing.Size(180, 23);
      this.uniformEdgeWeightsComboBox.SelectedIndexChanged += new System.EventHandler(this.OnUniformEdgeWeightsChanged);
      // 
      // directionComboBox
      // 
      this.directionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.directionComboBox.DropDownWidth = 100;
      this.directionComboBox.Items.AddRange(new object[] {
            "Undirected",
            "Directed"});
      this.directionComboBox.Name = "directionComboBox";
      this.directionComboBox.Size = new System.Drawing.Size(100, 23);
      this.directionComboBox.SelectedIndexChanged += new System.EventHandler(this.OnDirectedChanged);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
      // 
      // layoutButton
      // 
      this.layoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.layoutButton.Image = ((System.Drawing.Image)(resources.GetObject("layoutButton.Image")));
      this.layoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.layoutButton.Name = "layoutButton";
      this.layoutButton.Size = new System.Drawing.Size(47, 19);
      this.layoutButton.Text = "Layout";
      this.layoutButton.Click += new System.EventHandler(this.RunLayout);
      // 
      // nodeContextMenuStrip
      // 
      this.nodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsSourceMenuItem,
            this.markAsTargetMenuItem});
      this.nodeContextMenuStrip.Name = "nodeContextMenuStrip";
      this.nodeContextMenuStrip.Size = new System.Drawing.Size(157, 48);
      // 
      // markAsSourceMenuItem
      // 
      this.markAsSourceMenuItem.Name = "markAsSourceMenuItem";
      this.markAsSourceMenuItem.Size = new System.Drawing.Size(156, 22);
      this.markAsSourceMenuItem.Text = "Mark As Source";
      // 
      // markAsTargetMenuItem
      // 
      this.markAsTargetMenuItem.Name = "markAsTargetMenuItem";
      this.markAsTargetMenuItem.Size = new System.Drawing.Size(156, 22);
      this.markAsTargetMenuItem.Text = "Mark as target";
      // 
      // GraphAnalysisForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1351, 658);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "GraphAnalysisForm";
      this.Text = "Graph Analysis Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoaded);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.textboxPanel.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.nodeContextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox descriptionTextBox;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ContextMenuStrip nodeContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem markAsSourceMenuItem;
    private System.Windows.Forms.ToolStripMenuItem markAsTargetMenuItem;
    private ToolStripContainer toolStripContainer1;
    private ToolStrip toolStrip1;
    private ToolStripButton newGraphButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton zoomInButton;
    private ToolStripButton zoomOutButton;
    private ToolStripButton fitContentButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton undoButton;
    private ToolStripButton redoButton;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton previousButton;
    private ToolStripComboBox sampleComboBox;
    private ToolStripButton nextButton;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripComboBox algorithmComboBox;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton generateEdgeLabelsButton;
    private ToolStripButton removeEdgeLabelsButton;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripComboBox uniformEdgeWeightsComboBox;
    private ToolStripComboBox directionComboBox;
    private ToolStripSeparator toolStripSeparator7;
    private Panel panel1;
    private Label numberOfNodesLabel;
    private Label graphInformationLabel;
    private Label connectedLabel;
    private Label bipartiteLabel;
    private Label acyclicLabel;
    private Label numberOfEdges;
    private Label numberOfEdgesLabel;
    private Label numberOfNodes;
    private Label parallelEdgesLabel;
    private Label selfLoopsLabel;
    private Label treeLabel;
    private Label planarLabel;
    private Label stronglyConnectedLabel;
    private Label biconnectedLabel;
    private ToolStripButton layoutButton;
    private Panel textboxPanel;
  }
}

