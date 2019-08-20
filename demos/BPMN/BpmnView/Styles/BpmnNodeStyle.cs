/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.2.
 ** Copyright (c) 2000-2019 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using System.Drawing.Drawing2D;
using System.Reflection;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using IIcon = Demo.yFiles.Graph.Bpmn.Util.IIcon;

namespace Demo.yFiles.Graph.Bpmn.Styles {

  /// <summary>
  /// A <see cref="NodeStyleBase{TVisual}"/> implementation used as base class for nodes styles representing BPMN elements.
  /// </summary>
  [Obfuscation(StripAfterObfuscation = false, Exclude = true, ApplyToMembers = false)]
  public class BpmnNodeStyle : NodeStyleBase<IVisual> {

    #region Properties

    /// <summary>
    /// Gets or sets the minimum node size for nodes using this style.
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(typeof(SizeD), "Empty")]
    public SizeD MinimumSize { get; set; }

    internal IIcon Icon { get; set; }

    internal int ModCount { get; set; }

    #endregion

    #region IVisualCreator methods

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      UpdateIcon(node);
      if (Icon == null) {
        return null;
      }

      var bounds = node.Layout.ToRectD();
      Icon.SetBounds(new RectD(PointD.Origin, bounds.ToSizeD()));
      var visual = Icon.CreateVisual(context);

      var container = new BpmnContainer {ModCount = ModCount, Bounds = bounds};
      if (visual != null) {
        container.Add(visual);
      }
      container.Transform = new Matrix(1, 0, 0, 1, (float) bounds.X, (float) bounds.Y);
      return container;
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      if (Icon == null) {
        return null;
      }

      var container = oldVisual as BpmnContainer;
      if (container == null || container.ModCount != ModCount) {
        return CreateVisual(context, node);
      }

      RectD newBounds = node.Layout.ToRectD();

      if (container.Bounds == newBounds) {
        // node bounds didn't change
        return oldVisual;
      }

      if (container.Bounds.Size != newBounds.Size) {
        RectD newIconBounds = new RectD(PointD.Origin, newBounds.Size);
        Icon.SetBounds(newIconBounds);

        IVisual oldIconVisual = null;
        IVisual newIconVisual = null;
        if (container.Children.Count == 0) {
          newIconVisual = Icon.CreateVisual(context);
        } else {
          oldIconVisual = container.Children[0];
          newIconVisual = Icon.UpdateVisual(context, oldIconVisual);
        }

        // update visual
        if (oldIconVisual != newIconVisual) {
          if (oldIconVisual != null) {
            container.Remove(oldIconVisual);
          }
          if (newIconVisual != null) {
            container.Add(newIconVisual);
          }
        }
      }

      // update the transformation that moves the container to the location of the node
      container.Transform = new Matrix(1,0,0,1,(float) newBounds.X, (float) newBounds.Y);
      return container;
    }

    #endregion

    /// <summary>
    /// Updates the <see cref="Icon"/>.
    /// </summary>
    /// <param name="node">The node to which this style instance is assigned.</param>
    /// <remarks>
    /// This method is called by <see cref="CreateVisual"/>.
    /// </remarks>
    internal virtual void UpdateIcon(INode node) {
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override object Lookup(INode node, Type type) {
      var lookup = base.Lookup(node, type);
      if (lookup == null && type == typeof(INodeSizeConstraintProvider)) {
        if (!MinimumSize.IsEmpty) {
          return new NodeSizeConstraintProvider(MinimumSize, SizeD.Infinite);
        }
      } 
      return lookup;
    }

    private class BpmnContainer : VisualGroup
    {
      public int ModCount { get; set; }
      public RectD Bounds { get; set; }

    }
  }

}