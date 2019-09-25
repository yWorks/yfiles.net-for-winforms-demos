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
using System.Threading.Tasks;
using System.Windows.Forms;
using yWorks.Algorithms;
using yWorks.Layout;
using yWorks.Controls;
using yWorks.Graph;
using yWorks.Graph.LabelModels;

namespace Demo.yFiles.Layout.Configurations
{
  /// <summary>
  /// Abstract base class for configurations that can be displayed in an option editor.
  /// </summary>
  /// <remarks>
  /// Subclasses at least have to implement the method <see cref="CreateConfiguredLayout"/> so
  /// the <see cref="Apply"/> method can be called to run the returned layout algorithm and apply the
  /// layout result to the graph in the passed <see cref="GraphControl"/>.
  /// </remarks>
  public abstract class LayoutConfiguration
  {
    /// <summary>
    /// A guard to prevent running multiple layout calculations at the same time.
    /// </summary>
    private bool layoutRunning;

    /// <summary>
    /// Applies this configuration to the given <see cref="GraphControl"/>.
    /// </summary>
    /// <remarks>
    /// This is the main method of this class. Typically, it calls <see cref="CreateConfiguredLayout"/> to create and
    /// configure a layout and <see cref="CreateConfiguredLayoutData"/> to get a suitable <see cref="LayoutData"/> instance
    /// for the layout.
    /// </remarks>
    /// <param name="graphControl">The <c>GraphControl</c> to apply the configuration on.</param>
    /// <param name="doneHandler">A callback that is called after the configuration is applied. Can be <see langword="null"/></param>
    public virtual async Task Apply(GraphControl graphControl, Action doneHandler) {
      if (layoutRunning) {
        graphControl.BeginInvoke(doneHandler);
        return;
      }

      var layout = CreateConfiguredLayout(graphControl);
      if (layout == null) {
        graphControl.BeginInvoke(doneHandler);
        return;
      }

      var layoutData = CreateConfiguredLayoutData(graphControl, layout);

      // configure the LayoutExecutor
      var layoutExecutor = new LayoutExecutor(graphControl, new MinimumNodeSizeStage(layout)) {
          Duration = TimeSpan.FromSeconds(1),
          AnimateViewport = true
      };

      // set the cancel duration for the layout computation to 20s
      layoutExecutor.AbortHandler.CancelDuration = TimeSpan.FromSeconds(20);

      // set the layout data to the LayoutExecutor
      if (layoutData != null) {
        layoutExecutor.LayoutData = layoutData;
      }

      // start the LayoutExecutor
      try {
        await layoutExecutor.Start();
        layoutRunning = false;
      } catch (AlgorithmAbortedException) {
        MessageBox.Show(
            "The layout computation has been canceled because the maximum configured runtime of 20 seconds has been exceeded.");
      } catch (Exception e) {
        MessageBox.Show(
            "Running the layout has encountered an error: " + e.Message);
      } finally {
        PostProcess(graphControl);
        doneHandler();
      }
    }

    /// <summary>
    /// Creates and configures a layout algorithm.
    /// </summary>
    /// <param name="graphControl">The <see cref="GraphControl"/> to apply the configuration on.</param>
    /// <returns>The configured layout.</returns>
    protected abstract ILayoutAlgorithm CreateConfiguredLayout(GraphControl graphControl);

    /// <summary>
    /// Called by <see cref="Apply"/> to create the layout data of the configuration.
    /// </summary>
    /// <remarks>
    /// This method is typically overridden to provide item-specific configuration of a layout algorithm.
    /// </remarks>
    /// <param name="graphControl">The <see cref="GraphControl"/> to apply the configuration on.</param>
    /// <param name="layout">The layout algorithm to run.</param>
    /// <returns>A layout-specific <see cref="LayoutData"/> instance or <see langword="null"/>.</returns>
    protected virtual LayoutData CreateConfiguredLayoutData(GraphControl graphControl, ILayoutAlgorithm layout) {
      return null;
    }

    /// <summary>
    /// Called by <see cref="Apply"/> after the layout animation is done. This method is typically overridden to
    /// remove mappers from the mapper registry of the graph.
    /// </summary>
    protected virtual void PostProcess(GraphControl graphControl) {}

