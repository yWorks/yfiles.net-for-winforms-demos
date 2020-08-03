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
using System.Drawing.Drawing2D;
using System.Linq;
using yWorks.Controls.Input;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Graph.PortLocationModels;
using yWorks.Utils;

namespace Tutorial.CustomStyles
{

  ////////////////////////////////////////////////////////////////
  /////////////// This class is new in this sample ///////////////
  ////////////////////////////////////////////////////////////////

  /// <summary>
  /// A simple node style wrapper that takes a given node style and adds label edge rendering 
  /// as a visual decorator on top of the wrapped visualization.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This node style wrapper implementation adds the label edge rendering that was formerly part
  /// of <see cref="MySimpleNodeStyle"/> to the wrapped style. For this purpose of this tutorial step, 
  /// label edge rendering was removed from <see cref="MySimpleNodeStyle"/>.
  /// </para>
  /// <para>
  /// Similar to this implementation, wrapping styles for other graph items can be created by implementing
  /// <see cref="EdgeStyleBase{TVisual}"/>, <see cref="LabelStyleBase{TVisual}"/> and
  /// <see cref="PortStyleBase{TVisual}"/>.
  /// </para>
  /// </remarks>
  class MyNodeStyleDecorator : NodeStyleBase<VisualGroup> {

    // the wrapped style
    private readonly INodeStyle wrapped;

    /// <summary>
    /// Creates a new instance of this style using the given wrapped style.
    /// </summary>
    /// <param name="wrappedStyle">The style that is decorated by this instance.</param>
    public MyNodeStyleDecorator([NotNull]INodeStyle wrappedStyle) {
      this.wrapped = wrappedStyle;
    }

    #region Rendering

    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      // create the outer container
      VisualGroup container = new VisualGroup();
      // create the wrapped style's visual
      var wrappedVisual = wrapped.Renderer.GetVisualCreator(node, wrapped).CreateVisual(context);

      // create label edges as decorators for wrapped style
      var labelEdgesContainer = new VisualGroup();
      RenderLabelEdges(context, node, labelEdgesContainer);
      labelEdgesContainer.Transform = new Matrix(1, 0, 0, 1, (float) node.Layout.X, (float) node.Layout.Y);

      // add both visuals to outer container
      container.Add(wrappedVisual);
      container.Add(labelEdgesContainer);

      return container;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldVisual, INode node) {
      // get the container's children
      var wrappedVisual = oldVisual.Children[0];
      var labelEdgesContainer = (VisualGroup) oldVisual.Children[1];

      // update the wrapped visual
      var updateVisual = wrapped.Renderer.GetVisualCreator(node, wrapped).UpdateVisual(context, wrappedVisual);
      if(oldVisual.Children[0] != updateVisual) {
        oldVisual.Children[0] = updateVisual;
      }

      RenderLabelEdges(context, node, labelEdgesContainer);
      labelEdgesContainer.Transform = new Matrix(1, 0, 0, 1, (float) node.Layout.X, (float) node.Layout.Y);
      return oldVisual;
    }

    /// <summary>
    /// Draws the edge-like connectors from a node to its labels
    /// </summary>
    private void RenderLabelEdges(IRenderContext context, INode node, VisualGroup container) {
      int count = 0;
      if (node.Labels.Count > 0) {
        // Create a SimpleEdge which will be used as a dummy for the rendering
        SimpleEdge simpleEdge = new SimpleEdge(null, null);
        // Assign the style
        simpleEdge.Style = new MySimpleEdgeStyle { PathThickness = 2 };

        // Create a SimpleNode which provides the sourceport for the edge but won't be drawn itself
        SimpleNode sourceDummyNode = new SimpleNode {
          Layout = new RectD(0, 0, node.Layout.Width, node.Layout.Height),
          Style = node.Style
        };


        // Set sourceport to the port of the node using a dummy node that is located at the origin.
        simpleEdge.SourcePort = new SimplePort(sourceDummyNode, FreeNodePortLocationModel.NodeCenterAnchored);

        // Create a SimpleNode which provides the targetport for the edge but won't be drawn itself
        SimpleNode targetDummyNode = new SimpleNode();

        // Create port on targetDummynode for the label target
        targetDummyNode.Ports =
            new ListEnumerable<IPort>(new[]
            {new SimplePort(targetDummyNode, FreeNodePortLocationModel.NodeCenterAnchored)});
        simpleEdge.TargetPort = new SimplePort(targetDummyNode, FreeNodePortLocationModel.NodeCenterAnchored);

        var topLeft = node.Layout.GetTopLeft();
        var labelLocations = node.Labels.Select(l => l.GetLayout().GetCenter() - topLeft);

        // Render one edge for each label
        foreach (PointD labelLocation in labelLocations) {
          // move the dummy node to the location of the label
          targetDummyNode.Layout = new MutableRectangle(labelLocation, SizeD.Zero);

          // now create the visual using the style interface:
          IEdgeStyleRenderer renderer = simpleEdge.Style.Renderer;
          IVisualCreator creator = renderer.GetVisualCreator(simpleEdge, simpleEdge.Style);
          if (container.Children.Count > count) {
            container.Children[count] = creator.UpdateVisual(context, container.Children[count]);
          } else {
            container.Children.Add(creator.CreateVisual(context));
          }
          count++;
        }
      }
      // remove superfluous visuals
      while (container.Children.Count > count) {
        container.Children.RemoveAt(container.Children.Count - 1);
      }
    }
    
    #endregion

    #region Rendering Helper Methods

    protected override RectD GetBounds(ICanvasContext context, INode node) {
      // delegate this to the wrapped style
      return wrapped.Renderer.GetBoundsProvider(node, wrapped).GetBounds(context);
    }

    protected override bool IsVisible(ICanvasContext context, RectD rectangle, INode node) {
      // first check if the wrapped style is visible
      if (wrapped.Renderer.GetVisibilityTestable(node, wrapped).IsVisible(context, rectangle)) {
        return true;
      }
      // if not, check for labels connection lines 
      rectangle = rectangle.GetEnlarged(10);
      foreach (var label in node.Labels) {
        if (rectangle.IntersectsLine(node.Layout.GetCenter(), label.GetLayout().GetCenter())) {
          return true;
        }
      }
      return false;
    }

    protected override bool IsHit(IInputModeContext context, PointD location, INode node) {
      // delegate this to the wrapped style since we don't want the visual decorator to be hit testable
      return wrapped.Renderer.GetHitTestable(node, wrapped).IsHit(context, location);
    }

    protected override bool IsInBox(IInputModeContext context, RectD rectangle, INode node) {
      // delegate this to the wrapped style
      return wrapped.Renderer.GetMarqueeTestable(node, wrapped).IsInBox(context, rectangle);
    }

    protected override object Lookup(INode node, Type type) {
      // delegate this to the wrapped style
      return wrapped.Renderer.GetContext(node, wrapped).Lookup(type);
    }

    protected override PointD? GetIntersection(INode node, PointD inner, PointD outer) {
      // delegate this to the wrapped style
      return wrapped.Renderer.GetShapeGeometry(node, wrapped).GetIntersection(inner, outer);
    }

    protected override bool IsInside(INode node, PointD location) {
      // delegate this to the wrapped style
      return wrapped.Renderer.GetShapeGeometry(node, wrapped).IsInside(location);
    }

    protected override GeneralPath GetOutline(INode node) {
      // delegate this to the wrapped style
      return wrapped.Renderer.GetShapeGeometry(node, wrapped).GetOutline();
    }

    #endregion
  }
}
