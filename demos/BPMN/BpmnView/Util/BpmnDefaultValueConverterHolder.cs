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
using System.ComponentModel;
using System.Globalization;
using Demo.yFiles.Graph.Bpmn.Styles;

namespace Demo.yFiles.Graph.Bpmn.Util {

  [TypeConverter(typeof(BpmnDefaultValueConverter))]
  internal class BpmnDefaultValueConverterHolder {}

  internal class BpmnDefaultValueConverter : TypeConverter {
    private static readonly IDictionary<string, object> defaultsMap;

    static BpmnDefaultValueConverter() {
      defaultsMap = new Dictionary<string, object>();
      defaultsMap["Demo.yFiles.Graph.Bpmn.Styles.ChoreographyMessageLabelStyle.DefaultTextPlacement"] = ChoreographyMessageLabelStyle.defaultTextPlacement;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
      return sourceType == typeof(string);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
      object retval;
      defaultsMap.TryGetValue((string)value, out retval);
      return retval;
    }
  } 
}
