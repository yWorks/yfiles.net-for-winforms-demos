/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.LabelEditing
{
  /// <summary>
  /// A label style that renders labels always at the same size regardless of the zoom level.
  /// </summary>
  /// <remarks>
  /// The style is implemented as a wrapper for an existing label style.
  /// </remarks>
  public class ZoomInvariantLabelStyle : LabelStyleBase<VisualGroup>
  {

    private readonly SimpleLabel dummyLabel;
    private readonly OrientedRectangle rectangle;

    /// <summary>
    /// Instantiates a new label style.
    /// </summary>
    public ZoomInvariantLabelStyle(ILabelStyle innerLabelStyle) {
      InnerLabelStyle = innerLabelStyle;
      rectangle = new OrientedRectangle();
      dummyLabel = new SimpleLabel(null, string.Empty, new FreeLabelModel().CreateDynamic(rectangle));
    }

    /// <summary>
    /// The inner style to use for the rendering.
    /// </summary>
    public ILabelStyle InnerLabelStyle { get; set; }

    /// <summary>
    /// Returns the preferred size calculated by the <see cref="InnerLabelStyle"/>.
    /// </summary>
    protected override SizeD GetPreferredSize(ILabel label) {
      return InnerLabelStyle.Renderer.GetPreferredSize(label, InnerLabelStyle);
    }

    protected override VisualGroup CreateVisual(IRenderContext context, ILabel label) {
      // Updates the dummy label which is internally used for rendering with the properties of the given label.
      UpdateDummyLabel(context, label);

      // creates the container for the visual and sets a transform for view coordinates
      var container = new VisualGroup();
      var toViewTransform = context.WorldTransform.Clone();
      toViewTransform.Invert();
      // ReSharper disable once PossibleUnintendedReferenceComparison
      if (container.Transform != toViewTransform) {
        container.Transform = toViewTransform;
      }
      
      var creator = InnerLabelStyle.Renderer.GetVisualCreator(dummyLabel, InnerLabelStyle);

      // create a new IRenderContext with a zoom of 1
      // TODO: Projections
      var innerContext = new RenderContext(context.Graphics, context.CanvasControl) { ViewTransform = context.ViewTransform, WorldTransform = context.WorldTransform, Zoom = 1 };

      //The wrapped style should always think it's rendering with zoom level 1
      var visual = creator.CreateVisual(innerContext);
      if (visual == null) {
        return container;
      }

      // add the created visual to the container
      container.Children.Add(visual);
      

      IGraphSelection selection = context.CanvasControl != null ? context.CanvasControl.Lookup<IGraphSelection>() : null;

      bool selected = selection != null && selection.IsSelected(label);
      // if the label is selected, add the selection visualization, too.

      if (selected) {
        //The selection descriptor performs its own calculation in the view coordinate system, so
        //the size and the position of the visualization would be wrong and need to be converted back into world
        //coordinates
        var layout = dummyLabel.GetLayout();
        var p1 = context.CanvasControl.ToWorldCoordinates(layout.GetAnchorLocation());
        var selectionLayout = new OrientedRectangle
        {
          Anchor = p1,
          Width = layout.Width / context.Zoom,
          Height = layout.Height / context.Zoom
        };

        selectionLayout.SetUpVector(layout.UpX, layout.UpY);
        var selectionVisual = new OrientedRectangleIndicatorInstaller().Template;
        container.Children.Add(selectionVisual);
      }
      return container;
    }

    /// <summary>
    /// Updates the internal label to match the given original label.
    /// </summary>
    private void UpdateDummyLabel(ICanvasContext context, ILabel original) {
      dummyLabel.Owner = original.Owner;
      dummyLabel.Style = original.Style;
      dummyLabel.Tag = original.Tag;
      dummyLabel.Text = original.Text;

      var location = original.GetLayout();
      rectangle.Reshape(location);
      rectangle.Width = location.Width/context.Zoom;
      rectangle.Height = location.Height/context.Zoom;

      dummyLabel.PreferredSize = rectangle.ToSizeD();
      rectangle.Reshape(original.LayoutParameter.Model.GetGeometry(dummyLabel, original.LayoutParameter));
      dummyLabel.PreferredSize = location.ToSizeD();
      WorldToIntermediateCoordinates(context, rectangle);
    }

    /// <inheritdoc/>
    protected override RectD GetBounds(ICanvasContext context, ILabel label) {
      UpdateDummyLabel(context, label);
      return IntermediateToWorldCoordinates(context,
                                InnerLabelStyle.Renderer.GetBoundsProvider(dummyLabel, InnerLabelStyle).GetBounds(
                                  context));
    }

    /// <inheritdoc/>
    protected override bool IsVisible(ICanvasContext context, RectD rectangle, ILabel label) {
      return GetBounds(context, label).Intersects(rectangle);
    }

    /// <inheritdoc/>
    protected override bool IsHit(IInputModeContext context, PointD location, ILabel label) {
      return GetBounds(context, label).Contains(location);
    }

    /// <inheritdoc/>
    protected override bool IsInBox(IInputModeContext context, RectD rectangle, ILabel label) {
      return GetBounds(context, label).Intersects(rectangle);
    }

    /// <summary>
    /// This implementation of the look up provides a custom implementation of the 
    /// <see cref="ISelectionIndicatorInstaller"/> interface that better suits to this style.
    /// </summary>
    /// <remarks>
    /// This implementation uses the actual visual label bounds to render the selection
    /// </remarks>
    protected override object Lookup(ILabel label, Type type) {
      return type == typeof(ISelectionIndicatorInstaller)
        ? new OrientedRectangleIndicatorInstaller(new OrientedRectangle(0, 0, -1, -1), OrientedRectangleIndicatorInstaller.SelectionTemplateKey)
        : base.Lookup(label, type);
    }

    #region Conversion between view and world coordinates

    /// <summary>
    /// Converts the given <see cref="OrientedRectangle"/> from the world into the view coordinate space. 
    /// </summary>
    internal static void WorldToIntermediateCoordinates(ICanvasContext context, OrientedRectangle rect) {
      var anchor = new PointD(rect.Anchor);
      var anchorAndUp = anchor + rect.GetUp();

      var renderContext = context as IRenderContext ?? context.Lookup(typeof (IRenderContext)) as IRenderContext;
      if (renderContext != null) {
        anchor = renderContext.WorldToIntermediateCoordinates(anchor);
        anchorAndUp = renderContext.WorldToIntermediateCoordinates(anchorAndUp);
      } else {
        var cc = context.Lookup(typeof (CanvasControl)) as CanvasControl;
        if (cc != null) {
          anchor = cc.WorldToIntermediateCoordinates(anchor);
          anchorAndUp = cc.WorldToIntermediateCoordinates(anchorAndUp);
        } else {
          // too bad - infer trivial scale matrix
          anchor *= context.Zoom;
          anchorAndUp *= context.Zoom;
        }
      }

      rect.SetUpVector((anchorAndUp - anchor).Normalized);
      rect.SetAnchor(anchor);
      rect.Width *= context.Zoom;
      rect.Height *= context.Zoom;
    }

    /// <summary>
    /// Converts the given rectangle from the view into the world coordinate space. 
    /// </summary>
    internal static RectD IntermediateToWorldCoordinates(ICanvasContext context, RectD rect) {
      var renderContext = context as IRenderContext ?? context.Lookup(typeof (IRenderContext)) as IRenderContext;
      if (renderContext != null) {
        return IntermediateToWorldCoordinates(renderContext.CanvasControl, rect);
      }
      var cc = context.Lookup(typeof (CanvasControl)) as CanvasControl;
      if (cc != null) {
        return IntermediateToWorldCoordinates(cc, rect);
      }
      // too bad - infer trivial scale matrix
      return new RectD(rect.X, rect.Y, rect.Width / context.Zoom, rect.Height / context.Zoom);
    }

    /// <summary>
    /// Converts the given rectangle from the view into the world coordinate space. 
    /// </summary>
    internal static RectD IntermediateToWorldCoordinates(CanvasControl canvas, RectD rect) {
      var p1 = GetRounded(canvas.IntermediateToWorldCoordinates(rect.GetTopLeft()));
      var p2 = GetRounded(canvas.IntermediateToWorldCoordinates(rect.GetBottomRight()));
      return new RectD(p1.X, p1.Y, (int) Math.Max(0, p2.X - p1.X), (int) Math.Max(0, p2.Y - p1.Y));
    }

    private static PointD GetRounded(PointD p) {
      return new PointD(Math.Round(p.X), Math.Round(p.Y));
    }

    #endregion
  }
}
