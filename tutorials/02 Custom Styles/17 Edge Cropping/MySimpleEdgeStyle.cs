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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// This class is an example for a custom edge style based on <see cref="EdgeStyleBase{TVisual}"/>.
  /// </summary>
  /// <remarks>The color of the edge style depends in the selection state of the edge.</remarks>
  public class MySimpleEdgeStyle : EdgeStyleBase<VisualGroup>
  {

    /// <summary>
    /// Creates a <see cref="GeneralPath"/> from the edge's bends
    /// </summary>
    /// <param name="edge">The edge to create the path for.</param>
    /// <returns>A <see cref="GeneralPath"/> following the edge</returns>
    [NotNull]
    protected override GeneralPath GetPath(IEdge edge) {
      // Create a general path from the locations of the ports and the bends of the edge.
      GeneralPath path = new GeneralPath();
      path.MoveTo(edge.SourcePort.GetLocation());
      foreach (var bend in edge.Bends) {
        path.LineTo(bend.Location);
      }
      path.LineTo(edge.TargetPort.GetLocation());

      ////////////////////////////////////////////////////
      //////////////// New in this sample ////////////////
      ////////////////////////////////////////////////////
      
      // The following lines shorten the path in order to provide room for drawing the arrows.
      path = CropPath(edge, Arrows, Arrows, path);
      ////////////////////////////////////////////////////
      
      return path;
    }

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="MySimpleEdgeStyle"/> class.
    /// </summary>
    public MySimpleEdgeStyle() {
      Arrows = yWorks.Graph.Styles.Arrows.Default;
      PathThickness = 3;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the thickness of the edge
    /// </summary>
    [DefaultValue(3)]
    public int PathThickness { get; set; }

    /// <summary>
    /// Gets or sets the arrows drawn at the beginning and at the end of the edge.
    /// </summary>
    [NotNull]
    public IArrow Arrows { get; set; }

    #endregion

    #region Visual

    /// <summary>
    /// Creates a visual which renders the edge.
    /// </summary>
    /// <remarks>
    /// Creates a VisualGroup which contains the actual edge rendering visual and
    /// the visuals which represent the arrows.
    /// </remarks>
    /// <param name="context">The render context.</param>
    /// <param name="edge">The edge to render.</param>
    /// <returns>A visual which renders the edge.</returns>
    protected override VisualGroup CreateVisual(IRenderContext context, IEdge edge) {
      var color = GetColor(context, edge);

      var group = new VisualGroup();

      // Create the path to be drawn
      GeneralPath gp = GetPath(edge);
      var edgeVisual = new EdgeVisual();
      edgeVisual.Update(gp, PathThickness, color);
      group.Add(edgeVisual);
      // add arrow visuals to the group
      AddArrows(context, group, edge, gp, Arrows, Arrows);
      return group;
    }

    /// <summary>
    /// Updates the visual which renders the edge.
    /// </summary>
    /// <param name="context">The render context.</param>
    /// <param name="group">The visual to update.</param>
    /// <param name="edge">The edge to render.</param>
    /// <returns>The updated visual.</returns>
    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup group, IEdge edge) {
      EdgeVisual edgeVisual;
      if (group.Children.Count < 1 || (edgeVisual = group.Children[0] as EdgeVisual) == null) {
        return CreateVisual(context, edge);
      }
      var color = GetColor(context, edge);

      GeneralPath gp = GetPath(edge);
      edgeVisual.Update(gp, PathThickness, color);
      UpdateArrows(context, group, edge, gp, Arrows, Arrows);
      return group;
    }

    /// <summary>
    /// Get the color for the given edge.
    /// </summary>
    /// <param name="context">The render context.</param>
    /// <param name="edge">The edge to get the color for.</param>
    /// <returns>The color for the edge.</returns>
    private static Color GetColor(IRenderContext context, IEdge edge) {
      Color color = Color.FromArgb(0xc8, 0x00, 0x82, 0xb4);

      // Get the owner node of the source port
      INode owner = edge.SourcePort.Owner as INode;

      bool selected = false;
      // Check if edge is selected
      var graphControl = context.CanvasControl as GraphControl;
      if (graphControl != null) {
        selected = graphControl.Selection.SelectedEdges.IsSelected(edge);
      }

      // Check if the ownernode has stored a color in it's tag
      if (!selected && owner != null) {
        if (owner.Tag is Color) {
          color = (Color) owner.Tag;
        }
      } else if (selected) {
        // if edge is selected, draw with different color
        color = Color.DarkGoldenrod;
      }
      return color;
    }

    // Renders the edge path
    private class EdgeVisual : IVisual
    {
      private GeneralPath path;
      private GraphicsPath graphicsPath;
      private float pathThickness;
      private Color color;
      private LinearGradientBrush brush;

      public void Update(GeneralPath path, double pathThickness, Color color) {
        if (this.path == null || !this.path.Equals(path)) {
          this.graphicsPath = path.CreatePath(new Matrix());
        }
        this.path = path;
        this.pathThickness = (float) pathThickness;
        if (!this.color.Equals(color)) {
          brush = new LinearGradientBrush(new Point(0, 0), new Point(200, 200),
              Color.FromArgb((byte) Math.Max(0, color.A - 50),
                  (byte) Math.Min(255, color.R*1.7),
                  (byte) Math.Min(255, color.G*1.7),
                  (byte) Math.Min(255, color.B*1.7)),
              Color.FromArgb((byte) Math.Max(0, color.A - 50),
                  (byte) Math.Min(255, color.R*1.4),
                  (byte) Math.Min(255, color.G*1.4),
                  (byte) Math.Min(255, color.B*1.4))) {
                    WrapMode = WrapMode.TileFlipXY
                  };
        }
        this.color = color;
      }

      public void Paint(IRenderContext renderContext, Graphics graphics) {
        // Create the path to be drawn
        Pen pen = new Pen(brush) {Width = pathThickness};

        // Draw
        graphics.DrawPath(pen, graphicsPath);
        graphics.DrawPath(Pens.AliceBlue, graphicsPath);
      }

    }

    #endregion

    #region Painting Helper Methods

   /// <summary>
    /// Determines whether the visual representation of the edge has been hit at the given location.
    /// Overridden method to include the <see cref="PathThickness"/> and the HitTestRadius specified in the context
    /// in the calculation.
    /// </summary>
    protected override bool IsHit(IInputModeContext context, PointD location, IEdge edge) {
      // Use the convenience method in GeneralPath
      return GetPath(edge).PathContains(location, context.HitTestRadius + PathThickness * 0.5d);
    }



    /// <summary>
    /// This implementation of the look up provides a custom implementation of the 
    /// <see cref="ISelectionIndicatorInstaller"/> interface that better suits to this style.
    /// </summary>
    protected override object Lookup(IEdge edge, Type type) {
      if (type == typeof(ISelectionIndicatorInstaller)) {
        return new MySelectionIndicatorInstaller();
      } else {
        return base.Lookup(edge, type);
      }
    }

    #endregion

    #region Selection Customization

    /// <summary>
    /// This customized <see cref="ISelectionIndicatorInstaller"/> overrides the
    /// pen property to be <see langword="null"/>, so that no edge path is rendered if the edge is selected.
    /// </summary>
    private sealed class MySelectionIndicatorInstaller : EdgeSelectionIndicatorInstaller
    {
      protected override Pen GetPen(CanvasControl context, IEdge edge) {
        return null;
      }
    }

    #endregion
  }
}