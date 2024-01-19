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
using System.Drawing;
using System.Drawing.Drawing2D;
using Demo.yFiles.Option.Constraint;
using Demo.yFiles.Option.DataBinding.UI;
using Demo.yFiles.Option.Handler;
using yWorks.Graph;

namespace Demo.yFiles.Option.DataBinding
{
  /// <summary>
  /// Default implementation of <see cref="IOptionBuilder"/> and <see cref="IPropertyMapBuilder"/> for .NET
  /// standard brushes.
  /// </summary>
  /// <remarks>This implementation builds properties and options for the most important brush properties, such as colors or fill patterns.
  /// </remarks>
  internal class DefaultBrushOptionBuilder : IOptionBuilder
  {
    private bool allowNullValue = true;

    /// <summary>
    /// Whether null values are valid to indicate empty (non-set) brushes
    /// </summary>
    /// <value>Default value is <see langword="true"/></value>
    public bool AllowNullValue {
      get { return allowNullValue; }
      set { allowNullValue = value; }
    }

    /// <inheritdoc/>
    public virtual void AddItems(IOptionBuilderContext context, Type subjectType, object subject) {
//      CollectionOptionItem<string> fillTypeItem =
//        new CollectionOptionItem<string>(DefaultBrushPropertyMapBuilder.FillType,
//        new string[] {
//                       DefaultBrushPropertyMapBuilder.SolidBrushFillType, 
//      DefaultBrushPropertyMapBuilder.HatchBrushFillType, 
//      DefaultBrushPropertyMapBuilder.LinearGradientBrushFillType, 
//      DefaultBrushPropertyMapBuilder.TextureBrushFillType
//    });
      GenericOptionItem<BrushTypes> fillTypeItem =
        new GenericOptionItem<BrushTypes>(DefaultBrushPropertyMapBuilder.FillType);
      fillTypeItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, AllowNullValue);
//      fillTypeItem.SetAttribute(OptionItem.NULL_VALUE_OBJECT, BrushTypes.Nothing);
      fillTypeItem.SetAttribute(OptionItem.NULL_VALUE_STRING_ATTRIBUTE, "Nothing");
      bool fillTypeAdded =
        context.BindItem(fillTypeItem, 
        DefaultBrushPropertyMapBuilder.FillType
      );

      ColorOptionItem foreColorItem = new ColorOptionItem(DefaultBrushPropertyMapBuilder.ForegroundColor);
      bool foreColorAdded = context.BindItem(foreColorItem, DefaultBrushPropertyMapBuilder.ForegroundColor);

      ColorOptionItem backColorOptionItem = new ColorOptionItem(DefaultBrushPropertyMapBuilder.BackgroundColor);
      bool backColorAdded = context.BindItem(backColorOptionItem,DefaultBrushPropertyMapBuilder.BackgroundColor);

      GenericOptionItem<HatchStyle> hatchStyleItem =
        new GenericOptionItem<HatchStyle>(DefaultBrushPropertyMapBuilder.HatchStyle, OptionItem.VALUE_UNDEFINED);
      hatchStyleItem.SetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE, true);
      hatchStyleItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      

      bool hatchItemAdded =
        context.BindItem(hatchStyleItem, DefaultBrushPropertyMapBuilder.HatchStyle);
      
      if(hatchItemAdded) {
        EnumUITypeEditor<HatchStyle> hatchEditor = new EnumUITypeEditor<HatchStyle>();
        hatchEditor.Renderer = new HatchStyleItemRenderer();
        hatchStyleItem.SetAttribute(OptionItem.CUSTOM_TABLEITEM_EDITOR, hatchEditor); 
      }

      FloatOptionItem rotationItem = new FloatOptionItem(DefaultBrushPropertyMapBuilder.Rotation);
      bool floatItemAdded = context.BindItem(rotationItem, DefaultBrushPropertyMapBuilder.Rotation);

