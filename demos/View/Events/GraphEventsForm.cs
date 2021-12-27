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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Demo.yFiles.Graph.Events.Properties;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.PortLocationModels;
using yWorks.Graph.Styles;
using yWorks.Utils;
// ReSharper disable UnusedParameter.Local

namespace Demo.yFiles.Graph.Events
{
  /// <summary>
  /// This demo shows how to register to the various events provided by the <see cref="IGraph">graph</see>,
  /// the <see cref="GraphControl"></see> and the input modes.
  /// </summary>
  public partial class GraphEventsForm: Form
  {
    private GraphEditorInputMode editorMode;
    private FoldingManager manager;
    private GraphViewerInputMode viewerMode;

    public GraphEventsForm() {
      InitializeComponent();

      RegisterToolStripCommands();
      RegisterMenuItemCommands();

      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    /// <summary>
    /// Initializes the graph and the input mode.
    /// </summary>
    /// <seealso cref="InitializeInputModes"/>
    /// <seealso cref="InitializeGraph"/>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      EnableFolding();
      InitializeGraph();
      InitializeInputModes();
      SetupToolTips();
      SetupContextMenu();
      EnableUndo();

      graphControl.FileOperationsEnabled = true;

      // load sample graph
      graphControl.ImportFromGraphML("Resources/sample.graphml");
      graphControl.FitGraphBounds();

      
      logInputModeEvents.Checked = true;
      toggleEditingButton.Checked = true;
      splitContainer2.Panel2MinSize = 300;

      // Timer to regularly update the list. Binding is too slow
      var timer = new Timer { Interval = 100 };
      timer.Tick += delegate {
        if (!dirty) {
          return;
        }
        messagePane.BeginUpdate();
        messagePane.Items.Clear();
        messagePane.Items.AddRange(messages.ToArray());
        messagePane.EndUpdate();
        dirty = false;
      };
      timer.Start();
    }

    #region Command registration

    private void RegisterToolStripCommands() {
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentButton.SetCommand(Commands.FitContent, graphControl);

      loadGraphMLButton.SetCommand(Commands.Open, graphControl);
      zoom11ToolStripMenuItem.SetCommand(Commands.Zoom, 1.0d, graphControl);
    }

    private void RegisterMenuItemCommands() {
      zoomInToolStripMenuItem.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutToolStripMenuItem.SetCommand(Commands.DecreaseZoom, graphControl);
      fitContentToolStripMenuItem.SetCommand(Commands.FitContent, graphControl);

      openToolStripMenuItem.SetCommand(Commands.Open, graphControl);
      saveToolStripMenuItem.SetCommand(Commands.SaveAs, graphControl);

      cutToolStripMenuItem.SetCommand(Commands.Cut, graphControl);
      copyToolStripMenuItem.SetCommand(Commands.Copy, graphControl);
      pasteToolStripMenuItem.SetCommand(Commands.Paste, graphControl);
      deleteToolStripMenuItem.SetCommand(Commands.Delete, graphControl);
      undoToolStripMenuItem.SetCommand(Commands.Undo, graphControl);
      redoToolStripMenuItem.SetCommand(Commands.Redo, graphControl);

      groupSelectionToolStripMenuItem.SetCommand(Commands.GroupSelection, graphControl);
      ungroupSelectionToolStripMenuItem.SetCommand(Commands.UngroupSelection, graphControl);
      expandGroupToolStripMenuItem.SetCommand(Commands.ExpandGroup, graphControl);
      collapseGroupToolStripMenuItem.SetCommand(Commands.CollapseGroup, graphControl);
      enterGroupToolStripMenuItem.SetCommand(Commands.EnterGroup, graphControl);
      exitGroupToolStripMenuItem.SetCommand(Commands.ExitGroup, graphControl);
    }

    #endregion

    #region Initialization

    private void EnableFolding() {
      var graph = graphControl.Graph;

      // enabled changing ports
      var decorator = graph.GetDecorator().EdgeDecorator.EdgeReconnectionPortCandidateProviderDecorator;
      decorator.SetImplementation(EdgeReconnectionPortCandidateProviders.AllNodeCandidates);

      manager = new FoldingManager(graph);
      graphControl.Graph = manager.CreateFoldingView().Graph;
    }

    private void InitializeGraph() {
      var graph = graphControl.Graph;
      graph.NodeDefaults.Style = new ShinyPlateNodeStyle {Brush = Brushes.Orange};
      var groupStyle = new PanelNodeStyle {
        Color = Color.FromArgb(255, 214, 229, 248),
        LabelInsetsColor = Color.FromArgb(255, 214, 229, 248),
        Insets = new InsetsD(5, 18, 5, 5)
      };
      graph.GroupNodeDefaults.Style = new CollapsibleNodeStyleDecorator(groupStyle);
      graph.GroupNodeDefaults.Labels.LayoutParameter = InteriorStretchLabelModel.North;
      graph.GroupNodeDefaults.Labels.Style = new DefaultLabelStyle {
        StringFormat = new StringFormat {Alignment = StringAlignment.Far}
      };
    }

    private void InitializeInputModes() {
      editorMode = new GraphEditorInputMode {
        OrthogonalEdgeEditingContext = new OrthogonalEdgeEditingContext {Enabled = false, MovePorts = true},
        AllowGroupingOperations = true,
        ItemHoverInputMode = {HoverItems = GraphItemTypes.All}
      };
      viewerMode = new GraphViewerInputMode {ItemHoverInputMode = {HoverItems = GraphItemTypes.All}};

      graphControl.InputMode = editorMode;
    }

    private void SetupToolTips() {
      editorMode.ToolTipItems = GraphItemTypes.Node;
      editorMode.QueryItemToolTip += (sender, args) => {
        args.ToolTip = "ToolTip for " + args.Item;
        args.Handled = true;
      };

      viewerMode.ToolTipItems = GraphItemTypes.Node;
      viewerMode.QueryItemToolTip += (sender, args) => {
        args.ToolTip = "ToolTip for " + args.Item;
        args.Handled = true;
      };
    }

    private void SetupContextMenu() {
      editorMode.ContextMenuItems = GraphItemTypes.Node;
      editorMode.PopulateItemContextMenu += (sender, args) => {
        args.Menu.Items.Add(new ToolStripMenuItem("Dummy Item"));
        args.Handled = true;
      };

      viewerMode.ContextMenuItems = GraphItemTypes.Node;
      viewerMode.PopulateItemContextMenu += (sender, args) => {
        args.Menu.Items.Add(new ToolStripMenuItem("Dummy Item"));
        args.Handled = true;
      };
    }

    private void EnableUndo() {
      manager.MasterGraph.SetUndoEngineEnabled(true);
    }

    #endregion

    #region Event Registration

    private void OnLogKeyEventsClicked(object sender, EventArgs args) {
      if(logKeyEvents.Checked) {
        RegisterGraphControlKeyEvents(sender, args);
      }
      else {
        DeregisterGraphControlKeyEvents(sender, args);
      }
    }

    private void RegisterGraphControlKeyEvents(object sender,  EventArgs args) {
      graphControl.CompoundKeyPressed += ControlOnCompoundKeyPressed;
      graphControl.CompoundKeyReleased += ControlOnCompoundKeyReleased;
      graphControl.CompoundKeyTyped += ControlOnCompoundKeyTyped;
    }

    private void DeregisterGraphControlKeyEvents(object sender,  EventArgs args) {
      graphControl.CompoundKeyPressed -= ControlOnCompoundKeyPressed;
      graphControl.CompoundKeyReleased -= ControlOnCompoundKeyReleased;
      graphControl.CompoundKeyTyped -= ControlOnCompoundKeyTyped;
    }

    private void OnLogClipboardCopierEventsClicked(object sender, EventArgs args) {
      if (logClipboard.Checked) {
        RegisterClipboardCopierEvents(sender, args);
      } else {
        DeregisterClipboardCopierEvents(sender, args);
      }
    }

    private void RegisterClipboardCopierEvents(object sender,  EventArgs args) {
      graphControl.Clipboard.ToClipboardCopier.GraphCopied += ClipboardOnGraphCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.NodeCopied += ClipboardOnNodeCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.EdgeCopied += ClipboardOnEdgeCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.PortCopied += ClipboardOnPortCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.LabelCopied += ClipboardOnLabelCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.ObjectCopied += ClipboardOnObjectCopiedToClipboard;

      graphControl.Clipboard.FromClipboardCopier.GraphCopied += ClipboardOnGraphCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.NodeCopied += ClipboardOnNodeCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.EdgeCopied += ClipboardOnEdgeCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.PortCopied += ClipboardOnPortCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.LabelCopied += ClipboardOnLabelCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.ObjectCopied += ClipboardOnObjectCopiedFromClipboard;

      graphControl.Clipboard.DuplicateCopier.GraphCopied += ClipboardOnGraphDuplicated;
      graphControl.Clipboard.DuplicateCopier.NodeCopied += ClipboardOnNodeDuplicated;
      graphControl.Clipboard.DuplicateCopier.EdgeCopied += ClipboardOnEdgeDuplicated;
      graphControl.Clipboard.DuplicateCopier.PortCopied += ClipboardOnPortDuplicated;
      graphControl.Clipboard.DuplicateCopier.LabelCopied += ClipboardOnLabelDuplicated;
      graphControl.Clipboard.DuplicateCopier.ObjectCopied += ClipboardOnObjectDuplicated;
    }

    private void DeregisterClipboardCopierEvents(object sender,  EventArgs args) {
      graphControl.Clipboard.ToClipboardCopier.GraphCopied -= ClipboardOnGraphCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.NodeCopied -= ClipboardOnNodeCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.EdgeCopied -= ClipboardOnEdgeCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.PortCopied -= ClipboardOnPortCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.LabelCopied -= ClipboardOnLabelCopiedToClipboard;
      graphControl.Clipboard.ToClipboardCopier.ObjectCopied -= ClipboardOnObjectCopiedToClipboard;

      graphControl.Clipboard.FromClipboardCopier.GraphCopied -= ClipboardOnGraphCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.NodeCopied -= ClipboardOnNodeCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.EdgeCopied -= ClipboardOnEdgeCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.PortCopied -= ClipboardOnPortCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.LabelCopied -= ClipboardOnLabelCopiedFromClipboard;
      graphControl.Clipboard.FromClipboardCopier.ObjectCopied -= ClipboardOnObjectCopiedFromClipboard;

      graphControl.Clipboard.DuplicateCopier.GraphCopied -= ClipboardOnGraphDuplicated;
      graphControl.Clipboard.DuplicateCopier.NodeCopied -= ClipboardOnNodeDuplicated;
      graphControl.Clipboard.DuplicateCopier.EdgeCopied -= ClipboardOnEdgeDuplicated;
      graphControl.Clipboard.DuplicateCopier.PortCopied -= ClipboardOnPortDuplicated;
      graphControl.Clipboard.DuplicateCopier.LabelCopied -= ClipboardOnLabelDuplicated;
      graphControl.Clipboard.DuplicateCopier.ObjectCopied -= ClipboardOnObjectDuplicated;
    }
    
