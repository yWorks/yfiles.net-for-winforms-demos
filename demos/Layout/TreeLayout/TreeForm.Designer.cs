/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.2.
 ** Copyright (c) 2000-2019 by yWorks GmbH, Vor dem Kreuzberg 28,
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

namespace Demo.yFiles.Layout.Tree
{
  partial class TreeForm
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
      yWorks.Controls.ViewportLimiter viewportLimiter1 = new yWorks.Controls.ViewportLimiter();
      yWorks.Controls.Input.ReparentNodeHandler reparentNodeHandler1 = new yWorks.Controls.Input.ReparentNodeHandler();
      yWorks.Controls.GraphSelection graphSelection1 = new yWorks.Controls.GraphSelection();
      yWorks.Graph.DefaultGraph defaultGraph1 = new yWorks.Graph.DefaultGraph();
      yWorks.Graph.EdgeDefaults edgeDefaults1 = new yWorks.Graph.EdgeDefaults();
      yWorks.Graph.LabelDefaults labelDefaults1 = new yWorks.Graph.LabelDefaults();
      yWorks.Graph.Styles.DefaultLabelStyle DefaultLabelStyle1 = new yWorks.Graph.Styles.DefaultLabelStyle();
      System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
      yWorks.Graph.PortDefaults portDefaults1 = new yWorks.Graph.PortDefaults();
      yWorks.Graph.Styles.PolylineEdgeStyle polylineEdgeStyle1 = new yWorks.Graph.Styles.PolylineEdgeStyle();
      yWorks.Graph.MapperRegistry mapperRegistry1 = new yWorks.Graph.MapperRegistry();
      yWorks.Graph.NodeDefaults nodeDefaults1 = new yWorks.Graph.NodeDefaults();
      yWorks.Graph.LabelDefaults labelDefaults2 = new yWorks.Graph.LabelDefaults();
      yWorks.Graph.Styles.DefaultLabelStyle DefaultLabelStyle2 = new yWorks.Graph.Styles.DefaultLabelStyle();
      System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
      yWorks.Graph.PortDefaults portDefaults2 = new yWorks.Graph.PortDefaults();
      yWorks.Graph.Styles.ShapeNodeStyle shapeNodeStyle1 = new yWorks.Graph.Styles.ShapeNodeStyle();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.sampleComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.reloadSampleButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.nodePlacerPanel = new NodePlacerPanel();
      this.toolStrip1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.sampleComboBox,
            this.reloadSampleButton,
            this.toolStripSeparator3,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(754, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.ToolTipText = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.ToolTipText = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 20);
      this.fitContentButton.ToolTipText = "Fit Content into View";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 20);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.redo_16;
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
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(49, 20);
      this.toolStripLabel1.Text = "Sample:";
      // 
      // sampleComboBox
      // 
      this.sampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.sampleComboBox.Items.AddRange(new object[] {
            "Example Tree",
            "Organization Chart"});
      this.sampleComboBox.Name = "sampleComboBox";
      this.sampleComboBox.Size = new System.Drawing.Size(121, 23);
      this.sampleComboBox.SelectedIndexChanged += new System.EventHandler(this.SampleComboBoxSelectedValueChanged);
      // 
      // reloadSampleButton
      // 
      this.reloadSampleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.reloadSampleButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.reload_16;
      this.reloadSampleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.reloadSampleButton.Name = "reloadSampleButton";
      this.reloadSampleButton.Size = new System.Drawing.Size(23, 20);
      this.reloadSampleButton.Text = "toolStripButton1";
      this.reloadSampleButton.Click += new System.EventHandler(this.OnReloadSampleButtonClicked);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.AutoScroll = true;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer2);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(754, 701);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(754, 732);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
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
      this.splitContainer2.Panel1.AutoScroll = true;
      this.splitContainer2.Panel1.Controls.Add(this.graphControl);
      this.splitContainer2.Panel1MinSize = 297;
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.nodePlacerPanel);
      this.splitContainer2.Size = new System.Drawing.Size(754, 701);
      this.splitContainer2.SplitterDistance = 448;
      this.splitContainer2.TabIndex = 1;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FitContentViewMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(448, 701);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl1";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      labelDefaults1.AutoAdjustPreferredSize = true;
      labelDefaults1.ShareLayoutParameterInstance = true;
      labelDefaults1.ShareStyleInstance = true;
      DefaultLabelStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
      stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
      stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
      stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
      DefaultLabelStyle1.StringFormat = stringFormat1;
      labelDefaults1.Style = DefaultLabelStyle1;
      edgeDefaults1.Labels = labelDefaults1;
      portDefaults1.AutoCleanUp = true;
      portDefaults1.ShareLocationParameterInstance = true;
      portDefaults1.ShareStyleInstance = true;
      edgeDefaults1.Ports = portDefaults1;
      edgeDefaults1.ShareStyleInstance = true;
      edgeDefaults1.Style = polylineEdgeStyle1;
      defaultGraph1.EdgeDefaults = edgeDefaults1;
      defaultGraph1.MapperRegistry = mapperRegistry1;
      labelDefaults2.AutoAdjustPreferredSize = true;
      labelDefaults2.ShareLayoutParameterInstance = true;
      labelDefaults2.ShareStyleInstance = true;
      DefaultLabelStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
      stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
      stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
      stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
      DefaultLabelStyle2.StringFormat = stringFormat2;
      labelDefaults2.Style = DefaultLabelStyle2;
      nodeDefaults1.Labels = labelDefaults2;
      portDefaults2.AutoCleanUp = true;
      portDefaults2.ShareLocationParameterInstance = true;
      portDefaults2.ShareStyleInstance = true;
      nodeDefaults1.Ports = portDefaults2;
      nodeDefaults1.ShareStyleInstance = true;
      nodeDefaults1.Size = new yWorks.Geometry.SizeD(30D, 30D);
      nodeDefaults1.Style = shapeNodeStyle1;
      defaultGraph1.NodeDefaults = nodeDefaults1;
      graphSelection1.Graph = defaultGraph1;
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
      this.splitContainer1.Size = new System.Drawing.Size(1008, 732);
      this.splitContainer1.SplitterDistance = 250;
      this.splitContainer1.TabIndex = 1;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(250, 732);
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
      // nodePlacerPanel
      // 
      this.nodePlacerPanel.AutoScroll = true;
      this.nodePlacerPanel.BackColor = System.Drawing.Color.White;
      this.nodePlacerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nodePlacerPanel.CurrentDescriptor = null;
      this.nodePlacerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nodePlacerPanel.Level = 0;
      this.nodePlacerPanel.Location = new System.Drawing.Point(0, 0);
      this.nodePlacerPanel.MinimumSize = new System.Drawing.Size(275, 2);
      this.nodePlacerPanel.Name = "nodePlacerPanel";
      this.nodePlacerPanel.Size = new System.Drawing.Size(302, 701);
      this.nodePlacerPanel.TabIndex = 0;
      // 
      // TreeForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1008, 732);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Layout.Tree.Properties.Resources.yIcon;
      this.Name = "TreeForm";
      this.Text = "Generic Tree Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox sampleComboBox;
    private System.Windows.Forms.ToolStripButton reloadSampleButton;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private yWorks.Controls.GraphControl graphControl;
    private NodePlacerPanel nodePlacerPanel;
  }
}

