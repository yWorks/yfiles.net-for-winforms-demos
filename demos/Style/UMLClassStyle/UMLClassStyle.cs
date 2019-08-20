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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Markup;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

[assembly: XmlnsDefinition("http://www.yworks.com/yfiles.net/4.0/demos/UMLClassStyle", "Demo.yFiles.Graph.UMLClassStyle")]
[assembly: XmlnsPrefix("http://www.yworks.com/yfiles.net/4.0/demos/UMLClassStyle", "demo")]

namespace Demo.yFiles.Graph.UMLClassStyle 
{
  
  /// <summary>
  /// Sample NodeStyle that can be used to represent a UML class. 
  /// </summary>
  /// <remarks>
  /// In order to use this style,
  /// <c>Item.Lookup&lt;ClassInfo&gt;()</c> has to return a <see cref="ClassInfo"/> instance, that encapsulates 
  /// UML information for the rendered node.
  /// </remarks>
  public class UMLClassStyle : NodeStyleBase<VisualGroup>
  {
    public static readonly ICommand AdjustNodeBoundsCommand = Commands.CreateCommand("Adjust Node Bounds");

    private static readonly Image imageUp2 = Properties.Resources.navigate_up2;
    private static readonly Image imageDown2 = Properties.Resources.navigate_down2;
    private static readonly Image imageUp = Properties.Resources.navigate_up2;
    private static readonly Image imageDown = Properties.Resources.navigate_down2;
    private static readonly Image imagePrivate = Properties.Resources.pill_red;
    private static readonly Image imageProtected = Properties.Resources.pill_blue;
    private static readonly Image imagePublic = Properties.Resources.pill_green;
    private readonly Font  featureFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular, GraphicsUnit.Pixel);
    private readonly Brush featureBrush = Brushes.Black;
    private readonly Font  sectionFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold, GraphicsUnit.Pixel);
    private readonly Brush sectionBrush = Brushes.LightBlue;
    private readonly Brush sectionTextBrush = Brushes.Black;
    private readonly Pen outlinePen = Pens.DarkGray;
    private const float ImageSize = 10f;

    private readonly Font titleFont = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold, GraphicsUnit.Pixel);
    private readonly Brush titleBrush = Brushes.Black;
    private readonly Font typeFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular, GraphicsUnit.Pixel);
    private readonly Brush typeBrush = Brushes.Black;


    #region the actual rendering

    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      var layout = node.Layout.ToRectD();
      var classInfo = node.Tag as ClassInfo;
      if (layout.Size.IsEmpty || classInfo == null) {
        return null;
      }
      var box = layout.ToRectD();
      if (box.IsEmpty) return null;

      var group = new VisualGroup();
      // draw the shadow
      var shadowBox = box.GetTranslated(new PointD(4, 4));
      group.Add(new RoundedRectangleVisual(shadowBox, 7){Brush = Brushes.Beige});
      // draw the background
      group.Add(new RoundedRectangleVisual(layout, 7) {Brush = Brushes.White});

      RectD currentBox = box;
      currentBox = DrawHeader(context, group, classInfo, currentBox);

      // if the node is expanded
      if (classInfo.ShowDetails) {
        // show the Fields, Properties, and Methods section
        // the DrawSection methods handle their own expanded/collapsed state
        currentBox = DrawSection(context, group, classInfo.Fields, "Fields", currentBox);
        currentBox = DrawSection(context, group, classInfo.Properties, "Properties", currentBox);
        DrawSection(context, group, classInfo.Methods, "Methods", currentBox);
      }

      // draw the outline
      group.Add(new RoundedRectangleVisual(layout, 7) {Pen = outlinePen});

      // add a dashed line as focus indicator if the node is the current item
      if (context.CanvasControl is GraphControl && ((GraphControl)context.CanvasControl).CurrentItem == node) {
        var innerRect = new RoundedRectangleVisual(layout.GetEnlarged(-3), 7) {
          Pen = new Pen(Brushes.Gray) {DashStyle = DashStyle.Dot}
        };
        group.Add(innerRect);
      }
      return group;
    }

    protected override object Lookup(INode node, Type type) {
      if (type == typeof(IClickHandler)) {
        // support the buttons to expand and collapse the sections
        return new UmlClickHandler(node, this);
      }
      if (type == typeof(ISelectionIndicatorInstaller)) {
        // show a custom selection paintable
        return new RectangleIndicatorInstaller(node.Layout) { Template = new MyTemplate() };
      }
      if (type == typeof(IFocusIndicatorInstaller)) {
        // don't show the focus
        return new RectangleIndicatorInstaller { Template = TemplateVisual.VoidTemplateVisual };
      }
      return base.Lookup(node, type);
    }

    // The rectangle Template for the selection indicator
    private class MyTemplate : TemplateVisual
    {
      public override void Paint(IRenderContext ctx, Graphics g) {
        IRectangle rect = Bounds.ToRectD().GetEnlarged(3);
        var rectangleVisual = new RectangleVisual(rect) {
          Pen = new Pen(Brushes.DarkGray) {DashStyle = DashStyle.Dot, Width = 2}
        };
        rectangleVisual.Paint(ctx, g);
      }
    }

    // draw the header:
    // class name and expand button
    private RectD DrawHeader(IRenderContext context, VisualGroup group, ClassInfo info, RectD box) {
      
      float height = titleFont.Height + typeFont.Height + 5 + 2;
      var headerBox = new RectD(box.X, box.Y, box.Width, height);

      LinearGradientBrush brush = new LinearGradientBrush(headerBox.ToRectangleF(), Color.LightBlue, Color.White, LinearGradientMode.Horizontal);
      group.Add(new RoundedRectangleVisual(headerBox, 7) {Brush = brush});
      if (info.ShowDetails) {
        // If the node is expanded, we need to fill in the lower corners as well
        var secondHeaderBox = new RectD(headerBox.X, headerBox.Y + headerBox.Height / 2, headerBox.Width, headerBox.Height / 2);
        group.Add(new RectangleVisual(secondHeaderBox) { Brush = brush });
      }

      var textBox = new RectD(box.X + 5, box.Y + 5, box.Width - 20, titleFont.Height);
      DrawTrimmedString(context, group, info.Name, titleFont, titleBrush, textBox);
      textBox = new RectD(box.X + 5, box.Y + titleFont.Height + 7, box.Width - 20, typeFont.Height);
      DrawTrimmedString(context, group, info.Type, typeFont, typeBrush, textBox);
      
      Image image = info.ShowDetails ? imageUp2 : imageDown2;
      group.Add(new ImageVisual{Image = image, Rectangle = new RectD(box.X + box.Width - ImageSize - 5, box.Y + 5, ImageSize, ImageSize)});
      return new RectD(box.X, box.Y + height, box.Width, box.Height - height);
    }

    private static void DrawTrimmedString(IRenderContext context, VisualGroup group, string text, Font font, Brush brush, RectD box) {
      if(!box.IsEmpty) {
        group.Add(new TextVisual {
          Text = text,
          Font = font,
          Format = new StringFormat { Trimming = StringTrimming.EllipsisCharacter },
          Brush = brush,
          Location = box.GetTopLeft()
        });
      }
    }

    /// <summary>
    /// Add the visuals which render a section to the given group. If the section is expanded, also add its features.
    /// </summary>
    /// <param name="context">The current render context.</param>
    /// <param name="group">The VisualGroup to add the visuals to.</param>
    /// <param name="features">The featurese which make up the section.</param>
    /// <param name="section">The name of the section (e.g. "Methods").</param>
    /// <param name="box">The rectangle which defines the remaining space inside the node visualization.</param>
    /// <returns>The rectangle which defines the remaining space inside the node visualization after the section has been drawn.</returns>
    private RectD DrawSection(IRenderContext context, VisualGroup group, FeatureSection features, String section, RectD box) {
      box = DrawSectionHeader(context, group, section, box, features.DetailsHidden);
      if (!features.DetailsHidden) {
        foreach (FeatureInfo feature in features) {
          box = DrawFeature(context, group, feature, box);
        }
      }
      return box;
    }

    private const float ImageInset = 5f;

    /// <summary>
    /// Add the visuals which render a section header.
    /// </summary>
    /// <param name="context">The current render context.</param>
    /// <param name="group">The VisualGroup to add the visuals to.</param>
    /// <param name="section">The name of the section (e.g. "Methods").</param>
    /// <param name="box">The rectangle which defines the remaining space inside the node visualization.</param>
    /// <param name="detailsHidden">Whether the section is collapsed.</param>
    /// <returns>The rectangle which defines the remaining space inside the node visualization after the header has been drawn.</returns>
    private RectD DrawSectionHeader(IRenderContext context, VisualGroup group, String section, RectD box, bool detailsHidden) {
      float height = sectionFont.Height;
      group.Add(new RectangleVisual(box.X, box.Y, box.Width, height) { Brush = sectionBrush });
      Image image = detailsHidden ? imageDown : imageUp;
      group.Add(new ImageVisual {
        Image = image,
        Rectangle = new RectD(box.X + ImageInset, box.Y + 0.5*(height - ImageSize), ImageSize, ImageSize)
      });
      var textBox = new RectD(box.X + (ImageSize + ImageInset), box.Y, box.Width - (ImageInset + ImageSize), height);
      DrawTrimmedString(context, group, section, sectionFont, sectionTextBrush, textBox);
      return new RectD(box.X, box.Y + height, box.Width, box.Height - height);
    }


    /// <summary>
    /// Add the visuals which render a single feature.
    /// </summary>
    /// <param name="context">The current render context.</param>
    /// <param name="group">The VisualGroup to add the visuals to.</param>
    /// <param name="info">The feature.</param>
    /// <param name="box">The rectangle which defines the remaining space inside the node visualization.</param>
    /// <returns>The rectangle which defines the remaining space inside the node visualization after the feature has been drawn.</returns>
    private RectD DrawFeature(IRenderContext context, VisualGroup group, FeatureInfo info, RectD box) {
      Image image = null;
      switch(info.Modifier)
      {
        case FeatureModifier.Private:
          image = imagePrivate;
          break;
        case FeatureModifier.Protected:
          image = imageProtected;
          break;
        case FeatureModifier.Public:
          image = imagePublic;
          break;
      }
      float height = featureFont.GetHeight();
      if (image != null) {
        group.Add(new ImageVisual{Image = image, Rectangle = new RectD(box.X + 12, box.Y, 10, 10)});
      }
      var textBox = new RectD(box.X + 30, box.Y, box.Width - 30, height);
      DrawTrimmedString(context, group, info.Signature, featureFont, featureBrush, textBox);
      return new RectD(box.X, box.Y + height, box.Width, box.Height - height);
    }

    #endregion
  
    #region common node style renderer features

    protected override bool IsHit(IInputModeContext context, PointD location, INode node) {
      const double cornerRadius = 7;
      var layout = node.Layout;
      // Our shape is a rounded rectangle. Hits can appear in either of the following rectangles ...
      var rect1 = new RectD(layout.X, layout.Y + cornerRadius, layout.Width, layout.Height - 2 * cornerRadius);
      var rect2 = new RectD(layout.X + cornerRadius, layout.Y, layout.Width - 2 * cornerRadius, layout.Height);
      if (rect1.Contains(location, context.HitTestRadius) || rect2.Contains(location, context.HitTestRadius)) {
        return true;
      }
      // ... or in circle arcs around the corners
      var p1 = new PointD(layout.X + cornerRadius, layout.Y + cornerRadius);
      var p2 = new PointD(layout.X + layout.Width - cornerRadius, layout.Y + cornerRadius);
      var p3 = new PointD(layout.X + cornerRadius, layout.Y + layout.Height - cornerRadius);
      var p4 = new PointD(layout.X + layout.Width - cornerRadius, layout.Y + layout.Height - cornerRadius);
      if (location.DistanceTo(p1) < cornerRadius + context.HitTestRadius ||
          location.DistanceTo(p2) < cornerRadius + context.HitTestRadius ||
          location.DistanceTo(p3) < cornerRadius + context.HitTestRadius ||
          location.DistanceTo(p4) < cornerRadius + context.HitTestRadius) {
        return true;
      }
      return false;
    }

    protected override bool IsInBox(IInputModeContext context, RectD rectangle, INode node) {
      return rectangle.Intersects(node.Layout.ToRectD());
    }

    protected override RectD GetBounds(ICanvasContext context, INode node) {
      return node.Layout.ToRectD();
    }

    #endregion

    #region Size calculations

    // the minimum size which encloses all expanded sections
    public SizeD GetPreferredSize(INode node) {
      ClassInfo info = node.Tag as ClassInfo;
      SizeF result = GetHeaderSize(info);
      if (info != null && info.ShowDetails) {
        SizeF size = GetSectionSize(info.Fields, "Fields");
        result.Width = Math.Max(result.Width, size.Width);
        result.Height += size.Height;
        size = GetSectionSize(info.Properties, "Properties");
        result.Width = Math.Max(result.Width, size.Width);
        result.Height += size.Height;
        size = GetSectionSize(info.Methods, "Methods");
        result.Width = Math.Max(result.Width, size.Width) + 5;
        result.Height += size.Height + 5;
      }
      return result;
    }

    // the size of the header
    private SizeF GetHeaderSize(ClassInfo info) {
      SizeF size = GetTextSize(info.Name, titleFont);
      float width = size.Width;
      float height = titleFont.Height + typeFont.Height + 5 + 2;
      size = GetTextSize(info.Type, typeFont);
      width = Math.Max(width, size.Width);
      width += 5 + ImageSize + 7;
      return new SizeF(width, height);
    }

    // the size of the given section
    private SizeF GetSectionSize(FeatureSection features, string section) {
      float height = sectionFont.Height;
      SizeF size = GetTextSize(section, sectionFont);
      float width = ImageSize + 2f + size.Width;
      if (!features.DetailsHidden)
      {
        foreach (FeatureInfo feature in features) {
          size = GetTextSize(feature.Signature, featureFont);
          width = Math.Max(width, size.Width + 30f);
          height += featureFont.Height;
        }
      }
      return new SizeF(width, height);
    }

    // the size of the given text
    private static SizeF GetTextSize(String text, Font font) {
      GraphicsPath path = new GraphicsPath();
      path.AddString(text, font.FontFamily, (int)font.Style, font.Size, new PointF(0, 0), StringFormat.GenericDefault);
      RectangleF bounds = path.GetBounds();
      return new SizeF((float) Math.Ceiling(bounds.Width + bounds.X), (float) Math.Ceiling(bounds.Y + bounds.Height));
    }

