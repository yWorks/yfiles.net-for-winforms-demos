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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{
  internal partial class DialogSectionControl : UserControl, IStatusChangeSupport
  {
    #region private members

    private BindingList<PropertyModelView> _registeredViews = new BindingList<PropertyModelView>();
    private BindingList<DescriptorProxy> _registeredProxies = new BindingList<DescriptorProxy>();
    private IDictionary<Guid, Control> propertyControlMapping = new Dictionary<Guid, Control>();
    private PropertyDescriptorCollection descriptors;
    private IList<Control> inputControls = new List<Control>();

    private int minWidth = 0;
    private int minHeight = 0;
    private ItemFactory itemFactory;
    private bool drawBox;
    private bool nested = false;
    private object bindingSource;
    private IStatusChangeSupport parent;
    private GroupBox enclosure;
    private TableLayoutPanel clientControl;

    #endregion

    public DialogSectionControl(DialogEditorControl parent, PropertyDescriptorCollection descriptors,
                                bool drawBox,
                                string title) {
      this.descriptors = descriptors;
      this.drawBox = drawBox;
      this.title = title;
      RegisterView(parent.View);
      bindingSource = _registeredViews;
      this.parent = parent;
      parent.StatusChanged += view_StatusChanged;
      itemFactory = new ItemFactory(this);
      InitializeComponent();
      ConfigureFirstColumnWidth();
      SetChildWidth(minWidth);
//      if(clientControl != null) {
//        clientControl.Dock = DockStyle.Fill;
//      }
//      
//      if(enclosure != null) {
//        enclosure.Dock = DockStyle.Fill;
//      }
//      this.PerformLayout();
                                }

    public DialogSectionControl(DialogSectionControl parent,
                                PropertyDescriptorCollection descriptors, PropertyDescriptor parentDesc, bool drawBox) {
      nested = true;
      this.descriptors = descriptors;
      this.drawBox = drawBox;
      this.title = parentDesc.DisplayName;
      _registeredProxies.Add(new DescriptorProxy(descriptors));
      bindingSource = _registeredProxies;
      this.parent = parent;
      parent.StatusChanged += view_StatusChanged;
      itemFactory = new ItemFactory(this);
      InitializeComponent();
//      if (clientControl != null) {
//        clientControl.Dock = DockStyle.Fill;
//      }
//
//      if (enclosure != null) {
//        enclosure.Dock = DockStyle.Fill;
//      }
//      this.PerformLayout();
                                }

    public DialogSectionControl(DialogEditorControl parent,
                                PropertyDescriptorCollection descriptors, PropertyDescriptor parentDesc, bool drawBox) {
      this.descriptors = descriptors;
      this.drawBox = drawBox;
      this.title = parentDesc.DisplayName;
      _registeredProxies.Add(new DescriptorProxy(descriptors));
      bindingSource = _registeredProxies;
      this.parent = parent;
      parent.StatusChanged += view_StatusChanged;
      itemFactory = new ItemFactory(this);
      InitializeComponent();
      ConfigureFirstColumnWidth();
      SetChildWidth(minWidth);
      this.minWidth = this.Width = minWidth + 6;
//      if (clientControl != null) {
//        clientControl.Dock = DockStyle.Fill;
//      }
//
//      if (enclosure != null) {
//        enclosure.Dock = DockStyle.Fill;
//      }
//      this.PerformLayout();
    }

    private void ConfigureFirstColumnWidth() {
      this.SuspendLayout();
      float colWidth = 0;     
      colWidth = GetFirstColumnWidth();
      clientControl.AutoSize = false;
      SetFirstColumnWidth(colWidth, 0);
      this.ResumeLayout();
    }

    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private string title;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      foreach (Control control in inputControls) {
        control.DataBindings.Clear();
      }
      parent.StatusChanged -= view_StatusChanged;
      parent = null;
      propertyControlMapping.Clear();
      if (_registeredViews.Count > 0) {
        _registeredViews.Clear();
      }
      if (_registeredProxies.Count > 0) {
        _registeredProxies.Clear();
      }
//      bindingSource.Clear();
      base.Dispose(disposing);
    }

    private void RegisterView(IModelView view) {
      PropertyModelView propertyModelView = view as PropertyModelView;
      if (propertyModelView != null) {
        _registeredViews.Add(propertyModelView);
      }
    }

    private void view_StatusChanged(object source, ItemStatusEventArgs statusInformation) {
      if (!statusInformation.IndirectChange) {
        if (statusInformation.EnabledChanged) {
          if (propertyControlMapping.ContainsKey((Guid) source)) {
            Control c = propertyControlMapping[(Guid) source];
            c.Enabled = statusInformation.Enabled;
          } else {
            if (StatusChanged != null) {
              //propagate to subscribers
              StatusChanged(source, statusInformation);
            }
          }
        }
        Invalidate();
      }
    }

    #region private helper methods

    private void CreateEditors() {
      //todo: create factory methods!
      //todo: enhance layout!
      SuspendLayout();

      int count = 0;
      if (!drawBox) {
        //we don't draw an enclosing groupbox
        clientControl = CreateClientPanel(this.DisplayRectangle);
        this.Controls.Add(clientControl);
      } else {
        enclosure = new GroupBox();
        enclosure.Size = new Size(0, 0);
        enclosure.AutoSize = true;
        enclosure.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        enclosure.Text = title;
        this.Controls.Add(enclosure);
        enclosure.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        enclosure.Dock = DockStyle.Fill;
        clientControl = CreateClientPanel(enclosure.DisplayRectangle);
        enclosure.Controls.Add(clientControl);
      }

    //  ColumnStyle style = new ColumnStyle(SizeType.Absolute);
      //style.Width = 100;
      //if(enclosure != null) {
      //  enclosure.Width += (int)(width - oldwidth);
      //}
      //clientControl.ColumnStyles.Add(style);
      //ColumnStyle style2 = new ColumnStyle(SizeType.Percent);
      //style2.Width = 200;
      //clientControl.ColumnStyles.Add(style2);

      foreach (PropertyModelView.OptionItemPropertyDescriptor prop in descriptors) {
        AddControlToClientPane(clientControl, count, prop);
        ++count;
      }
      ConfigureInitialSize();
    }
    
    private void ConfigureInitialSize() {
      //scan all controls for size of first column
      //this.SuspendLayout();
      clientControl.Size = clientControl.PreferredSize;
      minWidth = clientControl.PreferredSize.Width + clientControl.Margin.Horizontal;

      if (!drawBox) {
        foreach (int height in clientControl.GetRowHeights()) {
          minHeight += height;
        }
        minHeight += clientControl.Margin.Vertical;
      } else {
        minHeight = enclosure.GetPreferredSize(new Size()).Height;
        if (nested) {
          minHeight -= 12;
        }
        minWidth = enclosure.Width = enclosure.PreferredSize.Width;
       enclosure.Height = minHeight;
      }
      this.Height = GetPreferredSize(new Size()).Height;      
      this.Width = GetPreferredSize(new Size()).Width;
    }
    
    private void SetFirstColumnWidth(float width, int level) {
      if (clientControl != null) {
        foreach (Control control in clientControl.Controls) {
          DialogSectionControl sec = control as DialogSectionControl;
          if (sec != null) {
            sec.SetFirstColumnWidth(width, 0);
          }
        }
//        float oldwidth = clientControl.GetColumnWidths()[0];
        ColumnStyle style = new ColumnStyle(SizeType.Absolute);
        style.Width = width - level;
        //if(enclosure != null) {
        //  enclosure.Width += (int)(width - oldwidth);
        //}
        clientControl.ColumnStyles.Add(style);
        clientControl.Width = clientControl.PreferredSize.Width;
        if (enclosure != null) {
          enclosure.Width = enclosure.PreferredSize.Width;
          minWidth = this.Width = enclosure.Width;
        }
        else {
          minWidth = this.Width = clientControl.Width;
        }
        ColumnStyle style2 = new ColumnStyle(SizeType.Percent);
        style2.Width = 100;
//        clientControl.ColumnStyles.Add(style2);
      }
    }

    private float GetFirstColumnWidth() {
      if (clientControl != null) {
        float width = clientControl.GetColumnWidths()[0];
        foreach (Control control in clientControl.Controls) {
          DialogSectionControl sec = control as DialogSectionControl;
          if (sec != null) {
            width = Math.Max(width, sec.GetFirstColumnWidth());
          }
        }
        return width;
      }
      return 0;
    }

    internal void SetChildWidth(int width) {
      foreach (Control control in inputControls) {
        if (control is DialogSectionControl) {
          ((DialogSectionControl)control).SetChildWidth(
            this.Width - this.Margin.Horizontal);
        }
      }
      SetChildWidthCore(width);
    }

    internal void SetChildWidthCore(int width) {
      if (enclosure != null) {
        this.AutoSize = false;
        this.Width = width;
        //todo: make this work correctly
        this.minWidth = width;
        clientControl.AutoSize = false;
        clientControl.Dock = DockStyle.None;
        clientControl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        enclosure.AutoSize = false;
        clientControl.Width = width - 6;
        enclosure.Width = width;
      } else {
        this.AutoSize = false;
        this.Width = width;
        this.minWidth = width;
        clientControl.AutoSize = false;
        clientControl.Dock = DockStyle.None;
        clientControl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        clientControl.Width = width;
      }
    }


    ///<summary>
    ///Retrieves the size of a rectangular area into which a control can be fitted.
    ///</summary>
    ///
    ///<returns>
    ///An ordered pair of type <see cref="T:System.Drawing.Size"></see> representing the width and height of a rectangle.
    ///</returns>
    ///
    ///<param name="proposedSize">The custom-sized area for a control. </param><filterpriority>2</filterpriority>
    public override Size GetPreferredSize(Size proposedSize) {
      return new Size(minWidth, minHeight);
    }

    private TableLayoutPanel CreateClientPanel(Rectangle rect) {
      TableLayoutPanel panel = new TableLayoutPanel();
      panel.AutoSize = false;
      panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      //if (nested) {
      //  panel.BackColor = Color.Red;
      //} else {
      //  panel.BackColor = Color.Pink;
      //}
      //this.BackColor = Color.Yellow;
      panel.Location = new Point(rect.X, rect.Y);
      panel.Name = "panel";
      panel.ColumnCount = 2;
//      panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      return panel;
    }

    /// <summary>
    /// Add input control to client pane.
    /// </summary>
    /// <param name="clientControl"></param>
    /// <param name="count"></param>
    /// <param name="prop"></param>
    private void AddControlToClientPane(TableLayoutPanel clientControl, int count,
                                        PropertyModelView.OptionItemPropertyDescriptor prop) {
      Control inputControl;
      inputControl = itemFactory.AddItemControl(prop, clientControl, count);
      propertyControlMapping.Add(prop.ID, inputControl);
//      clientControl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        }

    #endregion

    private class ItemFactory
    {
      private DialogSectionControl _enclosingInstance;

      public ItemFactory(DialogSectionControl _enclosingInstance) {
        this._enclosingInstance = _enclosingInstance;
      }

      public Control AddItemControl(PropertyModelView.OptionItemPropertyDescriptor prop,
                                    TableLayoutPanel clientControl, int count) {
        System.Windows.Forms.Binding viewBinding;
        bool span = false;
        string bindingTarget = "Value";

        Control inputControl = prop.GetAttribute(OptionItem.CUSTOM_DIALOGITEM_EDITOR) as Control;
        if (inputControl != null && inputControl is IDialogItemControl) {
          span = true;
        } else {
          Type editorType = prop.GetAttribute(OptionItem.CUSTOM_DIALOGITEM_EDITOR) as Type;
          if (editorType != null) {
            Control c = System.Activator.CreateInstance(editorType) as Control;
            if (c != null && c is IDialogItemControl) {
              inputControl = c;
              span = true;
            }
          } else {
            //now build default editors
            if (prop is PropertyModelView.OptionGroupPropertyDescriptor) {
              inputControl =
                new DialogSectionControl(_enclosingInstance, prop.GetChildProperties(), prop, true);
              span = true;
              //don't bind...
              bindingTarget = null;
            } else if (prop.Type == typeof (bool)) {
              //checkbox for bools
              inputControl = new CheckBoxWrapper();
              bool supportUndefined =
                (bool) prop.GetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE);
              ((CheckBoxWrapper) inputControl).ThreeState = supportUndefined &&
                                                            prop.GetValue(this) ==
                                                            OptionItem.VALUE_UNDEFINED;
            } else {
              inputControl = new GenericValueEditor(prop);
            }
          }
        }
        inputControl.Size = inputControl.GetPreferredSize(new Size());

        clientControl.Controls.Add(inputControl, 1, count);
        _enclosingInstance.inputControls.Add(inputControl);
        inputControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
        inputControl.Enabled = prop.Enabled;


        if (span) {
          clientControl.SetColumnSpan(inputControl, 2);
        } else {
          Label inputLabel = new Label();
          inputLabel.AutoSize = true;
          inputLabel.TextAlign = ContentAlignment.MiddleLeft;
          inputLabel.Text = prop.DisplayName + ":";

          //todo: needs serious overhaul...
          inputLabel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
          clientControl.Controls.Add(inputLabel, 0, count);
        }
        if (bindingTarget != null) {
          viewBinding =
            new System.Windows.Forms.Binding(bindingTarget,
                                             _enclosingInstance.bindingSource, prop.Name, true,
                                             DataSourceUpdateMode.OnPropertyChanged);

          inputControl.DataBindings.Add(viewBinding);
        }
        return inputControl;
                                    }
    }

    public event ItemStatusHandler StatusChanged;
  }

  internal class DescriptorProxy : ICustomTypeDescriptor, INotifyPropertyChanged
  {
    //this Collection caches all property descriptors
    private PropertyDescriptorCollection _descriptorCache;

    #region constructors

    /// <summary>
    /// Create new view that is associated to the given OptionHandler
    /// </summary>
    public DescriptorProxy(PropertyDescriptorCollection descriptors) {
      //no GC problem here
      this._descriptorCache = descriptors;
      foreach (PropertyDescriptor entry in descriptors) {
        PropertyModelView.OptionItemPropertyDescriptor desc =
          entry as PropertyModelView.OptionItemPropertyDescriptor;
        if (desc != null) {
          desc.PropertyChanged += OnPropertyChanged;
        }
      }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (PropertyChanged != null) {
        PropertyChanged(this, e);
      }
    }

    #endregion

    #region ICustomTypeDescriptor Members

    public PropertyDescriptorCollection GetProperties() {
      return GetProperties(null);
    }

    public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
      if (attributes == null) {
        return _descriptorCache;
      }
      List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
      foreach (PropertyDescriptor prop in _descriptorCache) {
        //filter attributes
        foreach (Attribute attribute in attributes) {
          if (prop.Attributes.Contains(attribute)) {
            properties.Add(prop);
            break;
          }
        }
      }
      return new PropertyDescriptorCollection(properties.ToArray());
    }

    public PropertyDescriptor GetDefaultProperty() {
      return (_descriptorCache == null || _descriptorCache.Count == 0) ? null : _descriptorCache[0];
    }

    public object GetEditor(Type editorBaseType) {
      return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    public EventDescriptorCollection GetEvents(Attribute[] attributes) {
      EventDescriptorCollection retval = TypeDescriptor.GetEvents(this, attributes, true);
      return retval;
    }

    public EventDescriptorCollection GetEvents() {
      return TypeDescriptor.GetEvents(this, true);
    }

    public TypeConverter GetConverter() {
      return TypeDescriptor.GetConverter(this, true);
    }

    public object GetPropertyOwner(PropertyDescriptor pd) {
      return this;
    }

    public AttributeCollection GetAttributes() {
      return TypeDescriptor.GetAttributes(this, true);
    }

    public string GetComponentName() {
      return TypeDescriptor.GetComponentName(this, true);
    }

    public EventDescriptor GetDefaultEvent() {
      return TypeDescriptor.GetDefaultEvent(this, true);
    }

    public string GetClassName() {
      return TypeDescriptor.GetClassName(this, true);
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}