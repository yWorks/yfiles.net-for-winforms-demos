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

namespace Demo.yFiles.Layout.LayoutStyles
{
  partial class LayoutStylesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutStylesForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
            this.overviewLabel = new System.Windows.Forms.Label();
            this.splitContainerGraphAndOptions = new System.Windows.Forms.SplitContainer();
            this.graphControl = new yWorks.Controls.GraphControl();
            this.optionPanel = new System.Windows.Forms.Panel();
            this.configEditorPanel = new System.Windows.Forms.Panel();
            this.configurationEditor = new Demo.yFiles.Toolkit.OptionHandler.ConfigurationEditor();
            this.applyResetPanel = new System.Windows.Forms.Panel();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.layoutStylePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.LayoutComboBox = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newGraphButton = new System.Windows.Forms.ToolStripButton();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.previousButton = new System.Windows.Forms.ToolStripButton();
            this.SampleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.nextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoom100Button = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.fitContentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CutButton = new System.Windows.Forms.ToolStripButton();
            this.copyButton = new System.Windows.Forms.ToolStripButton();
            this.PasteButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToggleSnapLinesButton = new System.Windows.Forms.ToolStripButton();
            this.ToggleOrthogonalEdgesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.GroupSelectionButton = new System.Windows.Forms.ToolStripButton();
            this.UngroupSelectionButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.GenerateRandomNodeLabelsButton = new System.Windows.Forms.ToolStripButton();
            this.GenerateRandomEdgeLabelsButton = new System.Windows.Forms.ToolStripButton();
            this.RemoveAllLabelsButton = new System.Windows.Forms.ToolStripButton();
            this.GenerateRandomEdgeThicknessButton = new System.Windows.Forms.ToolStripButton();
            this.ResetAllEdgeThicknessButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.GenerateRandomEdgeDirectionsButton = new System.Windows.Forms.ToolStripButton();
            this.ResetAllEdgeDirectionsButton = new System.Windows.Forms.ToolStripButton();
            this.nodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markAsSourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsTargetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGraphAndOptions)).BeginInit();
            this.splitContainerGraphAndOptions.Panel1.SuspendLayout();
            this.splitContainerGraphAndOptions.Panel2.SuspendLayout();
            this.splitContainerGraphAndOptions.SuspendLayout();
            this.optionPanel.SuspendLayout();
            this.configEditorPanel.SuspendLayout();
            this.applyResetPanel.SuspendLayout();
            this.layoutStylePanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.nodeContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1095, 627);
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.descriptionTextBox);
            this.splitContainer2.Panel1.Controls.Add(this.descriptionLabel);
            this.splitContainer2.Panel1.Controls.Add(this.graphOverviewControl);
            this.splitContainer2.Panel1.Controls.Add(this.overviewLabel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainerGraphAndOptions);
            this.splitContainer2.Size = new System.Drawing.Size(1095, 627);
            this.splitContainer2.SplitterDistance = 312;
            this.splitContainer2.TabIndex = 1;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(0, 234);
            this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(10);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(312, 393);
            this.descriptionTextBox.TabIndex = 5;
            this.descriptionTextBox.Text = "";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.BackColor = System.Drawing.Color.Gray;
            this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.descriptionLabel.ForeColor = System.Drawing.Color.White;
            this.descriptionLabel.Location = new System.Drawing.Point(0, 204);
            this.descriptionLabel.MinimumSize = new System.Drawing.Size(0, 30);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(312, 30);
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "Description";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // graphOverviewControl
            // 
            this.graphOverviewControl.AutoDrag = false;
            this.graphOverviewControl.BackColor = System.Drawing.Color.White;
            this.graphOverviewControl.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.graphOverviewControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.graphOverviewControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
            this.graphOverviewControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.graphOverviewControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
            this.graphOverviewControl.GraphControl = null;
            this.graphOverviewControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
            this.graphOverviewControl.Location = new System.Drawing.Point(0, 30);
            this.graphOverviewControl.MouseWheelBehavior = yWorks.Controls.MouseWheelBehaviors.None;
            this.graphOverviewControl.Name = "graphOverviewControl";
            this.graphOverviewControl.Size = new System.Drawing.Size(312, 174);
            this.graphOverviewControl.TabIndex = 2;
            this.graphOverviewControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
            // 
            // overviewLabel
            // 
            this.overviewLabel.BackColor = System.Drawing.Color.Gray;
            this.overviewLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.overviewLabel.ForeColor = System.Drawing.Color.White;
            this.overviewLabel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.overviewLabel.Location = new System.Drawing.Point(0, 0);
            this.overviewLabel.MinimumSize = new System.Drawing.Size(0, 30);
            this.overviewLabel.Name = "overviewLabel";
            this.overviewLabel.Size = new System.Drawing.Size(312, 30);
            this.overviewLabel.TabIndex = 1;
            this.overviewLabel.Text = "Overview";
            this.overviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainerGraphAndOptions
            // 
            this.splitContainerGraphAndOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGraphAndOptions.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerGraphAndOptions.IsSplitterFixed = true;
            this.splitContainerGraphAndOptions.Location = new System.Drawing.Point(0, 0);
            this.splitContainerGraphAndOptions.Name = "splitContainerGraphAndOptions";
            // 
            // splitContainerGraphAndOptions.Panel1
            // 
            this.splitContainerGraphAndOptions.Panel1.Controls.Add(this.graphControl);
            // 
            // splitContainerGraphAndOptions.Panel2
            // 
            this.splitContainerGraphAndOptions.Panel2.Controls.Add(this.optionPanel);
            this.splitContainerGraphAndOptions.Size = new System.Drawing.Size(779, 627);
            this.splitContainerGraphAndOptions.SplitterDistance = 400;
            this.splitContainerGraphAndOptions.TabIndex = 1;
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
            this.graphControl.Size = new System.Drawing.Size(463, 627);
            this.graphControl.TabIndex = 0;
            // 
            // optionPanel
            // 
            this.optionPanel.Controls.Add(this.configEditorPanel);
            this.optionPanel.Controls.Add(this.applyResetPanel);
            this.optionPanel.Controls.Add(this.layoutStylePanel);
            this.optionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionPanel.Location = new System.Drawing.Point(0, 0);
            this.optionPanel.Name = "optionPanel";
            this.optionPanel.Size = new System.Drawing.Size(312, 627);
            this.optionPanel.TabIndex = 0;
            // 
            // configEditorPanel
            // 
            this.configEditorPanel.Controls.Add(this.configurationEditor);
            this.configEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configEditorPanel.Location = new System.Drawing.Point(0, 50);
            this.configEditorPanel.Name = "configEditorPanel";
            this.configEditorPanel.Size = new System.Drawing.Size(312, 537);
            this.configEditorPanel.TabIndex = 1;
            // 
            // configurationEditor
            // 
            this.configurationEditor.AutoSize = true;
            this.configurationEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.configurationEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationEditor.Location = new System.Drawing.Point(0, 0);
            this.configurationEditor.MaximumSize = new System.Drawing.Size(999999, 999999);
            this.configurationEditor.Name = "configurationEditor";
            this.configurationEditor.Size = new System.Drawing.Size(312, 537);
            this.configurationEditor.TabIndex = 1;
            // 
            // applyResetPanel
            // 
            this.applyResetPanel.Controls.Add(this.ResetButton);
            this.applyResetPanel.Controls.Add(this.ApplyButton);
            this.applyResetPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.applyResetPanel.Location = new System.Drawing.Point(0, 587);
            this.applyResetPanel.Name = "applyResetPanel";
            this.applyResetPanel.Size = new System.Drawing.Size(312, 40);
            this.applyResetPanel.TabIndex = 5;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(164, 9);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(126, 23);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButtonClick);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ApplyButton.Location = new System.Drawing.Point(21, 9);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(137, 23);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButtonClick);
            // 
            // layoutStylePanel
            // 
            this.layoutStylePanel.BackColor = System.Drawing.Color.Gray;
            this.layoutStylePanel.Controls.Add(this.label1);
            this.layoutStylePanel.Controls.Add(this.LayoutComboBox);
            this.layoutStylePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutStylePanel.Location = new System.Drawing.Point(0, 0);
            this.layoutStylePanel.Name = "layoutStylePanel";
            this.layoutStylePanel.Size = new System.Drawing.Size(312, 50);
            this.layoutStylePanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Layout Style";
            // 
            // LayoutComboBox
            // 
            this.LayoutComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayoutComboBox.FormattingEnabled = true;
            this.LayoutComboBox.Location = new System.Drawing.Point(18, 23);
            this.LayoutComboBox.Name = "LayoutComboBox";
            this.LayoutComboBox.Size = new System.Drawing.Size(272, 21);
            this.LayoutComboBox.TabIndex = 0;
            this.LayoutComboBox.SelectedIndexChanged += new System.EventHandler(this.OnLayoutChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGraphButton,
            this.openButton,
            this.SaveButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.previousButton,
            this.SampleComboBox,
            this.nextButton,
            this.toolStripSeparator3,
            this.zoomInButton,
            this.zoom100Button,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.CutButton,
            this.copyButton,
            this.PasteButton,
            this.DeleteButton,
            this.toolStripSeparator6,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator4,
            this.ToggleSnapLinesButton,
            this.ToggleOrthogonalEdgesButton,
            this.toolStripSeparator5,
            this.GroupSelectionButton,
            this.UngroupSelectionButton,
            this.toolStripSeparator7,
            this.GenerateRandomNodeLabelsButton,
            this.GenerateRandomEdgeLabelsButton,
            this.RemoveAllLabelsButton,
            this.GenerateRandomEdgeThicknessButton,
            this.ResetAllEdgeThicknessButton,
            this.toolStripSeparator8,
            this.GenerateRandomEdgeDirectionsButton,
            this.ResetAllEdgeDirectionsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
            this.toolStrip1.Size = new System.Drawing.Size(1095, 31);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newGraphButton
            // 
            this.newGraphButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newGraphButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.new_document_16;
            this.newGraphButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newGraphButton.Name = "newGraphButton";
            this.newGraphButton.Size = new System.Drawing.Size(23, 20);
            this.newGraphButton.Text = "New Graph";
            this.newGraphButton.Click += new System.EventHandler(this.NewFileButtonClick);
            // 
            // openButton
            // 
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.open_16;
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(23, 20);
            this.openButton.Text = "Open";
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.save_16;
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(23, 20);
            this.SaveButton.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(81, 20);
            this.toolStripLabel1.Text = "Sample Graph";
            // 
            // previousButton
            // 
            this.previousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.arrow_left_16;
            this.previousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(23, 20);
            this.previousButton.Text = "Previous sample";
            this.previousButton.Click += new System.EventHandler(this.PreviousSample_OnClick);
            // 
            // SampleComboBox
            // 
            this.SampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SampleComboBox.Name = "SampleComboBox";
            this.SampleComboBox.Size = new System.Drawing.Size(230, 23);
            this.SampleComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSampleChanged);
            // 
            // nextButton
            // 
            this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.arrow_right_16;
            this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(23, 20);
            this.nextButton.Text = "Next sample";
            this.nextButton.Click += new System.EventHandler(this.NextSample_OnClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.plus_16;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 20);
            this.zoomInButton.Text = "Zoom In";
            // 
            // zoom100Button
            // 
            this.zoom100Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoom100Button.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.zoom_original3_16;
            this.zoom100Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoom100Button.Name = "zoom100Button";
            this.zoom100Button.Size = new System.Drawing.Size(23, 20);
            this.zoom100Button.Text = "1:1";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.minus_16;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
            this.zoomOutButton.Text = "Zoom Out";
            // 
            // fitContentButton
            // 
            this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fitContentButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.fit_16;
            this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fitContentButton.Name = "fitContentButton";
            this.fitContentButton.Size = new System.Drawing.Size(23, 20);
            this.fitContentButton.Text = "Fit Content";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // CutButton
            // 
            this.CutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.cut2_16;
            this.CutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(23, 20);
            this.CutButton.Text = "Cut";
            // 
            // copyButton
            // 
            this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.copy_16;
            this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(23, 20);
            this.copyButton.Text = "Copy";
            // 
            // PasteButton
            // 
            this.PasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.paste_16;
            this.PasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(23, 20);
            this.PasteButton.Text = "Paste";
            // 
            // DeleteButton
            // 
            this.DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.delete3_16;
            this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(23, 20);
            this.DeleteButton.Text = "Delete";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 23);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.undo_16;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 20);
            this.undoButton.Text = "Undo";
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.redo_16;
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
            // ToggleSnapLinesButton
            // 
            this.ToggleSnapLinesButton.CheckOnClick = true;
            this.ToggleSnapLinesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToggleSnapLinesButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.snap_16;
            this.ToggleSnapLinesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToggleSnapLinesButton.Name = "ToggleSnapLinesButton";
            this.ToggleSnapLinesButton.Size = new System.Drawing.Size(23, 20);
            this.ToggleSnapLinesButton.Text = "Toggle Snap Lines";
            this.ToggleSnapLinesButton.Click += new System.EventHandler(this.ToggleSnapLines);
            // 
            // ToggleOrthogonalEdgesButton
            // 
            this.ToggleOrthogonalEdgesButton.CheckOnClick = true;
            this.ToggleOrthogonalEdgesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToggleOrthogonalEdgesButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.orthogonal_editing_16;
            this.ToggleOrthogonalEdgesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToggleOrthogonalEdgesButton.Name = "ToggleOrthogonalEdgesButton";
            this.ToggleOrthogonalEdgesButton.Size = new System.Drawing.Size(23, 20);
            this.ToggleOrthogonalEdgesButton.Text = "Toggle Orthogonal Edges";
            this.ToggleOrthogonalEdgesButton.Click += new System.EventHandler(this.ToggleOrthogonalEdges);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
            // 
            // GroupSelectionButton
            // 
            this.GroupSelectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GroupSelectionButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.group_16;
            this.GroupSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GroupSelectionButton.Name = "GroupSelectionButton";
            this.GroupSelectionButton.Size = new System.Drawing.Size(23, 20);
            this.GroupSelectionButton.Text = "Group Selection";
            // 
            // UngroupSelectionButton
            // 
            this.UngroupSelectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UngroupSelectionButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.ungroup_16;
            this.UngroupSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UngroupSelectionButton.Name = "UngroupSelectionButton";
            this.UngroupSelectionButton.Size = new System.Drawing.Size(23, 20);
            this.UngroupSelectionButton.Text = "Ungroup Selection";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 23);
            // 
            // GenerateRandomNodeLabelsButton
            // 
            this.GenerateRandomNodeLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GenerateRandomNodeLabelsButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.nodelabel_16;
            this.GenerateRandomNodeLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GenerateRandomNodeLabelsButton.Name = "GenerateRandomNodeLabelsButton";
            this.GenerateRandomNodeLabelsButton.Size = new System.Drawing.Size(23, 20);
            this.GenerateRandomNodeLabelsButton.Text = "Random Node Labels";
            this.GenerateRandomNodeLabelsButton.Click += new System.EventHandler(this.GenerateRandomNodeLabels);
            // 
            // GenerateRandomEdgeLabelsButton
            // 
            this.GenerateRandomEdgeLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GenerateRandomEdgeLabelsButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.edgelabel_16;
            this.GenerateRandomEdgeLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GenerateRandomEdgeLabelsButton.Name = "GenerateRandomEdgeLabelsButton";
            this.GenerateRandomEdgeLabelsButton.Size = new System.Drawing.Size(23, 20);
            this.GenerateRandomEdgeLabelsButton.Text = "Random Edge Labels";
            this.GenerateRandomEdgeLabelsButton.Click += new System.EventHandler(this.GenerateRandomEdgeLabels);
            // 
            // RemoveAllLabelsButton
            // 
            this.RemoveAllLabelsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveAllLabelsButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.delete2_16;
            this.RemoveAllLabelsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveAllLabelsButton.Name = "RemoveAllLabelsButton";
            this.RemoveAllLabelsButton.Size = new System.Drawing.Size(23, 20);
            this.RemoveAllLabelsButton.Text = "Remove All Labels";
            this.RemoveAllLabelsButton.Click += new System.EventHandler(this.RemoveAllLabels);
            // 
            // GenerateRandomEdgeThicknessButton
            // 
            this.GenerateRandomEdgeThicknessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GenerateRandomEdgeThicknessButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.edge_thickness_16;
            this.GenerateRandomEdgeThicknessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GenerateRandomEdgeThicknessButton.Name = "GenerateRandomEdgeThicknessButton";
            this.GenerateRandomEdgeThicknessButton.Size = new System.Drawing.Size(23, 20);
            this.GenerateRandomEdgeThicknessButton.Text = "Generate Random Thickness";
            this.GenerateRandomEdgeThicknessButton.Click += new System.EventHandler(this.GenerateRandomEdgeThickness);
            // 
            // ResetAllEdgeThicknessButton
            // 
            this.ResetAllEdgeThicknessButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetAllEdgeThicknessButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.delete2_16;
            this.ResetAllEdgeThicknessButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetAllEdgeThicknessButton.Name = "ResetAllEdgeThicknessButton";
            this.ResetAllEdgeThicknessButton.Size = new System.Drawing.Size(23, 20);
            this.ResetAllEdgeThicknessButton.Text = "Reset All Edge Thickness";
            this.ResetAllEdgeThicknessButton.Click += new System.EventHandler(this.ResetEdgeThickness);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 23);
            // 
            // GenerateRandomEdgeDirectionsButton
            // 
            this.GenerateRandomEdgeDirectionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GenerateRandomEdgeDirectionsButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.edge_direction_16;
            this.GenerateRandomEdgeDirectionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GenerateRandomEdgeDirectionsButton.Name = "GenerateRandomEdgeDirectionsButton";
            this.GenerateRandomEdgeDirectionsButton.Size = new System.Drawing.Size(23, 20);
            this.GenerateRandomEdgeDirectionsButton.Text = "Generate Random Edge Directions";
            this.GenerateRandomEdgeDirectionsButton.Click += new System.EventHandler(this.GenerateRandomEdgeDirectedness);
            // 
            // ResetAllEdgeDirectionsButton
            // 
            this.ResetAllEdgeDirectionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetAllEdgeDirectionsButton.Image = global::Demo.yFiles.Layout.LayoutStyles.Properties.Resources.delete2_16;
            this.ResetAllEdgeDirectionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetAllEdgeDirectionsButton.Name = "ResetAllEdgeDirectionsButton";
            this.ResetAllEdgeDirectionsButton.Size = new System.Drawing.Size(23, 20);
            this.ResetAllEdgeDirectionsButton.Text = "Reset All Edge Directions";
            this.ResetAllEdgeDirectionsButton.Click += new System.EventHandler(this.ResetEdgeDirectedness);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // LayoutStylesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 658);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LayoutStylesForm";
            this.Text = "Layout Styles Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnLoaded);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainerGraphAndOptions.Panel1.ResumeLayout(false);
            this.splitContainerGraphAndOptions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGraphAndOptions)).EndInit();
            this.splitContainerGraphAndOptions.ResumeLayout(false);
            this.optionPanel.ResumeLayout(false);
            this.configEditorPanel.ResumeLayout(false);
            this.configEditorPanel.PerformLayout();
            this.applyResetPanel.ResumeLayout(false);
            this.layoutStylePanel.ResumeLayout(false);
            this.layoutStylePanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.nodeContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ContextMenuStrip nodeContextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem markAsSourceMenuItem;
    private System.Windows.Forms.ToolStripMenuItem markAsTargetMenuItem;
    private ToolStripContainer toolStripContainer1;
    private ToolStrip toolStrip1;
    private ToolStripButton zoomInButton;
    private ToolStripButton zoomOutButton;
    private ToolStripButton fitContentButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton undoButton;
    private ToolStripButton redoButton;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton previousButton;
    private ToolStripComboBox SampleComboBox;
    private ToolStripButton nextButton;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton newGraphButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripButton openButton;
    private ToolStripButton SaveButton;
    private ToolStripLabel toolStripLabel1;
    private ToolStripButton zoom100Button;
    private ToolStripButton CutButton;
    private ToolStripButton copyButton;
    private ToolStripButton PasteButton;
    private ToolStripButton DeleteButton;
    private ToolStripButton ToggleSnapLinesButton;
    private ToolStripButton ToggleOrthogonalEdgesButton;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton GroupSelectionButton;
    private ToolStripButton UngroupSelectionButton;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton GenerateRandomNodeLabelsButton;
    private ToolStripButton GenerateRandomEdgeLabelsButton;
    private ToolStripButton RemoveAllLabelsButton;
    private ToolStripButton GenerateRandomEdgeThicknessButton;
    private ToolStripButton ResetAllEdgeThicknessButton;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripButton GenerateRandomEdgeDirectionsButton;
    private ToolStripButton ResetAllEdgeDirectionsButton;
    private SplitContainer splitContainer2;
    private SplitContainer splitContainerGraphAndOptions;
    private Panel optionPanel;
    private ComboBox LayoutComboBox;
    private ContextMenuStrip contextMenuStrip1;
    private yWorks.Controls.GraphControl graphControl;
    private Toolkit.OptionHandler.ConfigurationEditor configurationEditor;
    private Button ResetButton;
    private Button ApplyButton;
    private Panel applyResetPanel;
    private Panel layoutStylePanel;
    private Panel configEditorPanel;
    private Label label1;
    private Label overviewLabel;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private Label descriptionLabel;
    private RichTextBox descriptionTextBox;
  }
}

