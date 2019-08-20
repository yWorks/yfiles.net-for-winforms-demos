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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using Demo.yFiles.Graph.Bpmn.Util;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;
using IIcon = Demo.yFiles.Graph.Bpmn.Util.IIcon;

namespace Demo.yFiles.Graph.Bpmn.Styles {

  /// <summary>
  /// An <see cref="INodeStyle"/> implementation representing an Choreography according to the BPMN.
  /// </summary>
  [Obfuscation(StripAfterObfuscation = false, Exclude = true, ApplyToMembers = false)]
  public class ChoreographyNodeStyle : BpmnNodeStyle
  {

    #region Initialize static fields

    private static readonly ShapeNodeStyle sns;

    private static readonly IIcon topInitiatingMessageIcon;
    private static readonly IIcon bottomResponseMessageIcon;
    private static readonly IIcon bottomInitiatingMessageIcon;
    private static readonly IIcon topResponseMessageIcon;
    private static readonly IIcon taskBandBackgroundIcon;
    private static readonly IIcon multiInstanceIcon;
    private const int MessageDistance = 15;


    static ChoreographyNodeStyle() {

      sns = new ShapeNodeStyle(new ShapeNodeStyleRenderer {RoundRectArcRadius = BpmnConstants.ChoreographyCornerRadius})
      {
        Shape = ShapeNodeShape.RoundRectangle,
        Pen = Pens.Black,
        Brush = null
      };

      var lineIcon = IconFactory.CreateLine(BpmnConstants.Pens.ChoreographyMessageLink, 0.5, 0, 0.5, 1);
      var initiatingMessageIcon = IconFactory.CreateMessage(BpmnConstants.Pens.Message, BpmnConstants.Brushes.ChoreographyInitializingParticipant);
      var responseMessageIcon = IconFactory.CreateMessage(BpmnConstants.Pens.Message, BpmnConstants.Brushes.ChoreographyReceivingParticipant);

      var icons = new List<IIcon>();
      icons.Add(IconFactory.CreatePlacedIcon(lineIcon, ExteriorLabelModel.North, new SizeD(MessageDistance, MessageDistance)));
      icons.Add(IconFactory.CreatePlacedIcon(initiatingMessageIcon, BpmnConstants.Placements.ChoreographyTopMessage, BpmnConstants.Sizes.Message));
      topInitiatingMessageIcon = IconFactory.CreateCombinedIcon(icons);

      icons = new List<IIcon>();
      icons.Add(IconFactory.CreatePlacedIcon(lineIcon, ExteriorLabelModel.South, new SizeD(MessageDistance, MessageDistance)));
      icons.Add(IconFactory.CreatePlacedIcon(responseMessageIcon, BpmnConstants.Placements.ChoreographyBottomMessage, BpmnConstants.Sizes.Message));
      bottomResponseMessageIcon = IconFactory.CreateCombinedIcon(icons);
        
      icons = new List<IIcon>();
      icons.Add(IconFactory.CreatePlacedIcon(lineIcon, ExteriorLabelModel.South, new SizeD(MessageDistance, MessageDistance)));
      icons.Add(IconFactory.CreatePlacedIcon(initiatingMessageIcon, BpmnConstants.Placements.ChoreographyBottomMessage, BpmnConstants.Sizes.Message));
      bottomInitiatingMessageIcon = IconFactory.CreateCombinedIcon(icons);

      icons = new List<IIcon>();
      icons.Add(IconFactory.CreatePlacedIcon(lineIcon, ExteriorLabelModel.North, new SizeD(MessageDistance, MessageDistance)));
      icons.Add(IconFactory.CreatePlacedIcon(responseMessageIcon, BpmnConstants.Placements.ChoreographyTopMessage, BpmnConstants.Sizes.Message));
      topResponseMessageIcon = IconFactory.CreateCombinedIcon(icons);

      taskBandBackgroundIcon = IconFactory.CreateChoreographyTaskBand();
      multiInstanceIcon = IconFactory.CreatePlacedIcon(IconFactory.CreateLoopCharacteristic(LoopCharacteristic.Parallel),
        BpmnConstants.Placements.ChoreographyMarker,
        BpmnConstants.Sizes.Marker);
    }

