/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.6.
 ** Copyright (c) 2000-2024 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing;
using System.Windows.Forms;
using yWorks.Controls;


namespace Demo.yFiles.Layout.SankeyLayout
{
  partial class SankeyForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.description = new System.Windows.Forms.RichTextBox();
            this.graphControl = new yWorks.Controls.GraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.fitContentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.directionComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.useDrawingAsSketch = new System.Windows.Forms.ToolStripButton();
            this.layoutButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphControl);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(900, 637);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.TabIndex = 0;
            // 
            // description
            // 
            this.description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.description.Location = new System.Drawing.Point(0, 0);
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Size = new System.Drawing.Size(252, 637);
            this.description.TabIndex = 1;
            this.description.Text = "";
            // 
            // graphControl
            // 
            this.graphControl.BackColor = System.Drawing.Color.White;
            this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.graphControl.FileOperationsEnabled = true;
            this.graphControl.Location = new System.Drawing.Point(0, 42);
            this.graphControl.Name = "graphControl";
            this.graphControl.Size = new System.Drawing.Size(644, 595);
            this.graphControl.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.directionComboBox,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.useDrawingAsSketch,
            this.layoutButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
            this.toolStrip1.Size = new System.Drawing.Size(644, 42);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = global::Demo.yFiles.Layout.SankeyLayout.Properties.Resources.plus_16;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(34, 29);
            this.zoomInButton.Text = "Zoom In";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::Demo.yFiles.Layout.SankeyLayout.Properties.Resources.minus_16;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(34, 29);
            this.zoomOutButton.Text = "Zoom Out";
            // 
            // fitContentButton
            // 
            this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fitContentButton.Image = global::Demo.yFiles.Layout.SankeyLayout.Properties.Resources.fit_16;
            this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fitContentButton.Name = "fitContentButton";
            this.fitContentButton.Size = new System.Drawing.Size(34, 29);
            this.fitContentButton.Text = "Fit Content";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.Image = global::Demo.yFiles.Layout.SankeyLayout.Properties.Resources.undo_16;
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 20);
            this.undoButton.Text = "Undo";
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.Image = global::Demo.yFiles.Layout.SankeyLayout.Properties.Resources.redo_16;
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(23, 20);
            this.redoButton.Text = "Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(128, 29);
            this.toolStripLabel1.Text = "Nodes determine the color of ";
            // 
            // directionComboBox
            // 
            this.directionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.directionComboBox.Items.AddRange(new object[] {
            "incoming",
            "outgoing"});
            this.directionComboBox.Name = "directionComboBox";
            this.directionComboBox.Size = new System.Drawing.Size(121, 34);
            this.directionComboBox.SelectedIndex = 1;
            this.directionComboBox.SelectedIndexChanged += new System.EventHandler(this.OnDirectionChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(128, 29);
            this.toolStripLabel2.Text = "edges ";
            // 
            // useDrawingAsSketch
            // 
            this.useDrawingAsSketch.Checked = true;
            this.useDrawingAsSketch.CheckOnClick = true;
            this.useDrawingAsSketch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useDrawingAsSketch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.useDrawingAsSketch.Name = "useDrawingAsSketch";
            this.useDrawingAsSketch.Size = new System.Drawing.Size(136, 29);
            this.useDrawingAsSketch.Text = "Use Drawing as Sketch";
            // 
            // layoutButton
            // 
            this.layoutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.layoutButton.Name = "layoutButton";
            this.layoutButton.Size = new System.Drawing.Size(69, 29);
            this.layoutButton.Text = "Layout";
            this.layoutButton.ToolTipText = "Run the Layout";
            this.layoutButton.Click += new System.EventHandler(this.OnLayoutButton);
            // 
            // SankeyForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 637);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::Demo.yFiles.Layout.SankeyLayout.Properties.Resources.yIcon;
            this.Name = "SankeyForm";
            this.Text = "Sankey Layout Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnLoaded);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton undoButton;
    private ToolStripButton redoButton;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton layoutButton;
    private ToolStripComboBox directionComboBox;
    private ToolStripLabel toolStripLabel1;
    private ToolStripLabel toolStripLabel2;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton useDrawingAsSketch;
  }
}
