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

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using yWorks.Algorithms;
using yWorks.Algorithms.Geometry;
using yWorks.Layout;
using yWorks.Layout.Grouping;
using Rectangle = System.Drawing.Rectangle;

namespace Demo.yWorks.LayoutGraphViewer
{
  public class LayoutGraphPanel : Panel
  {
    private static readonly Brush nodeFill = new SolidBrush(Color.FromArgb(255, 108, 0));
    private static readonly Brush nodeStroke = new SolidBrush(Color.FromArgb(102, 43, 0));
    private static readonly Brush groupNodeStroke = new SolidBrush(Color.FromArgb(4, 45, 55));
    private static readonly Brush labelFill = new SolidBrush(Color.FromArgb(255, 195, 152));
    private static readonly Pen edgePen = new Pen(nodeStroke, 2);
    private static readonly Pen nodePen = new Pen(nodeStroke, 2);
    private static readonly Pen groupNodePen = new Pen(groupNodeStroke, 2);
    private static readonly Font labelFont = new Font("sansserif", 6);

    private LayoutGraph layoutGraph;

    private GroupingSupport grouping;

    private Brush nodeFillBrush;

    private Rectangle worldRect = new Rectangle(0, 0, 500, 500);
    private int insets = 20;
    private float zoom = 1.0f;

    public Rectangle WorldRect {
      get { return this.worldRect; }

      set {
        this.worldRect = value;
        this.AutoScrollMinSize = new Size((int) (value.Width*zoom), (int) (value.Height*zoom));
        this.Invalidate();
      }
    }

    public float Zoom {
      get { return this.zoom; }

      set {
        float newValue = Math.Min(1000f, Math.Max(0.01f, value));
        if (newValue != this.Zoom) {
          this.zoom = newValue;
          this.AutoScrollMinSize = new Size((int) (this.worldRect.Width*zoom), (int) (this.worldRect.Height*zoom));
          this.Invalidate();
        }
      }
    }

    public LayoutGraphPanel(LayoutGraph graph) {
      if (!GroupingSupport.IsFlat(graph)) {
        grouping = new GroupingSupport(graph);
      }

      this.BackColor = Color.White;
      this.BorderStyle = BorderStyle.Fixed3D;
      this.AutoScroll = true;
      global::yWorks.Algorithms.Geometry.Rectangle2D rect = LayoutGraphUtilities.GetBoundingBox(graph, graph.GetNodeCursor(), graph.GetEdgeCursor());
      this.HScroll = true;
      this.VScroll = true;
      this.layoutGraph = graph;

      this.nodeFillBrush = nodeFill;

      this.MouseDown += new MouseEventHandler(LayoutGraphPanel_MouseDown);
      SetWorldRect(rect.X - insets, rect.Y - insets, rect.Width + insets*2, rect.Height + insets*2);
    }

    public void SetWorldRect(double x, double y, double w, double h) {
      this.worldRect.X = (int) x;
      this.worldRect.Y = (int) y;
      this.worldRect.Width = (int) w;
      this.worldRect.Height = (int) h;
      this.AutoScrollMinSize = new Size((int) (w*zoom), (int) (h*zoom));
    }

    protected override void OnPaint(PaintEventArgs e) {
      base.OnPaint(e);
      Graphics g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      g.TranslateTransform(this.AutoScrollPosition.X/zoom - this.worldRect.X,
                           this.AutoScrollPosition.Y/zoom - this.worldRect.Y, MatrixOrder.Append);
      g.ScaleTransform(this.zoom, this.zoom, MatrixOrder.Append);
      PaintGraph(g);
    }

    public void PaintGraph(Graphics g) {
      if (this.grouping != null) {
        for (ListCell cell = this.grouping.GetChildren(this.grouping.Root).FirstCell; cell != null; cell = cell.Succ()) {
          Node node = (Node) cell.Info;
          PaintNodeAndChildren(g, layoutGraph, node);
        }
      } else {
        foreach (Node node in layoutGraph.Nodes) {
          PaintNode(g, layoutGraph, node);
          INodeLabelLayout[] labels = layoutGraph.GetLabelLayout(node);
          if (labels != null && labels.Length > 0) {
            foreach (INodeLabelLayout label in labels) {
              PaintNodeLabel(g, layoutGraph, node, label);
            }
          }
        }
      }

      foreach (Edge edge in layoutGraph.Edges) {
        PaintEdge(g, layoutGraph, edge);
        IEdgeLabelLayout[] labels = layoutGraph.GetLabelLayout(edge);
        if (labels != null && labels.Length > 0) {
          foreach (IEdgeLabelLayout label in labels) {
            PaintEdgeLabel(g, layoutGraph, edge, label);
          }
        }
      }
    }

    private void PaintNodeAndChildren(Graphics g, LayoutGraph graph, Node node) {
      PaintNode(g, graph, node);
      INodeLabelLayout[] labels = layoutGraph.GetLabelLayout(node);
      if (labels != null && labels.Length > 0) {
        foreach (INodeLabelLayout label in labels) {
          PaintNodeLabel(g, layoutGraph, node, label);
        }
      }
      if (grouping.IsGroupNode(node)) {
        for (ListCell cell = this.grouping.GetChildren(node).FirstCell; cell != null; cell = cell.Succ()) {
          Node child = (Node) cell.Info;
          PaintNodeAndChildren(g, layoutGraph, child);
        }
      }
    }

