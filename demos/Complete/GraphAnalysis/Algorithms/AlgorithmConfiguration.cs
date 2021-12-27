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
using System.Linq;
using yWorks.Analysis;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Algorithms.GraphAnalysis
{

  /// <summary>
  /// Base class for algorithm configurations.
  /// </summary>
  /// <remarks>
  /// Contains code to run a specific algorithm and to display the result.
  /// Subclasses have to implement <see cref="RunAlgorithm"/>
  /// to both do the actual calculations and mark the result.
  /// </remarks>
  public abstract class AlgorithmConfiguration
  {
    /// <summary>
    /// Whether the graph is considered as directed.
    /// </summary>
    public bool Directed { get; set; }

    /// <summary>
    /// Whether the algorithm supports both directed and undirected graphs.
    /// </summary>
    public virtual bool SupportsDirectedAndUndirected {
      get { return false; }
    }

    /// <summary>
    /// Whether the current algorithm supports edge weights.
    /// </summary>
    public virtual bool SupportsWeights {
      get { return false; }
    }

    /// <summary>
    /// Whether to use uniform edge weights.
    /// </summary>
    public bool UseUniformWeights { get; set; }

    public DictionaryMapper<INode, bool> IncrementalElements { get; set; }
    
    public bool EdgeRemoved { get; set; }

    /// <summary>
    /// Apply the algorithm.
    /// </summary>
    public void Apply(GraphControl graphControl) {
      RunAlgorithm(graphControl.Graph);
    }

    /// <summary>
    /// Populate the context menu specifically for the current algorithm.
    /// </summary>
    public virtual void PopulateContextMenu(PopulateItemContextMenuEventArgs<IModelItem> args) { }

    /// <summary>
    /// Run the actual algorithm.
    /// </summary>
    public abstract void RunAlgorithm(IGraph graph);

    /// <summary>
    /// Calculates the weight for the given edge.
    /// </summary>
    /// <remarks>
    /// If <see cref="UseUniformWeights"/> is set to true 1 is returned.
    /// If the edge has labels the double value of the first label is parsed and returned.
    ///   If the label cannot be parsed into a double value 0 is returned.
    /// Otherwise the length of the edge path is returned.
    /// </remarks>
    /// <param name="edge">The edge to calculate the weight for.</param>
    /// <returns>The edge weight.</returns>
    public double GetEdgeWeight(IEdge edge) {
      if (UseUniformWeights) {
        return 1;
      }

      // if edge has at least one label ...
      if (edge.Labels.Any()) {
        double edgeWeight;
        // try to return its value
        return double.TryParse(edge.Labels[0].Text, out edgeWeight) ? edgeWeight : 0;
      }

      // calculate geometric edge length
      return edge.Style.Renderer.GetPathGeometry(edge, edge.Style).GetPath().GetLength();
    }

    /// <summary>
    /// Generated a set of colors.
    /// </summary>
    /// <param name="gradient">Whether to generate a gradient of blue colors. If false a prepared set of colors is returned.</param>
    /// <param name="count">The number of gradient steps. Is ignored if <paramref name="gradient"/> is false.</param>
    /// <param name="lightToDark">Whether the gradient is generated from light to dark colors.</param>
    /// <returns></returns>
    protected Color[] GenerateColors(bool gradient, int count = 0, bool lightToDark = false) {
      if (gradient) {
        var colors = new Color[count];
        float stepCount = count - 1;
        var c1 = Color.LightBlue;
        var c2 = Color.Blue;

        for (int i = 0; i < count; i++) {
          float r = c1.R * (((stepCount - i) / stepCount)) + c2.R * (i / stepCount);
          float g = c1.G * (((stepCount - i) / stepCount)) + c2.G * (i / stepCount);
          float b = c1.B * (((stepCount - i) / stepCount)) + c2.B * (i / stepCount);
          colors[i] = Color.FromArgb((int) r, (int) g, (int) b);
        }

        if (lightToDark) {
          colors = colors.Reverse().ToArray();
        }

        return colors;
      }

      return new[] {
          Color.RoyalBlue,
          Color.Gold,
          Color.Crimson,
          Color.DarkTurquoise,
          Color.CornflowerBlue,
          Color.DarkSlateBlue,
          Color.OrangeRed,
          Color.MediumSlateBlue,
          Color.ForestGreen,
          Color.MediumVioletRed,
          Color.DarkCyan,
          Color.Chocolate,
          Color.Orange,
          Color.LimeGreen,
          Color.MediumOrchid
      };
    }

    /// <summary>
    /// Returns the set of components which are associated with the nodes in the <see cref="IncrementalElements"/>.
    /// </summary>
    /// <param name="components">The components to get the affected components from.</param>
    /// <returns>The set of components which are associated with the nodes in the <see cref="IncrementalElements"/>.</returns>
    public ISet<Component> GetAffectedNodeComponents(IMapper<INode, Component> components) {
      var affectedComponents = new HashSet<Component>();

      if (IncrementalElements != null) {
        foreach (var pair in IncrementalElements.Entries) {
          if (components != null) {
            var node = pair.Key;
            if (node != null) {
              var component = components[node];
              affectedComponents.Add(component);
            }
          }
        }
      }
      return affectedComponents;
    }


    public Color DetermineElementColor(Color[] colors, Component component, ISet<Component> affectedComponents,
        Dictionary<Component, Color> color2AffectedComponent, Component largestComponent,
        ResultItemCollection<Component> allComponents, IGraph graph, IModelItem element) {
      var componentId = allComponents.ToList().IndexOf(component);
      if (null == IncrementalElements) {
        return colors[componentId % colors.Length];
      }
      var currentColor = ((Tag)element.Tag).CurrentColor;
      if (affectedComponents.Contains(component)) {
        if (!color2AffectedComponent.ContainsKey(component)) {
          Color l;
          if (largestComponent == component && 1 != component.InducedEdges.Count) {
            l = GenerateMajorColor(component);
          } else
            l = largestComponent == component && 1 == component.InducedEdges.Count
                ? HasValidColorTag(element)
                    ? (Color)element.Tag
                    : GenerateUniqueColor(graph, colors)
                : element is IEdge
                    ? (IncrementalElements[((IEdge)element).GetSourceNode()] && IncrementalElements[((IEdge)element).GetTargetNode()])
                        ? GenerateUniqueColor(graph, colors)
                        : !EdgeRemoved && element.Tag is Tag && currentColor.HasValue
                            ? currentColor.Value
                            : GenerateUniqueColor(graph, colors)
                    : !EdgeRemoved && element.Tag is Tag && currentColor.HasValue
                        ? currentColor.Value
                        : GenerateUniqueColor(graph, colors);
          color2AffectedComponent[component] = l;
        }
        return color2AffectedComponent[component];
      }
      return currentColor.Value;
    }
    
    /// <summary>
    /// Gets the largest component, i.e. the component with the largest number of edges and nodes.
    /// </summary>
    /// <param name="affectedComponents">The components to get the largest one from.</param>
    /// <returns>The largest component.</returns>
    protected Component GetLargestComponent(ISet<Component> affectedComponents) {
      Component largest = null;
      int largestCount = 0;
      foreach (var component in affectedComponents) {
        var count = component.Nodes.Count + component.InducedEdges.Count;
        if (count > largestCount) {
          largestCount = count;
          largest = component;
        }
      }
      return largest;
    }
    
    private Color GenerateMajorColor(Component component) {
      var color2Frequency = new Dictionary<Color, int>();
      // finds the colors of the nodes in the current component
      foreach (var node in component.Nodes) {
        var tag = node.Tag as Tag;
        if (tag != null && tag.CurrentColor.HasValue) {
          var color = tag.CurrentColor.Value;
          int frequency;
          if (color2Frequency.TryGetValue(color, out frequency)) {
            color2Frequency[color] = frequency + 1;
          } else {
            color2Frequency[color] = 1;
          }
        }
      }

      // finds the color with the maximum frequency
      int maxFrequency = 0;
      Color colorWithMaxFrequency = Color.White;
      foreach (var pair in color2Frequency) {
        if (maxFrequency < pair.Value) {
          maxFrequency = pair.Value;
          colorWithMaxFrequency = pair.Key;
        }
      }

      return colorWithMaxFrequency;
    }

    private Color GenerateUniqueColor(IGraph graph, Color[] colors) {
      var existingColors = new HashSet<Color>();

      foreach (var node in graph.Nodes) {
        if (HasValidColorTag(node)) {
          existingColors.Add(((Tag) node.Tag).CurrentColor.Value);
        }
      }

      foreach (var edge in graph.Edges) {
        if (HasValidColorTag(edge)) {
          existingColors.Add(((Tag) edge.Tag).CurrentColor.Value);
        }
      }

      if (existingColors.Count >= colors.Length) {
        return colors[new Random(42).Next(colors.Length)];
      }

      for (var i = 0; i < colors.Length; i++) {
        if (!existingColors.Contains(colors[i])) {
          return colors[i];
        }
      }
      return Color.White;
    }

    static bool HasValidColorTag(IModelItem element) {
      return element.Tag is Color;
    }

    /// <summary>
    /// Resets the the styles and tags and removes all markers.
    /// </summary>
    /// <param name="graph">The graph to reset.</param>
    public void ResetGraph(IGraph graph) {
      foreach (var node in graph.Nodes) {
        // reset size
        graph.SetNodeLayout(node, new RectD(node.Layout.GetTopLeft(), graph.NodeDefaults.Size));
        // reset style
        graph.SetStyle(node, graph.NodeDefaults.Style);
        // reset the tag
        node.Tag = new Tag();
        // remove labels
        foreach (var label in node.Labels.Where(l => Equals(l.Tag, "Centrality")).ToList()) {
          graph.Remove(label);
        }
      }

      var arrow = Arrows.Default;
      var defaultEdgeStyle = graph.EdgeDefaults.Style as PolylineEdgeStyle;
      if (defaultEdgeStyle != null) {
        defaultEdgeStyle.TargetArrow = Directed ? arrow : Arrows.None;

        foreach (var edge in graph.Edges) {
          // reset style
          graph.SetStyle(edge, defaultEdgeStyle);
          // reset the tag
          edge.Tag = new Tag { Directed = SupportsDirectedAndUndirected && Directed };
          // remove labels
          foreach (var label in edge.Labels.Where(l => Equals(l.Tag, "Centrality")).ToList()) {
            graph.Remove(label);
          }
        }
      }
    }
  }

  /// <summary>
  /// Custom edge style renderer which uses the edge's <see cref="ITagOwner.Tag"/> to determine how to render the edge.
  /// </summary>
  public class AnalysisPolylineEdgeStyleRenderer : PolylineEdgeStyleRenderer
  {
    protected override Pen GetPen() {
      const float thickness = 5;
      var tag = Edge.Tag as Tag;
      if (tag != null) {
        if (tag.CurrentColor != null) {
          return new Pen(new SolidBrush((Color) tag.CurrentColor), thickness);
        }
        if (tag.GradientValue != null) {
          var v = Math.Min(1, Math.Max(0, (float) (double) tag.GradientValue));
          var c1 = Color.LightBlue;
          var c2 = Color.Blue;
          Color color;

          if (tag.LightToDark) {
            float r = c1.R * (1 - v) + c2.R * v;
            float g = c1.G * (1 - v) + c2.G * v;
            float b = c1.B * (1 - v) + c2.B * v;
            color = Color.FromArgb((int) r, (int) g, (int) b);

          } else {
            float r = c2.R * (1 - v) + c1.R * v;
            float g = c2.G * (1 - v) + c1.G * v;
            float b = c2.B * (1 - v) + c1.B * v;

            color = Color.FromArgb((int) r, (int) g, (int) b);
          }


          return new Pen(color, thickness);
        }
      }

      return Pens.Black;
    }

    protected override IArrow GetTargetArrow() {
      var tag = Edge.Tag as Tag;
      if (tag != null && tag.Directed) {
        if (tag.CurrentColor != null) {
          return new Arrow((Color) tag.CurrentColor) { Type = ArrowType.Default, Pen = null };
        }
        return new Arrow(Color.Black) { Type = ArrowType.Default, Pen = null };
      }

      return Arrows.None;
    }
  }

  /// <summary>
  /// Custom node style renderer which uses the node's <see cref="ITagOwner.Tag"/> to determine how to render the node.
  /// </summary>
  public class AnalysisShapeNodeStyleRenderer : ShapeNodeStyleRenderer
  {
    protected override Brush GetBrush() {
      var tag = Node.Tag as Tag;
      if (tag != null) {
        if (tag.IsSource || tag.IsTarget) {
          return Brushes.White;
        }
        if (tag.CurrentColor != null) {
          return new SolidBrush((Color) tag.CurrentColor);
        }
        if (tag.GradientValue != null) {
          var v = Math.Min(1, Math.Max(0, (float) (double) tag.GradientValue));
          var c1 = Color.LightBlue;
          var c2 = Color.Blue;
          Color color;

          if (tag.LightToDark) {
            float r = c1.R * (1 - v) + c2.R * v;
            float g = c1.G * (1 - v) + c2.G * v;
            float b = c1.B * (1 - v) + c2.B * v;
            color = Color.FromArgb((int) r,(int) g,(int) b);
            
          } else {
            float r = c2.R * (1 - v) + c1.R * v;
            float g = c2.G * (1 - v) + c1.G * v;
            float b = c2.B * (1 - v) + c1.B * v;

            color = Color.FromArgb((int) r, (int) g, (int) b);
          }

          return new SolidBrush(color);
        }
      }

      return Brushes.LightGray;
    }

    protected override Pen GetPen() {
      const float thickness = 5;
      var tag = Node.Tag as Tag;
      if (tag != null) {
        if (tag.IsSource && tag.IsTarget) {
          return new Pen(new HatchBrush(HatchStyle.LargeCheckerBoard, Color.IndianRed, Color.YellowGreen), thickness);
        }
        if (tag.IsSource) {
          return new Pen(Brushes.YellowGreen, thickness);
        }
        if (tag.IsTarget) {
          return new Pen(Brushes.IndianRed, thickness);
        }
        if (tag.CurrentColor != null) {
          return null;
        }
        if (tag.GradientValue != null) {
          return Pens.Black;
        }
      }

      return Pens.Black;
    }
  }

  /// <summary>
  /// Used to set different properties to highlight a model item.
  /// </summary>
  /// <remarks>
  /// Has to be set as <see cref="ITagOwner.Tag"/> of the model item to highlight.
  /// </remarks>
  public class Tag
  {
    public IEnumerable<ColorGroup> ColorGroups { get; set; }
    public double? GradientValue { get; set; }
    public bool IsSource { get; set; }
    public bool IsTarget { get; set; }
    public Color? CurrentColor { get; set; }

    public bool Directed { get; set; }

    public bool LightToDark { get; set; }
  }

  public class ColorGroup
  {
    private static readonly Color[] colors = {
        Color.RoyalBlue,
        Color.Gold,
        Color.Crimson,
        Color.DarkTurquoise,
        Color.CornflowerBlue,
        Color.DarkSlateBlue,
        Color.OrangeRed,
        Color.MediumSlateBlue,
        Color.ForestGreen,
        Color.MediumVioletRed,
        Color.DarkCyan,
        Color.Chocolate,
        Color.Orange,
        Color.LimeGreen,
        Color.MediumOrchid
    };

    public int Index { get; private set; }

    public ColorGroup(int index) {
      Index = index;
    }

    public Color Color {
      get { return colors[Index % colors.Length]; }
    }
  }
}
