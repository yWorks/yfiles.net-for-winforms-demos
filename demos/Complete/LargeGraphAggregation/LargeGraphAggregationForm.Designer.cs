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

namespace Demo.yFiles.Complete.LargeGraphAggregation
{
  partial class LargeGraphAggregationForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LargeGraphAggregationForm));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
      this.overviewLabel = new System.Windows.Forms.Label();
      this.splitContainerGraphAndOptions = new System.Windows.Forms.SplitContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.optionPanel = new System.Windows.Forms.Panel();
      this.editorPanel = new System.Windows.Forms.Panel();
      this.layoutStyleComboBox = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.switchViewButton = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.labelNodeLabel = new System.Windows.Forms.Label();
      this.labelWeightSum = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.labelCount = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.labelOriginalEdges = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.labelOriginalNodes = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.settingsBox = new System.Windows.Forms.GroupBox();
      this.runButton = new System.Windows.Forms.Button();
      this.configurationEditor = new Demo.yFiles.Toolkit.OptionHandler.ConfigurationEditor();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoom100Button = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.graphLoadingBar = new System.Windows.Forms.ProgressBar();
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
      this.editorPanel.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.settingsBox.SuspendLayout();
      this.toolStrip1.SuspendLayout();
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
      this.optionPanel.Controls.Add(this.editorPanel);
      this.optionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.optionPanel.Location = new System.Drawing.Point(0, 0);
      this.optionPanel.Name = "optionPanel";
      this.optionPanel.Size = new System.Drawing.Size(312, 627);
      this.optionPanel.TabIndex = 0;
      // 
      // editorPanel
      // 
      this.editorPanel.Controls.Add(this.layoutStyleComboBox);
      this.editorPanel.Controls.Add(this.label3);
      this.editorPanel.Controls.Add(this.switchViewButton);
      this.editorPanel.Controls.Add(this.groupBox2);
      this.editorPanel.Controls.Add(this.groupBox1);
      this.editorPanel.Controls.Add(this.settingsBox);
      this.editorPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.editorPanel.Location = new System.Drawing.Point(0, 0);
      this.editorPanel.Name = "editorPanel";
      this.editorPanel.Size = new System.Drawing.Size(312, 627);
      this.editorPanel.TabIndex = 4;
      // 
      // layoutStyleComboBox
      // 
      this.layoutStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.layoutStyleComboBox.FormattingEnabled = true;
      this.layoutStyleComboBox.Items.AddRange(new object[] {
            "Balloon",
            "Cactus"});
      this.layoutStyleComboBox.Location = new System.Drawing.Point(24, 26);
      this.layoutStyleComboBox.Name = "layoutStyleComboBox";
      this.layoutStyleComboBox.Size = new System.Drawing.Size(195, 21);
      this.layoutStyleComboBox.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(9, 10);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(65, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Layout Style";
      // 
      // switchViewButton
      // 
      this.switchViewButton.Location = new System.Drawing.Point(6, 580);
      this.switchViewButton.Name = "switchViewButton";
      this.switchViewButton.Size = new System.Drawing.Size(303, 23);
      this.switchViewButton.TabIndex = 5;
      this.switchViewButton.Text = "Switch To Filtered View";
      this.switchViewButton.UseVisualStyleBackColor = true;
      this.switchViewButton.Click += new System.EventHandler(this.SwitchViewButtonClick);
      // 
      // groupBox2
      // 
      this.groupBox2.BackColor = System.Drawing.Color.White;
      this.groupBox2.Controls.Add(this.labelNodeLabel);
      this.groupBox2.Controls.Add(this.labelWeightSum);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.labelCount);
      this.groupBox2.Controls.Add(this.label9);
      this.groupBox2.Location = new System.Drawing.Point(3, 432);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(306, 71);
      this.groupBox2.TabIndex = 4;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Current Node";
      // 
      // labelNodeLabel
      // 
      this.labelNodeLabel.AutoSize = true;
      this.labelNodeLabel.Location = new System.Drawing.Point(8, 16);
      this.labelNodeLabel.Name = "labelNodeLabel";
      this.labelNodeLabel.Size = new System.Drawing.Size(0, 13);
      this.labelNodeLabel.TabIndex = 4;
      // 
      // labelWeightSum
      // 
      this.labelWeightSum.AutoSize = true;
      this.labelWeightSum.Location = new System.Drawing.Point(142, 42);
      this.labelWeightSum.Name = "labelWeightSum";
      this.labelWeightSum.Size = new System.Drawing.Size(0, 13);
      this.labelWeightSum.TabIndex = 3;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(7, 42);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(129, 13);
      this.label7.TabIndex = 2;
      this.label7.Text = "Descendant Weight Sum:";
      // 
      // labelCount
      // 
      this.labelCount.AutoSize = true;
      this.labelCount.Location = new System.Drawing.Point(142, 29);
      this.labelCount.Name = "labelCount";
      this.labelCount.Size = new System.Drawing.Size(0, 13);
      this.labelCount.TabIndex = 1;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(8, 29);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(99, 13);
      this.label9.TabIndex = 0;
      this.label9.Text = "Descendant Count:";
      // 
      // groupBox1
      // 
      this.groupBox1.BackColor = System.Drawing.Color.White;
      this.groupBox1.Controls.Add(this.labelOriginalEdges);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.labelOriginalNodes);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(2, 509);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(306, 65);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Graph Info";
      // 
      // labelOriginalEdges
      // 
      this.labelOriginalEdges.AutoSize = true;
      this.labelOriginalEdges.Location = new System.Drawing.Point(94, 37);
      this.labelOriginalEdges.Name = "labelOriginalEdges";
      this.labelOriginalEdges.Size = new System.Drawing.Size(12, 13);
      this.labelOriginalEdges.TabIndex = 3;
      this.labelOriginalEdges.Text = "/";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(8, 37);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(78, 13);
      this.label4.TabIndex = 2;
      this.label4.Text = "Original Edges:";
      // 
      // labelOriginalNodes
      // 
      this.labelOriginalNodes.AutoSize = true;
      this.labelOriginalNodes.Location = new System.Drawing.Point(94, 20);
      this.labelOriginalNodes.Name = "labelOriginalNodes";
      this.labelOriginalNodes.Size = new System.Drawing.Size(12, 13);
      this.labelOriginalNodes.TabIndex = 1;
      this.labelOriginalNodes.Text = "/";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 20);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(79, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Original Nodes:";
      // 
      // settingsBox
      // 
      this.settingsBox.BackColor = System.Drawing.Color.White;
      this.settingsBox.Controls.Add(this.graphLoadingBar);
      this.settingsBox.Controls.Add(this.runButton);
      this.settingsBox.Controls.Add(this.configurationEditor);
      this.settingsBox.Location = new System.Drawing.Point(3, 53);
      this.settingsBox.Name = "settingsBox";
      this.settingsBox.Size = new System.Drawing.Size(309, 373);
      this.settingsBox.TabIndex = 2;
      this.settingsBox.TabStop = false;
      this.settingsBox.Text = "Aggregation Properties";
      // 
      // runButton
      // 
      this.runButton.Location = new System.Drawing.Point(9, 325);
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(294, 23);
      this.runButton.TabIndex = 1;
      this.runButton.Text = "Run";
      this.runButton.UseVisualStyleBackColor = true;
      this.runButton.Click += new System.EventHandler(this.RunAggregation);
      // 
      // configurationEditor
      // 
      this.configurationEditor.AutoSize = true;
      this.configurationEditor.BackColor = System.Drawing.Color.White;
      this.configurationEditor.Location = new System.Drawing.Point(6, 19);
      this.configurationEditor.Name = "configurationEditor";
      this.configurationEditor.Size = new System.Drawing.Size(294, 300);
      this.configurationEditor.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoom100Button,
            this.zoomOutButton,
            this.fitContentButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(1095, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Complete.LargeGraphAggregation.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoom100Button
      // 
      this.zoom100Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoom100Button.Image = global::Demo.yFiles.Complete.LargeGraphAggregation.Properties.Resources.zoom_original3_16;
      this.zoom100Button.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoom100Button.Name = "zoom100Button";
      this.zoom100Button.Size = new System.Drawing.Size(23, 20);
      this.zoom100Button.Text = "1:1";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Complete.LargeGraphAggregation.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Complete.LargeGraphAggregation.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.Text = "Fit Content";
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
      // 
      // graphLoadingBar
      // 
      this.graphLoadingBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.graphLoadingBar.Location = new System.Drawing.Point(6, 351);
      this.graphLoadingBar.Name = "graphLoadingBar";
      this.graphLoadingBar.Size = new System.Drawing.Size(297, 15);
      this.graphLoadingBar.TabIndex = 7;
      // 
      // LargeGraphAggregationForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1095, 658);
      this.Controls.Add(this.toolStripContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "LargeGraphAggregationForm";
      this.Text = "Large Graph Aggregation Demo";
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
      this.editorPanel.ResumeLayout(false);
      this.editorPanel.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.settingsBox.ResumeLayout(false);
      this.settingsBox.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private ToolStripContainer toolStripContainer1;
    private ToolStrip toolStrip1;
    private ToolStripButton zoomInButton;
    private ToolStripButton zoomOutButton;
    private ToolStripButton fitContentButton;
    private ToolStripButton zoom100Button;
    private SplitContainer splitContainer2;
    private SplitContainer splitContainerGraphAndOptions;
    private Panel optionPanel;
    private ContextMenuStrip contextMenuStrip1;
    private yWorks.Controls.GraphControl graphControl;
    private Panel editorPanel;
    private Label overviewLabel;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private Label descriptionLabel;
    private RichTextBox descriptionTextBox;
    private GroupBox settingsBox;
    private Button runButton;
    private Toolkit.OptionHandler.ConfigurationEditor configurationEditor;
    private GroupBox groupBox1;
    private Label labelOriginalNodes;
    private Label label2;
    private GroupBox groupBox2;
    private Label labelWeightSum;
    private Label label7;
    private Label labelCount;
    private Label label9;
    private Label labelOriginalEdges;
    private Label label4;
    private Button switchViewButton;
    private Label labelNodeLabel;
    private ComboBox layoutStyleComboBox;
    private Label label3;
    private ProgressBar graphLoadingBar;
  }
}

