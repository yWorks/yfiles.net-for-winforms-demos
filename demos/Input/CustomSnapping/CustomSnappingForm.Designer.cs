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

using System.Windows.Forms;
using yWorks.Geometry;

namespace Demo.yFiles.Graph.Input.CustomSnapping
{
  partial class CustomSnappingForm : Form
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
      this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.description = new System.Windows.Forms.RichTextBox();
      this.splitContainerHorizontal.Panel1.SuspendLayout();
      this.splitContainerHorizontal.Panel2.SuspendLayout();
      this.splitContainerHorizontal.SuspendLayout();
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
      this.splitContainerHorizontal.Panel2.Controls.Add(this.graphControl);
      this.splitContainerHorizontal.Size = new System.Drawing.Size(1008, 732);
      this.splitContainerHorizontal.SplitterDistance = 252;
      this.splitContainerHorizontal.TabIndex = 0;
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
      this.graphControl.Size = new System.Drawing.Size(752, 732);
      this.graphControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      this.graphControl.TabIndex = 2;
      this.graphControl.Text = "graphControl";
      viewportLimiter1.Bounds = null;
      this.graphControl.ViewportLimiter = viewportLimiter1;
      // 
      // description
      // 
      this.description.BackColor = System.Drawing.SystemColors.Window;
      this.description.Dock = System.Windows.Forms.DockStyle.Fill;
      this.description.Location = new System.Drawing.Point(0, 0);
      this.description.Name = "description";
      this.description.ReadOnly = true;
      this.description.Size = new System.Drawing.Size(252, 732);
      this.description.TabIndex = 1;
      this.description.Text = "";
      // 
      // CustomSnappingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1008, 732);
      this.Controls.Add(this.splitContainerHorizontal);
      this.Icon = global::Demo.yFiles.Graph.Input.CustomSnapping.Properties.Resources.yIcon;
      this.Name = "CustomSnappingForm";
      this.Text = "Custom Snapping Demo";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.splitContainerHorizontal.Panel1.ResumeLayout(false);
      this.splitContainerHorizontal.Panel2.ResumeLayout(false);
      this.splitContainerHorizontal.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainerHorizontal;
    private RichTextBox description;
    private yWorks.Controls.GraphControl graphControl;
  }
}
