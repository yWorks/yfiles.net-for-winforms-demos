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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using PointF = System.Drawing.PointF;

namespace Demo.yFiles.Graph.OrgChart
{
  public class OrgChartNodeStyle : NodeStyleBase<VisualGroup>
  {
    private readonly Dictionary<DoubleRange, INodeStyle> nodeStyles = new Dictionary<DoubleRange, INodeStyle>();
    private DoubleRange maxValue;
    private readonly ShinyPlateNodeStyle dropShadow = new ShinyPlateNodeStyle { Brush = Brushes.Transparent, DrawShadow = true };

    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var style = GetNodeStyle(context.Zoom);

      var group = new OrgChartNodeStyleVisualGroup(style);

      // use ShinyPlateNodeStyle to render a high-performance dropshadow for better looks
      group.Add(dropShadow.Renderer.GetVisualCreator(node, dropShadow).CreateVisual(context));
      group.Add(style.Renderer.GetVisualCreator(node, style).CreateVisual(context));

      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldVisual, INode node) {
      var style = GetNodeStyle(context.Zoom);

      var group = oldVisual as OrgChartNodeStyleVisualGroup;
      // If the old visual cannot be updated, create a new one (e.g. when the style changed due to the zoom level).
      if (group == null || group.Children.Count != 2 || group.style != style) {
        return CreateVisual(context, node);
      }

      var childVisual = group.Children[1];
      var newChildVisual = style.Renderer.GetVisualCreator(node, style).UpdateVisual(context, childVisual);
      if (childVisual != newChildVisual) {
        group.Children[1] = newChildVisual;
      }
      return group;
    }

    private class OrgChartNodeStyleVisualGroup : VisualGroup
    {
      public readonly INodeStyle style;

      public OrgChartNodeStyleVisualGroup(INodeStyle style) {
        this.style = style;
      }
    }

    private INodeStyle GetNodeStyle(double zoom) {
      foreach (var key in nodeStyles.Keys) {
        if (key.IsInRange(zoom)) {
          return nodeStyles[key];
        }
      }
      return VoidNodeStyle.Instance;
    }

    public void SetNodeStyle(DoubleRange range, INodeStyle style) {
      nodeStyles[range] = style;
      if (maxValue == null || range.Max > maxValue.Max) {
        maxValue = range;
      }
    }

    private INodeStyle GetMaxDetailNodeStyle() {
      try {
        return nodeStyles[maxValue];
      } catch {
        return null;
      }
    }

    public class DoubleRange
    {
      public DoubleRange(double min, double max) {
        Min = min;
        Max = max;
      }

      public double Min { get; set; }
      public double Max { get; set; }

      public bool IsInRange(double value) {
        return (value >= Min && value < Max);
      }
    }

    protected override object Lookup(INode node, Type type) {
      if (type == typeof(IClickHandler)) {
        return new MyClickHandler(node, this);
      }
      if (type == typeof (INodeInsetsProvider)) {
        var style = GetMaxDetailNodeStyle();
        if (style is INodeInsetsProvider) {
          return style;
        }
      } else if (type == typeof(INodeSizeConstraintProvider)) {
        var style = GetMaxDetailNodeStyle();
        if (style is INodeSizeConstraintProvider) {
          return style;
        }
      }
      return base.Lookup(node, type);
    }

    private sealed class MyClickHandler : IClickHandler, IHitTestable
    {
      private readonly INode node;
      private readonly OrgChartNodeStyle orgChartNodeStyle;

      public MyClickHandler(INode node, OrgChartNodeStyle orgChartNodeStyle) {
        this.node = node;
        this.orgChartNodeStyle = orgChartNodeStyle;
      }

      public IHitTestable HitTestable {
        get { return this; }
      }

      public void OnClicked(IInputModeContext context, PointD location) {
        INodeStyle nodeStyle = orgChartNodeStyle.GetNodeStyle(context.CanvasControl.Zoom);
        if (nodeStyle != null) {
          var clickHandler = nodeStyle.Renderer.GetContext(node, nodeStyle).Lookup<IClickHandler>();
          if (clickHandler != null && clickHandler.HitTestable.IsHit(context, location)) {
            clickHandler.OnClicked(context, location);
          }
        }
      }

