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
using Demo.yFiles.Option.Constraint;
using Demo.yFiles.Option.DataBinding.UI;
using Demo.yFiles.Option.Handler;
using yWorks.Graph;

namespace Demo.yFiles.Option.DataBinding
{
  internal class DefaultPenOptionBuilder : IOptionBuilder
  {
    
    private bool allowNullValue = true;

    public bool AllowNullValue {
      get { return allowNullValue; }
      set { allowNullValue = value; }
    }

    /// <inheritdoc/>
    public virtual void AddItems(IOptionBuilderContext context, Type subjectType, object subject) {
      DefaultBrushOptionBuilder brushOptionBuilder = new DefaultBrushOptionBuilder();
      brushOptionBuilder.AllowNullValue = AllowNullValue;
      brushOptionBuilder.AddItems(context, typeof(Brush), null);

      FloatOptionItem widthItem = new FloatOptionItem(DefaultPenPropertyMapBuilder.Width);
      bool widthItemAdded = context.BindItem(widthItem, DefaultPenPropertyMapBuilder.Width);
      GenericOptionItem<DashStyle> dashStyleItem =
        new GenericOptionItem<DashStyle>(DefaultPenPropertyMapBuilder.DashStyle, OptionItem.VALUE_UNDEFINED);
      dashStyleItem.SetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE, true);
      dashStyleItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
//      dashStyleItem.SetAttribute(OptionItem.CUSTOM_TABLEITEM_EDITOR, typeof(PenDashStyleUITypeEditor));
      EnumUITypeEditor<DashStyle> editor = new EnumUITypeEditor<DashStyle>();
      editor.Renderer = new DashStyleItemRenderer();

      dashStyleItem.SetAttribute(OptionItem.CUSTOM_TABLEITEM_EDITOR, editor);
//      dashStyleItem.SetAttribute(OptionItem.CUSTOM_CELLRENDERER, typeof(DashStyleItemRenderer));
      bool dashStyleItemAdded = context.BindItem(dashStyleItem, DefaultPenPropertyMapBuilder.DashStyle);

      IOptionGroup parent = context.Lookup<IOptionGroup>();
      if (parent != null) {
        IOptionItem fillTypeItem = parent[DefaultBrushPropertyMapBuilder.FillType];
        ConstraintManager cm = parent.Lookup<ConstraintManager>();
        if (cm != null && fillTypeItem != null) {
          ICondition cond = ConstraintManager.LogicalCondition.Not(cm.CreateValueEqualsCondition(fillTypeItem, null));
          if (widthItemAdded) {
            cm.SetEnabledOnCondition(cond, widthItem);
          }
          if (dashStyleItemAdded) {
            cm.SetEnabledOnCondition(cond, dashStyleItem);
          }
        }
      }
    }
  }

  internal class DefaultPenPropertyMapBuilder : PropertyMapBuilderBase<Pen>
  {
    
    internal const string Width = "Width";
    internal const string DashStyle = "DashStyle";
    

    public DefaultPenPropertyMapBuilder() : base(false) { }
    #region IPropertyMapBuilder Members

    protected override void BuildPropertyMapImpl(IPropertyBuildContext<Pen> context) {
      Brush b = context.CurrentInstance == null ? null : context.CurrentInstance.Brush;
      IPropertyMapBuilder brushBuilder;
      if (b == null) {
        brushBuilder = context.GetPropertyMapBuilder(typeof (Brush), b);
      } else {
        brushBuilder = context.GetPropertyMapBuilder(b);
      }

      if (brushBuilder != null) {
        brushBuilder.BuildPropertyMap(context.CreateChildContext<Brush>("",
                                                                        delegate() {
                                                                          Pen p = context.CurrentInstance;
                                                                          return p == null ? null:context.CurrentInstance.Brush;
                                                                        },
                                                                        delegate(Brush newInstance) {
                                                                          Pen p = context.CurrentInstance;
                                                                          if(newInstance == null) {
                                                                            context.SetNewInstance(null);
                                                                            return;
                                                                          }
                                                                          if (p == null) {
                                                                            context.SetNewInstance(new Pen(newInstance));
                                                                          } else {
                                                                            Pen clone = (Pen) p.Clone();
                                                                            clone.Brush = newInstance;
                                                                            context.SetNewInstance(clone);
                                                                          }
                                                                        }, AssignmentPolicy.CreateNewInstance));
      }

      context.AddEntry<float>(Width,
                       delegate {
                           Pen pen = context.CurrentInstance;
                           return pen == null ? 0 : pen.Width;
                         },
                       delegate(float value) {
                                                   Pen pen = context.CurrentInstance;
                                                   Pen clone = pen.Clone() as Pen;
                                                   if (clone != null) {
                                                     clone.Width = value;
                                                     context.SetNewInstance(clone);
                                                   }
                                                 });
      context.AddEntry(DashStyle,
                   new DelegateGetter<object>(
                     delegate() {
                       Pen pen = context.CurrentInstance;
                       return pen == null ? OptionItem.VALUE_UNDEFINED : pen.DashStyle;
                     }),
                   new DelegateSetter<DashStyle>(delegate(DashStyle value) {
                                                Pen pen = context.CurrentInstance;
                                                   Pen clone = pen.Clone() as Pen;
                                                   if (clone != null) {
                                                     clone.DashStyle = value;
                                                     context.SetNewInstance(clone);
                                                   }
                                                 }));
    }

    #endregion
  }
}