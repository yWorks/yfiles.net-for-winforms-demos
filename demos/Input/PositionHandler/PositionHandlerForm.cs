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
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.PositionHandler.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.PositionHandler
{
  /// <summary>
  /// Shows how to customize and restrict the movement behavior of nodes
  /// by implementing a custom <see cref="IPositionHandler"/>.
  /// </summary>
  public partial class PositionHandlerForm : Form
  {
    /// <summary>
    /// Shows how to register the custom <see cref="IPositionHandler"/> implementations with the <see cref="IGraph"/>'s
    /// <see cref="ILookupDecorator"/> mechanism.
    /// </summary>
    private void RegisterPositionHandler(MutableRectangle boundaryRectangle) {
      var nodeDecorator = graphControl.Graph.GetDecorator().NodeDecorator;
      nodeDecorator.PositionHandlerDecorator.SetImplementationWrapper(
        (node, delegateHandler) => {
          // Obtain the tag from the node
          object nodeTag = node.Tag;

          // Check if it is a known tag and choose the respective implementation.
          // Fallback to the default behavior otherwise.
          if (!(nodeTag is Color)) {
            return delegateHandler;
          } else if (Color.Orange.Equals(nodeTag)) {
            // This implementation delegates certain behavior to the default implementation
            return new OrangePositionHandler(boundaryRectangle, node, delegateHandler);
          } else if (Color.Firebrick.Equals(nodeTag)) {
            // A simple implementation that prohibits moving
            return new RedPositionHandler();
          } else if (Color.RoyalBlue.Equals(nodeTag)) {
            // Implementation that uses two levels of delegation to create a combined behavior
            return new OrangePositionHandler(boundaryRectangle, node, new GreenPositionHandler(delegateHandler));
          } else if (Color.Green.Equals(nodeTag)) {
            // Another implementation that delegates certain behavior to the default implementation
            return new GreenPositionHandler(delegateHandler);
          } else {
            return delegateHandler;
          }
        });

    }
    
    #region Initialization

    public PositionHandlerForm() {
      InitializeComponent();
      try {
        description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      } catch (MissingMethodException) {
        // Workaround for https://github.com/microsoft/msbuild/issues/4581
        description.Text = "The description is not available with this version of .NET Core.";
      }
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
      // don't show resize handles,
      graphEditorInputMode.ShowHandleItems = GraphItemTypes.None;
      // and enable the undo feature.
      graph.SetUndoEngineEnabled(true);

      // Finally, set the input mode to the graph control.
      graphControl.InputMode = graphEditorInputMode;

      // Create the rectangle that limits the movement of some nodes
      // and add it to the GraphControl.
      var boundaryRectangle = new MutableRectangle(20, 20, 480, 400);
      var rectangle = new RectangleVisual(boundaryRectangle) {Pen = Pens.Black};
      graphControl.RootGroup.AddChild(rectangle, CanvasObjectDescriptors.Visual);

      RegisterPositionHandler(boundaryRectangle);

      CreateSampleGraph(graph);
    }

    #endregion

    /// <summary>
    /// A <see cref="ConstrainedPositionHandler"/> that limits the movement of a
    /// node to be within an rectangle and delegates for other aspects to
    /// another (the original) handler.
    /// </summary>
    public class OrangePositionHandler : ConstrainedPositionHandler
    {
      private readonly MutableRectangle boundaryRectangle;
      private readonly INode node;
      private RectD boundaryPositionRectangle;

      public OrangePositionHandler(MutableRectangle boundaryRectangle, INode node, IPositionHandler wrappedHandler)
          : base(wrappedHandler) {
        this.boundaryRectangle = boundaryRectangle;
        this.node = node;
      }

      /// <summary>
      /// Prepares the rectangle that is actually used to limit the node
      /// position, besides the base functionality. Since a position handler
      /// works on points, the actual rectangle must be a limit for the upper
      /// left corner of the node and not for the node's bounding box.
      /// </summary>
      protected override void OnInitialized(IInputModeContext context, PointD originalLocation) {
        base.OnInitialized(context, originalLocation);
        // Shrink the retangle by the node size to get the limits for the upper left node corner
        boundaryPositionRectangle = boundaryRectangle.ToRectD() + new InsetsD(0, 0, -node.Layout.Width, -node.Layout.Height);
      }

      /// <summary>
      /// Returns the position that is constrained by the rectangle.
      /// </summary>
      protected override PointD ConstrainNewLocation(IInputModeContext context, PointD originalLocation,
                                                     PointD newLocation) {
        return newLocation.GetConstrained(boundaryPositionRectangle);
      }
    }

    /// <summary>
    /// A position handler that prevents node movements. This implementation is
    /// very simple since most methods do nothing at all.
    /// </summary>
    public class RedPositionHandler : IPositionHandler
    {
      public IPoint Location {
        get { return PointD.Origin; }
      }

      public void InitializeDrag(IInputModeContext context) {}

      public void HandleMove(IInputModeContext context, PointD originalLocation, PointD newLocation) {}

      public void CancelDrag(IInputModeContext context, PointD originalLocation) {}

      public void DragFinished(IInputModeContext context, PointD originalLocation, PointD newLocation) {}
    }

    /// <summary>
    /// A position handler that constrains the movement of a node to one axis
    /// (for each gesture) and delegates for other aspects to another (the
    /// original) handler.
    /// </summary>
    /// <remarks>
    /// Note that the simpler solution for this use case is subclassing 
    /// <see cref="ConstrainedPositionHandler"/>, however the interface is
    /// completely implemented for illustration, here.
    /// </remarks>
    public class GreenPositionHandler : IPositionHandler
    {
      private readonly IPositionHandler handler;
      private PointD lastLocation;

      public GreenPositionHandler(IPositionHandler handler) {
        this.handler = handler;
      }

      public IPoint Location {
        get { return handler.Location; }
      }

      /// <summary>
      /// Stores the initial location of the movement for reference, and calls the base method.
      /// </summary>
      public void InitializeDrag(IInputModeContext context) {
        handler.InitializeDrag(context);
        lastLocation = new PointD(handler.Location);
      }

      /// <summary>
      /// Constrains the movement to one axis. This is done by calculating the
      /// constrained location for the given new location, and invoking the
      /// original handler with the constrained location.
      /// </summary>
      public void HandleMove(IInputModeContext context, PointD originalLocation, PointD newLocation) {
        // The larger difference in coordinates specifies whether this is
        // a horizontal or vertical movement.
        PointD delta = newLocation - originalLocation;
        if (Math.Abs(delta.X) > Math.Abs(delta.Y)) {
          newLocation = new PointD(newLocation.X, originalLocation.Y);
        } else {
          newLocation = new PointD(originalLocation.X, newLocation.Y);
        }
        if (newLocation != lastLocation) {
          handler.HandleMove(context, originalLocation, newLocation);
          lastLocation = newLocation;
        }
      }

      public void CancelDrag(IInputModeContext context, PointD originalLocation) {
        handler.CancelDrag(context, originalLocation);
      }

      public void DragFinished(IInputModeContext context, PointD originalLocation, PointD newLocation) {
        handler.DragFinished(context, originalLocation, lastLocation);
      }
    }

    #region Sample Graph Creation

    private static void CreateSampleGraph(IGraph graph) {
      CreateNode(graph, 100, 100, 100, 30, Color.Firebrick, Color.WhiteSmoke, "Unmovable");
      CreateNode(graph, 300, 100, 100, 30, Color.Green, Color.WhiteSmoke, "One Axis");
      CreateNode(graph, 80, 250, 140, 40, Color.Orange, Color.Black, "Limited to Rectangle");
      CreateNode(graph, 280, 250, 140, 40, Color.RoyalBlue, Color.WhiteSmoke, "Limited to Rectangle\nand One Axis");
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

    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new PositionHandlerForm());
    }

    #endregion

  }
}
