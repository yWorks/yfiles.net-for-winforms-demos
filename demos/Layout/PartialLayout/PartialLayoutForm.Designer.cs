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

namespace Demo.yFiles.Layout.PartialLayout
{
  partial class PartialLayoutForm
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.propertiesPanel = new System.Windows.Forms.Panel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.runLayoutButton = new System.Windows.Forms.Button();
      this.optionPanel = new System.Windows.Forms.Panel();
      this.scenarioPanel = new System.Windows.Forms.Panel();
      this.refreshButton = new System.Windows.Forms.Button();
      this.scenarioComboBox = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitToSizeButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.lockSelectionButton = new System.Windows.Forms.ToolStripButton();
      this.unlockSelectionButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.propertiesPanel.SuspendLayout();
      this.panel1.SuspendLayout();
      this.scenarioPanel.SuspendLayout();
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
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
      this.splitContainer1.Size = new System.Drawing.Size(990, 614);
      this.splitContainer1.SplitterDistance = 302;
      this.splitContainer1.TabIndex = 0;
      // 
      // description
      // 
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Margin = new System.Windows.Forms.Padding(10);
      this.description.Name = "description";
      this.description.Size = new System.Drawing.Size(302, 614);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer2);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(684, 583);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(684, 614);
      this.toolStripContainer1.TabIndex = 0;
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
      this.splitContainer2.Panel1.Controls.Add(this.graphControl);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.propertiesPanel);
      this.splitContainer2.Size = new System.Drawing.Size(684, 583);
      this.splitContainer2.SplitterDistance = 300;
      this.splitContainer2.TabIndex = 0;
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.graphControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.FileOperationsEnabled = true;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(373, 583);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl1";
      viewportLimiter2.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter2;
      // 
      // propertiesPanel
      // 
      this.propertiesPanel.Controls.Add(this.panel1);
      this.propertiesPanel.Controls.Add(this.optionPanel);
      this.propertiesPanel.Controls.Add(this.scenarioPanel);
      this.propertiesPanel.Controls.Add(this.label5);
      this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
      this.propertiesPanel.Name = "propertiesPanel";
      this.propertiesPanel.Size = new System.Drawing.Size(307, 583);
      this.propertiesPanel.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.AutoSize = true;
      this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.panel1.Controls.Add(this.runLayoutButton);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 63);
      this.panel1.Name = "panel1";
      this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.panel1.Size = new System.Drawing.Size(307, 52);
      this.panel1.TabIndex = 14;
      // 
      // runLayoutButton
      // 
      this.runLayoutButton.AutoSize = true;
      this.runLayoutButton.Image = global::Demo.yFiles.Layout.PartialLayout.Properties.Resources.play_16;
      this.runLayoutButton.Location = new System.Drawing.Point(6, 11);
      this.runLayoutButton.Name = "runLayoutButton";
      this.runLayoutButton.Size = new System.Drawing.Size(83, 33);
      this.runLayoutButton.TabIndex = 13;
      this.runLayoutButton.Text = "Layout";
      this.runLayoutButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.runLayoutButton.UseVisualStyleBackColor = true;
      this.runLayoutButton.Click += new System.EventHandler(this.OnRunButtonClicked);
      // 
      // optionPanel
      // 
      this.optionPanel.AutoSize = false;
      this.optionPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.optionPanel.Location = new System.Drawing.Point(0, 63);
      this.optionPanel.Name = "optionPanel";
      this.optionPanel.Size = new System.Drawing.Size(307, 200);
      this.optionPanel.TabIndex = 11;
      // 
      // scenarioPanel
      // 
      this.scenarioPanel.AutoSize = true;
      this.scenarioPanel.Controls.Add(this.refreshButton);
      this.scenarioPanel.Controls.Add(this.scenarioComboBox);
      this.scenarioPanel.Controls.Add(this.label1);
      this.scenarioPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.scenarioPanel.Location = new System.Drawing.Point(0, 23);
      this.scenarioPanel.Name = "scenarioPanel";
      this.scenarioPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.scenarioPanel.Size = new System.Drawing.Size(307, 40);
      this.scenarioPanel.TabIndex = 10;
      // 
      // refreshButton
      // 
      this.refreshButton.AutoSize = true;
      this.refreshButton.Image = global::Demo.yFiles.Layout.PartialLayout.Properties.Resources.reload_16;
      this.refreshButton.Location = new System.Drawing.Point(209, 9);
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(24, 23);
      this.refreshButton.TabIndex = 2;
      this.refreshButton.UseVisualStyleBackColor = true;
      this.refreshButton.Click += new System.EventHandler(this.OnRefreshButtonClicked);
      // 
      // scenarioComboBox
      // 
      this.scenarioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.scenarioComboBox.FormattingEnabled = true;
      this.scenarioComboBox.Location = new System.Drawing.Point(61, 11);
      this.scenarioComboBox.Name = "scenarioComboBox";
      this.scenarioComboBox.Size = new System.Drawing.Size(121, 21);
      this.scenarioComboBox.TabIndex = 1;
      this.scenarioComboBox.SelectedIndexChanged += new System.EventHandler(this.ScenarioComboBoxSelectedValueChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(52, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Scenario:";
      // 
      // label5
      // 
      this.label5.Dock = System.Windows.Forms.DockStyle.Top;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(0, 0);
      this.label5.Name = "label5";
      this.label5.Padding = new System.Windows.Forms.Padding(5);
      this.label5.Size = new System.Drawing.Size(307, 23);
      this.label5.TabIndex = 9;
      this.label5.Text = "Layout Settings";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitToSizeButton,
            this.toolStripSeparator1,
            this.lockSelectionButton,
            this.unlockSelectionButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(684, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 0;
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomInButton.Image = global::Demo.yFiles.Layout.PartialLayout.Properties.Resources.plus_16;
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(23, 20);
      this.zoomInButton.Text = "toolStripButton1";
      this.zoomInButton.ToolTipText = "Zoom in1";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.zoomOutButton.Image = global::Demo.yFiles.Layout.PartialLayout.Properties.Resources.minus_16;
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(23, 20);
      this.zoomOutButton.Text = "toolStripButton1";
      this.zoomOutButton.ToolTipText = "Zoom out";
      // 
      // fitToSizeButton
      // 
      this.fitToSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fitToSizeButton.Image = global::Demo.yFiles.Layout.PartialLayout.Properties.Resources.fit_16;
      this.fitToSizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitToSizeButton.Name = "fitToSizeButton";
      this.fitToSizeButton.Size = new System.Drawing.Size(23, 20);
      this.fitToSizeButton.Text = "toolStripButton1";
      this.fitToSizeButton.ToolTipText = "Fit graph bounds";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // lockSelectionButton
      // 
      this.lockSelectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.lockSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.lockSelectionButton.Name = "lockSelectionButton";
      this.lockSelectionButton.Size = new System.Drawing.Size(87, 20);
      this.lockSelectionButton.Text = "Lock Selection";
      this.lockSelectionButton.Click += new System.EventHandler(this.OnLockSelectionButtonClicked);
      // 
      // unlockSelectionButton
      // 
      this.unlockSelectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.unlockSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.unlockSelectionButton.Name = "unlockSelectionButton";
      this.unlockSelectionButton.Size = new System.Drawing.Size(99, 20);
      this.unlockSelectionButton.Text = "Unlock Selection";
      this.unlockSelectionButton.Click += new System.EventHandler(this.OnUnlockSelectionButtonClicked);
      // 
      // PartialLayoutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(990, 614);
      this.Controls.Add(this.splitContainer1);
      this.Icon = global::Demo.yFiles.Layout.PartialLayout.Properties.Resources.yIcon;
      this.Name = "PartialLayoutForm";
      this.Text = "PartialLayout Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.OnLoaded);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.propertiesPanel.ResumeLayout(false);
      this.propertiesPanel.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.scenarioPanel.ResumeLayout(false);
      this.scenarioPanel.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.Panel propertiesPanel;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitToSizeButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton lockSelectionButton;
    private System.Windows.Forms.ToolStripButton unlockSelectionButton;
    private Panel optionPanel;
    private Panel scenarioPanel;
    private Button refreshButton;
    private ComboBox scenarioComboBox;
    private Label label1;
    private Label label5;
    private Panel panel1;
    private Button runLayoutButton;
  }
}
