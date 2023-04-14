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

namespace Demo.yFiles.Layout.Tree
{
  partial class NodePlacerPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      yWorks.Controls.ViewportLimiter viewportLimiter1 = new yWorks.Controls.ViewportLimiter();
      this.label1 = new System.Windows.Forms.Label();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.levelUpDown = new System.Windows.Forms.NumericUpDown();
      this.layerVisualizationBorder = new System.Windows.Forms.Panel();
      this.applyConfigButton = new System.Windows.Forms.Button();
      this.reloadConfigurationsButton = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.nodePlacerTypeComboBox = new System.Windows.Forms.ComboBox();
      this.rotationGrid = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.leftRotationButton = new System.Windows.Forms.Button();
      this.rightRotationButton = new System.Windows.Forms.Button();
      this.horizMirrorButton = new System.Windows.Forms.Button();
      this.vertMirrorButton = new System.Windows.Forms.Button();
      this.placerOptionsBox = new System.Windows.Forms.GroupBox();
      this.nodeSettingsLabel = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.nodePlacerTypeLabel = new System.Windows.Forms.Label();
      this.nodePlacerDescriptionTextBox = new System.Windows.Forms.TextBox();
      this.panel4 = new System.Windows.Forms.Panel();
      this.previewControl = new yWorks.Controls.GraphControl();
      this.flowLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.levelUpDown)).BeginInit();
      this.rotationGrid.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.placerOptionsBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.label1.Size = new System.Drawing.Size(283, 30);
      this.label1.TabIndex = 0;
      this.label1.Text = "Node Placer Settings";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.label2);
      this.flowLayoutPanel1.Controls.Add(this.levelUpDown);
      this.flowLayoutPanel1.Controls.Add(this.layerVisualizationBorder);
      this.flowLayoutPanel1.Controls.Add(this.applyConfigButton);
      this.flowLayoutPanel1.Controls.Add(this.reloadConfigurationsButton);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 30);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
      this.flowLayoutPanel1.Size = new System.Drawing.Size(283, 45);
      this.flowLayoutPanel1.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(13, 18);
      this.label2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(36, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Level:";
      // 
      // levelUpDown
      // 
      this.levelUpDown.Location = new System.Drawing.Point(55, 15);
      this.levelUpDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.levelUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.levelUpDown.Name = "levelUpDown";
      this.levelUpDown.Size = new System.Drawing.Size(53, 20);
      this.levelUpDown.TabIndex = 1;
      this.levelUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.levelUpDown.ValueChanged += new System.EventHandler(this.levelUpDown_ValueChanged);
      // 
      // layerVisualizationBorder
      // 
      this.layerVisualizationBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.layerVisualizationBorder.Location = new System.Drawing.Point(114, 13);
      this.layerVisualizationBorder.Name = "layerVisualizationBorder";
      this.layerVisualizationBorder.Size = new System.Drawing.Size(34, 25);
      this.layerVisualizationBorder.TabIndex = 2;
      // 
      // applyConfigButton
      // 
      this.applyConfigButton.Location = new System.Drawing.Point(154, 13);
      this.applyConfigButton.Name = "applyConfigButton";
      this.applyConfigButton.Size = new System.Drawing.Size(47, 25);
      this.applyConfigButton.TabIndex = 3;
      this.applyConfigButton.Text = "Apply";
      this.applyConfigButton.UseVisualStyleBackColor = true;
      this.applyConfigButton.Click += new System.EventHandler(this.OnApplyButtonClicked);
      // 
      // reloadConfigurationsButton
      // 
      this.reloadConfigurationsButton.Image = global::Demo.yFiles.Layout.Tree.Properties.Resources.reload_16;
      this.reloadConfigurationsButton.Location = new System.Drawing.Point(207, 13);
      this.reloadConfigurationsButton.Name = "reloadConfigurationsButton";
      this.reloadConfigurationsButton.Size = new System.Drawing.Size(50, 25);
      this.reloadConfigurationsButton.TabIndex = 4;
      this.reloadConfigurationsButton.UseVisualStyleBackColor = true;
      this.reloadConfigurationsButton.Click += new System.EventHandler(this.OnReloadButtonClicked);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(23, 96);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(34, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Type:";
      // 
      // nodePlacerTypeComboBox
      // 
      this.nodePlacerTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.nodePlacerTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.nodePlacerTypeComboBox.FormattingEnabled = true;
      this.nodePlacerTypeComboBox.Location = new System.Drawing.Point(73, 93);
      this.nodePlacerTypeComboBox.Name = "nodePlacerTypeComboBox";
      this.nodePlacerTypeComboBox.Size = new System.Drawing.Size(191, 21);
      this.nodePlacerTypeComboBox.TabIndex = 3;
      this.nodePlacerTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.NodePlacerComboBoxSelectionChanged);
      // 
      // rotationGrid
      // 
      this.rotationGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.rotationGrid.Controls.Add(this.tableLayoutPanel1);
      this.rotationGrid.Location = new System.Drawing.Point(17, 129);
      this.rotationGrid.Name = "rotationGrid";
      this.rotationGrid.Size = new System.Drawing.Size(250, 100);
      this.rotationGrid.TabIndex = 4;
      this.rotationGrid.TabStop = false;
      this.rotationGrid.Text = "Rotation";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.leftRotationButton, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.rightRotationButton, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.horizMirrorButton, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.vertMirrorButton, 2, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 81);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(3, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(39, 40);
      this.label4.TabIndex = 0;
      this.label4.Text = "Rotate";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Location = new System.Drawing.Point(3, 40);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(39, 41);
      this.label5.TabIndex = 1;
      this.label5.Text = "Mirror";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // leftRotationButton
      // 
      this.leftRotationButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.leftRotationButton.Location = new System.Drawing.Point(53, 8);
      this.leftRotationButton.Margin = new System.Windows.Forms.Padding(8);
      this.leftRotationButton.Name = "leftRotationButton";
      this.leftRotationButton.Size = new System.Drawing.Size(83, 24);
      this.leftRotationButton.TabIndex = 2;
      this.leftRotationButton.Text = "Left";
      this.leftRotationButton.UseVisualStyleBackColor = true;
      this.leftRotationButton.Click += new System.EventHandler(this.LeftRotateButtonClicked);
      // 
      // rightRotationButton
      // 
      this.rightRotationButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rightRotationButton.Location = new System.Drawing.Point(152, 8);
      this.rightRotationButton.Margin = new System.Windows.Forms.Padding(8);
      this.rightRotationButton.Name = "rightRotationButton";
      this.rightRotationButton.Size = new System.Drawing.Size(84, 24);
      this.rightRotationButton.TabIndex = 3;
      this.rightRotationButton.Text = "Right";
      this.rightRotationButton.UseVisualStyleBackColor = true;
      this.rightRotationButton.Click += new System.EventHandler(this.RightRotateButtonClicked);
      // 
      // horizMirrorButton
      // 
      this.horizMirrorButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.horizMirrorButton.Location = new System.Drawing.Point(53, 48);
      this.horizMirrorButton.Margin = new System.Windows.Forms.Padding(8);
      this.horizMirrorButton.Name = "horizMirrorButton";
      this.horizMirrorButton.Size = new System.Drawing.Size(83, 25);
      this.horizMirrorButton.TabIndex = 4;
      this.horizMirrorButton.Text = "Horiz";
      this.horizMirrorButton.UseVisualStyleBackColor = true;
      this.horizMirrorButton.Click += new System.EventHandler(this.MirrorHorizButtonClicked);
      // 
      // vertMirrorButton
      // 
      this.vertMirrorButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.vertMirrorButton.Location = new System.Drawing.Point(152, 48);
      this.vertMirrorButton.Margin = new System.Windows.Forms.Padding(8);
      this.vertMirrorButton.Name = "vertMirrorButton";
      this.vertMirrorButton.Size = new System.Drawing.Size(84, 25);
      this.vertMirrorButton.TabIndex = 5;
      this.vertMirrorButton.Text = "Vert";
      this.vertMirrorButton.UseVisualStyleBackColor = true;
      this.vertMirrorButton.Click += new System.EventHandler(this.MirrorVertButtonClicked);
      // 
      // placerOptionsBox
      // 
      this.placerOptionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.placerOptionsBox.Controls.Add(this.nodeSettingsLabel);
      this.placerOptionsBox.Location = new System.Drawing.Point(16, 247);
      this.placerOptionsBox.Name = "placerOptionsBox";
      this.placerOptionsBox.Size = new System.Drawing.Size(250, 206);
      this.placerOptionsBox.TabIndex = 5;
      this.placerOptionsBox.TabStop = false;
      this.placerOptionsBox.Text = "Placer Settings";
      // 
      // nodeSettingsLabel
      // 
      this.nodeSettingsLabel.AutoSize = true;
      this.nodeSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nodeSettingsLabel.ForeColor = System.Drawing.SystemColors.ScrollBar;
      this.nodeSettingsLabel.Location = new System.Drawing.Point(7, 29);
      this.nodeSettingsLabel.Name = "nodeSettingsLabel";
      this.nodeSettingsLabel.Size = new System.Drawing.Size(155, 16);
      this.nodeSettingsLabel.TabIndex = 0;
      this.nodeSettingsLabel.Text = "No settings available";
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.Black;
      this.panel2.Location = new System.Drawing.Point(10, 460);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(257, 1);
      this.panel2.TabIndex = 6;
      // 
      // panel3
      // 
      this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel3.BackColor = System.Drawing.Color.Black;
      this.panel3.Location = new System.Drawing.Point(10, 81);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(257, 1);
      this.panel3.TabIndex = 7;
      // 
      // nodePlacerTypeLabel
      // 
      this.nodePlacerTypeLabel.AutoSize = true;
      this.nodePlacerTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nodePlacerTypeLabel.Location = new System.Drawing.Point(13, 469);
      this.nodePlacerTypeLabel.Name = "nodePlacerTypeLabel";
      this.nodePlacerTypeLabel.Size = new System.Drawing.Size(99, 13);
      this.nodePlacerTypeLabel.TabIndex = 8;
      this.nodePlacerTypeLabel.Text = "nodePlacerType";
      // 
      // nodePlacerDescriptionTextBox
      // 
      this.nodePlacerDescriptionTextBox.BackColor = System.Drawing.Color.White;
      this.nodePlacerDescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.nodePlacerDescriptionTextBox.Location = new System.Drawing.Point(12, 485);
      this.nodePlacerDescriptionTextBox.Multiline = true;
      this.nodePlacerDescriptionTextBox.Name = "nodePlacerDescriptionTextBox";
      this.nodePlacerDescriptionTextBox.ReadOnly = true;
      this.nodePlacerDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
      this.nodePlacerDescriptionTextBox.Size = new System.Drawing.Size(255, 82);
      this.nodePlacerDescriptionTextBox.TabIndex = 9;
      // 
      // panel4
      // 
      this.panel4.BackColor = System.Drawing.Color.Black;
      this.panel4.Location = new System.Drawing.Point(10, 575);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(257, 1);
      this.panel4.TabIndex = 10;
      // 
      // previewControl
      // 
      this.previewControl.BackColor = System.Drawing.Color.White;
      this.previewControl.ContentMargins = new yWorks.Geometry.InsetsD(10D, 10D, 10D, 10D);
      this.previewControl.ContentRect = new yWorks.Geometry.RectD(0D, 0D, 400D, 400D);
      this.previewControl.Location = new System.Drawing.Point(12, 590);
      this.previewControl.Name = "previewControl";
      this.previewControl.Size = new System.Drawing.Size(257, 300);
      this.previewControl.TabIndex = 11;
      this.previewControl.Text = "graphControl1";
      viewportLimiter1.Bounds = null;
      this.previewControl.ViewportLimiter = viewportLimiter1;
      // 
      // NodePlacerPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.previewControl);
      this.Controls.Add(this.panel4);
      this.Controls.Add(this.nodePlacerDescriptionTextBox);
      this.Controls.Add(this.nodePlacerTypeLabel);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.placerOptionsBox);
      this.Controls.Add(this.rotationGrid);
      this.Controls.Add(this.nodePlacerTypeComboBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.label1);
      this.MinimumSize = new System.Drawing.Size(283, 2);
      this.Name = "NodePlacerPanel";
      this.Size = new System.Drawing.Size(283, 847);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.levelUpDown)).EndInit();
      this.rotationGrid.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.placerOptionsBox.ResumeLayout(false);
      this.placerOptionsBox.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown levelUpDown;
    private System.Windows.Forms.Panel layerVisualizationBorder;
    private System.Windows.Forms.Button applyConfigButton;
    private System.Windows.Forms.Button reloadConfigurationsButton;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox nodePlacerTypeComboBox;
    private System.Windows.Forms.GroupBox rotationGrid;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button leftRotationButton;
    private System.Windows.Forms.Button rightRotationButton;
    private System.Windows.Forms.Button horizMirrorButton;
    private System.Windows.Forms.Button vertMirrorButton;
    private System.Windows.Forms.GroupBox placerOptionsBox;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label nodePlacerTypeLabel;
    private System.Windows.Forms.TextBox nodePlacerDescriptionTextBox;
    private System.Windows.Forms.Panel panel4;
    private yWorks.Controls.GraphControl previewControl;
    private System.Windows.Forms.Label nodeSettingsLabel;
  }
}