    #endregion

    #region Properties

    private ChoreographyType type;

    /// <summary>
    /// Gets or sets the choreography type of this style. 
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(ChoreographyType.Task)]
    public ChoreographyType Type {
      get { return type; }
      set {
        if (type != value || outlineIcon == null) {
          ModCount++;
          type = value;
          outlineIcon = IconFactory.CreateChoreography(type);
          if (type == ChoreographyType.Call) {
            outlineIcon = new PlacedIcon(outlineIcon, BpmnConstants.Placements.ThickLine, SizeD.Empty);
          }
        }
      }
    }

    private LoopCharacteristic loopCharacteristic;

    /// <summary>
    /// Gets or sets the loop characteristic of this style. 
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(LoopCharacteristic.None)]
    public LoopCharacteristic LoopCharacteristic {
      get { return loopCharacteristic; }
      set {
        if (loopCharacteristic != value) {
          ModCount++;
          loopCharacteristic = value;
          loopIcon = IconFactory.CreateLoopCharacteristic(value);
        }
      }
    }


    private SubState subState;

    /// <summary>
    /// Gets or sets the sub state of this style.
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(SubState.None)]
    public SubState SubState {
      get { return subState; }
      set {
        if (subState != value) {
          ModCount++;
          subState = value;
        }
      }
    }

    private bool initiatingMessage;

    /// <summary>
    /// Gets or sets whether the initiating message icon is displayed. 
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(false)]
    public bool InitiatingMessage {
      get { return initiatingMessage; }
      set {
        if (initiatingMessage != value) {
          ModCount++;
          initiatingMessage = value;
        }
      }
    }

    private bool responseMessage;

    /// <summary>
    /// Gets or sets whether the response message icon is displayed. 
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(false)]
    public bool ResponseMessage {
      get { return responseMessage; }
      set {
        if (responseMessage != value) {
          ModCount++;
          responseMessage = value;
        }
      }
    }

    private bool initiatingAtTop = true;

    /// <summary>
    /// Gets or sets whether the initiating message icon or the response message icon is displayed on top of the node while the other one is at the bottom side. 
    /// </summary>
    /// <remarks>
    /// Whether the initiating and response message icons are displayed at all depends on <see cref="InitiatingMessage"/> and <see cref="ResponseMessage"/>.
    /// This property only determines which one is displayed on which side of the node.
    /// </remarks>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(true)]
    public bool InitiatingAtTop {
      get { return initiatingAtTop; }
      set {
        if (initiatingAtTop != value) {
          initiatingAtTop = value;
          if (InitiatingMessage || ResponseMessage) {
            ModCount++;
          }
        }
      }
    }

    private readonly ParticipantList topParticipants = new ParticipantList();

    /// <summary>
    /// Gets the list of <see cref="Participant"/>s at the top of the node, ordered from top to bottom.
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public IList<Participant> TopParticipants {
      get { return topParticipants; }
    }

    private readonly ParticipantList bottomParticipants = new ParticipantList();

    /// <summary>
    /// Gets the list of <see cref="Participant"/>s at the bottom of the node, ordered from bottom to top.
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public IList<Participant> BottomParticipants {
      get { return bottomParticipants; }
    }


    /// <summary>
    /// Gets or sets the insets for the task name band of the given item.
    /// </summary>
    /// <remarks>
    /// These insets are extended by the sizes of the participant bands on top and bottom side 
    /// and returned via an <see cref="INodeInsetsProvider"/> if such an instance is queried through the
    /// <see cref="INodeStyleRenderer.GetContext">context lookup</see>.
    /// </remarks>
    /// <seealso cref="INodeInsetsProvider"/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(typeof(InsetsD), "5")]
    public InsetsD Insets {
      get { return insets; }
      set { insets = value; }
    }

    private bool ShowTopMessage {
      get { return (InitiatingMessage && InitiatingAtTop) || (ResponseMessage && !InitiatingAtTop); }
    }

    private bool ShowBottomMessage {
      get { return (InitiatingMessage && !InitiatingAtTop) || (ResponseMessage && InitiatingAtTop); }
    }

    #endregion

    private IIcon outlineIcon;
    private IIcon loopIcon;
    private InsetsD insets = new InsetsD(5);

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public ChoreographyNodeStyle() {
      Type = ChoreographyType.Task;
      MinimumSize = new SizeD(30, 30);
      LoopCharacteristic = LoopCharacteristic.None;
      SubState = SubState.None;
    }

    #region IVisualCreator methods

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override IVisual CreateVisual(IRenderContext context, INode node) {
      var bounds = node.Layout.ToRectD();
      var container = new ChoreographyContainer();

      // task band
      var taskBandContainer = new TaskBandContainer();
      var bandIcon = CreateTaskBandIcon(node);
      bandIcon.SetBounds(GetRelativeTaskNameBandBounds(node));
      taskBandContainer.Add(bandIcon.CreateVisual(context));
      taskBandContainer.Icon = bandIcon;
      container.Children.Add(taskBandContainer);

      // top participants
      double topOffset = 0;
      bool first = true;
      foreach (Participant participant in topParticipants) {
        var participantIcon = CreateParticipantIcon(participant, true, first);
        var height = participant.GetSize();
        participantIcon.SetBounds(new RectD(0, topOffset, bounds.Width, height));
        container.Add(participantIcon.CreateVisual(context));
        container.TopParticipantIcons.Add(participantIcon);
        topOffset += height;
        first = false;
      }

      // bottom participants
      double bottomOffset = bounds.Height;
      first = true;
      foreach (Participant participant in bottomParticipants) {
        var participantIcon = CreateParticipantIcon(participant, false, first);
        var height = participant.GetSize();
        bottomOffset -= height;
        participantIcon.SetBounds(new RectD(0, bottomOffset, bounds.Width, height));
        container.Add(participantIcon.CreateVisual(context));
        container.BottomParticipantIcons.Add(participantIcon);
        first = false;
      }

      // outline
      outlineIcon.SetBounds(new RectD(PointD.Origin, bounds.Size));
      container.Add(outlineIcon.CreateVisual(context));

      // messages
      if (InitiatingMessage) {
        var initiatingMessageIcon = InitiatingAtTop ? topInitiatingMessageIcon : bottomInitiatingMessageIcon;
        initiatingMessageIcon.SetBounds(new RectD(0, 0, bounds.Width, bounds.Height));
        container.Add(initiatingMessageIcon.CreateVisual(context));
      }
      if (ResponseMessage) {
        var responseMessageIcon = InitiatingAtTop ? bottomResponseMessageIcon : topResponseMessageIcon;
        responseMessageIcon.SetBounds(new RectD(0, 0, bounds.Width, bounds.Height));
        container.Add(responseMessageIcon.CreateVisual(context));
      }

      container.Transform = new Matrix(1,0,0,1,(float) bounds.X, (float) bounds.Y);
      container.ModCount = ModCount + topParticipants.ModCount + bottomParticipants.ModCount;
      container.Bounds = bounds;
      return container;
    }

    private class ChoreographyContainer : VisualGroup
    {
      public int ModCount;
      public RectD Bounds;
      public readonly IList<IIcon> TopParticipantIcons = new List<IIcon>();
      public readonly IList<IIcon> BottomParticipantIcons = new List<IIcon>();
    }

    private class TaskBandContainer : VisualGroup
    {
      public IIcon Icon;
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override IVisual UpdateVisual(IRenderContext context, IVisual oldVisual, INode node) {
      return CreateVisual(context, node);
      var container = oldVisual as ChoreographyContainer;
      if (container == null) {
        return CreateVisual(context, node);
      }
      var cache = container;
      var currentModCount = ModCount + topParticipants.ModCount + bottomParticipants.ModCount;
      if (cache.ModCount != currentModCount) {
        return CreateVisual(context, node);
      }

      var newBounds = node.Layout.ToRectD();

      if (cache.Bounds == newBounds) {
        return container;
      }

      if (cache.Bounds.Width != newBounds.Width || cache.Bounds.Height != newBounds.Height) {
        // update icon bounds
        int childIndex = 0;

        // task band
        var taskBandContainer = container.Children[childIndex++] as TaskBandContainer;
        var taskBandIcon = taskBandContainer != null ? taskBandContainer.Icon : null;
        var taskBandBounds = GetRelativeTaskNameBandBounds(node);

        if (taskBandIcon != null && taskBandContainer.Children.Count == 1) {
          taskBandIcon.SetBounds(taskBandBounds);
          UpdateChildVisual(context, taskBandContainer, 0, taskBandIcon);
        }

        // top participants
        double topOffset = 0;
        for (int i = 0; i < topParticipants.Count; i++) {
          var participant = topParticipants[i];
          var participantIcon = cache.TopParticipantIcons[i];
          var height = participant.GetSize();
          participantIcon.SetBounds(new RectD(0, topOffset, newBounds.Width, height));
          UpdateChildVisual(context, container, childIndex++, participantIcon);
          topOffset += height;
        }

        // bottom participants
        double bottomOffset = newBounds.Height;
        for (int i = 0; i < bottomParticipants.Count; i++) {
          var participant = bottomParticipants[i];
          var participantIcon = cache.BottomParticipantIcons[i];
          var height = participant.GetSize();
          bottomOffset -= height;
          participantIcon.SetBounds(new RectD(0, bottomOffset, newBounds.Width, height));
          UpdateChildVisual(context, container, childIndex++, participantIcon);
        }

        // outline
        outlineIcon.SetBounds(new RectD(PointD.Origin, newBounds.Size));
        UpdateChildVisual(context, container, childIndex++, outlineIcon);

        // messages
        if (InitiatingMessage) {
          var initiatingMessageIcon = InitiatingAtTop ? topInitiatingMessageIcon : bottomInitiatingMessageIcon;
          initiatingMessageIcon.SetBounds(new RectD(0, 0, newBounds.Width, newBounds.Height));
          UpdateChildVisual(context, container, childIndex++, initiatingMessageIcon);
        }
        if (ResponseMessage) {
          var responseMessageIcon = InitiatingAtTop ? bottomResponseMessageIcon : topResponseMessageIcon;
          responseMessageIcon.SetBounds(new RectD(0, 0, newBounds.Width, newBounds.Height));
          UpdateChildVisual(context, container, childIndex++, responseMessageIcon);
        }
      }
      container.Bounds = newBounds;
      container.Transform = new Matrix(1, 0, 0, 1, (float) newBounds.X, (float) newBounds.Y);
      return container;
    }

    private IIcon CreateTaskBandIcon(INode node) {
      IIcon collapseIcon = null;
      if (SubState != SubState.None) {
        collapseIcon = SubState == SubState.Dynamic ? IconFactory.CreateDynamicSubState(node) : IconFactory.CreateStaticSubState(SubState);
      }

      IIcon markerIcon = null;
      if (loopIcon != null && collapseIcon != null) {
        markerIcon = IconFactory.CreateLineUpIcon(new List<IIcon>(new [] {loopIcon, collapseIcon}),
          BpmnConstants.Sizes.Marker, 5);
      } else if (loopIcon != null) {
        markerIcon = loopIcon;
      } else if (collapseIcon != null) {
        markerIcon = collapseIcon;
      }
      if (markerIcon != null) {
        var placedMarkers = IconFactory.CreatePlacedIcon(markerIcon, BpmnConstants.Placements.ChoreographyMarker,
          BpmnConstants.Sizes.Marker);
        return IconFactory.CreateCombinedIcon(new List<IIcon>(new[] {taskBandBackgroundIcon, placedMarkers}));
      } else {
        return taskBandBackgroundIcon;
      }
    }

    private IIcon CreateParticipantIcon(Participant participant, bool top, bool isFirst) {
      var isInitializing = isFirst && (top ^ !InitiatingAtTop);

      var icon = IconFactory.CreateChoreographyParticipant(isInitializing,
        top && isFirst ? BpmnConstants.ChoreographyCornerRadius : 0,
        !top && isFirst ? BpmnConstants.ChoreographyCornerRadius : 0);
      if (participant.MultiInstance) {
        icon = IconFactory.CreateCombinedIcon(new List<IIcon>(new[] { icon, multiInstanceIcon }));
      }
      return icon;
    }

    private static void UpdateChildVisual(IRenderContext context, VisualGroup container, int index, IVisualCreator icon) {
      var oldPathVisual = container.Children[index];
      var newPathVisual = icon.UpdateVisual(context, oldPathVisual);
      if (!oldPathVisual.Equals(newPathVisual)) {
        newPathVisual = newPathVisual ?? new VisualGroup();
        container.Children.Remove(oldPathVisual);
        container.Children.Insert(index, newPathVisual);
      }
    }

#endregion

    /// <summary>
    /// Returns the participant at the specified location.
    /// </summary>
    /// <param name="node">The node whose bounds shall be used.</param>
    /// <param name="location">The location of the participant.</param>
    /// <returns></returns>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    public Participant GetParticipant(INode node, PointD location) {
      if (!node.Layout.Contains(location)) {
        return null;
      }

      var relativeY = (location - node.Layout.GetTopLeft()).Y;
      if (relativeY < topParticipants.GetHeight()) {
        foreach (Participant participant in TopParticipants) {
          var size = participant.GetSize();
          if (relativeY < size) {
            return participant;
          }
          relativeY -= size;
        }
      } else if (node.Layout.Height - bottomParticipants.GetHeight() < relativeY) {
        var yFromBottom = node.Layout.Height - relativeY;
        foreach (Participant participant in BottomParticipants) {
          var size = participant.GetSize();
          if (yFromBottom < size) {
            return participant;
          }
          yFromBottom -= size;
        }
      }

      return null;
    }


    /// <summary>
    /// Returns the bounds of the specified participant band.
    /// </summary>
    /// <param name="owner">The node whose bounds shall be used.</param>
    /// <param name="index">The index of the participant in its list.</param>
    /// <param name="top">Whether the top of bottom list of participants shall be used.</param>
    /// <returns></returns>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    public RectD GetParticipantBandBounds(INode owner, int index, bool top) {
      var width = owner.Layout.Width;
      if (top && index <= topParticipants.Count) {
        int i = 0;
        double yOffset = 0;
        foreach (Participant topParticipant in topParticipants) {
          if (index == i++) {
            return new RectD(owner.Layout.X, owner.Layout.Y + yOffset, width, topParticipant.GetSize());
          } else {
            yOffset += topParticipant.GetSize();
          }
        }
      } else if (!top && index < bottomParticipants.Count) {
        int i = 0;
        double yOffset = owner.Layout.Height;
        foreach (Participant bottomParticipant in bottomParticipants) {
          yOffset -= bottomParticipant.GetSize();
          if (index == i++) {
            return new RectD(owner.Layout.X, owner.Layout.Y + yOffset, width, bottomParticipant.GetSize());
          }
        }
      }
      return GetTaskNameBandBounds(owner);
    }

    public ILabelModelParameter GetParticipantParameters(Participant participant) {

      bool top;
      int index;

      if (BottomParticipants.Contains(participant)) {
        top = false;
        index = BottomParticipants.IndexOf(participant);
      } else {
        top = true;
        index = TopParticipants.IndexOf(participant);
      }
      
      return ChoreographyLabelModel.Instance.CreateParticipantParameter(top, index);
    }
    
    /// <summary>
    /// Returns the bounds of the task name band.
    /// </summary>
    /// <param name="owner">The node whose bounds shall be used.</param>
    /// <returns></returns>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    public RectD GetTaskNameBandBounds(INode owner) {
      return GetRelativeTaskNameBandBounds(owner).GetTranslated(owner.Layout.GetTopLeft());
    }

    private RectD GetRelativeTaskNameBandBounds(INode owner) {
      var topHeight = topParticipants.GetHeight();
      return new RectD(0, topHeight, owner.Layout.Width, Math.Max(0, owner.Layout.Height - topHeight - bottomParticipants.GetHeight()));
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override GeneralPath GetOutline(INode node) {
      sns.Renderer.GetShapeGeometry(node, sns);
      var path = ((ShapeNodeStyleRenderer) sns.Renderer).GetOutline()??new GeneralPath();

      if (ShowTopMessage) {
        var topBoxSize = BpmnConstants.Sizes.Message;
        var cx = node.Layout.GetCenter().X;
        double topBoxMaxY = node.Layout.Y - MessageDistance;
        path.MoveTo(cx - topBoxSize.Width/2, node.Layout.Y);
        path.LineTo(cx - topBoxSize.Width/2, topBoxMaxY);
        path.LineTo(cx - topBoxSize.Width/2, topBoxMaxY - topBoxSize.Height);
        path.LineTo(cx + topBoxSize.Width/2, topBoxMaxY - topBoxSize.Height);
        path.LineTo(cx + topBoxSize.Width/2, topBoxMaxY);
        path.LineTo(cx - topBoxSize.Width / 2, topBoxMaxY);
        path.Close();
      }

      if (ShowBottomMessage) {
        var bottomBoxSize = BpmnConstants.Sizes.Message;
        var cx = node.Layout.GetCenter().X;
        var bottomBoxY = node.Layout.GetMaxY() + MessageDistance;
        path.MoveTo(cx - bottomBoxSize.Width / 2, node.Layout.GetMaxY());
        path.LineTo(cx - bottomBoxSize.Width/2, bottomBoxY);
        path.LineTo(cx - bottomBoxSize.Width / 2, bottomBoxY + bottomBoxSize.Height);
        path.LineTo(cx + bottomBoxSize.Width / 2, bottomBoxY + bottomBoxSize.Height);
        path.LineTo(cx + bottomBoxSize.Width / 2, bottomBoxY);
        path.LineTo(cx - bottomBoxSize.Width / 2, bottomBoxY);
        path.Close();
      }

      return path;
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override bool IsHit(IInputModeContext context, PointD location, INode node) {
      if (sns.Renderer.GetHitTestable(node, sns).IsHit(context, location)) {
        return true;
      }
      var layout = node.Layout.ToRectD();
      if (ShowTopMessage) {
        var cx = layout.GetCenter().X;
        var topBoxSize = BpmnConstants.Sizes.Message;
        var messageRect = new RectD(new PointD(cx - topBoxSize.Width/2, layout.Y - MessageDistance - topBoxSize.Height), topBoxSize);
        if (messageRect.Contains(location, context.HitTestRadius)) {
          return true;
        }
        if (Math.Abs(location.X - cx) < context.HitTestRadius && layout.Y - MessageDistance - context.HitTestRadius < location.Y && location.Y < layout.Y + context.HitTestRadius) {
          return true;
        }
      }

      if (ShowBottomMessage) {
        var bottomBoxSize = BpmnConstants.Sizes.Message;
        var cx = layout.GetCenter().X;
        var messageRect = new RectD(new PointD(cx - bottomBoxSize.Width / 2, layout.GetMaxY() + MessageDistance), bottomBoxSize);
        if (messageRect.Contains(location, context.HitTestRadius)) {
          return true;
        }
        if (Math.Abs(location.X - cx) < context.HitTestRadius && layout.GetMaxY() - context.HitTestRadius < location.Y && location.Y < layout.GetMaxY() + MessageDistance + context.HitTestRadius) {
          return true;
        }
      }
      return false;
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override RectD GetBounds(ICanvasContext context, INode node) {
      RectD bounds = node.Layout.ToRectD();
      if (ShowTopMessage) {
        bounds = bounds.GetEnlarged(new InsetsD(0, MessageDistance + BpmnConstants.Sizes.Message.Height, 0, 0));
      }
      if (ShowBottomMessage) {
        bounds = bounds.GetEnlarged(new InsetsD(0, 0, 0, MessageDistance + BpmnConstants.Sizes.Message.Height));
      }

      return bounds;
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    protected override object Lookup(INode node, Type type) {
      if (type == typeof(INodeSizeConstraintProvider)) {
        var minWidth = Math.Max(0, MinimumSize.Width);
        var minHeight = Math.Max(0, MinimumSize.Height) + topParticipants.GetHeight() + bottomParticipants.GetHeight();
        return new NodeSizeConstraintProvider(new SizeD(minWidth, minHeight), SizeD.Infinite);
      } else if (type == typeof (INodeInsetsProvider)) {
        return new ChoreographyInsetsProvider(this);
      } else if (type == typeof (IEditLabelHelper)) {
        return new ChoreographyEditLabelHelper(node);
      }
      if (type == typeof(IClickHandler)) {
        var bandIcon = CreateTaskBandIcon(node);
        if (bandIcon != null) {
          bandIcon.SetBounds(GetTaskNameBandBounds(node));
          return bandIcon.Lookup<IClickHandler>();
        }
      }
      return base.Lookup(node, type);
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    public override object Clone() {
      ChoreographyNodeStyle clone = new ChoreographyNodeStyle
      {
        InitiatingAtTop = InitiatingAtTop, 
        InitiatingMessage = InitiatingMessage, 
        Insets = Insets, 
        LoopCharacteristic = LoopCharacteristic, 
        MinimumSize = MinimumSize,
        ResponseMessage = ResponseMessage, 
        SubState = SubState, 
        Type = Type,
      };
      foreach (var participant in TopParticipants) {
        clone.TopParticipants.Add(participant.Clone());
      }
      foreach (var participant in BottomParticipants) {
        clone.BottomParticipants.Add(participant.Clone());
      }
      return clone;
    }


    private class ParticipantList : IList<Participant> {
      private readonly IList<Participant> innerList = new List<Participant>();

      private int modCount;

      public int ModCount {
        get {
          return modCount + GetParticipantModCount();
        }
      }

      public double GetHeight() {
        double height = 0;
        foreach (var participant in innerList) {
          height += participant.GetSize();
        }
        return height;
      }

      private int GetParticipantModCount() {
        var participantCount = 0;
        foreach (var participant in innerList) {
          participantCount += participant.ModCount;
        }
        return participantCount;
      }

      IEnumerator<Participant> IEnumerable<Participant>.GetEnumerator() {
        return innerList.GetEnumerator();
      }

      public IEnumerator GetEnumerator() {
        return innerList.GetEnumerator();
      }

      public void Add(Participant item) {
        modCount++;
        innerList.Add(item);
      }

      public void Clear() {
        modCount += GetParticipantModCount() + 1;
        innerList.Clear();
      }

      public bool Contains(Participant item) {
        return innerList.Contains(item);
      }

      public void CopyTo(Participant[] array, int arrayIndex) {
        innerList.CopyTo(array, arrayIndex);
      }

      public bool Remove(Participant item) {
        modCount += item.ModCount + 1;
        return innerList.Remove(item);
      }

      public int Count {
        get { return innerList.Count; }
      }

      public bool IsReadOnly {
        get { return innerList.IsReadOnly; }
      }

      public int IndexOf(Participant item) {
        return innerList.IndexOf(item);
      }

      public void Insert(int index, Participant item) {
        modCount++;
        innerList.Insert(index, item);
      }

      public void RemoveAt(int index) {
        modCount += innerList[index].ModCount + 1;
        innerList.RemoveAt(index);
      }

      public Participant this[int index] {
        get { return innerList[index]; }
        set { innerList[index] = value; }
      }
    }


    /// <summary>
    /// Uses the style insets extended by the size of the participant bands.
    /// </summary>
    private class ChoreographyInsetsProvider : INodeInsetsProvider {
      private readonly ChoreographyNodeStyle style;

      internal ChoreographyInsetsProvider(ChoreographyNodeStyle style) {
        this.style = style;
      }

      public InsetsD GetInsets(INode node) {
        double topInsets = ((ParticipantList) style.TopParticipants).GetHeight();
        double bottomInsets = ((ParticipantList) style.BottomParticipants).GetHeight();

        bottomInsets += style.LoopCharacteristic != LoopCharacteristic.None || style.SubState != SubState.None 
          ? BpmnConstants.Sizes.Marker.Height + ((InteriorLabelModel)BpmnConstants.Placements.ChoreographyMarker.Model).Insets.Bottom
        : 0;

        return new InsetsD(style.Insets.Left, style.Insets.Top + topInsets, style.Insets.Right, style.Insets.Bottom + bottomInsets);
      }
    }

    private class ChoreographyEditLabelHelper : IEditLabelHelper
    {
      private readonly INode node;

      public ChoreographyEditLabelHelper(INode node) {
        this.node = node;
      }

      public void OnLabelEditing(LabelEditingEventArgs args) {
        if (node.Labels.Count == 0) {
          OnLabelAdding(args);
          return;
        }
        args.Label = node.Labels[0];
        args.Handled = true;
      }

      public void OnLabelAdding(LabelEditingEventArgs args) {
        var parameter = ChoreographyLabelModel.Instance.FindNextParameter(node);
        ILabelStyle labelStyle;
        if (parameter == ChoreographyLabelModel.NorthMessage || parameter == ChoreographyLabelModel.SouthMessage) {
          labelStyle = new ChoreographyMessageLabelStyle();
        } else {
          labelStyle = ((GraphControl)args.Context.CanvasControl).Graph.NodeDefaults.Labels.Style;
        }
        if (parameter == null) {
          parameter = ExteriorLabelModel.West;
        }

        args.LayoutParameter = parameter;
        args.Owner = node;
        args.Style = labelStyle;
        args.Handled = true;
      }
    }
  }

  /// <summary>
  /// A participant of a Choreography that can be added to a <see cref="ChoreographyNodeStyle"/>.
  /// </summary>
  [Obfuscation(StripAfterObfuscation = false, Exclude = true, ApplyToMembers = false)]
  public class Participant {

    #region Properties

    internal int ModCount {
      get { return modCount; }
    }

    private bool multiInstance = false;
    private int modCount;

    /// <summary>
    /// Gets or sets if the participant contains multiple instances.
    /// </summary>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    [DefaultValue(false)]
    public bool MultiInstance {
      get { return multiInstance; }
      set {
        if (multiInstance != value) {
          modCount++;
          multiInstance = value;
        }
      }
    }

    #endregion

    internal double GetSize() {
      return MultiInstance ? 32: 20;
    }

    /// <inheritdoc/>
    [Obfuscation(StripAfterObfuscation = false, Exclude = true)]
    public Participant Clone() {
      return new Participant {MultiInstance = MultiInstance};
    }
  }
}
