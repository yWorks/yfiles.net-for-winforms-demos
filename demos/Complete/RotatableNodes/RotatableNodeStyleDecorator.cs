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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Markup;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.GraphML;

namespace Demo.yFiles.Complete.RotatableNodes
{
  /// <summary>
  /// A node style that displays another wrapped style rotated by a specified rotation angle.
  /// </summary>
  /// <remarks>
  /// The angle is stored in this decorator to keep the tag free for user data. Hence, this decorator should not be
  /// shared between nodes if they can have different angles.
  /// </remarks>
  public class RotatableNodeStyleDecorator : NodeStyleBase<VisualGroup>, IMarkupExtensionConverter
  {
    private readonly Matrix2D matrix = new Matrix2D();
    private PointD matrixCenter = PointD.Origin;
    private double matrixAngle;

    private readonly Matrix2D inverseMatrix = new Matrix2D();
    private PointD inverseMatrixCenter = PointD.Origin;
    private double inverseMatrixAngle;

    private readonly CachingOrientedRectangle rotatedLayout = new CachingOrientedRectangle();

    /// <summary>
    /// The wrapped style.
    /// </summary>
    public INodeStyle Wrapped { get; set; }

    /// <summary>
    /// The rotation angle.
    /// </summary>
    public double Angle {
      get { return rotatedLayout.Angle; }
      set { rotatedLayout.Angle = value; }
    }

    /// <summary>
    /// Creates a new instance with a wrapped node style and an angle.
    /// </summary>
    public RotatableNodeStyleDecorator(INodeStyle wrapped = null, double angle = 0) {
      Wrapped = wrapped ?? new ShapeNodeStyle();
      Angle = angle;
    }

    /// <summary>
    /// Creates a visual which rotates the visualization of the wrapped style.
    /// </summary>
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var visual = Wrapped.Renderer.GetVisualCreator(node, Wrapped).CreateVisual(context);
      var center = node.Layout.GetCenter();
      var rotation = new Matrix();
      rotation.RotateAt((float)-Angle, new PointF((float)center.X, (float)center.Y));
      var container = new RotatableNodeStyleVisual(Wrapped, Angle, center) {Transform = rotation};
      container.Add(visual);
      context.RegisterForChildrenIfNecessary(container, DisposeChildren);
      return container;
    }

    private IVisual DisposeChildren(IRenderContext ctx, IVisual removedVisual, bool dispose) {
      var container = removedVisual as VisualGroup;
      if (container != null && container.Children.Count > 0) {
        ctx.ChildVisualRemoved(container.Children[0]);
      }
      return null;
    }