      public bool IsHit(IInputModeContext context, PointD location) {
        INodeStyle nodeStyle = orgChartNodeStyle.GetNodeStyle(context.Zoom);
        if (nodeStyle != null) {
          var clickHandler = nodeStyle.Renderer.GetContext(node, nodeStyle).Lookup<IClickHandler>();
          return clickHandler != null && clickHandler.HitTestable.IsHit(context, location);
        }
        return false;
      }
    }

    protected override bool IsVisible(ICanvasContext context, RectD rectangle, INode node) {
      var nodeStyle = GetNodeStyle(context.Zoom);
      if (nodeStyle != null) {
        return nodeStyle.Renderer.GetVisibilityTestable(node, nodeStyle).IsVisible(context, rectangle);
      }
      return base.IsVisible(context, rectangle, node);
    }

    protected override bool IsInBox(IInputModeContext context, RectD box, INode node) {
      var nodeStyle = GetNodeStyle(context.Zoom);
      if (nodeStyle != null) {
        return nodeStyle.Renderer.GetMarqueeTestable(node, nodeStyle).IsInBox(context, box);
      }
      return base.IsInBox(context, box, node);
    }

    protected override PointD? GetIntersection(INode node, PointD inner, PointD outer) {
      INodeStyle maxDetailStyle = GetMaxDetailNodeStyle();
      if (maxDetailStyle != null) {
        return maxDetailStyle.Renderer.GetShapeGeometry(node, maxDetailStyle).GetIntersection(inner, outer);
      }
      return base.GetIntersection(node, inner, outer);
    }

    protected override bool IsInside(INode node, PointD point) {
      INodeStyle maxDetailStyle = nodeStyles[maxValue];
      if (maxDetailStyle != null) {
        return maxDetailStyle.Renderer.GetShapeGeometry(node, maxDetailStyle).IsInside(point);
      }
      return base.IsInside(node, point);
    }

    protected override GeneralPath GetOutline(INode node) {
      INodeStyle maxDetailStyle = nodeStyles[maxValue];
      if (maxDetailStyle != null) {
        return maxDetailStyle.Renderer.GetShapeGeometry(node, maxDetailStyle).GetOutline();
      }
      return base.GetOutline(node);
    }

