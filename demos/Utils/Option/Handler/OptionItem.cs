/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.6.
 ** Copyright (c) 2000-2024 by yWorks GmbH, Vor dem Kreuzberg 28,
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

/* Don't remove this comment, it's needed for deployment */
/*@TRACE_DEFINE@*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;
using yWorks.Graph;
using yWorks.Support;

namespace Demo.yFiles.Option.Handler
{
  /// <summary>
  /// Abstract implementation of interface <see cref="OptionItem"/> that handles general objects.
  /// </summary>
  /// <remarks>For specialized handling/validation etc. of custom types, this class must
  /// be extended by custom implementations</remarks>
  [Serializable]
  public abstract class OptionItem : INotifyPropertyChanged, IOptionItem, IOwnerSettable
  {

    #region private members

    private string _name;
    private object _value;
    private bool _enabled = true;
//    private bool _hidden = false;
    private IOptionGroup _owner;
    private IDictionary<string, object> attributes;
    private Guid _id;

    /// <summary>
    /// Attribute key that controls this option item supports the "no value" concept. 
    /// </summary>
    /// <remarks>If this is set to <see langword="false"/>,
    /// attempts to set a <see langword="null"/> <see cref="Value"/> will be rejected. If set to
    /// <see langword="true"/>, UI editors will automatically contain a wrapper that
    /// allows to specify an "empty" value.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string SUPPORT_NULL_VALUE_ATTRIBUTE = "OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE";

    /// <summary>
    /// Attribute key that controls this option item supports the "undefined value" concept. 
    /// </summary>
    /// <remarks>If set to <see langword="true"/>, UI editors will correctly handle
    /// when an undefined value (set with <see cref="SetUndefined"/> or by setting a <see cref="Value"/>
    /// of <see cref="VALUE_UNDEFINED"/>) is set on this item. This is mainly useful for
    /// items that encapsulate a multiple selection of actual objects.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string SUPPORT_UNDEFINED_VALUE_ATTRIBUTE = "OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE";

    /// <summary>
    /// An optional string that appears as the description in a 
    /// <see cref="System.Windows.Forms.PropertyGrid"/> view.
    /// </summary>
    /// <remarks>This value will not be localized.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string DESCRIPTION_ATTRIBUTE = "OptionItem.DESCRIPTION_ATTRIBUTE";

    /// <summary>
    /// An alternative label for UI editors that will be used instead of the name.
    /// </summary>
    /// <remarks>This value will not be localized.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string DISPLAYNAME_ATTRIBUTE = "OptionItem.DISPLAYNAME_ATTRIBUTE";

    /// <summary>
    /// A custom representation string for <see langword="null"/> values in UI editors.
    /// </summary>
    /// <remarks>If not specified, a default value will be used (currently <c>(null))</c></remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string NULL_VALUE_STRING_ATTRIBUTE = "OptionItem.NULL_VALUE_STRING_ATTRIBUTE";

    //add overrides for editors and/or converters

    /// <summary>
    /// Allows to specify an alternative <see cref="TypeConverter"/> for UI editors.
    /// </summary>
    /// <remarks>You can either set an existing <see cref="TypeConverter"/> instance or
    /// the fully qualified classname your custom converter here.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string CUSTOM_CONVERTER_ATTRIBUTE = "OptionItem.CUSTOM_CONVERTER_ATTRIBUTE";

    /// <summary>
    /// Allows to specify an alternative <see cref="UITypeEditor"/> for UI editors in
    /// a <see cref="System.Windows.Forms.PropertyGrid"/>.
    /// </summary>
    /// <remarks>You can either set an existing <see cref="UITypeEditor"/> instance or
    /// the fully qualified classname your custom editor here.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string CUSTOM_TABLEITEM_EDITOR = "OptionItem.CUSTOM_TABLEITEM_EDITOR";

    /// <summary>
    /// Allows to specify an alternative <see cref="UITypeEditor"/> for UI editors in
    /// a dialog like view.
    /// </summary>
    /// <remarks>You can either set an existing <see cref="UITypeEditor"/> instance or
    /// the fully qualified classname your custom editor here.</remarks>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string CUSTOM_DIALOGITEM_EDITOR = "OptionItem.CUSTOM_DIALOGITEM_EDITOR";
    
    /// <summary>
    /// Allows to specify an alternative internationalization prefix for the localization of this item.
    /// </summary>
    /// <seealso cref="IOptionItem.SetAttribute"/>
    /// <seealso cref="IOptionItem.GetAttribute"/>
    public const string CUSTOM_I18N_PREFIX = "OptionItem.I18N_PREFIX";

    /// <summary>
    /// Singleton instance for a object that marks an <c>undefined</c> value.
    /// </summary>
    /// <seealso cref="SetUndefined"/>
    /// <seealso cref="SUPPORT_UNDEFINED_VALUE_ATTRIBUTE"/>
    public static readonly object VALUE_UNDEFINED = new UndefinedValueType();

    /// <summary>
    /// Allows to specify an alternative object that plays the role of <see langword="null"/> e.g. for value types.
    /// </summary>
    public const string NULL_VALUE_OBJECT = "OptionItem.NULL_VALUE_OBJECT";

    [NonSerialized] private bool suppressEvents = false;

    #endregion

    #region constructors

    /// <summary>
    /// Create new ObjectOptionItem with given name and no initial value set.
    /// </summary>
    /// <remarks>The option is enabled by default.</remarks>
    /// <param name="name">The canonical (non-localized) name of the option</param>
    protected OptionItem(string name) {
      if (name == null || name == "") {
        throw new ArgumentNullException("value", "Name must not be null or empty");
      }
      _name = name;
      attributes = new Dictionary<string, object>();
      //by default, null values and undefined values are supported
      attributes[SUPPORT_NULL_VALUE_ATTRIBUTE] = true;
      attributes[SUPPORT_UNDEFINED_VALUE_ATTRIBUTE] = true;
      attributes[NULL_VALUE_STRING_ATTRIBUTE] = "(null)";
      //set id
      _id = Guid.NewGuid();
      Value = null;
    }

    /// <summary>
    /// Create new ObjectOptionItem with given name and initial value.
    /// </summary>
    /// <remarks>The option is enabled by default.</remarks>
    /// <param name="name">The canonical (non-localized) name of the option</param>
    /// <param name="initialValue">The initial value for this item.</param>
    protected OptionItem(string name, object initialValue) : this(name) {
      Value = initialValue;
    }
    
    /// <summary>
    /// Create new ObjectOptionItem with given name and no initial value set.
    /// </summary>
    /// <remarks>The option is enabled by default.</remarks>
    /// <param name="name">The canonical (non-localized) name of the option</param>
    /// <param name="attributes">An optional map of attributes for the item</param>
    protected OptionItem(string name, IDictionary<string, object> attributes) {
      if (name == null || name == "") {
        throw new ArgumentNullException("value", "Name must not be null or empty");
      }
      _name = name;
      Value = null;
      this.attributes = attributes;
      //set id
      _id = Guid.NewGuid();
      
    }

    /// <summary>
    /// Create new ObjectOptionItem with given name and initial value.
    /// </summary>
    /// <remarks>The option is enabled by default.</remarks>
    /// <param name="name">The canonical (non-localized) name of the option</param>
    /// <param name="initialValue">The initial value for this item.</param>
    /// <param name="attributes">An optional map of attributes for the item</param>
    protected OptionItem(string name, object initialValue, IDictionary<string, object> attributes) : this(name, attributes) {
      Value = initialValue;
    }

    #endregion

    /// <summary>
    /// Get or set the owning group of an item
    /// </summary>
    public virtual IOptionGroup Owner {
      get { return _owner; }
    }

    /// <summary>
    /// Get or set the owning group of an item
    /// </summary>
    IOptionGroup IOwnerSettable.Owner {
      set { _owner = value as OptionGroup; }
    }

    /// <inheritdoc/>
    public abstract Type Type { get; }

    /// <inheritdoc/>
    public string Name {
      get { return _name; }
    }

    /// <inheritdoc/>
    public virtual bool Enabled {
      get {
        if (_owner == null) {
          return _enabled;
        } else {
          return _owner.Enabled & _enabled;
        }
      }
      set {
        if (_enabled != value) {
          _enabled = value;
          //fire event to notify listeners
          OnStatusChanged(ID, _enabled, false, true, false);
        }
      }
    }

//    public bool Hidden {
//      get { return _hidden; }
//      set {
//        if (_hidden != value) {
//          _hidden = value;
//          //fire event to notify listeners
//          OnStatusChanged(ID, Enabled, _hidden, false, true);
//        }
//      }
//    }

    internal bool SuppressEvents {
      get {
        OptionGroup owner = _owner as OptionGroup;
        if (owner == null) {
          return suppressEvents;
        } else {
          return owner.SuppressEvents && suppressEvents;
        }
      }
      set { suppressEvents = value; }
    }

    /// <summary>
    /// Get or set the value of this item.
    /// </summary>
    /// <remarks>When a new value is set, a <see cref="PropertyChanged"/> event is fired.</remarks>
    public virtual object Value {
      get {
//        Trace.WriteLine("Getting value for " + _name + " as: " + _value);
//                if(IsUndefined)
//                {
////                    throw 
//                }
        return _value;
      }
      set {
        //todo: check whether equals is appropriate?
        if (value == VALUE_UNDEFINED &&
            !(bool) GetAttribute(SUPPORT_UNDEFINED_VALUE_ATTRIBUTE)) {
          throw new ArgumentException("Undefined value is not supported for this OptionItem");
        }
        if (value == null && !(bool) GetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE)) {
          throw new ArgumentException("Null value is not supported for this OptionItem");
        }
        if (_value == null || value == null
            || !_value.Equals(value)) {
//          Trace.WriteLine("Setting new value for " + _name + " to: " + value);
          object oldValue = _value;
          _value = value;
          OnPropertyChanged(this, oldValue, value);
        }
      }
    }

    /// <summary>
    /// Convenience method that allows to check whether an OptionItem has an undefined Value
    /// </summary>
    public bool IsUndefined {
      get { return _value == VALUE_UNDEFINED; }
    }

    /// <summary>
    /// Convenience method to set the item state to an undefined value.
    /// </summary>
    /// <remarks>This is an atomic operation that overwrites the item's value with an
    /// <see cref="VALUE_UNDEFINED"/> token</remarks>
    public void SetUndefined() {
      Value = VALUE_UNDEFINED;
    }

//    public void SetValueState(OptionValueChangedEventArgs args) {
//      if (args.IsUndefined) {
//        //set the value to undefined state
//        Value = VALUE_UNDEFINED;
//      } else {
//        Value = args.NewValue;
//      }
//    }

    /// <inheritdoc/>     
    public Guid ID {
      get { return _id; }
    }

    /// <inheritdoc/>
    public object GetAttribute(string key) {
      if(attributes == null) {
        return null;
      }
      if (attributes.ContainsKey(key)) {
        return attributes[key];
      }
      return null;
    }

    /// <inheritdoc/>
    public void SetAttribute(string key, object value) {
      if (key != null) {
        object oldVal = GetAttribute(key);
        attributes[key] = value;
        OnAttributeChanged(key, oldVal, value);
      }
    }

    /// <summary>
    /// Raises the <see cref="AttributeChanged"/> event
    /// </summary>
    /// <param name="key"></param>
    /// <param name="val"></param>
    /// <param name="value"></param>
    protected void OnAttributeChanged(string key, object val, object value) {
      if (!SuppressEvents && AttributeChanged != null) {
        AttributeChanged(ID, new AttributeChangedEventArgs(key, val, value));
      }
    }

    /// <inheritdoc/>
    public IList<string> GetAttributeKeys() {
      return (new List<string>(attributes.Keys)).AsReadOnly();
    }

    #region IOptionItem Members

    ///<inheritdoc/>
    public void SetLookup(Type t, object impl) {
      lookup.Put(t, impl);
    }

    ///<inheritdoc/>
    public virtual void SaveState(XmlElement parent) {
      XmlDocument owner = parent.OwnerDocument;
      XmlElement elem = owner.CreateElement("Item");
      elem.SetAttribute("name", Name);
      elem.SetAttribute("type", Value.GetType().ToString());
      parent.AppendChild(elem);
      if(IsUndefined) {
        elem.SetAttribute("undefined", XmlConvert.ToString(true));
      }
      else if(Value == null) {
        elem.SetAttributeNode("isNull", XmlConvert.ToString(true));
      }
      else {
        XmlText text = owner.CreateTextNode(GetStringValue());
        elem.AppendChild(text);
      }
    }

    /// <summary>
    /// Returns the current value of the item as string
    /// </summary>
    /// <returns>Returns the current value of the item as string</returns>
    protected abstract string GetStringValue();

    ///<inheritdoc/>
    public virtual void RestoreState(XmlElement elem) {
      XmlAttribute attr = elem.Attributes["isUndefined"]; 
      if(attr != null) {
        if(XmlConvert.ToBoolean(attr.Value)) {
          SetUndefined();
          return;
        }
      }
      attr = elem.Attributes["isNull"];
      if (attr != null) {
        if (XmlConvert.ToBoolean(attr.Value)) {
          Value = null;
          return;
        }
      }
      XmlNode n = elem.FirstChild;
      if(n.NodeType == XmlNodeType.Text) {
        Value = GetValueFromNode(elem);
      }
    }

    /// <summary>
    ///Gets the current value of the item from the given XML node
    /// </summary>
    /// <param name="value">The XML node that contains the elements value</param>
    /// <returns>The current value of the item as stored in <paramref name="value"/></returns>
    protected abstract object GetValueFromNode(XmlElement value);

    [NonSerialized]
    private DictionaryLookup lookup = new DictionaryLookup();

    #endregion

    #region INotifyPropertyChanged Members

    /// <summary>
    /// This event gets fired (only) whenever the <see cref="Value"/> property is changed.
    /// </summary>
    [field : NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <inheritdoc/>
    protected void OnPropertyChanged(OptionItem source, object oldValue, object newValue) {
      if (!SuppressEvents && PropertyChanged != null) {
        PropertyChanged(source,
                        new OptionValueChangedEventArgs(source.Name, source.ID, oldValue, newValue, IsUndefined));
      }
    }

    #endregion

    #region IOptionItem Members

    /// <inheritdoc/>
    [field : NonSerialized]
    public event ItemStatusHandler StatusChanged;

    /// <inheritdoc/>
    [field : NonSerialized]
    public event AttributeChangedHandler AttributeChanged;

    /// <summary>
    /// Raises the <see cref="StatusChanged"/> event.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="status"></param>
    /// <param name="hidden"></param>
    /// <param name="enabledChanged"></param>
    /// <param name="hiddenChanged"></param>
    protected internal virtual void OnStatusChanged(object source, bool status, bool hidden, bool enabledChanged,
                                                    bool hiddenChanged) {
      if (!SuppressEvents && StatusChanged != null) {
        //Console.WriteLine("{0}: Firing status change to {1}", this._name, status);
        StatusChanged(source, new ItemStatusEventArgs(status, hidden, enabledChanged, hiddenChanged, false));
      }
                                                    }

    #endregion

    #region undefined value support

    /// <summary>
    /// Type for objects that can be used as markers for undefined values
    /// </summary>
    [Serializable]
    internal sealed class UndefinedValueType
    {
      ///<summary>
      ///Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
      ///</summary>
      ///
      ///<returns>
      ///true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
      ///</returns>
      ///
      ///<param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>. </param><filterpriority>2</filterpriority>
      public override bool Equals(object obj) {
        if(obj == null) {
          return false;
        }
        //there can only be one undefined token in the whole framework
        if (ReferenceEquals(this, null)) {
          return false;
        }
        if (ReferenceEquals(this, obj)) {
          return true;
        }

        return obj.GetType().Equals(typeof (UndefinedValueType));
      }

      ///<summary>
      ///Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
      ///</summary>
      ///
      ///<returns>
      ///A hash code for the current <see cref="T:System.Object"></see>.
      ///</returns>
      ///<filterpriority>2</filterpriority>
      public override int GetHashCode() {
        return base.GetHashCode();
      }

      /// <summary>
      /// Override of equality operator
      /// </summary>
      /// <param name="left"></param>
      /// <param name="right"></param>
      /// <returns></returns>
      public static bool operator ==(Object left, UndefinedValueType right) {
        //delegate to Equals method
        return !ReferenceEquals(left, null) && left.Equals(right);
      }

      /// <summary>
      /// Override of inequality operator
      /// </summary>
      /// <param name="left"></param>
      /// <param name="right"></param>
      /// <returns></returns>
      public static bool operator !=(Object left, UndefinedValueType right) {
        return !(left == right);
      }

      public static bool operator ==(UndefinedValueType left, Object right) {
        //delegate to Equals method
        return !ReferenceEquals(left, null) && left.Equals(right);
      }

      public static bool operator !=(UndefinedValueType left, Object right) {
        return !(left == right);
      }

      ///<summary>
      ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
      ///</summary>
      ///
      ///<returns>
      ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
      ///</returns> 
      ///<filterpriority>2</filterpriority>
      public override string ToString() {
        return OptionHandler.FallBackI18NFactory.GetString(null, "OptionHandlerI18N_UNDEFINED");
      }
    }


    /// <summary>
    /// Callback method that can be used to copy the state and value from an existing option item
    /// to this instance.
    /// </summary>
    /// <param name="savedItem">The existing instance to be used as reference.</param>
    protected internal virtual void ReadStateFromItem(IOptionItem savedItem) {
      if (savedItem.GetType() != GetType() ||
          savedItem.Type != Type ||
          savedItem.Name != Name) {
        //don't read from incompatible items
        return;
      }
      foreach (string key in GetAttributeKeys()) {
        SetAttribute(key, savedItem.GetAttribute(key));
      }
      Enabled = savedItem.Enabled;
      Value = savedItem.Value;
    }

    #endregion

    #region ILookup Members

    ///<inheritdoc/>
    public object Lookup(Type type) {
      object o = lookup.Lookup(type);
      if(o == null && Owner != null) {
        return Owner.Lookup(type);
      }
      return o;
    }

    #endregion
  }

  /// <summary>
  /// Represents the state change of an <see cref="IOptionItem"/>.
  /// </summary> 
  /// <seealso cref="IOptionItem.Enabled"/>
  /// <seealso cref="IOptionItem.StatusChanged"/>
  public class ItemStatusEventArgs : EventArgs
  {
    private bool enabled;
    private bool hidden;
    private bool enabledChanged;
    private bool hiddenChanged;
    private bool indirectChange;

    /// <summary>
    /// Create a new instance
    /// </summary>
    /// <param name="enabled"><see langword="true"/> iff <see cref="IOptionItem.Enabled"/> is currently <see langword="true"/></param>
    /// <param name="hidden">This is currently ignored</param>
    /// <param name="enabledChanged"><see langword="true"/> if the value of <see cref="IOptionItem.Enabled"/>
    /// has been changed</param>
    /// <param name="hiddenChanged">This is currently ignored</param>
    /// <param name="indirectChange"><see langword="true"/> iff the status change
    /// has been triggered by a status change in a containing <see cref="OptionGroup"/></param>
    public ItemStatusEventArgs(bool enabled, bool hidden, bool enabledChanged, bool hiddenChanged,
                               bool indirectChange) {
      this.enabled = enabled;
      this.hidden = hidden;
      this.enabledChanged = enabledChanged;
      this.hiddenChanged = hiddenChanged;
      this.indirectChange = indirectChange;
                               }

    /// <summary>
    /// <see langword="true"/> iff <see cref="IOptionItem.Enabled"/> 
    /// is currently <see langword="true"/>
    /// </summary>
    public bool Enabled {
      get { return enabled; }
    }

    /// <summary>
    /// This is <see langword="true"/> iff 
    /// the status change is caused by a change of its owners status
    /// </summary>
    public bool IndirectChange {
      get { return indirectChange; }
    }

    /// <summary>
    /// Ignored for now
    /// </summary>
    public bool Hidden {
      get { return hidden; }
    }

    /// <summary>
    /// This is <see langword="true"/> iff 
    /// if the value of <see cref="IOptionItem.Enabled"/> has been changed
    /// </summary>
    public bool EnabledChanged {
      get { return enabledChanged; }
    }

    /// <summary>
    /// Ignored for now
    /// </summary>
    public bool HiddenChanged {
      get { return hiddenChanged; }
    }
  }

  /// <summary>
  /// Represents the change of an attribute value
  /// </summary>
  /// <seealso cref="IOptionItem.SetAttribute"/>
  /// <seealso cref="IOptionItem.GetAttribute"/>
  /// <seealso cref="IOptionItem.AttributeChanged"/>
  public class AttributeChangedEventArgs : EventArgs
  {
    private string key;
    private object oldValue;
    private object newValue;

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="key">The key of the attribute that has been changed</param>
    /// <param name="oldValue">The old value of the attribute</param>
    /// <param name="newValue">The new value of the attribute</param>
    public AttributeChangedEventArgs(string key, object oldValue, object newValue) {
      this.key = key;
      this.oldValue = oldValue;
      this.newValue = newValue;
    }

    /// <summary>
    /// Get the key of the attribute that has been changed
    /// </summary>
    public string Key {
      get { return key; }
    }

    /// <summary>
    /// Get the old value of the attribute
    /// </summary>
    public object OldValue {
      get { return oldValue; }
    }

    /// <summary>
    /// Get the new value of the attribute
    /// </summary>
    public object NewValue {
      get { return newValue; }
    }
  }

  /// <summary>
  /// Represents the value change of an <see cref="IOptionItem"/>
  /// </summary>
  /// <seealso cref="IOptionItem.Value"/>
  /// <seealso cref="INotifyPropertyChanged.PropertyChanged"/>
  public class OptionValueChangedEventArgs : PropertyChangedEventArgs
  {
    private object oldValue;
    private object newValue;
    private Guid sourceID;
    private bool undefined;

    /// <summary>
    /// Create a new instance
    /// </summary>
    /// <param name="propertyName">The <see cref="IOptionItem.Name"/> 
    /// of the item that has been changed</param>
    /// <param name="sourceID">The <see cref="IOptionItem.ID"/> of the item that has been changed</param>
    /// <param name="oldValue">The old value of the item that has been changed</param>
    /// <param name="newValue">The new value of the item that has been changed</param>
    /// <param name="undefined"><see langword="true"/> iff the item is in undefined state now</param>
    public OptionValueChangedEventArgs(string propertyName, Guid sourceID, object oldValue, object newValue,
                                       bool undefined) : base(propertyName) {
      this.oldValue = oldValue;
      this.newValue = newValue;
      this.sourceID = sourceID;
      this.undefined = undefined;
                                       }

    /// <summary>
    /// Get the old value of the item that has been changed
    /// </summary>
    public object OldValue {
      get { return oldValue; }
    }

    /// <summary>
    /// Get the new value of the item that has been changed
    /// </summary>
    public object NewValue {
      get { return newValue; }
    }

    /// <summary>
    /// Get the <see cref="IOptionItem.ID"/> of the item that has been changed
    /// </summary>
    public Guid SourceID {
      get { return sourceID; }
    }

    /// <summary>
    /// Return <see langword="true"/> iff the item is in undefined state now
    /// </summary>
    public bool IsUndefined {
      get { return undefined; }
    }
  }

  /// <summary>
  /// Event handler for status changes
  /// </summary>
  /// <param name="source"></param>
  /// <param name="statusInformation"></param>
  public delegate void ItemStatusHandler(object source, ItemStatusEventArgs statusInformation);

  /// <summary>
  /// Event handler for attribute changes
  /// </summary>
  /// <param name="source"></param>
  /// <param name="args"></param>
  public delegate void AttributeChangedHandler(object source, AttributeChangedEventArgs args);
}