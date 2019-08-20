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
using System.Collections.Generic;
using System.Windows.Markup;
using System.Xml.Linq;
using Demo.yFiles.IO.GraphML.Compat;
using Demo.yFiles.IO.GraphML.Compat.Xaml;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using yWorks.GraphML;
using yWorks.Markup.Common;
using yWorks.Markup.Platform;

// Assembly attributes to allow the framework XAML deserializer to find our types.
[assembly: XmlnsDefinition(GraphMLCompatibility.YfilesCommonNS20, "Demo.yFiles.IO.GraphML.Compat.Common")]
[assembly: XmlnsDefinition(GraphMLCompatibility.YfilesCommonMarkupNS20, "Demo.yFiles.IO.GraphML.Compat.CommonMarkup")]
[assembly: XmlnsDefinition(GraphMLCompatibility.YfilesNetXamlNS10, "Demo.yFiles.IO.GraphML.Compat.Xaml")]

namespace Demo.yFiles.IO.GraphML.Compat
{
  /// <summary>
  /// Helper class to enable parsing GraphML files from previous versions of yFiles.NET.
  /// </summary>
  /// <remarks>
  /// For usage in your own applications, you can reference the demo and simply call
  /// the <see cref="ConfigureGraphMLCompatibility"/> method with your <see cref="GraphMLIOHandler"/>
  /// (or use the provided extension method on <see cref="GraphMLIOHandler"/>).
  /// </remarks>
  public static class GraphMLCompatibility
  {
    /// <summary>
    /// Configures a <see cref="GraphMLIOHandler"/> instance to read GraphML files written by
    /// previous versions of yFiles.NET.
    /// </summary>
    public static void ConfigureGraphMLCompatibility(GraphMLIOHandler handler) {
      // Register our three special namespaces containing MarkupExtensions that can then just be picked up by namespace and name
      // and don't have to be mapped manually (see below).
      // These mappings are akin to the assembly attributes at the top of this file.
      handler.AddXamlNamespaceMapping(YfilesCommonMarkupNS20, typeof(CommonMarkup.StaticExtension));
      handler.AddXamlNamespaceMapping(YfilesNetXamlNS10, typeof(NodeScaledPortLocationModelExtension));
      handler.AddXamlNamespaceMapping(YfilesCommonNS20, typeof(Common.LabelExtension));
      handler.AddXamlNamespaceMapping(FormsNS10, typeof(SolidBrushExtension));

      // Provides a mapping between XML element names in specific yFiles.NET 4.4 namespaces
      // to either their equivalent in the current library version, or to specially-written
      // MarkupExtensions that surface the old API and map it to the correct instances in the new API.
      var mappings = new Dictionary<XName, Type> {
        // General graph stuff
        { XName.Get("Bend", YfilesCommonNS20), typeof(BendExtension) },
        { XName.Get("GraphSettings", YfilesCommonNS20), typeof(GraphSettings) },
        { XName.Get("NodeDefaults", YfilesCommonNS20), typeof(NodeDefaults) },
        { XName.Get("EdgeDefaults", YfilesCommonNS20), typeof(EdgeDefaults) },
        { XName.Get("GraphMLReference", YfilesCommonNS20), typeof(GraphMLReferenceExtension) },

        // Void styles
        { XName.Get("VoidPortStyle", YfilesCommonNS20), typeof(VoidPortStyle) },
        { XName.Get("VoidNodeStyle", YfilesCommonNS20), typeof(VoidNodeStyle) },
        { XName.Get("VoidEdgeStyle", YfilesCommonNS20), typeof(VoidEdgeStyle) },
        { XName.Get("VoidLabelStyle", YfilesCommonNS20), typeof(VoidLabelStyle) },

        // Node styles
        { XName.Get("ShapeNodeStyle", YfilesNetXamlNS10), typeof(ShapeNodeStyle) },
        { XName.Get("BevelNodeStyle", YfilesNetXamlNS10), typeof(BevelNodeStyle) },
        { XName.Get("UriImageNodeStyle", YfilesNetXamlNS10), typeof(UriImageNodeStyleExtension) },
        { XName.Get("MemoryImageNodeStyle", YfilesNetXamlNS10), typeof(MemoryImageNodeStyle) },
        { XName.Get("Bitmap", YfilesNetXamlNS10), typeof(BitmapExtension) },
        { XName.Get("GeneralPathNodeStyle", YfilesNetXamlNS10), typeof(GeneralPathNodeStyle) },
        { XName.Get("GeneralPathMarkup", YfilesNetXamlNS10), typeof(GeneralPathExtension) },
        { XName.Get("MoveTo", YfilesNetXamlNS10), typeof(MoveTo) },
        { XName.Get("LineTo", YfilesNetXamlNS10), typeof(LineTo) },
        { XName.Get("QuadTo", YfilesNetXamlNS10), typeof(QuadTo) },
        { XName.Get("CubicTo", YfilesNetXamlNS10), typeof(CubicTo) },
        { XName.Get("Close", YfilesNetXamlNS10), typeof(Close) },
        { XName.Get("PanelNodeStyle", YfilesNetXamlNS10), typeof(PanelNodeStyle) },
        { XName.Get("ShinyPlateNodeStyle", YfilesNetXamlNS10), typeof(ShinyPlateNodeStyle) },
        { XName.Get("ShadowNodeStyleDecorator", YfilesNetXamlNS10), typeof(ShadowNodeStyleDecorator) },
        { XName.Get("CollapsibleNodeStyleDecorator", YfilesNetXamlNS10), typeof(CollapsibleNodeStyleDecorator) },
        { XName.Get("TableNodeStyle", YfilesNetXamlNS10), typeof(TableNodeStyle) },

        // Port styles
        { XName.Get("NodeStylePortStyleAdapter", YfilesNetXamlNS10), typeof(NodeStylePortStyleAdapter) },

        // Edge styles
        { XName.Get("PolylineEdgeStyle", YfilesNetXamlNS10), typeof(PolylineEdgeStyleExtension) },
        { XName.Get("ArcEdgeStyle", YfilesNetXamlNS10), typeof(ArcEdgeStyle) },

        // Label styles
        { XName.Get("NodeStyleLabelStyleAdapter", YfilesNetXamlNS10), typeof(NodeStyleLabelStyleAdapter) },

        // Auxiliary style classes
        { XName.Get("Arrow", YfilesNetXamlNS10), typeof(Arrow) },
        { XName.Get("DefaultArrow", YfilesNetXamlNS10), typeof(Arrows) },
        { XName.Get("Table", YfilesCommonNS20), typeof(TableExtension) },
        { XName.Get("Row", YfilesCommonNS20), typeof(Common.RowExtension) },
        { XName.Get("Column", YfilesCommonNS20), typeof(Common.ColumnExtension) },

        // Label models and their parameters
        { XName.Get("FreeLabelModel", YfilesNetXamlNS10), typeof(FreeLabelModel) },
        { XName.Get("FixedLabelModelParameter", YfilesNetXamlNS10), typeof(FixedLabelModelParameterExtension) },
        { XName.Get("AnchoredLabelModelParameter", YfilesNetXamlNS10), typeof(AnchoredLabelModelParameterExtension) },
        { XName.Get("CompositeLabelModel", YfilesNetXamlNS10), typeof(CompositeLabelModel) },
        { XName.Get("CompositeLabelModelParameter", YfilesNetXamlNS10), typeof(CompositeLabelModelParameterExtension) },
        { XName.Get("GenericModel", YfilesNetXamlNS10), typeof(GenericLabelModelExtension) },
        { XName.Get("GenericLabelModelParameter", YfilesNetXamlNS10), typeof(GenericLabelModelParameterExtension) },
        { XName.Get("DescriptorWrapperLabelModel", YfilesNetXamlNS10), typeof(DescriptorWrapperLabelModel) },
        { XName.Get("DescriptorWrapperLabelModelParameter", YfilesNetXamlNS10), typeof(DescriptorWrapperLabelModelParameterExtension) },

        { XName.Get("FreeNodeLabelModel", YfilesNetXamlNS10), typeof(FreeNodeLabelModel) },
        { XName.Get("RatioAnchoredLabelModelParameter", YfilesNetXamlNS10), typeof(RatioAnchoredLabelModelParameterExtension) },
        { XName.Get("SandwichLabelModel", YfilesNetXamlNS10), typeof(SandwichLabelModel) },
        { XName.Get("SandwichParameter", YfilesNetXamlNS10), typeof(SandwichParameterExtension) },
        { XName.Get("ExteriorLabelModel", YfilesNetXamlNS10), typeof(ExteriorLabelModel) },
        { XName.Get("ExteriorLabelModelParameter", YfilesNetXamlNS10), typeof(ExteriorLabelModelParameterExtension) },
        { XName.Get("InteriorLabelModel", YfilesNetXamlNS10), typeof(InteriorLabelModel) },
        { XName.Get("InteriorLabelModelParameter", YfilesNetXamlNS10), typeof(InteriorLabelModelParameterExtension) },
        { XName.Get("InteriorStretchLabelModel", YfilesNetXamlNS10), typeof(InteriorStretchLabelModel) },
        { XName.Get("InteriorStretchLabelModelParameter", YfilesNetXamlNS10), typeof(InteriorStretchLabelModelParameterExtension) },

        { XName.Get("NinePositionsEdgeLabelModel", YfilesNetXamlNS10), typeof(NinePositionsEdgeLabelModel) },
        { XName.Get("NinePositionsEdgeLabelParameter", YfilesNetXamlNS10), typeof(Xaml.NinePositionsEdgeLabelModelParameterExtension) },
        { XName.Get("FreeEdgeLabelModel", YfilesNetXamlNS10), typeof(FreeEdgeLabelModel) },
        { XName.Get("FreeEdgeLabelModelParameter", YfilesNetXamlNS10), typeof(FreeEdgeLabelModelParameterExtension) },
        { XName.Get("SmartEdgeLabelModel", YfilesNetXamlNS10), typeof(SmartEdgeLabelModel) },
        { XName.Get("SmartEdgeLabelModelParameter", YfilesNetXamlNS10), typeof(SmartEdgeLabelModelParameterExtension) },
        
        { XName.Get("StripeLabelModelParameter", YfilesNetXamlNS10), typeof(StripeLabelModelParameterExtension) },
        { XName.Get("StretchStripeLabelModelParameter", YfilesNetXamlNS10), typeof(StretchStripeLabelModelParameterExtension) }
//        ,
//
//        { XName.Get("SolidBrush", FormsNS10), typeof(SolidBrushExtension) }
      };
      handler.QueryType += (sender, args) => {
        Type result;
        if (mappings.TryGetValue(args.XmlName, out result)) {
          args.Result = result;
        }
      };
    }

