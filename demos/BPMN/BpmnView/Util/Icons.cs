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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;

namespace Demo.yFiles.Graph.Bpmn.Util
{

  /// <summary>
  /// An extension of <see cref="IVisualCreator"/> that allows to set bounds for the visualization.
  /// </summary>
  /// <remarks>
  /// To use this interface for the flyweight pattern, <see cref="SetBounds"/> should be called before creating or updating the visuals.
  /// </remarks>
  internal interface IIcon : IVisualCreator, ILookup {

    /// <summary>
    /// Sets the bounds the visual shall consider.
    /// </summary>
    /// <param name="bounds"></param>
    void SetBounds(IRectangle bounds);
  }

  internal abstract class IconBase : IIcon {

    public IRectangle Bounds { get; protected set; }

    protected IconBase() {
      Bounds = new MutableRectangle(0, 0, 0, 0);
    }

    public virtual void SetBounds(IRectangle bounds) {
      Bounds = bounds;
    }

    public abstract IVisual CreateVisual(IRenderContext context);
    public abstract IVisual UpdateVisual(IRenderContext context, IVisual oldVisual);
    
    public virtual object Lookup(Type type) {
      return null;
    }
  }

  internal class PathIcon : IconBase
  {
    public Brush Brush { get; set; }

    public Pen Pen { get; set; }

    internal GeneralPath Path { get; set; }

    public override IVisual CreateVisual(IRenderContext context) {
      var matrix = new Matrix();
      matrix.Translate((float) Bounds.X, (float) Bounds.Y);
      matrix.Scale((float) Math.Max(0, Bounds.Width), (float) Math.Max(0, Bounds.Height));
      return new GeneralPathVisual(Path){Brush = Brush, Pen = Pen, Transform = matrix};
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      var path = oldVisual as GeneralPathVisual;
      if (path == null) {
        return CreateVisual(context);
      }
      path.Path = Path;
      path.Brush = Brush;
      path.Pen = Pen;
      var matrix = new Matrix();
      matrix.Translate((float) Bounds.X, (float) Bounds.Y);
      matrix.Scale((float) Math.Max(0, Bounds.Width), (float) Math.Max(0, Bounds.Height));
      path.Transform = matrix;
      return path;
    }
  }


  internal class CombinedIcon : IconBase
  {

    private readonly IList<IIcon> icons;

    public CombinedIcon(IList<IIcon> icons) {
      this.icons = icons;
    }

    public override IVisual CreateVisual(IRenderContext context) {
      if (Bounds == null) {
        return null;
      }
      var container = new CombinedGroup{Size = Bounds.GetSize(), TopLeft = Bounds.GetTopLeft()};

      var iconBounds = new RectD(PointD.Origin, Bounds.ToSizeD());
      foreach (var icon in icons) {
        icon.SetBounds(iconBounds);
        container.Add(icon.CreateVisual(context));
      }
      container.Transform = new Matrix(1,0,0,1,(float) Bounds.X, (float) Bounds.Y);
      return container;
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      var container = oldVisual as CombinedGroup;
      if (container == null || container.Children.Count != icons.Count) {
        return CreateVisual(context);
      }
      if (container.Size != Bounds.ToSizeD()) {
        // size changed -> we have to update the icons
        var iconBounds = new RectD(PointD.Origin, Bounds.ToSizeD());
        int index = 0;
        foreach (var pathIcon in icons) {
          pathIcon.SetBounds(iconBounds);
          var oldPathVisual = container.Children[index];
          var newPathVisual = pathIcon.UpdateVisual(context, oldPathVisual);
          if (oldPathVisual != newPathVisual) {
            container.Children.Remove(oldPathVisual);
            container.Children.Insert(index, newPathVisual);
          }
          index++;
        }
        container.Size = Bounds.ToSizeD();
      } else if (container.TopLeft == Bounds.GetTopLeft()) {
        // bounds didn't change at all
        return container;
      }
      container.TopLeft = Bounds.GetTopLeft();
      container.Transform = new Matrix(1,0,0,1,(float) Bounds.X, (float) Bounds.Y);

      return container;
    }