    /// <summary>
    /// Adds a mapper with a <see cref="PreferredPlacementDescriptor"/> that matches the given settings to the mapper
    /// registry of the given graph. In addition, sets the label model of all edge labels to free since that model
    /// can realizes any label placement calculated by a layout algorithm.
    /// </summary>
    public static void AddPreferredPlacementDescriptor(
        IGraph graph, EnumLabelPlacementAlongEdge placeAlongEdge, EnumLabelPlacementSideOfEdge sideOfEdge,
        EnumLabelPlacementOrientation orientation, double distanceToEdge) {

      var model = new FreeEdgeLabelModel();
      var descriptor = CreatePreferredPlacementDescriptor(placeAlongEdge, sideOfEdge, orientation, distanceToEdge);

      graph.MapperRegistry.CreateConstantMapper<ILabel, PreferredPlacementDescriptor>(
        LayoutGraphAdapter.EdgeLabelLayoutPreferredPlacementDescriptorDpKey,
        descriptor);

      foreach (ILabel label in graph.GetEdgeLabels()) {
        graph.SetLabelLayoutParameter(
          label,
          model.FindBestParameter(label, model, label.GetLayout()));
      }
    }

    /// <summary>
    /// Creates a new <see cref="PreferredPlacementDescriptor"/> that matches the given settings.
    /// </summary>
    public static PreferredPlacementDescriptor CreatePreferredPlacementDescriptor(
        EnumLabelPlacementAlongEdge placeAlongEdge, EnumLabelPlacementSideOfEdge sideOfEdge,
        EnumLabelPlacementOrientation orientation, double distanceToEdge) {
      var descriptor = new PreferredPlacementDescriptor();

      switch(sideOfEdge) {
        case EnumLabelPlacementSideOfEdge.Anywhere:
          descriptor.SideOfEdge = LabelPlacements.Anywhere;
          break;
        case EnumLabelPlacementSideOfEdge.OnEdge:
          descriptor.SideOfEdge = LabelPlacements.OnEdge;
          break;
        case EnumLabelPlacementSideOfEdge.Left:
          descriptor.SideOfEdge = LabelPlacements.LeftOfEdge;
          break;
        case EnumLabelPlacementSideOfEdge.Right:
          descriptor.SideOfEdge = LabelPlacements.RightOfEdge;
          break;
        case EnumLabelPlacementSideOfEdge.LeftOrRight:
          descriptor.SideOfEdge = LabelPlacements.LeftOfEdge | LabelPlacements.RightOfEdge;
          break;
      }

      switch(placeAlongEdge) {
        case EnumLabelPlacementAlongEdge.Anywhere:
          descriptor.PlaceAlongEdge = LabelPlacements.Anywhere;
          break;
        case EnumLabelPlacementAlongEdge.AtSourcePort:
          descriptor.PlaceAlongEdge = LabelPlacements.AtSourcePort;
          break;
        case EnumLabelPlacementAlongEdge.AtTargetPort:
          descriptor.PlaceAlongEdge = LabelPlacements.AtTargetPort;
          break;
        case EnumLabelPlacementAlongEdge.AtSource:
          descriptor.PlaceAlongEdge = LabelPlacements.AtSource;
          break;
        case EnumLabelPlacementAlongEdge.AtTarget:
          descriptor.PlaceAlongEdge = LabelPlacements.AtTarget;
          break;
        case EnumLabelPlacementAlongEdge.Centered:
          descriptor.PlaceAlongEdge = LabelPlacements.AtCenter;
          break;
      }

      switch(orientation) {
        case EnumLabelPlacementOrientation.Parallel:
          descriptor.Angle = 0.0d;
          descriptor.AngleReference = LabelAngleReferences.RelativeToEdgeFlow;
          break;
        case EnumLabelPlacementOrientation.Orthogonal:
          descriptor.Angle = Math.PI/2;
          descriptor.AngleReference = LabelAngleReferences.RelativeToEdgeFlow;
          break;
        case EnumLabelPlacementOrientation.Horizontal:
          descriptor.Angle = 0.0d;
          descriptor.AngleReference = LabelAngleReferences.Absolute;
          break;
        case EnumLabelPlacementOrientation.Vertical:
          descriptor.Angle = 90.0d;
          descriptor.AngleReference = LabelAngleReferences.Absolute;
          break;
      }

      descriptor.DistanceToEdge = distanceToEdge;
      return descriptor;
    }

    /// <summary>
    /// Specifies constants for the preferred placement along an edge used by layout configurations.
    /// </summary>
    public enum EnumLabelPlacementAlongEdge
    {
      Anywhere,
      AtSourcePort,
      AtTargetPort,
      AtSource,
      AtTarget,
      Centered
    }

    /// <summary>
    /// Specifies constants for the preferred placement at a side of an edge used by layout configurations.
    /// </summary>
    public enum EnumLabelPlacementSideOfEdge
    {
      Anywhere,
      OnEdge,
      Left,
      Right,
      LeftOrRight
    }

    /// <summary>
    /// Specifies constants for the orientation of an edge label used by layout configurations.
    /// </summary>
    public enum EnumLabelPlacementOrientation
    {
      Parallel,
      Orthogonal,
      Horizontal,
      Vertical
    }
  }
}
