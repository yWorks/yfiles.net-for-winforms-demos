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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Demo.yFiles.Option.Constraint;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using Demo.yFiles.Option.View;
using Demo.yFiles.Option.DataBinding.UI;
using System.Diagnostics;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Abstract base class for factories that can create visual representations of OptionHandlers
  /// </summary>
  /// <remarks>This class can either create a complete standalone editor form together with the
  /// necessary buttons for applying/adopting/canceling, or just the bare core
  /// <see cref="EditorControl"/> for the OptionHandler. An instance of <see cref="IModelView"/>
  /// is created implicitly to control the synchronization and optionally ensure
  /// that the <see cref="ConstraintManager"/> is correctly registered with the editor.</remarks>
  /// <example> This sample shows how create a standalone editor form for a simple option handler
  /// that is automatic synchronization state.
  /// <code lang="C#">
  ///   class ConstraintDemo 
  ///   {
  ///      public static int Main() 
  ///      {
  ///         oh = new OptionHandler("Settings);
  ///         //add various simple items directly to the handler            
  ///         IOptionItem stringItem = oh.AddString(null, "String", "foo");
  ///         oh.AddInt(null, "Int", 3);
  ///         
  ///         //this will show a PropertyGrid like editor
  ///         //synchronization properties can be set
  ///         EditorForm form = (new TableEditorFactory()).CreateEditor(oh, true, true);
  ///         //use the editor as modal dialog
  ///         if(form.DialogResult == DialogResult.Cancel) {
  ///            MessageBox.Show("Dialog has been canceled");
  ///         }
  ///         if(form.DialogResult == DialogResult.OK) {
  ///            MessageBox.Show("Dialog has been accepted");
  ///         }  
  ///       }
  ///    }
  /// </code>
  /// </example>
  public abstract class EditorFactory
  {
    /// <summary>
    /// Create a complete editor that allows to edit values in an OptionHandler, together with buttons
    /// for applying and adopting values and canceling the editor.
    /// </summary>
    /// <remarks>The actual buttons present depend on the state of the <paramref name="autoAdopt"/>
    /// and <paramref name="autoCommit"/> parameters, i.e. the Apply and Adopt buttons are
    /// only shown if the respective parameter is <see langword="false"/>.</remarks>
    /// <param name="oh">The OptionHandler for which to create an editor.</param>
    /// <param name="autoAdopt">Whether the Editor should be in autoAdopt state. If <see langword="false"/>,
    /// an Adopt button is shown on the form, and values that are changed externally must be explicitly
    /// adopted from the underlying OptionHandler <paramref name="oh"/>.</param>
    /// <param name="autoCommit">Whether the Editor should be in autoCommit state. If <see langword="false"/>,
    /// an Apply button is shown on the form, and values that are changed in the editor must be explicitly
    /// commited to the underlying OptionHandler <paramref name="oh"/>.</param>
    /// <returns>A new EditorForm</returns>
    public virtual EditorForm CreateEditor(OptionHandler oh, bool autoAdopt, bool autoCommit) {
      EditorForm form = new EditorForm(CreateControl(oh, autoAdopt, autoCommit));
      ConfigureForm(form);
      return form;
    }

    /// <summary>
    /// Create a complete editor that allows to edit values in an OptionHandler, together with buttons
    /// for applying and adopting values and canceling the editor.
    /// </summary>
    /// <remarks>The actual buttons present depend on the state of the <paramref name="autoAdopt"/>
    /// and <paramref name="autoCommit"/> parameters, i.e. the Apply and Adopt buttons are
    /// only shown if the respective parameter is <see langword="false"/>.</remarks>
    /// <param name="oh">The OptionHandler for which to create an editor.</param>
    /// <param name="autoAdopt">Whether the Editor should be in autoAdopt state. If <see langword="false"/>,
    /// an Adopt button is shown on the form, and values that are changed externally must be explicitly
    /// adopted from the underlying OptionHandler <paramref name="oh"/>.</param>
    /// <param name="autoCommit">Whether the Editor should be in autoCommit state. If <see langword="false"/>,
    /// an Apply button is shown on the form, and values that are changed in the editor must be explicitly
    /// commited to the underlying OptionHandler <paramref name="oh"/>.</param>
    /// <param name="showResetButton">Whether the "reset" button should be visible. Default is true</param>
    /// <returns>A new EditorForm</returns>
    public virtual EditorForm CreateEditor(OptionHandler oh, bool autoAdopt, bool autoCommit, bool showResetButton) {
      EditorForm form = new EditorForm(CreateControl(oh, autoAdopt, autoCommit), showResetButton);
      ConfigureForm(form);
      return form;
    }

    /// <summary>
    /// Create the core control for a visual representation of an OptionHandler.
    /// </summary>
    /// <remarks>This differs from <see cref="CreateEditor(OptionHandler,bool,bool)"/> in that only the core <see cref="EditorControl"/>
    /// for an OptionHandler is shown, which can embedded as any
    /// <see cref="UserControl"/>. It is the users responsibility to provide methods for
    /// explicit synchronization if the EditorControl is not in automatic synchronization mode.</remarks>
    /// <param name="oh">The OptionHandler for which to create an EditorControl.</param>
    /// <param name="autoAdopt">Sets the resulting EditorControl's 
    /// <see cref="EditorControl.IsAutoAdopt"/> property.</param>
    /// <param name="autoCommit">Sets the resulting EditorControl's 
    /// <see cref="EditorControl.IsAutoCommit"/> property.</param>
    /// <returns>An embeddable <see cref="EditorControl"/> for the OptionHandler <paramref name="oh"/>.</returns>
    public abstract EditorControl CreateControl(OptionHandler oh, bool autoAdopt, bool autoCommit);

    internal virtual TypeConverter CreateConverter(PropertyModelView.OptionItemPropertyDescriptor desc,
                                                   I18NFactory i18NFactory, string context) {
      OptionItem item = desc.Item;
      TypeConverterAttribute converterAttr =
        TypeDescriptor.GetProperties(item, false)["Value"].Attributes[typeof (TypeConverterAttribute)]
        as TypeConverterAttribute;

      TypeConverter coreConverter;
      if (desc is PropertyModelView.ListPropertyDescriptor && converterAttr.ConverterTypeName == "") {
        //special handling for the list converter
        coreConverter =
          new I18NTypeConverter(((PropertyModelView.ListPropertyDescriptor) desc).GetStringRepresentation(),
                                (bool)
                                item.GetAttribute(
                                  CollectionOptionItem<object>.USE_ONLY_DOMAIN_ATTRIBUTE));
      } else if (item is ICollectionSupport && converterAttr.ConverterTypeName == "") {
        //special handling for the list converter
        coreConverter = new ListTypeConverter(((ICollectionSupport) item).Domain,
                                              ((ICollectionSupport) item).EntryType,
                                              (bool)
                                              item.GetAttribute(
                                                CollectionOptionItem<object>.USE_ONLY_DOMAIN_ATTRIBUTE),
                                              i18NFactory, desc.I18nKey, context);
      } else if (converterAttr.ConverterTypeName != "") {
        //attribute set, get the converter that is set by the attribute
        coreConverter = TypeDescriptor.GetProperties(item, false)["Value"].Converter;
      } else {
        //no attribute, delegate to base
        coreConverter = TypeDescriptor.GetConverter(item.Type);
      }

      //TypeConverter coreConverter = converterAttr.ConverterTypeName != "" ? itemConverter : base.Converter;
      //todo: make this dependent on item attribute
      bool supportNull = (bool) item.GetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE);
      bool supportUndefined = (bool) item.GetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE);
      string nullValueRepresentation = item.GetAttribute(OptionItem.NULL_VALUE_STRING_ATTRIBUTE) as string;
      if (i18NFactory != null) {
        nullValueRepresentation = PropertyModelView.GetLocalizedString(
          i18NFactory, context, desc.I18nKey + ".VALUE.", nullValueRepresentation);
      }
      //test for overrides
      object converterOverride = item.GetAttribute(OptionItem.CUSTOM_CONVERTER_ATTRIBUTE);
      if (converterOverride != null) {
        if (converterOverride is TypeConverter) {
          return (TypeConverter) converterOverride;
        }
        if (converterOverride is Type) {
          try {
            TypeConverter o = System.Activator.CreateInstance((Type) converterOverride) as TypeConverter;
            if (o != null) {
              coreConverter = o;
            }
          } catch {
            Trace.WriteLine("Cannot create converter instance");
          }
        } else if (converterOverride is string) {
          try {
            TypeConverter o =
              System.Activator.CreateInstance(Type.GetType((string) converterOverride)) as TypeConverter;
            if (o != null) {
              coreConverter = o;
            }
          } catch {
            Trace.WriteLine("Cannot create converter instance");
          }
        }
        else  {
          Trace.WriteLine("Invalid type for converter attribute");
        }
      }
      if (supportNull || supportUndefined) {
        //custom conversions needed...
        if (coreConverter is ExpandableObjectConverter
            || coreConverter is FontConverter
            || coreConverter is PointConverter
          ) {
          return new ExpandableNullableTypeConverter(coreConverter,
                                                     supportNull,
                                                     supportUndefined,
                                                     nullValueRepresentation == null
                                                       ? ""
                                                       : nullValueRepresentation);
        }                   
        else {
          return new NullableTypeConverter(coreConverter,
                                           supportNull,
                                           supportUndefined,
                                           nullValueRepresentation == null ? "" : nullValueRepresentation);
        }
      }
      return coreConverter;
    }

    internal virtual object CreateUIEditor(PropertyModelView.OptionItemPropertyDescriptor desc) {
      OptionItem item = desc.Item;
//      EditorAttribute editorAttribute =
//        TypeDescriptor.GetProperties(item, false)["Value"].Attributes[typeof(EditorAttribute)]
//        as EditorAttribute;

//      UITypeEditor coreEditor;
//      if (editorAttribute.EditorTypeName != "") {
//        //attribute set, get the converter that is set by the attribute
//        coreEditor = TypeDescriptor.GetProperties(item, false)["Value"].GetEditor();
//      } else {
//        //no attribute, delegate to base
//        coreEditor = TypeDescriptor.GetConverter(item.Type);
//      }

      object editor = TypeDescriptor.GetEditor(desc.PropertyType, typeof (UITypeEditor));
      UITypeEditor _delegate = editor as UITypeEditor;

      object editorOverride = item.GetAttribute(OptionItem.CUSTOM_TABLEITEM_EDITOR);
      if (editorOverride != null) {
        if (editorOverride is Type) {
          try {
            UITypeEditor o = System.Activator.CreateInstance((Type)editorOverride) as UITypeEditor;
            if (o != null) {
              _delegate = o;
            }
          } catch {
            Trace.WriteLine("Cannot create UI editor instance");
          }
        } else if (editorOverride is string) {
          try {
            UITypeEditor o =
              System.Activator.CreateInstance(Type.GetType((string)editorOverride)) as UITypeEditor;
            if (o != null) {
              _delegate = o;
            }
          } catch {
            Trace.WriteLine("Cannot create UI editor instance");
          }
        }
        else if(editorOverride is UITypeEditor) {
          _delegate = (UITypeEditor) editorOverride;
        }
        else {
          Trace.WriteLine("Invalid type for UI editor attribute");
        }
      }

//      if(_delegate != null && _delegate is EnumUITypeEditor) {
//        IItemRenderer renderer = GetRenderer(item);
//        if(renderer != null) {
//          ((EnumUITypeEditor) _delegate).Renderer = renderer;
//        }
//      }

      bool supportNull = (bool) desc.Item.GetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE);
      bool supportUndef = (bool) desc.Item.GetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE);
      if (supportNull) {
        return _delegate == null
                 ? (supportUndef ? new UndefinedValueUITypeEditor() : editor)
                 :
               new NullableUITypeEditor(_delegate);
      } else {
        return supportUndef ? new UndefinedValueUITypeEditor(_delegate) : _delegate;
      }
    }

