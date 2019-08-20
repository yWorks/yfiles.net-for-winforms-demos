/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.2.
 ** Copyright (c) 2000-2019 by yWorks GmbH, Vor dem Kreuzberg 28,
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

namespace Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder
{
  partial class InteractiveNodesGraphBuilderForm
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.description = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.graphControl = new yWorks.Controls.GraphControl();
			this.label2 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.nodesListBox = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.currentItemLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.predecessorsListBox = new System.Windows.Forms.ListBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.successorListBox = new System.Windows.Forms.ListBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
			this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.FitContentButton = new System.Windows.Forms.ToolStripButton();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
			this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
			this.splitContainer.Panel2.Controls.Add(this.toolStrip1);
			this.splitContainer.Size = new System.Drawing.Size(836, 590);
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
			this.description.Size = new System.Drawing.Size(252, 590);
			this.description.TabIndex = 0;
			this.description.Text = "";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 31);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.graphControl);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(580, 559);
			this.splitContainer1.SplitterDistance = 259;
			this.splitContainer1.TabIndex = 3;
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
			this.graphControl.Size = new System.Drawing.Size(259, 559);
			this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			this.graphControl.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AllowDrop = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Image = global::Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder.Properties.Resources.delete3_16;
			this.label2.Location = new System.Drawing.Point(139, 522);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 32);
			this.label2.TabIndex = 1;
			this.label2.DragDrop += new System.Windows.Forms.DragEventHandler(this.trashcan_DragDrop);
			this.label2.DragOver += new System.Windows.Forms.DragEventHandler(this.HandleDragOver);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.panel2);
			this.flowLayoutPanel1.Controls.Add(this.nodesListBox);
			this.flowLayoutPanel1.Controls.Add(this.groupBox2);
			this.flowLayoutPanel1.Controls.Add(this.panel1);
			this.flowLayoutPanel1.Controls.Add(this.predecessorsListBox);
			this.flowLayoutPanel1.Controls.Add(this.panel3);
			this.flowLayoutPanel1.Controls.Add(this.successorListBox);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(341, 512);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.button4);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(308, 31);
			this.panel2.TabIndex = 4;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(280, 5);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(21, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "-";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.RemoveNode);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(256, 5);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(22, 23);
			this.button4.TabIndex = 1;
			this.button4.Text = "+";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.AddNode);
			// 
			// nodesListBox
			// 
			this.nodesListBox.AllowDrop = true;
			this.nodesListBox.FormattingEnabled = true;
			this.nodesListBox.Location = new System.Drawing.Point(3, 40);
			this.nodesListBox.Name = "nodesListBox";
			this.nodesListBox.Size = new System.Drawing.Size(308, 95);
			this.nodesListBox.TabIndex = 0;
			this.nodesListBox.SelectedValueChanged += new System.EventHandler(this.ListBox_SelectedValueChanged);
			this.nodesListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.nodeSourceList_DragDrop);
			this.nodesListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.nodeSourceList_DragOver);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.currentItemLabel);
			this.groupBox2.Location = new System.Drawing.Point(3, 141);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(308, 41);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Current Item";
			// 
			// currentItemLabel
			// 
			this.currentItemLabel.AutoSize = true;
			this.currentItemLabel.Location = new System.Drawing.Point(3, 16);
			this.currentItemLabel.Name = "currentItemLabel";
			this.currentItemLabel.Size = new System.Drawing.Size(0, 13);
			this.currentItemLabel.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(3, 188);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(308, 31);
			this.panel1.TabIndex = 2;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(280, 5);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(21, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "-";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.RemovePredecessor);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(256, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(22, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "+";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.AddPredecessor);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Predecessors";
			// 
			// predecessorsListBox
			// 
			this.predecessorsListBox.AllowDrop = true;
			this.predecessorsListBox.FormattingEnabled = true;
			this.predecessorsListBox.Location = new System.Drawing.Point(3, 225);
			this.predecessorsListBox.Name = "predecessorsListBox";
			this.predecessorsListBox.Size = new System.Drawing.Size(308, 95);
			this.predecessorsListBox.TabIndex = 3;
			this.predecessorsListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.predecessorList_DragDrop);
			this.predecessorsListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.predecessorList_DragOver);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button5);
			this.panel3.Controls.Add(this.button6);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Location = new System.Drawing.Point(3, 326);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(308, 31);
			this.panel3.TabIndex = 5;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(280, 3);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(21, 23);
			this.button5.TabIndex = 2;
			this.button5.Text = "-";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.RemoveSuccessor);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(256, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(22, 23);
			this.button6.TabIndex = 1;
			this.button6.Text = "+";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.AddSuccessor);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Successors";
			// 
			// successorListBox
			// 
			this.successorListBox.AllowDrop = true;
			this.successorListBox.FormattingEnabled = true;
			this.successorListBox.Location = new System.Drawing.Point(3, 363);
			this.successorListBox.Name = "successorListBox";
			this.successorListBox.Size = new System.Drawing.Size(308, 95);
			this.successorListBox.TabIndex = 6;
			this.successorListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.successorList_DragDrop);
			this.successorListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.successorList_DragOver);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomInButton,
            this.ZoomOutButton,
            this.FitContentButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
			this.toolStrip1.Size = new System.Drawing.Size(580, 31);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// ZoomInButton
			// 
			this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomInButton.Image = global::Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder.Properties.Resources.plus_16;
			this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomInButton.Name = "ZoomInButton";
			this.ZoomInButton.Size = new System.Drawing.Size(23, 20);
			this.ZoomInButton.ToolTipText = "Zoom In";
			// 
			// ZoomOutButton
			// 
			this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomOutButton.Image = global::Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder.Properties.Resources.minus_16;
			this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomOutButton.Name = "ZoomOutButton";
			this.ZoomOutButton.Size = new System.Drawing.Size(23, 20);
			this.ZoomOutButton.ToolTipText = "Zoom Out";
			// 
			// FitContentButton
			// 
			this.FitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FitContentButton.Image = global::Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder.Properties.Resources.fit_16;
			this.FitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FitContentButton.Name = "FitContentButton";
			this.FitContentButton.Size = new System.Drawing.Size(23, 20);
			this.FitContentButton.ToolTipText = "Fit Content into View";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Nodes Source";
			// 
			// InteractiveNodesGraphBuilderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(836, 590);
			this.Controls.Add(this.splitContainer);
			this.Icon = global::Demo.yFiles.DataBinding.InteractiveNodesGraphBuilder.Properties.Resources.yIcon;
			this.Name = "InteractiveNodesGraphBuilderForm";
			this.Text = "InteractiveNodesGraphBuilderForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.OnLoad);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.RichTextBox description;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton ZoomInButton;
    private System.Windows.Forms.ToolStripButton ZoomOutButton;
    private System.Windows.Forms.ToolStripButton FitContentButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private yWorks.Controls.GraphControl graphControl;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.ListBox nodesListBox;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label currentItemLabel;
    private System.Windows.Forms.BindingSource bindingSource1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.ListBox predecessorsListBox;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ListBox successorListBox;
    private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
  }
}

