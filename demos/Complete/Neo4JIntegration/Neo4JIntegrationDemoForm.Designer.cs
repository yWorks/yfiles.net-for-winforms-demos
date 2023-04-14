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
using yWorks.Geometry;

namespace Neo4JIntegration {

  partial class Neo4JIntegrationDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Neo4JIntegrationDemo));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.description = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.graphOverviewControl = new yWorks.Controls.GraphOverviewControl();
            this.graphControl = new yWorks.Controls.GraphControl();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.fitContentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cypherQueryLabel = new System.Windows.Forms.ToolStripLabel();
            this.QueryTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.runQueryButton = new System.Windows.Forms.ToolStripButton();
            this.ReplaceGraphCheckbox = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(844, 552);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(844, 583);
            this.toolStripContainer.TabIndex = 1;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
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
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphControl);
            this.splitContainer1.Size = new System.Drawing.Size(844, 552);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 3;
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Window;
            this.description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.description.Location = new System.Drawing.Point(0, 215);
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Size = new System.Drawing.Size(350, 337);
            this.description.TabIndex = 2;
            this.description.Text = "";
            this.description.DetectUrls = true;
            this.description.LinkClicked += new LinkClickedEventHandler(this.OnLinkClicked);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.graphOverviewControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 215);
            this.panel1.TabIndex = 3;
            // 
            // graphOverviewControl
            // 
            this.graphOverviewControl.AnimatedViewportChanges = ((yWorks.Controls.ViewportChanges)((((((yWorks.Controls.ViewportChanges.AutoDrag | yWorks.Controls.ViewportChanges.MouseWheelZoom) 
            | yWorks.Controls.ViewportChanges.MouseWheelScroll) 
            | yWorks.Controls.ViewportChanges.ScrollBar) 
            | yWorks.Controls.ViewportChanges.ZoomCommand) 
            | yWorks.Controls.ViewportChanges.FitContentCommand)));
            this.graphOverviewControl.AutoDrag = false;
            this.graphOverviewControl.BackColor = System.Drawing.Color.White;
            this.graphOverviewControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.graphOverviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphOverviewControl.DoubleClickSize = new yWorks.Geometry.SizeD(4D, 4D);
            this.graphOverviewControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.graphOverviewControl.DragSize = new yWorks.Geometry.SizeD(4D, 4D);
            this.graphOverviewControl.GraphControl = null;
            this.graphOverviewControl.HorizontalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
            this.graphOverviewControl.Location = new System.Drawing.Point(0, 0);
            this.graphOverviewControl.MouseWheelBehavior = yWorks.Controls.MouseWheelBehaviors.None;
            this.graphOverviewControl.Name = "graphOverviewControl";
            this.graphOverviewControl.Size = new System.Drawing.Size(346, 211);
            this.graphOverviewControl.TabIndex = 1;
            this.graphOverviewControl.VerticalScrollBarPolicy = yWorks.Controls.ScrollBarVisibility.Never;
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
            this.graphControl.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
            this.graphControl.Size = new System.Drawing.Size(490, 552);
            this.graphControl.TabIndex = 1;
            this.graphControl.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.cypherQueryLabel,
            this.QueryTextBox,
            this.runQueryButton,
            this.ReplaceGraphCheckbox});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
            this.toolStrip.Size = new System.Drawing.Size(844, 31);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 0;
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = global::Neo4JIntegration.Properties.Resources.plus_16;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 20);
            this.zoomInButton.Text = "Zoom In";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::Neo4JIntegration.Properties.Resources.minus_16;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
            this.zoomOutButton.Text = "Zoom Out";
            // 
            // fitContentButton
            // 
            this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fitContentButton.Image = global::Neo4JIntegration.Properties.Resources.fit_16;
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
            // cypherQueryLabel
            // 
            this.cypherQueryLabel.Name = "cypherQueryLabel";
            this.cypherQueryLabel.Size = new System.Drawing.Size(80, 20);
            this.cypherQueryLabel.Text = "Cypher Query";
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(300, 23);
            this.QueryTextBox.ToolTipText = "Enter Cypher Query, e.g. \"Match (n1)-[r1]->(n2) RETURN n1,r1,n2 LIMIT 20\"";
            // 
            // runQueryButton
            // 
            this.runQueryButton.Image = global::Neo4JIntegration.Properties.Resources.play2_16;
            this.runQueryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runQueryButton.Name = "runQueryButton";
            this.runQueryButton.Size = new System.Drawing.Size(83, 20);
            this.runQueryButton.Text = "Run Query";
            this.runQueryButton.Click += new System.EventHandler(this.RunQuery_OnClick);
            // 
            // ReplaceGraphCheckbox
            // 
            this.ReplaceGraphCheckbox.Checked = true;
            this.ReplaceGraphCheckbox.CheckOnClick = true;
            this.ReplaceGraphCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReplaceGraphCheckbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ReplaceGraphCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("ReplaceGraphCheckbox.Image")));
            this.ReplaceGraphCheckbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReplaceGraphCheckbox.Name = "ReplaceGraphCheckbox";
            this.ReplaceGraphCheckbox.Size = new System.Drawing.Size(130, 20);
            this.ReplaceGraphCheckbox.Text = "Replace Current Graph";
            // 
            // Neo4JIntegrationDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 583);
            this.Controls.Add(this.toolStripContainer);
            this.Icon = global::Neo4JIntegration.Properties.Resources.yIcon;
            this.Name = "Neo4JIntegrationDemo";
            this.Text = "Neo4J Integration Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private yWorks.Controls.GraphOverviewControl graphOverviewControl;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel cypherQueryLabel;
    private System.Windows.Forms.ToolStripTextBox QueryTextBox;
    private System.Windows.Forms.ToolStripButton runQueryButton;
    private System.Windows.Forms.ToolStripButton ReplaceGraphCheckbox;
  }
}

