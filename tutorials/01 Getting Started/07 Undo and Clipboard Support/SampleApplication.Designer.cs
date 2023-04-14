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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleApplication));
      this.graphControl = new yWorks.Controls.GraphControl();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.helpStrip = new System.Windows.Forms.StatusStrip();
      this.helpLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.zoomInButton = new System.Windows.Forms.ToolStripButton();
      this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
      this.fitContentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.helpStrip.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // graphControl
      // 
      this.graphControl.BackColor = System.Drawing.Color.White;
      this.graphControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.graphControl.Location = new System.Drawing.Point(0, 0);
      this.graphControl.Name = "graphControl";
      this.graphControl.Size = new System.Drawing.Size(624, 453);
      this.graphControl.TabIndex = 0;
      this.graphControl.Text = "graphControl";
      this.graphControl.Center = new PointD(312F, 226.5F);
      this.graphControl.ContentRect = new RectD(0F, 0F, 624F, 453F);
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.BottomToolStripPanel
      // 
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.helpStrip);
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.AutoScroll = true;
      this.toolStripContainer1.ContentPanel.Controls.Add(this.graphControl);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(624, 453);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(624, 524);
      this.toolStripContainer1.TabIndex = 1;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // helpStrip
      // 
      this.helpStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.helpStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpLabel});
      this.helpStrip.Location = new System.Drawing.Point(0, 0);
      this.helpStrip.Name = "helpStrip";
      this.helpStrip.Size = new System.Drawing.Size(624, 22);
      this.helpStrip.TabIndex = 3;
      this.helpStrip.Text = "Help";
      // 
      // helpLabel
      // 
      this.helpLabel.Name = "helpLabel";
      this.helpLabel.Size = new System.Drawing.Size(465, 17);
      this.helpLabel.Text = "Tutorial Demo 7: How to activate clipboard and undoability. See the sources for d" +
          "etails.";
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(624, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
      this.editToolStripMenuItem.Text = "&Edit";
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.cutToolStripMenuItem.Text = "Cu&t";
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.copyToolStripMenuItem.Text = "&Copy";
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.pasteToolStripMenuItem.Text = "&Paste";
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.deleteToolStripMenuItem.Text = "&Delete";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
      // 
      // undoToolStripMenuItem
      // 
      this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
      this.undoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.undoToolStripMenuItem.Text = "&Undo";
      // 
      // redoToolStripMenuItem
      // 
      this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
      this.redoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.redoToolStripMenuItem.Text = "&Redo";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitContentButton});
      this.toolStrip1.Location = new System.Drawing.Point(3, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(204, 25);
      this.toolStrip1.TabIndex = 0;
      // 
      // zoomInButton
      // 
      this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
      this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomInButton.Name = "zoomInButton";
      this.zoomInButton.Size = new System.Drawing.Size(56, 22);
      this.zoomInButton.Text = "Zoom In";
      // 
      // zoomOutButton
      // 
      this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
      this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.zoomOutButton.Name = "zoomOutButton";
      this.zoomOutButton.Size = new System.Drawing.Size(66, 22);
      this.zoomOutButton.Text = "Zoom Out";
      // 
      // fitContentButton
      // 
      this.fitContentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.fitContentButton.Image = ((System.Drawing.Image)(resources.GetObject("fitContentButton.Image")));
      this.fitContentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fitContentButton.Name = "fitContentButton";
      this.fitContentButton.Size = new System.Drawing.Size(70, 22);
      this.fitContentButton.Text = "Fit Content";
      // 
      // SampleApplication
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 524);
      this.Controls.Add(this.toolStripContainer1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "SampleApplication";
      this.Text = "07 Undo and Clipboard Support";
      this.Load += new System.EventHandler(this.OnLoaded);
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.helpStrip.ResumeLayout(false);
      this.helpStrip.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private yWorks.Controls.GraphControl graphControl;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton zoomInButton;
    private System.Windows.Forms.ToolStripButton zoomOutButton;
    private System.Windows.Forms.ToolStripButton fitContentButton;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.StatusStrip helpStrip;
    private System.Windows.Forms.ToolStripStatusLabel helpLabel;
  }
}