    /// <summary>The namespace URI that is used by the yFiles XAML extensions.</summary>
    public const string YfilesNetXamlNS10 = "http://www.yworks.com/xml/yfiles.net/1.0/forms";
    /// <summary>The namespace URI for common yFiles extensions to GraphML.</summary>
    public const string YfilesCommonNS20 = "http://www.yworks.com/xml/yfiles-common/2.0";
    /// <summary>The namespace URI for common yFiles extensions to GraphML.</summary>
    public const string YfilesCommonMarkupNS20 = "http://www.yworks.com/xml/yfiles-common/markup/2.0";
    public const string FormsNS10 = "http://www.yworks.com/xml/yfiles-common/1.0/forms";
  }

}

namespace yWorks.GraphML
{
  /// <summary>
  /// Contains an extension method on <see cref="GraphMLIOHandler"/> to allow parsing GraphML files from earlier versions of yFiles.NET.
  /// </summary>
  public static class GraphMLCompatibilityExtensions
  {
    /// <summary>
    /// Configures a <see cref="GraphMLIOHandler"/> instance to read GraphML files written by
    /// previous versions of yFiles.NET.
    /// </summary>
    public static void ConfigureGraphMLCompatibility(this GraphMLIOHandler handler) {
      GraphMLCompatibility.ConfigureGraphMLCompatibility(handler);
    }
  }
}
