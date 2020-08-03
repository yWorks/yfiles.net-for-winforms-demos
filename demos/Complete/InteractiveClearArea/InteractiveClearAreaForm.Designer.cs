/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
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

namespace Demo.yFiles.Graph.InteractiveClearArea
{
  partial class InteractiveClearAreaForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    #endregion

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.zoomFitButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.sampleGraphLabel = new ToolStripLabel();
      this.loadPreviousSampleButton = new ToolStripButton();
      this.sampleGraphComboBox = new ToolStripComboBox();
      this.loadNextSampleButton = new ToolStripButton();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.clearingStrategyLabel = new ToolStripLabel();
      this.clearAreaStrategyComboBox = new ToolStripComboBox();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.componentAssignmentStrategyLabel = new ToolStripLabel();
      this.componentAssignmentStrategyComboBox = new ToolStripComboBox();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.considerEdgesToggleButton = new ToolStripButton();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStrip1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
          this.undoButton,
          this.redoButton,
          this.toolStripSeparator1,
          this.zoomInButton,
          this.zoomOutButton,
          this.zoomFitButton,
          this.toolStripSeparator2,
          this.sampleGraphLabel,
          this.loadPreviousSampleButton,
          this.sampleGraphComboBox,
          this.loadNextSampleButton,
          this.toolStripSeparator3,
          this.clearingStrategyLabel,
          this.clearAreaStrategyComboBox,
          this.toolStripSeparator4,
          this.componentAssignmentStrategyLabel,
          this.componentAssignmentStrategyComboBox,
          this.considerEdgesToggleButton,
          this.toolStripSeparator5});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(678, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 20);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.redo_16;
      this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.redoButton.Name = "redoButton";
      this.redoButton.Size = new System.Drawing.Size(23, 20);
      this.redoButton.Text = "Redo";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Increase Zoom";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Decrease Zoom";
      // 
      // zoomFitButton
      // 
      this.zoomFitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomFitButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.fit_16;
      this.zoomFitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomFitButton.Name = "zoomFitButton";
      this.zoomFitButton.Size = new System.Drawing.Size(23, 20);
      this.zoomFitButton.Text = "Fit Graph Bounds";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // sampleGraphLabel
      // 
      this.sampleGraphLabel.Name = "sampleGraphLabel";
      this.sampleGraphLabel.Size = new System.Drawing.Size(94, 20);
      this.sampleGraphLabel.Text = "Sample Graph:";
      // 
      // loadPreviousSampleButton
      // 
      this.loadPreviousSampleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.loadPreviousSampleButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.arrow_left_16;
      this.loadPreviousSampleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.loadPreviousSampleButton.Name = "loadPreviousSampleButton";
      this.loadPreviousSampleButton.Size = new System.Drawing.Size(23, 20);
      this.loadPreviousSampleButton.Text = "Load previous sample";
      this.loadPreviousSampleButton.Click += new System.EventHandler(this.LoadPreviousSampleGraph);
      // 
      // sampleGraphComboBox
      // 
      this.sampleGraphComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.sampleGraphComboBox.Name = "sampleGraphComboBox";
      this.sampleGraphComboBox.Size = new System.Drawing.Size(200, 23);
      this.sampleGraphComboBox.ToolTipText = "Select a sample graph";
      this.sampleGraphComboBox.SelectedIndexChanged += new System.EventHandler(this.SampleGraphChanged);
      // 
      // loadNextSampleButton
      // 
      this.loadNextSampleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.loadNextSampleButton.Image = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.arrow_right_16;
      this.loadNextSampleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.loadNextSampleButton.Name = "loadNextSampleButton";
      this.loadNextSampleButton.Size = new System.Drawing.Size(23, 20);
      this.loadNextSampleButton.Text = "Load next sample";
      this.loadNextSampleButton.Click += new System.EventHandler(this.LoadNextSampleGraph);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // clearingStrategyLabel
      // 
      this.clearingStrategyLabel.Name = "clearingStrategyLabel";
      this.clearingStrategyLabel.Size = new System.Drawing.Size(94, 20);
      this.clearingStrategyLabel.Text = "Clearing Strategy:";
      // 
      // clearAreaStrategyComboBox
      // 
      this.clearAreaStrategyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.clearAreaStrategyComboBox.Name = "clearAreaStrategyComboBox";
      this.clearAreaStrategyComboBox.Size = new System.Drawing.Size(200, 23);
      this.clearAreaStrategyComboBox.ToolTipText = "Select a clear area strategy";
      this.clearAreaStrategyComboBox.SelectedIndexChanged += new System.EventHandler(this.ClearAreaStrategyChanged);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
      // 
      // componentAssignmentStrategyLabel
      // 
      this.componentAssignmentStrategyLabel.Name = "componentAssignmentStrategyLabel";
      this.componentAssignmentStrategyLabel.Size = new System.Drawing.Size(94, 20);
      this.componentAssignmentStrategyLabel.Text = "Component Assignment Strategy:";
      // 
      // componentAssignmentStrategyComboBox
      // 
      this.componentAssignmentStrategyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.componentAssignmentStrategyComboBox.Name = "componentAssignmentStrategyComboBox";
      this.componentAssignmentStrategyComboBox.Size = new System.Drawing.Size(200, 23);
      this.componentAssignmentStrategyComboBox.ToolTipText = "Select a clear area strategy";
      this.componentAssignmentStrategyComboBox.SelectedIndexChanged += new System.EventHandler(this.ComponentAssignmentStrategyChanged);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
      // 
      // considerEdgesToggleButton
      // 
      this.considerEdgesToggleButton.CheckOnClick = true;
      this.considerEdgesToggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.considerEdgesToggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.considerEdgesToggleButton.Name = "considerEdgesToggleButton";
      this.considerEdgesToggleButton.Size = new System.Drawing.Size(109, 20);
      this.considerEdgesToggleButton.Text = "Consider Edges";
      this.considerEdgesToggleButton.ToolTipText = "Orthogonal edges are not allowed to intersect the rectangular area";
      this.considerEdgesToggleButton.Click += new System.EventHandler(this.ToggleConsiderEdges);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.AutoScroll = true;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(678, 433);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(678, 464);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // graphControl
      // 
      this.graphControl.AnimateScrollCommands = false;
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
      this.graphControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(678, 433);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 0;
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
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
      this.splitContainer1.Size = new System.Drawing.Size(934, 464);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 1;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 464);
      this.description.TabIndex = 5;
      this.description.Text = "";
      // 
      // InteractiveClearAreaForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(934, 464);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.InteractiveClearArea.Properties.Resources.yIcon;
      this.Name = "InteractiveClearAreaForm";
      this.Text = "Interactive ClearArea Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoad);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton zoomFitButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private ToolStripLabel sampleGraphLabel;
    private System.Windows.Forms.ToolStripButton loadPreviousSampleButton;
    private System.Windows.Forms.ToolStripComboBox sampleGraphComboBox;
    private System.Windows.Forms.ToolStripButton loadNextSampleButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private ToolStripLabel clearingStrategyLabel;
    private System.Windows.Forms.ToolStripComboBox clearAreaStrategyComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private ToolStripLabel componentAssignmentStrategyLabel;
    private System.Windows.Forms.ToolStripComboBox componentAssignmentStrategyComboBox;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton considerEdgesToggleButton;
  }
}