    private void OnLogClipboardEventsClicked(object sender, EventArgs args) {
      if (logClipboardEvents.Checked) {
        RegisterClipboardEvents(sender, args);
      } else {
        DeregisterClipboardEvents(sender, args);
      }
    }

    private void RegisterClipboardEvents(object sender, EventArgs args) {
      graphControl.Clipboard.ElementsCut += ClipboardOnCut;
      graphControl.Clipboard.ElementsCopied += ClipboardOnCopy;
      graphControl.Clipboard.ElementsPasted += ClipboardOnPaste;
    }

    private void DeregisterClipboardEvents(object sender, EventArgs args) {
      graphControl.Clipboard.ElementsCut -= ClipboardOnCut;
      graphControl.Clipboard.ElementsCopied -= ClipboardOnCopy;
      graphControl.Clipboard.ElementsPasted -= ClipboardOnPaste;
    }

    private void OnLogMouseEventsClicked(object sender, EventArgs args) {
      if (logMouseEvents.Checked) {
        RegisterGraphControlMouseEvents(sender, args);
      } else {
        DeregisterGraphControlMouseEvents(sender, args);
      }
    }
    
    private void OnLogUndoEventsClicked(object sender, EventArgs args) {
      if (logUndoEvents.Checked) {
        RegisterUndoEvents(sender, args);
      } else {
        DeregisterUndoEvents(sender, args);
      }
    }

    private void RegisterUndoEvents(object sender, EventArgs args) {
      var undoEngine = graphControl.Graph.GetUndoEngine();
      undoEngine.UnitUndone += UndoEngineOnUnitUndone;
      undoEngine.UnitRedone += UndoEngineOnUnitRedone;
    }

    private void DeregisterUndoEvents(object sender, EventArgs args) {
      var undoEngine = graphControl.Graph.GetUndoEngine();
      undoEngine.UnitUndone -= UndoEngineOnUnitUndone;
      undoEngine.UnitRedone -= UndoEngineOnUnitRedone;
    }

    private void RegisterGraphControlMouseEvents(object sender, EventArgs args) {
      graphControl.Mouse2DClicked += ControlOnMouse2DClicked;
      graphControl.Mouse2DEntered += ControlOnMouse2DEntered;
      graphControl.Mouse2DExited += ControlOnMouse2DExited;
      graphControl.Mouse2DLostCapture += ControlOnMouse2DLostCapture;
      graphControl.Mouse2DPressed += ControlOnMouse2DPressed;
      graphControl.Mouse2DReleased += ControlOnMouse2DReleased;
      graphControl.Mouse2DWheelTurned += ControlOnMouse2DWheelTurned;
      graphControl.Mouse2DDragged += ControlOnMouse2DDragged;
      graphControl.Mouse2DMoved += ControlOnMouse2DMoved;
    }

    private void DeregisterGraphControlMouseEvents(object sender,  EventArgs args) {
      graphControl.Mouse2DClicked -= ControlOnMouse2DClicked;
      graphControl.Mouse2DEntered -= ControlOnMouse2DEntered;
      graphControl.Mouse2DExited -= ControlOnMouse2DExited;
      graphControl.Mouse2DLostCapture -= ControlOnMouse2DLostCapture;
      graphControl.Mouse2DPressed -= ControlOnMouse2DPressed;
      graphControl.Mouse2DReleased -= ControlOnMouse2DReleased;
      graphControl.Mouse2DWheelTurned -= ControlOnMouse2DWheelTurned;
      graphControl.Mouse2DDragged -= ControlOnMouse2DDragged;
      graphControl.Mouse2DMoved -= ControlOnMouse2DMoved;
    }

    private void OnLogRenderEventsClicked(object sender, EventArgs args) {
      if (logRenderEvents.Checked) {
        RegisterGraphControlRenderEvents(sender, args);
      } else {
        DeregisterGraphControlRenderEvents(sender, args);
      }
    }

    private void RegisterGraphControlRenderEvents(object sender,  EventArgs args) {
      graphControl.PrepareRenderContext += ControlOnPrepareRenderContext;
    }

    private void DeregisterGraphControlRenderEvents(object sender,  EventArgs args) {
      graphControl.PrepareRenderContext -= ControlOnPrepareRenderContext;
    }

    private void OnLogViewportEventsClicked(object sender, EventArgs args) {
      if (logViewportEvents.Checked) {
        RegisterGraphControlViewportEvents(sender, args);
      } else {
        DeregisterGraphControlViewportEvents(sender, args);
      }
    }

    private void RegisterGraphControlViewportEvents(object sender,  EventArgs args) {
      graphControl.MouseWheelZoomFactorChanged += ControlOnMouseWheelZoomFactorChanged;
      graphControl.ViewportChanged += ControlOnViewportChanged;
      graphControl.ZoomChanged += ControlOnZoomChanged;
    }

    private void DeregisterGraphControlViewportEvents(object sender,  EventArgs args) {
      graphControl.MouseWheelZoomFactorChanged -= ControlOnMouseWheelZoomFactorChanged;
      graphControl.ViewportChanged -= ControlOnViewportChanged;
      graphControl.ZoomChanged -= ControlOnZoomChanged;
    }

    private void OnLogNodeEventsClicked(object sender, EventArgs args) {
      if (logNodeEvents.Checked) {
        RegisterNodeEvents(sender, args);
      } else {
        DeregisterNodeEvents(sender, args);
      }
    }

    private void RegisterNodeEvents(object sender,  EventArgs args) {
      graphControl.Graph.NodeStyleChanged += OnNodeStyleChanged;
      graphControl.Graph.NodeTagChanged += OnNodeTagChanged;
      graphControl.Graph.NodeCreated += OnNodeCreated;
      graphControl.Graph.NodeRemoved += OnNodeRemoved;
      graphControl.Graph.IsGroupNodeChanged += OnIsGroupNodeChanged;
      graphControl.Graph.ParentChanged += OnParentChanged;
    }

    private void DeregisterNodeEvents(object sender,  EventArgs args) {
      graphControl.Graph.NodeStyleChanged -= OnNodeStyleChanged;
      graphControl.Graph.NodeTagChanged -= OnNodeTagChanged;
      graphControl.Graph.NodeCreated -= OnNodeCreated;
      graphControl.Graph.NodeRemoved -= OnNodeRemoved;
      graphControl.Graph.IsGroupNodeChanged -= OnIsGroupNodeChanged;
      graphControl.Graph.ParentChanged -= OnParentChanged;
    }

    private void OnLogEdgeEventsClicked(object sender, EventArgs args) {
      if (logEdgeEvents.Checked) {
        RegisterEdgeEvents(sender, args);
      } else {
        DeregisterEdgeEvents(sender, args);
      }
    }

    private void RegisterEdgeEvents(object sender,  EventArgs args) {
      graphControl.Graph.EdgeStyleChanged += OnEdgeStyleChanged;
      graphControl.Graph.EdgePortsChanged += OnEdgePortsChanged;
      graphControl.Graph.EdgeTagChanged += OnEdgeTagChanged;
      graphControl.Graph.EdgeCreated += OnEdgeCreated;
      graphControl.Graph.EdgeRemoved += OnEdgeRemoved;
    }

    private void DeregisterEdgeEvents(object sender,  EventArgs args) {
      graphControl.Graph.EdgeStyleChanged -= OnEdgeStyleChanged;
      graphControl.Graph.EdgePortsChanged -= OnEdgePortsChanged;
      graphControl.Graph.EdgeTagChanged -= OnEdgeTagChanged;
      graphControl.Graph.EdgeCreated -= OnEdgeCreated;
      graphControl.Graph.EdgeRemoved -= OnEdgeRemoved;
    }

    private void OnLogBendEventsClicked(object sender, EventArgs args) {
      if (logBendEvents.Checked) {
        RegisterBendEvents(sender, args);
      } else {
        DeregisterBendEvents(sender, args);
      }
    }

    private void RegisterBendEvents(object sender,  EventArgs args) {
      graphControl.Graph.BendAdded += OnBendAdded;
      graphControl.Graph.BendLocationChanged += OnBendLocationChanged;
      graphControl.Graph.BendTagChanged += OnBendTagChanged;
      graphControl.Graph.BendRemoved += OnBendRemoved;
    }

    private void DeregisterBendEvents(object sender,  EventArgs args) {
      graphControl.Graph.BendAdded -= OnBendAdded;
      graphControl.Graph.BendLocationChanged -= OnBendLocationChanged;
      graphControl.Graph.BendTagChanged -= OnBendTagChanged;
      graphControl.Graph.BendRemoved -= OnBendRemoved;
    }

    private void OnLogPortEventsClicked(object sender, EventArgs args) {
      if (logPortEvents.Checked) {
        RegisterPortEvents(sender, args);
      } else {
        DeregisterPortEvents(sender, args);
      }
    }

    private void RegisterPortEvents(object sender,  EventArgs args) {
      graphControl.Graph.PortAdded += OnPortAdded;
      graphControl.Graph.PortLocationParameterChanged += OnPortLocationParameterChanged;
      graphControl.Graph.PortStyleChanged += OnPortStyleChanged;
      graphControl.Graph.PortTagChanged += OnPortTagChanged;
      graphControl.Graph.PortRemoved += OnPortRemoved;
    }

    private void DeregisterPortEvents(object sender,  EventArgs args) {
      graphControl.Graph.PortAdded -= OnPortAdded;
      graphControl.Graph.PortLocationParameterChanged -= OnPortLocationParameterChanged;
      graphControl.Graph.PortStyleChanged -= OnPortStyleChanged;
      graphControl.Graph.PortTagChanged -= OnPortTagChanged;
      graphControl.Graph.PortRemoved -= OnPortRemoved;
    }

    private void OnLogLabelEventsClicked(object sender, EventArgs args) {
      if (logLabelEvents.Checked) {
        RegisterLabelEvents(sender, args);
      } else {
        DeregisterLabelEvents(sender, args);
      }
    }

    private void RegisterLabelEvents(object sender,  EventArgs args) {
      graphControl.Graph.LabelAdded += OnLabelAdded;
      graphControl.Graph.LabelRemoved += OnLabelRemoved;
      graphControl.Graph.LabelLayoutParameterChanged += OnLabelModelParameterChanged;
      graphControl.Graph.LabelStyleChanged += OnLabelStyleChanged;
      graphControl.Graph.LabelPreferredSizeChanged += OnLabelPreferredSizeChanged;
      graphControl.Graph.LabelTagChanged += OnLabelTagChanged;
      graphControl.Graph.LabelTextChanged += OnLabelTextChanged;
    }