      GenericOptionItem<Image> imageItem =
        new GenericOptionItem<Image>(DefaultBrushPropertyMapBuilder.Image, OptionItem.VALUE_UNDEFINED);
      imageItem.SetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE, true);
      imageItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      bool imageItemAdded = context.BindItem(imageItem, DefaultBrushPropertyMapBuilder.Image);

      if (fillTypeAdded) {
        ConstraintManager cm = context.Lookup<ConstraintManager>();
        if (cm != null) {
          if (foreColorAdded) {
            ICondition cond = cm.CreateValueIsOneOfCondition(fillTypeItem, BrushTypes.SolidBrush, BrushTypes.HatchBrush, BrushTypes.LinearGradientBrush);
            cm.SetEnabledOnCondition(cond, foreColorItem);
          }
          if (backColorAdded) {
            ICondition cond = cm.CreateValueIsOneOfCondition(fillTypeItem, BrushTypes.HatchBrush, BrushTypes.LinearGradientBrush);
            cm.SetEnabledOnCondition(cond, backColorOptionItem);
          }
          if (hatchItemAdded) {
            cm.SetEnabledOnValueEquals(fillTypeItem, BrushTypes.HatchBrush, hatchStyleItem);
          }
          if (imageItemAdded) {
            cm.SetEnabledOnValueEquals(fillTypeItem, BrushTypes.TextureBrush, imageItem);
          }
          if (floatItemAdded) {
            cm.SetEnabledOnValueEquals(fillTypeItem, BrushTypes.LinearGradientBrush, rotationItem);
          }
        }
      }
    }

    
  }

  internal class DefaultBrushPropertyMapBuilder : PropertyMapBuilderBase<Brush> {
    private static readonly float sqrt2 = (float) Math.Sqrt(2);

    internal const string FillType = "FillType";
    internal const string ForegroundColor = "ForegroundColor";
    internal const string BackgroundColor = "BackgroundColor";
    internal const string HatchStyle = "HatchStyle";
    internal const string Rotation = "Rotation";
    internal const string Image = "Image";

    public DefaultBrushPropertyMapBuilder() : base(false) {
    }

    #region IPropertyMapBuilder Members
    protected override void BuildPropertyMapImpl(IPropertyBuildContext<Brush> context) {
      IValueGetter fillTypeGetter = new DelegateGetter<object>(
        delegate() {
          Brush brush = context.CurrentInstance;
          if (brush == null) {
            return null;
          }
          if (brush is SolidBrush) {
            return BrushTypes.SolidBrush;
          }
          if (brush is HatchBrush) {
            return BrushTypes.HatchBrush;
          }
          if (brush is LinearGradientBrush) {
            return BrushTypes.LinearGradientBrush;
          }
          if (brush is TextureBrush) {
            return BrushTypes.TextureBrush;
          }
          return BrushTypes.Custom;
        });

      IValueSetter fillTypeSetter = new DelegateSetter<object>(
        delegate(object value) {
          if (value != null) {
            if(!(value is BrushTypes)) {
              return;
            }
            BrushTypes type = (BrushTypes) value;
            Brush brush = context.CurrentInstance;
            if (type == BrushTypes.Custom) {
              context.SetNewInstance(brush);
              return;
            }
            //todo: this code should be replaced by a composite brush editor some day
            Color fg = Color.Black;
            Color bg = Color.Empty;
            
            if(brush != null) {
              if(brush is SolidBrush) {
                fg = ((SolidBrush) brush).Color;
              }
              if(brush is HatchBrush) {
                HatchBrush hb = (HatchBrush) brush;
                fg = hb.ForegroundColor;
                bg = hb.BackgroundColor;
              }
              if(brush is LinearGradientBrush) {
                LinearGradientBrush lb = (LinearGradientBrush) brush;
                Color[] colors = lb.LinearColors;
                if(colors != null) {
                  fg = colors[0];
                  bg = colors[1];
                }
              }
            }
            switch (type) {
              case BrushTypes.SolidBrush:
                if (!(brush is SolidBrush)) {
                  context.SetNewInstance(new SolidBrush(fg));
                  return;
                }
                break;
              case BrushTypes.HatchBrush:
                if (!(brush is HatchBrush)) {
                  context.SetNewInstance(new HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, fg, bg));
                  return;
                }
                break;
              case BrushTypes.TextureBrush:
                if (!(brush is TextureBrush)) {
                  Image img = new Bitmap(1, 1);
                  context.SetNewInstance(new TextureBrush(img));
                  return;
                }
                break;
              case BrushTypes.LinearGradientBrush:
                if (!(brush is LinearGradientBrush)) {
                  //Accomodate for rounding errors
                  LinearGradientBrush lgb = new LinearGradientBrush(new PointF(), new PointF(1.01f, 0), fg, bg);
                  context.SetNewInstance(lgb);
                  return;
                }
                break;
            }
          } else {
            context.SetNewInstance(null);
          }
        });

      context.AddEntry(FillType, fillTypeGetter, fillTypeSetter, null);


      DelegateGetter<Color> foregroundColorGetter = new DelegateGetter<Color>(
        delegate() {
          Brush brush = context.CurrentInstance;
          if (brush == null) {
            return Color.Empty;
          }
          if (brush is SolidBrush) {
            return ((SolidBrush) brush).Color;
          }
          if (brush is HatchBrush) {
            return
              ((HatchBrush) brush).ForegroundColor;
          }
          if (brush is LinearGradientBrush) {
            Color[] colors =
              ((LinearGradientBrush) brush).
                LinearColors;
            return
              colors == null
                ? Color.Empty
                : colors[0];
          }
          return Color.Empty;
        });

      DelegateSetter<Color> foregroundColorSetter =
        new DelegateSetter<Color>(
        delegate(Color value) {
          Brush brush = context.CurrentInstance;
          if (brush is SolidBrush) {
            SolidBrush newBrush = new SolidBrush(value);
            context.SetNewInstance(newBrush);
            return;
          }

          if (brush is HatchBrush) {
            HatchBrush hb = (HatchBrush)brush;
            HatchBrush newBrush = new HatchBrush(hb.HatchStyle, value, hb.BackgroundColor);
            context.SetNewInstance(newBrush);
            return;
          }

          if (brush is LinearGradientBrush) {
            LinearGradientBrush lb = (LinearGradientBrush)brush;
            Color[] colors = lb.LinearColors;
            LinearGradientBrush newBrush = new LinearGradientBrush(lb.Rectangle, value, colors[1], 0f);
            newBrush.WrapMode = lb.WrapMode;
            newBrush.Transform = lb.Transform;
            //(LinearGradientBrush)lb.Clone();

            //            newBrush.LinearColors = new Color[] { colors[0], value };
            context.SetNewInstance(newBrush);
            return;
          }

//          if (brush is LinearGradientBrush) {
//            LinearGradientBrush lb = (LinearGradientBrush)brush;
//            Color[] colors = lb.LinearColors;
//            LinearGradientBrush newBrush = (LinearGradientBrush)lb.Clone();
//            newBrush.LinearColors = new Color[] { value, colors[1] };
//
//            context.SetNewInstance(newBrush);
//            return;
//          }
        }, delegate() {
             Brush brush = context.CurrentInstance;
             if (brush is SolidBrush || brush is HatchBrush) {
               return true;
             }

             if (brush is LinearGradientBrush) {
               LinearGradientBrush lb = (LinearGradientBrush) brush;
               Color[] colors = lb.LinearColors;
               return colors != null;
             }
             return false;
           });

      context.AddEntry(ForegroundColor,
                   foregroundColorGetter, foregroundColorSetter, new ArgbEqualityComparer());

      DelegateGetter<Color> backgroundColorGetter = new DelegateGetter<Color>(
        delegate() {
          Brush brush = context.CurrentInstance;
          if (brush == null) {
            return Color.Empty;
          }
          if (brush is HatchBrush) {
            return
              ((HatchBrush) brush).BackgroundColor;
          }
          if (brush is LinearGradientBrush) {
            Color[] colors =
              ((LinearGradientBrush) brush).
                LinearColors;
            return
              colors == null
                ? Color.Empty
                : colors[1];
          }
          return Color.Empty;
        });

      DelegateSetter<Color> backgroundColorSetter =
        new DelegateSetter<Color>(
        delegate(Color value) {
          Brush brush = context.CurrentInstance;
          if (brush is HatchBrush) {
            HatchBrush hb = (HatchBrush)brush;
            HatchBrush newBrush = new HatchBrush(hb.HatchStyle, hb.ForegroundColor, value);
            context.SetNewInstance(newBrush);
            return;
          }

          if (brush is LinearGradientBrush) {
            LinearGradientBrush lb = (LinearGradientBrush)brush;
            Color[] colors = lb.LinearColors;
            LinearGradientBrush newBrush = new LinearGradientBrush(lb.Rectangle, colors[0], value, 0f);
            newBrush.Transform = lb.Transform;
            newBrush.WrapMode = lb.WrapMode;
              //(LinearGradientBrush)lb.Clone();
            
//            newBrush.LinearColors = new Color[] { colors[0], value };
            context.SetNewInstance(newBrush);
            return;
          }
        }, delegate() {
             Brush brush = context.CurrentInstance;
             if (brush is SolidBrush || brush is HatchBrush) {
               return true;
             }

             if (brush is LinearGradientBrush) {
               LinearGradientBrush lb = (LinearGradientBrush) brush;
               Color[] colors = lb.LinearColors;
               return colors != null;
             }
             return false;
           });
      context.AddEntry(BackgroundColor, backgroundColorGetter, backgroundColorSetter, new ArgbEqualityComparer());

      context.AddEntry(HatchStyle,
                   new DelegateGetter<object>(delegate() {
                                                Brush brush = context.CurrentInstance;
                                                HatchBrush hb = brush as HatchBrush;
                                                if (hb != null) {
                                                  return hb.HatchStyle;
                                                } else {
                                                  return OptionItem.VALUE_UNDEFINED;
                                                }
                                              }),
                   new DelegateSetter<HatchStyle>(
                     delegate(HatchStyle value) {
                       HatchBrush hb = context.CurrentInstance as HatchBrush;
                       if (hb != null) {
                         HatchBrush newBrush = new HatchBrush(value, hb.ForegroundColor, hb.BackgroundColor);
                         context.SetNewInstance(newBrush);                       
                       }
                     }));

      context.AddEntry(Rotation,
                   new DelegateGetter<float>(delegate() {
                                                LinearGradientBrush lb = context.CurrentInstance as LinearGradientBrush;
                                                if (lb != null) {
                                                  return (float) CalculateAngle(lb.Transform);
                                                } else {
                                                  return 0;
                                                }
                                              }),
                   new DelegateSetter<float>(
                     delegate(float value) {
                       LinearGradientBrush lb = context.CurrentInstance as LinearGradientBrush;
                       if (lb != null) {
                         context.SetNewInstance(RotateBrush(lb, value));                       
                       }
                     }));
      context.AddEntry(Image,
                   new DelegateGetter<Image>(delegate() {
                                               TextureBrush textureBrush = context.CurrentInstance as TextureBrush;
                                               if (textureBrush != null) {
                                                 return textureBrush.Image;
                                               } else {
                                                 return null;
                                               }
                                             }),
                   new DelegateSetter<Image>(
                     delegate(Image value) {
                       TextureBrush newBrush = new TextureBrush(value);
                       context.SetNewInstance(newBrush);
                     }));

      #endregion
    }

    private static LinearGradientBrush RotateBrush(LinearGradientBrush lb, float value) {
      float scale = (float) (Math.Cos(Math.PI/4 - Math.PI*value/180)*sqrt2);
      LinearGradientBrush newBrush = (LinearGradientBrush)lb.Clone();
      newBrush.ResetTransform();
      newBrush.ScaleTransform(scale, scale);               
      newBrush.RotateTransform(value);
      newBrush.WrapMode = lb.WrapMode;
      return newBrush;
    }


    private static double CalculateAngle(Matrix transform) {
      PointF p = new PointF(1f, 0f);
      PointF[] points = new PointF[] {p};
      transform.TransformVectors(points);
      return Math.Round(Math.Atan2(points[0].Y, points[0].X)*180/Math.PI, 1);
    }
  }

  internal enum BrushTypes
  {
    SolidBrush,
    HatchBrush,
    LinearGradientBrush,
    TextureBrush,
    Custom
  }
}
