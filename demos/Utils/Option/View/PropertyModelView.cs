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
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Xml;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;

namespace Demo.yFiles.Option.View
{
  /// <summary>
  /// View implementation that presents a property-like view onto an OptionHandler instance
  /// </summary>
  /// <remarks>This is intended mainly for a PropertyGrid. Each OptionItem is represented as a property
  /// of this view, where:
  /// <list type="number">
  /// <item>The toplevel group of the item maps to the category attribute of the property</item>
  /// <item>The name of the item maps to the DisplayName of the property</item>
  /// </list>
  /// </remarks>
  internal sealed class PropertyModelView : ICustomTypeDescriptor, IModelView, IDisposable
  {
    internal static string GetLocalizedString(I18NFactory i18NFactory, string context, string i18nKeyString, string name) {
      if (i18NFactory != OptionHandler.FallBackI18NFactory) {
        string s = i18NFactory.GetString(
          context, i18nKeyString);
        return s == i18nKeyString ? name : s;
      }
      return name;
    }

    #region private fields

    private OptionHandler _handler;

    public bool UseTableRenderingHints {
      get { return useTableRenderingHints; }
      set { useTableRenderingHints = value; }
    }

    public bool UseDefaultRenderingHints {
      get { return useDefaultRenderingHints; }
      set { useDefaultRenderingHints = value; }
    }

    private bool useTableRenderingHints;
    private bool useDefaultRenderingHints;

    private bool _autoAdopt;
    private bool _autoCommit;

    private EditorFactory editorFactory;
    private readonly IList<Guid> controls = new List<Guid>();

    //this Collection caches all property descriptors
    private PropertyDescriptorCollection _descriptorCache;

    private readonly IDictionary<Guid, OptionItemPropertyDescriptor> _descriptorMap =
      new Dictionary<Guid, OptionItemPropertyDescriptor>();

    private OptionGroupPropertyDescriptor handlerDesc;
    private bool keepToplevel;
    private bool inCommit;
    private bool inAdopt;

    internal PropertyDescriptorCollection DescriptorCache {
      get {
        if (_descriptorCache == null) {
          CreateDescriptorCache();
        }
        return _descriptorCache;
      }
    }

    #endregion

    #region public properties

    /// <inheritdoc/>
    public bool IsAutoAdopt {
      get { return _autoAdopt; }
      set { _autoAdopt = value; }
    }

    /// <inheritdoc/>
    public bool IsAutoCommit {
      get { return _autoCommit; }
      set { _autoCommit = value; }
    }

    /// <inheritdoc/>
    public INotifyPropertyChanged GetViewItem(INotifyPropertyChanged item) {
      OptionItemPropertyDescriptor retval = null;
      //todo: use indirect lookup
      IOptionItem optionItem = item as IOptionItem;
      if (optionItem != null) {
        _descriptorMap.TryGetValue(optionItem.ID, out retval);
      }
      
      return retval;
    }


    private I18NFactory I18NFactory {
      get { return _handler.I18nFactory; }
    }

    #endregion

    #region constructors

    public PropertyModelView(OptionHandler handler, EditorFactory editorFactory) : this(handler, editorFactory, false) {}

    public PropertyModelView(OptionHandler handler, EditorFactory editorFactory, bool keepToplevel) {
      //no GC problem here
      this.keepToplevel = keepToplevel;
      _handler = handler;
      _handler.ContentChanged += _handler_ContentChanged;
      this.editorFactory = editorFactory;
      //propagate changes to registered editors
      //descriptors do the listening stuff themselves now
      CreateDescriptorCache();
      _handler.AddView(this);
    }

    #endregion

    #region ICustomTypeDescriptor Members

    public PropertyDescriptorCollection GetProperties() {
      return GetProperties(null);
    }