    private class CombinedGroup : VisualGroup
    {
      public PointD TopLeft;
      public SizeD Size;
    }


    public override object Lookup(Type type) {
      if (type == typeof(IClickHandler)) {
        return new MyClickHandler(this);
      }
      return base.Lookup(type);
    }

    private class MyClickHandler : IClickHandler, IHitTestable
    {
      private readonly IList<IIcon> icons;
      private readonly CombinedIcon coreIcon;

      public MyClickHandler(CombinedIcon coreIcon) {
        this.icons = coreIcon.icons;
        this.coreIcon = coreIcon;
      }

      public IHitTestable HitTestable {
        get { return this; }
      }

      public void OnClicked(IInputModeContext ctx, PointD p) {
        var topLeft = coreIcon.Bounds.GetTopLeft();
        foreach (var icon in icons) {
          var abp = icon.Lookup<IClickHandler>();
          var iconBounds = new RectD(PointD.Origin, coreIcon.Bounds.ToSizeD());
          icon.SetBounds(iconBounds);
          if (ctx.CanvasControl != null && abp != null && abp.HitTestable.IsHit(ctx.CanvasControl.InputModeContext, p - topLeft)) {
            abp.OnClicked(ctx, p - topLeft);
            return;
          }
        }
      }

      public bool IsHit(IInputModeContext ctx, PointD p) {
        var topLeft = coreIcon.Bounds.GetTopLeft();
        foreach (var icon in icons) {
          var abp = icon.Lookup<IClickHandler>();
          if (abp != null) {
            var iconBounds = new RectD(PointD.Origin, coreIcon.Bounds.ToSizeD());
            icon.SetBounds(iconBounds);
            if (abp.HitTestable.IsHit(ctx, p - topLeft)) {
              return true;
            }
          }
        }
        return false;
      }
    }
  }

  internal class LineUpIcon : IconBase
  {
    private readonly IList<IIcon> icons;
    private readonly SizeD innerIconSize;
    private readonly double gap;

    private readonly SizeD combinedSize;

    public LineUpIcon(IList<IIcon> icons, SizeD innerIconSize, double gap) {
      this.icons = icons;
      this.innerIconSize = innerIconSize;
      this.gap = gap;

      double combinedWidth = icons.Count * innerIconSize.Width + (icons.Count - 1) * gap;
      combinedSize = new SizeD(combinedWidth, innerIconSize.Height);
    }

    public override object Lookup(Type type) {
      if (type == typeof(IClickHandler)) {
        return new MyClickHandler(this);
      }
      return base.Lookup(type);
    }

    public override IVisual CreateVisual(IRenderContext context) {
      if (Bounds == null) {
        return null;
      }

      var container = new VisualGroup();

      double offset = 0;
      foreach (var pathIcon in icons) {
        pathIcon.SetBounds(new RectD(offset, 0, innerIconSize.Width, innerIconSize.Height));
        container.Add(pathIcon.CreateVisual(context));
        offset += innerIconSize.Width + gap;
      }
      container.Transform = new Matrix(1,0,0,1,(float) Bounds.X, (float) Bounds.Y);

      return container;
    }

    private class MyClickHandler : IClickHandler, IHitTestable
    {
      private readonly IList<IIcon> icons;
      private readonly LineUpIcon coreIcon;

      public MyClickHandler(LineUpIcon coreIcon) {
        this.icons = coreIcon.icons;
        this.coreIcon = coreIcon;
      }

      public IHitTestable HitTestable {
        get { return this; }
      }