    protected override bool IsHit(IInputModeContext context, PointD p, INode node) {
      var nodeStyle = GetNodeStyle(context.Zoom);
      if (nodeStyle != null) {
        return nodeStyle.Renderer.GetHitTestable(node, nodeStyle).IsHit(context, p);
      }
      return base.IsHit(context, p, node);
    }
  }

  public class ButtonDecoratorNodeStyle : NodeStyleBase<VisualGroup>
  {
    public ICollection<Button> Buttons { get; set; }
    public INodeStyle DecoratedStyle { get; set; }

    public ButtonDecoratorNodeStyle() {
      Buttons = new List<Button>();
    }

    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var group = new VisualGroup();

      group.Add(DecoratedStyle.Renderer.GetVisualCreator(node, DecoratedStyle).CreateVisual(context));

      foreach (var button in Buttons) {
        var oldLabel = button.Visualization;
        var label = new SimpleLabel(node, oldLabel.Text, oldLabel.LayoutParameter) {
          Style = oldLabel.Style,
          PreferredSize = oldLabel.PreferredSize
        };
        group.Add(label.Style.Renderer.GetVisualCreator(label, label.Style).CreateVisual(context));
      }

      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldGroup, INode node) {
      if (oldGroup == null || oldGroup.Children.Count != Buttons.Count + 1) {
        return CreateVisual(context, node);
      }

      var decoratedVisual = oldGroup.Children[0];
      var newDecoratedVisual = DecoratedStyle.Renderer.GetVisualCreator(node, DecoratedStyle).UpdateVisual(context, decoratedVisual);
      if (newDecoratedVisual != decoratedVisual) {
        oldGroup.Children[0] = newDecoratedVisual;
      }

      var i = 1;
      foreach (var button in Buttons) {
        var buttonVisual = oldGroup.Children[i];
        var oldLabel = button.Visualization;
        var label = new SimpleLabel(node, oldLabel.Text, oldLabel.LayoutParameter)
        {
          Style = oldLabel.Style,
          PreferredSize = oldLabel.PreferredSize
        };
        var newButtonVisual = label.Style.Renderer.GetVisualCreator(label, label.Style).UpdateVisual(context, buttonVisual);
        if (newButtonVisual != buttonVisual) {
          oldGroup.Children[i] = newButtonVisual;
        }
        i++;
      }

      return oldGroup;
    }

    protected override object Lookup(INode node, Type type) {
      if (type == typeof(IClickHandler)) {
        return new MyClickHandler(node, this);
      }
      return base.Lookup(node, type);
    }

    private sealed class MyClickHandler : IClickHandler, IHitTestable
    {
      private readonly INode node;
      private readonly ButtonDecoratorNodeStyle style;

      public MyClickHandler(INode node, ButtonDecoratorNodeStyle style) {
        this.node = node;
        this.style = style;
      }

      public IHitTestable HitTestable {
        get { return this; }
      }

      public void OnClicked(IInputModeContext context, PointD location) {
        foreach (var button in style.Buttons) {
          var label = CopyLabel(button.Visualization);
          if (label.Style.Renderer.GetHitTestable(label, label.Style).IsHit(context, location)) {
            button.Click(node, context.CanvasControl);
            return;
          }
        }
      }

      public bool IsHit(IInputModeContext context, PointD location) {
        foreach (var button in style.Buttons) {
          var label = CopyLabel(button.Visualization);
          if (label.Style.Renderer.GetHitTestable(label, label.Style).IsHit(context, location)) {
            return true;
          }
        }
        return false;
      }

      private SimpleLabel CopyLabel(ILabel label) {
        var newLabel = new SimpleLabel(node, label.Text, label.LayoutParameter) {
          Style = label.Style,
          PreferredSize = label.PreferredSize
        };
        return newLabel;
      }
    }
  }

  public sealed class Button
  {
    public static readonly object UseNodeParameter = new Object();
    public static readonly object UseNodeTagParameter = new Object();
    public static readonly Control UseCanvasControlTarget = new Control();

    public Button() {
      CommandTarget = UseCanvasControlTarget;
    }

    public ILabel Visualization { get; set; }
    public ICommand Command { get; set; }
    public object CommandParameter { get; set; }
    public Control CommandTarget { get; set; }

    public EventHandler<CanExecuteCommandEventArgs> CanExecuteHandler { get; set; }

    internal void Click(INode node, CanvasControl hostingControl) {
      var commandParameter = CommandParameter;
      if (commandParameter == UseNodeParameter) {
        commandParameter = node;
      } else if (commandParameter == UseNodeTagParameter) {
        commandParameter = node.Tag;
      }
      var commandTarget = CommandTarget;
      if (commandTarget == UseCanvasControlTarget) {
        commandTarget = hostingControl;
      }
      Command.Execute(commandParameter, commandTarget);
    }

    internal bool CanExecute(INode node, CanvasControl hostingControl) {
      var commandParameter = CommandParameter;
      if (commandParameter == UseNodeParameter) {
        commandParameter = node;
      } else if (commandParameter == UseNodeTagParameter) {
        commandParameter = node.Tag;
      }
      var commandTarget = CommandTarget;
      if (commandTarget == UseCanvasControlTarget) {
        commandTarget = hostingControl;
      }
      if (CanExecuteHandler != null) {
        var args = new CanExecuteCommandEventArgs(Command, commandParameter, commandTarget);
        CanExecuteHandler(commandTarget, args);
        if (args.Handled) {
          return args.CanExecute;
        }
      }
      return Command.CanExecute(commandParameter, commandTarget);
    }
  }

  public class DetailNodeStyle : NodeStyleBase<VisualGroup>
  {
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      // Fetch information for the child visuals
      var employee = (Employee) node.Tag;

      Color borderColor, backgroundColor1, backgroundColor2, ribbonColor;
      if (((GraphControl) context.CanvasControl).CurrentItem == node) {
        borderColor = Color.Orange;
        backgroundColor1 = Color.White;
        backgroundColor2 = Color.Orange;
      } else {
        borderColor = Color.FromArgb(255, 24, 154, 231);
        backgroundColor1 = Color.FromArgb(255, 204, 255, 255);
        backgroundColor2 = Color.FromArgb(255, 24, 154, 231);
      }

      if (employee.Status == EmployeeStatus.Travel) {
        ribbonColor = Color.Purple;
      } else if (employee.Status == EmployeeStatus.Unavailable) {
        ribbonColor = Color.Red;
      } else {
        ribbonColor = Color.Green;
      }

      var layout = node.Layout;

      var icon = GetIcon(employee.Icon);
      var iconScalingFactor = (layout.Height - 10d) / icon.Height;
      var iconWidth = icon.Width * iconScalingFactor;

      var nameText = new TextVisual {
        Text = employee.Name,
        Font = new Font("Arial", 13, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(iconWidth + 10, 10)
      };
      var positionText = new TextVisual {
        Text = employee.Position,
        Font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(iconWidth + 10, 35)
      };
      var emailText = new TextVisual {
        Text = employee.Email,
        Font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(iconWidth + 10, 50)
      };
      var phone1Text = new TextVisual {
        Text = employee.Phone,
        Font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(iconWidth + 10, 65)
      };
      var faxText = new TextVisual {
        Text = employee.Fax,
        Font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(iconWidth + 10, 80)
      };

      var border = new RectangleVisual(0, 0, layout.Width, layout.Height) {Pen = new Pen(borderColor)};
      var background = new RectangleVisual(0, 0, layout.Width, layout.Height) {
        Brush = new LinearGradientBrush(new PointF(0, 0), new PointF((float) layout.Width, (float) layout.Height), backgroundColor1, backgroundColor2)
      };
      var iconVisual = new ImageVisual {
        Image = icon,
        Rectangle = new RectD(5, 5, icon.Width * iconScalingFactor, icon.Height * iconScalingFactor)
      };

      IRectangle rect = new RectD(layout.Width - 30, 5, 25, 25);
      var circle1 = new EllipseVisual(rect) {Brush = new SolidBrush(ribbonColor)};
      IRectangle rect1 = new RectD(layout.Width - 25, 10, 15, 15);
      var circle2 = new EllipseVisual(rect1) {Brush = Brushes.White};
      IRectangle rect2 = new RectD(layout.Width - 20, 15, 5, 5);
      var circle3 = new EllipseVisual(rect2) {Brush = circle1.Brush};

      // Set a transform on the group, matching the node's location.
      // That way only the transform has to be updated instead of every single child visual.
      var transform = new Matrix();
      transform.Translate((float) layout.X, (float) layout.Y);

      var group = new VisualGroup {
        Transform = transform,
        Children = {
          border,
          background,
          iconVisual,
          nameText,
          positionText,
          emailText,
          phone1Text,
          faxText,
          circle1,
          circle2,
          circle3
        }
      };

      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup group, INode node) {
      if (group == null || group.Children.Count != 11) {
        return CreateVisual(context, node);
      }
      var layout = node.Layout;

      // Update the location of the visualization
      var transform = new Matrix();
      transform.Translate((float) layout.X, (float) layout.Y);
      group.Transform = transform;

      // Update the background if selected
      Color borderColor, backgroundColor1, backgroundColor2;
      if (((GraphControl) context.CanvasControl).CurrentItem == node) {
        borderColor = Color.Orange;
        backgroundColor1 = Color.White;
        backgroundColor2 = Color.Orange;
      } else {
        borderColor = Color.FromArgb(255, 24, 154, 231);
        backgroundColor1 = Color.FromArgb(255, 204, 255, 255);
        backgroundColor2 = Color.FromArgb(255, 24, 154, 231);
      }
      var border = (ShapeVisual) group.Children[0];
      border.Pen.Color = borderColor;
      var background = (ShapeVisual) group.Children[1];
      var gradient = (LinearGradientBrush) background.Brush;
      if (gradient.LinearColors[0] != backgroundColor1 || gradient.LinearColors[1] != backgroundColor2) {
        gradient.LinearColors = new[] { backgroundColor1, backgroundColor2 };
      }

      return group;
    }

    private static Image GetIcon(string value) {
      try {
        return Image.FromFile("Resources/" + value + ".png");
      } catch {
        return null;
      }
    }
  }

  public class IntermediateNodeStyle : NodeStyleBase<VisualGroup>
  {
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      Color borderColor, backgroundColor1, backgroundColor2;

      if (((GraphControl) context.CanvasControl).CurrentItem == node) {
        borderColor = Color.Orange;
        backgroundColor1 = Color.White;
        backgroundColor2 = Color.Orange;
      } else {
        borderColor = Color.FromArgb(255, 24, 154, 231);
        backgroundColor1 = Color.FromArgb(255, 204, 255, 255);
        backgroundColor2 = Color.FromArgb(255, 24, 154, 231);
      }

      var layout = node.Layout;

      var employee = (Employee) node.Tag;

      var text = new TextVisual {
        Text = employee.Name,
        Font = new Font("Arial", 29, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(5, 5)
      };
      var locationX = layout.Width / 2 - text.GetBounds(context).Width / 2;
      var locationY = layout.Height / 2 - text.GetBounds(context).Height / 2;
      text = new TextVisual {
        Text = employee.Name,
        Font = new Font("Arial", 29, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(locationX, locationY)
      };

      var ribbonColor = Color.Green;
      if (employee.Status == EmployeeStatus.Travel) {
        ribbonColor = Color.Purple;
      }
      if (employee.Status == EmployeeStatus.Unavailable) {
        ribbonColor = Color.Red;
      }

      var border = new RectangleVisual(0, 0, layout.Width, layout.Height) {Pen = new Pen(borderColor)};
      var background = new RectangleVisual(0, 0, layout.Width, layout.Height) {
        Brush = new LinearGradientBrush(new PointF(0, 0), new PointF((float) layout.Width, (float) layout.Height), backgroundColor1, backgroundColor2)
      };
      double x = layout.Width / 2 - 10;
      var ribbon = new RectangleVisual(x, 0, 20, 20) {Brush = new SolidBrush(ribbonColor)};

      // Set a transform on the group, matching the node's location.
      // That way only the transform has to be updated instead of every single child visual.
      var transform = new Matrix();
      transform.Translate((float) layout.X, (float) layout.Y);

      var group = new VisualGroup
      {
        Transform = transform,
        Children = {
          border, background, text, ribbon
        }
      };

      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup group, INode node) {
      if (group == null || group.Children.Count != 4) {
        return CreateVisual(context, node);
      }
      var layout = node.Layout;

      // Update the location of the visualization
      var transform = new Matrix();
      transform.Translate((float) layout.X, (float) layout.Y);
      group.Transform = transform;

      // Update the background if selected
      Color borderColor, backgroundColor1, backgroundColor2;
      if (((GraphControl) context.CanvasControl).CurrentItem == node) {
        borderColor = Color.Orange;
        backgroundColor1 = Color.White;
        backgroundColor2 = Color.Orange;
      } else {
        borderColor = Color.FromArgb(255, 24, 154, 231);
        backgroundColor1 = Color.FromArgb(255, 204, 255, 255);
        backgroundColor2 = Color.FromArgb(255, 24, 154, 231);
      }
      var border = (ShapeVisual) group.Children[0];
      border.Pen.Color = borderColor;
      var background = (ShapeVisual) group.Children[1];
      var gradient = (LinearGradientBrush) background.Brush;
      if (gradient.LinearColors[0] != backgroundColor1 || gradient.LinearColors[1] != backgroundColor2) {
        gradient.LinearColors = new[] { backgroundColor1, backgroundColor2 };
      }

      return group;
    }
  }

  public class OverviewNodeStyle : NodeStyleBase<VisualGroup>
  {
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      Color borderColor, backgroundColor1, backgroundColor2;

      if (((GraphControl) context.CanvasControl).CurrentItem == node) {
        borderColor = Color.Orange;
        backgroundColor1 = Color.White;
        backgroundColor2 = Color.Orange;
      } else {
        borderColor = Color.FromArgb(255, 24, 154, 231);
        backgroundColor1 = Color.FromArgb(255, 204, 255, 255);
        backgroundColor2 = Color.FromArgb(255, 24, 154, 231);
      }

      var layout = node.Layout;

      var employee = (Employee) node.Tag;

      var text = new TextVisual {
        Text = GetShortName(employee.Name),
        Font = new Font("Arial", 37, FontStyle.Regular, GraphicsUnit.Pixel),
        Brush = Brushes.Black,
        Location = new PointD(5, 5)
      };
      double locationX = layout.Width / 2 - text.GetBounds(context).Width / 2;
      double locationY = layout.Height / 2 - text.GetBounds(context).Height / 2;
      string s1 = GetShortName(employee.Name);
      Font font1 = new Font("Arial", 37, FontStyle.Regular, GraphicsUnit.Pixel);
      Brush brush1 = Brushes.Black;
      IPoint location1 = new PointD(locationX, locationY);
      text = new TextVisual {Text = s1, Font = font1, Brush = brush1, Location = location1};

      var ribbonColor = Color.Green;
      if (employee.Status == EmployeeStatus.Travel) {
        ribbonColor = Color.Purple;
      }
      if (employee.Status == EmployeeStatus.Unavailable) {
        ribbonColor = Color.Red;
      }

      var border = new RectangleVisual(0, 0, layout.Width, layout.Height) {Pen = new Pen(borderColor)};
      var background = new RectangleVisual(0, 0, layout.Width, layout.Height) {
        Brush = new LinearGradientBrush(new PointF(0, 0), new PointF((float) layout.Width, (float) layout.Height), backgroundColor1, backgroundColor2)
      };
      var ribbonPath = new GeneralPath();
      ribbonPath.MoveTo(0, 20);
      ribbonPath.LineTo(25, 0);
      ribbonPath.LineTo(40, 0);
      ribbonPath.LineTo(0, 35);
      ribbonPath.Close();
      var ribbon = new GeneralPathVisual(ribbonPath) {Brush = new SolidBrush(ribbonColor)};

      // Set a transform on the group, matching the node's location.
      // That way only the transform has to be updated instead of every single child visual.
      var transform = new Matrix();
      transform.Translate((float) layout.X, (float) layout.Y);

      var group = new VisualGroup
      {
        Transform = transform,
        Children = {
          border, background, text, ribbon
        }
      };

      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup oldVisual, INode node) {
      var group = oldVisual as VisualGroup;
      if (group == null || group.Children.Count != 4) {
        return CreateVisual(context, node);
      }
      var layout = node.Layout;

      // Update the location of the visualization
      var transform = new Matrix();
      transform.Translate((float) layout.X, (float) layout.Y);
      group.Transform = transform;

      // Update the background if selected
      Color borderColor, backgroundColor1, backgroundColor2;
      if (((GraphControl) context.CanvasControl).CurrentItem == node) {
        borderColor = Color.Orange;
        backgroundColor1 = Color.White;
        backgroundColor2 = Color.Orange;
      } else {
        borderColor = Color.FromArgb(255, 24, 154, 231);
        backgroundColor1 = Color.FromArgb(255, 204, 255, 255);
        backgroundColor2 = Color.FromArgb(255, 24, 154, 231);
      }
      var border = (ShapeVisual) group.Children[0];
      border.Pen.Color = borderColor;
      var background = (ShapeVisual) group.Children[1];
      var gradient = (LinearGradientBrush) background.Brush;
      if (gradient.LinearColors[0] != backgroundColor1 || gradient.LinearColors[1] != backgroundColor2) {
        gradient.LinearColors = new[] { backgroundColor1, backgroundColor2 };
      }

      return group;
    }

    private static string GetShortName(string name) {
      var names = name.Split(' ');
      return names.Length > 1
        ? names[0].Substring(0, 1) + ". " + names[names.Length - 1]
        : names[0];
    }
  }

  public class ButtonLabelStyle : LabelStyleBase<IVisual>
  {
    public ButtonLabelStyle() {
      BackgroundColor = Color.White;
      ForegroundColor = Color.Black;
    }

    public Color BackgroundColor { get; set; }
    public Color ForegroundColor { get; set; }
    public ButtonIcon Icon { get; set; }
    public Button Button { get; set; }

    protected override IVisual CreateVisual(IRenderContext context, ILabel label) {
      return new ButtonVisual(label, Button, Icon, BackgroundColor, ForegroundColor);
    }

    private class ButtonVisual : IVisual
    {
      private readonly ILabel label;
      private IOrientedRectangle labelLayout;
      private readonly Button button;
      private readonly ButtonIcon icon;
      private readonly Color backgroundColor;
      private readonly Color foregroundColor;

      public ButtonVisual(ILabel label, Button button, ButtonIcon icon, Color backgroundColor, Color foregroundColor) {
        this.label = label;
        this.labelLayout = label.LayoutParameter.Model.GetGeometry(label, label.LayoutParameter);
        this.button = button;
        this.icon = icon;
        this.backgroundColor = backgroundColor;
        this.foregroundColor = foregroundColor;
      }

      void IVisual.Paint(IRenderContext context, Graphics graphics) {
        bool enabled = false;
        if (button != null) {
          enabled = button.CanExecute((INode) label.Owner, context.CanvasControl);
        }

        var layout = new RectD(labelLayout.AnchorX, labelLayout.AnchorY - label.PreferredSize.Height,
          label.PreferredSize.Width, label.PreferredSize.Height);
        Brush backgroundBrush;
        Pen foregroundPen;
        if (enabled) {
          // enabled style
          backgroundBrush = new LinearGradientBrush(layout.TopLeft, layout.BottomLeft,
            Mix(Color.White, backgroundColor, 0.5d), backgroundColor);
          foregroundPen = new Pen(foregroundColor);
        } else {
          // disabled style
          backgroundBrush = new LinearGradientBrush(layout.TopLeft, layout.BottomLeft,
            Mix(Color.White, backgroundColor, 0.7),
            Mix(Color.White, backgroundColor, 0.7));
          foregroundPen = new Pen(Mix(Color.White, foregroundColor, 0.7));
        }
        graphics.FillEllipse(backgroundBrush, layout.ToRectangleF());
        GraphicsPath path;
        switch (icon) {
          case ButtonIcon.ShowParent:
            path = new GraphicsPath(
              new PointF[] {
                layout.TopLeft + new PointD(layout.Width * 0.3, layout.Height * 0.7),
                layout.TopLeft + new PointD(layout.Width * 0.7, layout.Height * 0.7),
                layout.TopLeft + new PointD(layout.Width * 0.5, layout.Height * 0.3)
              },
              new[] { (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line });
            path.CloseAllFigures();
            graphics.DrawPath(foregroundPen, path);
            break;
          case ButtonIcon.HideParent:
            path = new GraphicsPath(
              new PointF[] {
                layout.TopLeft + new PointD(layout.Width * 0.3, layout.Height * 0.3),
                layout.TopLeft + new PointD(layout.Width * 0.7, layout.Height * 0.3),
                layout.TopLeft + new PointD(layout.Width * 0.5, layout.Height * 0.7)
              },
              new[] { (byte) PathPointType.Start, (byte) PathPointType.Line, (byte) PathPointType.Line });
            path.CloseAllFigures();
            graphics.DrawPath(foregroundPen, path);
            break;
          case ButtonIcon.ShowChildren:
            graphics.DrawLine(foregroundPen, layout.TopLeft + new PointD(layout.Width * 0.3, layout.Height * 0.5),
              layout.TopLeft + new PointD(layout.Width * 0.7, layout.Height * 0.5));
            graphics.DrawLine(foregroundPen, layout.TopLeft + new PointD(layout.Width * 0.5, layout.Height * 0.3),
              layout.TopLeft + new PointD(layout.Width * 0.5, layout.Height * 0.7));
            break;
          case ButtonIcon.HideChildren:
            graphics.DrawLine(foregroundPen, layout.TopLeft + new PointD(layout.Width * 0.3, layout.Height * 0.5),
              layout.TopLeft + new PointD(layout.Width * 0.7, layout.Height * 0.5));
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      ///<summary>
      /// Mixes two colors using the provided ratio.
      ///</summary>
      private static Color Mix(Color color0, Color color1, double ratio) {
        double iratio = 1 - ratio;
        double a = color0.A * ratio + color1.A * iratio;
        double r = color0.R * ratio + color1.R * iratio;
        double g = color0.G * ratio + color1.G * iratio;
        double b = color0.B * ratio + color1.B * iratio;
        return
            Color.FromArgb((int) Math.Round(a), (int) Math.Round(r),
              (int) Math.Round(g), (int) Math.Round(b));
      }
    }

    protected override SizeD GetPreferredSize(ILabel label) {
      return label.PreferredSize;
    }

    public enum ButtonIcon
    {
      None,
      ShowParent,
      HideParent,
      ShowChildren,
      HideChildren,
    }
    
  }
}
