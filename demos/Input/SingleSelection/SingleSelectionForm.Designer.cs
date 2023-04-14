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

using yWorks.Geometry;

namespace Demo.yFiles.Graph.Input.SingleSelection
{
  partial class SingleSelectionForm
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
      yWorks.Controls.ViewportLimiter viewportLimiter1 = new yWorks.Controls.ViewportLimiter();
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.description = new System.Windows.Forms.RichTextBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.singleSelectionButton = new System.Windows.Forms.ToolStripButton();
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
      this.splitContainer.Size = new System.Drawing.Size(659, 447);
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
      this.description.Size = new System.Drawing.Size(252, 447);
      this.description.TabIndex = 0;
      this.description.Text = "";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleSelectionButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(4);
      this.toolStrip1.Size = new System.Drawing.Size(403, 31);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // singleSelectionButton
      // 
      this.singleSelectionButton.Checked = true;
      this.singleSelectionButton.CheckOnClick = true;
      this.singleSelectionButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.singleSelectionButton.Image = global::Demo.yFiles.Graph.Input.SingleSelection.Properties.Resources.settings_16;
      this.singleSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.singleSelectionButton.Name = "singleSelectionButton";
      this.singleSelectionButton.Size = new System.Drawing.Size(144, 20);
      this.singleSelectionButton.Text = "Single Selection Mode";
      this.singleSelectionButton.ToolTipText = "Toggle Single Selection Mode";
      this.singleSelectionButton.CheckedChanged += new System.EventHandler(this.ToggleSingleSelection_Click);
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
      this.graphControl.Size = new System.Drawing.Size(403, 447);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // SingleSelectionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(659, 447);
      this.Controls.Add(this.splitContainer);
      this.Icon = global::Demo.yFiles.Graph.Input.SingleSelection.Properties.Resources.yIcon;
      this.Name = "SingleSelectionForm";
      this.Text = "Single Selection Demo";
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
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton singleSelectionButton;
  }
}