    private void DeregisterLabelEvents(object sender,  EventArgs args) {
      graphControl.Graph.LabelAdded -= OnLabelAdded;
      graphControl.Graph.LabelRemoved -= OnLabelRemoved;
      graphControl.Graph.LabelLayoutParameterChanged -= OnLabelModelParameterChanged;
      graphControl.Graph.LabelStyleChanged -= OnLabelStyleChanged;
      graphControl.Graph.LabelPreferredSizeChanged -= OnLabelPreferredSizeChanged;
      graphControl.Graph.LabelTagChanged -= OnLabelTagChanged;
      graphControl.Graph.LabelTextChanged -= OnLabelTextChanged;
    }

    private void OnLogGraphRenderEventsClicked(object sender, EventArgs args) {
      if (logGraphRenderEvents.Checked) {
        RegisterGraphRenderEvents(sender, args);
      } else {
        DeregisterGraphRenderEvents(sender, args);
      }
    }

    private void RegisterGraphRenderEvents(object sender,  EventArgs args) {
      graphControl.Graph.DisplaysInvalidated += OnDisplaysInvalidated;
    }

    private void DeregisterGraphRenderEvents(object sender,  EventArgs args) {
      graphControl.Graph.DisplaysInvalidated -= OnDisplaysInvalidated;
    }

    private void OnLogGraphControlClicked(object sender, EventArgs args) {
      if (logGraphControl.Checked) {
        RegisterGraphControlEvents(sender, args);
      } else {
        DeregisterGraphControlEvents(sender, args);
      }
    }

    private void RegisterGraphControlEvents(object sender,  EventArgs args) {
      graphControl.CurrentItemChanged += ControlOnCurrentItemChanged;
      graphControl.GraphChanged += ControlOnGraphChanged;
      graphControl.InputModeChanged += ControlOnInputModeChanged;
    }

    private void DeregisterGraphControlEvents(object sender,  EventArgs args) {
      graphControl.CurrentItemChanged -= ControlOnCurrentItemChanged;
      graphControl.GraphChanged -= ControlOnGraphChanged;
      graphControl.InputModeChanged -= ControlOnInputModeChanged;
    }

    private void OnLogNodeBoundsEventsClicked(object sender, EventArgs args) {
      if (logNodeBoundsEvents.Checked) {
        RegisterNodeBoundsEvents(sender, args);
      } else {
        DeregisterNodeBoundsEvents(sender, args);
      }
    }

    private void RegisterNodeBoundsEvents(object sender,  EventArgs args) {
      graphControl.Graph.NodeLayoutChanged += OnNodeLayoutChanged;
    }

    private void DeregisterNodeBoundsEvents(object sender,  EventArgs args) {
      graphControl.Graph.NodeLayoutChanged -= OnNodeLayoutChanged;
    }

    private void OnLogInputModeEventsClicked(object sender, EventArgs args) {
      if (logInputModeEvents.Checked) {
        RegisterInputModeEvents(sender, args);
      } else {
        DeregisterInputModeEvents(sender, args);
      }
    }

    private void RegisterInputModeEvents(object sender,  EventArgs args) {
      editorMode.CanvasClicked += GeimOnCanvasClicked;
      editorMode.DeletedItem += GeimOnDeletedItem;
      editorMode.DeletedSelection += GeimOnDeletedSelection;
      editorMode.DeletingSelection += GeimOnDeletingSelection;
      editorMode.ItemClicked += GeimOnItemClicked;
      editorMode.ItemDoubleClicked += GeimOnItemDoubleClicked;
      editorMode.ItemLeftClicked += GeimOnItemLeftClicked;
      editorMode.ItemLeftDoubleClicked += GeimOnItemLeftDoubleClicked;
      editorMode.ItemRightClicked += GeimOnItemRightClicked;
      editorMode.ItemRightDoubleClicked += GeimOnItemRightDoubleClicked;
      editorMode.LabelAdding += GeimOnLabelAdding;
      editorMode.LabelAdded += GeimOnLabelAdded;
      editorMode.LabelEditing += GeimOnLabelEditing;
      editorMode.LabelTextChanged += GeimOnLabelTextChanged;
      editorMode.LabelTextEditingCanceled += GeimOnLabelTextEditingCanceled;
      editorMode.LabelTextEditingStarted += GeimOnLabelTextEditingStarted;
      editorMode.MultiSelectionFinished += GeimOnMultiSelectionFinished;
      editorMode.MultiSelectionStarted += GeimOnMultiSelectionStarted;
      editorMode.NodeCreated += GeimOnNodeCreated;
      editorMode.NodeReparented += GeimOnNodeReparented;
      editorMode.EdgePortsChanged += GeimOnEdgePortsChanged;
      editorMode.PopulateItemContextMenu += GeimOnPopulateItemContextMenu;
      editorMode.QueryItemToolTip += GeimOnQueryItemToolTip;
      editorMode.ValidateLabelText += GeimOnValidateLabelText;
      editorMode.ElementsCopied += GeimOnElementsCopied;
      editorMode.ElementsCut += GeimOnElementsCut;
      editorMode.ElementsPasted += GeimOnElementsPasted;
      viewerMode.CanvasClicked += GvimOnCanvasClicked;
      viewerMode.ItemClicked += GvimOnItemClicked;
      viewerMode.ItemDoubleClicked += GvimOnItemDoubleClicked;
      viewerMode.ItemLeftClicked += GvimOnItemLeftClicked;
      viewerMode.ItemLeftDoubleClicked += GvimOnItemLeftDoubleClicked;
      viewerMode.ItemRightClicked += GvimOnItemRightClicked;
      viewerMode.ItemRightDoubleClicked += GvimOnItemRightDoubleClicked;
      viewerMode.MultiSelectionFinished += GvimOnMultiSelectionFinished;
      viewerMode.MultiSelectionStarted += GvimOnMultiSelectionStarted;
      viewerMode.PopulateItemContextMenu += GvimOnPopulateItemContextMenu;
      viewerMode.QueryItemToolTip += GvimOnQueryItemToolTip;
      viewerMode.ElementsCopied += GvimOnElementsCopied;
    }

    private void DeregisterInputModeEvents(object sender,  EventArgs args) {
      editorMode.CanvasClicked -= GeimOnCanvasClicked;
      editorMode.DeletedItem -= GeimOnDeletedItem;
      editorMode.DeletedSelection -= GeimOnDeletedSelection;
      editorMode.DeletingSelection -= GeimOnDeletingSelection;
      editorMode.ItemClicked -= GeimOnItemClicked;
      editorMode.ItemDoubleClicked -= GeimOnItemDoubleClicked;
      editorMode.ItemLeftClicked -= GeimOnItemLeftClicked;
      editorMode.ItemLeftDoubleClicked -= GeimOnItemLeftDoubleClicked;
      editorMode.ItemRightClicked -= GeimOnItemRightClicked;
      editorMode.ItemRightDoubleClicked -= GeimOnItemRightDoubleClicked;
      editorMode.LabelAdding -= GeimOnLabelAdding;
      editorMode.LabelAdded -= GeimOnLabelAdded;
      editorMode.LabelEditing -= GeimOnLabelEditing;
      editorMode.LabelTextChanged -= GeimOnLabelTextChanged;
      editorMode.LabelTextEditingCanceled -= GeimOnLabelTextEditingCanceled;
      editorMode.LabelTextEditingStarted -= GeimOnLabelTextEditingStarted;
      editorMode.MultiSelectionFinished -= GeimOnMultiSelectionFinished;
      editorMode.MultiSelectionStarted -= GeimOnMultiSelectionStarted;
      editorMode.NodeCreated -= GeimOnNodeCreated;
      editorMode.NodeReparented -= GeimOnNodeReparented;
      editorMode.EdgePortsChanged -= GeimOnEdgePortsChanged;
      editorMode.PopulateItemContextMenu -= GeimOnPopulateItemContextMenu;
      editorMode.QueryItemToolTip -= GeimOnQueryItemToolTip;
      editorMode.ValidateLabelText -= GeimOnValidateLabelText;
      viewerMode.CanvasClicked -= GvimOnCanvasClicked;
      viewerMode.ItemClicked -= GvimOnItemClicked;
      viewerMode.ItemDoubleClicked -= GvimOnItemDoubleClicked;
      viewerMode.ItemLeftClicked -= GvimOnItemLeftClicked;
      viewerMode.ItemLeftDoubleClicked -= GvimOnItemLeftDoubleClicked;
      viewerMode.ItemRightClicked -= GvimOnItemRightClicked;
      viewerMode.ItemRightDoubleClicked -= GvimOnItemRightDoubleClicked;
      viewerMode.MultiSelectionFinished -= GvimOnMultiSelectionFinished;
      viewerMode.MultiSelectionStarted -= GvimOnMultiSelectionStarted;
      viewerMode.PopulateItemContextMenu -= GvimOnPopulateItemContextMenu;
      viewerMode.QueryItemToolTip -= GvimOnQueryItemToolTip;
    }

    private void OnLogMoveModeEventsClicked(object sender, EventArgs args) {
      if (logMoveModeEvents.Checked) {
        RegisterMoveModeEvents(sender, args);
      } else {
        DeregisterMoveModeEvents(sender, args);
      }
    }

    private void RegisterMoveModeEvents(object sender,  EventArgs args) {
      editorMode.MoveInputMode.DragCanceled += InputModeOnDragCanceled;
      editorMode.MoveInputMode.DragCanceling += InputModeOnDragCanceling;
      editorMode.MoveInputMode.DragFinished += InputModeOnDragFinished;
      editorMode.MoveInputMode.DragFinishing += InputModeOnDragFinishing;
      editorMode.MoveInputMode.DragStarted += InputModeOnDragStarted;
      editorMode.MoveInputMode.DragStarting += InputModeOnDragStarting;

      editorMode.MoveInputMode.Dragged += InputModeOnDragged;
      editorMode.MoveInputMode.Dragging += InputModeOnDragging;
    }

    private void DeregisterMoveModeEvents(object sender,  EventArgs args) {
      editorMode.MoveInputMode.DragCanceled -= InputModeOnDragCanceled;
      editorMode.MoveInputMode.DragCanceling -= InputModeOnDragCanceling;
      editorMode.MoveInputMode.DragFinished -= InputModeOnDragFinished;
      editorMode.MoveInputMode.DragFinishing -= InputModeOnDragFinishing;
      editorMode.MoveInputMode.DragStarted -= InputModeOnDragStarted;
      editorMode.MoveInputMode.DragStarting -= InputModeOnDragStarting;

      editorMode.MoveInputMode.Dragged -= InputModeOnDragged;
      editorMode.MoveInputMode.Dragging -= InputModeOnDragging;
    }

    private void OnLogMoveLabelModeEventsClicked(object sender, EventArgs args) {
      if (logMoveLabelModeEvents.Checked) {
        RegisterMoveLabelModeEvents(sender, args);
      } else {
        DeregisterMoveLabelModeEvents(sender, args);
      }
    }

