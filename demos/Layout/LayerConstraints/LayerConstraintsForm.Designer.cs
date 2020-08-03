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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Demo.yFiles.Layout.LayerConstraints.Properties;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;

namespace Demo.yFiles.Layout.LayerConstraints
{
  partial class LayerConstraintsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.newGraphButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.runButton = new System.Windows.Forms.ToolStripButton();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadGraphMLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveGraphMLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.newGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.constraintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.disableAllConstraintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.enableAllConstraintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.increaseZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.decreaseZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fitGraphBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
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
      this.splitContainer1.Panel1.Controls.Add(this.description);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.graphControl);
      this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
      this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
      this.splitContainer1.Size = new System.Drawing.Size(900, 637);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.TabIndex = 0;
      // 
      // description
      // 
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.Size = new System.Drawing.Size(252, 637);
      this.description.TabIndex = 1;
      this.description.Text = "";
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.FitContentViewMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.Location = new System.Drawing.Point(0, 55);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(644, 582);
      this.graphControl.TabIndex = 7;
      this.graphControl.Text = "graphControl1";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGraphButton,
            this.toolStripSeparator1,
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.runButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(644, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.LayerConstraints.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.LayerConstraints.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Layout.LayerConstraints.Properties.Resources.fit_16;
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
      // newGraphButton
      // 
      this.newGraphButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newGraphButton.Image = global::Demo.yFiles.Layout.LayerConstraints.Properties.Resources.new_document_16;
      this.newGraphButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newGraphButton.Name = "newGraphButton";
      this.newGraphButton.Size = new System.Drawing.Size(23, 20);
      this.newGraphButton.Text = "New Random Graph";
      this.newGraphButton.ToolTipText = "Generate a new random Graph";
      this.newGraphButton.Click += new System.EventHandler(this.OnNewGraphClick);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // runButton
      // 
      this.runButton.Image = global::Demo.yFiles.Layout.LayerConstraints.Properties.Resources.layout_tree_16;
      this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(63, 20);
      this.runButton.Text = "Layout";
      this.runButton.ToolTipText = "Run the Layout";
      this.runButton.Click += new System.EventHandler(this.OnLayoutClick);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.constraintsToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(644, 24);
      this.menuStrip1.TabIndex = 8;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGraphToolStripMenuItem,
            this.toolStripSeparator3,
            this.loadGraphMLMenuItem,
            this.saveGraphMLMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // loadGraphMLMenuItem
      // 
      this.loadGraphMLMenuItem.Name = "loadGraphMLMenuItem";
      this.loadGraphMLMenuItem.Size = new System.Drawing.Size(152, 22);
      this.loadGraphMLMenuItem.Text = "Open";
      // 
      // saveGraphMLMenuItem
      // 
      this.saveGraphMLMenuItem.Name = "saveGraphMLMenuItem";
      this.saveGraphMLMenuItem.Size = new System.Drawing.Size(152, 22);
      this.saveGraphMLMenuItem.Text = "Save as...";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
      // 
      // newGraphToolStripMenuItem
      // 
      this.newGraphToolStripMenuItem.Name = "newGraphToolStripMenuItem";
      this.newGraphToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.newGraphToolStripMenuItem.Text = "New Graph";
      this.newGraphToolStripMenuItem.Click += new System.EventHandler(this.OnNewGraphClick);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItemClick);
      // 
      // constraintsToolStripMenuItem
      // 
      this.constraintsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableAllConstraintsToolStripMenuItem,
            this.enableAllConstraintsToolStripMenuItem});
      this.constraintsToolStripMenuItem.Name = "constraintsToolStripMenuItem";
      this.constraintsToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
      this.constraintsToolStripMenuItem.Text = "Constraints";
      // 
      // disableAllConstraintsToolStripMenuItem
      // 
      this.disableAllConstraintsToolStripMenuItem.Name = "disableAllConstraintsToolStripMenuItem";
      this.disableAllConstraintsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
      this.disableAllConstraintsToolStripMenuItem.Text = "Disable all Constraints";
      this.disableAllConstraintsToolStripMenuItem.Click += new System.EventHandler(this.OnDisableConstraints);
      // 
      // enableAllConstraintsToolStripMenuItem
      // 
      this.enableAllConstraintsToolStripMenuItem.Name = "enableAllConstraintsToolStripMenuItem";
      this.enableAllConstraintsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
      this.enableAllConstraintsToolStripMenuItem.Text = "Enable all Constraints";
      this.enableAllConstraintsToolStripMenuItem.Click += new System.EventHandler(this.OnEnableConstraints);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseZoomToolStripMenuItem,
            this.decreaseZoomToolStripMenuItem,
            this.fitGraphBoundsToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // increaseZoomToolStripMenuItem
      // 
      this.increaseZoomToolStripMenuItem.Name = "increaseZoomToolStripMenuItem";
      this.increaseZoomToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.increaseZoomToolStripMenuItem.Text = "Increase Zoom";
      // 
      // decreaseZoomToolStripMenuItem
      // 
      this.decreaseZoomToolStripMenuItem.Name = "decreaseZoomToolStripMenuItem";
      this.decreaseZoomToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.decreaseZoomToolStripMenuItem.Text = "Decrease Zoom";
      // 
      // fitGraphBoundsToolStripMenuItem
      // 
      this.fitGraphBoundsToolStripMenuItem.Name = "fitGraphBoundsToolStripMenuItem";
      this.fitGraphBoundsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.fitGraphBoundsToolStripMenuItem.Text = "Fit Graph Bounds";
      // 
      // LayerConstraintsForm
      // 
      this.ClientSize = new System.Drawing.Size(900, 637);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Layout.LayerConstraints.Properties.Resources.yIcon;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "LayerConstraintsForm";
      this.Text = "LayerConstraints Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoaded);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private SplitContainer splitContainer1;
    private RichTextBox description;
    private ToolStrip toolStrip1;
    private GraphControl graphControl;
    private ToolStripButton zoomInButton;
    private ToolStripButton zoomOutButton;
    private ToolStripButton fitContentButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton newGraphButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton runButton;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem constraintsToolStripMenuItem;
    private ToolStripMenuItem loadGraphMLMenuItem;
    private ToolStripMenuItem saveGraphMLMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem newGraphToolStripMenuItem;
    private ToolStripMenuItem disableAllConstraintsToolStripMenuItem;
    private ToolStripMenuItem enableAllConstraintsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem increaseZoomToolStripMenuItem;
    private ToolStripMenuItem decreaseZoomToolStripMenuItem;
    private ToolStripMenuItem fitGraphBoundsToolStripMenuItem;
  }
}