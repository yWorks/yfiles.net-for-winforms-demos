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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// This class is a simple example for a custom arrow based on <see cref="IArrow"/>.
  /// </summary>
  public class MySimpleArrow : IArrow, IVisualCreator, IBoundsProvider
  {
    // these variables hold the state for the flyweight pattern
    // they are populated in GetVisualCreator and used in the implementations of the IVisualCreator interface.
    private PointD anchor;
    private PointD direction;
    private double thickness;

    #region Properties

    /// <summary>
    /// Returns the length of the arrow, i.e. the distance from the arrow's tip to
    /// the position where the visual representation of the edge's path should begin.
    /// </summary>
    /// <value>Always returns 0</value>
    public double Length {
      get { return 0; }
    }

    /// <summary>
    /// Gets the cropping length associated with this instance.
    /// </summary>
    /// <value>Always returns 1</value>
    /// <remarks>
    /// This value is used by <see cref="IEdgeStyle"/>s to let the
    /// edge appear to end shortly before its actual target.
    /// </remarks>
    public double CropLength {
      get { return 1; }
    }

    /// <summary>
    /// Gets or sets the thickness of the arrow
    /// </summary>
    [DefaultValue(2.0d)]
    private double Thickness { get; set; }

    #endregion

    #region IArrow members

    /// <summary>
    /// Gets an <see cref="IVisualCreator"/> implementation that will paint
    /// this arrow at the given location using the given direction 
    /// for the given edge.
    /// </summary>
    /// <param name="edge">The edge this arrow belongs to</param>
    /// <param name="atSource">Whether this will be the source arrow</param>
    /// <param name="anchor">The anchor point for the tip of the arrow</param>
    /// <param name="direction">The direction the arrow is pointing in</param>
    /// <returns>
    /// Itself as a flyweight.
    /// </returns>
    public IVisualCreator GetVisualCreator(IEdge edge, bool atSource, PointD anchor, PointD direction) {
      // Get the edge's thickness
      MySimpleEdgeStyle style = edge.Style as MySimpleEdgeStyle;
      if (style != null) {
        thickness = style.PathThickness;
      } else {
        thickness = Thickness;
      }
      this.anchor = anchor;
      this.direction = direction.Normalized;
      return this;
    }

    /// <summary>
    /// Gets an <see cref="IBoundsProvider"/> implementation that can yield
    /// this arrow's bounds if painted at the given location using the
    /// given direction for the given edge.
    /// </summary>
    /// <param name="edge">The edge this arrow belongs to</param>
    /// <param name="atSource">Whether this will be the source arrow</param>
    /// <param name="anchor">The anchor point for the tip of the arrow</param>
    /// <param name="directionVector">The direction the arrow is pointing in</param>
    /// <returns>
    /// an implementation of the <see cref="IBoundsProvider"/> interface that can
    /// subsequently be used to query the bounds. Clients will always call
    /// this method before using the implementation and may not cache the instance returned.
    /// This allows for applying the flyweight design pattern to implementations.
    /// </returns>
    public IBoundsProvider GetBoundsProvider(IEdge edge, bool atSource, PointD anchor, PointD directionVector) {
      // Get the edge's thickness
      MySimpleEdgeStyle style = edge.Style as MySimpleEdgeStyle;
      if (style != null) {
        Thickness = style.PathThickness;
      }
      this.anchor = anchor;
      this.direction = directionVector;
      return this;
    }

    #endregion

    #region Visual

    /// <summary>
    /// Creates a path visual which renders the arrow.
    /// </summary>
    public IVisual CreateVisual(IRenderContext context) {
      // Create a new Path to draw the arrow
      GraphicsPath path = new GraphicsPath();
      path.AddLine(new PointF(-7, (float) (-thickness/2f)), new PointF(-7, (float) (thickness/2f)));
      path.AddBezier(new PointF(-7, (float) (thickness/2f)),new PointF(-5, (float) (thickness/2f)), new PointF(-1.5f, (float) (thickness/2)), new PointF(1, (float) (thickness*1.666f)));
      path.AddBezier(new PointF(1, (float) (thickness*1.666f)),new PointF(0, (float) (thickness*0.833)), new PointF(0, (float) (-thickness*0.833)), new PointF(1, (float) (-thickness*1.666)));
      path.AddBezier(new PointF(1, (float) (-thickness*1.666)),new PointF(-1.5f, (float) (-thickness/2f)), new PointF(-5, (float) (-thickness/2f)), new PointF(-7, (float) (-thickness/2f)));

      return new ArrowVisual {
        Path = path,
        Brush = Brushes.DarkGray,
        Transform = new Matrix((float) direction.X, (float) direction.Y, (float) (-direction.Y), (float) direction.X, (float) anchor.X, (float) anchor.Y),
        Thickness = thickness
      };
    }

    /// <summary>
    /// This method updates or replaces a previously created <see cref="IVisual"/> for inclusion
    /// in the <see cref="IRenderContext"/>.
    /// </summary>
    /// <param name="context">The context that describes where the visual will be used in.</param>
    /// <param name="oldVisual">The visual instance that had been returned the last time the <see cref="IVisualCreator.CreateVisual"/>
    /// method was called on this instance.</param>
    /// <returns>
    /// 	<paramref name="oldVisual"/>, if this instance modified the visual, or a new visual that should replace the
    /// existing one in the canvas object visual tree.
    /// </returns>
    /// <remarks>
    /// The <see cref="CanvasControl"/> uses this method to give implementations a chance to
    /// update an existing <see cref="IVisual"/> that has previously been created by the same instance during a call
    /// to <see cref="IVisualCreator.CreateVisual"/>. Implementation may update the <paramref name="oldVisual"/>
    /// and return that same reference, or create a new visual and return the new instance or <see langword="null"/>.
    /// </remarks>
    /// <seealso cref="IVisualCreator.CreateVisual"/>
    /// <seealso cref="ICanvasObjectDescriptor"/>
    /// <seealso cref="CanvasControl"/>
    public IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      var p = oldVisual as ArrowVisual;
      if (p == null) {
        return CreateVisual(context);
      }
      // get thickness of old arrow
      double oldThickness = p.Thickness;
      // if thickness has changed
      if (oldThickness != thickness) {
        // re-render arrow
        return CreateVisual(context);
      } else {
        p.Transform = new Matrix((float) direction.X, (float) direction.Y, (float) -direction.Y, (float) direction.X, (float) anchor.X, (float) anchor.Y);
        return p;
      }
    }

    // Keeps the current thickness as state
    // so update visual can determine when to create a new path
    private class ArrowVisual : GraphicsPathVisual
    {
      public double Thickness { get; set; }
    }

    #endregion

    #region IBoundsProvider members

    /// <summary>
    /// Returns the bounds of the arrow for the current flyweight configuration.
    /// </summary>
    RectD IBoundsProvider.GetBounds(ICanvasContext ctx) {
      return new RectD(anchor.X - 8 - thickness, anchor.Y - 8 - thickness, 16 + thickness * 2, 16 + thickness * 2);
    }

    #endregion

  }
}