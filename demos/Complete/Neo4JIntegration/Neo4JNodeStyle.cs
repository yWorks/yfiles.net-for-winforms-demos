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
using INode = yWorks.Graph.INode;
using INeo4jNode = Neo4j.Driver.INode;
using yWorks.Controls;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;
using System.Globalization;

namespace Neo4JIntegration
{
  /// <summary>
  /// A Node style which displays a Neo4J node.
  /// </summary>
  /// <remarks>
  /// Displays ID and first label and all properties.
  /// </remarks>
  public class Neo4JNodeStyle : NodeStyleBase<VisualGroup>
  {

    private readonly Pen outlinePen = Pens.DarkGray;

    /// <summary>
    /// Creates the visualization.
    /// </summary>
    /// <param name="context">The render context.</param>
    /// <param name="node">The node to render.</param>
    /// <returns>The visual representation of the node.</returns>
    protected override VisualGroup CreateVisual(IRenderContext context, INode node) {
      
      var layout = node.Layout;
      var box = layout.ToRectD();
      if (box.IsEmpty) {
        return null;
      }

      // get the Neo4J node which is represented by the node
      var item = GetItem(node);

      var group = new VisualGroup();

      // draw the background
      group.Add(new RoundedRectangleVisual(layout, 7) { Brush = Brushes.White });

      // the box is the rectangle which contains the content: reduce by insets
      box = box.GetReduced(new InsetsD(5, 10, 5, 5));

      // draw ID and first label
      box = DrawProperty(context, group, "ID", item.Id.ToString(), box);
      box = DrawProperty(context, group, "Label", item.Labels[0], box);

      // draw the properties
      foreach (var property in item.Properties) {
        box = DrawProperty(context, group, property, box);
        if (box.Y + keyFont.GetHeight() > layout.GetMaxY()) {
          // stop if the next property will be drawn outside the box
          break;
        }
      }

      // draw the outline
      group.Add(new RoundedRectangleVisual(layout, 7) { Pen = outlinePen });

      // add the type indicator stripe at the top
      var pathVisual = new GraphicsPathVisual(GetHalfRoundedRect(layout, 7));
      var headerBrush = GetHeaderBrush(item.Labels);
      pathVisual.Brush = headerBrush;
      pathVisual.Pen = new Pen(headerBrush);
      group.Add(pathVisual);

      return group;
    }

    /// <summary>
    /// Gets the preferred size, i.e. the size which is large enough to render all properties.
    /// </summary>
    /// <param name="node">The node to get the size for.</param>
    /// <returns>The preferred size</returns>
    public SizeD GetPreferredSize(INode node) {
      // the height is insets + id + label + properties
      var item = GetItem(node);
      var textHeight = keyFont.GetHeight();
      var height = 15 + (item.Properties.Count + 2) * textHeight;
      // the width is the current width
      return new SizeD(node.Layout.Width, height);
    }

    private readonly Font keyFont = new Font(new FontFamily("Arial"), 9, FontStyle.Bold);
    private readonly Font valueFont = new Font(new FontFamily("Arial"), 9, FontStyle.Regular);
    
    /// <summary>
    /// Add the visuals which render a single property.
    /// </summary>
    /// <param name="context">The current render context.</param>
    /// <param name="group">The VisualGroup to add the visuals to.</param>
    /// <param name="info">The property.</param>
    /// <param name="box">The rectangle which defines the remaining space inside the node visualization.</param>
    /// <returns>The rectangle which defines the remaining space inside the node visualization after the feature has been drawn.</returns>
    private RectD DrawProperty(IRenderContext context, VisualGroup group, KeyValuePair<string, object> info, RectD box) {
      var key = info.Key;
      var value = info.Value;
      return DrawProperty(context, @group, key, value.ToString(), box);
    }