#endregion

    #region The expand/collapse section buttons

    // specify button type
    public enum HitInfo { None = 0, ToggleAllDetails = 1, TogglePropertyDetails = 2, ToggleMethodDetails = 4, ToggleFieldDetails = 8,  };

    // The handler hich handles the button clicks
    private class UmlClickHandler : IClickHandler, IHitTestable
    {
      private readonly INode node;
      private readonly UMLClassStyle umlStyle;

      public UmlClickHandler(INode node, UMLClassStyle umlStyle) {
        this.node = node;
        this.umlStyle = umlStyle;
      }

      public IHitTestable HitTestable {
        get { return this; }
      }

      // whether a button is hit
      public bool IsHit(IInputModeContext context, PointD location) {
        return GetHitInfo(location) != HitInfo.None;
      }

      // a button has been hit: perform the action
      public void OnClicked(IInputModeContext context, PointD location) {
        var hitInfo = GetHitInfo(location);
        var classInfo = (ClassInfo) node.Tag;
        var graph = context.GetGraph();
        switch (hitInfo) {
          case HitInfo.None:
            break;
          case HitInfo.ToggleAllDetails:
            classInfo.ShowDetails = !classInfo.ShowDetails;
            if (graph != null) {
              graph.AddUndoUnit("Toggle Details", "Toggle Details", () => classInfo.ShowDetails = !classInfo.ShowDetails,
                  () => classInfo.ShowDetails = !classInfo.ShowDetails);
            }
            AdjustNodeBoundsCommand.Execute(node, context.CanvasControl);
            break;
          case HitInfo.TogglePropertyDetails:
            classInfo.Properties.DetailsHidden = !classInfo.Properties.DetailsHidden;
            if (graph != null) {
              graph.AddUndoUnit("Toggle Property Details", "Toggle Property Details",
                  () => classInfo.Properties.DetailsHidden = !classInfo.Properties.DetailsHidden,
                  () => classInfo.Properties.DetailsHidden = !classInfo.Properties.DetailsHidden);
            }
            AdjustNodeBoundsCommand.Execute(node, context.CanvasControl);
            break;
          case HitInfo.ToggleMethodDetails:
            classInfo.Methods.DetailsHidden = !classInfo.Methods.DetailsHidden;
            if (graph != null) {
              graph.AddUndoUnit("Toggle Method Details", "Toggle Method Details",
                  () => classInfo.Methods.DetailsHidden = !classInfo.Methods.DetailsHidden,
                  () => classInfo.Methods.DetailsHidden = !classInfo.Methods.DetailsHidden);
            }
            AdjustNodeBoundsCommand.Execute(node, context.CanvasControl);
            break;
          case HitInfo.ToggleFieldDetails:
            classInfo.Fields.DetailsHidden = !classInfo.Fields.DetailsHidden;
            if (graph != null) {
              graph.AddUndoUnit("Toggle Field Details", "Toggle Field Details",
                  () => classInfo.Fields.DetailsHidden = !classInfo.Fields.DetailsHidden,
                  () => classInfo.Fields.DetailsHidden = !classInfo.Fields.DetailsHidden);
            }
            AdjustNodeBoundsCommand.Execute(node, context.CanvasControl);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      // whether a button is hit
      private bool IsButtonHit(PointD location, double offsetY) {
        IRectangle layout = node.Layout;
        float height = umlStyle.sectionFont.Height;
        double buttonX = layout.X + ImageInset;
        double buttonY = layout.Y + offsetY + 0.5f*(height - ImageSize);
        return new RectD(buttonX, buttonY, ImageSize, ImageSize).Contains(location);
      }

      // the type of button which has been hit
      private HitInfo GetHitInfo(PointD location) {
        RectD layout = node.Layout.ToRectD();
        var classInfo = node.Tag as ClassInfo;

        PointD buttonLocation = layout.TopRight + new PointD(-ImageSize - 5, 5);
        RectD buttonRect = new RectD(buttonLocation, new SizeD(ImageSize, ImageSize));

        if (buttonRect.Contains(location)) {
          return HitInfo.ToggleAllDetails;
        }
        double height = umlStyle.GetHeaderSize(classInfo).Height;
        if (IsButtonHit(location, height)) {
          return HitInfo.ToggleFieldDetails;
        }
        if (classInfo != null) {
          height += umlStyle.GetSectionSize(classInfo.Fields, "Fields").Height;
        }
        if (IsButtonHit(location, height)) {
          return HitInfo.TogglePropertyDetails;
        }
        if (classInfo != null) {
          height += umlStyle.GetSectionSize(classInfo.Properties, "Properties").Height;
        }

        if (IsButtonHit(location, height)) {
          return HitInfo.ToggleMethodDetails;
        }
        return HitInfo.None;
      }
    }

    #endregion

  }
}