    private void RegisterMoveLabelModeEvents(object sender,  EventArgs args) {
      editorMode.MoveLabelInputMode.DragCanceled += InputModeOnDragCanceled;
      editorMode.MoveLabelInputMode.DragCanceling += InputModeOnDragCanceling;
      editorMode.MoveLabelInputMode.DragFinished += InputModeOnDragFinished;
      editorMode.MoveLabelInputMode.DragFinishing += InputModeOnDragFinishing;
      editorMode.MoveLabelInputMode.DragStarted += InputModeOnDragStarted;
      editorMode.MoveLabelInputMode.DragStarting += InputModeOnDragStarting;

      editorMode.MoveLabelInputMode.Dragged += InputModeOnDragged;
      editorMode.MoveLabelInputMode.Dragging += InputModeOnDragging;
    }

    private void DeregisterMoveLabelModeEvents(object sender,  EventArgs args) {
      editorMode.MoveLabelInputMode.DragCanceled -= InputModeOnDragCanceled;
      editorMode.MoveLabelInputMode.DragCanceling -= InputModeOnDragCanceling;
      editorMode.MoveLabelInputMode.DragFinished -= InputModeOnDragFinished;
      editorMode.MoveLabelInputMode.DragFinishing -= InputModeOnDragFinishing;
      editorMode.MoveLabelInputMode.DragStarted -= InputModeOnDragStarted;
      editorMode.MoveLabelInputMode.DragStarting -= InputModeOnDragStarting;

      editorMode.MoveLabelInputMode.Dragged -= InputModeOnDragged;
      editorMode.MoveLabelInputMode.Dragging -= InputModeOnDragging;
    }

    private void OnLogItemHoverModeEventsClicked(object sender, EventArgs args) {
      if (logItemHoverModeEvents.Checked) {
        RegisterItemHoverModeEvents(sender, args);
      } else {
        DeregisterItemHoverModeEvents(sender, args);
      }
    }

    private void RegisterItemHoverModeEvents(object sender,  EventArgs args) {
      editorMode.ItemHoverInputMode.HoveredItemChanged += ItemHoverInputModeOnHoveredItemChanged;
      viewerMode.ItemHoverInputMode.HoveredItemChanged += ItemHoverInputModeOnHoveredItemChanged;
    }

    private void DeregisterItemHoverModeEvents(object sender,  EventArgs args) {
      editorMode.ItemHoverInputMode.HoveredItemChanged -= ItemHoverInputModeOnHoveredItemChanged;
      viewerMode.ItemHoverInputMode.HoveredItemChanged -= ItemHoverInputModeOnHoveredItemChanged;
    }

    private void OnLogCreateBendModeEventsClicked(object sender, EventArgs args) {
      if (logCreateBendModeEvents.Checked) {
        RegisterCreateBendModeEvents(sender, args);
      } else {
        DeregisterCreateBendModeEvents(sender, args);
      }
    }

    private void RegisterCreateBendModeEvents(object sender,  EventArgs args) {
      editorMode.CreateBendInputMode.BendCreated += CreateBendInputModeOnBendCreated;
      editorMode.CreateBendInputMode.DragCanceled += InputModeOnDragCanceled;

      editorMode.CreateBendInputMode.Dragged += InputModeOnDragged;
      editorMode.CreateBendInputMode.Dragging += InputModeOnDragging;
    }

    private void DeregisterCreateBendModeEvents(object sender,  EventArgs args) {
      editorMode.CreateBendInputMode.BendCreated -= CreateBendInputModeOnBendCreated;
      editorMode.CreateBendInputMode.DragCanceled -= InputModeOnDragCanceled;

      editorMode.CreateBendInputMode.Dragged -= InputModeOnDragged;
      editorMode.CreateBendInputMode.Dragging -= InputModeOnDragging;
    }

    private void OnLogContextMenuModeEventsClicked(object sender, EventArgs args) {
      if (logContextMenuModeEvents.Checked) {
        RegisterContextMenuModeEvents(sender, args);
      } else {
        DeregisterContextMenuModeEvents(sender, args);
      }
    }

    private void RegisterContextMenuModeEvents(object sender,  EventArgs args) {
      editorMode.ContextMenuInputMode.PopulateMenu += ContextMenuInputModeOnPopulateMenu;
      viewerMode.ContextMenuInputMode.PopulateMenu += ContextMenuInputModeOnPopulateMenu;
    }

    private void DeregisterContextMenuModeEvents(object sender,  EventArgs args) {
      editorMode.ContextMenuInputMode.PopulateMenu -= ContextMenuInputModeOnPopulateMenu;
      viewerMode.ContextMenuInputMode.PopulateMenu -= ContextMenuInputModeOnPopulateMenu;
    }

    private void OnLogTextEditorModeEventsClicked(object sender, EventArgs args) {
      if (logTextEditorModeEvents.Checked) {
        RegisterTextEditorModeEvents(sender, args);
      } else {
        DeregisterTextEditorModeEvents(sender, args);
      }
    }

    private void RegisterTextEditorModeEvents(object sender,  EventArgs args) {
      editorMode.TextEditorInputMode.EditingCanceled += TextEditorInputModeOnEditingCanceled;
      editorMode.TextEditorInputMode.EditingStarted += TextEditorInputModeOnEditingStarted;
      editorMode.TextEditorInputMode.TextEdited += TextEditorInputModeOnTextEdited;
    }

    private void DeregisterTextEditorModeEvents(object sender,  EventArgs args) {
      editorMode.TextEditorInputMode.EditingCanceled -= TextEditorInputModeOnEditingCanceled;
      editorMode.TextEditorInputMode.EditingStarted -= TextEditorInputModeOnEditingStarted;
      editorMode.TextEditorInputMode.TextEdited -= TextEditorInputModeOnTextEdited;
    }

    private void OnLogMouseHoverModeEventsClicked(object sender, EventArgs args) {
      if (logMouseHoverModeEvents.Checked) {
        RegisterMouseHoverModeEvents(sender, args);
      } else {
        DeregisterMouseHoverModeEvents(sender, args);
      }
    }

    private void RegisterMouseHoverModeEvents(object sender,  EventArgs args) {
      editorMode.MouseHoverInputMode.QueryToolTip += MouseHoverInputModeOnQueryToolTip;
      viewerMode.MouseHoverInputMode.QueryToolTip += MouseHoverInputModeOnQueryToolTip;
    }

    private void DeregisterMouseHoverModeEvents(object sender,  EventArgs args) {
      editorMode.MouseHoverInputMode.QueryToolTip -= MouseHoverInputModeOnQueryToolTip;
      viewerMode.MouseHoverInputMode.QueryToolTip -= MouseHoverInputModeOnQueryToolTip;
    }

    private void OnLogNavigationModeEventsClicked(object sender, EventArgs args) {
      if (logNavigationModeEvents.Checked) {
        RegisterNavigationModeEvents(sender, args);
      } else {
        DeregisterNavigationModeEvents(sender, args);
      }
    }

    private void RegisterNavigationModeEvents(object sender,  EventArgs args) {
      editorMode.NavigationInputMode.GroupCollapsed += NavigationInputModeOnGroupCollapsed;
      editorMode.NavigationInputMode.GroupCollapsing += NavigationInputModeOnGroupCollapsing;
      editorMode.NavigationInputMode.GroupEntered += NavigationInputModeOnGroupEntered;
      editorMode.NavigationInputMode.GroupEntering += NavigationInputModeOnGroupEntering;
      editorMode.NavigationInputMode.GroupExited += NavigationInputModeOnGroupExited;
      editorMode.NavigationInputMode.GroupExiting += NavigationInputModeOnGroupExiting;
      editorMode.NavigationInputMode.GroupExpanded += NavigationInputModeOnGroupExpanded;
      editorMode.NavigationInputMode.GroupExpanding += NavigationInputModeOnGroupExpanding;

      viewerMode.NavigationInputMode.GroupCollapsed += NavigationInputModeOnGroupCollapsed;
      viewerMode.NavigationInputMode.GroupCollapsing += NavigationInputModeOnGroupCollapsing;
      viewerMode.NavigationInputMode.GroupEntered += NavigationInputModeOnGroupEntered;
      viewerMode.NavigationInputMode.GroupEntering += NavigationInputModeOnGroupEntering;
      viewerMode.NavigationInputMode.GroupExited += NavigationInputModeOnGroupExited;
      viewerMode.NavigationInputMode.GroupExiting += NavigationInputModeOnGroupExiting;
      viewerMode.NavigationInputMode.GroupExpanded += NavigationInputModeOnGroupExpanded;
      viewerMode.NavigationInputMode.GroupExpanding += NavigationInputModeOnGroupExpanding;
    }

    private void DeregisterNavigationModeEvents(object sender,  EventArgs args) {
      editorMode.NavigationInputMode.GroupCollapsed -= NavigationInputModeOnGroupCollapsed;
      editorMode.NavigationInputMode.GroupCollapsing -= NavigationInputModeOnGroupCollapsing;
      editorMode.NavigationInputMode.GroupEntered -= NavigationInputModeOnGroupEntered;
      editorMode.NavigationInputMode.GroupEntering -= NavigationInputModeOnGroupEntering;
      editorMode.NavigationInputMode.GroupExited -= NavigationInputModeOnGroupExited;
      editorMode.NavigationInputMode.GroupExiting -= NavigationInputModeOnGroupExiting;
      editorMode.NavigationInputMode.GroupExpanded -= NavigationInputModeOnGroupExpanded;
      editorMode.NavigationInputMode.GroupExpanding -= NavigationInputModeOnGroupExpanding;

      viewerMode.NavigationInputMode.GroupCollapsed -= NavigationInputModeOnGroupCollapsed;
      viewerMode.NavigationInputMode.GroupCollapsing -= NavigationInputModeOnGroupCollapsing;
      viewerMode.NavigationInputMode.GroupEntered -= NavigationInputModeOnGroupEntered;
      viewerMode.NavigationInputMode.GroupEntering -= NavigationInputModeOnGroupEntering;
      viewerMode.NavigationInputMode.GroupExited -= NavigationInputModeOnGroupExited;
      viewerMode.NavigationInputMode.GroupExiting -= NavigationInputModeOnGroupExiting;
      viewerMode.NavigationInputMode.GroupExpanded -= NavigationInputModeOnGroupExpanded;
      viewerMode.NavigationInputMode.GroupExpanding -= NavigationInputModeOnGroupExpanding;
    }

    private void OnLogClickModeEventsClicked(object sender, EventArgs args) {
      if (logClickModeEvents.Checked) {
        RegisterClickModeEvents(sender, args);
      } else {
        DeregisterClickModeEvents(sender, args);
      }
    }