    /// <summary>
    /// Add the visual which render a key value pair.
    /// </summary>
    /// <param name="context">The current render context.</param>
    /// <param name="group">The VisualGroup to add the visuals to.</param>
    /// <param name="key">The left string to render in bold.</param>
    /// <param name="value">The right string.</param>
    /// <param name="box">The rectangle which defines the remaining space inside the node visualization.</param>
    /// <returns>The rectangle which defines the remaining space inside the node visualization after the feature has been drawn.</returns>
    private RectD DrawProperty(IRenderContext context, VisualGroup @group, string key, string value, RectD box) {
      float height = keyFont.GetHeight();
      var textBox = new RectD(box.X, box.Y, box.Width * 0.29, height);
      DrawTrimmedString(context, @group, CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(key), keyFont, Brushes.Black, textBox);
      textBox = new RectD(box.X + box.Width * 0.31, box.Y, box.Width * 0.68, height);
      DrawTrimmedString(context, @group, value, valueFont, Brushes.Black, textBox);
      return new RectD(box.X, box.Y + height, box.Width, box.Height - height);
    }

    /// <summary>
    /// Draws the string in a way it is trimmed if the border is exceeded.
    /// </summary>
    /// <param name="context">The current render context.</param>
    /// <param name="group">The group to add the visualization to.</param>
    /// <param name="text">The text to render.</param>
    /// <param name="font">The text font.</param>
    /// <param name="brush">The text brush.</param>
    /// <param name="box">The box to fit the text in.</param>
    private static void DrawTrimmedString(IRenderContext context, VisualGroup group, string text, Font font, Brush brush, RectD box) {
      if (!box.IsEmpty) {
        group.Add(new TextVisual
        {
            Text = text,
            Font = font,
            Format = new StringFormat { Trimming = StringTrimming.EllipsisCharacter },
            Brush = brush,
            Location = box.TopLeft,
            MaximumSize = box.Size
        });
      }
    }

    #region Top stripe

    /// <summary>
    /// Gets the stripe shape at the top.
    /// </summary>
    /// <param name="rect">The rect to fit the shape in.</param>
    /// <param name="radius">The radius of the upper round corners.</param>
    /// <returns>the shape.</returns>
    private GraphicsPath GetHalfRoundedRect(IRectangle rect, float radius) {

      RectangleF baseRect = new RectangleF((float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height);
      
      float diameter = radius * 2.0F;
      SizeF sizeF = new SizeF(diameter, diameter);
      RectangleF arc = new RectangleF(baseRect.Location, sizeF);
      GraphicsPath path = new GraphicsPath();

      // top left arc 
      path.AddArc(arc, 180, 90);

      // top right arc 
      arc.X = baseRect.Right - diameter;
      path.AddArc(arc, 270, 90);

      path.CloseFigure();
      return path;
    }
    
    private static readonly Brush[] brushes = new[] { Brushes.DodgerBlue, Brushes.Orange, Brushes.Crimson, Brushes.Green, Brushes.DarkSlateBlue };

    private static Dictionary<string, Brush> seenLabels = new Dictionary<string, Brush>();

    /// <summary>
    /// Gets the brush for the header stripe.
    /// </summary>
    /// <remarks>
    /// The color is determined by the first <paramref name="labels"/>.
    /// </remarks>
    /// <param name="labels">The labels whose first element defines the color.</param>
    /// <returns>The color for the header</returns>
    public Brush GetHeaderBrush(IReadOnlyList<string> labels) {
      // Somewhat arbitrarily, use the first label as the "type" of the node and assign the same color to identical types. 
      var label = labels[0];
      Brush brush;
      if (!seenLabels.ContainsKey(label)) {
        brush = brushes[seenLabels.Count % brushes.Length];
        seenLabels[label] = brush;
      } else {
        brush = seenLabels[label];
      }
      return brush;
    }

    #endregion

    /// <summary>
    /// Get the Neo4J node which is represented by the given yFiles node.
    /// </summary>
    /// <param name="node">The node to get the item for.</param>
    /// <returns>The item for the node.</returns>
    private INeo4jNode GetItem(INode node) {
      return (INeo4jNode) node.Tag;
    }

  }
}
