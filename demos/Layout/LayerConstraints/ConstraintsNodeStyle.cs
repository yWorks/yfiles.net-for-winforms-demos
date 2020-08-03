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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Layout.LayerConstraints
{
  /// <summary>
  /// An <see cref="INodeStyle"/> implementation used to display and edit layout constraints.
  /// </summary>
  /// <remarks>
  /// This style will add buttons to decrease/increase and disable/enable layout constraints for the specific node. It can only
  /// handle nodes whose <see cref="ITagOwner.Tag"/> is set to be an instance of <see cref="LayerConstraintsInfo"/>.
  /// </remarks>
  class ConstraintsNodeStyle : NodeStyleBase<VisualGroup>
  {
    #region Properties and fields

    /// <summary>
    /// The decorated style.
    /// </summary>
    public INodeStyle DecoratedStyle { get; set; }

    /// <summary>
    /// The buttons to be displayed on the node.
    /// </summary>
    private readonly ICollection<Button> buttons;

    /// <summary>
    /// The font used to display the textual representation of the layout constraint.
    /// </summary>
    private readonly Font font = new Font(FontFamily.GenericSansSerif, 11);

    /// <summary>
    /// The font used to display the textual representation of the layout constraint.
    /// </summary>
    private readonly Font labelFont = new Font(FontFamily.GenericSansSerif, 9);

    /// <summary>
    /// The insets used by the label and buttons
    /// </summary>
    private readonly InsetsD insets = new InsetsD(5);

    /// <summary>
    /// The style used by the toggle state button.
    /// </summary>
    private ButtonLabelStyle toggleStateStyle;

    /// <summary>
    /// The default size for buttons
    /// </summary>
    private const int ButtonSize = 14;

    private readonly SimpleNode node;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstraintsNodeStyle"/> class.
    /// </summary>
    public ConstraintsNodeStyle() {
      buttons = new List<Button>();
      DecoratedStyle = new ShapeNodeStyle
                         {
                           Brush =
                             new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.White,
                                                     Color.FromArgb(255, 221, 221, 255)),
                           Shape = ShapeNodeShape.Rectangle,
                           Pen = new Pen(Color.Gray, 2)
                         };
      AddDecoratorButtons();
      node = new SimpleNode();
    }
    
    protected override VisualGroup CreateVisual(IRenderContext context, INode node1) {      
      var group = new VisualGroup();
      group.Transform = new Matrix(1, 0, 0, 1, (float) node1.Layout.X, (float) node1.Layout.Y);
      var data = (LayerConstraintsInfo)node1.Tag;
      node.Layout = new RectD(PointD.Origin, node1.Layout.GetSize());
      node.Tag = data;

      var x = (float)insets.Left;
      var y = (float)insets.Top;
      var width = (float)(node.Layout.Width - insets.HorizontalInsets - ButtonSize);
      //var height = (float)(font.GetHeight(graphics) + insets.VerticalInsets);
      var height = ButtonSize * 2;

      // add background
      group.Add(DecoratedStyle.Renderer.GetVisualCreator(node, DecoratedStyle).CreateVisual(context));

      // paint label
      if (data.Constraints) {
        var rectangle = new RectangleVisual(x, y, width, height) {Brush = GetBackgroundBrush(data)};
        group.Add(rectangle);
        group.Add(new TextVisual {
          Text = data.ToString(),
          Font = font,
          Brush = GetForegroundBrush(data),
          Location = new PointD(x + insets.Left, y + insets.Top)
        });
      } else {
        group.Add(VoidVisualCreator.Instance.CreateVisual(context));
        group.Add(VoidVisualCreator.Instance.CreateVisual(context));
      }

      // paint buttons
      toggleStateStyle.Icon = data.Constraints ? ButtonLabelStyle.ButtonIcon.Toggle : ButtonLabelStyle.ButtonIcon.None;
      toggleStateStyle.BackgroundColor = data.Constraints ? Color.Green : Color.Gray;
      foreach (var button in buttons) {
        ILabel buttonLabel = button.Visualization;
        var icon = ((ButtonLabelStyle)buttonLabel.Style).Icon;
        if (data.Constraints || icon == ButtonLabelStyle.ButtonIcon.Toggle || icon == ButtonLabelStyle.ButtonIcon.None) {
          SimpleLabel label = new SimpleLabel(node, buttonLabel.Text, buttonLabel.LayoutParameter) {
            Style = buttonLabel.Style,
            PreferredSize = buttonLabel.PreferredSize
          };
          group.Add(label.Style.Renderer.GetVisualCreator(label, label.Style).CreateVisual(context));
        } else {
          group.Add(VoidVisualCreator.Instance.CreateVisual(context));
        }
      }

      if (data.Constraints) {
        double w = width + ButtonSize;
        var rectangle = new RectangleVisual(x, y, w, height) {Pen = Pens.Black};
        group.Add(rectangle);
        var line = new LineVisual(x + width, y, x + width, y + height) {Pen = Pens.Black};
        group.Add(line);
      } else {
        group.Add(VoidVisualCreator.Instance.CreateVisual(context));
        group.Add(VoidVisualCreator.Instance.CreateVisual(context));
      }

      group.Add(new TextVisual {
        Text = data.Constraints ? "Enabled" : "Disabled",
        Font = labelFont,
        Brush = Brushes.Black,
        Location = new PointD(x + ButtonSize + 1, y + (float) node.Layout.Height + 1 - (float) insets.Bottom - font.Height - ButtonSize * 0.2f)
      });
      return group;
    }

    protected override VisualGroup UpdateVisual(IRenderContext context, VisualGroup group, INode node1) {
      if (group == null || group.Children.Count != 6 + buttons.Count) {
        return CreateVisual(context, node1);
      }
      group.Transform = new Matrix(1, 0, 0, 1, (float) node1.Layout.X, (float) node1.Layout.Y);
      var data = (LayerConstraintsInfo)node1.Tag;
      node.Tag = data;

      var x = (float)insets.Left;
      var y = (float)insets.Top;
      var width = (float)(node.Layout.Width - insets.HorizontalInsets - ButtonSize);
      //var height = (float)(font.GetHeight(graphics) + insets.VerticalInsets);
      var height = ButtonSize * 2;

      // update background
      group.Children[0] = DecoratedStyle.Renderer.GetVisualCreator(node, DecoratedStyle).UpdateVisual(context, group.Children[0]);

      // update labels
      if (data.Constraints) {
        if (group.Children[1] == null) {
          var rectangle = new RectangleVisual(x, y, width, height) {Brush = GetBackgroundBrush(data)};
          group.Children[1] = rectangle;
          string s = data.ToString();
          Brush brush = GetForegroundBrush(data);
          IPoint location = new PointD(x + insets.Left, y + insets.Top);
          group.Children[2] = new TextVisual {Text = s, Font = font, Brush = brush, Location = location};
        } else {
          ((ShapeVisual) group.Children[1]).Brush = GetBackgroundBrush(data);
          var textPaintable = (TextVisual) group.Children[2];
          textPaintable.Brush = GetForegroundBrush(data);
          textPaintable.Text = data.ToString();
        }
      } else {
        if (group.Children[1] != null) {
          group.Children[1] = VoidVisualCreator.Instance.CreateVisual(context);
          group.Children[2] = VoidVisualCreator.Instance.CreateVisual(context);
        }
      }

      // paint buttons
      toggleStateStyle.Icon = data.Constraints ? ButtonLabelStyle.ButtonIcon.Toggle : ButtonLabelStyle.ButtonIcon.None;
      toggleStateStyle.BackgroundColor = data.Constraints ? Color.Green : Color.Gray;
      int childIndex = 3;
      foreach (var button in buttons) {
        ILabel oldLabel = button.Visualization;
        var icon = ((ButtonLabelStyle)button.Visualization.Style).Icon;
        if (data.Constraints || icon == ButtonLabelStyle.ButtonIcon.Toggle || icon == ButtonLabelStyle.ButtonIcon.None) {
          SimpleLabel label = new SimpleLabel(node, oldLabel.Text, oldLabel.LayoutParameter)
          {
            Style = oldLabel.Style,
            PreferredSize = oldLabel.PreferredSize
          };
          group.Children[childIndex] = label.Style.Renderer.GetVisualCreator(label, label.Style).UpdateVisual(context, group.Children[childIndex]);
        } else {
          group.Children[childIndex] = VoidVisualCreator.Instance.CreateVisual(context);
        }
        childIndex++;
      }

      if (data.Constraints) {
        if (group.Children[childIndex] == null) {
          double w = width + ButtonSize;
          var rectangle = new RectangleVisual(x, y, w, height) {Pen = Pens.Black};
          group.Children[childIndex] = rectangle;
          var line = new LineVisual(x + width, y, x + width, y + height) {Pen = Pens.Black};
          group.Children[childIndex + 1] = line;
        }
      } else {
        if (group.Children[childIndex] != null) {
          group.Children[childIndex] = VoidVisualCreator.Instance.CreateVisual(context);
          group.Children[childIndex + 1] = VoidVisualCreator.Instance.CreateVisual(context);
        }
      }
      childIndex += 2;

      ((TextVisual) group.Children[childIndex]).Text = data.Constraints ? "Enabled" : "Disabled";
      return group;
    }

    #region IClickHandler implementation

    /// <summary>
    /// Performs the <see cref="ILookup.Lookup"/> operation for
    /// the <see cref="INodeStyleRenderer.GetContext"/>.
    /// </summary>
    /// <param name="node">The node to use for the context lookup.</param>
    /// <param name="type">The type to query.</param>
    /// <returns>
    /// An implementation of the <paramref name="type"/> or <see langword="null"/>.
    /// </returns>
    protected override object Lookup(INode node, Type type) {
      if (type == typeof(IClickHandler)) {
        return new MyClickHandler(node, this);
      } else {
        return base.Lookup(node, type);
      }
    }

    /// <summary>
    /// Handles interaction with the buttons.
    /// </summary>
    private sealed class MyClickHandler : IClickHandler, IHitTestable
    {
      private readonly INode node;
      private readonly ConstraintsNodeStyle style;

      /// <summary>
      /// Initializes a new instance of the <see cref="MyClickHandler"/> class.
      /// </summary>
      /// <param name="node">The node.</param>
      /// <param name="style">The style.</param>
      public MyClickHandler(INode node, ConstraintsNodeStyle style) {
        this.node = node;
        this.style = style;
      }


      public IHitTestable HitTestable {
        get { return this; }
      }

      public void OnClicked(IInputModeContext context, PointD location) {
        foreach (var button in style.buttons) {
          SimpleLabel label = CopyLabel(button.Visualization);
          if (label.Style.Renderer.GetHitTestable(label, label.Style).IsHit(context, location)) {
            button.Click(node, context.CanvasControl);
            return;
          }
        }
      }

      /// <summary>
      /// Determines if something has been hit at the given coordinates
      /// in the world coordinate system.
      /// </summary>
      /// <param name="p">the coordinates in world coordinate system</param>
      /// <param name="ctx">the context the hit test is performed in</param>
      /// <returns>
      /// whether something has been hit
      /// </returns>
      public bool IsHit(IInputModeContext ctx, PointD p) {
        foreach (var button in style.buttons) {
          SimpleLabel label = CopyLabel(button.Visualization);
          if (label.Style.Renderer.GetHitTestable(label, label.Style).IsHit(ctx, p)) {
            return true;
          }
        }
        return false;
      }

      /// <summary>
      /// Copies the label with its model parameter, text, owner, style and size.
      /// </summary>
      /// <param name="label">The label.</param>
      /// <returns></returns>
      private SimpleLabel CopyLabel(ILabel label) {
        SimpleLabel newLabel = new SimpleLabel(node, label.Text, label.LayoutParameter) { Style = label.Style, PreferredSize = label.PreferredSize };
        return newLabel;
      }
    }

    #endregion

    #region Data Converters

    /// <summary>
    /// Gets the background brush.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    private Brush GetBackgroundBrush(LayerConstraintsInfo data) {
      switch (data.Value) {
        case 0:
          return Brushes.Black;
        case 7:
          return Brushes.White;
        default:
          int v = data.Value;
          return new SolidBrush(Color.FromArgb((byte)((v * 255) / 7), (byte)((v * 255) / 7), 255));
      }
    }

    /// <summary>
    /// Gets the foreground brush.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    private Brush GetForegroundBrush(LayerConstraintsInfo data) {
      return data.Value < 4 ? Brushes.White : Brushes.Black;
    }

    #endregion

    /// <summary>
    /// Adds the buttons to the <see cref="ConstraintsNodeStyle" />.
    /// </summary>
    private void AddDecoratorButtons() {
      // increase button
      var b1 = new Button
      {
        Command = LayerConstraintsForm.IncreaseLayerCommand,
        CommandParameter = Button.UseNodeParameter,
        CommandTarget = Button.UseCanvasControlTarget,
        // set a label as the button visualization
        Visualization = CreateButtonLabel(ButtonLabelStyle.ButtonIcon.Increase, ButtonSize,
          new PointD(1, 0),
          new PointD(-insets.Right - ButtonSize, insets.Top), null)
      };
      // set ButtonLabelStyle.Button so the style knows it's owner
      ((ButtonLabelStyle)b1.Visualization.Style).Button = b1;
      buttons.Add(b1);

      // decrease button
      var b2 = new Button
      {
        Command = LayerConstraintsForm.DecreaseLayerCommand,
        CommandParameter = Button.UseNodeParameter,
        CommandTarget = Button.UseCanvasControlTarget,
        // set a label as the button visualization
        Visualization = CreateButtonLabel(ButtonLabelStyle.ButtonIcon.Decrease, ButtonSize,
          new PointD(1, 0),
          new PointD(-insets.Right - ButtonSize, insets.Top + ButtonSize), null)
      };
      // set ButtonLabelStyle.Button so the style knows it's owner
      ((ButtonLabelStyle)b2.Visualization.Style).Button = b2;
      buttons.Add(b2);

      // toggle state button
      var b3 = new Button
      {
        Command = LayerConstraintsForm.ToggleConstraintsStateCommand,
        CommandParameter = Button.UseNodeParameter,
        CommandTarget = Button.UseCanvasControlTarget,
        // set a label as the button visualization
        Visualization = CreateButtonLabel(ButtonLabelStyle.ButtonIcon.Toggle, ButtonSize,
          new PointD(0, 1),
          new PointD(insets.Left, -insets.Bottom-ButtonSize), Pens.Black)
      };
      // set ButtonLabelStyle.Button so the style knows it's owner
      ((ButtonLabelStyle)b3.Visualization.Style).Button = b3;
      buttons.Add(b3);
      toggleStateStyle = (ButtonLabelStyle) b3.Visualization.Style;
    }

    /// <summary>
    /// Creates a button label.
    /// </summary>
    /// <param name="icon">The icon.</param>
    /// <param name="buttonSize">Size of the button.</param>
    /// <param name="anchor">The anchor is the relative position on the node.
    /// Valid values are between 0 and 1 for each of the points coordinates, where 0 means the top/left and 1 means bottom/right.</param>
    /// <param name="offset">The offset from the specified anchor point. This must be specified in absolute values.</param>
    /// <param name="borderPen">The pen for the border</param>
    /// <returns></returns>
    private ILabel CreateButtonLabel(ButtonLabelStyle.ButtonIcon icon, int buttonSize, PointD anchor, PointD offset, Pen borderPen) {
      return new SimpleLabel(null, "", new FreeNodeLabelModel().CreateParameter(anchor, offset, PointD.Origin, PointD.Origin, 0))
        {
          // style the label
          Style = new ButtonLabelStyle
                    {
                      BackgroundColor = Color.Black,
                      ForegroundColor = Color.White,
                      Icon = icon,
                      BorderPen = borderPen
                    },
          PreferredSize = new SizeD(buttonSize, buttonSize)
        };
    }
  }

  /// <summary>
  /// A Button for the <see cref="ConstraintsNodeStyle"/>.
  /// </summary>
  public sealed class Button
  {
    /// <summary>
    /// The event parameter will contain the node.
    /// </summary>
    public static readonly object UseNodeParameter = new Object();

    /// <summary>
    /// The event parameter will contain the nodes tag.
    /// </summary>
    public static readonly object UseNodeTagParameter = new Object();

    /// <summary>
    /// The event source will be the <see cref="CanvasControl"/>.
    /// </summary>
    /// <remarks>
    /// This is the default setting.
    /// </remarks>
    public static readonly Control UseCanvasControlTarget = new Control();

    public Button() {
      CommandTarget = UseCanvasControlTarget;
    }

    public ILabel Visualization { get; set; }
    public ICommand Command { get; set; }
    public object CommandParameter { get; set; }
    public Control CommandTarget { get; set; }

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
      return Command.CanExecute(commandParameter, commandTarget);
    }
  }

  /// <summary>
  /// An <see cref="ILabelStyle"/> implementation that displays like a small round button and contains an icon.
  /// </summary>
  public class ButtonLabelStyle : LabelStyleBase<VisualGroup>
  {
    public ButtonLabelStyle() {
      BackgroundColor = Color.White;
      ForegroundColor = Color.Black;
    }

    public Color BackgroundColor { get; set; }
    public Color ForegroundColor { get; set; }
    public ButtonIcon Icon { get; set; }
    public Button Button { get; set; }

    public Pen BorderPen { get; set; }

    protected override VisualGroup CreateVisual(IRenderContext context, ILabel label) {
      var group = new VisualGroup();
      bool enabled = false;
      if (Button != null) {
        enabled = Button.CanExecute((INode) label.Owner, context.CanvasControl);
      }

      var labelLayout = label.GetLayout();
      var layout = new RectD(labelLayout.AnchorX, labelLayout.AnchorY - label.PreferredSize.Height,
          label.PreferredSize.Width, label.PreferredSize.Height);
      Brush backgroundBrush;
      Brush foregroundBrush;
      Pen foregroundPen;
      if (enabled) {
        // enabled style
        if (Icon != ButtonIcon.Increase) {
          backgroundBrush = new LinearGradientBrush(layout.TopLeft, layout.BottomLeft,
              BackgroundColor,
              Mix(Color.White, BackgroundColor, 0.5d));
        } else {
          backgroundBrush = new LinearGradientBrush(layout.TopLeft, layout.BottomLeft,
              Mix(Color.White, BackgroundColor, 0.5d),
              BackgroundColor);
        }
        foregroundPen = new Pen(ForegroundColor);
        foregroundBrush = new SolidBrush(ForegroundColor);
      } else {
        // disabled style
        backgroundBrush = new LinearGradientBrush(layout.TopLeft, layout.BottomLeft,
            Mix(Color.White, BackgroundColor, 0.7),
            Mix(Color.White, BackgroundColor, 0.7));
        foregroundPen = new Pen(Mix(Color.White, ForegroundColor, 0.7));
        foregroundBrush = new SolidBrush(Mix(Color.White, ForegroundColor, 0.7));
      }
      var backgroundRect = new RectangleVisual(layout) {
        Brush = backgroundBrush,
        Pen = BorderPen
      };
      group.Add(backgroundRect);
      GeneralPath path;
      ShapeVisual pathPaintable;
      switch (Icon) {
        case ButtonIcon.Increase: // paint "up"-arrow
          path = new GeneralPath();
          path.MoveTo(layout.TopLeft + new PointD(layout.Width*0.3, layout.Height*0.7));
          path.LineTo(layout.TopLeft + new PointD(layout.Width*0.7, layout.Height*0.7));
          path.LineTo(layout.TopLeft + new PointD(layout.Width*0.5, layout.Height*0.3));
          path.Close();
          pathPaintable = new GeneralPathVisual(path) {Brush = foregroundBrush};
          group.Add(pathPaintable);
          break;
        case ButtonIcon.Decrease: // paint "down"-arrow
          path = new GeneralPath();
          path.MoveTo(layout.TopLeft + new PointD(layout.Width*0.3, layout.Height*0.3));
          path.LineTo(layout.TopLeft + new PointD(layout.Width*0.7, layout.Height*0.3));
          path.LineTo(layout.TopLeft + new PointD(layout.Width*0.5, layout.Height*0.7));
          path.Close();
          pathPaintable = new GeneralPathVisual(path) {Brush = foregroundBrush};
          group.Add(pathPaintable);
          break;
        case ButtonIcon.Toggle: // paint "check"
          path = new GeneralPath();
          path.MoveTo(layout.TopLeft + new PointD(layout.Width*0.3, layout.Height*0.5));
          path.LineTo(layout.TopLeft + new PointD(layout.Width*0.5, layout.Height*0.7));
          path.LineTo(layout.TopLeft + new PointD(layout.Width*0.7, layout.Height*0.3));;
          pathPaintable = new GeneralPathVisual(path) {Pen = foregroundPen};
          group.Add(pathPaintable);
          break;
        case ButtonIcon.None: // paint nothing
          break;
        default: // can't happen
          throw new ArgumentOutOfRangeException();
      }
      return group;
    }

    ///<summary>
    /// Mixes two colors using the provided ratio.
    ///</summary>
    private static Color Mix(Color color0, Color color1, double ratio) {
      double iratio = 1 - ratio;
      double a = color0.A*ratio + color1.A*iratio;
      double r = color0.R*ratio + color1.R*iratio;
      double g = color0.G*ratio + color1.G*iratio;
      double b = color0.B*ratio + color1.B*iratio;
      return
          Color.FromArgb((int) Math.Round(a), (int) Math.Round(r),
              (int) Math.Round(g), (int) Math.Round(b));
    }


    protected override SizeD GetPreferredSize(ILabel label) {
      return label.PreferredSize;
    }

    public enum ButtonIcon
    {
      None,
      Increase,
      Decrease,
      Toggle
    }
  }
}