    private void RegisterClickModeEvents(object sender,  EventArgs args) {
      editorMode.ClickInputMode.Clicked += ClickInputModeOnClicked;
      editorMode.ClickInputMode.DoubleClicked += ClickInputModeOnDoubleClicked;
      editorMode.ClickInputMode.LeftClicked += ClickInputModeOnLeftClicked;
      editorMode.ClickInputMode.LeftDoubleClicked += ClickInputModeOnLeftDoubleClicked;
      editorMode.ClickInputMode.RightClicked += ClickInputModeOnRightClicked;
      editorMode.ClickInputMode.RightDoubleClicked += ClickInputModeOnRightDoubleClicked;

      viewerMode.ClickInputMode.Clicked += ClickInputModeOnClicked;
      viewerMode.ClickInputMode.DoubleClicked += ClickInputModeOnDoubleClicked;
      viewerMode.ClickInputMode.LeftClicked += ClickInputModeOnLeftClicked;
      viewerMode.ClickInputMode.LeftDoubleClicked += ClickInputModeOnLeftDoubleClicked;
      viewerMode.ClickInputMode.RightClicked += ClickInputModeOnRightClicked;
      viewerMode.ClickInputMode.RightDoubleClicked += ClickInputModeOnRightDoubleClicked;
    }

    private void DeregisterClickModeEvents(object sender,  EventArgs args) {
      editorMode.ClickInputMode.Clicked -= ClickInputModeOnClicked;
      editorMode.ClickInputMode.DoubleClicked -= ClickInputModeOnDoubleClicked;
      editorMode.ClickInputMode.LeftClicked -= ClickInputModeOnLeftClicked;
      editorMode.ClickInputMode.LeftDoubleClicked -= ClickInputModeOnLeftDoubleClicked;
      editorMode.ClickInputMode.RightClicked -= ClickInputModeOnRightClicked;
      editorMode.ClickInputMode.RightDoubleClicked -= ClickInputModeOnRightDoubleClicked;

      viewerMode.ClickInputMode.Clicked -= ClickInputModeOnClicked;
      viewerMode.ClickInputMode.DoubleClicked -= ClickInputModeOnDoubleClicked;
      viewerMode.ClickInputMode.LeftClicked -= ClickInputModeOnLeftClicked;
      viewerMode.ClickInputMode.LeftDoubleClicked -= ClickInputModeOnLeftDoubleClicked;
      viewerMode.ClickInputMode.RightClicked -= ClickInputModeOnRightClicked;
      viewerMode.ClickInputMode.RightDoubleClicked -= ClickInputModeOnRightDoubleClicked;
    }

    private void OnLogHandleModeEventsClicked(object sender, EventArgs args) {
      if (logHandleModeEvents.Checked) {
        RegisterHandleModeEvents(sender, args);
      } else {
        DeregisterHandleModeEvents(sender, args);
      }
    }

    private void RegisterHandleModeEvents(object sender,  EventArgs args) {
      editorMode.HandleInputMode.DragCanceled += InputModeOnDragCanceled;
      editorMode.HandleInputMode.DragCanceling += InputModeOnDragCanceling;
      editorMode.HandleInputMode.DragFinished += InputModeOnDragFinished;
      editorMode.HandleInputMode.DragFinishing += InputModeOnDragFinishing;
      editorMode.HandleInputMode.DragStarted += InputModeOnDragStarted;
      editorMode.HandleInputMode.DragStarting += InputModeOnDragStarting;

      editorMode.HandleInputMode.Dragged += InputModeOnDragged;
      editorMode.HandleInputMode.Dragging += InputModeOnDragging;
    }

    private void DeregisterHandleModeEvents(object sender,  EventArgs args) {
      editorMode.HandleInputMode.DragCanceled -= InputModeOnDragCanceled;
      editorMode.HandleInputMode.DragCanceling -= InputModeOnDragCanceling;
      editorMode.HandleInputMode.DragFinished -= InputModeOnDragFinished;
      editorMode.HandleInputMode.DragFinishing -= InputModeOnDragFinishing;
      editorMode.HandleInputMode.DragStarted -= InputModeOnDragStarted;
      editorMode.HandleInputMode.DragStarting -= InputModeOnDragStarting;

      editorMode.HandleInputMode.Dragged -= InputModeOnDragged;
      editorMode.HandleInputMode.Dragging -= InputModeOnDragging;
    }

    private void OnLogMoveViewportModeEventsClicked(object sender, EventArgs args) {
      if (logMoveViewportModeEvents.Checked) {
        RegisterMoveViewportModeEvents(sender, args);
      } else {
        DeregisterMoveViewportModeEvents(sender, args);
      }
    }

    private void RegisterMoveViewportModeEvents(object sender,  EventArgs args) {
      editorMode.MoveViewportInputMode.DragCanceled += InputModeOnDragCanceled;
      editorMode.MoveViewportInputMode.DragCanceling += InputModeOnDragCanceling;
      editorMode.MoveViewportInputMode.DragFinished += InputModeOnDragFinished;
      editorMode.MoveViewportInputMode.DragFinishing += InputModeOnDragFinishing;
      editorMode.MoveViewportInputMode.DragStarted += InputModeOnDragStarted;
      editorMode.MoveViewportInputMode.DragStarting += InputModeOnDragStarting;

      editorMode.MoveViewportInputMode.Dragged += InputModeOnDragged;
      editorMode.MoveViewportInputMode.Dragging += InputModeOnDragging;

      viewerMode.MoveViewportInputMode.DragCanceled += InputModeOnDragCanceled;
      viewerMode.MoveViewportInputMode.DragCanceling += InputModeOnDragCanceling;
      viewerMode.MoveViewportInputMode.DragFinished += InputModeOnDragFinished;
      viewerMode.MoveViewportInputMode.DragFinishing += InputModeOnDragFinishing;
      viewerMode.MoveViewportInputMode.DragStarted += InputModeOnDragStarted;
      viewerMode.MoveViewportInputMode.DragStarting += InputModeOnDragStarting;

      viewerMode.MoveViewportInputMode.Dragged += InputModeOnDragged;
      viewerMode.MoveViewportInputMode.Dragging += InputModeOnDragging;
    }

    private void DeregisterMoveViewportModeEvents(object sender,  EventArgs args) {
      editorMode.MoveViewportInputMode.DragCanceled -= InputModeOnDragCanceled;
      editorMode.MoveViewportInputMode.DragCanceling -= InputModeOnDragCanceling;
      editorMode.MoveViewportInputMode.DragFinished -= InputModeOnDragFinished;
      editorMode.MoveViewportInputMode.DragFinishing -= InputModeOnDragFinishing;
      editorMode.MoveViewportInputMode.DragStarted -= InputModeOnDragStarted;
      editorMode.MoveViewportInputMode.DragStarting -= InputModeOnDragStarting;

      editorMode.MoveViewportInputMode.Dragged -= InputModeOnDragged;
      editorMode.MoveViewportInputMode.Dragging -= InputModeOnDragging;

      viewerMode.MoveViewportInputMode.DragCanceled -= InputModeOnDragCanceled;
      viewerMode.MoveViewportInputMode.DragCanceling -= InputModeOnDragCanceling;
      viewerMode.MoveViewportInputMode.DragFinished -= InputModeOnDragFinished;
      viewerMode.MoveViewportInputMode.DragFinishing -= InputModeOnDragFinishing;
      viewerMode.MoveViewportInputMode.DragStarted -= InputModeOnDragStarted;
      viewerMode.MoveViewportInputMode.DragStarting -= InputModeOnDragStarting;

      viewerMode.MoveViewportInputMode.Dragged -= InputModeOnDragged;
      viewerMode.MoveViewportInputMode.Dragging -= InputModeOnDragging;
    }

    private void OnLogCreateEdgeModeEventsClicked(object sender, EventArgs args) {
      if (logCreateEdgeModeEvents.Checked) {
        RegisterCreateEdgeModeEvents(sender, args);
      } else {
        DeregisterCreateEdgeModeEvents(sender, args);
      }
    }

    private void RegisterCreateEdgeModeEvents(object sender,  EventArgs args) {
      editorMode.CreateEdgeInputMode.EdgeCreated += CreateEdgeInputModeOnEdgeCreated;
      editorMode.CreateEdgeInputMode.EdgeCreationStarted += CreateEdgeInputModeOnEdgeCreationStarted;
      editorMode.CreateEdgeInputMode.GestureCanceled += CreateEdgeInputModeOnGestureCanceled;
      editorMode.CreateEdgeInputMode.GestureCanceling += CreateEdgeInputModeOnGestureCanceling;
      editorMode.CreateEdgeInputMode.GestureFinished += CreateEdgeInputModeOnGestureFinished;
      editorMode.CreateEdgeInputMode.GestureFinishing += CreateEdgeInputModeOnGestureFinishing;
      editorMode.CreateEdgeInputMode.GestureStarted += CreateEdgeInputModeOnGestureStarted;
      editorMode.CreateEdgeInputMode.GestureStarting += CreateEdgeInputModeOnGestureStarting;
      editorMode.CreateEdgeInputMode.Moved += CreateEdgeInputModeOnMoved;
      editorMode.CreateEdgeInputMode.Moving += CreateEdgeInputModeOnMoving;
      editorMode.CreateEdgeInputMode.PortAdded += CreateEdgeInputModeOnPortAdded;
      editorMode.CreateEdgeInputMode.SourcePortCandidateChanged += CreateEdgeInputModeOnSourcePortCandidateChanged;
      editorMode.CreateEdgeInputMode.TargetPortCandidateChanged += CreateEdgeInputModeOnTargetPortCandidateChanged;
    }

    private void DeregisterCreateEdgeModeEvents(object sender,  EventArgs args) {
      editorMode.CreateEdgeInputMode.EdgeCreated -= CreateEdgeInputModeOnEdgeCreated;
      editorMode.CreateEdgeInputMode.EdgeCreationStarted -= CreateEdgeInputModeOnEdgeCreationStarted;
      editorMode.CreateEdgeInputMode.GestureCanceled -= CreateEdgeInputModeOnGestureCanceled;
      editorMode.CreateEdgeInputMode.GestureCanceling -= CreateEdgeInputModeOnGestureCanceling;
      editorMode.CreateEdgeInputMode.GestureFinished -= CreateEdgeInputModeOnGestureFinished;
      editorMode.CreateEdgeInputMode.GestureFinishing -= CreateEdgeInputModeOnGestureFinishing;
      editorMode.CreateEdgeInputMode.GestureStarted -= CreateEdgeInputModeOnGestureStarted;
      editorMode.CreateEdgeInputMode.GestureStarting -= CreateEdgeInputModeOnGestureStarting;
      editorMode.CreateEdgeInputMode.Moved -= CreateEdgeInputModeOnMoved;
      editorMode.CreateEdgeInputMode.Moving -= CreateEdgeInputModeOnMoving;
      editorMode.CreateEdgeInputMode.PortAdded -= CreateEdgeInputModeOnPortAdded;
      editorMode.CreateEdgeInputMode.SourcePortCandidateChanged -= CreateEdgeInputModeOnSourcePortCandidateChanged;
      editorMode.CreateEdgeInputMode.TargetPortCandidateChanged -= CreateEdgeInputModeOnTargetPortCandidateChanged;
    }

