/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.3.
 ** Copyright (c) 2000-2020 by yWorks GmbH, Vor dem Kreuzberg 28,
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
using Demo.yFiles.Graph.UMLClassStyle.Properties;
using yWorks.Annotations;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.UMLClassStyle
{
  /// <summary>
  /// A demo that shows how to create a simple class diagram editor similar to the one 
  /// bundled with Visual Studio. The demo does not allow to edit the UML features displayed 
  /// in a node, but you will be able to toggle visibility of feature sections by clicking on
  /// the open/close icons displayed inside a node.
  /// </summary>
  /// <remarks>
  /// This demo addresses various customization aspects:
  /// <list type="bullet">
  /// <item>how to create a simple UML model for classes</item>
  /// <item>how to create a UML class node representation, i.e. your own <see cref="INodeStyle"/></item>
  /// <item>how to act upon clicking on a specific region inside a node, using the <see cref="IClickHandler"/> interface</item> 
  /// <item>how to customize resize behavior and resize handle placement of nodes by decorating the lookup of the nodes</item>
  /// </list>
  /// </remarks>
  public partial class UMLClassStyleDemo : Form
  {
    public UMLClassStyleDemo() {
      InitializeComponent();
      InitializeInputModes();
      openButton.SetCommand(Commands.Open, graphControl);

      saveButton.SetCommand(Commands.SaveAs, graphControl);

      graphControl.FileOperationsEnabled = true;


      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);

      // Decorate the lookup of the nodes to change the default behavior
      // for moving, selection paint, resizing, etc.
      IGraph graph = graphControl.Graph;

      graph.SetUndoEngineEnabled(true);

      var nodeDecorator = graph.GetDecorator().NodeDecorator;

      // set a custom IPositionHandler for nodes with UMLClassStyle
      nodeDecorator.PositionHandlerDecorator.SetImplementationWrapper(
        node => node.Style is UMLClassStyle,
        (node, handler) => new UMLPositionHandler(handler, node));

      // for resizing the item using a shadow and grid
      nodeDecorator.ReshapeHandleProviderDecorator.SetImplementationWrapper(
          node => node.Style is UMLClassStyle,
          (node, provider) => {
            if (provider is ReshapeHandleProviderBase) {
              ((ReshapeHandleProviderBase) provider).HandlePositions = HandlePositions.East | HandlePositions.West;
            }
            return provider;
          });
      nodeDecorator.NodeSnapResultProviderDecorator.SetImplementation(node => node.Style is UMLClassStyle, new MyNodeSnapResultProvider());
      
      // constrain the size
      nodeDecorator.SizeConstraintProviderDecorator.SetImplementation(
        node => node.Style is UMLClassStyle,
        new NodeSizeConstraintProvider(new SizeD(100, 0), new SizeD(800, double.MaxValue)));


      // to decorate the node's lookup for types which are not provided with the GraphDecorator
      // we need to decorate the "low level" lookup decorator:
      var lookupDecorator = graph.SafeLookup<ILookupDecorator>();
      
      // set a custom IReshapeHandler
      // for dragging the item using a shadow and grid
      lookupDecorator.Add<INode, IReshapeHandler>((node, handler) => new UMLReshapeHandler(handler));

      // get the class info from the tag:
      lookupDecorator.Add<INode, ClassInfo>(node => node.Tag as ClassInfo);
      
      // register a node grid with the canvas' input mode context lookup
      GridConstraintProvider<INode> nodeGrid = new GridConstraintProvider<INode>(20);
      IContextLookupChainLink gridLink =
        Lookups.AddingLookupChainLink(typeof (IGridConstraintProvider<INode>), nodeGrid);
      graphControl.InputModeContextLookupChain.Add(gridLink);

      // remove the highlight indicator manager from the input context - disables highlight hints
      IContextLookupChainLink hidingLink = Lookups.HidingLookupChainLink(typeof (HighlightIndicatorManager<IModelItem>));
      graphControl.InputModeContextLookupChain.Add(hidingLink);

      // Create a style
      UMLClassStyle umlStyle = new UMLClassStyle();
      graph.NodeDefaults.Style = umlStyle;
      graph.NodeDefaults.Size = new SizeD(100, 100);

      // Add a sample node
      INode sampleNode = graph.CreateNode(new PointD(100, 100));
      sampleNode.Tag = CreateDefaultClassInfo();
      SizeF preferredSize = umlStyle.GetPreferredSize(sampleNode);
      graph.SetNodeLayout(sampleNode, new RectD(new PointD(100,100), preferredSize));

      // Enable clipboard
      graphControl.Clipboard = new GraphClipboard {
                                   ToClipboardCopier = {
                                         Clone = GraphCopier.CloneTypes.Tags,
                                         ReferentialIdentityTypes = GraphCopier.CloneTypes.All
                                       },
                                   FromClipboardCopier = {
                                         Clone = GraphCopier.CloneTypes.Tags,
                                         ReferentialIdentityTypes = GraphCopier.CloneTypes.All
                                       },
                                   DuplicateCopier = {
                                         Clone = GraphCopier.CloneTypes.Tags,
                                         ReferentialIdentityTypes = GraphCopier.CloneTypes.All
                                       }
                                 };

    }

    public void InitializeInputModes() {
      // Create a default editor input mode
      GraphEditorInputMode editMode = new GraphEditorInputMode();

      // then customize it to suit our needs

      // orthogonal edges
      editMode.OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext();

      // snapping
      editMode.SnapContext = new GraphSnapContext {
                                 CollectEdgeSnapLines = false,
                                 CollectNodePairSegmentSnapLines = false,
                                 CollectNodePairSnapLines = false,
                                 CollectPortSnapLines = false,
                                 SnapBendAdjacentSegments = false,
                                 SnapPortAdjacentSegments = false,
                                 SnapBendsToSnapLines = false,
                                 SnapSegmentsToSnapLines = false,
                                 GridSnapType = GridSnapTypes.All,
                                 NodeGridConstraintProvider = new GridConstraintProvider<INode>(20),
                                 GridSnapDistance = double.MaxValue,
                                 VisualizeSnapResults = false,
                               };

      
      // tweak the CreateEdgeInputMode
      editMode.CreateEdgeInputMode.ShowPortCandidates = ShowPortCandidates.None;
      editMode.CreateEdgeInputMode.SnapToTargetCandidate = false;
      
      //Enable label editing only for edges and edge labels
      editMode.LabelEditableItems = GraphItemTypes.Edge | GraphItemTypes.EdgeLabel;

      // we add a command to the KeyboardInputMode, although this will not be triggered
      // by a keyboard gesture, this has the advantage that the command is disabled
      // if any of the input modes owns the mutex.
      editMode.KeyboardInputMode.AddCommandBinding(UMLClassStyle.AdjustNodeBoundsCommand,
        delegate(object sender, ExecutedCommandEventArgs args) {
          INode node = args.Parameter as INode;
          if (node != null && node.Style is UMLClassStyle) {
            UMLClassStyle style = (UMLClassStyle) node.Style;
            var preferredSize = style.GetPreferredSize(node);
            preferredSize = new SizeD(Math.Max(node.Layout.Width, preferredSize.Width), preferredSize.Height);
            RectD newBounds = new RectD(node.Layout.GetTopLeft(), preferredSize);
            editMode.SetNodeLayout(node, newBounds);
          }
        });

      // customize the node creation
      editMode.NodeCreator = CreateNode;

      // and finally register our input mode with the control.
      graphControl.InputMode = editMode;
    }

    // Creates a node and sets it's correct size
    private static INode CreateNode(IInputModeContext context, IGraph graph, PointD location, INode parent) {
      INode node = graph.CreateNode(location);
      node.Tag = CreateDefaultClassInfo();
      var style = node.Style as UMLClassStyle;
      if (style != null) {
        SizeD size = style.GetPreferredSize(node);
        graph.SetNodeLayout(node, RectD.FromCenter(location, new SizeD(120, size.Height)));
      }
      return node;
    }

    /// <summary>
    /// Creates a default <see cref="ClassInfo"/> instance.
    /// </summary>
    private static ClassInfo CreateDefaultClassInfo() {
      ClassInfo info = new ClassInfo("Class", "MyNodeStyle");
      info.Fields.Add(new FeatureInfo(FeatureModifier.Private, "fixAspect"));
      info.Fields.Add(new FeatureInfo(FeatureModifier.Private, "pointCount"));
      info.Fields.Add(new FeatureInfo(FeatureModifier.Private, "ratio"));
      info.Fields.Add(new FeatureInfo(FeatureModifier.Private, "renderer"));
      info.Properties.Add(new FeatureInfo(FeatureModifier.Public, "FixAspect"));
      info.Properties.Add(new FeatureInfo(FeatureModifier.Public, "PointCount"));
      info.Properties.Add(new FeatureInfo(FeatureModifier.Public, "Ratio"));
      info.Properties.Add(new FeatureInfo(FeatureModifier.Public, "Renderer"));
      info.Methods.Add(new FeatureInfo(FeatureModifier.Public, "Clone"));
      info.Methods.Add(new FeatureInfo(FeatureModifier.Public, "Install"));
      return info;
    }




// TODO

      private class MyNodeSnapResultProvider : NodeSnapResultProvider
      {
        protected override void CollectGridSnapResults(GraphSnapContext context, CollectSnapResultsEventArgs args, RectD suggestedLayout, INode node) {
          AddGridSnapResult(context, args, suggestedLayout.GetTopLeft(), node);
        }
      }

















    #region Custom node resize behavior

//    private void ResizeNodeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
//      NodeControl nodeControl = e.OriginalSource as NodeControl;
//      e.CanExecute = nodeControl != null;
//      e.Handled = true;
//    }
//
//    private void ResizeNodeExecuted(object sender, ExecutedRoutedEventArgs e) {
//      NodeControl nodeControl = e.OriginalSource as NodeControl;
//      if (nodeControl != null) {
//        // parameter is desired size
//        var desiredSize = (Size?) e.Parameter;
//        if (desiredSize.HasValue) {
//          INode node = nodeControl.Item;
//          GraphEditorInputMode mode = (GraphEditorInputMode) graphControl.InputMode;
//          IRectangle layout = node.Layout;
//          // adjust only height
//          mode.SetNodeLayout(node, new RectD(layout.X, layout.Y, layout.Width, desiredSize.Value.Height));
//          e.Handled = true;
//        }
//      }
//    }

    #endregion

    #region Customized node reshape/movement handling

    public class UMLReshapeHandler : IReshapeHandler, IRectangle
    {
      private readonly IReshapeHandler originalHandler;
      private MutableRectangle simulationRectangle;
      private ICanvasObject shadowObject;
      private RectD originalBounds;

      public UMLReshapeHandler(IReshapeHandler originalHandler) {
        this.originalHandler = originalHandler;
      }

      public IRectangle Bounds {
        get { return this; }
      }

      public void InitializeReshape(IInputModeContext context) {
        simulationRectangle = new MutableRectangle(originalHandler.Bounds);
        var node = new SimpleNode {
          Layout = simulationRectangle,
          Style =
            new ShapeNodeStyle {
              Shape = ShapeNodeShape.RoundRectangle,
              Brush = Brushes.Transparent,
              Pen = new Pen(Brushes.Gray, 2)
            }
        };

        shadowObject = context.CanvasControl.RootGroup.AddChild(node, GraphModelManager.DefaultNodeDescriptor).ToFront();

        originalHandler.InitializeReshape(context);
        originalBounds = originalHandler.Bounds.ToRectD();
      }

      public void HandleReshape(IInputModeContext context, RectD originalBounds, RectD newBounds) {
        simulationRectangle.Reshape(newBounds);
      }

      public void CancelReshape(IInputModeContext context, RectD originalBounds) {
        shadowObject.Remove();
        simulationRectangle = null;
        originalHandler.CancelReshape(context, this.originalBounds);
      }

      public void ReshapeFinished(IInputModeContext context, RectD originalBounds, RectD newBounds) {
        shadowObject.Remove();
        simulationRectangle = null;
        originalHandler.HandleReshape(context, this.originalBounds, newBounds);
        originalHandler.ReshapeFinished(context, this.originalBounds, newBounds);
      }

      double ISize.Width {
        get { return (simulationRectangle ?? originalHandler.Bounds).Width; }
      }

      double ISize.Height {
        get { return (simulationRectangle ?? originalHandler.Bounds).Height; }
      }

      double IPoint.X {
        get { return (simulationRectangle ?? originalHandler.Bounds).X; }
      }

      double IPoint.Y {
        get { return (simulationRectangle ?? originalHandler.Bounds).Y; }
      }
    }

    /// <summary>
    /// A specialized position handler used for dragging ghosts of nodes on a grid.
    /// </summary>
    public class UMLPositionHandler : IPositionHandler
    {
      private readonly IPositionHandler wrappedHandler;
      private readonly INode node;
      private ICanvasObject shadowObject;
      private MutablePoint shadowLocation;
      private PointD emulatedOffset;

      public UMLPositionHandler([NotNull] IPositionHandler wrappedHandler, [NotNull] INode node) {
        this.wrappedHandler = wrappedHandler;
        this.node = node;
      }

      public IPoint Location {
        get { return wrappedHandler.Location.ToPointD() + emulatedOffset; }
      }

      public void InitializeDrag(IInputModeContext context) {
        wrappedHandler.InitializeDrag(context);
        shadowLocation = node.Layout.GetTopLeft();
        emulatedOffset = PointD.Origin;
        var dummyNode = new SimpleNode {
          Layout = new DynamicRectangle(shadowLocation, node.Layout),
          Style =
            new ShapeNodeStyle {
              Shape = ShapeNodeShape.RoundRectangle,
              Brush = Brushes.Transparent,
              Pen = new Pen(Brushes.Gray, 2)
            }
        };

        shadowObject = context.CanvasControl.RootGroup.AddChild(dummyNode, GraphModelManager.DefaultNodeDescriptor).ToFront();
      }

      public void HandleMove(IInputModeContext context, PointD originalLocation, PointD newLocation) {
        emulatedOffset = newLocation - originalLocation;
        shadowLocation.Relocate(node.Layout.GetTopLeft() + emulatedOffset);
      }

      public void CancelDrag(IInputModeContext context, PointD originalLocation) {
        wrappedHandler.CancelDrag(context, originalLocation);
        shadowObject.Remove();
        shadowObject = null;
        shadowLocation = null;
        emulatedOffset = PointD.Origin;
      }

      public void DragFinished(IInputModeContext context, PointD originalLocation, PointD newLocation) {
        wrappedHandler.HandleMove(context, originalLocation, newLocation);
        wrappedHandler.DragFinished(context, originalLocation, newLocation);
        shadowObject.Remove();
        shadowObject = null;
        shadowLocation = null;
        emulatedOffset = PointD.Origin;
      }

      /// <summary>
      /// A simple rectangle that delegates its properties to a point and size.
      /// </summary>
      private sealed class DynamicRectangle : IRectangle
      {
        private readonly IPoint location;
        private readonly ISize size;

        public DynamicRectangle(IPoint location, ISize size) {
          this.location = location;
          this.size = size;
        }

        public double X {
          get { return location.X; }
        }

        public double Y {
          get { return location.Y; }
        }

        public double Width {
          get { return size.Width; }
        }

        public double Height {
          get { return size.Height; }
        }
      }
    }

    #endregion
    
    private void ZoomInButton_Click(object sender, EventArgs e) {
      graphControl.Zoom *= 1.5f;
    }

    private void ZoomOutButton_Click(object sender, EventArgs e) {
      graphControl.Zoom /= 1.5f;
    }

    private void FitContentButton_Click(object sender, EventArgs e) {
      graphControl.FitGraphBounds();
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new UMLClassStyleDemo());
    }


  }
}
