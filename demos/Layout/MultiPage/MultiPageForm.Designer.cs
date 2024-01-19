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

namespace Demo.yFiles.Layout.MultiPage
{
  partial class MultiPageForm
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
      this.loadingIndicator = new System.Windows.Forms.Panel();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.label4 = new System.Windows.Forms.Label();
      this.stopButton = new System.Windows.Forms.Button();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.prevPageButton = new System.Windows.Forms.ToolStripButton();
      this.pageNumberTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.nextPageButton = new System.Windows.Forms.ToolStripButton();
      this.description = new System.Windows.Forms.RichTextBox();
      this.outerSplitContainer = new System.Windows.Forms.SplitContainer();
      this.innerSplitContainer = new System.Windows.Forms.SplitContainer();
      this.graphControlSplitContainer = new System.Windows.Forms.SplitContainer();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.originalGraphControl = new yWorks.Controls.GraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButtonOriginal = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButtonOriginal = new System.Windows.Forms.ToolStripButton();
      this.fitContentButtonOriginal = new System.Windows.Forms.ToolStripButton();
      this.propertiesPanel = new System.Windows.Forms.Panel();
      this.showInputGraphButton = new System.Windows.Forms.CheckBox();
      this.layoutButton = new System.Windows.Forms.Button();
      this.coreLayoutComboBox = new System.Windows.Forms.ComboBox();
      this.pageHeightTextBox = new System.Windows.Forms.TextBox();
      this.pageWidthTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.inputGraphButton = new System.Windows.Forms.CheckBox();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.loadingIndicator.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.outerSplitContainer.Panel1.SuspendLayout();
      this.outerSplitContainer.Panel2.SuspendLayout();
      this.outerSplitContainer.SuspendLayout();
      this.innerSplitContainer.Panel1.SuspendLayout();
      this.innerSplitContainer.Panel2.SuspendLayout();
      this.innerSplitContainer.SuspendLayout();
      this.graphControlSplitContainer.Panel1.SuspendLayout();
      this.graphControlSplitContainer.Panel2.SuspendLayout();
      this.graphControlSplitContainer.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.propertiesPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.loadingIndicator);
      this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(383, 552);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(383, 583);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
      // 
      // loadingIndicator
      // 
      this.loadingIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.loadingIndicator.BackColor = System.Drawing.SystemColors.ControlDark;
      this.loadingIndicator.Controls.Add(this.flowLayoutPanel1);
      this.loadingIndicator.Location = new System.Drawing.Point(0, 176);
      this.loadingIndicator.Name = "loadingIndicator";
      this.loadingIndicator.Size = new System.Drawing.Size(383, 200);
      this.loadingIndicator.TabIndex = 2;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.flowLayoutPanel1.AutoSize = true;
      this.flowLayoutPanel1.Controls.Add(this.label4);
      this.flowLayoutPanel1.Controls.Add(this.stopButton);
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(159, 76);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(81, 48);
      this.flowLayoutPanel1.TabIndex = 2;
      this.flowLayoutPanel1.WrapContents = false;
      // 
      // label4
      // 
      this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(54, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Loading...";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // stopButton
      // 
      this.stopButton.Location = new System.Drawing.Point(3, 16);
      this.stopButton.Name = "stopButton";
      this.stopButton.Size = new System.Drawing.Size(75, 23);
      this.stopButton.TabIndex = 1;
      this.stopButton.Text = "Stop";
      this.stopButton.UseVisualStyleBackColor = true;
      this.stopButton.Click += new System.EventHandler(this.StopLayoutButtonClick);
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
      this.graphControl.Size = new System.Drawing.Size(383, 552);
      this.graphControl.TabIndex = 1;
      this.graphControl.Text = "graphControl";
      this.graphControl.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.prevPageButton,
            this.pageNumberTextBox,
            this.nextPageButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(383, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(36, 20);
      this.toolStripLabel1.Text = "Page:";
      // 
      // prevPageButton
      // 
      this.prevPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.prevPageButton.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.arrow_left_16;
      this.prevPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.prevPageButton.Name = "prevPageButton";
      this.prevPageButton.Size = new System.Drawing.Size(23, 20);
      this.prevPageButton.Text = "<";
      this.prevPageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      this.prevPageButton.ToolTipText = "Previous Page";
      // 
      // pageNumberTextBox
      // 
      this.pageNumberTextBox.Name = "pageNumberTextBox";
      this.pageNumberTextBox.Size = new System.Drawing.Size(40, 23);
      this.pageNumberTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PageNumberTextBox_KeyDown);
      // 
      // nextPageButton
      // 
      this.nextPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.nextPageButton.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.arrow_right_16;
      this.nextPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.nextPageButton.Name = "nextPageButton";
      this.nextPageButton.Size = new System.Drawing.Size(23, 20);
      this.nextPageButton.Text = ">";
      this.nextPageButton.ToolTipText = "Next Page";
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(250, 583);
      this.description.TabIndex = 2;
      this.description.Text = "";
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
      this.outerSplitContainer.Panel2.Controls.Add(this.innerSplitContainer);
      this.outerSplitContainer.Size = new System.Drawing.Size(844, 583);
      this.outerSplitContainer.SplitterDistance = 250;
      this.outerSplitContainer.TabIndex = 2;
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
      this.innerSplitContainer.Panel1.Controls.Add(this.graphControlSplitContainer);
      // 
      // innerSplitContainer.Panel2
      // 
      this.innerSplitContainer.Panel2.Controls.Add(this.propertiesPanel);
      this.innerSplitContainer.Panel2.Controls.Add(this.label5);
      this.innerSplitContainer.Size = new System.Drawing.Size(590, 583);
      this.innerSplitContainer.SplitterDistance = 350;
      this.innerSplitContainer.TabIndex = 0;
      // 
      // graphControlSplitContainer
      // 
      this.graphControlSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControlSplitContainer.IsSplitterFixed = true;
      this.graphControlSplitContainer.Location = new System.Drawing.Point(0, 0);
      this.graphControlSplitContainer.Name = "graphControlSplitContainer";
      // 
      // graphControlSplitContainer.Panel1
      // 
      this.graphControlSplitContainer.Panel1.Controls.Add(this.toolStripContainer1);
      this.graphControlSplitContainer.Panel1MinSize = 0;
      // 
      // graphControlSplitContainer.Panel2
      // 
      this.graphControlSplitContainer.Panel2.Controls.Add(this.toolStripContainer);
      this.graphControlSplitContainer.Size = new System.Drawing.Size(387, 583);
      this.graphControlSplitContainer.SplitterDistance = 0;
      this.graphControlSplitContainer.TabIndex = 0;
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.originalGraphControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(0, 552);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(0, 583);
      this.toolStripContainer1.TabIndex = 1;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // originalGraphControl
      // 
      this.originalGraphControl.BackColor = System.Drawing.Color.White;
      this.originalGraphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.originalGraphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.originalGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.originalGraphControl.Location = new System.Drawing.Point(0, 0);
      this.originalGraphControl.Name = "originalGraphControl";
      this.originalGraphControl.Size = new System.Drawing.Size(0, 552);
      this.originalGraphControl.TabIndex = 0;
      this.originalGraphControl.Text = "graphControl1";
      viewportLimiter2.Bounds = null;
      this.originalGraphControl.ViewportLimiter = viewportLimiter2;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButtonOriginal,
            this.zoomOutButtonOriginal,
            this.fitContentButtonOriginal});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(0, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      // 
      // zoomInButtonOriginal
      // 
      this.zoomInButtonOriginal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButtonOriginal.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.plus_16;
      this.zoomInButtonOriginal.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButtonOriginal.Name = "zoomInButtonOriginal";
      this.zoomInButtonOriginal.Size = new System.Drawing.Size(23, 20);
      this.zoomInButtonOriginal.Text = "Zoom In";
      // 
      // zoomOutButtonOriginal
      // 
      this.zoomOutButtonOriginal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButtonOriginal.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.minus_16;
      this.zoomOutButtonOriginal.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButtonOriginal.Name = "zoomOutButtonOriginal";
      this.zoomOutButtonOriginal.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButtonOriginal.Text = "Zoom Out";
      // 
      // fitContentButtonOriginal
      // 
      this.fitContentButtonOriginal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButtonOriginal.Image = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.fit_16;
      this.fitContentButtonOriginal.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButtonOriginal.Name = "fitContentButtonOriginal";
      this.fitContentButtonOriginal.Size = new System.Drawing.Size(23, 20);
      this.fitContentButtonOriginal.Text = "Fit Content";
      // 
      // propertiesPanel
      // 
      this.propertiesPanel.Controls.Add(this.showInputGraphButton);
      this.propertiesPanel.Controls.Add(this.layoutButton);
      this.propertiesPanel.Controls.Add(this.coreLayoutComboBox);
      this.propertiesPanel.Controls.Add(this.pageHeightTextBox);
      this.propertiesPanel.Controls.Add(this.pageWidthTextBox);
      this.propertiesPanel.Controls.Add(this.label3);
      this.propertiesPanel.Controls.Add(this.label2);
      this.propertiesPanel.Controls.Add(this.label1);
      this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertiesPanel.Location = new System.Drawing.Point(0, 23);
      this.propertiesPanel.Name = "propertiesPanel";
      this.propertiesPanel.Size = new System.Drawing.Size(199, 560);
      this.propertiesPanel.TabIndex = 0;
      // 
      // showInputGraphButton
      // 
      this.showInputGraphButton.Appearance = System.Windows.Forms.Appearance.Button;
      this.showInputGraphButton.Location = new System.Drawing.Point(7, 123);
      this.showInputGraphButton.Name = "showInputGraphButton";
      this.showInputGraphButton.Size = new System.Drawing.Size(189, 23);
      this.showInputGraphButton.TabIndex = 6;
      this.showInputGraphButton.Text = "Show Input Graph";
      this.showInputGraphButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.showInputGraphButton.UseVisualStyleBackColor = true;
      this.showInputGraphButton.CheckedChanged += new System.EventHandler(this.ShowInputGraph_Click);
      // 
      // layoutButton
      // 
      this.layoutButton.Location = new System.Drawing.Point(7, 94);
      this.layoutButton.Name = "layoutButton";
      this.layoutButton.Size = new System.Drawing.Size(189, 23);
      this.layoutButton.TabIndex = 5;
      this.layoutButton.Text = "Run Layout";
      this.layoutButton.UseVisualStyleBackColor = true;
      this.layoutButton.Click += new System.EventHandler(this.RunLayout_Click);
      // 
      // coreLayoutComboBox
      //
      this.coreLayoutComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.coreLayoutComboBox.FormattingEnabled = true;
      this.coreLayoutComboBox.Location = new System.Drawing.Point(74, 57);
      this.coreLayoutComboBox.Name = "coreLayoutComboBox";
      this.coreLayoutComboBox.Size = new System.Drawing.Size(122, 21);
      this.coreLayoutComboBox.TabIndex = 4;
      // 
      // pageHeightTextBox
      // 
      this.pageHeightTextBox.Location = new System.Drawing.Point(74, 30);
      this.pageHeightTextBox.Name = "pageHeightTextBox";
      this.pageHeightTextBox.Size = new System.Drawing.Size(122, 20);
      this.pageHeightTextBox.TabIndex = 3;
      this.pageHeightTextBox.Text = "800";
      // 
      // pageWidthTextBox
      // 
      this.pageWidthTextBox.Location = new System.Drawing.Point(74, 4);
      this.pageWidthTextBox.Name = "pageWidthTextBox";
      this.pageWidthTextBox.Size = new System.Drawing.Size(122, 20);
      this.pageWidthTextBox.TabIndex = 2;
      this.pageWidthTextBox.Text = "800";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(4, 60);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(64, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "Core Layout";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 33);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(66, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Page Height";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 7);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Page Width";
      // 
      // label5
      // 
      this.label5.Dock = System.Windows.Forms.DockStyle.Top;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(0, 0);
      this.label5.Name = "label5";
      this.label5.Padding = new System.Windows.Forms.Padding(5);
      this.label5.Size = new System.Drawing.Size(199, 23);
      this.label5.TabIndex = 7;
      this.label5.Text = "Layout Settings";
      // 
      // inputGraphButton
      // 
      this.inputGraphButton.Appearance = System.Windows.Forms.Appearance.Button;
      this.inputGraphButton.Location = new System.Drawing.Point(7, 123);
      this.inputGraphButton.Name = "inputGraphButton";
      this.inputGraphButton.Size = new System.Drawing.Size(189, 23);
      this.inputGraphButton.TabIndex = 6;
      this.inputGraphButton.Text = "Show Input Graph";
      this.inputGraphButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.inputGraphButton.UseVisualStyleBackColor = true;
      this.inputGraphButton.CheckedChanged += new System.EventHandler(this.ShowInputGraph_Click);
      // 
      // MultiPageForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(844, 583);
      this.Controls.Add(this.outerSplitContainer);
      this.Icon = global::Demo.yFiles.Layout.MultiPage.Properties.Resources.yIcon;
      this.Name = "MultiPageForm";
      this.Text = "MultiPage Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoad);
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.loadingIndicator.ResumeLayout(false);
      this.loadingIndicator.PerformLayout();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.outerSplitContainer.Panel1.ResumeLayout(false);
      this.outerSplitContainer.Panel2.ResumeLayout(false);
      this.outerSplitContainer.ResumeLayout(false);
      this.innerSplitContainer.Panel1.ResumeLayout(false);
      this.innerSplitContainer.Panel2.ResumeLayout(false);
      this.innerSplitContainer.ResumeLayout(false);
      this.graphControlSplitContainer.Panel1.ResumeLayout(false);
      this.graphControlSplitContainer.Panel2.ResumeLayout(false);
      this.graphControlSplitContainer.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.propertiesPanel.ResumeLayout(false);
      this.propertiesPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.SplitContainer outerSplitContainer;
    private System.Windows.Forms.SplitContainer innerSplitContainer;
    private System.Windows.Forms.SplitContainer graphControlSplitContainer;
    private System.Windows.Forms.Panel propertiesPanel;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private yWorks.Controls.GraphControl originalGraphControl;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton zoomInButtonOriginal;
    private System.Windows.Forms.ToolStripButton zoomOutButtonOriginal;
    private System.Windows.Forms.ToolStripButton fitContentButtonOriginal;
    private System.Windows.Forms.TextBox pageWidthTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button layoutButton;
    private System.Windows.Forms.ComboBox coreLayoutComboBox;
    private System.Windows.Forms.TextBox pageHeightTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton prevPageButton;
    private System.Windows.Forms.ToolStripTextBox pageNumberTextBox;
    private System.Windows.Forms.ToolStripButton nextPageButton;
    private System.Windows.Forms.Panel loadingIndicator;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox showInputGraphButton;
    private System.Windows.Forms.CheckBox inputGraphButton;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button stopButton;
  }
}

