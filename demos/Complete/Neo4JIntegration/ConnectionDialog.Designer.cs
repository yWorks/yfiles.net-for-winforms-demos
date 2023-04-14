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


namespace Neo4JIntegration
{
  partial class ConnectionDialog
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
      this.label1 = new System.Windows.Forms.Label();
      this.databaseUrlTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.databaseNameTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.usernameTextBox = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.passwordTextBox = new System.Windows.Forms.TextBox();
      this.ConnectButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Database URL";
      // 
      // databaseUrlTextBox
      // 
      this.databaseUrlTextBox.Location = new System.Drawing.Point(98, 6);
      this.databaseUrlTextBox.Name = "databaseUrlTextBox";
      this.databaseUrlTextBox.Size = new System.Drawing.Size(238, 20);
      this.databaseUrlTextBox.TabIndex = 1;
      this.databaseUrlTextBox.Text = "neo4j+s://demo.neo4jlabs.com";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 36);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Database Name";
      // 
      // databaseNameTextBox
      // 
      this.databaseNameTextBox.Location = new System.Drawing.Point(98, 33);
      this.databaseNameTextBox.Name = "databaseNameTextBox";
      this.databaseNameTextBox.Size = new System.Drawing.Size(238, 20);
      this.databaseNameTextBox.TabIndex = 3;
      this.databaseNameTextBox.Text = "movies";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 63);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(55, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Username";
      // 
      // usernameTextBox
      // 
      this.usernameTextBox.Location = new System.Drawing.Point(98, 60);
      this.usernameTextBox.Name = "usernameTextBox";
      this.usernameTextBox.Size = new System.Drawing.Size(238, 20);
      this.usernameTextBox.TabIndex = 5;
      this.usernameTextBox.Text = "movies";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(15, 89);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Password";
      // 
      // passwordTextBox
      // 
      this.passwordTextBox.Location = new System.Drawing.Point(98, 87);
      this.passwordTextBox.Name = "passwordTextBox";
      this.passwordTextBox.Size = new System.Drawing.Size(238, 20);
      this.passwordTextBox.TabIndex = 7;
      this.passwordTextBox.Text = "movies";
      // 
      // ConnectButton
      // 
      this.ConnectButton.Location = new System.Drawing.Point(261, 145);
      this.ConnectButton.Name = "ConnectButton";
      this.ConnectButton.Size = new System.Drawing.Size(75, 23);
      this.ConnectButton.TabIndex = 8;
      this.ConnectButton.Text = "Connect";
      this.ConnectButton.UseVisualStyleBackColor = true;
      this.ConnectButton.Click += new System.EventHandler(this.Connect);
      // 
      // ConnectionDialog
      // 
      this.AcceptButton = this.ConnectButton;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(348, 180);
      this.ControlBox = false;
      this.Controls.Add(this.ConnectButton);
      this.Controls.Add(this.passwordTextBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.usernameTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.databaseNameTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.databaseUrlTextBox);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ConnectionDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Connect to Neo4J Database";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox databaseUrlTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox databaseNameTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox usernameTextBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Button ConnectButton;
  }
}