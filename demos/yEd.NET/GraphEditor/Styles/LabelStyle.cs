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
using System.Windows.Forms.VisualStyles;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.GraphEditor.Styles
{
  public sealed class LabelStyle : ILabelStyle
  {
    private static readonly LabelStyleRenderer renderer = new LabelStyleRenderer();
    private DefaultLabelStyle style;

    public LabelStyle() {
      style = CreateInnerStyle();
    }

    private static DefaultLabelStyle CreateInnerStyle() {
      var defaultLabelStyle = new DefaultLabelStyle {
        AutoFlip = true, ClipText = true, TextBrush = Brushes.Black,
        Font = new System.Drawing.Font("Arial", 10)
      };
      defaultLabelStyle.StringFormat.LineAlignment = StringAlignment.Center;
      return defaultLabelStyle;
    }

    public object Clone() {
      var clone = (LabelStyle) MemberwiseClone();
      clone.style = CreateInnerStyle();
      return clone;
    }

    ILabelStyleRenderer ILabelStyle.Renderer {
      get { return renderer; }
    }

    public System.Drawing.Font Font {
      get { return style.Font; }
    }

    [NotNull,DisplayName("Text Color")]
    [DefaultValue(typeof(SolidBrush), "Black")]
    public Brush TextBrush {
      get { return style.TextBrush; }
      set { style.TextBrush = value; }
    }
    
    public bool AutoFlip {
      get { return true; }
    }

    [DisplayName("Font Style")]
    [DefaultValue(typeof(FontStyle), "Normal")]
    public FontStyle FontStyle {
      get { return Font.Style; }
      set { 
        if (Font.Style != value) {
          this.style.Font = new System.Drawing.Font(Font.FontFamily.Name, Font.Size, value);
        }
      }
    }

    [DisplayName("Font Size")]
    [DefaultValue(typeof(FontStyle), "Normal")]
    public float FontSize {
      get { return Font.Size; }
      set { 
        if (Font.Size != value) {
          this.style.Font = new System.Drawing.Font(Font.FontFamily.Name, value, Font.Style);
        }
      }
    }

    [CanBeNull, DisplayName("Border")]
    [DefaultValue(null)]
    public Pen BackgroundPen {
      get { return style.BackgroundPen; }
      set { style.BackgroundPen = value; }
    }

    [CanBeNull, DisplayName("Background")]
    [DefaultValue(null)]
    public Brush BackgroundBrush {
      get { return style.BackgroundBrush; }
      set { style.BackgroundBrush = value; }
    }

    [DisplayName("V-Alignment")]
    [DefaultValue(VerticalAlignment.Center)]
    public StringAlignment VerticalTextAlignment {
      get { return style.StringFormat.LineAlignment; }
      set {
        if (value != style.StringFormat.LineAlignment) {
          style.StringFormat = (StringFormat)style.StringFormat.Clone();
          style.StringFormat.LineAlignment = value;
        }
      }
    }

    [DisplayName("H-Alignment")]
    [DefaultValue(StringAlignment.Near)]
    public StringAlignment HorizontalTextAlignment {
      get { return style.StringFormat.Alignment; }
      set {
        if (value != style.StringFormat.Alignment) {
          style.StringFormat = (StringFormat) style.StringFormat.Clone();
          style.StringFormat.Alignment = value;
        }
      }
    }

    public StringFormat StringFormat {
      get { return style.StringFormat; }
    }

    [DisplayName("Clip Text")]
    [DefaultValue(true)]
    public bool ClipText {
      get { return style.ClipText; }
      set { style.ClipText = value; }
    }

    private class LabelStyleRenderer : ILabelStyleRenderer {
      private static readonly DefaultLabelStyleRenderer renderer = new DefaultLabelStyleRenderer();

      public IVisualCreator GetVisualCreator(ILabel label, ILabelStyle style) {
        return renderer.GetVisualCreator(label, ((LabelStyle)style).style);
      }

      public IBoundsProvider GetBoundsProvider(ILabel label, ILabelStyle style) {
        return renderer.GetBoundsProvider(label, ((LabelStyle)style).style);
      }

      public IHitTestable GetHitTestable(ILabel label, ILabelStyle style) {
        return renderer.GetHitTestable(label, ((LabelStyle)style).style);
      }

      public IMarqueeTestable GetMarqueeTestable(ILabel label, ILabelStyle style) {
        return renderer.GetMarqueeTestable(label, ((LabelStyle)style).style);
      }

      public IVisibilityTestable GetVisibilityTestable(ILabel label, ILabelStyle style) {
        return renderer.GetVisibilityTestable(label, ((LabelStyle)style).style);
      }

      public ILookup GetContext(ILabel label, ILabelStyle style) {
        return renderer.GetContext(label, ((LabelStyle)style).style);
      }

      public SizeD GetPreferredSize(ILabel label, ILabelStyle style) {
        return renderer.GetPreferredSize(label, ((LabelStyle)style).style);
      }
    }
  }
}
