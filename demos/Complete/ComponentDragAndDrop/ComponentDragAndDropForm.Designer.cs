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

namespace Demo.yFiles.Graph.ComponentDragAndDrop
{
  partial class ComponentDragAndDropForm : Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentDragAndDropForm));
      this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
      this.paletteListBox = new System.Windows.Forms.ListBox();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.undoButton = new System.Windows.Forms.ToolStripButton();
      this.redoButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.zoomFitButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.keepComponentsButton = new System.Windows.Forms.ToolStripButton();
      ((System.ComponentModel.ISupportInitialize) (this.splitContainerHorizontal)).BeginInit();
      this.splitContainerHorizontal.Panel1.SuspendLayout();
      this.splitContainerHorizontal.Panel2.SuspendLayout();
      this.splitContainerHorizontal.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize) (this.splitContainerVertical)).BeginInit();
      this.splitContainerVertical.Panel1.SuspendLayout();
      this.splitContainerVertical.Panel2.SuspendLayout();
      this.splitContainerVertical.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainerHorizontal
      // 
      this.splitContainerHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerHorizontal.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainerHorizontal.Location = new System.Drawing.Point(0, 0);
      this.splitContainerHorizontal.Name = "splitContainerHorizontal";
      // 
      // splitContainerHorizontal.Panel1
      // 
      this.splitContainerHorizontal.Panel1.Controls.Add(this.description);
      // 
      // splitContainerHorizontal.Panel2
      // 
      this.splitContainerHorizontal.Panel2.Controls.Add(this.toolStripContainer1);
      this.splitContainerHorizontal.Size = new System.Drawing.Size(784, 564);
      this.splitContainerHorizontal.SplitterDistance = 252;
      this.splitContainerHorizontal.TabIndex = 0;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 564);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerVertical);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(528, 533);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(528, 564);
      this.toolStripContainer1.TabIndex = 3;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // splitContainerVertical
      // 
      this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainerVertical.IsSplitterFixed = true;
      this.splitContainerVertical.Location = new System.Drawing.Point(0, 0);
      this.splitContainerVertical.Name = "splitContainerVertical";
      // 
      // splitContainerVertical.Panel1
      // 
      this.splitContainerVertical.Panel1.Controls.Add(this.paletteListBox);
      // 
      // splitContainerVertical.Panel2
      // 
      this.splitContainerVertical.Panel2.Controls.Add(this.graphControl);
      this.splitContainerVertical.Size = new System.Drawing.Size(528, 533);
      this.splitContainerVertical.SplitterDistance = 160;
      this.splitContainerVertical.TabIndex = 1;
      // 
      // paletteListBox
      // 
      this.paletteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.paletteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.paletteListBox.FormattingEnabled = true;
      this.paletteListBox.IntegralHeight = false;
      this.paletteListBox.ItemHeight = 160;
      this.paletteListBox.Location = new System.Drawing.Point(0, 0);
      this.paletteListBox.Name = "paletteListBox";
      this.paletteListBox.Size = new System.Drawing.Size(160, 533);
      this.paletteListBox.TabIndex = 2;
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
      this.graphControl.Size = new System.Drawing.Size(364, 533);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 2;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.undoButton, this.redoButton, this.toolStripSeparator1, this.zoomInButton, this.zoomOutButton, this.zoomFitButton, this.toolStripSeparator2, this.keepComponentsButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(528, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 3;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // undoButton
      // 
      this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.undoButton.Image = global::Demo.yFiles.Graph.ComponentDragAndDrop.Properties.Resources.undo_16;
      this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.undoButton.Name = "undoButton";
      this.undoButton.Size = new System.Drawing.Size(23, 20);
      this.undoButton.Text = "Undo";
      // 
      // redoButton
      // 
      this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.redoButton.Image = global::Demo.yFiles.Graph.ComponentDragAndDrop.Properties.Resources.redo_16;
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
      this.zoomInButton.Image = global::Demo.yFiles.Graph.ComponentDragAndDrop.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "Increase Zoom";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.ComponentDragAndDrop.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "Decrease Zoom";
      // 
      // zoomFitButton
      // 
      this.zoomFitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomFitButton.Image = global::Demo.yFiles.Graph.ComponentDragAndDrop.Properties.Resources.fit_16;
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
      // keepComponentsButton
      // 
      this.keepComponentsButton.CheckOnClick = true;
      this.keepComponentsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.keepComponentsButton.Image = ((System.Drawing.Image) (resources.GetObject("keepComponentsButton.Image")));
      this.keepComponentsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.keepComponentsButton.Name = "keepComponentsButton";
      this.keepComponentsButton.Size = new System.Drawing.Size(109, 20);
      this.keepComponentsButton.Text = "Keep Components";
      this.keepComponentsButton.ToolTipText = "Prevent the components from being changed during the drag and drop operation";
      // 
      // ComponentDragAndDropForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 564);
      this.Controls.Add(this.splitContainerHorizontal);
      this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
      this.Name = "ComponentDragAndDropForm";
      this.Text = "Component Drag And Drop Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoad);
      this.splitContainerHorizontal.Panel1.ResumeLayout(false);
      this.splitContainerHorizontal.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize) (this.splitContainerHorizontal)).EndInit();
      this.splitContainerHorizontal.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitContainerVertical.Panel1.ResumeLayout(false);
      this.splitContainerVertical.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize) (this.splitContainerVertical)).EndInit();
      this.splitContainerVertical.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
    }

    private System.Windows.Forms.RichTextBox description;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripButton keepComponentsButton;
    private System.Windows.Forms.ListBox paletteListBox;
    private System.Windows.Forms.ToolStripButton redoButton;
    private System.Windows.Forms.SplitContainer splitContainerHorizontal;
    private System.Windows.Forms.SplitContainer splitContainerVertical;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton undoButton;
    private System.Windows.Forms.ToolStripButton zoomFitButton;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;

    #endregion
  }
}
