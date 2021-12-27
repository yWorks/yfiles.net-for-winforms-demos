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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Create a <see cref="PropertyGrid"/> representation of an <see cref="OptionHandler"/> instance.
  /// </summary>
  /// <remarks>Each <see cref="IOptionItem"/> in the handler is normally
  /// represented as a propery in the grid. Nested <see cref="OptionGroup"/>s are shown
  /// as nested properties which can be collapsed/expanded. The grid
  /// uses the usual <see cref="UITypeEditor"/> and <see cref="TypeConverter"/> mechanism
  /// for the editors, which can also be set at runtime with <see cref="OptionItem.CUSTOM_CONVERTER_ATTRIBUTE"/>
  /// and <see cref="OptionItem.CUSTOM_TABLEITEM_EDITOR"/> attributes on the item level.
  /// If <see cref="OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE"/> is set to <see langword="true"/>,
  /// an automatic wrapper is created for the item editor and/or the TypeConverter that allows
  /// to explicitly state that "No value" should be set for this item (may differ from setting a <see langword="null"/>
  /// value for the item, which is usually not supported by the core converters and editors.)
  /// If <see cref="OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE"/> is set to <see langword="true"/>,
  /// there is also automatic support for displaying so-called <c>undefined</c> values,
  /// which may result from conflicting source values if the editor tries to show
  /// values from multiple instances at once. Undefined values can't be entered in the editor, and
  /// editing an item will clear the <c>undefined</c> state for the item, however
  /// canceling the edit will leave the <c>undefined</c> state intact.</remarks>
  public class TableEditorFactory : EditorFactory
  {
    /// <summary>
    /// Attribute key for display-related settings that can be used on an <see cref="IOptionItem"/> as
    /// attribute key for <see cref="IOptionItem.SetAttribute"/>.
    /// </summary>
    /// <remarks>This key is used to change the appearance of a table view created with this factory.
    /// The framework expects to find bitwise combinations from values in <see cref="RenderingHints"/>
    /// there, other values are silently ignored.</remarks>
    public const string RENDERING_HINTS_ATTRIBUTE = "TableEditorFactory.RENDERING_HINTS_ATTRIBUTE";

    private bool toolbarVisible = true;

    /// <summary>
    /// Gets or sets whether the embedded <see cref="PropertyGrid"/> should show its standard toolbar.
    /// </summary>
    /// <value>Default value is <see langword="true"/>.</value>
    public bool ToolbarVisible {
      get { return toolbarVisible; }
      set { toolbarVisible = value; }
    }

    /// <inheritdoc/>
    public override EditorControl CreateControl(OptionHandler oh, bool autoAdopt, bool autoCommit) {
      PropertyModelView view = new PropertyModelView(oh, this);
      view.IsAutoAdopt = autoAdopt;
      view.IsAutoCommit = autoCommit;
      PropertyGridEditorControl clientControl = new PropertyGridEditorControl(view);
      clientControl.ToolbarVisible = ToolbarVisible;
      return clientControl;
    }

    /// <summary>
    /// Defines hints that can be used to fine-tune the appearance of an item in the table view.
    /// </summary>
    /// <remarks>This is meant to be used in conjunction with <see cref="IOptionItem.SetAttribute"/>
    /// for the key <see cref="TableEditorFactory.RENDERING_HINTS_ATTRIBUTE"/>.</remarks>
    [Flags]
    public enum RenderingHints
    {
      /// <summary>
      /// Specifies that no hints should be applied to the item.
      /// </summary>
      None = 0,
      /// <summary>
      /// If set, the item is not displayed. If the item is a group, its children
      /// are moved to the group's parent.
      /// </summary>
      Invisible = 1,
      /// <summary>
      /// If set, this section is shown collapsed when the table view is built.
      /// </summary>
      Collapsed = 2
    }
    
  }
}