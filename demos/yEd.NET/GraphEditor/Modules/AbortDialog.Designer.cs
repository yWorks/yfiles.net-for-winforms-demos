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

namespace Demo.yFiles.GraphEditor.Modules.Layout
{
  partial class AbortDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbortDialog));
      this.stopButton = new System.Windows.Forms.Button();
      this.graphLoadingBar = new System.Windows.Forms.ProgressBar();
      this.label = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // stopButton
      // 
      this.stopButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stopButton.Location = new System.Drawing.Point(64, 66);
      this.stopButton.Name = "stopButton";
      this.stopButton.Size = new System.Drawing.Size(161, 26);
      this.stopButton.TabIndex = 1;
      this.stopButton.Text = "Stop";
      this.stopButton.UseVisualStyleBackColor = true;
      this.stopButton.Click += new System.EventHandler(this.OnStopClicked);
      // 
      // graphLoadingBar
      // 
      this.graphLoadingBar.Location = new System.Drawing.Point(64, 32);
      this.graphLoadingBar.Name = "graphLoadingBar";
      this.graphLoadingBar.Size = new System.Drawing.Size(161, 20);
      this.graphLoadingBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.graphLoadingBar.TabIndex = 2;
      // 
      // label
      // 
      this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label.Location = new System.Drawing.Point(0, 9);
      this.label.Name = "label";
      this.label.Size = new System.Drawing.Size(305, 20);
      this.label.TabIndex = 3;
      this.label.Text = "Layout in Progress";
      this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // AbortDialog
      // 
      this.AcceptButton = this.stopButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 104);
      this.Controls.Add(this.stopButton);
      this.Controls.Add(this.graphLoadingBar);
      this.Controls.Add(this.label);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(320, 140);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(320, 140);
      this.Name = "AbortDialog";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Layout";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button stopButton;
    private System.Windows.Forms.ProgressBar graphLoadingBar;
    private System.Windows.Forms.Label label;
  }
}