      public void OnClicked(IInputModeContext ctx, PointD p) {
        var topLeft = coreIcon.Bounds.GetTopLeft();
        double offset = 0;
        foreach (var icon in icons) {
          var abp = icon.Lookup<IClickHandler>();
          icon.SetBounds(new RectD(offset, 0, coreIcon.innerIconSize.Width, coreIcon.innerIconSize.Height));
          if (ctx.CanvasControl != null && abp != null && abp.HitTestable.IsHit(ctx.CanvasControl.InputModeContext, p-topLeft)) {
            abp.OnClicked(ctx, p-topLeft);
            return;
          }
          offset += coreIcon.innerIconSize.Width + coreIcon.gap;
        }
      }

      public bool IsHit(IInputModeContext ctx, PointD p) {
        var topLeft = coreIcon.Bounds.GetTopLeft();
        double offset = 0;
        foreach (var icon in icons) {
          var abp = icon.Lookup<IClickHandler>();
          icon.SetBounds(new RectD(offset, 0, coreIcon.innerIconSize.Width, coreIcon.innerIconSize.Height));
          if (abp != null) {
            if (abp.HitTestable.IsHit(ctx, p-topLeft)) {
              return true;
            }
          }
          offset += coreIcon.innerIconSize.Width + coreIcon.gap;
        }
        return false;
      }
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      var container = oldVisual as VisualGroup;
      if (container == null || container.Children.Count != icons.Count) {
        return CreateVisual(context);
      }
      container.Transform = new Matrix(1, 0, 0, 1, (float) Bounds.X, (float) Bounds.Y);
      return container;
    }

    public override void SetBounds(IRectangle bounds) {
      base.SetBounds(RectD.FromCenter(bounds.GetCenter(), combinedSize));
    }
  }

  internal class PlacedIcon : IIcon
  {
    private readonly SimpleNode dummyNode;
    private readonly SimpleLabel dummyLabel;
    private readonly ILabelModelParameter placementParameter;
    private readonly IIcon innerIcon;

    public PlacedIcon(IIcon innerIcon, ILabelModelParameter placementParameter, SizeD minimumSize) {
      this.innerIcon = innerIcon;
      this.placementParameter = placementParameter;
      dummyNode = new SimpleNode();
      dummyLabel = new SimpleLabel(dummyNode, "", placementParameter) { PreferredSize = minimumSize };
    }

    public IVisual CreateVisual(IRenderContext context) {
      return innerIcon.CreateVisual(context);
    }

    public IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      return innerIcon.UpdateVisual(context, oldVisual);
    }

    public object Lookup(Type type) {
      if (type == typeof(IClickHandler)) {
        return innerIcon.Lookup<IClickHandler>();
      }
      return null;
    }

