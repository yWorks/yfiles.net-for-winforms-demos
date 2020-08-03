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

using System.ComponentModel;

namespace Demo.yFiles.Complete.IsometricDrawing
{
  partial class IsometricDrawingDemo
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IsometricDrawingDemo));
      this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
      this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.FitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.hlButton = new System.Windows.Forms.ToolStripButton();
      this.otButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.OpenButton = new System.Windows.Forms.ToolStripButton();
      this.PrintButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripContainer.ContentPanel.SuspendLayout();
      this.toolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer.SuspendLayout();
      this.toolStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer
      // 
      // 
      // toolStripContainer.ContentPanel
      // 
      this.toolStripContainer.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(865, 722);
      this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer.Name = "toolStripContainer";
      this.toolStripContainer.Size = new System.Drawing.Size(865, 753);
      this.toolStripContainer.TabIndex = 1;
      this.toolStripContainer.Text = "toolStripContainer1";
      // 
      // toolStripContainer.TopToolStripPanel
      // 
      this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
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
      this.graphControl.Size = new System.Drawing.Size(865, 722);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 1;
      // 
      // toolStrip
      // 
      this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.OpenButton, this.toolStripSeparator4, this.PrintButton, this.toolStripSeparator3, this.ZoomInButton, this.ZoomOutButton, this.FitContentButton, this.toolStripSeparator1, this.hlButton, this.otButton, this.toolStripSeparator2, this.toolStripButton2, this.toolStripLabel1, this.toolStripButton1 });
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip.Size = new System.Drawing.Size(865, 31);
      this.toolStrip.Stretch = true;
      this.toolStrip.TabIndex = 0;
      // 
      // ZoomInButton
      // 
      this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomInButton.Image = ((System.Drawing.Image) (resources.GetObject("ZoomInButton.Image")));
      this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomInButton.Name = "ZoomInButton";
      this.ZoomInButton.Size = new System.Drawing.Size(23, 20);
      this.ZoomInButton.Text = "Zoom In";
      // 
      // ZoomOutButton
      // 
      this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ZoomOutButton.Image = ((System.Drawing.Image) (resources.GetObject("ZoomOutButton.Image")));
      this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ZoomOutButton.Name = "ZoomOutButton";
      this.ZoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.ZoomOutButton.Text = "Zoom Out";
      // 
      // FitContentButton
      // 
      this.FitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.FitContentButton.Image = ((System.Drawing.Image) (resources.GetObject("FitContentButton.Image")));
      this.FitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.FitContentButton.Name = "FitContentButton";
      this.FitContentButton.Size = new System.Drawing.Size(23, 20);
      this.FitContentButton.Text = "Fit Content into View";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // hlButton
      // 
      this.hlButton.Image = ((System.Drawing.Image) (resources.GetObject("hlButton.Image")));
      this.hlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.hlButton.Name = "hlButton";
      this.hlButton.Size = new System.Drawing.Size(120, 20);
      this.hlButton.Text = "Hierarchic Layout";
      this.hlButton.ToolTipText = "Perform hierarchic layout";
      this.hlButton.Click += new System.EventHandler(this.OnHLLayoutClick);
      // 
      // otButton
      // 
      this.otButton.Image = ((System.Drawing.Image) (resources.GetObject("otButton.Image")));
      this.otButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.otButton.Name = "otButton";
      this.otButton.Size = new System.Drawing.Size(127, 20);
      this.otButton.Text = "Orthogonal Layout";
      this.otButton.ToolTipText = "Perform orthogonal layout";
      this.otButton.Click += new System.EventHandler(this.OnOTLayoutClick);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton2.Image")));
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 20);
      this.toolStripButton2.Text = "toolStripButton2";
      this.toolStripButton2.ToolTipText = "Rotate to the left";
      this.toolStripButton2.Click += new System.EventHandler(this.OnRotateLeft);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(52, 20);
      this.toolStripLabel1.Text = "Rotation";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 20);
      this.toolStripButton1.Text = "toolStripButton1";
      this.toolStripButton1.ToolTipText = "Rotate to the right";
      this.toolStripButton1.Click += new System.EventHandler(this.OnRotateRight);
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
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer);
      this.splitContainer1.Size = new System.Drawing.Size(1122, 753);
      this.splitContainer1.SplitterDistance = 252;
      this.splitContainer1.SplitterWidth = 5;
      this.splitContainer1.TabIndex = 2;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 753);
      this.description.TabIndex = 5;
      this.description.TabStop = false;
      this.description.Text = "";
      // 
      // OpenButton
      // 
      this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.OpenButton.Image = ((System.Drawing.Image) (resources.GetObject("OpenButton.Image")));
      this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.OpenButton.Name = "OpenButton";
      this.OpenButton.Size = new System.Drawing.Size(23, 20);
      this.OpenButton.Text = "toolStripButton3";
      this.OpenButton.ToolTipText = "Open...";
      // 
      // PrintButton
      // 
      this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.PrintButton.Image = ((System.Drawing.Image) (resources.GetObject("PrintButton.Image")));
      this.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.PrintButton.Name = "PrintButton";
      this.PrintButton.Size = new System.Drawing.Size(23, 20);
      this.PrintButton.Text = "toolStripButton4";
      this.PrintButton.ToolTipText = "Print...";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
      // 
      // IsometricDrawingDemo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1122, 753);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
      this.Name = "IsometricDrawingDemo";
      this.Text = "Isometric Drawing Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.toolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolStripContainer.ResumeLayout(false);
      this.toolStripContainer.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton ZoomInButton;
    private System.Windows.Forms.ToolStripButton ZoomOutButton;
    private System.Windows.Forms.ToolStripButton FitContentButton;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;  
    private System.Windows.Forms.ToolStripButton hlButton;
    private System.Windows.Forms.ToolStripButton otButton;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton PrintButton;
    private System.Windows.Forms.ToolStripButton OpenButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
  }
}
