/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
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

    private MenuItem fileMenu;

    private MenuItem exitItem;

    private ToolBar toolBar1;

    private ToolBarButton zoomIn;

    private ToolBarButton zoomOut;

    private ToolBarButton zoomFit;

    private MainMenu mainMenu;

    private ImageList imageList;

    private MenuItem menuItem1;

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
      this.mainMenu = new MainMenu();
      this.fileMenu = new MenuItem();
      this.menuItem1 = new MenuItem();
      this.exitItem = new MenuItem();
      this.toolBar1 = new ToolBar();
      this.zoomIn = new ToolBarButton();
      this.zoomOut = new ToolBarButton();
      this.zoomFit = new ToolBarButton();
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
      this.mainMenu.MenuItems.AddRange(new MenuItem[]
                                         {
                                           this.fileMenu
                                         });

      // 

      // fileMenu

      // 

      this.fileMenu.Index = 0;

      this.fileMenu.MenuItems.AddRange(new MenuItem[]
                                         {
                                           this.menuItem1,
                                           this.exitItem
                                         });

      this.fileMenu.Text = "File";

      // 

      // menuItem1

      // 

      this.menuItem1.Index = 0;

      this.menuItem1.Shortcut = Shortcut.CtrlS;

      this.menuItem1.Text = "Save as EMF...";

      this.menuItem1.Click += new EventHandler(this.saveButton_Click);

      // 

      // exitItem

      // 

      this.exitItem.Index = 1;

      this.exitItem.Shortcut = Shortcut.CtrlX;

      this.exitItem.Text = "Exit";

      this.exitItem.Click += new EventHandler(this.exit_Click);

      // 

      // toolBar1

      // 

      this.toolBar1.Appearance = ToolBarAppearance.Flat;

      this.toolBar1.Buttons.AddRange(new ToolBarButton[]
                                       {
                                         this.zoomIn,
                                         this.zoomOut,
                                         this.zoomFit
                                       });

      this.toolBar1.DropDownArrows = true;

      this.toolBar1.ImageList = this.imageList;

      this.toolBar1.Location = new Point(0, 0);

      this.toolBar1.Name = "toolBar1";

      this.toolBar1.ShowToolTips = true;

      this.toolBar1.Size = new Size(392, 36);

      this.toolBar1.TabIndex = 5;

      this.toolBar1.ButtonClick += new ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);

      // 

      // zoomIn

      // 

      this.zoomIn.ImageIndex = 0;

      this.zoomIn.ToolTipText = "Zoom In";

      // 

      // zoomOut

      // 

      this.zoomOut.ImageIndex = 2;

      this.zoomOut.ToolTipText = "Zoom Out";

      // 

      // zoomFit

      // 

      this.zoomFit.ImageIndex = 1;

      this.zoomFit.ToolTipText = "Zoom to 100%";

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

      this.Menu = this.mainMenu;

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


    private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
      if (e.Button == this.zoomFit) {
        GetCurrentLayoutGraphPanel().Zoom = 1.0F;
      } else if (e.Button == this.zoomFit) {
        GetCurrentLayoutGraphPanel().Zoom = 1.0F;
      } else if (e.Button == this.zoomIn) {
        GetCurrentLayoutGraphPanel().Zoom *= 1.2F;
      } else if (e.Button == this.zoomOut) {
        GetCurrentLayoutGraphPanel().Zoom /= 1.2F;
      }
    }
  }
}