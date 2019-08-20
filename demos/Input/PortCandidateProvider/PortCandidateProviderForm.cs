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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.PortCandidateProvider.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.PortCandidateProvider
{
  /// <summary>
  /// Shows how to customize the port relocation feature
  /// by implementing a custom <see cref="IPortCandidateProvider"/>.
  /// </summary>
  public partial class PortCandidateProviderForm : Form
  {
    
    /// <summary>
    /// Registers a callback function as decorator that provides a custom
    /// <see cref="IPortCandidateProvider"/> for each node.
    /// </summary>
    /// <remarks>
    /// This callback function is called whenever a node in the graph is queried
    /// for its <c>IPortCandidateProvider</c>. In this case, the 'node'
    /// parameter will be set the that node.
    /// </remarks>
    private void RegisterPortCandidateProvider() {
      var nodeDecorator = graphControl.Graph.GetDecorator().NodeDecorator;
      nodeDecorator.PortCandidateProviderDecorator.SetFactory(
        node => {
          // Obtain the tag from the edge
          object nodeTag = node.Tag;

          // Check if it is a known tag and choose the respective implementation
          if (!(nodeTag is Color)) {
            return null;
          } else if (Color.Firebrick.Equals(nodeTag)) {
            return new RedPortCandidateProvider(node);
          } else if (Color.RoyalBlue.Equals(nodeTag)) {
            return new BluePortCandidateProvider(node);
          } else if (Color.Green.Equals(nodeTag)) {
            return new GreenPortCandidateProvider(node);
          } else if (Color.Orange.Equals(nodeTag)) {
            return new OrangePortCandidateProvider(node);
          } else if (Color.Purple.Equals(nodeTag)) {
            return new PurplePortCandidateProvider(node);
          } else {
            // otherwise revert to default behavior
            return null;
          }
        });    
    }

    #region Initialization

    public PortCandidateProviderForm() {
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

      // Disable automatic cleanup of unconnected ports since some nodes have a predefined set of ports
      graph.NodeDefaults.Ports.AutoCleanUp = false;

      // Create a default editor input mode and configure it
      GraphEditorInputMode graphEditorInputMode = new GraphEditorInputMode();
      graphEditorInputMode.CreateEdgeInputMode.UseHitItemsCandidatesOnly = true;

      // Just for user convenience: disable node creation,
      graphEditorInputMode.AllowCreateNode = false;
      // allow edge deletion only
      graphEditorInputMode.DeletableItems = GraphItemTypes.Edge;
      // and enable the undo feature.
      graph.SetUndoEngineEnabled(true);

      // Finally, set the input mode to the graph control.
      graphControl.InputMode = graphEditorInputMode;

      RegisterPortCandidateProvider();  
      
      CreateSampleGraph();
    }

    #endregion

    /// <summary>
    /// This port candidate provider always returns an invalid port candidate
    /// and thus prevents edge creation.
    /// </summary>
    public class RedPortCandidateProvider : PortCandidateProviderBase
    {
      private readonly INode node;

      public RedPortCandidateProvider(INode node) {
        this.node = node;
      }

      /// <summary>
      /// Returns a list with a single invalid port candidate. This candidate is
      /// located in the center of the node to display the invalid port
      /// highlight at that location.
      /// </summary>
      /// <remarks>
      /// Note that the various variants of getPortCandidates of
      /// <see cref="PortCandidateProviderBase"/> delegate to this method.
      /// This can be used to provide the same candidates for all use-cases.
      /// </remarks>
      protected override IEnumerable<IPortCandidate> GetPortCandidates(IInputModeContext context) {
        List<IPortCandidate> candidates = new List<IPortCandidate>();
        candidates.Add(new DefaultPortCandidate(node, FreeNodePortLocationModel.NodeCenterAnchored)
                         { Validity = PortCandidateValidity.Invalid });
        return candidates;
      }
    }

    /// <summary>
    /// This port candidate provider provides port candidates for the 
    /// ports of a node. If a port already has a connected edge, its
    /// port candidate is marked as invalid.
    /// </summary>
    public class BluePortCandidateProvider : PortCandidateProviderBase
    {
      private readonly INode node;

      public BluePortCandidateProvider(INode node) {
        this.node = node;
      }

      /// <summary>
      /// Returns a list that contains a port candidate for each of the node's 
      /// ports. Each candidate has the same location as the port. If a port
      /// already has a connected edge, its port candidate is marked as invalid.
      /// </summary>
      /// <remarks>
      /// Note that the various variants of getPortCandidates of
      /// <see cref="PortCandidateProviderBase"/> delegate to this method.
      /// This can be used to provide the same candidates for all use-cases.
      /// </remarks>
      protected override IEnumerable<IPortCandidate> GetPortCandidates(IInputModeContext context) {
        List<IPortCandidate> candidates = new List<IPortCandidate>();
        var graph = context.GetGraph();
        
        // Create the candidate for each port
        if (graph != null) {
          foreach (IPort port in node.Ports) {
            DefaultPortCandidate portCandidate = new DefaultPortCandidate(port);
            portCandidate.Validity = graph.Degree(port) == 0 ? PortCandidateValidity.Valid : PortCandidateValidity.Invalid;
            candidates.Add(portCandidate);
          }
        }
        
        // If no candidates have been created so far, create a single invalid candidate as fallback
        if (candidates.Count == 0) {
          DefaultPortCandidate item = new DefaultPortCandidate(node, FreeNodePortLocationModel.NodeCenterAnchored);
          item.Validity = PortCandidateValidity.Invalid;
          candidates.Add(item);
        }

        return candidates;
      }
    }

    /// <summary>
    /// This port candidate provider only allows connections from green nodes.
    /// To achieve this, this class returns different port candidates for source
    /// and target ports.
    /// </summary>
    public class GreenPortCandidateProvider : PortCandidateProviderBase
    {
      private readonly INode node;

      public GreenPortCandidateProvider(INode node) {
        this.node = node;
      }

      /// <summary>
      /// Returns a central port candidate if the owner node of the source
      /// candidate is green, and an empty list otherwise.
      /// </summary>
      public override IEnumerable<IPortCandidate> GetTargetPortCandidates(IInputModeContext context, IPortCandidate source) {
        // Check if the source node is green
        if (Color.Green.Equals(source.Owner.Tag)) {
          return PortCandidateProviders.FromNodeCenter(node).GetTargetPortCandidates(context, source);
        } else {
          return new IPortCandidate[0];
        }
      }
      
      /// <summary>
      /// Returns an empty list.
      /// </summary>
      public override IEnumerable<IPortCandidate> GetTargetPortCandidates(IInputModeContext context) {
        return new IPortCandidate[0];
      }

      /// <summary>
      /// Returns a list that contains a port candidate for each of the node's 
      /// ports. Each candidate has the same location as the port. If a port
      /// already has a connected edge, its port candidate is marked as invalid.
      /// </summary>
      /// <remarks>
      /// Note that the variants of getPortCandidates for target ports are all
      /// implemented by this class. Therefore, this method is only used for
      /// source ports.
      /// </remarks>
      protected override IEnumerable<IPortCandidate> GetPortCandidates(IInputModeContext context) {
        List<IPortCandidate> candidates = new List<IPortCandidate>();
        bool hasValid = false;
        var graph = context.GetGraph();
        if (graph != null) {
          // Create a port candidate for each port on the node
          foreach (IPort port in node.Ports) {
            DefaultPortCandidate portCandidate = new DefaultPortCandidate(port);
            bool valid = graph.OutDegree(port) == 0;
            hasValid |= valid;
            portCandidate.Validity = valid ? PortCandidateValidity.Valid : PortCandidateValidity.Invalid;
            candidates.Add(portCandidate);
          }
        }

        // If no valid candidates have been created so far, use the ShapeGeometryPortCandidateProvider as fallback.
        // This provides a candidate in the middle of each of the four sides of the node.
        if (!hasValid) {
          candidates.AddRange(PortCandidateProviders.FromShapeGeometry(node).GetSourcePortCandidates(context));
        }
        return candidates;
      }
    }

    /// <summary>
    /// This port candidate provider uses dynamic port candidates that allow 
    /// any location inside the node.
    /// </summary>
    public class OrangePortCandidateProvider : PortCandidateProviderBase
    {
      private readonly INode node;

      public OrangePortCandidateProvider(INode node) {
        this.node = node;
      }

      /// <summary>
      /// Returns a list that contains a single dynamic port candidate. That candidate
      /// allows any location inside the node layout.
      /// </summary>
      /// <remarks>
      /// Note that the various variants of getPortCandidates of
      /// <see cref="PortCandidateProviderBase"/> delegate to this method. This can be
      /// used to provide the same candidates for all use-cases.
      /// </remarks>
      protected override IEnumerable<IPortCandidate> GetPortCandidates(IInputModeContext context) {
        var list = new List<IPortCandidate>();
        list.Add(new DefaultPortCandidate(node, FreeNodePortLocationModel.Instance));
        return list;
      }
    }

    /// <summary>
    /// This port candidate provider allows only connections to port candidates 
    /// that have the same color as the edge's source port or source node.
    /// </summary>
    private class PurplePortCandidateProvider : PortCandidateProviderBase {
      private readonly INode node;

      public PurplePortCandidateProvider(INode node) {
        this.node = node;
      }

      /// <summary>
      /// Returns all port candidates that apply for the provided opposite port candidate.
      /// </summary>
      /// <remarks>
      /// In this implementation only source ports with the same color and source ports of nodes 
      /// with the same color as the target port are accepted.
      /// </remarks>
      public override IEnumerable<IPortCandidate> GetTargetPortCandidates(IInputModeContext context, IPortCandidate source) {
        List<IPortCandidate> candidates = new List<IPortCandidate>();
        var graph = context.GetGraph();
        if (graph != null) {
          foreach (IPort port in node.Ports) {
            DefaultPortCandidate portCandidate = new DefaultPortCandidate(port);
            var sourcePort = source.CreatePort(context);
            bool valid = port.Tag.Equals(source.Owner.Tag) || sourcePort != null && port.Tag.Equals(sourcePort.Tag);
            portCandidate.Validity = valid ? PortCandidateValidity.Valid : PortCandidateValidity.Invalid;
            candidates.Add(portCandidate);
          }
        }

        return candidates;
      }

      /// <summary>
      /// Creates an enumeration of possible port candidates, in this 
      /// case one port candidate for each of the node's 
      /// ports in the same location as the port.
      /// </summary>
      /// <remarks>
      /// This method is used to provide the same candidates for all
      /// use-cases. It is used as a fallback if methods <c>GetSourcePortCandidates()</c>
      /// and <c>GetTargetPortCandidates()</c> aren't implemented.
      /// </remarks>
      protected override IEnumerable<IPortCandidate> GetPortCandidates(IInputModeContext context) {
        List<IPortCandidate> candidates = new List<IPortCandidate>();
        var graph = context.GetGraph();
        if (graph != null) {
          foreach (IPort port in node.Ports) {
            DefaultPortCandidate portCandidate = new DefaultPortCandidate(port);
            candidates.Add(portCandidate);
          }
        }
        if (candidates.Count == 0) {
          DefaultPortCandidate item = new DefaultPortCandidate(node, FreeNodePortLocationModel.NodeCenterAnchored);
          item.Validity = PortCandidateValidity.Invalid;
          candidates.Add(item);
        }

        return candidates;
      }
    }

    #region Sample Graph Creation

    private void CreateSampleGraph() {
      IGraph graph = graphControl.Graph;

      CreateNode(graph, 100, 100, 80, 30, Color.Firebrick, "No Edge");
      CreateNode(graph, 350, 100, 80, 30, Color.Green, "Green Only");
      CreateNode(graph, 100, 200, 80, 30, Color.Green, "Green Only");
      CreateNode(graph, 350, 200, 80, 30, Color.Firebrick, "No Edge");

      // The blue nodes have predefined ports
      var portStyle = new ColorPortStyle();

      var blue1 = CreateNode(graph, 100, 300, 80, 30, Color.RoyalBlue, "One   Port");
      graph.AddPort(blue1, blue1.Layout.GetCenter(), portStyle).Tag = Color.Black;

      var blue2 = CreateNode(graph, 350, 300, 100, 100, Color.RoyalBlue, "Many Ports");
      var portCandidateProvider = PortCandidateProviders.FromShapeGeometry(blue2, 0, 0.25, 0.5, 0.75);
      portCandidateProvider.Style = portStyle;
      portCandidateProvider.Tag = Color.Black;
      var candidates = portCandidateProvider.GetSourcePortCandidates(graphControl.InputModeContext);
      foreach (IPortCandidate portCandidate in candidates) {
        if (portCandidate.Validity != PortCandidateValidity.Dynamic) {
          portCandidate.CreatePort(graphControl.InputModeContext);
        }
      }

      // The orange node
      CreateNode(graph, 100, 400, 100, 100, Color.Orange, "Dynamic Ports");

      INode n = CreateNode(graph, 100, 540, 100, 100, Color.Purple, "Individual\nPort Constraints");
      AddIndividualPorts(graph, n);

      n = CreateNode(graph, 350, 540, 100, 100, Color.Purple, "Individual\nPort Constraints");
      AddIndividualPorts(graph, n);
    }

    /// <summary>
    /// Creates a sample node for this demo.
    /// </summary>
    private static INode CreateNode(IGraph graph, double x, double y, double w, double h, Color color, string labelText) {
      var whiteTextLabelStyle = new DefaultLabelStyle { TextBrush = Brushes.White };
      INode node = graph.CreateNode(new RectD(x, y, w, h), new ShinyPlateNodeStyle{Brush = new SolidBrush(color)}, color);
      graph.SetStyle(graph.AddLabel(node, labelText), whiteTextLabelStyle);
      return node;
    }

    /// <summary>
    /// Adds ports with different colors to the node.
    /// </summary>
    private void AddIndividualPorts(IGraph graph, INode node) {
      var portStyle = new ColorPortStyle();
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(0.25, 0)), portStyle, Color.Firebrick);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(0.75, 0)), portStyle, Color.Green);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(0, 0.25)), portStyle, Color.Black);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(0, 0.75)), portStyle, Color.Black);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(1, 0.25)), portStyle, Color.RoyalBlue);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(1, 0.75)), portStyle, Color.Orange);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(0.25, 1)), portStyle, Color.Purple);
      graph.AddPort(node, FreeNodePortLocationModel.Instance.CreateParameter(new PointD(0.75, 1)), portStyle, Color.Purple);
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
      Application.Run(new PortCandidateProviderForm());
    }

    #endregion

    }
  #region Port Style

  /// <summary>
  /// A very simple port style implementation that uses the color in the port's tag.
  /// </summary>
  internal class ColorPortStyle : PortStyleBase<IVisual> {
    public ColorPortStyle() : this(6) {
    }

    public ColorPortStyle(int renderSize) {
      this.renderSize = renderSize;
      this.renderSizeHalf = renderSize*0.5;
    }

    private readonly int renderSize;
    private readonly double renderSizeHalf;

    protected override IVisual CreateVisual(IRenderContext context, IPort port) {
      return new PortVisual(port, renderSize, renderSizeHalf);
    }

    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, IPort port) {
      var pv = oldVisual as PortVisual;
      if (pv == null) {
        return CreateVisual(context, port);
      }
      return pv;
    }

    private class PortVisual : IVisual
    {
      public PortVisual(IPort port, int renderSize, double renderSizeHalf) {
        this.port = port;
        this.renderSize = renderSize;
        this.renderSizeHalf = renderSizeHalf;
      }

      private IPort port;
      private int renderSize;
      private double renderSizeHalf;

      public void Paint(IRenderContext renderContext, Graphics graphics) {
        var color = port.Tag is Color ? (Color) port.Tag : Color.White;
        var location = port.GetLocation();
        graphics.FillEllipse(new SolidBrush(color), (float) (location.X - renderSizeHalf), (float) (location.Y - renderSizeHalf), renderSize, renderSize);
        graphics.DrawEllipse(Pens.Gray, (float) (location.X - renderSizeHalf), (float) (location.Y - renderSizeHalf), renderSize, renderSize);
      }
    }

    protected override RectD GetBounds(ICanvasContext context, IPort port) {
      var location = port.GetLocation();
      return new RectD(location.X-3, location.Y-3, 6, 6);
    }
  }

  #endregion
}
