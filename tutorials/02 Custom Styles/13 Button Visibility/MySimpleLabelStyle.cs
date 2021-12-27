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
using System.Drawing;
using Tutorial.CustomStyles.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Tutorial.CustomStyles
{
  /// <summary>
  /// This class is an example for a custom style based on the <see cref="LabelStyleBase{TVisual}"/>.
  /// The font for the label text can be set. The label text is drawn with black letters inside a rounded rectangle.
  /// Also there is a customized button displayed in the label at certain zoom levels that enables editing of the label text.
  /// </summary>
  public class MySimpleLabelStyle : LabelStyleBase<VisualGroup>
  {

    ////////////////////////////////////////////////////
    //////////////// New in this sample ////////////////
    ////////////////////////////////////////////////////

    private const double buttonZoomThreshold = 1.5;
    ////////////////////////////////////////////////////

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

      ////////////////////////////////////////////////////
      //////////////// New in this sample ////////////////
      ////////////////////////////////////////////////////

      // Draw the label's text
      if (context.Zoom >= buttonZoomThreshold) {
        // draw the action button
        group.Add(new ImageVisual{Image = Resources.edit_label, Rectangle =  new RectD(layout.Width - (inset + buttonWidth), inset, buttonWidth, layout.Height - inset * 2)});
        group.Add(new TextVisual {Text = label.Text, Font = Font, Brush = Brushes.Black, Location = PointD.Origin});
      } else {
        group.Add(new TextVisual {
          Text = label.Text,
          Font = Font,
          Brush = Brushes.Black,
          Location = new PointD((buttonWidth + 2*inset)*0.5, 0)
        });
      }

      ////////////////////////////////////////////////////
      
      return group;
    }

    #endregion

    #region Fields

    private const int inset = 2;
    private const int buttonWidth = 7;

    #endregion

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

    #region Painting Helper Methods

    /// <summary>
    /// Calculates the preferred size for the given label if this style is used for the rendering.
    /// </summary>
    /// <remarks>
    /// The size is calculated from the label's text.
    /// </remarks>
    protected override SizeD GetPreferredSize(ILabel label) {
      using (Graphics g = Graphics.FromImage(new Bitmap(1, 1))) {
        SizeF preferredSize = g.MeasureString(label.Text, Font, new PointF(0, 0), StringFormat.GenericDefault);
        // add the size for the action button
        return new SizeD(preferredSize.Width + 2 * inset + buttonWidth, Math.Max(2 * inset + buttonWidth, preferredSize.Height));
      }
    }

    protected override object Lookup(ILabel label, Type type) {
      // provide the action button provider
      if (type == typeof(IClickHandler)) {
        return new MyClickHandler(label);
      } else {
        return base.Lookup(label, type);
      }
    }

    #endregion

    #region Click Handler

    /// <summary>
    /// An <see cref="IClickHandler" /> implementation that provides 
    /// a custom edit button for a label.
    /// </summary>
    private sealed class MyClickHandler : IClickHandler, IHitTestable
    {
      private readonly ILabel label;

      public MyClickHandler(ILabel label) {
        this.label = label;
      }

      public IHitTestable HitTestable {
        get { return this; }
      }

      // starts label editing
      public void OnClicked(IInputModeContext context, PointD location) {
        Commands.EditLabel.Execute(label, context.CanvasControl);
      }

      // determines whether the button has been hit
      public bool IsHit(IInputModeContext context, PointD location) {
        // only handle if the button is displayed
        if (context.Zoom >= buttonZoomThreshold) {
          // see if the button is visible at all
          IOrientedRectangle orientedRectangle = label.GetLayout();
          if (orientedRectangle.Contains(location, context.HitTestRadius)) {
            location = location - orientedRectangle.GetAnchorLocation();

            double upX = orientedRectangle.UpX;
            double upY = orientedRectangle.UpY;

            double tx = location.X*-upY + location.Y*upX;
            double ty = location.X*upX + location.Y*upY;

            // consider auto flipping of the label contents
            if (upY > 0) {
              return
                  new RectD(inset, inset, 20, orientedRectangle.Height - 2*inset).Contains(new PointD(tx, ty),
                      context.HitTestRadius);
            } else {
              return
                  new RectD(orientedRectangle.Width - (inset + buttonWidth), inset, buttonWidth,
                      orientedRectangle.Height - 2*inset).Contains(new PointD(tx, ty),
                          context.HitTestRadius);
            }
          } else {
            return false;
          }
        } else {
          return false;
        }
      }
    }

    #endregion
  }
}