    public virtual void SetBounds(IRectangle bounds) {
      dummyNode.Layout = bounds;
      innerIcon.SetBounds(placementParameter.Model.GetGeometry(dummyLabel, placementParameter).GetBounds());
    }
  }

  internal class RectIcon : IconBase
  {
    internal double CornerRadius { get; set; }

    internal Brush Brush { get; set; }

    internal Pen Pen { get; set; }

    public override IVisual CreateVisual(IRenderContext context) {
      return new RoundedRectangleVisual(new RectD(PointD.Origin, Bounds.GetSize()), CornerRadius) {
        Pen = Pen,
        Brush = Brush,
        Transform = new Matrix(1, 0, 0, 1, (float) Bounds.X, (float) Bounds.Y)
      };
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      var rect = oldVisual as RoundedRectangleVisual;
      if (rect == null) {
        return CreateVisual(context);
      }
      rect.Pen = Pen;
      rect.Brush = Brush;
      rect.Radius = CornerRadius;
      rect.Rectangle = new RectD(PointD.Origin, Bounds.GetSize());
      rect.Transform = new Matrix(1, 0, 0, 1, (float) Bounds.X, (float) Bounds.Y);
      return rect;
    }
  }

  internal class VariableRectIcon : IconBase {

    internal double TopLeftRadius { get; set; }
    internal double TopRightRadius { get; set; }
    internal double BottomLeftRadius { get; set; }
    internal double BottomRightRadius { get; set; }

    internal Brush Brush { get; set; }

    internal Pen Pen { get; set; }

    public override IVisual CreateVisual(IRenderContext context) {
      var bounds = Bounds;
      var width = bounds.Width;
      var height = bounds.Height;

      var path = new GeneralPath();
      path.MoveTo(0, TopLeftRadius);
      path.QuadTo(0, 0, TopLeftRadius, 0);
      path.LineTo(width - TopRightRadius, 0);
      path.QuadTo(width, 0, width, TopRightRadius);
      path.LineTo(width, height - BottomRightRadius);
      path.QuadTo(width, height, width - BottomRightRadius, height);
      path.LineTo(BottomLeftRadius, height);
      path.QuadTo(0, height, 0, height - BottomRightRadius);
      path.Close();

      return new GeneralPathVisual(path){Brush = Brush, Pen = Pen, Transform = new Matrix(1,0,0,1,(float) Bounds.X, (float) Bounds.Y)};
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      return CreateVisual(context);
    }
  }

  internal class DataObjectIcon : IconBase
  {

    internal Brush Brush { get; set; }

    internal Pen Pen { get; set; }


    public override IVisual CreateVisual(IRenderContext context) {
      VisualGroup container = new VisualGroup();

      var bounds = Bounds;
      var width = bounds.Width;
      var height = bounds.Height;
      var cornerSize = Math.Min(width, height) * 0.4;

      var path = new GeneralPath();
      path.MoveTo(0, 0);
      path.LineTo(width - cornerSize, 0);
      path.LineTo(width, cornerSize);
      path.LineTo(width, height);
      path.LineTo(0, height);
      path.Close();
      container.Add(new GeneralPathVisual(path){Brush = Brush, Pen = Pen});

      path = new GeneralPath();
      path.MoveTo(width - cornerSize, 0);
      path.LineTo(width - cornerSize, cornerSize);
      path.LineTo(width, cornerSize);
      container.Add(new GeneralPathVisual(path){Brush = null, Pen = Pen});

      container.Transform = new Matrix(1,0,0,1,(float) Bounds.X, (float) Bounds.Y);
      return container;
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      return CreateVisual(context);
    }
  }

  internal class CollapseButtonIcon : IconBase, IClickHandler
  {

    private readonly IIcon collapsedIcon;
    private readonly IIcon expandedIcon;

    private readonly INode node;

    public CollapseButtonIcon(INode node, Brush iconBrush) {
      this.node = node;
      collapsedIcon = IconFactory.CreateStaticSubState(SubState.Collapsed, iconBrush);
      expandedIcon = IconFactory.CreateStaticSubState(SubState.Expanded, iconBrush);
    }

    public override IVisual CreateVisual(IRenderContext context) {
      collapsedIcon.SetBounds(new RectD(Bounds.GetTopLeft(), Bounds.GetSize()));
      expandedIcon.SetBounds(new RectD(Bounds.GetTopLeft(), Bounds.GetSize()));
      bool expanded = true;

      CanvasControl canvas = context != null ? context.CanvasControl : null;
      if (canvas != null) {
        IGraph graph = canvas.Lookup(typeof(IGraph)) as IGraph;
        if (graph != null) {
          IFoldingView foldedGraph = graph.Lookup<IFoldingView>();
          if (foldedGraph != null && foldedGraph.Graph.Contains(node)) {
            expanded = foldedGraph.IsExpanded(node);
          }
        }
      }
      return expanded ? expandedIcon.CreateVisual(context) : collapsedIcon.CreateVisual(context);
    }

    public override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual) {
      return CreateVisual(context);
    }
    
    public IHitTestable HitTestable {
      get { return HitTestables.Create((ctx, p) => Bounds.ToRectD().Contains(p, ctx.HitTestRadius)); }
    }

    public void OnClicked(IInputModeContext context, PointD location) {
      Commands.ToggleExpansionState.Execute(node, context.CanvasControl);
    }

    public override object Lookup(Type type) {
      if(type == typeof(IClickHandler)) {
        return this;
      }
      return base.Lookup(type);
    }
  }
}
