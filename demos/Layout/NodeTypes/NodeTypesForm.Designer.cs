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


namespace Demo.yFiles.Layout.NodeTypes
{
  partial class NodeTypesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeTypesForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.description = new System.Windows.Forms.RichTextBox();
            this.graphControl = new yWorks.Controls.GraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.fitContentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.PreviousSampleButton = new System.Windows.Forms.ToolStripButton();
            this.SampleGraphComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.NextSampleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ConsiderTypes = new System.Windows.Forms.ToolStripButton();
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
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.PreviousSampleButton,
            this.SampleGraphComboBox,
            this.NextSampleButton,
            this.toolStripSeparator1,
            this.runButton,
            this.toolStripSeparator3,
            this.ConsiderTypes});
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
            this.zoomInButton.Image = global::Demo.yFiles.Layout.NodeTypes.Properties.Resources.plus_16;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(34, 29);
            this.zoomInButton.Text = "Zoom In";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::Demo.yFiles.Layout.NodeTypes.Properties.Resources.minus_16;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(34, 29);
            this.zoomOutButton.Text = "Zoom Out";
            // 
            // fitContentButton
            // 
            this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fitContentButton.Image = global::Demo.yFiles.Layout.NodeTypes.Properties.Resources.fit_16;
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(128, 29);
            this.toolStripLabel1.Text = "Sample Graph:";
            // 
            // PreviousSampleButton
            // 
            this.PreviousSampleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PreviousSampleButton.Image = global::Demo.yFiles.Layout.NodeTypes.Properties.Resources.arrow_left_16;
            this.PreviousSampleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviousSampleButton.Name = "PreviousSampleButton";
            this.PreviousSampleButton.Size = new System.Drawing.Size(34, 29);
            this.PreviousSampleButton.Text = "toolStripButton1";
            this.PreviousSampleButton.Click += new System.EventHandler(this.LoadPreviousSampleGraph);
            // 
            // SampleGraphComboBox
            // 
            this.SampleGraphComboBox.Name = "SampleGraphComboBox";
            this.SampleGraphComboBox.Size = new System.Drawing.Size(121, 34);
            this.SampleGraphComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSampleChanged);
            // 
            // NextSampleButton
            // 
            this.NextSampleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextSampleButton.Image = global::Demo.yFiles.Layout.NodeTypes.Properties.Resources.arrow_right_16;
            this.NextSampleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextSampleButton.Name = "NextSampleButton";
            this.NextSampleButton.Size = new System.Drawing.Size(34, 29);
            this.NextSampleButton.Text = "toolStripButton1";
            this.NextSampleButton.Click += new System.EventHandler(this.LoadNextSampleGraph);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // runButton
            // 
            this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(69, 29);
            this.runButton.Text = "Layout";
            this.runButton.ToolTipText = "Run the Layout";
            this.runButton.Click += new System.EventHandler(this.OnLayoutClicked);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // ConsiderTypes
            // 
            this.ConsiderTypes.Checked = true;
            this.ConsiderTypes.CheckOnClick = true;
            this.ConsiderTypes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConsiderTypes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ConsiderTypes.Image = ((System.Drawing.Image)(resources.GetObject("ConsiderTypes.Image")));
            this.ConsiderTypes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConsiderTypes.Name = "ConsiderTypes";
            this.ConsiderTypes.Size = new System.Drawing.Size(136, 29);
            this.ConsiderTypes.Text = "Consider Types";
            this.ConsiderTypes.Click += new System.EventHandler(this.OnConsiderTypesClicked);
            // 
            // NodeTypesForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 637);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::Demo.yFiles.Layout.NodeTypes.Properties.Resources.yIcon;
            this.Name = "NodeTypesForm";
            this.Text = "NodeTypes Demo";
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
    private ToolStripButton runButton;
    private ToolStripComboBox SampleGraphComboBox;
    private ToolStripButton PreviousSampleButton;
    private ToolStripButton NextSampleButton;
    private ToolStripLabel toolStripLabel1;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton ConsiderTypes;
  }
}
