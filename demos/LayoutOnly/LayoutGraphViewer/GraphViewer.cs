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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using yWorks.Layout;

namespace Demo.yWorks.LayoutGraphViewer

{
  /// <summary>
  /// Summary description for GraphViewer.
  /// </summary>
  public class GraphViewer : Form

  {
    private TabControl tabControl;

    private ToolStripMenuItem fileMenu;

    private ToolStripMenuItem exitItem;

    private ToolStrip toolBar1;

    private ToolStripButton zoomIn;

    private ToolStripButton zoomOut;

    private ToolStripButton zoomFit;

    private MenuStrip mainMenu;

    private ImageList imageList;

    private ToolStripMenuItem menuItem1;

    private IContainer components;


    public GraphViewer() : this(null, null) {}


    public GraphViewer(LayoutGraph graph) : this(graph, "") {}


    public GraphViewer(LayoutGraph graph, String title) {
      this.Text = "yFiles LayoutGraph Viewer";

      this.AutoScroll = false;

      InitializeComponent();


      if (graph != null) {
        AddLayoutGraph(graph, title);
      }
    }


    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing) {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }

      base.Dispose(disposing);
    }


    private void InitializeComponent() {
      this.components = new Container();

      ResourceManager resources = new ResourceManager(typeof (GraphViewer));

      this.tabControl = new TabControl();
      this.mainMenu = new MenuStrip();
      this.fileMenu = new ToolStripMenuItem();
      this.menuItem1 = new ToolStripMenuItem();
      this.exitItem = new ToolStripMenuItem();
      this.toolBar1 = new MenuStrip();
      this.zoomIn = new ToolStripButton();
      this.zoomOut = new ToolStripButton();
      this.zoomFit = new ToolStripButton();
      this.imageList = new ImageList(this.components);
      this.SuspendLayout();
      // 
      // tabControl
      // 
      this.tabControl.Dock = DockStyle.Fill;
      this.tabControl.Location = new Point(0, 36);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new Size(392, 282);
      this.tabControl.TabIndex = 4;
      // 
      // mainMenu
      // 
      this.mainMenu.Items.Add(this.fileMenu);

      // 
      // fileMenu
      // 
      this.fileMenu.DropDownItems.AddRange(new []
                                         {
                                           this.menuItem1,
                                           this.exitItem
                                         });

      this.fileMenu.Text = "File";

      // 
      // menuItem1
      // 
      this.menuItem1.Text = "Save as EMF...";
      this.menuItem1.Click += new EventHandler(this.saveButton_Click);

      // 
      // exitItem
      // 
      this.exitItem.Text = "Exit";
      this.exitItem.Click += new EventHandler(this.exit_Click);

      // 
      // toolBar1
      // 
      this.toolBar1.Items.AddRange(new []
                                       {
                                         this.zoomIn,
                                         this.zoomOut,
                                         this.zoomFit
                                       });

      this.toolBar1.ImageList = this.imageList;
      this.toolBar1.Location = new Point(0, 0);
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.ShowItemToolTips = true;

      this.toolBar1.Size = new Size(392, 36);

      this.toolBar1.TabIndex = 5;

      // 
      // zoomIn
      // 
      this.zoomIn.ImageIndex = 0;
      this.zoomIn.ToolTipText = "Zoom In";
      this.zoomIn.Click += delegate {
        GetCurrentLayoutGraphPanel().Zoom *= 1.2F;
      };

      // 
      // zoomOut
      // 

      this.zoomOut.ImageIndex = 2;
      this.zoomOut.ToolTipText = "Zoom Out";
      this.zoomOut.Click += delegate {
        GetCurrentLayoutGraphPanel().Zoom /= 1.2F;
      };

      // 
      // zoomFit
      // 

      this.zoomFit.ImageIndex = 1;
      this.zoomFit.ToolTipText = "Zoom to 100%";
      this.zoomFit.Click += delegate {
        GetCurrentLayoutGraphPanel().Zoom = 1.0F;
      };

      // 
      // imageList
      // 

      this.imageList.ImageSize = new Size(24, 24);
      this.imageList.ImageStream = ((ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = Color.Transparent;

      // 
      // GraphViewer
      // 

      this.AutoScaleBaseSize = new Size(5, 13);

      this.ClientSize = new Size(392, 318);

      this.Controls.Add(this.tabControl);

      this.Controls.Add(this.toolBar1);

      this.MainMenuStrip = this.mainMenu;

      this.Name = "GraphViewer";

      this.ResumeLayout(false);
    }


    public void AddLayoutGraph(LayoutGraph graph, String title) {
      LayoutGraphPanel panel = new LayoutGraphPanel(graph);

      panel.BorderStyle = BorderStyle.None;

      TabPage tp = new TabPage(title);

      tp.Controls.Add(panel);

      panel.Dock = DockStyle.Fill;

      this.tabControl.Controls.Add(tp);
    }


    private LayoutGraphPanel GetCurrentLayoutGraphPanel() {
      return (LayoutGraphPanel) this.tabControl.SelectedTab.Controls[0];
    }

    private void saveButton_Click(object sender, EventArgs e) {
      SaveFileDialog saveFileDialog = new SaveFileDialog();

      saveFileDialog.Filter = "EMF File|*.emf";

      saveFileDialog.Title = "Save an EMF File";

      saveFileDialog.ShowDialog();


      // If the file name is not an empty string open it for saving.

      if (saveFileDialog.FileName != "") {
        GetCurrentLayoutGraphPanel().ExportToEmf(saveFileDialog.FileName);
      }
    }


    private void exit_Click(object sender, EventArgs e) {
      Close();
    }
  }
}
