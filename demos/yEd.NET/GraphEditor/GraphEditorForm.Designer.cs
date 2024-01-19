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

using System.ComponentModel;
using System.Windows.Forms;
using yWorks.Geometry;
using yWorks.Graph.Styles;
using yWorks.Graph.LabelModels;
using yWorks.Graph;

namespace Demo.yFiles.GraphEditor
{
  partial class GraphEditorForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphEditorForm));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toplevel_splitContainer = new System.Windows.Forms.SplitContainer();
      this.inner_splitContainer = new System.Windows.Forms.SplitContainer();
      this.propertyTabControl = new System.Windows.Forms.TabControl();
      this.nodePropertiesTabPage = new System.Windows.Forms.TabPage();
      this.nodePropertiesPanel = new System.Windows.Forms.Panel();
      this.edgePropertiesTabPage = new System.Windows.Forms.TabPage();
      this.edgePropertiesPanel = new System.Windows.Forms.Panel();
      this.labelPropertiesTabPage = new System.Windows.Forms.TabPage();
      this.labelPropertiesPanel = new System.Windows.Forms.Panel();
      this.portPropertiesTabPage = new System.Windows.Forms.TabPage();
      this.portPropertiesPanel = new System.Windows.Forms.Panel();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.palettePanel = new System.Windows.Forms.Panel();
      this.panel9 = new System.Windows.Forms.Panel();
      this.portStyleListBox = new System.Windows.Forms.ListBox();
      this.portStyleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.applyPortStyleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label9 = new System.Windows.Forms.Label();
      this.panel8 = new System.Windows.Forms.Panel();
      this.labelStyleListBox = new System.Windows.Forms.ListBox();
      this.labelStyleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.applyLabelStyleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label8 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.edgeStyleListBox = new System.Windows.Forms.ListBox();
      this.edgeStyleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.applyEdgeStyleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label3 = new System.Windows.Forms.Label();
      this.panel5 = new System.Windows.Forms.Panel();
      this.peopleStyleListBox = new System.Windows.Forms.ListBox();
      this.nodeStyleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.applyNodeStyleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label5 = new System.Windows.Forms.Label();
      this.panel4 = new System.Windows.Forms.Panel();
      this.computersStyleListBox = new System.Windows.Forms.ListBox();
      this.label4 = new System.Windows.Forms.Label();
      this.panel7 = new System.Windows.Forms.Panel();
      this.groupNodeStyleListBox = new System.Windows.Forms.ListBox();
      this.label7 = new System.Windows.Forms.Label();
      this.panel6 = new System.Windows.Forms.Panel();
      this.shapeNodeStyleListBox = new System.Windows.Forms.ListBox();
      this.label6 = new System.Windows.Forms.Label();
      this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.resetToFactoryDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zoomToOriginalSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fitContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.setZoomLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.hierarchicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.organicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.edgeRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.polylineEdgeRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.channelRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.organicEdgeRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.busRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.parallelEdgeRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.directedOrthogonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.orthogonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.compactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.circularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.treesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.balloonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.radialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.seriesparallelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.labelingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.componentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.partialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.randomGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.treeGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.configurePortConstraintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.configureEdgeGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.showOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.showPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.paletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.hierarchyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ungroupSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.adjustGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.collapseGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.expandGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
      this.enterGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.quickReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sampleFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutYEdNETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mainButtonsToolStrip = new System.Windows.Forms.ToolStrip();
      this.newFileButton = new System.Windows.Forms.ToolStripButton();
      this.openFileButton = new System.Windows.Forms.ToolStripButton();
      this.saveFileButton = new System.Windows.Forms.ToolStripButton();
      this.printButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.cutButton = new System.Windows.Forms.ToolStripButton();
      this.copyButton = new System.Windows.Forms.ToolStripButton();
      this.pasteButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.zoomToOriginalSizeButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.setZoomtoolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.toggleOrthogonalEdgesButton = new System.Windows.Forms.ToolStripButton();
      this.toggleSnaplinesButton = new System.Windows.Forms.ToolStripButton();
      this.toggleGridButton = new System.Windows.Forms.ToolStripButton();
      this.toggleLassoModeButton = new System.Windows.Forms.ToolStripButton();
      this.structureViewImages = new System.Windows.Forms.ImageList(this.components);
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.panel1 = new System.Windows.Forms.Panel();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.toplevel_splitContainer)).BeginInit();
      this.toplevel_splitContainer.Panel1.SuspendLayout();
      this.toplevel_splitContainer.Panel2.SuspendLayout();
      this.toplevel_splitContainer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.inner_splitContainer)).BeginInit();
      this.inner_splitContainer.Panel1.SuspendLayout();
      this.inner_splitContainer.Panel2.SuspendLayout();
      this.inner_splitContainer.SuspendLayout();
      this.propertyTabControl.SuspendLayout();
      this.nodePropertiesTabPage.SuspendLayout();
      this.edgePropertiesTabPage.SuspendLayout();
      this.labelPropertiesTabPage.SuspendLayout();
      this.portPropertiesTabPage.SuspendLayout();
      this.palettePanel.SuspendLayout();
      this.panel9.SuspendLayout();
      this.portStyleContextMenu.SuspendLayout();
      this.panel8.SuspendLayout();
      this.labelStyleContextMenu.SuspendLayout();
      this.panel2.SuspendLayout();
      this.edgeStyleContextMenu.SuspendLayout();
      this.panel5.SuspendLayout();
      this.nodeStyleContextMenu.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel7.SuspendLayout();
      this.panel6.SuspendLayout();
      this.mainMenuStrip.SuspendLayout();
      this.mainButtonsToolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.BottomToolStripPanel
      // 
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
      // 
      // toolStripContainer1.ContentPanel
      // 
      resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
      this.toolStripContainer1.ContentPanel.Controls.Add(this.toplevel_splitContainer);
      resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
      this.toolStripContainer1.Name = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainButtonsToolStrip);
      // 
      // statusStrip1
      // 
      resources.ApplyResources(this.statusStrip1, "statusStrip1");
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.toolStripStatusLabel2,
      this.toolStripStatusLabel3});
      this.statusStrip1.Name = "statusStrip1";
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
      // 
      // toolStripStatusLabel3
      // 
      this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
      this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
      resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
      // 
      // toplevel_splitContainer
      // 
      this.toplevel_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      resources.ApplyResources(this.toplevel_splitContainer, "toplevel_splitContainer");
      this.toplevel_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.toplevel_splitContainer.Name = "toplevel_splitContainer";
      // 
      // toplevel_splitContainer.Panel1
      // 
      this.toplevel_splitContainer.Panel1.Controls.Add(this.inner_splitContainer);
      // 
      // toplevel_splitContainer.Panel2
      // 
      this.toplevel_splitContainer.Panel2.Controls.Add(this.palettePanel);
      // 
      // inner_splitContainer
      // 
      resources.ApplyResources(this.inner_splitContainer, "inner_splitContainer");
      this.inner_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.inner_splitContainer.Name = "inner_splitContainer";
      // 
      // inner_splitContainer.Panel1
      // 
      this.inner_splitContainer.Panel1.Controls.Add(this.propertyTabControl);
      // 
      // inner_splitContainer.Panel2
      // 
      this.inner_splitContainer.Panel2.Controls.Add(this.graphControl);
      // 
      // propertyTabControl
      // 
      this.propertyTabControl.Controls.Add(this.nodePropertiesTabPage);
      this.propertyTabControl.Controls.Add(this.edgePropertiesTabPage);
      this.propertyTabControl.Controls.Add(this.labelPropertiesTabPage);
      this.propertyTabControl.Controls.Add(this.portPropertiesTabPage);
      resources.ApplyResources(this.propertyTabControl, "propertyTabControl");
      this.propertyTabControl.Name = "propertyTabControl";
      this.propertyTabControl.SelectedIndex = 0;
      // 
      // nodePropertiesTabPage
      // 
      this.nodePropertiesTabPage.Controls.Add(this.nodePropertiesPanel);
      resources.ApplyResources(this.nodePropertiesTabPage, "nodePropertiesTabPage");
      this.nodePropertiesTabPage.Name = "nodePropertiesTabPage";
      this.nodePropertiesTabPage.UseVisualStyleBackColor = true;
      // 
      // nodePropertiesPanel
      // 
      this.nodePropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      resources.ApplyResources(this.nodePropertiesPanel, "nodePropertiesPanel");
      this.nodePropertiesPanel.Name = "nodePropertiesPanel";
      // 
      // edgePropertiesTabPage
      // 
      this.edgePropertiesTabPage.Controls.Add(this.edgePropertiesPanel);
      resources.ApplyResources(this.edgePropertiesTabPage, "edgePropertiesTabPage");
      this.edgePropertiesTabPage.Name = "edgePropertiesTabPage";
      this.edgePropertiesTabPage.UseVisualStyleBackColor = true;
      // 
      // edgePropertiesPanel
      // 
      this.edgePropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      resources.ApplyResources(this.edgePropertiesPanel, "edgePropertiesPanel");
      this.edgePropertiesPanel.Name = "edgePropertiesPanel";
      // 
      // labelPropertiesTabPage
      // 
      this.labelPropertiesTabPage.Controls.Add(this.labelPropertiesPanel);
      resources.ApplyResources(this.labelPropertiesTabPage, "labelPropertiesTabPage");
      this.labelPropertiesTabPage.Name = "labelPropertiesTabPage";
      this.labelPropertiesTabPage.UseVisualStyleBackColor = true;
      // 
      // labelPropertiesPanel
      // 
      this.labelPropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      resources.ApplyResources(this.labelPropertiesPanel, "labelPropertiesPanel");
      this.labelPropertiesPanel.Name = "labelPropertiesPanel";
      // 
      // portPropertiesTabPage
      // 
      this.portPropertiesTabPage.Controls.Add(this.portPropertiesPanel);
      resources.ApplyResources(this.portPropertiesTabPage, "portPropertiesTabPage");
      this.portPropertiesTabPage.Name = "portPropertiesTabPage";
      this.portPropertiesTabPage.UseVisualStyleBackColor = true;
      // 
      // portPropertiesPanel
      // 
      this.portPropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      resources.ApplyResources(this.portPropertiesPanel, "portPropertiesPanel");
      this.portPropertiesPanel.Name = "portPropertiesPanel";
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      resources.ApplyResources(this.graphControl, "graphControl");
      this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.Name = "graphControl";
      this.graphControl.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      // 
      // palettePanel
      // 
      resources.ApplyResources(this.palettePanel, "palettePanel");
      this.palettePanel.Controls.Add(this.panel9);
      this.palettePanel.Controls.Add(this.panel8);
      this.palettePanel.Controls.Add(this.panel2);
      this.palettePanel.Controls.Add(this.panel5);
      this.palettePanel.Controls.Add(this.panel4);
      this.palettePanel.Controls.Add(this.panel7);
      this.palettePanel.Controls.Add(this.panel6);
      this.palettePanel.Name = "palettePanel";
      // 
      // panel9
      // 
      this.panel9.Controls.Add(this.portStyleListBox);
      this.panel9.Controls.Add(this.label9);
      resources.ApplyResources(this.panel9, "panel9");
      this.panel9.Name = "panel9";
      // 
      // portStyleListBox
      // 
      this.portStyleListBox.ContextMenuStrip = this.portStyleContextMenu;
      resources.ApplyResources(this.portStyleListBox, "portStyleListBox");
      this.portStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.portStyleListBox.FormattingEnabled = true;
      this.portStyleListBox.MultiColumn = true;
      this.portStyleListBox.Name = "portStyleListBox";
      // 
      // portStyleContextMenu
      // 
      this.portStyleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.applyPortStyleMenuItem});
      this.portStyleContextMenu.Name = "portStyleContextMenu";
      resources.ApplyResources(this.portStyleContextMenu, "portStyleContextMenu");
      this.portStyleContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.portStyleContextMenu_ItemClicked);
      // 
      // applyPortStyleMenuItem
      // 
      this.applyPortStyleMenuItem.Name = "applyPortStyleMenuItem";
      resources.ApplyResources(this.applyPortStyleMenuItem, "applyPortStyleMenuItem");
      // 
      // label9
      // 
      this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label9, "label9");
      this.label9.Name = "label9";
      // 
      // panel8
      // 
      this.panel8.Controls.Add(this.labelStyleListBox);
      this.panel8.Controls.Add(this.label8);
      resources.ApplyResources(this.panel8, "panel8");
      this.panel8.Name = "panel8";
      // 
      // labelStyleListBox
      // 
      this.labelStyleListBox.ContextMenuStrip = this.labelStyleContextMenu;
      resources.ApplyResources(this.labelStyleListBox, "labelStyleListBox");
      this.labelStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.labelStyleListBox.FormattingEnabled = true;
      this.labelStyleListBox.MultiColumn = true;
      this.labelStyleListBox.Name = "labelStyleListBox";
      // 
      // labelStyleContextMenu
      // 
      this.labelStyleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.applyLabelStyleMenuItem});
      this.labelStyleContextMenu.Name = "labelStyleContextMenu";
      resources.ApplyResources(this.labelStyleContextMenu, "labelStyleContextMenu");
      this.labelStyleContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.labelStyleContextMenu_ItemClicked);
      // 
      // applyLabelStyleMenuItem
      // 
      this.applyLabelStyleMenuItem.Name = "applyLabelStyleMenuItem";
      resources.ApplyResources(this.applyLabelStyleMenuItem, "applyLabelStyleMenuItem");
      // 
      // label8
      // 
      this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label8, "label8");
      this.label8.Name = "label8";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.edgeStyleListBox);
      this.panel2.Controls.Add(this.label3);
      resources.ApplyResources(this.panel2, "panel2");
      this.panel2.Name = "panel2";
      // 
      // edgeStyleListBox
      // 
      this.edgeStyleListBox.ContextMenuStrip = this.edgeStyleContextMenu;
      resources.ApplyResources(this.edgeStyleListBox, "edgeStyleListBox");
      this.edgeStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.edgeStyleListBox.FormattingEnabled = true;
      this.edgeStyleListBox.MultiColumn = true;
      this.edgeStyleListBox.Name = "edgeStyleListBox";
      // 
      // edgeStyleContextMenu
      // 
      this.edgeStyleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.applyEdgeStyleMenuItem});
      this.edgeStyleContextMenu.Name = "styleContextMenu";
      resources.ApplyResources(this.edgeStyleContextMenu, "edgeStyleContextMenu");
      this.edgeStyleContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.edgeStyleContextMenu_ItemClicked);
      // 
      // applyEdgeStyleMenuItem
      // 
      this.applyEdgeStyleMenuItem.Name = "applyEdgeStyleMenuItem";
      resources.ApplyResources(this.applyEdgeStyleMenuItem, "applyEdgeStyleMenuItem");
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label3, "label3");
      this.label3.Name = "label3";
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.peopleStyleListBox);
      this.panel5.Controls.Add(this.label5);
      resources.ApplyResources(this.panel5, "panel5");
      this.panel5.Name = "panel5";
      // 
      // peopleStyleListBox
      // 
      this.peopleStyleListBox.ContextMenuStrip = this.nodeStyleContextMenu;
      resources.ApplyResources(this.peopleStyleListBox, "peopleStyleListBox");
      this.peopleStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.peopleStyleListBox.FormattingEnabled = true;
      this.peopleStyleListBox.MultiColumn = true;
      this.peopleStyleListBox.Name = "peopleStyleListBox";
      // 
      // nodeStyleContextMenu
      // 
      this.nodeStyleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.applyNodeStyleMenuItem});
      this.nodeStyleContextMenu.Name = "styleContextMenu";
      resources.ApplyResources(this.nodeStyleContextMenu, "nodeStyleContextMenu");
      this.nodeStyleContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.nodeStyleContextMenu_ItemClicked);
      // 
      // applyNodeStyleMenuItem
      // 
      this.applyNodeStyleMenuItem.Name = "applyNodeStyleMenuItem";
      resources.ApplyResources(this.applyNodeStyleMenuItem, "applyNodeStyleMenuItem");
      // 
      // label5
      // 
      this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label5, "label5");
      this.label5.Name = "label5";
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.computersStyleListBox);
      this.panel4.Controls.Add(this.label4);
      resources.ApplyResources(this.panel4, "panel4");
      this.panel4.Name = "panel4";
      // 
      // computersStyleListBox
      // 
      this.computersStyleListBox.ContextMenuStrip = this.nodeStyleContextMenu;
      resources.ApplyResources(this.computersStyleListBox, "computersStyleListBox");
      this.computersStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.computersStyleListBox.FormattingEnabled = true;
      this.computersStyleListBox.MultiColumn = true;
      this.computersStyleListBox.Name = "computersStyleListBox";
      // 
      // label4
      // 
      this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label4, "label4");
      this.label4.Name = "label4";
      // 
      // panel7
      // 
      this.panel7.Controls.Add(this.groupNodeStyleListBox);
      this.panel7.Controls.Add(this.label7);
      resources.ApplyResources(this.panel7, "panel7");
      this.panel7.Name = "panel7";
      // 
      // groupNodeStyleListBox
      // 
      this.groupNodeStyleListBox.ContextMenuStrip = this.nodeStyleContextMenu;
      resources.ApplyResources(this.groupNodeStyleListBox, "groupNodeStyleListBox");
      this.groupNodeStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.groupNodeStyleListBox.FormattingEnabled = true;
      this.groupNodeStyleListBox.MultiColumn = true;
      this.groupNodeStyleListBox.Name = "groupNodeStyleListBox";
      // 
      // label7
      // 
      this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label7, "label7");
      this.label7.Name = "label7";
      // 
      // panel6
      // 
      this.panel6.Controls.Add(this.shapeNodeStyleListBox);
      this.panel6.Controls.Add(this.label6);
      resources.ApplyResources(this.panel6, "panel6");
      this.panel6.Name = "panel6";
      // 
      // shapeNodeStyleListBox
      // 
      this.shapeNodeStyleListBox.BackColor = System.Drawing.SystemColors.Window;
      this.shapeNodeStyleListBox.ContextMenuStrip = this.nodeStyleContextMenu;
      resources.ApplyResources(this.shapeNodeStyleListBox, "shapeNodeStyleListBox");
      this.shapeNodeStyleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.shapeNodeStyleListBox.FormattingEnabled = true;
      this.shapeNodeStyleListBox.MultiColumn = true;
      this.shapeNodeStyleListBox.Name = "shapeNodeStyleListBox";
      // 
      // label6
      // 
      this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.label6, "label6");
      this.label6.Name = "label6";
      // 
      // mainMenuStrip
      // 
      resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
      this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.fileToolStripMenuItem,
      this.editToolStripMenuItem,
      this.viewToolStripMenuItem,
      this.toolStripMenuItem1,
      this.toolsToolStripMenuItem,
      this.windowToolStripMenuItem,
      this.hierarchyToolStripMenuItem,
      this.helpToolStripMenuItem});
      this.mainMenuStrip.Name = "mainMenuStrip";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.newToolStripMenuItem,
      this.loadToolStripMenuItem,
      this.recentFilesToolStripMenuItem,
      this.toolStripSeparator4,
      this.saveToolStripMenuItem,
      this.saveAsToolStripMenuItem,
      this.exportToolStripMenuItem,
      this.toolStripSeparator5,
      this.printToolStripMenuItem,
      this.toolStripSeparator6,
      this.preferencesToolStripMenuItem,
      this.resetToFactoryDefaultsToolStripMenuItem,
      this.toolStripSeparator1,
      this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
      // 
      // loadToolStripMenuItem
      // 
      this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
      resources.ApplyResources(this.loadToolStripMenuItem, "loadToolStripMenuItem");
      // 
      // recentFilesToolStripMenuItem
      // 
      resources.ApplyResources(this.recentFilesToolStripMenuItem, "recentFilesToolStripMenuItem");
      this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
      // 
      // exportToolStripMenuItem
      // 
      this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
      resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
      // 
      // preferencesToolStripMenuItem
      // 
      this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
      resources.ApplyResources(this.preferencesToolStripMenuItem, "preferencesToolStripMenuItem");
      // 
      // resetToFactoryDefaultsToolStripMenuItem
      // 
      this.resetToFactoryDefaultsToolStripMenuItem.Name = "resetToFactoryDefaultsToolStripMenuItem";
      resources.ApplyResources(this.resetToFactoryDefaultsToolStripMenuItem, "resetToFactoryDefaultsToolStripMenuItem");
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.undoToolStripMenuItem,
      this.redoToolStripMenuItem,
      this.toolStripSeparator2,
      this.cutToolStripMenuItem,
      this.copyToolStripMenuItem,
      this.pasteToolStripMenuItem,
      this.deleteToolStripMenuItem,
      this.toolStripSeparator3,
      this.duplicateToolStripMenuItem,
      this.reverseToolStripMenuItem,
      this.toolStripSeparator13,
      this.selectAllToolStripMenuItem,
      this.clearSelectionToolStripMenuItem,
      this.toolStripSeparator12,
      this.propertiesToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
      // 
      // undoToolStripMenuItem
      // 
      this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
      resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
      // 
      // redoToolStripMenuItem
      // 
      this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
      resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
      // 
      // duplicateToolStripMenuItem
      // 
      this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
      resources.ApplyResources(this.duplicateToolStripMenuItem, "duplicateToolStripMenuItem");
      // 
      // reverseToolStripMenuItem
      // 
      this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
      resources.ApplyResources(this.reverseToolStripMenuItem, "reverseToolStripMenuItem");
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
      // 
      // selectAllToolStripMenuItem
      // 
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
      // 
      // clearSelectionToolStripMenuItem
      // 
      this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
      resources.ApplyResources(this.clearSelectionToolStripMenuItem, "clearSelectionToolStripMenuItem");
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
      // 
      // propertiesToolStripMenuItem
      // 
      this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
      resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.zoomInToolStripMenuItem,
      this.zoomOutToolStripMenuItem,
      this.zoomToOriginalSizeToolStripMenuItem,
      this.fitContentToolStripMenuItem,
      this.setZoomLevelToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
      // 
      // zoomInToolStripMenuItem
      // 
      this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
      resources.ApplyResources(this.zoomInToolStripMenuItem, "zoomInToolStripMenuItem");
      // 
      // zoomOutToolStripMenuItem
      // 
      this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
      resources.ApplyResources(this.zoomOutToolStripMenuItem, "zoomOutToolStripMenuItem");
      // 
      // zoomToOriginalSizeToolStripMenuItem
      // 
      this.zoomToOriginalSizeToolStripMenuItem.Name = "zoomToOriginalSizeToolStripMenuItem";
      resources.ApplyResources(this.zoomToOriginalSizeToolStripMenuItem, "zoomToOriginalSizeToolStripMenuItem");
      // 
      // fitContentToolStripMenuItem
      // 
      this.fitContentToolStripMenuItem.Name = "fitContentToolStripMenuItem";
      resources.ApplyResources(this.fitContentToolStripMenuItem, "fitContentToolStripMenuItem");
      // 
      // setZoomLevelToolStripMenuItem
      // 
      this.setZoomLevelToolStripMenuItem.Name = "setZoomLevelToolStripMenuItem";
      resources.ApplyResources(this.setZoomLevelToolStripMenuItem, "setZoomLevelToolStripMenuItem");
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.hierarchicToolStripMenuItem,
      this.organicToolStripMenuItem,
      this.edgeRouterToolStripMenuItem,
      this.directedOrthogonalToolStripMenuItem,
      this.circularToolStripMenuItem,
      this.treesToolStripMenuItem,
      this.balloonToolStripMenuItem,
      this.radialToolStripMenuItem,
      this.seriesparallelToolStripMenuItem,
      this.labelingToolStripMenuItem,
      this.componentsToolStripMenuItem,
      this.partialToolStripMenuItem,
      this.transformToolStripMenuItem});
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
      // 
      // hierarchicToolStripMenuItem
      // 
      this.hierarchicToolStripMenuItem.Name = "hierarchicToolStripMenuItem";
      resources.ApplyResources(this.hierarchicToolStripMenuItem, "hierarchicToolStripMenuItem");
      // 
      // organicToolStripMenuItem
      // 
      this.organicToolStripMenuItem.Name = "organicToolStripMenuItem";
      resources.ApplyResources(this.organicToolStripMenuItem, "organicToolStripMenuItem");
      // 
      // edgeRouterToolStripMenuItem
      // 
      this.edgeRouterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.polylineEdgeRouterToolStripMenuItem,
      this.channelRouterToolStripMenuItem,
      this.organicEdgeRouterToolStripMenuItem,
      this.busRouterToolStripMenuItem,
      this.parallelEdgeRouterToolStripMenuItem});
      this.edgeRouterToolStripMenuItem.Name = "edgeRouterToolStripMenuItem";
      resources.ApplyResources(this.edgeRouterToolStripMenuItem, "edgeRouterToolStripMenuItem");
      // 
      // polylineEdgeRouterToolStripMenuItem
      // 
      this.polylineEdgeRouterToolStripMenuItem.Name = "polylineEdgeRouterToolStripMenuItem";
      resources.ApplyResources(this.polylineEdgeRouterToolStripMenuItem, "polylineEdgeRouterToolStripMenuItem");
      // 
      // channelRouterToolStripMenuItem
      // 
      this.channelRouterToolStripMenuItem.Name = "channelRouterToolStripMenuItem";
      resources.ApplyResources(this.channelRouterToolStripMenuItem, "channelRouterToolStripMenuItem");
      // 
      // organicEdgeRouterToolStripMenuItem
      // 
      this.organicEdgeRouterToolStripMenuItem.Name = "organicEdgeRouterToolStripMenuItem";
      resources.ApplyResources(this.organicEdgeRouterToolStripMenuItem, "organicEdgeRouterToolStripMenuItem");
      // 
      // busRouterToolStripMenuItem
      // 
      this.busRouterToolStripMenuItem.Name = "busRouterToolStripMenuItem";
      resources.ApplyResources(this.busRouterToolStripMenuItem, "busRouterToolStripMenuItem");
      // 
      // parallelEdgeRouterToolStripMenuItem
      // 
      this.parallelEdgeRouterToolStripMenuItem.Name = "parallelEdgeRouterToolStripMenuItem";
      resources.ApplyResources(this.parallelEdgeRouterToolStripMenuItem, "parallelEdgeRouterToolStripMenuItem");
      // 
      // directedOrthogonalToolStripMenuItem
      // 
      this.directedOrthogonalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.orthogonalToolStripMenuItem,
      this.compactToolStripMenuItem});
      this.directedOrthogonalToolStripMenuItem.Name = "directedOrthogonalToolStripMenuItem";
      resources.ApplyResources(this.directedOrthogonalToolStripMenuItem, "directedOrthogonalToolStripMenuItem");
      // 
      // orthogonalToolStripMenuItem
      // 
      this.orthogonalToolStripMenuItem.Name = "orthogonalToolStripMenuItem";
      resources.ApplyResources(this.orthogonalToolStripMenuItem, "orthogonalToolStripMenuItem");
      // 
      // compactToolStripMenuItem
      // 
      this.compactToolStripMenuItem.Name = "compactToolStripMenuItem";
      resources.ApplyResources(this.compactToolStripMenuItem, "compactToolStripMenuItem");
      // 
      // circularToolStripMenuItem
      // 
      this.circularToolStripMenuItem.Name = "circularToolStripMenuItem";
      resources.ApplyResources(this.circularToolStripMenuItem, "circularToolStripMenuItem");
      // 
      // treesToolStripMenuItem
      // 
      this.treesToolStripMenuItem.Name = "treesToolStripMenuItem";
      resources.ApplyResources(this.treesToolStripMenuItem, "treesToolStripMenuItem");
      // 
      // balloonToolStripMenuItem
      // 
      this.balloonToolStripMenuItem.Name = "balloonToolStripMenuItem";
      resources.ApplyResources(this.balloonToolStripMenuItem, "balloonToolStripMenuItem");
      // 
      // radialToolStripMenuItem
      // 
      this.radialToolStripMenuItem.Name = "radialToolStripMenuItem";
      resources.ApplyResources(this.radialToolStripMenuItem, "radialToolStripMenuItem");
      // 
      // seriesparallelToolStripMenuItem
      // 
      this.seriesparallelToolStripMenuItem.Name = "seriesparallelToolStripMenuItem";
      resources.ApplyResources(this.seriesparallelToolStripMenuItem, "seriesparallelToolStripMenuItem");
      // 
      // labelingToolStripMenuItem
      // 
      this.labelingToolStripMenuItem.Name = "labelingToolStripMenuItem";
      resources.ApplyResources(this.labelingToolStripMenuItem, "labelingToolStripMenuItem");
      // 
      // componentsToolStripMenuItem
      // 
      this.componentsToolStripMenuItem.Name = "componentsToolStripMenuItem";
      resources.ApplyResources(this.componentsToolStripMenuItem, "componentsToolStripMenuItem");
      // 
      // partialToolStripMenuItem
      // 
      this.partialToolStripMenuItem.Name = "partialToolStripMenuItem";
      resources.ApplyResources(this.partialToolStripMenuItem, "partialToolStripMenuItem");
      // 
      // transformToolStripMenuItem
      // 
      this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
      resources.ApplyResources(this.transformToolStripMenuItem, "transformToolStripMenuItem");
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.generatorToolStripMenuItem,
      this.configurePortConstraintsToolStripMenuItem,
      this.configureEdgeGroupsToolStripMenuItem});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
      // 
      // generatorToolStripMenuItem
      // 
      this.generatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.randomGeneratorToolStripMenuItem,
      this.treeGeneratorToolStripMenuItem});
      this.generatorToolStripMenuItem.Name = "generatorToolStripMenuItem";
      resources.ApplyResources(this.generatorToolStripMenuItem, "generatorToolStripMenuItem");
      // 
      // randomGeneratorToolStripMenuItem
      // 
      this.randomGeneratorToolStripMenuItem.Name = "randomGeneratorToolStripMenuItem";
      resources.ApplyResources(this.randomGeneratorToolStripMenuItem, "randomGeneratorToolStripMenuItem");
      // 
      // treeGeneratorToolStripMenuItem
      // 
      this.treeGeneratorToolStripMenuItem.Name = "treeGeneratorToolStripMenuItem";
      resources.ApplyResources(this.treeGeneratorToolStripMenuItem, "treeGeneratorToolStripMenuItem");
      // 
      // configurePortConstraintsToolStripMenuItem
      // 
      this.configurePortConstraintsToolStripMenuItem.Name = "configurePortConstraintsToolStripMenuItem";
      resources.ApplyResources(this.configurePortConstraintsToolStripMenuItem, "configurePortConstraintsToolStripMenuItem");
      // 
      // configureEdgeGroupsToolStripMenuItem
      // 
      this.configureEdgeGroupsToolStripMenuItem.Name = "configureEdgeGroupsToolStripMenuItem";
      resources.ApplyResources(this.configureEdgeGroupsToolStripMenuItem, "configureEdgeGroupsToolStripMenuItem");
      // 
      // windowToolStripMenuItem
      // 
      this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.showOverviewToolStripMenuItem,
      this.showPropertiesToolStripMenuItem,
      this.paletteToolStripMenuItem});
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
      // 
      // showOverviewToolStripMenuItem
      // 
      this.showOverviewToolStripMenuItem.CheckOnClick = true;
      this.showOverviewToolStripMenuItem.CheckState = global::Demo.yFiles.GraphEditor.Properties.Settings.Default.ShowOverviewState;
      this.showOverviewToolStripMenuItem.Name = "showOverviewToolStripMenuItem";
      resources.ApplyResources(this.showOverviewToolStripMenuItem, "showOverviewToolStripMenuItem");
      // 
      // showPropertiesToolStripMenuItem
      // 
      this.showPropertiesToolStripMenuItem.Checked = true;
      this.showPropertiesToolStripMenuItem.CheckOnClick = true;
      this.showPropertiesToolStripMenuItem.CheckState = global::Demo.yFiles.GraphEditor.Properties.Settings.Default.ShowPropertyViewState;
      this.showPropertiesToolStripMenuItem.Name = "showPropertiesToolStripMenuItem";
      resources.ApplyResources(this.showPropertiesToolStripMenuItem, "showPropertiesToolStripMenuItem");
      this.showPropertiesToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.showPropertiesToolStripMenuItem_CheckStateChanged);
      // 
      // paletteToolStripMenuItem
      // 
      this.paletteToolStripMenuItem.Checked = true;
      this.paletteToolStripMenuItem.CheckOnClick = true;
      this.paletteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.paletteToolStripMenuItem.Name = "paletteToolStripMenuItem";
      resources.ApplyResources(this.paletteToolStripMenuItem, "paletteToolStripMenuItem");
      this.paletteToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.paletteToolStripMenuItem_CheckStateChanged);
      // 
      // hierarchyToolStripMenuItem
      // 
      this.hierarchyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.groupSelectionToolStripMenuItem,
      this.ungroupSelectionToolStripMenuItem,
      this.toolStripSeparator16,
      this.adjustGroupToolStripMenuItem,
      this.toolStripSeparator14,
      this.collapseGroupToolStripMenuItem,
      this.expandGroupToolStripMenuItem,
      this.toolStripSeparator15,
      this.enterGroupToolStripMenuItem,
      this.exitGroupToolStripMenuItem});
      this.hierarchyToolStripMenuItem.Name = "hierarchyToolStripMenuItem";
      resources.ApplyResources(this.hierarchyToolStripMenuItem, "hierarchyToolStripMenuItem");
      // 
      // groupSelectionToolStripMenuItem
      // 
      this.groupSelectionToolStripMenuItem.Name = "groupSelectionToolStripMenuItem";
      resources.ApplyResources(this.groupSelectionToolStripMenuItem, "groupSelectionToolStripMenuItem");
      // 
      // ungroupSelectionToolStripMenuItem
      // 
      this.ungroupSelectionToolStripMenuItem.Name = "ungroupSelectionToolStripMenuItem";
      resources.ApplyResources(this.ungroupSelectionToolStripMenuItem, "ungroupSelectionToolStripMenuItem");
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      resources.ApplyResources(this.toolStripSeparator16, "toolStripSeparator16");
      // 
      // adjustGroupToolStripMenuItem
      // 
      this.adjustGroupToolStripMenuItem.Name = "adjustGroupToolStripMenuItem";
      resources.ApplyResources(this.adjustGroupToolStripMenuItem, "adjustGroupToolStripMenuItem");
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
      // 
      // collapseGroupToolStripMenuItem
      // 
      this.collapseGroupToolStripMenuItem.Name = "collapseGroupToolStripMenuItem";
      resources.ApplyResources(this.collapseGroupToolStripMenuItem, "collapseGroupToolStripMenuItem");
      // 
      // expandGroupToolStripMenuItem
      // 
      this.expandGroupToolStripMenuItem.Name = "expandGroupToolStripMenuItem";
      resources.ApplyResources(this.expandGroupToolStripMenuItem, "expandGroupToolStripMenuItem");
      // 
      // toolStripSeparator15
      // 
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      resources.ApplyResources(this.toolStripSeparator15, "toolStripSeparator15");
      // 
      // enterGroupToolStripMenuItem
      // 
      this.enterGroupToolStripMenuItem.Name = "enterGroupToolStripMenuItem";
      resources.ApplyResources(this.enterGroupToolStripMenuItem, "enterGroupToolStripMenuItem");
      // 
      // exitGroupToolStripMenuItem
      // 
      this.exitGroupToolStripMenuItem.Name = "exitGroupToolStripMenuItem";
      resources.ApplyResources(this.exitGroupToolStripMenuItem, "exitGroupToolStripMenuItem");
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.quickReferenceToolStripMenuItem,
      this.sampleFilesToolStripMenuItem,
      this.aboutYEdNETToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
      // 
      // quickReferenceToolStripMenuItem
      // 
      this.quickReferenceToolStripMenuItem.Name = "quickReferenceToolStripMenuItem";
      resources.ApplyResources(this.quickReferenceToolStripMenuItem, "quickReferenceToolStripMenuItem");
      // 
      // sampleFilesToolStripMenuItem
      // 
      this.sampleFilesToolStripMenuItem.Name = "sampleFilesToolStripMenuItem";
      resources.ApplyResources(this.sampleFilesToolStripMenuItem, "sampleFilesToolStripMenuItem");
      // 
      // aboutYEdNETToolStripMenuItem
      // 
      this.aboutYEdNETToolStripMenuItem.Name = "aboutYEdNETToolStripMenuItem";
      resources.ApplyResources(this.aboutYEdNETToolStripMenuItem, "aboutYEdNETToolStripMenuItem");
      // 
      // mainButtonsToolStrip
      // 
      resources.ApplyResources(this.mainButtonsToolStrip, "mainButtonsToolStrip");
      this.mainButtonsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
      this.newFileButton,
      this.openFileButton,
      this.saveFileButton,
      this.printButton,
      this.toolStripSeparator9,
      this.cutButton,
      this.copyButton,
      this.pasteButton,
      this.toolStripSeparator7,
      this.undoButton,
      this.redoButton,
      this.toolStripSeparator11,
      this.zoomInButton,
      this.zoomOutButton,
      this.zoomToOriginalSizeButton,
      this.fitContentButton,
      this.setZoomtoolStripComboBox,
      this.toolStripSeparator10,
      this.toggleOrthogonalEdgesButton,
      this.toggleSnaplinesButton,
      this.toggleGridButton,
      this.toggleLassoModeButton});
      this.mainButtonsToolStrip.Name = "mainButtonsToolStrip";
      // 
      // newFileButton
      // 
      this.newFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newFileButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.new_document_16;
      resources.ApplyResources(this.newFileButton, "newFileButton");
      this.newFileButton.Name = "newFileButton";
      // 
      // openFileButton
      // 
      this.openFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openFileButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.open_16;
      resources.ApplyResources(this.openFileButton, "openFileButton");
      this.openFileButton.Name = "openFileButton";
      // 
      // saveFileButton
      // 
      this.saveFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveFileButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.save_16;
      resources.ApplyResources(this.saveFileButton, "saveFileButton");
      this.saveFileButton.Name = "saveFileButton";
      // 
      // printButton
      // 
      this.printButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.printButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.print_16;
      resources.ApplyResources(this.printButton, "printButton");
      this.printButton.Name = "printButton";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
      // 
      // cutButton
      // 
      this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.cutButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.cut2_16;
      resources.ApplyResources(this.cutButton, "cutButton");
      this.cutButton.Name = "cutButton";
      // 
      // copyButton
      // 
      this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.copy_16;
      resources.ApplyResources(this.copyButton, "copyButton");
      this.copyButton.Name = "copyButton";
      // 
      // pasteButton
      // 
      this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.pasteButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.paste_16;
      resources.ApplyResources(this.pasteButton, "pasteButton");
      this.pasteButton.Name = "pasteButton";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.undo_16;
      resources.ApplyResources(this.undoButton, "undoButton");
      this.undoButton.Name = "undoButton";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.redo_16;
      resources.ApplyResources(this.redoButton, "redoButton");
      this.redoButton.Name = "redoButton";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.plus_16;
      resources.ApplyResources(this.zoomInButton, "zoomInButton");
      this.zoomInButton.Name = "zoomInButton";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.minus_16;
      resources.ApplyResources(this.zoomOutButton, "zoomOutButton");
      this.zoomOutButton.Name = "zoomOutButton";
      // 
      // zoomToOriginalSizeButton
      // 
      this.zoomToOriginalSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomToOriginalSizeButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.zoom_original3_16;
      resources.ApplyResources(this.zoomToOriginalSizeButton, "zoomToOriginalSizeButton");
      this.zoomToOriginalSizeButton.Name = "zoomToOriginalSizeButton";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.fit_16;
      resources.ApplyResources(this.fitContentButton, "fitContentButton");
      this.fitContentButton.Name = "fitContentButton";
      // 
      // setZoomtoolStripComboBox
      // 
      this.setZoomtoolStripComboBox.Name = "setZoomtoolStripComboBox";
      resources.ApplyResources(this.setZoomtoolStripComboBox, "setZoomtoolStripComboBox");
      this.setZoomtoolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.setZoomtoolStripComboBox_SelectedIndexChanged);
      this.setZoomtoolStripComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.setZoomtoolStripComboBox_Validating);
      this.setZoomtoolStripComboBox.Validated += new System.EventHandler(this.setZoomtoolStripComboBox_Validated);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
      // 
      // toggleOrthogonalEdgesButton
      // 
      this.toggleOrthogonalEdgesButton.CheckOnClick = true;
      this.toggleOrthogonalEdgesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toggleOrthogonalEdgesButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.orthogonal_editing_16;
      resources.ApplyResources(this.toggleOrthogonalEdgesButton, "toggleOrthogonalEdgesButton");
      this.toggleOrthogonalEdgesButton.Name = "toggleOrthogonalEdgesButton";
      // 
      // toggleSnaplinesButton
      // 
      this.toggleSnaplinesButton.CheckOnClick = true;
      this.toggleSnaplinesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toggleSnaplinesButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.snap_16;
      resources.ApplyResources(this.toggleSnaplinesButton, "toggleSnaplinesButton");
      this.toggleSnaplinesButton.Name = "toggleSnaplinesButton";
      // 
      // toggleGridButton
      // 
      this.toggleGridButton.CheckOnClick = true;
      this.toggleGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toggleGridButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.grid_16;
      resources.ApplyResources(this.toggleGridButton, "toggleGridButton");
      this.toggleGridButton.Name = "toggleGridButton";
      // 
      // toggleLassoModeButton
      // 
      this.toggleLassoModeButton.CheckOnClick = true;
      this.toggleLassoModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toggleLassoModeButton.Image = global::Demo.yFiles.GraphEditor.Properties.Resources.lasso;
      this.toggleLassoModeButton.Name = "toggleLassoModeButton";
      resources.ApplyResources(this.toggleLassoModeButton, "toggleLassoModeButton");
      // 
      // structureViewImages
      // 
      this.structureViewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("structureViewImages.ImageStream")));
      this.structureViewImages.TransparentColor = System.Drawing.Color.Magenta;
      this.structureViewImages.Images.SetKeyName(0, "");
      this.structureViewImages.Images.SetKeyName(1, "");
      this.structureViewImages.Images.SetKeyName(2, "");
      // 
      // openFileDialog
      // 
      this.openFileDialog.DefaultExt = "graphml";
      this.openFileDialog.FileName = "openFileDialog";
      resources.ApplyResources(this.openFileDialog, "openFileDialog");
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.DefaultExt = "graphml";
      resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
      // 
      // panel1
      // 
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Name = "panel1";
      // 
      // comboBox1
      // 
      resources.ApplyResources(this.comboBox1, "comboBox1");
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Name = "comboBox1";
      // 
      // label1
      // 
      resources.ApplyResources(this.label1, "label1");
      this.label1.Name = "label1";
      // 
      // GraphEditorForm
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.toolStripContainer1);
      this.Icon = global::Demo.yFiles.GraphEditor.Properties.Resources.yIcon;
      this.MainMenuStrip = this.mainMenuStrip;
      this.Name = "GraphEditorForm";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GraphEditorForm_FormClosing);
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.toplevel_splitContainer.Panel1.ResumeLayout(false);
      this.toplevel_splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.toplevel_splitContainer)).EndInit();
      this.toplevel_splitContainer.ResumeLayout(false);
      this.inner_splitContainer.Panel1.ResumeLayout(false);
      this.inner_splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.inner_splitContainer)).EndInit();
      this.inner_splitContainer.ResumeLayout(false);
      this.propertyTabControl.ResumeLayout(false);
      this.nodePropertiesTabPage.ResumeLayout(false);
      this.edgePropertiesTabPage.ResumeLayout(false);
      this.labelPropertiesTabPage.ResumeLayout(false);
      this.portPropertiesTabPage.ResumeLayout(false);
      this.palettePanel.ResumeLayout(false);
      this.panel9.ResumeLayout(false);
      this.portStyleContextMenu.ResumeLayout(false);
      this.panel8.ResumeLayout(false);
      this.labelStyleContextMenu.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.edgeStyleContextMenu.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.nodeStyleContextMenu.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel7.ResumeLayout(false);
      this.panel6.ResumeLayout(false);
      this.mainMenuStrip.ResumeLayout(false);
      this.mainMenuStrip.PerformLayout();
      this.mainButtonsToolStrip.ResumeLayout(false);
      this.mainButtonsToolStrip.PerformLayout();
      this.ResumeLayout(false);

    }



    #endregion

    private System.Windows.Forms.MenuStrip mainMenuStrip;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip mainButtonsToolStrip;
    private System.Windows.Forms.ToolStripButton newFileButton;
    private System.Windows.Forms.ToolStripButton openFileButton;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem zoomInToolStripMenuItem;
    private ToolStripMenuItem zoomOutToolStripMenuItem;
    private ToolStripMenuItem fitContentToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
    private System.Windows.Forms.SplitContainer toplevel_splitContainer;
    private yWorks.Controls.GraphControl graphControl;
    private ToolStripMenuItem zoomToOriginalSizeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem undoToolStripMenuItem;
    private ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem copyToolStripMenuItem;
    private ToolStripMenuItem cutToolStripMenuItem;
    private ToolStripMenuItem pasteToolStripMenuItem;
    private ToolStripMenuItem deleteToolStripMenuItem;
    private ToolStripMenuItem reverseToolStripMenuItem;
    private ToolStripMenuItem duplicateToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem selectAllToolStripMenuItem;
    private ToolStripMenuItem clearSelectionToolStripMenuItem;
    private ToolStripMenuItem setZoomLevelToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton saveFileButton;
    private System.Windows.Forms.ToolStripButton printButton;
    private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showOverviewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showPropertiesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutYEdNETToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripButton cutButton;
    private System.Windows.Forms.ToolStripButton copyButton;
    private System.Windows.Forms.ToolStripButton pasteButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton zoomToOriginalSizeButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripComboBox setZoomtoolStripComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private ToolStripMenuItem propertiesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem hierarchyToolStripMenuItem;
    private ToolStripMenuItem groupSelectionToolStripMenuItem;
    private ToolStripMenuItem ungroupSelectionToolStripMenuItem;
    private System.Windows.Forms.ImageList structureViewImages;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripMenuItem resetToFactoryDefaultsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripMenuItem sampleFilesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem generatorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem randomGeneratorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem treeGeneratorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem configurePortConstraintsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem configureEdgeGroupsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
    private ToolStripMenuItem collapseGroupToolStripMenuItem;
    private ToolStripMenuItem expandGroupToolStripMenuItem;
    private ToolStripMenuItem adjustGroupToolStripMenuItem;
    private ToolStripMenuItem enterGroupToolStripMenuItem;
    private ToolStripMenuItem exitGroupToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton toggleOrthogonalEdgesButton;
    private System.Windows.Forms.ToolStripButton toggleSnaplinesButton;
    private System.Windows.Forms.ToolStripButton toggleGridButton;
    private System.Windows.Forms.SplitContainer inner_splitContainer;
    private System.Windows.Forms.ContextMenuStrip nodeStyleContextMenu;
    private System.Windows.Forms.ToolStripMenuItem applyNodeStyleMenuItem;
    private System.Windows.Forms.TabControl propertyTabControl;
    private System.Windows.Forms.TabPage nodePropertiesTabPage;
    private System.Windows.Forms.Panel nodePropertiesPanel;
    private System.Windows.Forms.TabPage edgePropertiesTabPage;
    private System.Windows.Forms.Panel edgePropertiesPanel;
    private System.Windows.Forms.TabPage labelPropertiesTabPage;
    private System.Windows.Forms.Panel labelPropertiesPanel;
    private System.Windows.Forms.TabPage portPropertiesTabPage;
    private System.Windows.Forms.Panel portPropertiesPanel;
    private System.Windows.Forms.ContextMenuStrip edgeStyleContextMenu;
    private System.Windows.Forms.ToolStripMenuItem applyEdgeStyleMenuItem;
    private System.Windows.Forms.ListBox edgeStyleListBox;
    private System.Windows.Forms.ToolStripMenuItem paletteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem quickReferenceToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem hierarchicToolStripMenuItem;
    private ToolStripMenuItem organicToolStripMenuItem;
    private ToolStripMenuItem edgeRouterToolStripMenuItem;
    private ToolStripMenuItem channelRouterToolStripMenuItem;
    private ToolStripMenuItem organicEdgeRouterToolStripMenuItem;
    private ToolStripMenuItem busRouterToolStripMenuItem;
    private ToolStripMenuItem parallelEdgeRouterToolStripMenuItem;
    private ToolStripMenuItem directedOrthogonalToolStripMenuItem;
    private ToolStripMenuItem orthogonalToolStripMenuItem;
    private ToolStripMenuItem compactToolStripMenuItem;
    private ToolStripMenuItem circularToolStripMenuItem;
    private ToolStripMenuItem treesToolStripMenuItem;
    private ToolStripMenuItem balloonToolStripMenuItem;
    private ToolStripMenuItem radialToolStripMenuItem;
    private ToolStripMenuItem seriesparallelToolStripMenuItem;
    private ToolStripMenuItem labelingToolStripMenuItem;
    private ToolStripMenuItem componentsToolStripMenuItem;
    private ToolStripMenuItem partialToolStripMenuItem;
    private ToolStripMenuItem transformToolStripMenuItem;
    private Panel palettePanel;
    private Label label3;
    private Panel panel2;
    private Panel panel7;
    private ListBox groupNodeStyleListBox;
    private Label label7;
    private Panel panel6;
    private ListBox shapeNodeStyleListBox;
    private Label label6;
    private Panel panel5;
    private ListBox peopleStyleListBox;
    private Label label5;
    private Panel panel4;
    private ListBox computersStyleListBox;
    private Label label4;
    private ToolStripMenuItem polylineEdgeRouterToolStripMenuItem;
    private Panel panel9;
    private ListBox portStyleListBox;
    private ContextMenuStrip portStyleContextMenu;
    private ToolStripMenuItem applyPortStyleMenuItem;
    private Label label9;
    private Panel panel8;
    private ListBox labelStyleListBox;
    private ContextMenuStrip labelStyleContextMenu;
    private ToolStripMenuItem applyLabelStyleMenuItem;
    private Label label8;
    private ToolStripButton toggleLassoModeButton;
  }
}

