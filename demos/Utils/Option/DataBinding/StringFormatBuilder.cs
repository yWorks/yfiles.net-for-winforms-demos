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
using Demo.yFiles.Option.Handler;

namespace Demo.yFiles.Option.DataBinding
{
  internal class StringFormatOptionBuilder : IOptionBuilder
  {
    #region IOptionBuilder Members

    public void AddItems(IOptionBuilderContext context, Type subjectType, object subject) {
      GenericOptionItem<StringTrimming> trimmingItem =
        new GenericOptionItem<StringTrimming>(StringFormatPropertyMapBuilder.TrimmingProperty);
      trimmingItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      context.BindItem(trimmingItem, StringFormatPropertyMapBuilder.TrimmingProperty);

      GenericOptionItem<StringAlignment> alignmentItem =
        new GenericOptionItem<StringAlignment>(StringFormatPropertyMapBuilder.AlignmentProperty);
      alignmentItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      context.BindItem(alignmentItem, StringFormatPropertyMapBuilder.AlignmentProperty);

      GenericOptionItem<StringAlignment> lineAlignItem =
        new GenericOptionItem<StringAlignment>(StringFormatPropertyMapBuilder.LineAlignmentProperty);
      lineAlignItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      context.BindItem(lineAlignItem, StringFormatPropertyMapBuilder.LineAlignmentProperty);

      IList<StringFormatFlags> directionValues = new List<StringFormatFlags>(2);
      directionValues.Add(StringFormatFlags.DirectionRightToLeft);
      directionValues.Add(StringFormatFlags.DirectionVertical);

      CollectionOptionItem<StringFormatFlags> directionItem =
        new CollectionOptionItem<StringFormatFlags>(StringFormatPropertyMapBuilder.DirectionProperty, directionValues);
      directionItem.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, true);
      directionItem.SetAttribute(OptionItem.NULL_VALUE_STRING_ATTRIBUTE, "LeftToRight");
      context.BindItem(directionItem, StringFormatPropertyMapBuilder.DirectionProperty);
    }

    #endregion
  }

  internal class StringFormatPropertyMapBuilder : PropertyMapBuilderBase<StringFormat>
  {
    internal const string DirectionProperty = "Direction";
    internal const string AlignmentProperty = "Alignment";
    internal const string LineAlignmentProperty = "LineAlignment";
    internal const string TrimmingProperty = "Trimming";

    public StringFormatPropertyMapBuilder() : base(true) {}

    #region IPropertyMapBuilder Members

    protected override void BuildPropertyMapImpl(IPropertyBuildContext<StringFormat> context) {
      context.AddEntry(DirectionProperty,
                       new DelegateGetter<object>(delegate {
                                                    StringFormat format = context.CurrentInstance;
                                                    if ((format.FormatFlags & StringFormatFlags.DirectionRightToLeft) ==
                                                        StringFormatFlags.DirectionRightToLeft) {
                                                      return StringFormatFlags.DirectionRightToLeft;
                                                    }
                                                    if ((format.FormatFlags & StringFormatFlags.DirectionVertical) ==
                                                        StringFormatFlags.DirectionVertical) {
                                                      return StringFormatFlags.DirectionVertical;
                                                    }
                                                    return null;
                                                  }),
                       new DelegateSetter<object>(
                         delegate(object value) {
                           if (value == null) {
                             context.CurrentInstance.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
                             context.CurrentInstance.FormatFlags &= ~StringFormatFlags.DirectionVertical;
                           } else if (value is StringFormatFlags) {
                             if ((StringFormatFlags) value == StringFormatFlags.DirectionRightToLeft) {
                               context.CurrentInstance.FormatFlags &= ~StringFormatFlags.DirectionVertical;
                               context.CurrentInstance.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                             } else if ((StringFormatFlags) value == StringFormatFlags.DirectionVertical) {
                               context.CurrentInstance.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
                               context.CurrentInstance.FormatFlags |= StringFormatFlags.DirectionVertical;
                             }
                           }
                         }));
      context.AddEntry<StringAlignment>(AlignmentProperty, delegate { return context.CurrentInstance.Alignment; },
                                        delegate(StringAlignment value) { context.CurrentInstance.Alignment = value; });

      context.AddEntry<StringAlignment>(LineAlignmentProperty,
                                        delegate { return context.CurrentInstance.LineAlignment; },
                                        delegate(StringAlignment value) { context.CurrentInstance.LineAlignment = value; });
      context.AddEntry<StringTrimming>(TrimmingProperty, delegate { return context.CurrentInstance.Trimming; },
                                       delegate(StringTrimming value) { context.CurrentInstance.Trimming = value; });
    }

    #endregion
  }
}