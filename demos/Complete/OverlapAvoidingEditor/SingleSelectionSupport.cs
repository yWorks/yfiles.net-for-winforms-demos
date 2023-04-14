/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.5.
 ** Copyright (c) 2000-2023 by yWorks GmbH, Vor dem Kreuzberg 28,
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

using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Graph;

namespace Demo.yFiles.Graph.OverlapAvoidingEditor
{
  /// <summary>
  /// Enables single selection mode for interaction.
  /// </summary>
  /// <remarks>
  /// All default gestures that result in more than one item selected at a time are
  /// either switched off or changed so only one item gets selected.
  /// </remarks>
  public class SingleSelectionSupport
  {
    private readonly GraphEditorInputMode mode;
    private readonly GraphControl graphControl;

    /// <summary>
    /// Initializes a SingleSelectionSupport instance. 
    /// </summary>
    /// <param name="mode">The <see cref="GraphEditorInputMode">input mode</see> whose selection behavior is to be changed.</param>
    public SingleSelectionSupport(GraphEditorInputMode mode) {
      this.mode = mode;
      this.graphControl = mode.GraphControl;
    }

    /// <summary>
    /// Enables single selection mode. 
    /// </summary>
    public void Enable() {
      // disable marquee selection
      mode.MarqueeSelectionInputMode.Enabled = false;
      // disable multi selection with Ctrl-Click
      mode.MultiSelectionRecognizer = EventRecognizers.Never;

      // deactivate commands that can lead to multi selection
      mode.AvailableCommands.Remove(Commands.ToggleItemSelection);
      mode.AvailableCommands.Remove(Commands.SelectAll);

      mode.NavigationInputMode.AvailableCommands.Remove(Commands.ExtendSelectionLeft);
      mode.NavigationInputMode.AvailableCommands.Remove(Commands.ExtendSelectionUp);
      mode.NavigationInputMode.AvailableCommands.Remove(Commands.ExtendSelectionDown);
      mode.NavigationInputMode.AvailableCommands.Remove(Commands.ExtendSelectionRight);

      // add custom binding for toggle item selection
      mode.KeyboardInputMode.AddCommandBinding(Commands.ToggleItemSelection, ToggleItemSelectionExecuted, ToggleItemSelectionCanExecute);

      //Disable selection of (possibly multiple) items
      mode.PasteSelectableItems = GraphItemTypes.None;

      //Also clear the selection - even though the setup works when more than one item is selected, it looks a bit strange
      graphControl.Selection.Clear();
    }

    #region custom toggle item selection behavior

    private void ToggleItemSelectionCanExecute(object sender, CanExecuteCommandEventArgs e) {
      // if we have an item, the command can be executed
      var modelItem = (e.Parameter as IModelItem) ?? graphControl.CurrentItem;
      e.CanExecute = modelItem != null;
      e.Handled = true;
    }

    /// <summary>
    /// Custom command handler that allows toggling the selection state of an item 
    /// respecting the single selection policy.
    /// </summary>
    private void ToggleItemSelectionExecuted(object sender, ExecutedCommandEventArgs e) {
      // get the item
      var modelItem = (e.Parameter as IModelItem) ?? graphControl.CurrentItem;
      var inputMode = (GraphEditorInputMode) graphControl.InputMode;

      // check if it allowed to be selected
      if (modelItem != null && graphControl.Graph.Contains(modelItem) && inputMode.SelectableItems.Is(modelItem)) {
        var isSelected = inputMode.GraphSelection.IsSelected(modelItem);
        if (isSelected) {
          // the item is selected and needs to be unselected - just clear the selection
          inputMode.GraphSelection.Clear();
        } else {
          // the items is unselected - unselect all other items and select the currentItem
          inputMode.GraphSelection.Clear();
          inputMode.SetSelected(modelItem, true);
        }
        e.Handled = true;
      }
    }

    #endregion

  }
}