    public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
      if (attributes == null) {
        return DescriptorCache;
      }
      List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
      foreach (PropertyDescriptor prop in DescriptorCache) {
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

    public PropertyDescriptorCollection GetPropertiesForCategory(string category) {
      List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
      foreach (PropertyDescriptor prop in DescriptorCache) {
        if (prop.Category == category) {
          properties.Add(prop);
        }
      }
      return new PropertyDescriptorCollection(properties.ToArray());
    }

    public PropertyDescriptor GetDefaultProperty() {
      return (DescriptorCache == null || DescriptorCache.Count == 0) ? null : DescriptorCache[0];
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

    #region IModelView Members

    public void AdoptValues() {
      if(inCommit || inAdopt) {
        return;
      }
      inAdopt = true;
      //force value adopt on each descriptor
      foreach (OptionItemPropertyDescriptor desc in DescriptorCache) {
        desc.AdoptValue();
      }
      inAdopt = false;
    }

    public void CommitValues() {
      if(inCommit || inAdopt) {
        return;
      }
      inCommit = true;
      foreach (OptionItemPropertyDescriptor desc in DescriptorCache) {
        desc.CommitValue();
      }
      inCommit = false;
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;
    public event ItemStatusHandler StatusChanged;

    #endregion

    #region listeners

    private void OnPropertyChanged(string propertyName) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    internal void OnStatusChanged(Guid id, bool status, bool hidden, bool enabledChanged, bool hiddenChanged,
                                  bool dependent) {
      if (StatusChanged != null) {
        StatusChanged(id, new ItemStatusEventArgs(status, hidden, enabledChanged, hiddenChanged, dependent));
      }
                                  }

    #endregion

    #region internal helpers

    public bool IsUpdating {
      get { return isUpdating; }
    }

    private bool isUpdating;

    private void _handler_ContentChanged(object source, StructureChangeEventArgs e) {
      if (PreContentChange != null) {
        //just forward the event to all views
        PreContentChange(this, e);
      }
      isUpdating = true;
      DisposeDescriptors();
      CreateDescriptorCache();
      isUpdating = false;
      if (PostContentChange != null) {
        //just forward the event to all views
        PostContentChange(this, e);
      }
    }

    //delegate most stuff to _origin
    internal int SectionCount {
      get { return _handler.Groups.Count; }
    }

    public OptionHandler Handler {
      get { return _handler; }
    }

    public bool KeepToplevel {
      set { keepToplevel = value; }
      get { return keepToplevel; }
    }

    //TODO: trigger this method in response to structure changes
    private void CreateDescriptorCache() {
      List<OptionItemPropertyDescriptor> properties =
        new List<OptionItemPropertyDescriptor>();
      int maxIndex = SectionCount + 1;
      handlerDesc = new OptionGroupPropertyDescriptor(this, _handler, null);
      handlerDesc.SortOrder = maxIndex;
      --maxIndex;
      foreach (OptionItemPropertyDescriptor e in handlerDesc.GetChildProperties()) {
        //TODO: Sort this, so that main settings always come first (if any exist)
        //we want this option group to be a toplevel group
        if (e is OptionGroupPropertyDescriptor && !KeepToplevel) {
          //this is needed so that the first nesting is shown correctly in the grid
          //(actually, for the grid, these are toplevel items!)
          foreach (OptionItemPropertyDescriptor item in
            e.GetChildProperties()) {
            properties.Add(item);
          }
          e.SortOrder = maxIndex;
          --maxIndex;
        } else {
          properties.Add(e);
        }
      }
      properties.Sort(CompareCategories);
      _descriptorCache = new PropertyDescriptorCollection(properties.ToArray());
    }


    internal static int CompareCategories(OptionItemPropertyDescriptor desc1, OptionItemPropertyDescriptor desc2) {
      if (desc2.Owner != null && desc1.Owner != null) {
        int order =
          CompareCategories(desc1.Owner as OptionItemPropertyDescriptor, desc2.Owner as OptionItemPropertyDescriptor);
        if (order != 0) {
          return order;
        }
      }
      if (desc1.SortOrder > desc2.SortOrder) {
        return -1;
      }
      return desc1.SortOrder < desc2.SortOrder ? 1 : 0;
    }

    public void AddEditorControl(EditorControl control) {
      controls.Add(control.ID);
    }

    public void RemoveEditorControl(EditorControl control) {
      controls.Remove(control.ID);
      if (controls.Count == 0) {
        //we remove ourselves if no control exists anymore
        Dispose();
      }
    }

    #endregion

    #region IDisposable

    public void Dispose() {
      //            Console.WriteLine("Disposing View");
      //todo: notify controls of imminent death
      controls.Clear();
      handlerDesc.Dispose();
      handlerDesc = null;
      editorFactory = null;
      DescriptorCache.Clear();
      _descriptorMap.Clear();
      _handler.ContentChanged -= _handler_ContentChanged;
      _handler.RemoveView(this);
      _handler = null;
    }

    private void DisposeDescriptors() {
      //            Console.WriteLine("Disposing Descriptors");
      handlerDesc.Dispose();
      handlerDesc = null;
      DescriptorCache.Clear();
      _descriptorMap.Clear();
    }

    #endregion

    public void ResetValues() {
      handlerDesc.ResetValue(this);
    }

    #region events

    public event StructureChangedHandler PostContentChange;

    public event StructureChangedHandler PreContentChange;

    #endregion

    #region Inner class for PropertyDescriptor

    /// <summary>
    /// Helper class to create binding between option items and property view.
    /// </summary>
    /// <remarks>The purpose is to adapt an OptionItem to a .NET property of the OptionHandler,
    /// so that we can use PropertyGrid and/or .NET data binding.</remarks>
    internal class OptionItemPropertyDescriptor : PropertyDescriptor, IOptionItem, IDisposable, IOwnerSettable
    {
      protected OptionItem _item;
      protected Guid ownID = Guid.NewGuid();
      protected PropertyModelView _enclosingInstance;
      protected TypeConverter _converter;
      private object _cachedValue;
      private readonly object backupValue;
      //cached value for enabled state
      private bool enabled;

      //is this item enabled wrt to constraints
      private bool viewEnabled;
      private OptionGroupPropertyDescriptor owner;
      private object _editor;

      internal protected OptionGroupPropertyDescriptor DisplayOwner {
        get { return displayOwner; }
        set { displayOwner = value; }
      }

      private OptionGroupPropertyDescriptor displayOwner;
      
      public string I18nKey {
        get { return i18nKey; }
      }

      private readonly string i18nKey;

      public OptionItemPropertyDescriptor(PropertyModelView enclosingInstance,
                                          OptionItem item, OptionGroupPropertyDescriptor owner)
        : base((owner == null ? "" : owner.Name + "_") + item.Name, new Attribute[] {}
          ) {
        //no GC problem here, reference is in the right direction
        _item = item;
        _enclosingInstance = enclosingInstance;
        //initialize cache
        _cachedValue = MapToNullValue(_item.Value);
        backupValue = MapToNullValue(_item.Value);
        string customI18nPrefix = (string)item.GetAttribute(OptionItem.CUSTOM_I18N_PREFIX);
        if (customI18nPrefix != null) {
          i18nKey = customI18nPrefix;
        } else {
          if (owner == null) {
            i18nKey = item.Name;
          } else {
            i18nKey = owner.i18nKey + "." + item.Name;
          }
        }

        //transfer attributes specified on the Value property
        List<Attribute> attrs = new List<Attribute>(2);
        string description = (string) item.GetAttribute(OptionItem.DESCRIPTION_ATTRIBUTE);
        if (description != null) {
          attrs.Add(new DescriptionAttribute(GetLocalizedString(_enclosingInstance._handler.I18nFactory,
                                                 _enclosingInstance._handler.Name,
                                                 I18nKey, description)));
        } else {
          if (_enclosingInstance._handler.I18nFactory != OptionHandler.FallBackI18NFactory) {
            attrs.Add(new DescriptionAttribute(GetLocalizedString(_enclosingInstance._handler.I18nFactory,
                                                 _enclosingInstance._handler.Name,
                                                 I18nKey, item.Name)));
          }
        }

        base.AttributeArray = attrs.ToArray();
        enabled = item.Enabled;
        viewEnabled = enabled;
        this.owner = owner;
        _item.PropertyChanged += _item_PropertyChanged;
        _item.StatusChanged += _item_StatusChanged;
        _item.AttributeChanged += _item_AttributeChanged;
        //register ourselfes in the enclosing map
        //todo: this should probably use weak references or such, to ease GC
        _enclosingInstance._descriptorMap[_item.ID] = this;
        _editor = _enclosingInstance.editorFactory.CreateUIEditor(this);
          }

      ///<summary>
      ///Gets the name of the category to which the member belongs, as specified in the <see cref="T:System.ComponentModel.CategoryAttribute"></see>.
      ///</summary>
      ///
      ///<returns>
      ///The name of the category to which the member belongs. If there is no <see cref="T:System.ComponentModel.CategoryAttribute"></see>, the category name is set to the default category, Misc.
      ///</returns>
      public override string Category {
        get {
          StringBuilder sb = new StringBuilder();
//          if(categoryStr != null) {
//            return categoryStr;
//          }
          sb.Append('\r', DisplayOwner==null?owner.SortOrder:DisplayOwner.SortOrder);
          
          if (_enclosingInstance._handler.I18nFactory != OptionHandler.FallBackI18NFactory) {
            string key = "";
            if(DisplayOwner!=null) {
              key = DisplayOwner.I18nKey;
            }
            else if (Owner != null) {
              key = owner.I18nKey;
            }
            sb.Append(GetLocalizedString(_enclosingInstance._handler.I18nFactory,
                        _enclosingInstance._handler.Name, 
                        key, owner.DisplayName));
          } else {
            string key = "";
            if (DisplayOwner != null) {
              key = DisplayOwner.DisplayName;
            } else if (Owner != null) {
              key = owner.DisplayName;
            }
            sb.Append(key);
          }
          return sb.ToString();
        }
      }

      protected virtual void RegisterListeners() {
        //add listener to the handlers property value, to react to value changes  
        //todo: this is bad for garbage collection, either use weak references or make the
        //descriptor disposable (preferred)
        _item.PropertyChanged += _item_PropertyChanged;
        _item.StatusChanged += _item_StatusChanged;
        _item.AttributeChanged += _item_AttributeChanged;
      }

      private void _item_AttributeChanged(object source, AttributeChangedEventArgs args) {
        string key = args.Key;
        if (key == OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE ||
            key == OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE) {
          //rebuild converter and/or editor references
          _converter =
            _enclosingInstance.editorFactory.CreateConverter(this, _enclosingInstance._handler.I18nFactory,
                                                             _enclosingInstance._handler.Name);
          _editor = _enclosingInstance.editorFactory.CreateUIEditor(this);
        }
      }

      private void _item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        if (_enclosingInstance._autoAdopt) {
          //read value into cache
          AdoptValue();
        }
        //else ignore this event
      }

      protected virtual void _item_StatusChanged(object source, ItemStatusEventArgs statusInformation) {
        if (_enclosingInstance._autoAdopt) {
          if (AdoptStatus()) {
            _enclosingInstance.OnStatusChanged(ID, Enabled, false, true, false,
                                               statusInformation.IndirectChange);
          }
        }
      }

      internal virtual bool AdoptStatus() {
        bool retval = false;
        if (enabled != _item.Enabled) {
          enabled = _item.Enabled;
          retval = true;
        }

        return retval;
      }

      public virtual IOptionGroup Owner {
        get { return owner; }
      }

      IOptionGroup IOwnerSettable.Owner {
        set { owner = value as OptionGroupPropertyDescriptor; }
      }

      #region implementation of PropertyDescriptor

      public override bool CanResetValue(object component) {
        return false;
      }

      public override Type ComponentType {
        get { return null; }
      }

      public override object GetValue(object component) {
        return _cachedValue;
      }

      public override bool IsReadOnly {
        get { return !Enabled || !(owner == null ? true : owner.Enabled); }
      }

      public override Type PropertyType {
        get { return _item.Type; }
      }

      public override void ResetValue(object component) {
        if (_cachedValue != backupValue) {
          SetValue(component, backupValue);
          if (PropertyChanged != null) {
            //notify listeners on this item first
            PropertyChanged(this, new PropertyChangedEventArgs(Name));
          }
          //this will trigger refreshes on editor instances
          _enclosingInstance.OnPropertyChanged(Name);
        }
      }

      public override string DisplayName {
        get {
//          return Item.Name;
          string nameOverride = _item.GetAttribute(OptionItem.DISPLAYNAME_ATTRIBUTE) as string;
          if (nameOverride != null) {
            return _enclosingInstance._handler.I18nFactory.GetString(
              _enclosingInstance._handler.Name, nameOverride);
          } else {
            return GetLocalizedString(_enclosingInstance._handler.I18nFactory, _enclosingInstance._handler.Name, I18nKey, _item.Name);
          }
        }
      }

      public string FullyQualifiedName {
        get {
          StringBuilder sb = new StringBuilder();
          if (DisplayOwner != null) {
            sb.Append(DisplayOwner.FullyQualifiedName).Append(".");
          } else {
            sb.Append(Category).Append(".").Append(Name);
          }
          return sb.ToString();
        }
      }

      public override void SetValue(object component, object value) {
        if (_cachedValue == MapFromNullValue(null)
            || value == MapFromNullValue(null)
            //if any value is null, always set...
            || !MapFromNullValue(_cachedValue).Equals(MapFromNullValue(value))) {
          //change value in cache
          _cachedValue = MapFromNullValue(value);
          //notify listeners on this item
          if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs(Name));
          }
          if (_enclosingInstance._autoCommit) {
            //set value on handler
            _item.Value = _cachedValue;
          }
            // force editors that only listen to the view to sync on this value, 
            // regardless of adopt state
            // needed for consistent state on the same view
            // views with autoadopt don't need this, 
            // since they'll listen to the item change anyway 
            // (otherwise, they'll get the event twice)
            _enclosingInstance.OnPropertyChanged(Name);
        }
      }

      private object MapFromNullValue(object o) {
        object nvAttr = _item.GetAttribute(OptionItem.NULL_VALUE_OBJECT);
        if(o != null || nvAttr == null) {
          return o;
        }
        else {
          return nvAttr;
        }
      }

      private object MapToNullValue(object o) {
        object nvAttr = _item.GetAttribute(OptionItem.NULL_VALUE_OBJECT);
        if (nvAttr != o) {
          return o;
        } else {
          return null;
        }
      }

      public override bool ShouldSerializeValue(object component) {
        return false;
      }

      public override TypeConverter Converter {
        get {
          //lazy creation...
          if (_converter != null) {
            return _converter;
          }
          _converter =
            _enclosingInstance.editorFactory.CreateConverter(this, _enclosingInstance._handler.I18nFactory,
                                                             _enclosingInstance._handler.Name);
          return _converter;
        }
      }

      /// <summary>
      /// Getter for the underlying option item
      /// </summary>
      protected internal OptionItem Item {
        get { return _item; }
      }

      public override object GetEditor(Type editorBaseType) {
        return _editor;
      }

      #endregion

      public virtual void AdoptValue() {
        //refresh cache
        if(_enclosingInstance.inCommit ) {
          return;
        }
        
        if (AdoptStatus()) {
          _enclosingInstance.OnStatusChanged(ID, Enabled, false, true, false, false);
        }
        if (_cachedValue != MapToNullValue(Item.Value)) {
          _cachedValue = MapToNullValue(_item.Value);
          if (PropertyChanged != null) {
            //notify listeners on this item first
            PropertyChanged(this, new PropertyChangedEventArgs(Name));
          }
          //this will trigger refreshes on editor instances
          _enclosingInstance.OnPropertyChanged(Name);
        }
      }

      public virtual void CommitValue() {
        //make sure new value is stored
        if (_enclosingInstance.inAdopt) {
          return;
        }
        _item.Value = _cachedValue;
      }

      #region IOptionItem Members

      public virtual bool Enabled {
        get {
          return viewEnabled;
        }
        set {
          if (viewEnabled != value) {
            viewEnabled = value;
            _enclosingInstance.OnStatusChanged(ID, Enabled, false, true, false, false);
          }
        }
      }

#pragma warning disable 67
      public event ItemStatusHandler StatusChanged;
      public event AttributeChangedHandler AttributeChanged;
#pragma warning restore 67


      public Type Type {
        get { return PropertyType; }
      }

      public virtual object Value {
        get { return GetValue(this); }
        set { SetValue(this, value); }
      }

      /// <summary>
      /// Return the ID of the underlying option item
      /// </summary>
      public Guid ID {
        //todo: perhaps this is bad idea?
        get { return _item.ID; }
      }

      /// <summary>
      /// Get the value of an attribute
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      public object GetAttribute(string key) {
        return _item.GetAttribute(key);
      }

      /// <summary>
      /// Set the value of an attribute.
      /// </summary>
      /// <param name="key"></param>
      /// <param name="value"></param>
      public void SetAttribute(string key, object value) {
        _item.SetAttribute(key, value);
      }

      /// <summary>
      /// Return a readonly List of all attribute keys currently set for this item.
      /// </summary>
      /// <returns></returns>
      /// <remarks>This can be used to retrieve the values of all attributes</remarks>
      public IList<string> GetAttributeKeys() {
        return _item.GetAttributeKeys();
      }

      public void SetLookup(Type t, object impl) {
        throw new NotImplementedException();
      }

      public void SaveState(XmlElement parent) {        
      }

      public void RestoreState(XmlElement elem) {        
      }

      #endregion

      #region INotifyPropertyChanged Members

      public event PropertyChangedEventHandler PropertyChanged;

      #endregion

      public virtual void Dispose() {
        //remove listeners
        _item.PropertyChanged -= _item_PropertyChanged;
        _item.StatusChanged -= _item_StatusChanged;
        _item.AttributeChanged -= _item_AttributeChanged;
      }

      private int sortOrder;

      public int SortOrder {
        get { return sortOrder; }
        set { sortOrder = value; }
      }

      #region ILookup Members

      public object Lookup(Type type) {
        throw new NotImplementedException();
      }

      #endregion
    }

