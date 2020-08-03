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

namespace Demo.yFiles.Graph.Input.MoveUnselectedNodes
{
  partial class MoveUnselectedNodesForm : Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveUnselectedNodesForm));
      yWorks.Controls.ViewportLimiter viewportLimiter6 = new yWorks.Controls.ViewportLimiter();
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbMode = new System.Windows.Forms.ToolStripComboBox();
      this.btnEnable = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnClassic = new System.Windows.Forms.ToolStripButton();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.splitContainer.Panel1.SuspendLayout();
      this.splitContainer.Panel2.SuspendLayout();
      this.splitContainer.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer
      // 
      this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer.Location = new System.Drawing.Point(0, 0);
      this.splitContainer.Name = "splitContainer";
      // 
      // splitContainer.Panel1
      // 
      this.splitContainer.Panel1.Controls.Add(this.description);
      // 
      // splitContainer.Panel2
      // 
      this.splitContainer.Panel2.Controls.Add(this.toolStrip1);
      this.splitContainer.Panel2.Controls.Add(this.graphControl);
      this.splitContainer.Size = new System.Drawing.Size(925, 594);
      this.splitContainer.SplitterDistance = 252;
      this.splitContainer.TabIndex = 0;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 594);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbMode,
            this.btnEnable,
            this.toolStripSeparator2,
            this.btnClassic});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(669, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Graph.Input.MoveUnselectedNodes.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 22);
      this.zoomInButton.Text = "toolStripButton1";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Graph.Input.MoveUnselectedNodes.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
      this.zoomOutButton.Text = "toolStripButton2";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitContentButton.Image = global::Demo.yFiles.Graph.Input.MoveUnselectedNodes.Properties.Resources.fit_16;
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(23, 22);
      this.fitContentButton.Text = "toolStripButton3";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(138, 22);
      this.toolStripLabel1.Text = "Move Unselected Nodes:";
      this.toolStripLabel1.Click += new System.EventHandler(this.OnMoveModeChanged);
      // 
      // cmbMode
      // 
      this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbMode.Items.AddRange(new object[] {
            "Always",
            "Shift Not Pressed",
            "Drag at Top",
            "If Move Enabled",
            "Never"});
      this.cmbMode.Name = "cmbMode";
      this.cmbMode.Size = new System.Drawing.Size(121, 25);
      this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.OnMoveModeChanged);
      // 
      // btnEnable
      // 
      this.btnEnable.CheckOnClick = true;
      this.btnEnable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnEnable.Image = ((System.Drawing.Image)(resources.GetObject("btnEnable.Image")));
      this.btnEnable.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEnable.Name = "btnEnable";
      this.btnEnable.Size = new System.Drawing.Size(86, 22);
      this.btnEnable.Text = "Move Enabled";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnClassic
      // 
      this.btnClassic.CheckOnClick = true;
      this.btnClassic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnClassic.Image = ((System.Drawing.Image)(resources.GetObject("btnClassic.Image")));
      this.btnClassic.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnClassic.Name = "btnClassic";
      this.btnClassic.Size = new System.Drawing.Size(81, 22);
      this.btnClassic.Text = "Classic Mode";
      this.btnClassic.Click += new System.EventHandler(this.OnClassicClicked);
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(669, 594);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl";
      viewportLimiter6.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter6;
      // 
      // MoveUnselectedNodesForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(925, 594);
      this.Controls.Add(this.splitContainer);
      this.Icon = global::Demo.yFiles.Graph.Input.MoveUnselectedNodes.Properties.Resources.yIcon;
      this.Name = "MoveUnselectedNodesForm";
      this.Text = "Move Unselected Nodes Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.splitContainer.Panel1.ResumeLayout(false);
      this.splitContainer.Panel2.ResumeLayout(false);
      this.splitContainer.Panel2.PerformLayout();
      this.splitContainer.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.RichTextBox description;
    private yWorks.Controls.GraphControl graphControl;
    private ToolStrip toolStrip1;
    private ToolStripButton zoomOutButton;
    private ToolStripButton fitContentButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripLabel toolStripLabel1;
    private ToolStripComboBox cmbMode;
    private ToolStripButton btnEnable;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton btnClassic;
    private ToolStripButton zoomInButton;
  }
}