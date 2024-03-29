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

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.SimpleArrow
{
  /// <summary>
  /// An arrow that appears like the edge tapers to a point.
  /// </summary>
  public class TaperedArrow : IArrow, IVisualCreator, IBoundsProvider
  {
    // these variables hold the state for the flyweight pattern
    // they are populated in GetVisualCreator and used in the implementations of the IVisualCreator interface.
    private PointD anchor;
    private PointD direction;

    /// <summary>
    /// Backing field for the <see cref="Width"/> property
    /// </summary>
    private double width;

    /// <summary>
    /// The unrotated bounds of the arrow
    /// </summary>
    private RectD bounds;

    /// <summary>
    /// Backing field for the <see cref="Length"/> property
    /// </summary>
    private double length;

    private static readonly GraphicsPath arrowFigure;

    /// <summary>
    /// Returns the length of the arrow, i.e. the distance from the arrow's tip to
    /// the position where the visual representation of the edge's path should begin.
    /// </summary>
    [DefaultValue(10)]
    public double Length {
      get { return length; }
      set {
        length = value;
        bounds = new RectD(-length, -width / 2, length, width);
      }
    }

    /// <summary>
    /// Gets the cropping length associated with this instance.
    /// </summary>
    /// <value>Always returns 0</value>
    /// <remarks>
    /// This value is used by <see cref="IEdgeStyle"/>s to let the
    /// edge appear to end directly at its actual target.
    /// </remarks>
    public double CropLength {
      get { return 0; }
    }

    public Brush Fill { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the arrow
    /// </summary>
    [DefaultValue(2.0d)]
    public double Width {
      get {
        return width;
      }
      set {
        width = value;
        bounds = new RectD(-length, -width / 2, length, width);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TaperedArrow"/> class.
    /// </summary>
    public TaperedArrow() {
      this.Width = 2.0d;
      this.Length = 10.0d;
      this.Fill = Brushes.Black;
    }

    static TaperedArrow() {
      // Create a new Path to draw the arrow
      // We draw in a normalized coordinate system where the edge is horizontal and meets the target at (0,0)
      // The path is just a simple triangle with length and width 1 - the actual adjustment is done by simply scaling everything later.
      arrowFigure = new GraphicsPath();
      // We create a tiny overlap between the edge and the arrow by painting a fraction over the edge.
      // This avoids anti-aliasing artifacts where the edge meets the arrow.
      arrowFigure.AddLine(new PointF(-1.1f, -0.5f), new PointF(-1f, -0.5f));
      arrowFigure.AddLine(new PointF(-1f, -0.5f), new PointF(-0f, 0f));
      arrowFigure.AddLine(new PointF(-0f, 0f), new PointF(-1f, 0.5f));
      arrowFigure.AddLine(new PointF(-1f, 0.5f), new PointF(-1.1f, 0.5f));
    }

    /// <summary>
    /// Gets an <see cref="IVisualCreator"/> implementation that will create
    /// the <see cref="IVisual"/> for this arrow
    /// at the given location using the given direction for the given edge.
    /// </summary>
    /// <param name="edge">the edge this arrow belongs to</param>
    /// <param name="atSource">whether this will be the source arrow</param>
    /// <param name="anchor">the anchor point for the tip of the arrow</param>
    /// <param name="direction">the direction the arrow is pointing in</param>
    /// <returns>
    /// Itself as a flyweight.
    /// </returns>
    public IVisualCreator GetVisualCreator(IEdge edge, bool atSource, PointD anchor, PointD direction) {
      this.anchor = anchor;
      this.direction = direction;
      return this;
    }

    /// <summary>
    /// Gets an <see cref="IBoundsProvider"/> implementation that can yield
    /// this arrow's bounds if painted at the given location using the
    /// given direction for the given edge.
    /// </summary>
    /// <param name="edge">the edge this arrow belongs to</param>
    /// <param name="atSource">whether this will be the source arrow</param>
    /// <param name="anchor">the anchor point for the tip of the arrow</param>
    /// <param name="directionVector">the direction the arrow is pointing in</param>
    /// <returns>
    /// an implementation of the <see cref="IBoundsProvider"/> interface that can
    /// subsequently be used to query the bounds. Clients will always call
    /// this method before using the implementation and may not cache the instance returned.
    /// This allows for applying the flyweight design pattern to implementations.
    /// </returns>
    public IBoundsProvider GetBoundsProvider(IEdge edge, bool atSource, PointD anchor, PointD directionVector) {
      this.anchor = anchor;
      this.direction = directionVector;
      return this;
    }

    /// <summary>
    /// Creates the visual for an arrow.
    /// </summary>
    public IVisual CreateVisual(IRenderContext context) {
      return new GraphicsPathVisual { Brush = Fill, Path = arrowFigure, Transform = (Matrix) CreateMatrix() };
    }

    /// <summary>
    /// Re-renders the arrow using the old visual for performance reasons.
    /// </summary>
    public IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      if (!(oldVisual is GraphicsPathVisual p)) {
        // No path provided, recreate the visual from scratch
        return CreateVisual(context);
      }

      // get the brush with which the old visual was created
      Brush oldCache = p.Brush;
      // check if fill changed
      if (Fill != oldCache) {
        // fill changed - update the path
        p.Brush = Fill;
      }

      // Otherwise just scale and rearrange and transform the arrow path to the right location
      p.Transform = (Matrix)CreateMatrix();
      return p;
    }

    /// <summary>
    /// Returns the bounds of the arrow for the current flyweight configuration.
    /// </summary>
    RectD IBoundsProvider.GetBounds(ICanvasContext context) {
      return bounds.GetTransformed(CreateMatrix());
    }

    private Matrix2D CreateMatrix() {
      var m = new Matrix2D(direction.X, -direction.Y, direction.Y, direction.X, anchor.X, anchor.Y);
      m.Scale(length, width);
      return m;
    }
  }
}