    private void OnLogSelectionEventsClicked(object sender, EventArgs args) {
      if (logSelectionEvents.Checked) {
        RegisterSelectionEvents(sender, args);
      } else {
        DeregisterSelectionEvents(sender, args);
      }
    }

    private void RegisterSelectionEvents(object sender,  EventArgs args) {
      graphControl.Selection.ItemSelectionChanged += OnItemSelectionChanged;
    }

    private void DeregisterSelectionEvents(object sender,  EventArgs args) {
      graphControl.Selection.ItemSelectionChanged -= OnItemSelectionChanged;
    }

    #endregion

    #region InputMode Installing

    private void OnToggleEditingClicked(object sender, EventArgs args) {
      if (toggleEditingButton.Checked) {
        InstallEditorMode(sender, args);
      } else {
        InstallViewerMode(sender, args);
      }
    }

    private void InstallEditorMode(object sender,  EventArgs args) {
      graphControl.InputMode = editorMode;
    }

    private void InstallViewerMode(object sender,  EventArgs args) {
      graphControl.InputMode = viewerMode;
    }

    private void OnToggleOrthogonalEditingClicked(object sender, EventArgs args) {
      if (orthogonalEditingButton.Checked) {
        EnableOrthogonalEdges(sender, args);
      } else {
        DisableOrthogonalEdges(sender, args);
      }
    }

    private void EnableOrthogonalEdges(object sender,  EventArgs args) {
      editorMode.OrthogonalEdgeEditingContext.Enabled = true;
    }

    private void DisableOrthogonalEdges(object sender,  EventArgs args) {
      editorMode.OrthogonalEdgeEditingContext.Enabled = false;
    }

    #endregion

    #region Graph Events

    private void OnDisplaysInvalidated(object sender,  EventArgs args) {
      Log("Displays Invalidated");
    }

    private void OnEdgeStyleChanged(object o, ItemChangedEventArgs<IEdge, IEdgeStyle> args) {
      Log("Edge Style Changed: " + args.Item, "EdgeStyleChanged");
    }

    private void OnEdgePortsChanged(object o, EdgeEventArgs args) {
      Log("Edge Ports Changed: " + args.Item, "EdgePortsChanged");
    }

    private void OnEdgeTagChanged(object o, ItemChangedEventArgs<IEdge, object> args) {
      Log("Edge Tag Changed: " + args.Item, "EdgeTagChanged");
    }

    private void OnEdgeCreated(object o, ItemEventArgs<IEdge> args) {
      Log("Edge Created: " + args.Item, "EdgeCreated");
    }

    private void OnEdgeRemoved(object o, ItemEventArgs<IEdge> args) {
      Log("Edge Removed: " + args.Item, "EdgeRemoved");
    }

    private void OnLabelAdded(object o, ItemEventArgs<ILabel> args) {
      Log("Label Added: " + args.Item, "LabelAdded");
    }

    private void OnLabelModelParameterChanged(object o, ItemChangedEventArgs<ILabel, ILabelModelParameter> args) {
      Log("Label Layout Parameter Changed: " + args.Item, "LabelLayoutParameterChanged");
    }
    private void OnLabelStyleChanged(object o, ItemChangedEventArgs<ILabel, ILabelStyle> args) {
      Log("Label Style Changed: " + args.Item, "LabelStyleChanged");
    }
    private void OnLabelPreferredSizeChanged(object o, ItemChangedEventArgs<ILabel, SizeD> args) {
      Log("Label Preferrred Size Changed: " + args.Item, "LabelPreferredSizeChanged");
    }
    private void OnLabelTagChanged(object o, ItemChangedEventArgs<ILabel, object> args) {
      Log("Label Tag Changed: " + args.Item, "LabelTagChanged");
    }
    private void OnLabelTextChanged(object o, ItemChangedEventArgs<ILabel, String> args) {
      Log("Label Text Changed: " + args.Item, "LabelTextChanged");
    }

    private void OnLabelRemoved(object o, ItemEventArgs<ILabel> args) {
      Log("Label Removed: " + args.Item, "LabelRemoved");
    }

    private void OnNodeStyleChanged(object o, ItemChangedEventArgs<INode, INodeStyle> args) {
      Log("Node Style Changed: " + args.Item, "NodeStyleChanged");
    }

    private void OnNodeTagChanged(object o, ItemChangedEventArgs<INode, object> args) {
      Log("Node Tag Changed: " + args.Item, "NodeTagChanged");
    }

    private void OnNodeCreated(object o, ItemEventArgs<INode> args) {
      Log("Node Created: " + args.Item, "NodeCreated");
    }

    private void OnNodeRemoved(object o, ItemEventArgs<INode> args) {
      Log("Node Removed: " + args.Item, "NodeRemoved");
    }

    private void OnIsGroupNodeChanged(object sender, NodeEventArgs args) {
      Log("GroupNode Status Changed: " + args.Item, "IsGroupNodeChanged");
    }

    private void OnParentChanged(object sender, NodeEventArgs args) {
      Log("Parent Changed: " + args.Item, "ParentChanged");
    }

    private void OnPortAdded(object o, ItemEventArgs<IPort> args) {
      Log("Port Added: " + args.Item, "PortAdded");
    }

    private void OnPortLocationParameterChanged(object o, ItemChangedEventArgs<IPort, IPortLocationModelParameter> args) {
      Log("Port Location Parameter Changed: " + args.Item, "PortLocationParameterChanged");
    }

    private void OnPortStyleChanged(object o, ItemChangedEventArgs<IPort, IPortStyle> args) {
      Log("Port Style Changed: " + args.Item, "PortStyleChanged");
    }

    private void OnPortTagChanged(object o, ItemChangedEventArgs<IPort, object> args) {
      Log("Port Tag Changed: " + args.Item, "PortTagChanged");
    }

    private void OnPortRemoved(object o, ItemEventArgs<IPort> args) {
      Log("Port Removed: " + args.Item, "PortRemoved");
    }

    private void OnBendAdded(object o, ItemEventArgs<IBend> args) {
      Log("Bend Added: " + args.Item, "BendAdded");
    }

    private void OnBendLocationChanged(object o, IBend bend, PointD oldLocation) {
      Log("Bend Location Changed: " + bend + "; " + oldLocation, "BendLocationChanged");
    }

    private void OnBendTagChanged(object o, ItemChangedEventArgs<IBend, object> args) {
      Log("Bend Tag Changed: " + args.Item, "BendTagChanged");
    }

    private void OnBendRemoved(object o, ItemEventArgs<IBend> args) {
      Log("Bend Removed: " + args.Item, "BendRemoved");
    }

    #endregion

    #region Clipboard Events

    private void ClipboardOnGraphCopiedToClipboard(object sender, ItemCopiedEventArgs<IGraph> eventArgs) {
      Log("Graph copied to Clipboard");
    }

    private void ClipboardOnNodeCopiedToClipboard(object sender, ItemCopiedEventArgs<INode> args) {
      Log("Node " + args.Original + " copied to Clipboard: " + args.Copy);
    }

    private void ClipboardOnEdgeCopiedToClipboard(object sender, ItemCopiedEventArgs<IEdge> args) {
      Log("Edge " + args.Original + " copied to Clipboard: " + args.Copy);
    }

    private void ClipboardOnPortCopiedToClipboard(object sender, ItemCopiedEventArgs<IPort> args) {
      Log("Port " + args.Original + " copied to Clipboard: " + args.Copy);
    }

    private void ClipboardOnLabelCopiedToClipboard(object sender, ItemCopiedEventArgs<ILabel> args) {
      Log("Label " + args.Original + " copied to Clipboard: " + args.Copy);
    }

    private void ClipboardOnObjectCopiedToClipboard(object sender, ItemCopiedEventArgs<Object> args) {
      Log("Object " + args.Original + " copied to Clipboard: " + args.Copy);
    }

    private void ClipboardOnGraphCopiedFromClipboard(object sender, ItemCopiedEventArgs<IGraph> eventArgs) {
      Log("Graph copied from Clipboard");
    }

    private void ClipboardOnNodeCopiedFromClipboard(object sender, ItemCopiedEventArgs<INode> args) {
      Log("Node " + args.Original + " copied from Clipboard: " + args.Copy);
    }

    private void ClipboardOnEdgeCopiedFromClipboard(object sender, ItemCopiedEventArgs<IEdge> args) {
      Log("Edge " + args.Original + " copied from Clipboard: " + args.Copy);
    }

    private void ClipboardOnPortCopiedFromClipboard(object sender, ItemCopiedEventArgs<IPort> args) {
      Log("Port " + args.Original + " copied from Clipboard: " + args.Copy);
    }

    private void ClipboardOnLabelCopiedFromClipboard(object sender, ItemCopiedEventArgs<ILabel> args) {
      Log("Label " + args.Original + " copied from Clipboard: " + args.Copy);
    }

    private void ClipboardOnObjectCopiedFromClipboard(object sender, ItemCopiedEventArgs<Object> args) {
      Log("Object " + args.Original + " copied from Clipboard: " + args.Copy);
    }

    private void ClipboardOnGraphDuplicated(object sender, ItemCopiedEventArgs<IGraph> eventArgs) {
      Log("Graph duplicated.");
    }

    private void ClipboardOnNodeDuplicated(object sender, ItemCopiedEventArgs<INode> args) {
      Log("Node " + args.Original + " duplicated to: " + args.Copy);
    }

    private void ClipboardOnEdgeDuplicated(object sender, ItemCopiedEventArgs<IEdge> args) {
      Log("Edge " + args.Original + " duplicated to: " + args.Copy);
    }

    private void ClipboardOnPortDuplicated(object sender, ItemCopiedEventArgs<IPort> args) {
      Log("Port " + args.Original + " duplicated to: " + args.Copy);
    }

    private void ClipboardOnLabelDuplicated(object sender, ItemCopiedEventArgs<ILabel> args) {
      Log("Label " + args.Original + " duplicated to: " + args.Copy);
    }

    private void ClipboardOnObjectDuplicated(object sender, ItemCopiedEventArgs<Object> args) {
      Log("Object " + args.Original + " duplicated to: " + args.Copy);
    }

    private void ClipboardOnCut(object sender, EventArgs args) {
      Log("Clipboard operation: Cut");
    }

    private void ClipboardOnCopy(object sender, EventArgs args) {
      Log("Clipboard operation: Copy");
    }

    private void ClipboardOnPaste(object sender, EventArgs args) {
      Log("Clipboard operation: Paste");
    }

    #endregion

    #region Undo Events

