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
using yWorks.Geometry;

namespace Demo.yFiles.Graph.Viewer
{
  partial class GraphViewer
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
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.openButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.graphChooserBox = new System.Windows.Forms.ToolStripComboBox();
      this.previousButton = new System.Windows.Forms.ToolStripButton();
      this.nextButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.panel1 = new System.Windows.Forms.Panel();
      this.infoPanel = new System.Windows.Forms.Panel();
      this.nodeUrlButton = new System.Windows.Forms.LinkLabel();
      this.label3 = new System.Windows.Forms.Label();
      this.nodeDescriptionTextBlock = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.nodeLabelTextBlock = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.infoLabel = new System.Windows.Forms.Label();
      this.descriptionPanel = new System.Windows.Forms.Panel();
      this.graphDescriptionTextBlock = new System.Windows.Forms.RichTextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.overviewPanel = new System.Windows.Forms.Panel();
      this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
      this.overviewLabel = new System.Windows.Forms.Label();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.infoPanel.SuspendLayout();
      this.descriptionPanel.SuspendLayout();
      this.overviewPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.toolStripSeparator,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.graphChooserBox,
            this.previousButton,
            this.nextButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(752, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // openButton
      // 
      this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openButton.Image = global::Demo.yFiles.Graph.Viewer.Properties.Resources.open_16;
      this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(23, 20);
      this.openButton.Text = "&Open";
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.Viewer.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom in";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.Viewer.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.Viewer.Properties.Resources.fit_16;
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
      // graphChooserBox
      // 
      this.graphChooserBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.graphChooserBox.Name = "graphChooserBox";
      this.graphChooserBox.Size = new System.Drawing.Size(150, 23);
      this.graphChooserBox.ToolTipText = "Choose a graph to display";
      this.graphChooserBox.SelectedIndexChanged += new System.EventHandler(this.graphChooserBox_SelectedIndexChanged);
      // 
      // previousButton
      // 
      this.previousButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.previousButton.Image = global::Demo.yFiles.Graph.Viewer.Properties.Resources.arrow_left_16;
      this.previousButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.previousButton.Name = "previousButton";
      this.previousButton.Size = new System.Drawing.Size(23, 20);
      this.previousButton.Text = "Previous";
      this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.nextButton.Image = global::Demo.yFiles.Graph.Viewer.Properties.Resources.arrow_right_16;
      this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(23, 20);
      this.nextButton.Text = "Next";
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 31);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.graphControl);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.panel1);
      this.splitContainer1.Size = new System.Drawing.Size(752, 701);
      this.splitContainer1.SplitterDistance = 500;
      this.splitContainer1.TabIndex = 2;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(500, 701);
      this.graphControl.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Window;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.infoPanel);
      this.panel1.Controls.Add(this.descriptionPanel);
      this.panel1.Controls.Add(this.overviewPanel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(248, 701);
      this.panel1.TabIndex = 2;
      // 
      // infoPanel
      // 
      this.infoPanel.BackColor = System.Drawing.SystemColors.Window;
      this.infoPanel.Controls.Add(this.nodeUrlButton);
      this.infoPanel.Controls.Add(this.label3);
      this.infoPanel.Controls.Add(this.nodeDescriptionTextBlock);
      this.infoPanel.Controls.Add(this.label2);
      this.infoPanel.Controls.Add(this.nodeLabelTextBlock);
      this.infoPanel.Controls.Add(this.label1);
      this.infoPanel.Controls.Add(this.infoLabel);
      this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.infoPanel.Location = new System.Drawing.Point(0, 336);
      this.infoPanel.Name = "infoPanel";
      this.infoPanel.Size = new System.Drawing.Size(244, 361);
      this.infoPanel.TabIndex = 3;
      // 
      // nodeUrlButton
      // 
      this.nodeUrlButton.AutoSize = true;
      this.nodeUrlButton.Dock = System.Windows.Forms.DockStyle.Top;
      this.nodeUrlButton.Location = new System.Drawing.Point(0, 279);
      this.nodeUrlButton.Name = "nodeUrlButton";
      this.nodeUrlButton.Size = new System.Drawing.Size(0, 13);
      this.nodeUrlButton.TabIndex = 5;
      this.nodeUrlButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.nodeUrlButton_LinkClicked);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Top;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(0, 255);
      this.label3.Name = "label3";
      this.label3.Padding = new System.Windows.Forms.Padding(4);
      this.label3.Size = new System.Drawing.Size(43, 24);
      this.label3.TabIndex = 4;
      this.label3.Text = "URL";
      // 
      // nodeDescriptionTextBlock
      // 
      this.nodeDescriptionTextBlock.BackColor = System.Drawing.SystemColors.Window;
      this.nodeDescriptionTextBlock.Dock = System.Windows.Forms.DockStyle.Top;
      this.nodeDescriptionTextBlock.Location = new System.Drawing.Point(0, 91);
      this.nodeDescriptionTextBlock.Multiline = true;
      this.nodeDescriptionTextBlock.Name = "nodeDescriptionTextBlock";
      this.nodeDescriptionTextBlock.ReadOnly = true;
      this.nodeDescriptionTextBlock.Size = new System.Drawing.Size(244, 164);
      this.nodeDescriptionTextBlock.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Top;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(0, 67);
      this.label2.Name = "label2";
      this.label2.Padding = new System.Windows.Forms.Padding(4);
      this.label2.Size = new System.Drawing.Size(84, 24);
      this.label2.TabIndex = 2;
      this.label2.Text = "Description";
      // 
      // nodeLabelTextBlock
      // 
      this.nodeLabelTextBlock.BackColor = System.Drawing.SystemColors.Window;
      this.nodeLabelTextBlock.Dock = System.Windows.Forms.DockStyle.Top;
      this.nodeLabelTextBlock.Location = new System.Drawing.Point(0, 47);
      this.nodeLabelTextBlock.Multiline = true;
      this.nodeLabelTextBlock.Name = "nodeLabelTextBlock";
      this.nodeLabelTextBlock.ReadOnly = true;
      this.nodeLabelTextBlock.Size = new System.Drawing.Size(244, 100);
      this.nodeLabelTextBlock.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(0, 23);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(4);
      this.label1.Size = new System.Drawing.Size(50, 24);
      this.label1.TabIndex = 0;
      this.label1.Text = "Label";
      // 
      // infoLabel
      // 
      this.infoLabel.BackColor = System.Drawing.SystemColors.Control;
      this.infoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.infoLabel.Dock = System.Windows.Forms.DockStyle.Top;
      this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.infoLabel.Location = new System.Drawing.Point(0, 0);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(244, 23);
      this.infoLabel.TabIndex = 2;
      this.infoLabel.Text = "Node Info";
      this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // descriptionPanel
      // 
      this.descriptionPanel.Controls.Add(this.graphDescriptionTextBlock);
      this.descriptionPanel.Controls.Add(this.descriptionLabel);
      this.descriptionPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.descriptionPanel.Location = new System.Drawing.Point(0, 188);
      this.descriptionPanel.Name = "descriptionPanel";
      this.descriptionPanel.Size = new System.Drawing.Size(244, 148);
      this.descriptionPanel.TabIndex = 1;
      // 
      // graphDescriptionTextBlock
      // 
      this.graphDescriptionTextBlock.BackColor = System.Drawing.SystemColors.Window;
      this.graphDescriptionTextBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.graphDescriptionTextBlock.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphDescriptionTextBlock.Location = new System.Drawing.Point(0, 23);
      this.graphDescriptionTextBlock.Name = "graphDescriptionTextBlock";
      this.graphDescriptionTextBlock.ReadOnly = true;
      this.graphDescriptionTextBlock.Size = new System.Drawing.Size(244, 125);
      this.graphDescriptionTextBlock.TabIndex = 0;
      this.graphDescriptionTextBlock.Text = "";
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.BackColor = System.Drawing.SystemColors.Control;
      this.descriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
      this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.descriptionLabel.Location = new System.Drawing.Point(0, 0);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(244, 23);
      this.descriptionLabel.TabIndex = 3;
      this.descriptionLabel.Text = "Graph Description";
      this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // overviewPanel
      // 
      this.overviewPanel.Controls.Add(this.graphOverviewControl);
      this.overviewPanel.Controls.Add(this.overviewLabel);
      this.overviewPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.overviewPanel.Location = new System.Drawing.Point(0, 0);
      this.overviewPanel.Name = "overviewPanel";
      this.overviewPanel.Size = new System.Drawing.Size(244, 188);
      this.overviewPanel.TabIndex = 6;
      // 
      // graphOverviewControl
      // 
      this.graphOverviewControl.AnimateScrollCommands = false;
      this.graphOverviewControl.AutoDrag = false;
      this.graphOverviewControl.BackColor = System.Drawing.Color.White;
      this.graphOverviewControl.ContentMargins = new yWorks.Geometry.InsetsD(0D, 0D, 0D, 0D);
      this.graphOverviewControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphOverviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphOverviewControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphOverviewControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphOverviewControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphOverviewControl.GraphControl = null;
      this.graphOverviewControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      this.graphOverviewControl.Location = new System.Drawing.Point(0, 23);
      this.graphOverviewControl.MouseWheelBehavior = yWorks.Controls.MouseWheelBehaviors.None;
      this.graphOverviewControl.Name = "graphOverviewControl";
      this.graphOverviewControl.Size = new System.Drawing.Size(244, 165);
      this.graphOverviewControl.TabIndex = 0;
      this.graphOverviewControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
      // 
      // overviewLabel
      // 
      this.overviewLabel.BackColor = System.Drawing.SystemColors.Control;
      this.overviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.overviewLabel.Dock = System.Windows.Forms.DockStyle.Top;
      this.overviewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.overviewLabel.Location = new System.Drawing.Point(0, 0);
      this.overviewLabel.Name = "overviewLabel";
      this.overviewLabel.Size = new System.Drawing.Size(244, 23);
      this.overviewLabel.TabIndex = 1;
      this.overviewLabel.Text = "Overview";
      this.overviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
      this.splitContainer2.Panel2.Controls.Add(this.toolStrip1);
      this.splitContainer2.Size = new System.Drawing.Size(1008, 732);
      this.splitContainer2.SplitterDistance = 252;
      this.splitContainer2.TabIndex = 3;
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.descriptionTextBox.Location = new System.Drawing.Point(0, 0);
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.Size = new System.Drawing.Size(252, 732);
      this.descriptionTextBox.TabIndex = 0;
      this.descriptionTextBox.Text = "";
      // 
      // GraphViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1008, 732);
      this.Controls.Add(this.splitContainer2);
      this.Icon = global::Demo.yFiles.Graph.Viewer.Properties.Resources.yIcon;
      this.Name = "GraphViewer";
      this.Text = "Graph Viewer Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.infoPanel.ResumeLayout(false);
      this.infoPanel.PerformLayout();
      this.descriptionPanel.ResumeLayout(false);
      this.overviewPanel.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton openButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripComboBox graphChooserBox;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.LinkLabel nodeUrlButton;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox nodeDescriptionTextBlock;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox nodeLabelTextBlock;
    private System.Windows.Forms.Label label1;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton previousButton;
    private System.Windows.Forms.ToolStripButton nextButton;
    private SplitContainer splitContainer2;
    private RichTextBox descriptionTextBox;
    private Panel infoPanel;
    private Label infoLabel;
    private Panel descriptionPanel;
    private RichTextBox graphDescriptionTextBlock;
    private Label descriptionLabel;
    private Panel overviewPanel;
    private Label overviewLabel;
    private Panel panel1;

  }
}