    /// <summary>
    /// Updates a visual which rotates the visualization of the wrapped style.
    /// </summary>
    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldVisual, INode node) {
      var container = oldVisual as RotatableNodeStyleVisual;
      if (container == null || container.Children == null || container.Children.Count != 1) {
        return CreateVisual(context, node);
      }

      var oldWrappedStyle = container.WrappedStyle;
      var newWrappedStyle = Wrapped;
      var creator = newWrappedStyle.Renderer.GetVisualCreator(node, newWrappedStyle);

      IVisual oldWrappedVisual = container.Children[0];
      IVisual newWrappedVisual;
      if (newWrappedStyle != oldWrappedStyle) {
        newWrappedVisual = creator != null ? creator.CreateVisual(context) : null;
      } else {
        newWrappedVisual = creator != null ? creator.UpdateVisual(context, oldWrappedVisual) : null;
      }

      if (oldWrappedVisual != newWrappedVisual) {
        container.Children[0] = newWrappedVisual;
        context.ChildVisualRemoved(oldWrappedVisual);
      }
      context.RegisterForChildrenIfNecessary(oldVisual, DisposeChildren);

      var center = node.Layout.GetCenter();
      if (newWrappedStyle != oldWrappedStyle || container.Angle != Angle || !container.Center.Equals(center)) {
        var rotation = container.Transform;
        rotation.Reset();
        rotation.RotateAt((float)-Angle, new PointF((float)center.X, (float)center.Y));
        container.WrappedStyle = newWrappedStyle;
        container.Center = center;
        container.Angle = -Angle;
      }

      return container;
    }

    /// <summary>
    /// Returns bounds based on the size provided by the wrapped style and the location and rotation of the node.
    /// </summary>
    protected override RectD GetBounds(ICanvasContext context, INode node) {
      var nodeOrientedRect = GetRotatedLayout(node);

      // Create an oriented rectangle with the size of the wrapped bounds and the location and rotation of the node
      var wrappedBounds = Wrapped.Renderer.GetBoundsProvider(node, Wrapped).GetBounds(context);

      var orientedRectangle = new OrientedRectangle(0, 0, wrappedBounds.Width, wrappedBounds.Height,
        nodeOrientedRect.UpX, nodeOrientedRect.UpY);
      orientedRectangle.SetCenter(node.Layout.GetCenter());

      return orientedRectangle.GetBounds();
    }

    /// <summary>
    /// Returns the intersection point of the node's rotated bounds and the segment between the inner and outer point or
    /// <see langword="null"/> if there is no intersection.
    /// </summary>
    protected override PointD? GetIntersection(INode node, PointD inner, PointD outer) {
      var rotatedInner = GetRotatedPoint(inner, node, false);
      var rotatedOuter = GetRotatedPoint(outer, node, false);

      var rotatedIntersection = Wrapped.Renderer.GetShapeGeometry(node, Wrapped).GetIntersection(rotatedInner, rotatedOuter);
      return rotatedIntersection.HasValue ? (PointD?) GetRotatedPoint(rotatedIntersection.Value, node, true) : null;
    }

    protected override bool IsInside(INode node, PointD location) {
      return Wrapped.Renderer.GetShapeGeometry(node, Wrapped).IsInside(GetRotatedPoint(location, node, false));
    }

    /// <summary>
    /// Returns the outline of the node's rotated shape.
    /// </summary>
    protected override GeneralPath GetOutline(INode node) {
      var outline = Wrapped.Renderer.GetShapeGeometry(node, Wrapped).GetOutline();
      if (outline == null) {
        outline = new GeneralPath(4);
        outline.AppendRectangle(node.Layout.ToRectD(), false);
      } else {
        outline = (GeneralPath) outline.Clone();
      }
      outline.Transform(GetInverseRotationMatrix(node));
      return outline;
    }

    /// <summary>
    /// Returns whether or not the given location is inside the rotated node.
    /// </summary>
    protected override bool IsHit(IInputModeContext context, PointD location, INode node) {
      // rotated the point like the node, that is by the angle around the node center
      var transformedPoint = GetRotatedPoint(location, node, false);
      return Wrapped.Renderer.GetHitTestable(node, Wrapped).IsHit(context, transformedPoint);
    }

    /// <summary>
    /// Returns whether or not the given node is inside the rectangle.
    /// </summary>
    protected override bool IsInBox(IInputModeContext context, RectD rectangle, INode node) {
      var nodeOrientedRect = GetRotatedLayout(node);

      // Create an oriented rectangle with the size of the wrapped bounds and the location and rotation of the node
      var wrappedBounds = Wrapped.Renderer.GetBoundsProvider(node, Wrapped).GetBounds(context);
      var orientedRectangle = new OrientedRectangle(0, 0, wrappedBounds.Width, wrappedBounds.Height,
        nodeOrientedRect.UpX, nodeOrientedRect.UpY);
      orientedRectangle.SetCenter(node.Layout.GetCenter());

      return rectangle.Intersects(orientedRectangle, 0.01);
    }

    /// <summary>
    /// Returns whether or not the node is currently visible.
    /// </summary>
    protected override bool IsVisible(ICanvasContext context, RectD rectangle, INode node) {
      return Wrapped.Renderer.GetVisibilityTestable(node, Wrapped).IsVisible(context, rectangle) 
        || GetBounds(context, node).Intersects(rectangle);
    }

    /// <summary>
    /// Returns customized helpers that consider the node rotation for resizing and rotating gestures, highlight 
    /// indicators, and clipboard operations.
    /// </summary>
    /// <remarks>
    /// Other lookup calls will be delegated to the lookup of the wrapped node style.
    /// </remarks>
    protected override object Lookup(INode node, Type type) {
      // Custom reshape handles that rotate with the node
      if (type == typeof(IReshapeHandleProvider)) {
        return new RotatedReshapeHandleProvider(node);
      }
      // Custom handle to rotate the node
      if (type == typeof(IHandleProvider)) {
        return new NodeRotateHandleProvider(node);
      }
      // Selection decoration
      if (type == typeof(ISelectionIndicatorInstaller)) {
        return new RotatableNodeIndicatorInstaller(RotatableNodeIndicatorInstaller.NodeSelectionTemplateKey);
      }
      // Focus decoration
      if (type == typeof(IFocusIndicatorInstaller)) {
        return new RotatableNodeIndicatorInstaller(OrientedRectangleIndicatorInstaller.FocusTemplateKey);
      }
      // Highlight decoration
      if (type == typeof(IHighlightIndicatorInstaller)) {
        return new RotatableNodeIndicatorInstaller(OrientedRectangleIndicatorInstaller.HighlightTemplateKey);
      }
      // Clipboard helper that clones the style instance when pasting rotated nodes
      if (type == typeof(IClipboardHelper)) {
        return new RotatableNodeClipboardHelper();
      }

      return base.Lookup(node, type) ?? Wrapped.Renderer.GetContext(node, Wrapped).Lookup(type);
    }

    /// <summary>
    /// Creates a copy of this node style decorator.
    /// </summary>
    public override object Clone() {
      return new RotatableNodeStyleDecorator(Wrapped, Angle);
    }

    /// <summary>
    /// Returns the rotated bounds of the node.
    /// </summary>
    public CachingOrientedRectangle GetRotatedLayout(INode node) {
      rotatedLayout.UpdateCache(node.Layout.ToRectD());
      return rotatedLayout;
    }

    /// <summary>
    /// Returns the rotated point.
    /// </summary>
    public PointD GetRotatedPoint(PointD point, INode node, bool inverse) {
      var matrix = inverse ? GetInverseRotationMatrix(node) : GetRotationMatrix(node);
      return matrix.Transform(point);
    }

    /// <summary>
    /// Returns the rotation matrix for the given node and the current angle.
    /// </summary>
    private Matrix2D GetRotationMatrix(INode node) {
      var center = node.Layout.GetCenter();
      if (!(center.Equals(matrixCenter)) || Angle != matrixAngle) {
        matrix.Reset();
        matrix.Rotate(CachingOrientedRectangle.ToRadians(Angle), center);
        matrixCenter = center;
        matrixAngle = Angle;
      }
      return matrix;
    }

    /// <summary>
    /// Returns the inverse rotation matrix for the given node and the current angle.
    /// </summary>
    private Matrix2D GetInverseRotationMatrix(INode node) {
      var center = node.Layout.GetCenter();
      if (!(center.Equals(inverseMatrixCenter)) || Angle != inverseMatrixAngle) {
        inverseMatrix.Reset();
        inverseMatrix.Rotate(CachingOrientedRectangle.ToRadians(-Angle), center);
        inverseMatrixCenter = center;
        inverseMatrixAngle = Angle;
      }
      return inverseMatrix;
    }

    /// <summary>
    /// Returns that this style can be converted.
    /// </summary>
    /// <param name="context">The current write context.</param>
    /// <param name="value">The object to convert.</param>
    public bool CanConvert(IWriteContext context, object value) {
      return true;
    }

    /// <summary>
    /// Converts this style using <see cref="RotatableNodeStyleDecoratorExtension"/>.
    /// </summary>
    /// <param name="context">The current write context.</param>
    /// <param name="value">The object to convert.</param>
    public MarkupExtension Convert(IWriteContext context, object value) {
      var decorator = value as RotatableNodeStyleDecorator;
      var extension = new RotatableNodeStyleDecoratorExtension {
        Wrapped = decorator.Wrapped, Angle = decorator.Angle
      };
      return extension;
    }

    private sealed class RotatableNodeStyleVisual : VisualGroup
    {
      public INodeStyle WrappedStyle { get; set; }

      public double Angle { get; set; }

      public PointD Center { get; set; }

      public RotatableNodeStyleVisual(INodeStyle wrappedStyle, double angle, PointD center) {
        WrappedStyle = wrappedStyle;
        Angle = angle;
        Center = center;
      }
    }
  }

  /// <summary>
  /// Markup extension that helps (de-)serializing a <see cref="RotatableNodeStyleDecorator"/>.
  /// </summary>
  [ContentProperty("Wrapped")]
  public class RotatableNodeStyleDecoratorExtension : MarkupExtension
  {
    /// <summary>
    /// The rotation angle.
    /// </summary>
    [DefaultValue(0)]
    public double Angle { get; set; }

    /// <summary>
    /// The wrapped style.
    /// </summary>
    public INodeStyle Wrapped { get; set; }


    public override object ProvideValue(IServiceProvider serviceProvider) {
      return new RotatableNodeStyleDecorator(Wrapped, Angle);
    }
  }
}