    private void UndoEngineOnUnitUndone(object sender, EventArgs args) {
      Log("Undo performed");
    }

    private void UndoEngineOnUnitRedone(object sender, EventArgs args) {
      Log("Redo performed");
    }

    #endregion

    #region GraphControl Events

    private void ControlOnCurrentItemChanged(object sender,
                                             PropertyChangedEventArgs routedPropertyChangedEventArgs) {
      Log("GraphControl CurrentItemChanged");
    }

    private void ControlOnGraphChanged(object sender,
                                       EventArgs dependencyPropertyChangedEventArgs) {
      Log("GraphControl GraphChanged");
    }

    private void ControlOnInputModeChanged(object sender,
                                           EventArgs dependencyPropertyChangedEventArgs) {
      Log("GraphControl InputModeChanged");
    }

    #endregion

    #region GraphControl Key Events

    private void ControlOnCompoundKeyPressed(object sender, CompoundKeyEventArgs args) {
      Log("GraphControl CompoundKeyPressed: " + args.KeyCode, "GraphControlKeyPressed");
    }

    private void ControlOnCompoundKeyReleased(object sender, CompoundKeyEventArgs args) {
      Log("GraphControl CompoundKeyReleased: " + args.KeyCode, "GraphControlKeyReleased");
    }

    private void ControlOnCompoundKeyTyped(object sender, CompoundKeyEventArgs args) {
      Log("GraphControl CompoundKeyTyped: " + args.KeyCode, "GraphControlKeyTyped");
    }

    #endregion

    #region GraphControl Mouse Events

    private void ControlOnMouse2DClicked(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DClicked");
    }

    private void ControlOnMouse2DDragged(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DDragged");
    }

    private void ControlOnMouse2DEntered(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DEntered");
    }

    private void ControlOnMouse2DExited(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DExited");
    }

    private void ControlOnMouse2DLostCapture(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DLostCapture");
    }

    private void ControlOnMouse2DMoved(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DMoved");
    }

    private void ControlOnMouse2DPressed(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DPressed");
    }

    private void ControlOnMouse2DReleased(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DReleased");
    }

    private void ControlOnMouse2DWheelTurned(object sender, Mouse2DEventArgs me) {
      Log("GraphControl Mouse2DWheelTurned");
    }

    #endregion
    
    #region GraphControl Render Events

    private void ControlOnPrepareRenderContext(object src, PrepareRenderContextEventArgs args) {
      Log("GraphControl PrepareRenderContext");
    }
    
    #endregion

    #region GraphControl Navigation Events

    private void ControlOnMouseWheelZoomFactorChanged(object sender,
                                                      EventArgs
                                                          dependencyPropertyChangedEventArgs) {
      Log("GraphControl MouseWheelZoomFactorChanged");
    }

    private void ControlOnViewportChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
      Log("GraphControl ViewportChanged");
    }

    private void ControlOnZoomChanged(object sender,
                                      EventArgs dependencyPropertyChangedEventArgs) {
      Log("GraphControl ZoomChanged");
    }

    #endregion

    #region Node Layout Changed Event

    private void OnNodeLayoutChanged(object source, INode node, RectD oldLayout) {
      Log("Node Layout Changed");
    }

    #endregion

    #region GraphEditorInputMode Events

    private void GeimOnCanvasClicked(object sender, ClickEventArgs args) {
      Log("GraphEditorInputMode CanvasClicked");
    }

    private void GeimOnDeletedItem(object sender, ItemEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode DeletedItem");
    }

    private void GeimOnDeletedSelection(object sender, SelectionEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode DeletedSelection");
    }

    private void GeimOnDeletingSelection(object sender, SelectionEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode DeletingSelection");
    }

    private void GeimOnItemClicked(object sender, ItemClickedEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode ItemClicked " + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnItemDoubleClicked(object sender, ItemClickedEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode ItemDoubleClicked" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnItemLeftClicked(object sender, ItemClickedEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode ItemLeftClicked" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnItemLeftDoubleClicked(object sender, ItemClickedEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode ItemLeftDoubleClicked" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnItemRightClicked(object sender, ItemClickedEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode ItemRightClicked" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnItemRightDoubleClicked(object sender, ItemClickedEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode ItemRightDoubleClicked" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnLabelAdding(object sender, LabelEditingEventArgs e) {
      Log("GraphEditorInputMode LabelAdding");
    }

    private void GeimOnLabelAdded(object sender, ItemEventArgs<ILabel> args) {
      Log("GraphEditorInputMode LabelAdded");
    }

    private void GeimOnLabelEditing(object sender, LabelEditingEventArgs e) {
      Log("GraphEditorInputMode LabelEditing");
    }

    private void GeimOnLabelTextChanged(object sender, ItemEventArgs<ILabel> args) {
      Log("GraphEditorInputMode LabelTextChanged");
    }

    private void GeimOnLabelTextEditingStarted(object sender, ItemEventArgs<ILabel> args) {
      Log("GraphEditorInputMode LabelTextEditingStarted");
    }

    private void GeimOnLabelTextEditingCanceled(object sender, ItemEventArgs<ILabel> args) {
      Log("GraphEditorInputMode LabelTextEditingCanceled");
    }

    private void GeimOnMultiSelectionFinished(object sender, SelectionEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode MultiSelectionFinished");
    }

    private void GeimOnMultiSelectionStarted(object sender, SelectionEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode MultiSelectionStarted");
    }

    private void GeimOnNodeCreated(object sender, ItemEventArgs<INode> args) {
      Log("GraphEditorInputMode NodeCreated");
    }

    private void GeimOnNodeReparented(object sender, NodeEventArgs args) {
      Log("GraphEditorInputMode NodeReparented");
    }

    private void GeimOnEdgePortsChanged(object sender, EdgeEventArgs args) {
      Log("GraphEditorInputMode Edge " + args.Item + " Ports Changed from " + args.SourcePort + "->" + args.TargetPort + " to "  + args.Item.SourcePort+ "->" + args.Item.TargetPort);
    }

    private void GeimOnPopulateItemContextMenu(object sender, PopulateItemContextMenuEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode PopulateItemContextMenu" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnQueryItemToolTip(object sender, QueryItemToolTipEventArgs<IModelItem> args) {
      Log("GraphEditorInputMode QueryItemToolTip" + (args.Handled ? "(Handled)" : "(Unhandled)"));
    }

    private void GeimOnValidateLabelText(object sender, LabelTextValidatingEventArgs args) {
      Log("GraphEditorInputMode ValidateLabelText");
    }

    private void GeimOnElementsCopied(object sender, EventArgs e) {
      Log("GraphEditorInputMode ElementsCopied");
    }

    private void GeimOnElementsCut(object sender, EventArgs e) {
      Log("GraphEditorInputMode ElementsCut");
    }

    private void GeimOnElementsPasted(object sender, EventArgs e) {
      Log("GraphEditorInputMode ElementsPasted");
    }

    #endregion

    #region GraphViewerInputMode Events

    private void GvimOnCanvasClicked(object sender, ClickEventArgs itemInputEventArgs) {
      Log("GraphViewerInputMode CanvasClicked");
    }

    private void GvimOnItemClicked(object sender, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      Log("GraphViewerInputMode ItemClicked");
    }

    private void GvimOnItemDoubleClicked(object sender, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      Log("GraphViewerInputMode ItemDoubleClicked");
    }

    private void GvimOnItemLeftClicked(object sender, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      Log("GraphViewerInputMode ItemLeftClicked");
    }

    private void GvimOnItemLeftDoubleClicked(object sender, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      Log("GraphViewerInputMode ItemLeftDoubleClicked");
    }

    private void GvimOnItemRightClicked(object sender, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      Log("GraphViewerInputMode ItemRightClicked");
    }

    private void GvimOnItemRightDoubleClicked(object sender, ItemClickedEventArgs<IModelItem> itemClickedEventArgs) {
      Log("GraphViewerInputMode ItemRightDoubleClicked");
    }

    private void GvimOnMultiSelectionFinished(object sender, SelectionEventArgs<IModelItem> eventArgs) {
      Log("GraphViewerInputMode MultiSelectionFinished");
    }

    private void GvimOnMultiSelectionStarted(object sender, SelectionEventArgs<IModelItem> eventArgs) {
      Log("GraphViewerInputMode MultiSelectionStarted");
    }

    private void GvimOnPopulateItemContextMenu(object sender,
                                               PopulateItemContextMenuEventArgs<IModelItem>
                                                   populateItemContextMenuEventArgs) {
      Log("GraphViewerInputMode PopulateItemContextMenu");
    }

    private void GvimOnQueryItemToolTip(object sender, QueryItemToolTipEventArgs<IModelItem> queryItemToolTipEventArgs) {
      Log("GraphViewerInputMode QueryItemToolTip");
    }

    private void GvimOnElementsCopied(object sender, EventArgs e) {
      Log("GraphViewerInputMode ElementsCopied");
    }

    #endregion

    #region Drag Events

    private void InputModeOnDragCanceled(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " DragCanceled", "DragCanceled");
    }

    private void InputModeOnDragCanceling(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " DragCanceling", "DragCanceling");
    }

    private void InputModeOnDragFinished(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " DragFinished", "DragFinished");
    }