    protected void PaintNodeLabel(Graphics g, LayoutGraph graph, Node node, INodeLabelLayout label) {
      YOrientedRectangle pos = GetNodeLabelLocation(graph, node, label);
      PaintOrientedRect(pos, g);
    }

    private void PaintOrientedRect(YOrientedRectangle pos, Graphics g) {
      points[0] = new PointF((float) pos.AnchorX, (float) pos.AnchorY);
      points[1] = new PointF((float)(pos.AnchorX + pos.Height * pos.UpX), (float)(pos.AnchorY + pos.Height * pos.UpY));
      points[2] = new PointF((float)(pos.AnchorX + pos.Height * pos.UpX + pos.Width * -pos.UpY), (float)(pos.AnchorY + pos.Height * pos.UpY + pos.Width * pos.UpX));
      points[3] = new PointF((float)(pos.AnchorX + pos.Width * -pos.UpY), (float)(pos.AnchorY + pos.Width * pos.UpX));
      g.FillPolygon(labelFill, points);
    }

    protected void PaintEdgeLabel(Graphics g, LayoutGraph graph, Edge edge, IEdgeLabelLayout label) {
      YOrientedRectangle pos = GetEdgeLabelLocation(graph, edge, label);
      PaintOrientedRect(pos, g);
    }

    protected void PaintEdge(Graphics g, LayoutGraph graph, Edge e) {
      IEdgeLayout el = graph.GetLayout(e);
      YPoint sp = graph.GetSourcePointAbs(e);
      YPoint tp = graph.GetTargetPointAbs(e);
      PointF[] points = new PointF[el.PointCount() + 2];
      points[0] = new PointF((float) sp.X, (float) sp.Y);
      points[el.PointCount() + 1] = new PointF((float) tp.X, (float) tp.Y);
      for (int i = 0; i < el.PointCount(); i++) {
        YPoint p = el.GetPoint(i);
        points[i + 1] = new PointF((float) p.X, (float) p.Y);
      }
      g.DrawLines(edgePen, points);
    }

    private PointF[] points = new PointF[4];

    protected void PaintNode(Graphics g, LayoutGraph graph, Node node) {
      var isGroupNode = grouping != null && grouping.IsGroupNode(node);

      INodeLayout nl = graph.GetLayout(node);
      RectangleF rect = RectangleF.FromLTRB(
        (float) nl.X, (float) nl.Y, (float) (nl.X + nl.Width), (float) (nl.Y + nl.Height));

      if (!isGroupNode) {
        g.FillRectangle(nodeFillBrush, rect);
      }

      g.DrawRectangle(isGroupNode ? groupNodePen : nodePen, rect.X, rect.Y, rect.Width, rect.Height);

      string text = node.Index.ToString();
      SizeF size = g.MeasureString(text, labelFont);
      var textX = rect.X + rect.Width*0.5f - size.Width*0.5f;
      var textY = rect.Y + rect.Height*0.5f - size.Height*0.5f;
      g.DrawString(text, labelFont, isGroupNode ? groupNodeStroke : nodeStroke, textX, textY);
    }

    public void ExportToEmf(string emfFile) {
      // Create a Bitmap
      Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format24bppRgb);
      // Wrap a Graphics around the Bitmap
      Graphics gRef = Graphics.FromImage(bitmap);
      // Get an HDC from the Graphics
      IntPtr hDC = gRef.GetHdc();
      // Create a recordable Metafile
      Metafile mf = new Metafile(emfFile, hDC);
      // Release the HDC
      gRef.ReleaseHdc(hDC);
      // Wrap a Graphics around the Metafile so we can draw on it
      Graphics g = Graphics.FromImage(mf);
      // Draw on our Metafile
      PaintGraph(g);
      g.Flush();
      mf.Dispose();
      g.Dispose();
      gRef.Dispose();
      bitmap.Dispose();
    }

    private static YOrientedRectangle GetEdgeLabelLocation(LayoutGraph graph, Edge e, IEdgeLabelLayout ell) {
      YOrientedRectangle ellp = ell.LabelModel.GetLabelPlacement(
        ell.BoundingBox,
        graph.GetLayout(e),
        graph.GetLayout(e.Source),
        graph.GetLayout(e.Target),
        ell.ModelParameter);
      return ellp;
    }

    private static YOrientedRectangle GetNodeLabelLocation(LayoutGraph graph, Node n, INodeLabelLayout nll) {
      return nll.LabelModel.GetLabelPlacement(
        nll.BoundingBox,
        graph.GetLayout(n),
        nll.ModelParameter);
    }

    protected override void OnMouseWheel(MouseEventArgs e) {
      int numberOfTextLinesToMove = e.Delta*SystemInformation.MouseWheelScrollLines/120;
      if (numberOfTextLinesToMove != 0) {
        if (numberOfTextLinesToMove > 0) {
          for (int i = 0; i < numberOfTextLinesToMove; i++) {
            this.Zoom *= 1.2F;
          }
        } else {
          for (int i = 0; i > numberOfTextLinesToMove; i--) {
            this.Zoom /= 1.2F;
          }
        }
      }
    }

    private void LayoutGraphPanel_MouseDown(object sender, MouseEventArgs e) {
      this.Focus();
    }
  }
}
