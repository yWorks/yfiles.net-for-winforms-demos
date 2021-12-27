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
using yWorks.Geometry;

namespace Tutorial.GettingStarted
{
  partial class SampleApplication : Form
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
      this.helpStrip = new System.Windows.Forms.StatusStrip();
      this.helpLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.graphControl = new yWorks.Controls.GraphControl();
      this.helpStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // helpStrip
      // 
      this.helpStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpLabel});
      this.helpStrip.Location = new System.Drawing.Point(0, 444);
      this.helpStrip.Name = "helpStrip";
      this.helpStrip.Size = new System.Drawing.Size(624, 22);
      this.helpStrip.TabIndex = 2;
      this.helpStrip.Text = "Help";
      // 
      // helpLabel
      // 
      this.helpLabel.Name = "helpLabel";
      this.helpLabel.Size = new System.Drawing.Size(457, 17);
      this.helpLabel.Text = "Tutorial Demo 2: How to create a graph programmatically. See the sources for deta" +
          "ils.";
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(624, 444);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl";
      this.graphControl.Center = new PointD(312F, 222F);
      this.graphControl.ContentRect = new RectD(0F, 0F, 624F, 444F);
      // 
      // SampleApplication
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 466);
      this.Controls.Add(this.graphControl);
      this.Controls.Add(this.helpStrip);
      this.Name = "SampleApplication";
      this.Text = "02 Creating Graph Elements";
      this.Load += new System.EventHandler(this.OnLoaded);
      this.helpStrip.ResumeLayout(false);
      this.helpStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip helpStrip;
    private System.Windows.Forms.ToolStripStatusLabel helpLabel;
    private yWorks.Controls.GraphControl graphControl;

  }
}