//    private static IItemRenderer GetRenderer(OptionItem item) {
//      object rendererOverride = item.GetAttribute(OptionItem.CUSTOM_CELLRENDERER);
//      IItemRenderer renderer = rendererOverride as IItemRenderer;
//      if (renderer == null && rendererOverride != null) {
//        if (rendererOverride is Type) {
//          try {
//            IItemRenderer o = System.Activator.CreateInstance((Type)rendererOverride) as IItemRenderer;
//            if (o != null) {
//              renderer = o;
//            }
//          } catch {
//            Trace.WriteLine("Cannot create Item Renderer instance");
//          }
//        } else if (rendererOverride is string) {
//          try {
//            IItemRenderer o =
//              System.Activator.CreateInstance(Type.GetType((string)rendererOverride)) as IItemRenderer;
//            if (o != null) {
//              renderer = o;
//            }
//          } catch {
//            Trace.WriteLine("Cannot create Item Renderer instance");
//          }
//        } else {
//          Trace.WriteLine("Invalid type for Item Renderer attribute");
//        }
//      }
//      return renderer;
//    }

    /// <summary>
    /// Callback method to configure an editor form
    /// </summary>
    /// <remarks>This methods sets the following properties:
    /// <code>
    /// f.FormBorderStyle = FormBorderStyle.FixedDialog;
    ///  f.ShowIcon = false;
    ///   f.ShowInTaskbar = false;
    ///  f.MaximizeBox = false;
    ///  f.MinimizeBox = false;
    /// </code></remarks>
    /// <param name="f"></param>
    protected virtual void ConfigureForm(Form f) {
      f.FormBorderStyle = FormBorderStyle.FixedDialog;
      f.ShowIcon = false;
      f.ShowInTaskbar = false;
      f.MaximizeBox = false;
      f.MinimizeBox = false;
    }
  }
}