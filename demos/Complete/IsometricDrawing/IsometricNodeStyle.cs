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
using Demo.yFiles.Complete.IsometricDrawing.Model;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Complete.IsometricDrawing
{
  /// <summary>
  /// A node style that visualizes the node as block in an isometric fashion.
  /// </summary>
  public class IsometricNodeStyle : NodeStyleBase<IVisual>
  {
    /// <summary>
    /// Returns a darker shade fill of the given fill.
    /// </summary>
    /// <param name="fill">The base fill.</param>
    /// <returns>A darker shade fill of the <paramref name="fill"/>.</returns>
    public static SolidBrush Darker(SolidBrush fill) {
      var color = fill.Color;
      var factor = 0.7;
      var r = (byte) Math.Max(0, Math.Min(Math.Round(color.R * factor), 255));
      var g = (byte) Math.Max(0, Math.Min(Math.Round(color.G * factor), 255));
      var b = (byte) Math.Max(0, Math.Min(Math.Round(color.B * factor), 255));
      return new SolidBrush(Color.FromArgb(color.A, r, g, b));
    }
    
    /// <summary>
    /// Calculates a vector in world coordinates whose transformation by the <paramref name="projection"/> results
    /// in the vector (0, -1).
    /// </summary>
    /// <param name="projection">The projection to consider.</param>
    /// <returns>The vector in world coordinates that gets transformed to the vector (0, -1).</returns>
    public static PointD CalculateHeightVector(Matrix2D projection) {
      var proj = projection.Clone();
      proj.Invert();
      return proj.Transform(new PointD(0d, -1d));
    }
    
    #region Create/UpdateVisual

    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      var isometricNodeVisual = new IsometricNodeVisual();
      isometricNodeVisual.Update(context, node);
      return isometricNodeVisual;
    }

    /// <summary>
    /// The visual that renders the node as an isometric block.
    /// </summary>
    private sealed class IsometricNodeVisual : IVisual
    {
      private RectD layout;
      private double height;
      private Color color;
      private Matrix2D projection;

      private GeneralPath topFacePath;
      private GeneralPath leftFacePath;
      private GeneralPath rightFacePath;

      private SolidBrush topBrush;
      private SolidBrush leftBrush;
      private SolidBrush rightBrush;
      private Pen pen;

      public void Update(IRenderContext context, INode node) {
        var nodeData = node.Tag as NodeData;
        if (nodeData == null) {
          return;
        }

        if (node.Layout.ToRectD() != layout
            || nodeData.Geometry.Height != height
            || nodeData.Color != color 
            || nodeData.Pen != pen 
            || context.Projection != projection) {
          layout = node.Layout.ToRectD();
          height = nodeData.Geometry.Height;
          color = nodeData.Color;
          pen = nodeData.Pen;
          projection = context.Projection;
          
          var corners = CalculateCorners(context.Projection, layout.X, layout.Y, layout.Width, layout.Height, height);
          var brush = nodeData.Brush;
          if (brush != null) {
            this.topBrush = brush;
            if (height > 0) {
              this.leftBrush = Darker(brush);
              this.rightBrush = Darker(leftBrush);
            }
          }
        
          if (height == 0) {
            leftFacePath = null;
            rightFacePath = null;
          } else if (height > 0) {
            // check which of the left, right, back and front faces are visible using the current projection
            var upVector = CalculateHeightVector(context.Projection);
            var useLeft = upVector.X > 0; 
            var useBack = upVector.Y > 0; 

            leftFacePath = useLeft ? GetLeftFacePath(corners) : GetRightFacePath(corners);
            rightFacePath = useBack ? GetBackFacePath(corners) : GetFrontFacePath(corners);
          }
          topFacePath = GetTopFacePath(corners);
        }
      }
      
      public void Paint(IRenderContext context, Graphics g) {
        if (leftFacePath != null) {
          leftFacePath.Render(g, leftBrush, pen);
        }
        if (rightFacePath != null) {
          rightFacePath.Render(g, rightBrush, pen);
        }
        if (topFacePath != null) {
          topFacePath.Render(g, topBrush, pen);
        }
      }
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      var isometricNodeVisual = oldVisual as IsometricNodeVisual;
      if (isometricNodeVisual == null) {
        return CreateVisual(context, node);
      }
      isometricNodeVisual.Update(context, node);
      return isometricNodeVisual;
    }
    
    #endregion
    
    #region Face Path Factories

    /// <summary>
    /// Creates a <see cref="GeneralPath"/> that describes the face on the top of the block.
    /// </summary>
    /// <param name="corners">The coordinates of the corners of the block.</param>
    /// <returns>A <see cref="GeneralPath"/> that describes the face on the top of the block.</returns>
    static GeneralPath GetTopFacePath(double[] corners) {
      var path = new GeneralPath();
      path.MoveTo(
          corners[UpTopLeftX],
          corners[UpTopLeftY]
      );
      path.LineTo(
          corners[UpTopRightX],
          corners[UpTopRightY]
      );
      path.LineTo(
          corners[UpBottomRightX],
          corners[UpBottomRightY]
      );
      path.LineTo(
          corners[UpBottomLeftX],
          corners[UpBottomLeftY]
      );
      path.Close();
      return path;
    }

    /// <summary>
    /// Creates a <see cref="GeneralPath"/> that describes the face on the left side of the block.
    /// </summary>
    /// <param name="corners">The coordinates of the corners of the block.</param>
    /// <returns>A <see cref="GeneralPath"/> that describes the face on the left side of the block.</returns>
    static GeneralPath GetLeftFacePath(double[] corners) {
      var path = new GeneralPath();
      path.MoveTo(
          corners[LowTopLeftX],
          corners[LowTopLeftY]
      );
      path.LineTo(
          corners[UpTopLeftX],
          corners[UpTopLeftY]
      );
      path.LineTo(
          corners[UpBottomLeftX],
          corners[UpBottomLeftY]
      );
      path.LineTo(
          corners[LowBottomLeftX],
          corners[LowBottomLeftY]
      );
      path.Close();
      return path;
    }
    /// <summary>
    /// Creates a <see cref="GeneralPath"/> that describes the face on the right side of the block.
    /// </summary>
    /// <param name="corners">The coordinates of the corners of the block.</param>
    /// <returns>A <see cref="GeneralPath"/> that describes the face on the right side of the block.</returns>
    static GeneralPath GetRightFacePath(double[] corners) {
      var path = new GeneralPath();
      path.MoveTo(
          corners[LowTopRightX],
          corners[LowTopRightY]
      );
      path.LineTo(
          corners[UpTopRightX],
          corners[UpTopRightY]
      );
      path.LineTo(
          corners[UpBottomRightX],
          corners[UpBottomRightY]
      );
      path.LineTo(
          corners[LowBottomRightX],
          corners[LowBottomRightY]
      );
      path.Close();
      return path;
    }

    /// <summary>
    /// Creates a <see cref="GeneralPath"/> that describes the face on the front side of the block.
    /// </summary>
    /// <param name="corners">The coordinates of the corners of the block.</param>
    /// <returns>A <see cref="GeneralPath"/> that describes the face on the front side of the block.</returns>
    static GeneralPath GetFrontFacePath(double[] corners) {
      var path = new GeneralPath();
      path.MoveTo(
          corners[LowBottomLeftX],
          corners[LowBottomLeftY]
      );
      path.LineTo(
          corners[UpBottomLeftX],
          corners[UpBottomLeftY]
      );
      path.LineTo(
          corners[UpBottomRightX],
          corners[UpBottomRightY]
      );
      path.LineTo(
          corners[LowBottomRightX],
          corners[LowBottomRightY]
      );
      path.Close();
      return path;
    }
    
    /// <summary>
    /// Creates a <see cref="GeneralPath"/> that describes the face on the back side of the block.
    /// </summary>
    /// <param name="corners">The coordinates of the corners of the block.</param>
    /// <returns>A <see cref="GeneralPath"/> that describes the face on the back side of the block.</returns>
    static GeneralPath GetBackFacePath(double[] corners) {
      var path = new GeneralPath();
      path.MoveTo(
          corners[LowTopLeftX],
          corners[LowTopLeftY]
      );
      path.LineTo(
          corners[UpTopLeftX],
          corners[UpTopLeftY]
      );
      path.LineTo(
          corners[UpTopRightX],
          corners[UpTopRightY]
      );
      path.LineTo(
          corners[LowTopRightX],
          corners[LowTopRightY]
      );
      path.Close();
      return path;
    }
    
    // Indices for the corners of the bounding box.
    private const int LowTopLeftX = 0;
    private const int LowTopLeftY = 1;
    private const int LowTopRightX = 2;
    private const int LowTopRightY = 3;
    private const int LowBottomRightX = 4;
    private const int LowBottomRightY = 5;
    private const int LowBottomLeftX = 6;
    private const int LowBottomLeftY = 7;
    private const int UpTopLeftX = 8;
    private const int UpTopLeftY = 9;
    private const int UpTopRightX = 10;
    private const int UpTopRightY = 11;
    private const int UpBottomRightX = 12;
    private const int UpBottomRightY = 13;
    private const int UpBottomLeftX = 14;
    private const int UpBottomLeftY = 15;

    private static double[] CalculateCorners(Matrix2D projection, double x, double y, double width, double depth, double height) {
      var heightVec = height * CalculateHeightVector(projection);

      var corners = new double[16];
      corners[LowTopLeftX] = x;
      corners[LowTopLeftY] = y;

      corners[LowTopRightX] = x + width;
      corners[LowTopRightY] = y;

      corners[LowBottomRightX] = x + width;
      corners[LowBottomRightY] = y + depth;

      corners[LowBottomLeftX] = x;
      corners[LowBottomLeftY] = y + depth;

      for (var i = 0; i < 8; i += 2) {
        corners[i + 8] = corners[i] + heightVec.X;
        corners[i + 9] = corners[i + 1] + heightVec.Y;
      }
      return corners;
    }

    #endregion

    protected override RectD GetBounds(ICanvasContext context, INode node) {
      var layout = node.Layout;
      var nodeData = node.Tag as NodeData;
      if (nodeData == null) {
        return layout.ToRectD();
      }
      var corners = CalculateCorners(context.CanvasControl.Projection, layout.X, layout.Y, layout.Width, layout.Height, nodeData.Geometry.Height);

      double minX = Double.MaxValue;
      double minY = Double.MaxValue;
      double maxX = Double.MinValue;
      double maxY = Double.MinValue;
      for (var i = 0; i < corners.Length; i += 2) {
        minX = Math.Min(minX, corners[i]);
        maxX = Math.Max(maxX, corners[i]);
        minY = Math.Min(minY, corners[i + 1]);
        maxY = Math.Max(maxY, corners[i + 1]);
      }
      return new RectD(minX, minY, maxX - minX, maxY - minY);
    }
  }
}
