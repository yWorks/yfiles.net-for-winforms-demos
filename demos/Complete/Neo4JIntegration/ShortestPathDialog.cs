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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using INeo4jNode = Neo4j.Driver.INode;

namespace Neo4JIntegration
{
  /// <summary>
  /// Queries the target node to fetch the shortest path for a given source node.
  /// </summary>
  public partial class ShortestPathDialog : Form
  {
    public List<INode> Nodes { get; set; }
    public IEnumerable<INeo4jNode> NodeItems => Nodes.Select(n => (INeo4jNode) n.Tag);
    public INode From { get; set; }

    public int ToIndex {
      get {
        return toComboBox.SelectedIndex;
      }
      set {
        toComboBox.SelectedIndex = value;
      }
    }

    public INode To => Nodes[ToIndex];
    public INeo4jNode FromItem => (INeo4jNode) From.Tag;
    public INeo4jNode ToItem => (INeo4jNode) To.Tag;

    private GraphControl canvasControl = new GraphControl();
    private Bitmap bitmap;

    /// <summary>
    /// Creates a new instance the given <paramref name="from">source node</paramref>
    /// and the possible target <paramref name="nodes"/>.
    /// </summary>
    /// <remarks>
    /// Pressing the "Show Path" button will set the DialogResult.OK and close the dialog.
    /// It is up to the caller to fetch the ToItem and get the shortest path.
    /// </remarks>
    /// <param name="from">The source node.</param>
    /// <param name="nodes">The nodes to choose the target from.</param>
    public ShortestPathDialog(INode from, List<INode> nodes) {
      From = from;
      Nodes = nodes;

      InitializeComponent();
      toComboBox.DataSource = Nodes;

      // render the From node in the canvasControl
      var bounds = new RectD(0, 0, fromControl.Width, fromControl.Height);
      var layout = new RectD(0, 0, 240, 80);

      canvasControl.Size = new Size((int) bounds.Width, (int) bounds.Height);
      canvasControl.HorizontalScrollBarPolicy = ScrollBarVisibility.Never;
      canvasControl.VerticalScrollBarPolicy = ScrollBarVisibility.Never;
      canvasControl.Graph.CreateNode(layout, from.Style, from.Tag);
      canvasControl.ContentRect = bounds;
      canvasControl.FitContent();

      // render the canvasControl in a Bitmap
      bitmap = new Bitmap(canvasControl.Size.Width, canvasControl.Size.Height, PixelFormat.Format32bppArgb);
      using (Graphics g = Graphics.FromImage(bitmap)) {
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.Clear(Color.Transparent);
        ContextConfigurator cc = new ContextConfigurator(canvasControl.ContentRect);
        var renderContext = cc.CreateRenderContext(canvasControl, g);
        canvasControl.RenderContent(renderContext, g);
      }
      fromControl.Image = bitmap;
    }

    /// <summary>
    /// Called to render the combo box items.
    /// </summary>
    private void DrawComboBoxItem(object sender, DrawItemEventArgs e) {
      e.DrawBackground();
      e.DrawFocusRectangle();
      var node = Nodes[e.Index];
      var item = (INeo4jNode)node.Tag;
      var style = new Neo4JNodeStyle();
      var dummyNode = new SimpleNode {
          Layout = new RectD(0, 0, 240, 75), Tag = item, Style = style
      };
      var context = new RenderContext(e.Graphics, canvasControl);
      var visual = style.Renderer.GetVisualCreator(dummyNode, style).CreateVisual(context);
      ((VisualGroup) visual).Transform = new Matrix();
      ((VisualGroup) visual).Transform.Translate(e.Bounds.X, e.Bounds.Y);
      visual.Paint(context, e.Graphics);
    }

    /// <summary>
    /// Called when the "Show Path" button is clicked.
    /// </summary>
    private void ShowPath(object sender, EventArgs e) {
      DialogResult = DialogResult.OK;
    }

    private void ShortestPathDialog_FormClosed(object sender, FormClosedEventArgs e) {
      bitmap?.Dispose();
    }
  }
}