    #endregion

    #region specialized implementation for option groups

    internal class OptionGroupPropertyDescriptor : OptionItemPropertyDescriptor, IOptionGroup
    {
      #region private fields

      private PropertyDescriptorCollection _children;

      #endregion

//            internal PropertyDescriptorCollection Children {
//                get { return _children; }
//            }

      #region constructors

      public OptionGroupPropertyDescriptor(PropertyModelView enclosingInstance,
                                           OptionGroup item, OptionGroupPropertyDescriptor owner)
        : base(enclosingInstance, item, owner) {
        CreateChildDescriptors();
        _converter = new OptionGroupConverter(this);
        }

      #endregion

      internal override bool AdoptStatus() {
        bool retval = base.AdoptStatus();
        foreach (OptionItemPropertyDescriptor desc in _children) {
          //adopt status, but don't fire events
          if (desc.AdoptStatus()) {
            _enclosingInstance.OnStatusChanged(desc.ID, desc.Enabled, /*desc.Hidden*/false, true, false, true);
          }
        }
        return retval;
      }

      public override void Dispose() {
        foreach (OptionItemPropertyDescriptor desc in _children) {
          desc.Dispose();
        }
        _children.Clear();
        base.Dispose();
      }


      public override void ResetValue(object component) {
        foreach (OptionItemPropertyDescriptor desc in _children) {
          desc.ResetValue(component);
        }
      }

