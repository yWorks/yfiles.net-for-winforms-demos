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

namespace Demo.yFiles.Graph.SelectionStyling
{
  partial class SelectionStylingForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionStylingForm));
      yWorks.Controls.ViewportLimiter viewportLimiter3 = new yWorks.Controls.ViewportLimiter();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.openButton = new System.Windows.Forms.ToolStripButton();
      this.saveButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.resetZoomButton = new System.Windows.Forms.ToolStripButton();
      this.zoomFitButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.CustomNodeDecoratorButton = new System.Windows.Forms.ToolStripButton();
      this.CustomEdgeDecoratorButton = new System.Windows.Forms.ToolStripButton();
      this.CustomLabelDecoratorButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.zoomModeComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.toolStrip1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.saveButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton,
            this.resetZoomButton,
            this.zoomFitButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.CustomNodeDecoratorButton,
            this.CustomEdgeDecoratorButton,
            this.CustomLabelDecoratorButton,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.zoomModeComboBox});
      this.toolStrip1.Location = new System.Drawing.Point(3, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(617, 25);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // openButton
      // 
      this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openButton.Image = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.open_16;
      this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(23, 22);
      this.openButton.Text = "Open";
      // 
      // saveButton
      // 
      this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveButton.Image = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.save_16;
      this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(23, 22);
      this.saveButton.Text = "Save";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 22);
      this.zoomInButton.Text = "Increase Zoom";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
      this.zoomOutButton.Text = "Decrease Zoom";
      // 
      // resetZoomButton
      // 
      this.resetZoomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.resetZoomButton.Image = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.zoom_original3_16;
      this.resetZoomButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.resetZoomButton.Name = "resetZoomButton";
      this.resetZoomButton.Size = new System.Drawing.Size(23, 22);
      this.resetZoomButton.Text = "Zoom to 100%";
      // 
      // zoomFitButton
      // 
      this.zoomFitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomFitButton.Image = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.fit_16;
      this.zoomFitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomFitButton.Name = "zoomFitButton";
      this.zoomFitButton.Size = new System.Drawing.Size(23, 22);
      this.zoomFitButton.Text = "Fit Graph Bounds";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
      this.toolStripLabel1.Text = "Custom Painting:";
      // 
      // customNodesPaintingButton
      // 
      this.CustomNodeDecoratorButton.Checked = true;
      this.CustomNodeDecoratorButton.CheckOnClick = true;
      this.CustomNodeDecoratorButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CustomNodeDecoratorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.CustomNodeDecoratorButton.Image = ((System.Drawing.Image)(resources.GetObject("customNodesPaintingButton.Image")));
      this.CustomNodeDecoratorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.CustomNodeDecoratorButton.Name = "customNodesPaintingButton";
      this.CustomNodeDecoratorButton.Size = new System.Drawing.Size(45, 22);
      this.CustomNodeDecoratorButton.Text = "Nodes";
      this.CustomNodeDecoratorButton.ToolTipText = "Controls custom selection painting for nodes";
      this.CustomNodeDecoratorButton.CheckedChanged += new System.EventHandler(this.CustomNodeDecorationChanged);
      // 
      // customEdgesPaintingButton
      // 
      this.CustomEdgeDecoratorButton.Checked = true;
      this.CustomEdgeDecoratorButton.CheckOnClick = true;
      this.CustomEdgeDecoratorButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CustomEdgeDecoratorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.CustomEdgeDecoratorButton.Image = ((System.Drawing.Image)(resources.GetObject("customEdgesPaintingButton.Image")));
      this.CustomEdgeDecoratorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.CustomEdgeDecoratorButton.Name = "customEdgesPaintingButton";
      this.CustomEdgeDecoratorButton.Size = new System.Drawing.Size(42, 22);
      this.CustomEdgeDecoratorButton.Text = "Edges";
      this.CustomEdgeDecoratorButton.ToolTipText = "Controls custom selection painting for edges";
      this.CustomEdgeDecoratorButton.CheckedChanged += new System.EventHandler(this.CustomEdgeDecorationChanged);
      // 
      // customLabelsPaintingButton
      // 
      this.CustomLabelDecoratorButton.Checked = true;
      this.CustomLabelDecoratorButton.CheckOnClick = true;
      this.CustomLabelDecoratorButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CustomLabelDecoratorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.CustomLabelDecoratorButton.Image = ((System.Drawing.Image)(resources.GetObject("customLabelsPaintingButton.Image")));
      this.CustomLabelDecoratorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.CustomLabelDecoratorButton.Name = "customLabelsPaintingButton";
      this.CustomLabelDecoratorButton.Size = new System.Drawing.Size(44, 22);
      this.CustomLabelDecoratorButton.Text = "Labels";
      this.CustomLabelDecoratorButton.ToolTipText = "Controls custom selection painting for labels";
      this.CustomLabelDecoratorButton.CheckedChanged += new System.EventHandler(this.CustomLabelDecorationChanged);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(76, 22);
      this.toolStripLabel2.Text = "Zoom Mode:";
      // 
      // zoomModeComboBox
      // 
      this.zoomModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.zoomModeComboBox.Name = "zoomModeComboBox";
      this.zoomModeComboBox.Size = new System.Drawing.Size(121, 25);
      this.zoomModeComboBox.ToolTipText = "Zoom mode of the custom selection indicator";
      this.zoomModeComboBox.SelectedIndexChanged += new System.EventHandler(this.ZoomModeChanged);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.AutoScroll = true;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(620, 439);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(620, 464);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
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
      this.graphControl.Size = new System.Drawing.Size(620, 439);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl";
      viewportLimiter3.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter3;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
      this.splitContainer1.FixedPanel = FixedPanel.Panel1;
      this.splitContainer1.TabIndex = 1;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(310, 464);
      this.description.TabIndex = 5;
      this.description.Text = "";
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.DefaultExt = "graphml";
      this.saveFileDialog.Filter = "GraphML Files|*.graphml|XML Files|*.xml|All files|*.*";
      this.saveFileDialog.Title = "Save GraphML file...";
      // 
      // openFileDialog
      // 
      this.openFileDialog.DefaultExt = "graphml";
      this.openFileDialog.Filter = "GraphML Files|*.graphml|XML Files|*.xml|All files|*.*";
      this.openFileDialog.Title = "Load GraphML file...";
      // 
      // SelectionStylingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(934, 464);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Graph.SelectionStyling.Properties.Resources.yIcon;
      this.WindowState = FormWindowState.Maximized;
      this.Name = "SelectionStylingForm";
      this.Text = "Selection Styling Demo";
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.ToolStripButton openButton;
    private System.Windows.Forms.ToolStripButton saveButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton resetZoomButton;
    private System.Windows.Forms.ToolStripButton zoomFitButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton CustomNodeDecoratorButton;
    private System.Windows.Forms.ToolStripButton CustomEdgeDecoratorButton;
    private System.Windows.Forms.ToolStripButton CustomLabelDecoratorButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripComboBox zoomModeComboBox;
  }
}

