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
using System.IO;
using System.Windows.Forms;
using System.Windows.Markup;
using Demo.yFiles.Graph.SimpleArrow.Properties;
using Demo.yFiles.Toolkit;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

[assembly:XmlnsDefinition("http://www.yworks.com/yFiles.net/demos/SimpleArrow/1.0", "Demo.yFiles.Graph.SimpleArrow")]
[assembly:XmlnsPrefix("http://www.yworks.com/yFiles.net/demos/SimpleArrow/1.0", "demo")]

namespace Demo.yFiles.Graph.SimpleArrow
{
  /// <summary>
  /// This demo shows how to create a simple custom arrow.
  /// </summary>
  public partial class SimpleArrowForm : Form
  {
    /// <summary>
    /// Wires up the UI components and adds a
    /// <see cref="GraphControl"/> to the form.
    /// </summary>
    public SimpleArrowForm() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      loadGraphMLButton.SetCommand(Commands.Open, graphControl);
      saveGraphMLButton.SetCommand(Commands.SaveAs, graphControl);
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      graphControl.FileOperationsEnabled = true;
      graphControl.InputMode = new GraphEditorInputMode();
    }

    private void InitializeStyleDefaults() {
      DemoStyles.InitDemoStyles(graphControl.Graph);

      // default edge style with the following arrow configuration
      var newEdgeColor = new SolidBrush(Color.FromArgb(0xFF, 0xab, 0x23, 0x46));
      var taperedArrow = new TaperedArrow
      {
        Width = 5,
        Length = 10,
        Fill = newEdgeColor,
      };
      graphControl.Graph.EdgeDefaults.Style = new PolylineEdgeStyle
      {
        Pen = new Pen(newEdgeColor, 5),
        SourceArrow = taperedArrow,
        TargetArrow = taperedArrow
      };
    }

    /// <summary>
    /// Shows how to use the custom arrow
    /// </summary>
    protected void InitializeGraph() {
      var node1 = Graph.CreateNode(new PointD(0, 0));
      var node2 = Graph.CreateNode(new PointD(300, 0));

      var heights = new [] { -75, -50, -25, 0, 25, 50, 95 };
      var edgeThicknesses = new[] { 1, 2, 3, 4, 5, 8, 10 };
      var edgeColor = Themes.PaletteOrange.Stroke;
      for (var i = 0; i < heights.Length; i++) {
        var height = heights[i];
        var width = edgeThicknesses[i];
        var arrow = new TaperedArrow() { Width = width, Length = 20, Fill = edgeColor };
        Graph.CreateEdge(node1, node2,
            new BridgeEdgeStyle()
            {
              Pen = new Pen(edgeColor, width),
              Height = height,
              FanLength = 0.4,
              TargetArrow = arrow,
              SourceArrow = arrow
            });
      }
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    private void OnLoaded(object src, EventArgs e) {
      InitializeStyleDefaults();

      // initialize the graph
      InitializeGraph();

      graphControl.FitGraphBounds();
    }

    /// <summary>
    /// Gets the currently registered IGraph instance from the GraphControl.
    /// </summary>
    public IGraph Graph {
      get { return graphControl.Graph; }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new SimpleArrowForm());
    }

  }
}