      #region overrides

      public override TypeConverter Converter {
        get { return _converter; }
      }

      public override string Category {
        get {
          StringBuilder sb = new StringBuilder();
          if (Owner == _enclosingInstance.handlerDesc && _enclosingInstance.KeepToplevel) {
            if (_enclosingInstance._handler.I18nFactory != OptionHandler.FallBackI18NFactory) {
              sb.Append(
                GetLocalizedString(_enclosingInstance._handler.I18nFactory,
                          _enclosingInstance._handler.Name, I18nKey, Name));
            } else {
              sb.Append(DisplayName);
            }
            return sb.ToString();
          }
          return base.Category;
        }
      }

      ///<summary>
      ///Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> for a given object using a specified array of attributes as a filter.
      ///</summary>
      ///
      ///<returns>
      ///A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"></see> with the properties that match the specified attributes for the specified component.
      ///</returns>
      ///
      ///<param name="filter">An array of type <see cref="T:System.Attribute"></see> to use as a filter. </param>
      ///<param name="instance">A component to get the properties for. </param>
      public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter) {
        return _children;
      }

      //needed, since explicit commit/adopt may be triggered for the group rather for the children
      public override void AdoptValue() {
        if (AdoptStatus()) {
          _enclosingInstance.OnStatusChanged(ID, Enabled, /*Hidden*/false, true, false, false);
        }
        foreach (OptionItemPropertyDescriptor desc in _children) {
          desc.AdoptValue();
        }
      }

