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

namespace Demo.yFiles.Input.DragAndDrop
{
  partial class DragAndDropForm : Form
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
      yWorks.Controls.ViewportLimiter viewportLimiter2 = new yWorks.Controls.ViewportLimiter();
      this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
      this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
      this.styleListBox = new System.Windows.Forms.ListBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.featuresComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.splitContainerHorizontal.Panel1.SuspendLayout();
      this.splitContainerHorizontal.Panel2.SuspendLayout();
      this.splitContainerHorizontal.SuspendLayout();
      this.splitContainerVertical.Panel1.SuspendLayout();
      this.splitContainerVertical.Panel2.SuspendLayout();
      this.splitContainerVertical.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
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
      this.splitContainerVertical.Panel1.Controls.Add(this.styleListBox);
      // 
      // splitContainerVertical.Panel2
      // 
      this.splitContainerVertical.Panel2.Controls.Add(this.graphControl);
      this.splitContainerVertical.Size = new System.Drawing.Size(528, 533);
      this.splitContainerVertical.SplitterDistance = 80;
      this.splitContainerVertical.TabIndex = 1;
      // 
      // styleListBox
      // 
      this.styleListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.styleListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.styleListBox.FormattingEnabled = true;
      this.styleListBox.IntegralHeight = false;
      this.styleListBox.ItemHeight = 80;
      this.styleListBox.Location = new System.Drawing.Point(0, 0);
      this.styleListBox.Name = "styleListBox";
      this.styleListBox.Size = new System.Drawing.Size(80, 533);
      this.styleListBox.TabIndex = 2;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.featuresComboBox});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(528, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 3;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(100, 20);
      this.toolStripLabel1.Text = "D'n'D Features:";
      // 
      // featuresComboBox
      // 
      this.featuresComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.featuresComboBox.Name = "functionsComboBox";
      this.featuresComboBox.Size = new System.Drawing.Size(150, 23);
      this.featuresComboBox.SelectedIndexChanged += new System.EventHandler(this.FeatureSelectionChanged);
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
      this.graphControl.Size = new System.Drawing.Size(444, 533);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 2;
      this.graphControl.Text = "graphControl";
      viewportLimiter2.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter2;
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
      // DragAndDropForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 564);
      this.Controls.Add(this.splitContainerHorizontal);
      this.Icon = global::Demo.yFiles.Graph.Input.DragAndDrop.Properties.Resources.yIcon;
      this.Name = "DragAndDropForm";
      this.Text = "Drag And Drop Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.splitContainerHorizontal.Panel1.ResumeLayout(false);
      this.splitContainerHorizontal.Panel2.ResumeLayout(false);
      this.splitContainerHorizontal.ResumeLayout(false);
      this.splitContainerVertical.Panel1.ResumeLayout(false);
      this.splitContainerVertical.Panel2.ResumeLayout(false);
      this.splitContainerVertical.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainerHorizontal;
    private System.Windows.Forms.RichTextBox description;
    private SplitContainer splitContainerVertical;
    private ListBox styleListBox;
    private yWorks.Controls.GraphControl graphControl;
    private ToolStrip toolStrip1;
    private ToolStripLabel toolStripLabel1;
    private ToolStripComboBox featuresComboBox;
    private ToolStripContainer toolStripContainer1;
  }
}
