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

using System.Drawing;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Tutorial.CustomStyles
{
  ////////////////////////////////////////////////////////////////
  /////////////// This class is new in this sample ///////////////
  ////////////////////////////////////////////////////////////////
  
  /// <summary>
  /// This class is an example for a custom style based on the <see cref="LabelStyleBase{TVisual}"/>.
  /// The font for the label text can be set. The label text is drawn with black letters inside a blue rounded rectangle.
  /// </summary>
  public class MySimpleLabelStyle : LabelStyleBase<VisualGroup>
  {
    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="MySimpleLabelStyle"/> class using the "Arial" typeface.
    /// </summary>
    public MySimpleLabelStyle() {
      Font = new Font("Arial", 8);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the typeface used for rendering the label text.
    /// </summary>
    public Font Font { get; set; }

    #endregion

    #region Visual

    /// <summary>
    /// Creates an <see cref="IVisual"/> which renders the label.
    /// </summary>
    /// <remarks>
    /// Adds separate visuals for background and text in a VisualGroup.
    /// Delegates to method <see cref="LabelStyleBase{TVisual}.GetTransform"/>
    /// to get a valid transform for the label.
    /// </remarks>
    /// <param name="context">The Render Context.</param>
    /// <param name="label">The label to render.</param>
    /// <returns>A visual which renders the label.</returns>
    protected override VisualGroup CreateVisual(IRenderContext context, ILabel label) {
      var layout = label.GetLayout();

      // don't render invisible labels
      if (layout.Width < 0 || layout.Height < 0) {
        return null;
      }
      var group = new VisualGroup();

      // convert to convenient coordinate space
      group.Transform = GetTransform(context, layout, true);

      // Draw the label background as a rounded rectangle
      GeneralPath gp = new GeneralPath(10);
      double xRad = layout.Width / 4;
      double yRad = layout.Height / 10;
      gp.MoveTo(0, yRad);
      gp.QuadTo(0, 0, xRad, 0);
      gp.LineTo(layout.Width - xRad, 0);
      gp.QuadTo(layout.Width, 0, layout.Width, yRad);
      gp.LineTo(layout.Width, layout.Height - yRad);
      gp.QuadTo(layout.Width, layout.Height, layout.Width - xRad, layout.Height);
      gp.LineTo(xRad, layout.Height);
      gp.QuadTo(0, layout.Height, 0, layout.Height - yRad);
      gp.Close();
      var pathVisual = new GeneralPathVisual(gp){Brush = new SolidBrush(Color.FromArgb(255, 155, 226, 255)), Pen = Pens.SkyBlue};
      group.Add(pathVisual);

      // Draw the label's text
      group.Add(new TextVisual {
        Text = label.Text,
        Font = Font,
        Brush = Brushes.Black,
        Location = PointD.Origin
      });
      return group;
    }

    #endregion
    
    #region Painting Helper Methods

    /// <summary>
    /// Calculates the preferred size for the given label if this style is used for the rendering.
    /// </summary>
    /// <remarks>
    /// The size is calculated from the label's text.
    /// </remarks>
    protected override SizeD GetPreferredSize(ILabel label) {
      return new SizeD(80, 15);
    }

    #endregion
  }
}
