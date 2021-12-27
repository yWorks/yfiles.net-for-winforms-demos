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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.ReshapeHandleProviderConfiguration.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.ReshapeHandleProviderConfiguration
{
  /// <summary>
  /// Shows how to customize the resize behavior of nodes by using customized <see cref="IReshapeHandleProvider"/>.
  /// </summary>
  public partial class ReshapeHandleProviderConfigurationForm : Form
  {

    /// <summary>
    /// Registers a callback function as decorator that provides customized <see cref="IReshapeHandleProvider"/> for each node.
    /// </summary>
    /// <remarks>
    /// This callback function is called whenever a node in the graph is queried
    /// for its <c>IReshapeHandleProvider</c>. In this case, the 'node'
    /// parameter will be set to that node.
    /// </remarks>
    public void RegisterReshapeHandleProvider(RectD boundaryRectangle) {
      var nodeDecorator = graphControl.Graph.GetDecorator().NodeDecorator;

      // deactivate reshape handling for the red node
      nodeDecorator.ReshapeHandleProviderDecorator.HideImplementation(node => Color.Firebrick.Equals(node.Tag));
      
      // return customized reshape handle provider for the orange, blue and green node
      nodeDecorator.ReshapeHandleProviderDecorator.SetFactory(
          node => Color.Orange.Equals(node.Tag) 
                  || Color.RoyalBlue.Equals(node.Tag)
                  || Color.Green.Equals(node.Tag) 
                  || Color.Purple.Equals(node.Tag) 
                  || Color.Gray.Equals(node.Tag),
          (node) => {
            // Obtain the tag from the node
            object nodeTag = node.Tag;

            // Create a default reshape handle provider for nodes
            var reshapeHandler = node.Lookup<IReshapeHandler>();
            var provider = new NodeReshapeHandleProvider(node, reshapeHandler, HandlePositions.Border);

            // Customize the handle provider depending on the node's color
            if (Color.Orange.Equals(nodeTag)) {
              // Restrict the node bounds to the boundaryRectangle
              provider.MaximumBoundingArea = boundaryRectangle;
            } else if (Color.Green.Equals(nodeTag)) {
              // Show only handles at the corners and always use aspect ratio resizing
              provider.HandlePositions = HandlePositions.Corners;
              provider.RatioReshapeRecognizer = EventRecognizers.Always;
            } else if (Color.RoyalBlue.Equals(nodeTag)) {
              // Restrict the node bounds to the boundaryRectangle and
              // show only handles at the corners and always use aspect ratio resizing
              provider.MaximumBoundingArea = boundaryRectangle;
              provider.HandlePositions = HandlePositions.Corners;
              provider.RatioReshapeRecognizer = EventRecognizers.Always;
            } else if (Color.Purple.Equals(nodeTag)) {
              provider = new PurpleNodeReshapeHandleProvider(node, reshapeHandler);
            } else if (Color.Gray.Equals(nodeTag)) {
              provider.HandlePositions = HandlePositions.SouthEast;
              provider.CenterReshapeRecognizer = EventRecognizers.Always;
            }
            return provider;
          });
    }

    #region Initialization

    public ReshapeHandleProviderConfigurationForm() {
      InitializeComponent();
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    private void FormLoaded(object sender, EventArgs e) {
      IGraph graph = graphControl.Graph;

      // Create a default editor input mode
      GraphEditorInputMode graphEditorInputMode = new GraphEditorInputMode();

      // Just for user convenience: disable node and edge creation,
      graphEditorInputMode.AllowCreateEdge = false;
      graphEditorInputMode.AllowCreateNode = false;
      // disable deleting items
      graphEditorInputMode.DeletableItems = GraphItemTypes.None;
      // disable node moving
      graphEditorInputMode.MovableItems = GraphItemTypes.None;
      // enable the undo feature
      graph.SetUndoEngineEnabled(true);

      // Finally, set the input mode to the graph control.
      graphControl.InputMode = graphEditorInputMode;

      // Create the rectangle that limits the movement of some nodes
      // and add it to the GraphControl.
      var boundaryRectangle = new RectD(20, 20, 480, 550);
      var rectangle = new RectangleVisual(boundaryRectangle) {Pen = Pens.Black};
      graphControl.RootGroup.AddChild(rectangle, CanvasObjectDescriptors.Visual);

      RegisterReshapeHandleProvider(boundaryRectangle);

      CreateSampleGraph(graph);
    }

    #endregion

    #region Sample Graph Creation

    /// <summary>
    /// Creates the sample graph of this demo.
    /// </summary>
    private static void CreateSampleGraph(IGraph graph) {
      CreateNode(graph, 80, 100, 140, 30, Color.Firebrick, Color.WhiteSmoke, "Fixed Size");
      CreateNode(graph, 300, 100, 140, 30, Color.Green, Color.WhiteSmoke, "Keep Aspect Ratio");
      CreateNode(graph, 80, 250, 140, 50, Color.Gray, Color.WhiteSmoke, "Keep Center");
      CreateNode(graph, 300, 250, 140, 50, Color.Purple, Color.WhiteSmoke, "Keep Aspect Ratio\nat corners");
      CreateNode(graph, 80, 410, 140, 30, Color.Orange, Color.Black, "Limited to Rectangle");
      CreateNode(graph, 300, 400, 140, 50, Color.RoyalBlue, Color.WhiteSmoke, "Limited to Rectangle\nand Keep Aspect Ratio");
    }

    /// <summary>
    /// Creates a sample node for this demo.
    /// </summary>
    private static void CreateNode(IGraph graph, double x, double y, double w, double h, Color fillColor, Color textColor, string labelText) {
      var whiteTextLabelStyle = new DefaultLabelStyle { TextBrush = new SolidBrush(textColor) };
      INode node = graph.CreateNode(new RectD(x, y, w, h), new ShinyPlateNodeStyle { Brush = new SolidBrush(fillColor) }, fillColor);
      graph.SetStyle(graph.AddLabel(node, labelText), whiteTextLabelStyle);
    }

    #endregion

    #region Custom NodeReshapeHandleProvider

    /// <summary>
    /// A NodeReshapeHandleProvider for purple nodes that provides different handles for corners and borders.
    /// </summary>
    private class PurpleNodeReshapeHandleProvider : NodeReshapeHandleProvider {
      public PurpleNodeReshapeHandleProvider(INode node, IReshapeHandler reshapeHandler) 
          : base(node, reshapeHandler, HandlePositions.Border) { }

      public override IHandle GetHandle(IInputModeContext inputModeContext, HandlePositions position) {
        var handle = new NodeReshapeHandlerHandle(Node, ReshapeHandler, position);

        var atCorner = (position & HandlePositions.Corners) != HandlePositions.None;
        if (atCorner) {
          // handles at corners shall always keep the aspect ratio
          handle.ReshapePolicy = ReshapePolicy.Projection;
          handle.RatioReshapeRecognizer = EventRecognizers.Always;
          handle.Type = HandleTypes.Resize;
        } else {
          // handles at the sides shall ignore the aspect ratio and use another handle visualization
          handle.ReshapePolicy = ReshapePolicy.None;
          handle.RatioReshapeRecognizer = EventRecognizers.Never;
          handle.Type = HandleTypes.Warp;
        }

        return handle;
      }
    }

    #endregion


    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ReshapeHandleProviderConfigurationForm());
    }

    #endregion

  }
}
