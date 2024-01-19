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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.yFiles.Graph.ShapeNodeStyle.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.ShapeNodeStyle
{
  /// <summary>
  /// Shows the shapes that are available for <see cref="ShapeNodeStyle"/>.
  /// </summary>
  public partial class ShapeNodeStyleForm : Form
  {
    /// <summary>
    /// Initializes the demo.
    /// </summary>
    public ShapeNodeStyleForm() {
      InitializeComponent();
	    description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      InitializeStyleDefaults();
      CreateSampleNodes();
      InitializeInputMode();
    }

    private void InitializeStyleDefaults() {
      var graph = graphControl.Graph;
      
      // All node labels share the same style and label model parameter
      var labelPalette = Themes.Palette58;
      graph.NodeDefaults.Labels.Style = new DefaultLabelStyle {
          Shape = LabelShape.RoundRectangle,
          BackgroundBrush = labelPalette.NodeLabelFill,
          TextBrush = labelPalette.Text,
          StringFormat = new StringFormat {
              Alignment = StringAlignment.Center,
              LineAlignment = StringAlignment.Center
          },
          Insets = new InsetsD(4, 2, 4, 1),
          Font = new Font("Arial", 18)
      };
      graph.NodeDefaults.Labels.LayoutParameter = FreeNodeLabelModel.Instance.CreateParameter(
          new PointD(0.5, 0),
          new PointD(0, -50),
          new PointD(0.5, 0.5)
      );

      // Edges share the same style as well, they are not important in this demo
      var edgePalette = Themes.Palette56;
      graph.EdgeDefaults.Style = new PolylineEdgeStyle {
          Pen = new Pen(edgePalette.Stroke, 1.5f),
          TargetArrow = new Arrow { Type = ArrowType.Triangle, Brush = edgePalette.Stroke }
      };
    }

    /// <summary>
    /// Creates a sample graph with all the shapes that are available for <see cref="ShapeNodeStyle"/>.
    /// </summary>
    private void CreateSampleNodes() {
      // Create the various shape samples
      ShapeNodeShape[] rectangularShapes = {
          ShapeNodeShape.Rectangle, ShapeNodeShape.RoundRectangle, ShapeNodeShape.Pill
      };
      ShapeNodeShape[] ellipticalShapes = { ShapeNodeShape.Ellipse };
      ShapeNodeShape[] skewedShapes = {
          ShapeNodeShape.Diamond, ShapeNodeShape.ShearedRectangle, ShapeNodeShape.ShearedRectangle2,
          ShapeNodeShape.Trapez, ShapeNodeShape.Trapez2
      };
      ShapeNodeShape[] arrowShapes = { ShapeNodeShape.FatArrow, ShapeNodeShape.FatArrow2 };
      ShapeNodeShape[] polygonalShapes = {
          ShapeNodeShape.Triangle, ShapeNodeShape.Triangle2, ShapeNodeShape.Hexagon, ShapeNodeShape.Hexagon2,
          ShapeNodeShape.Octagon
      };
      ShapeNodeShape[] starShapes = {
          ShapeNodeShape.Star5, ShapeNodeShape.Star5Up, ShapeNodeShape.Star6, ShapeNodeShape.Star8
      };

      CreateShapeSamples(rectangularShapes, 0);
      CreateShapeSamples(ellipticalShapes.Concat(arrowShapes).ToArray(), 1);
      CreateShapeSamples(skewedShapes, 2);
      CreateShapeSamples(polygonalShapes, 3);
      CreateShapeSamples(starShapes, 4);
    }

    private void CreateShapeSamples(ShapeNodeShape[] shapes, int column) {
      var graph = graphControl.Graph;
      
      const int size1 = 45;
      const int size2 = 90;

      // Define colors for distinguishing the three different aspect ratios used below
      var fill1 = Themes.Palette54.Fill;
      var fill2 = Themes.Palette56.Fill;
      var fill3 = Themes.Palette510.Fill;
      var stroke1 = Themes.Palette54.Stroke;
      var stroke2 = Themes.Palette56.Stroke;
      var stroke3 = Themes.Palette510.Stroke;

      for (var i = 0; i < shapes.Length; i++) {
        // Create a green node with aspect ratio 1:1
        var n1 = graph.CreateNode(new RectD(column * 350 - size1 / 2, i * 200 - size1 / 2, size1, size1),
            new yWorks.Graph.Styles.ShapeNodeStyle { Shape = shapes[i], Brush = fill1, Pen = new Pen(stroke1, 1) }
        );

        // Create a blue node with aspect ratio 2:1
        var n2 = graph.CreateNode(new RectD(column * 350 + 100 - size2 / 2, i * 200 - size1 / 2, size2, size1),
            new yWorks.Graph.Styles.ShapeNodeStyle { Shape = shapes[i], Brush = fill2, Pen = new Pen(stroke2, 1) }
        );
        graph.AddLabel(n2, Enum.GetName(typeof(ShapeNodeShape), shapes[i]));

        // Create a yellow node with aspect ratio 1:2
        var n3 = graph.CreateNode(new RectD(column * 350 + 200 - size1 / 2, i * 200 - size2 / 2, size1, size2),
            new yWorks.Graph.Styles.ShapeNodeStyle { Shape = shapes[i], Brush = fill3, Pen = new Pen(stroke3, 1) }
        );

        graph.CreateEdge(n1, n2);
        graph.CreateEdge(n2, n3);
      }
    }

    /// <summary>
    /// Restricts user interaction.
    /// </summary>
    private void InitializeInputMode() {
      graphControl.InputMode = new GraphEditorInputMode {
          AllowGroupingOperations = false,
          MarqueeSelectableItems = GraphItemTypes.Node,
          AllowCreateBend = false,
          AllowCreateEdge = false,
          AllowCreateNode = false,
          AllowAddLabel = false,
          AllowEditLabel = false,
          DeletableItems = GraphItemTypes.None,
          MovableItems = GraphItemTypes.Node
      };
    }

    /// <summary>
    /// Center the sample graph in the visible area.
    /// </summary>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      // center the sample graph in the visible area
      graphControl.FitGraphBounds();
    }

    private void ToggleAspectRatio(object sender, EventArgs e) {
      var keepAspectRatio = ((ToolStripButton) sender).Checked;
      foreach (var node in graphControl.Graph.Nodes) {
        if (node.Style is yWorks.Graph.Styles.ShapeNodeStyle style) {
          style.KeepIntrinsicAspectRatio = keepAspectRatio;
        }
      }
      graphControl.Invalidate();
    }
    
	#region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ShapeNodeStyleForm());
    }

    #endregion

  }
}