      public override void CommitValue() {
        foreach (OptionItemPropertyDescriptor desc in _children) {
          desc.CommitValue();
        }
      }

      public override void SetValue(object component, object value) {}

      public override bool IsReadOnly {
        get { return true; }
      }

      protected override void RegisterListeners() {
        //add listener to the handlers property value, to react to value changes  
        //todo: this is bad for garbage collection, either use weak references or make the
        //descriptor disposable (preferred)
        _item.StatusChanged += _item_StatusChanged;
        _item.AttributeChanged += _item_AttributeChanged;
      }

      private static void _item_AttributeChanged(object source, AttributeChangedEventArgs args) {}

      #endregion

      #region private helpers

      private void CreateChildDescriptors() {
        List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
        //OptionGroup group = value as OptionGroup;
        int i = ((IOptionGroup) _item).Items.Count;
        foreach (OptionItem item in ((IOptionGroup) _item).Items) {
          OptionItemPropertyDescriptor desc;
          if (item is OptionGroup) {
            desc = new OptionGroupPropertyDescriptor(_enclosingInstance, (OptionGroup) item, this);
            //empty group?
            if(desc.GetChildProperties().Count == 0) {
              continue;
            }
            bool invisible = false;
            //get table rendering hint, since this needs
            if (_enclosingInstance.editorFactory is TableEditorFactory) {
              object hints = desc.GetAttribute(TableEditorFactory.RENDERING_HINTS_ATTRIBUTE);
              if (hints != null && (hints is int || hints is TableEditorFactory.RenderingHints)) {
                invisible = ((int) hints & (int) TableEditorFactory.RenderingHints.Invisible) != 0;
              }
            }
            else if (_enclosingInstance.editorFactory is DefaultEditorFactory) {
              object hints = desc.GetAttribute(DefaultEditorFactory.RENDERING_HINTS_ATTRIBUTE);
              if (hints != null && (hints is int || hints is DefaultEditorFactory.RenderingHints)) {
                invisible = ((int)hints & (int)DefaultEditorFactory.RenderingHints.Invisible) != 0;
              }
            }
            if (invisible) {
              foreach (PropertyDescriptor p in desc.GetChildProperties()) {
                properties.Add(p);
                if(_enclosingInstance.editorFactory is TableEditorFactory
                  && p is OptionItemPropertyDescriptor
                  && Owner != null && Owner.Owner == null) {
                  ((OptionItemPropertyDescriptor) p).DisplayOwner = this;
                }
                else if (_enclosingInstance.editorFactory is TableEditorFactory
                && p is OptionItemPropertyDescriptor
                && Owner == null) {
                  ((OptionItemPropertyDescriptor)p).DisplayOwner = this;
                }
              }
            } else {
              properties.Add(desc);
            }
          } else if (item is ICollectionSupport) {
            desc = new ListPropertyDescriptor(_enclosingInstance, item, this);
            properties.Add(desc);
          } else {
            desc = new OptionItemPropertyDescriptor(_enclosingInstance, item, this);
            properties.Add(desc);
          }
          ((IOwnerSettable) desc).Owner = this;
            desc.SortOrder = i--;
        }
        _children = new PropertyDescriptorCollection(properties.ToArray());
      }

