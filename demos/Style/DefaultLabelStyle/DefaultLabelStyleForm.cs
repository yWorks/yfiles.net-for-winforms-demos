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
using System.Windows.Forms;
using Demo.yFiles.Graph.DefaultLabels.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.DefaultLabels
{
  /// <summary>
  /// Shows the background shapes that are available for <see cref="DefaultLabelStyle"/>.
  /// </summary>
  public partial class DefaultLabelStyleForm : Form
  {
    /// <summary>
    /// Initializes the demo.
    /// </summary>
    public DefaultLabelStyleForm() {
      InitializeComponent();
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitContent, graphControl);

      InitializeInteraction();

      // Create node and edge labels using different label style settings
      CreateSampleNodeLabels(graphControl.Graph);
      CreateSampleEdgeLabels(graphControl.Graph);
    }

    /// <summary>
    /// Creates some sample node labels with different background styles.
    /// </summary>
    /// <param name="graph">The graph to add node labels to.</param>
    private static void CreateSampleNodeLabels(IGraph graph) {
      var n1 = graph.CreateNode(new RectD(-25, -100, 50, 200), CreateNodeStyle(Themes.PaletteOrange));
      // Add sample node labels to the first node, distributed evenly on the side and with different
      // background shapes
      graph.AddLabel(
          n1,
          "Rectangle Node Label",
          CreateNodeLabelParameter(new PointD(1, 0.2), new PointD(100, 0)),
          CreateNodeLabelStyle(shape : LabelShape.Rectangle)
      );
      graph.AddLabel(
          n1,
          "Rounded Node Label",
          CreateNodeLabelParameter(new PointD(1, 0.4), new PointD(100, 0)),
          CreateNodeLabelStyle(shape : LabelShape.RoundRectangle, insets : 2)
      );
      graph.AddLabel(
          n1,
          "Hexagon Node Label",
          CreateNodeLabelParameter(new PointD(1, 0.6), new PointD(100, 0)),
          //The hexagon background needs slightly larger insets at the sides
          CreateNodeLabelStyle(shape : LabelShape.Hexagon)
      );
      graph.AddLabel(
          n1,
          "Pill Node Label",
          CreateNodeLabelParameter(new PointD(1, 0.8), new PointD(100, 0)),
          CreateNodeLabelStyle(shape : LabelShape.Pill)
      );

      // Create two more nodes, the bottom one and the right one
      var n2 = graph.CreateNode(new RectD(275, 600, 50, 50));
      graph.SetStyle(n2, CreateNodeStyle(Themes.Palette14));
      var n3 = graph.CreateNode(new RectD(525, -100, 50, 200));
      graph.SetStyle(n3, CreateNodeStyle(Themes.Palette12));

      // Add three node labels to the right node showing different text clipping and text wrapping options
      graph.AddLabel(
          n3,
          "Wrapped and clipped label text",
          CreateNodeLabelParameter(new PointD(1, 0.2), new PointD(120, 0)),
          CreateNodeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.Pill,
              wrapText : true,
              trimming : StringTrimming.EllipsisWord,
              clipText : true
          ),
          new SizeD(140, 25)
      );

      graph.AddLabel(
          n3,
          "Un-wrapped but clipped label text",
          CreateNodeLabelParameter(new PointD(1, 0.5), new PointD(120, 0)),
          CreateNodeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.Pill,
              trimming : StringTrimming.None,
              clipText : true),
          new SizeD(140, 25)
      );

      // For the last label, disable text clipping
      graph.AddLabel(
          n3,
          "Un-wrapped and un-clipped label text",
          CreateNodeLabelParameter(new PointD(1, 0.8), new PointD(120, 0)),
          CreateNodeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.Pill,
              trimming : StringTrimming.None,
              clipText : false
          ),
          new SizeD(140, 25)
      );
    }

    /// <summary>
    /// Creates some sample edge labels with different background styles.
    /// </summary>
    /// <param name="graph">The graph to add edge labels to.</param>
    private static void CreateSampleEdgeLabels(IGraph graph) {
      var edgeLabelModel = new SmartEdgeLabelModel { Angle = Math.PI / 2 };

      graph.EdgeDefaults.Labels.LayoutParameter = edgeLabelModel.CreateDefaultParameter();
      graph.EdgeDefaults.Style = CreateEdgeStyle(Themes.Palette12);

      var edge1 = graph.CreateEdge(graph.Nodes[0], graph.Nodes[1]);
      graph.AddBend(edge1, new PointD(0, 400));

      //Add sample edge labels on the first edge segment, distributed evenly on the path and with different
      //background shapes
      graph.AddLabel(
          edge1,
          "Rectangle Edge Label\n" + "A second line of sample text.",
          edgeLabelModel.CreateParameterFromSource(0, 0, 0.2),
          CreateEdgeLabelStyle(shape : LabelShape.Rectangle)
      );
      graph.AddLabel(
          edge1,
          "Rounded Edge Label\n" + "A second line of sample text.",
          edgeLabelModel.CreateParameterFromSource(0, 0, 0.4),
          // For the round rectangle, we can manually increase the padding around the text
          // using the insets property. By default, this would be just as tight as for
          // LabelShape.RECTANGLE, but in order to make sure that text is less likely to touch
          // the stroke of the round rectangle, we add 2 extra pixels.
          CreateEdgeLabelStyle(shape : LabelShape.RoundRectangle, insets : 2)
      );
      graph.AddLabel(
          edge1,
          "Hexagon Edge Label\n" + "A second line of sample text.",
          edgeLabelModel.CreateParameterFromSource(0, 0, 0.6),
          CreateEdgeLabelStyle(shape : LabelShape.Hexagon)
      );
      graph.AddLabel(
          edge1,
          "Pill Edge Label\n" + "A second line of sample text.",
          edgeLabelModel.CreateParameterFromSource(0, 0, 0.8),
          CreateEdgeLabelStyle(shape : LabelShape.Pill)
      );

      //Add rotated edge labels on the second edge segment, distributed evenly and with different
      //background shapes
      graph.AddLabel(
          edge1,
          "Rotated Rectangle",
          edgeLabelModel.CreateParameterFromSource(1, 0, 0.2),
          CreateEdgeLabelStyle(
              palette : Themes.Palette15,
              shape : LabelShape.Rectangle,
              font: new Font("Monospace", 12)
          )
      );
      graph.AddLabel(
          edge1,
          "Rotated Rounded Rectangle",
          edgeLabelModel.CreateParameterFromSource(1, 0, 0.4),
          CreateEdgeLabelStyle(
              palette : Themes.Palette15,
              shape : LabelShape.RoundRectangle,
              // For the round rectangle, we can manually increase the padding around the text
              // using the insets property. By default, this would be just as tight as for
              // LabelShape.RECTANGLE, but in order to make sure that text is less likely to touch
              // the stroke of the round rectangle, we add 2 extra pixels.
              insets : 2,
              font: new Font("Monospace", 12)
          )
      );
      graph.AddLabel(
          edge1,
          "Rotated Hexagon",
          edgeLabelModel.CreateParameterFromSource(1, 0, 0.6),
          CreateEdgeLabelStyle(
              palette : Themes.Palette15,
              shape : LabelShape.Hexagon,
              font: new Font("Monospace", 12)
          )
      );
      graph.AddLabel(
          edge1,
          "Rotated Pill",
          edgeLabelModel.CreateParameterFromSource(1, 0, 0.8),
          CreateEdgeLabelStyle(
              palette : Themes.Palette15,
              shape : LabelShape.Pill,
              font: new Font("Monospace", 12)
          )
      );

      var edge2 = graph.CreateEdge(graph.Nodes[2], graph.Nodes[1]);
      graph.AddBend(edge2, new PointD(550, 625));

      // Add larger edge labels with different vertical and horizontal text alignment settings to the second edge
      graph.AddLabel(
          edge2,
          "Edge Label\nwith vertical text\nalignment at bottom",
          edgeLabelModel.CreateParameterFromSource(0, -20, 0.4),
          CreateEdgeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.RoundRectangle,
              insets : 2,
              font: new Font(new FontFamily("Arial"), 10.5f, FontStyle.Bold),
              verticalAlignment : StringAlignment.Far
          ),
          // Explicitly specify a preferred size for the label that is much larger than needed for the label's text
          new SizeD(150, 120)
      );
      graph.AddLabel(
          edge2,
          "Edge Label\nwith vertical text\nalignment at top",
          edgeLabelModel.CreateParameterFromSource(0, 20, 0.4),
          CreateEdgeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.RoundRectangle,
              insets : 2,
              font: new Font(new FontFamily("Arial"), 10.5f, FontStyle.Bold),
              verticalAlignment : StringAlignment.Near
          ),
          // Explicitly specify a preferred size for the label that is much larger than needed for the label's text
          new SizeD(150, 120)
      );
      graph.AddLabel(
          edge2,
          "Edge Label\nwith vertical center\nand horizontal left\ntext alignment",
          edgeLabelModel.CreateParameterFromSource(0, 20, 0.7),
          CreateEdgeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.RoundRectangle,
              insets : 2,
              font: new Font(new FontFamily("Arial"), 10.5f, FontStyle.Bold),
              verticalAlignment : StringAlignment.Center,
              textAlignment : StringAlignment.Near
          ),
          // Explicitly specify a preferred size for the label that is much larger than needed for the label's text
          new SizeD(150, 120)
      );
      graph.AddLabel(
          edge2,
          "Edge Label\nwith vertical bottom\nand horizontal right\ntext alignment",
          edgeLabelModel.CreateParameterFromSource(0, -20, 0.7),
          CreateEdgeLabelStyle(
              palette : Themes.Palette12,
              shape : LabelShape.RoundRectangle,
              insets : 2,
              font: new Font(new FontFamily("Arial"), 10.5f, FontStyle.Bold),
              verticalAlignment : StringAlignment.Far,
              textAlignment : StringAlignment.Far
          ),
          // Explicitly specify a preferred size for the label that is much larger than needed for the label's text
          new SizeD(150, 120)
      );
    }

    private static INodeStyle CreateNodeStyle(Palette palette) {
      return new RectangleNodeStyle {
          Brush = palette.Fill, Pen = new Pen(palette.Stroke, 1.5f), CornerStyle = CornerStyle.Round, CornerSize = 3.5
      };
    }

    private static IEdgeStyle CreateEdgeStyle(Palette palette) {
      return new PolylineEdgeStyle {
          Pen = new Pen(palette.Stroke, 1.5f),
          TargetArrow = new Arrow { Brush = palette.Stroke, Type = ArrowType.Triangle }
      };
    }

    /// <summary>
    /// Creates and configures a node label style.
    /// </summary>
    /// <param name="palette">The palette to use for the style's fills and stroke.</param>
    /// <param name="shape">The label shape for the background.</param>
    /// <param name="insets">Optional insets to account for special background shapes.</param>
    /// <param name="wrapText">The optional text wrapping defining how text of the label is wrapped.</param>
    /// <param name="clipText">Determines whether overflowing text should be clipped.</param>
    /// <param name="trimming">The optional text trimming defining how text of the label is trimmed.</param>
    /// <param name="font">The optional font defining the font to render the text with.</param>
    private static ILabelStyle CreateNodeLabelStyle(
        Palette palette = null,
        LabelShape shape = LabelShape.Rectangle,
        double insets = 0,
        bool wrapText = false,
        bool clipText = true,
        StringTrimming trimming = StringTrimming.None,
        Font font = null 
    ) {
      palette = palette ?? Themes.Palette13;
      font = font ?? new Font("Arial", 10.5f);
      return new DefaultLabelStyle {
          Shape = shape,
          BackgroundBrush = palette.NodeLabelFill,
          BackgroundPen = null,
          TextBrush = palette.Text,
          Insets = new InsetsD(insets),
          StringFormat = wrapText
              ? new StringFormat {
                  Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = trimming
              }
              : new StringFormat {
                  Alignment = StringAlignment.Center,
                  LineAlignment = StringAlignment.Center,
                  Trimming = trimming,
                  FormatFlags = StringFormatFlags.NoWrap
              },
          ClipText = clipText,
          Font = font
      };
    }

    /// <summary>
    /// Creates and configures an edge label style.
    /// </summary>
    /// <param name="palette">The palette to use for the style's fills and stroke.</param>
    /// <param name="shape">The label shape for the background.</param>
    /// <param name="insets">Optional insets to account for special background shapes.</param>
    /// <param name="verticalAlignment">The vertical text alignment.</param>
    /// <param name="textAlignment">The vertical text alignment.</param>
    /// <param name="font">The optional font defining the font to render the text with.</param>
    private static ILabelStyle CreateEdgeLabelStyle(
        Palette palette = null,
        LabelShape shape = LabelShape.Rectangle,
        double insets = 0,
        StringAlignment verticalAlignment = StringAlignment.Center,
        StringAlignment textAlignment = StringAlignment.Center,
        Font font = null) {
      palette = palette ?? Themes.Palette13;
      font = font ?? new Font("Arial", 10.5f);
      return new DefaultLabelStyle {
          Shape = shape,
          BackgroundBrush = palette.EdgeLabelFill,
          BackgroundPen = null,
          Insets = new InsetsD(insets),
          StringFormat = new StringFormat {
              Alignment = textAlignment,
              LineAlignment = verticalAlignment,
              FormatFlags = StringFormatFlags.NoWrap
          },
          Font = font
      };
    }

    /// <summary>
    /// Creates a node label at the specified vertical ratio.
    /// </summary>
    /// <param name="layoutRatio">The ratio that describes the point on the node's layout relative to its upper-left corner.</param>
    /// <param name="layoutOffset">The absolute offset to apply to the point on the node after the ratio has been determined.</param>
    private static ILabelModelParameter CreateNodeLabelParameter(PointD layoutRatio, PointD layoutOffset) {
      return FreeNodeLabelModel.Instance.CreateParameter(layoutRatio, layoutOffset, new PointD(0.5, 0.5));
    }

    /// <summary>
    /// Restricts user interaction to selecting, panning, and zooming.
    /// </summary>
    private void InitializeInteraction() {
      graphControl.InputMode = new GraphViewerInputMode();
    }

    /// <summary>
    /// Centers the sample graph in the visible area.
    /// </summary>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      // center the sample graph in the visible area
      graphControl.FitGraphBounds();
    }
    
    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new DefaultLabelStyleForm());
    }

    #endregion

  }
}
