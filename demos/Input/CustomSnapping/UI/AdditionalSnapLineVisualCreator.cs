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

using System.Collections.Generic;
using System.Drawing;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;

namespace Demo.yFiles.Graph.Input.CustomSnapping.UI
{
  /// <summary>
  /// A visual creator for simple orthogonal snap lines.
  /// </summary>
  public class AdditionalSnapLineVisualCreator : IVisualCreator
  {
    private readonly Brush SnapLineBrush = new SolidBrush(Color.FromArgb(0xCA, 0x0C, 0x3B));
    
    public PointD From { get; set; }

    public PointD To { get; set; }

    /// <summary>
    /// Creates a new instance with <see cref="From"/> and <see cref="To"/> both pointing to the origin.
    /// </summary>
    public AdditionalSnapLineVisualCreator() {}

    /// <summary>
    /// Creates a new instance with the given start and end point of the snap line.
    /// </summary>
    /// <param name="from">The location to start the additional snap line.</param>
    /// <param name="to">The location to end the additional snap line.</param>
    public AdditionalSnapLineVisualCreator(PointD from, PointD to) {
      From = from;
      To = to;
    }

    /// <summary>
    /// Creates the <see cref="OrthogonalSnapLine"/>s that are displayed by this visual creator.
    /// </summary>
    /// <remarks>
    /// Since items should be able to snap from both sides to this line, two snap lines with the same location and 
    /// different <see cref="SnapLineSnapTypes"/> are created.
    /// </remarks>
    /// <returns></returns>
    public IEnumerable<OrthogonalSnapLine> CreateSnapLines() {
      var lines = new List<OrthogonalSnapLine>();
      if (From.X == To.X) { // it's vertical
        lines.Add(new OrthogonalSnapLine(SnapLineOrientation.Vertical, SnapLineSnapTypes.Left,
            SnapLine.SnapLineFixedLineKey, (From + To) / 2, From.Y, To.Y, this, 50));
        lines.Add(new OrthogonalSnapLine(SnapLineOrientation.Vertical, SnapLineSnapTypes.Right,
            SnapLine.SnapLineFixedLineKey, (From + To) / 2, From.Y, To.Y, this, 50));
      } else if (From.Y == To.Y) { // it's horizontal
        lines.Add(new OrthogonalSnapLine(SnapLineOrientation.Horizontal, SnapLineSnapTypes.Top,
            SnapLine.SnapLineFixedLineKey, (From + To) / 2, From.X, To.X, this, 50));
        lines.Add(new OrthogonalSnapLine(SnapLineOrientation.Horizontal, SnapLineSnapTypes.Bottom,
            SnapLine.SnapLineFixedLineKey, (From + To) / 2, From.X, To.X, this, 50));
      }
      return lines;
    }

    public IVisual CreateVisual(IRenderContext context) {
      return new LineVisual(From, To) {Pen = new Pen(SnapLineBrush, 2f)};
    }

    public IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      return CreateVisual(context);
    }
  }
}