      #endregion

      #region type converter for option groups

      private class OptionGroupConverter : ExpandableObjectConverter
      {
        private readonly OptionGroupPropertyDescriptor _enclosingInstance;

        public OptionGroupConverter(OptionGroupPropertyDescriptor enclosingInstance)
           {
          _enclosingInstance = enclosingInstance;
          }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType) {
          if (destinationType == typeof (String)) {
            return "";
          }
          return base.ConvertTo(context, culture, value, destinationType);
                                         }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
          return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context,
                                                                   object value, Attribute[] attributes) {
          return _enclosingInstance._children;
                                                                   }
      }

      #endregion

      public IList<IOptionItem> Items {
        get { throw new NotImplementedException(); }
      }

      public IOptionItem this[string index] {
        get { throw new NotImplementedException(); }
      }

      public int Count {
        get { throw new NotImplementedException(); }
      }

      public IOptionItem AddOptionItem(IOptionItem item) {
        throw new NotImplementedException();
      }

      public void RemoveOptionItem(IOptionItem item) {
        throw new NotImplementedException();
      }

#pragma warning disable 67
      public event StructureChangedHandler StructureChanged;
#pragma warning restore 67

      public void Clear() {
        throw new NotImplementedException();
      }

      public IOptionItem GetItemByName(string itemPath) {
        throw new NotImplementedException();
      }
    }

    #endregion

    /// <summary>
    /// Specialization for list valued option items
    /// </summary>
    /// <remarks>This class handles I18N and conversion itself, since several
    /// TypeConverter methods are bypassed by the PropertyGrid under several circumstances
    /// </remarks>
    internal class ListPropertyDescriptor : OptionItemPropertyDescriptor
    {
      private readonly IDictionary<object, string> value2representation = new Dictionary<object, string>();
      private readonly IDictionary<string, object> representation2value = new Dictionary<string, object>();
      private readonly Type entryType;

      public ListPropertyDescriptor(PropertyModelView enclosingInstance, OptionItem item,
                                    OptionGroupPropertyDescriptor owner) : base(enclosingInstance, item, owner) {
        ICollectionSupport collectionItem = item as ICollectionSupport;
        if (collectionItem != null) {
          entryType = collectionItem.EntryType;
        }
        //fill lookup maps
        TypeConverter coreConverter = TypeDescriptor.GetConverter(entryType);
        if (collectionItem != null) {
          foreach (object o in collectionItem.Domain) {
            string interpretedValue;
            if (coreConverter != null && coreConverter.CanConvertTo(typeof (string))) {
              //we need this for a  human readable description
              string s = coreConverter.
                ConvertToString(o);
              if (_enclosingInstance.I18NFactory != null
                  && !string.IsNullOrEmpty(I18nKey)) {
                interpretedValue =
                  GetLocalizedString(_enclosingInstance.I18NFactory, _enclosingInstance._handler.Name,
                                     I18nKey + ".VALUE." + s, s);
              } else {
                interpretedValue = s;
              }
            } else {
              interpretedValue = o.ToString();
            }
            value2representation[o] = interpretedValue;
            representation2value[interpretedValue] = o;
          }
        }
      }

      public override object GetValue(object component) {
        object coreValue = base.GetValue(component);
        if (coreValue == null) {
          return coreValue;
        }
        //first, look in map...
        string representation;
        value2representation.TryGetValue(coreValue, out representation);
        return representation ?? coreValue;
      }

      public override void SetValue(object component, object value) {
        if (value == null) {
          base.SetValue(component, value);
        }
        //first, look in map...
        string representation = value as string;
        if (representation != null) {
          object coreValue;
          representation2value.TryGetValue(representation, out coreValue);
          if (coreValue != null) {
            base.SetValue(component, coreValue);
          } else {
            base.SetValue(component, value);
          }
        } else {
          base.SetValue(component, value);
        }
      }

      internal ICollection<string> GetStringRepresentation() {
        return representation2value.Keys;
      }

      public override TypeConverter Converter {
        get {
          if (_converter != null) {
            return _converter;
          }
          _converter =
            _enclosingInstance.editorFactory.CreateConverter(this, _enclosingInstance._handler.I18nFactory,
                                                             _enclosingInstance._handler.Name);
          return _converter;
        }
      }

      public override object Value {
        get { return base.GetValue(this); }
        set { base.SetValue(this, value); }
      }
    }


    public object Lookup(Type type) {
      if (type == typeof (I18NFactory)) {
        return I18NFactory;
      }
      return null;
    }
  }
}