    private void InputModeOnDragFinishing(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " DragFinishing" + GetAffectedItems(sender), "DragFinishing");
    }

    private void InputModeOnDragged(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " Dragged", "Dragged");
    }

    private void InputModeOnDragging(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " Dragging", "Dragging");
    }

    private void InputModeOnDragStarted(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " DragStarted" + GetAffectedItems(sender), "DragStarted");
    }

    private static string GetAffectedItems(object sender) {
      IEnumerable<IModelItem> items = null;
      var mim = sender as MoveInputMode;
      if (mim != null) {
        items = mim.AffectedItems;
      }
      var him = sender as HandleInputMode;
      if (him != null) {
        items = him.AffectedItems;
      }
      if (items != null) {
        var nodeCount = items.OfType<INode>().Count();
        var edgeCount = items.OfType<IEdge>().Count();
        var bendCount = items.OfType<IBend>().Count();
        var labelCount = items.OfType<ILabel>().Count();
        var portCount = items.OfType<IPort>().Count();
        return String.Format(" ({0} items: {1} nodes, {2} bends, {3} edges, {4} labels, {5} ports)", items.Count(),
          nodeCount, bendCount, edgeCount, labelCount, portCount);
      } else {
        return "";
      }
    }

    private void InputModeOnDragStarting(object sender, InputModeEventArgs inputModeEventArgs) {
      Log(sender.GetType().Name + " DragStarting", "DragStarting");
    }

    #endregion

    #region ItemHoverInputMode Events

    private void ItemHoverInputModeOnHoveredItemChanged(object sender, HoveredItemChangedEventArgs args) {
      Log("HoverInputMode Item changed from " + args.OldItem + " to " + (args.Item != null ? args.Item.ToString() : "null"), "HoveredItemChanged");
    }

    #endregion

    #region CreateBendInputMode Events

    private void CreateBendInputModeOnBendCreated(object sender, ItemEventArgs<IBend> itemEventArgs) {
      Log("CreateBendInputMode Bend Created");
    }

    #endregion

    #region ContextMenuInputMode Events

    private void ContextMenuInputModeOnPopulateMenu(object sender,
                                                           PopulateMenuEventArgs populateMenuEventArgs) {
      Log("ContextMenuInputMode Populate Menu");
    }

    #endregion

    #region TextEditorInputMode Events

    private void TextEditorInputModeOnEditingCanceled(object sender,  EventArgs args) {
      Log("TextEditorInputMode Editing Canceled");
    }

    private void TextEditorInputModeOnEditingStarted(object sender,  EventArgs args) {
      Log("TextEditorInputMode Editing Started");
    }

    private void TextEditorInputModeOnTextEdited(object sender,  EventArgs args) {
      Log("TextEditorInputMode Text Edited");
    }

    #endregion

    #region MouseHoverInputMode Events

    private void MouseHoverInputModeOnQueryToolTip(object sender, ToolTipQueryEventArgs toolTipQueryEventArgs) {
      Log("MouseHoverInputMode QueryToolTip");
    }

    #endregion

    #region ClickInputMode Events

    private void ClickInputModeOnClicked(object sender, ClickEventArgs clickEventArgs) {
      Log("ClickInputMode Clicked");
    }

    private void ClickInputModeOnDoubleClicked(object sender, ClickEventArgs clickEventArgs) {
      Log("ClickInputMode Double Clicked");
    }

    private void ClickInputModeOnLeftClicked(object sender, ClickEventArgs clickEventArgs) {
      Log("ClickInputMode Left Clicked");
    }

    private void ClickInputModeOnLeftDoubleClicked(object sender, ClickEventArgs clickEventArgs) {
      Log("ClickInputMode Left Double Clicked");
    }

    private void ClickInputModeOnRightClicked(object sender, ClickEventArgs clickEventArgs) {
      Log("ClickInputMode Right Clicked");
    }

    private void ClickInputModeOnRightDoubleClicked(object sender, ClickEventArgs clickEventArgs) {
      Log("ClickInputMode Right Double Clicked");
    }

    #endregion

    #region NavigationInputMode Events

    private void NavigationInputModeOnGroupCollapsed(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Collapsed: " + evt.Item, "GroupCollapsed");
    }

    private void NavigationInputModeOnGroupCollapsing(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Collapsing: " + evt.Item, "Group Collapsing");
    }

    private void NavigationInputModeOnGroupEntered(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Entered: " + evt.Item, "Group Entered");
    }

    private void NavigationInputModeOnGroupEntering(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Entering: " + evt.Item, "Group Entering");
    }

    private void NavigationInputModeOnGroupExited(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Exited: " + evt.Item, "Group Exited");
    }

    private void NavigationInputModeOnGroupExiting(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Exiting: " + evt.Item, "Group Exiting");
    }

    private void NavigationInputModeOnGroupExpanded(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Expanded: " + evt.Item, "Group Expanded");
    }

    private void NavigationInputModeOnGroupExpanding(object source, ItemEventArgs<INode> evt) {
      Log("NavigationInputMode Group Expanding: " + evt.Item, "Group Expanding");
    }

    #endregion

    #region CreateEdgeInputMode Events

    private void CreateEdgeInputModeOnEdgeCreated(object sender, ItemEventArgs<IEdge> itemEventArgs) {
      Log("CreateEdgeInputMode Edge Created");
    }

    private void CreateEdgeInputModeOnEdgeCreationStarted(object sender, ItemEventArgs<IEdge> itemEventArgs) {
      Log("CreateEdgeInputMode Edge Creation Started");
    }

    private void CreateEdgeInputModeOnGestureCanceled(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Gesture Canceled");
    }

    private void CreateEdgeInputModeOnGestureCanceling(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Gesture Canceling");
    }

    private void CreateEdgeInputModeOnGestureFinished(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Gesture Finished");
    }

    private void CreateEdgeInputModeOnGestureFinishing(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Gesture Finishing");
    }

    private void CreateEdgeInputModeOnGestureStarted(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Gesture Started");
    }

    private void CreateEdgeInputModeOnGestureStarting(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Gesture Starting");
    }

    private void CreateEdgeInputModeOnMoved(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Moved");
    }

    private void CreateEdgeInputModeOnMoving(object sender, InputModeEventArgs inputModeEventArgs) {
      Log("CreateEdgeInputMode Moving");
    }

    private void CreateEdgeInputModeOnPortAdded(object source, ItemEventArgs<IPort> evt) {
      Log("CreateEdgeInputMode Port Added");
    }

    private void CreateEdgeInputModeOnSourcePortCandidateChanged(object sender, ItemEventArgs<IPortCandidate> e) {
      Log("CreateEdgeInputMode Source Port Candidate Changed");
    }

    private void CreateEdgeInputModeOnTargetPortCandidateChanged(object sender, ItemEventArgs<IPortCandidate> e) {
      Log("CreateEdgeInputMode Target Port Candidate Changed");
    }

    #endregion

    #region Selection Events

    private void OnItemSelectionChanged(object source, ItemSelectionChangedEventArgs<IModelItem> evt) {
      if (evt.ItemSelected) {
        Log("GraphControl Item Selected: " + evt.Item, "ItemSelected");
      } else {
        Log("GraphControl Item Deselected: " + evt.Item, "ItemDeselected");
      }
    }

    #endregion

    #region UI event handlers

    private void ClearButtonClick(object sender, EventArgs e) {
      ClearLog();
    }

    private void ExitToolStripMenuItemClick(object sender, EventArgs e) {
      Application.Exit();
    }

    #endregion

    #region Logging

    private readonly List<ILogEntry> messages = new List<ILogEntry>();
    private bool dirty = false;

    private void Log(string message, string type = null) {
      if (type == null) {
        type = message;
      }
      var msg = new Message { Text = message, TimeStamp = DateTime.Now, Type = type };
      messages.Insert(0, msg);

      if (groupEvents.Checked) {
        MergeEvents();
      }
      dirty = true;
    }

    private void MergeEvents() {
      MergeWithLatestGroup();
      CreateNewGroup();
    }

    private void MergeWithLatestGroup() {
      var latestGroup = messages.FirstOrDefault(m => m is MessageGroup) as MessageGroup;
      if (latestGroup == null) {
        return;
      }

      var precedingEvents = messages.TakeWhile(m => m is Message).Cast<Message>().ToList();
      var groupCount = latestGroup.RepeatedMessages.Count;
      if (precedingEvents.Count < groupCount) {
        return;
      }

      var precedingTypes = precedingEvents.Select(m => m.Type);
      var groupTypes = latestGroup.RepeatedMessages.Select(m => m.Type);
      // Merge into group
      if (groupTypes.SequenceEqual(precedingTypes)) {
        latestGroup.RepeatedMessages.Clear();
        foreach (var m in precedingEvents) {
          latestGroup.RepeatedMessages.Add(m);
        }
        latestGroup.RepeatCount++;
        for (int i = messages.IndexOf(latestGroup) - 1; i >= 0; i--) {
          messages.RemoveAt(i);
        }
      }
    }

    private void CreateNewGroup() {
      var ungroupedEvents = messages.TakeWhile(m => m is Message).Cast<Message>().ToList();
      for (int start = ungroupedEvents.Count - 1; start >= 1; start--) {
        for (int length = 1; start - 2 * length + 1 >= 0; length++) {
          var types = ungroupedEvents.GetRange(start - length + 1, length).Select(m => m.Type);
          var preceding = ungroupedEvents.GetRange(start - 2 * length + 1, length);
          var precedingTypes = preceding.Select(m => m.Type);
          if (types.SequenceEqual(precedingTypes)) {
            var group = new MessageGroup();
            group.RepeatCount = 2;
            foreach (var m in preceding) {
              group.RepeatedMessages.Add(m);
            }
            messages.Insert(start + 1, group);
            for (int i = start; i >= start - 2 * length + 1; i--) {
              messages.RemoveAt(i);
            }
            return;
          }
        }
      }
    }
    
    private void ClearLog() {
      messages.Clear();
      dirty = true;
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
      Application.Run(new GraphEventsForm());
    }

    #endregion

    private static readonly StringFormat ellipsisFormat = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter};
    private static readonly StringFormat countFormat = new StringFormat() { Alignment = StringAlignment.Far };

    private void MessagePaneDrawItem(object sender, DrawItemEventArgs e) {
      if (e.Index == -1) {
        return;
      }
      var g = e.Graphics;
      var bounds = e.Bounds;
      var item = messagePane.Items[e.Index] as ILogEntry;
      var group = item as MessageGroup;
      g.FillRectangle(SystemBrushes.Window, bounds);
      if (group != null) {
        g.DrawRectangle(Pens.DarkBlue, new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4));
        for (int i = 0; i < group.RepeatedMessages.Count; i++) {
          var newBounds = new Rectangle(bounds.X + 3, bounds.Y + 15 / 2 + i * 15, bounds.Width - 45 - 3, 15);
          g.DrawString(group.RepeatedMessages[i].ToString(), e.Font, Brushes.Black, newBounds, ellipsisFormat);
        }
        g.DrawString(group.RepeatCount.ToString(), e.Font, Brushes.DarkBlue,
            new Rectangle(bounds.X + bounds.Width - 45 - 3, bounds.Y + bounds.Height / 2 - 15 / 2, 40, 15), countFormat);

      }
      else {
        g.DrawString(item.ToString(), e.Font, Brushes.Black, new Rectangle(bounds.X + 3, bounds.Y, bounds.Width - 6, 15),
            StringFormat.GenericDefault);
      }
    }

    private void MessagePaneMeasureItem(object sender, MeasureItemEventArgs e) {
      var group = messagePane.Items[e.Index] as MessageGroup;
      e.ItemHeight = (group != null ? group.RepeatedMessages.Count + 1 : 1) * 15;
    }
  }

  #region Logging classes

  public interface ILogEntry {}

  public class Message : ILogEntry
  {
    public DateTime TimeStamp { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }

    public override string ToString() {
      return "[" + TimeStamp.ToString("HH:mm:ss") + "]: " + Text;
    }
  }


  public class MessageGroup : ILogEntry, INotifyPropertyChanged
  {
    private readonly List<Message> repeatedMessages = new List<Message>();
    private int repeatCount;

    public List<Message> RepeatedMessages {
      get { return repeatedMessages; }
    }

    public int RepeatCount {
      get { return repeatCount; }
      set {
        repeatCount = value;
        OnPropertyChanged("RepeatCount");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName) {
      var handler = PropertyChanged;
      if (handler != null) {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }

  #endregion
}
