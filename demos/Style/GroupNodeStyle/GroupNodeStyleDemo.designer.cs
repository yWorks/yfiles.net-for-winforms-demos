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
using yWorks.Geometry;

namespace Demo.yFiles.Graph.GroupNodes
{
  partial class GroupNodeStyleDemo
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
            this.graphControl = new yWorks.Controls.GraphControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.description = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphControl
            // 
            this.graphControl.BackColor = System.Drawing.Color.White;
            this.graphControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphControl.DoubleClickTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.graphControl.Location = new System.Drawing.Point(0, 0);
            this.graphControl.Name = "graphControl";
            this.graphControl.Size = new System.Drawing.Size(752, 732);
            this.graphControl.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.description);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.graphControl);
            this.splitContainer2.Size = new System.Drawing.Size(1008, 732);
            this.splitContainer2.SplitterDistance = 252;
            this.splitContainer2.TabIndex = 3;
            // 
            // description
            // 
            this.description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.description.Location = new System.Drawing.Point(0, 0);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(252, 732);
            this.description.TabIndex = 0;
            this.description.Text = "";
            // 
            // GroupNodeStyleDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.splitContainer2);
            this.Icon = global::Demo.yFiles.Graph.GroupNodes.Properties.Resources.yIcon;
            this.Name = "GroupNodeStyleDemo";
            this.Text = "Group Node Styles Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
    private yWorks.Controls.GraphControl graphControl;
    private SplitContainer splitContainer2;
    private RichTextBox description;

  }
}

