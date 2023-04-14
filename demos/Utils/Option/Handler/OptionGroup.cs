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
using System.Xml;

namespace Demo.yFiles.Option.Handler
{
  
  /// <summary>
  /// Used to classify option items into groups.
  /// </summary>
  /// <remarks>OptionGroups can be nested.</remarks>
  [Serializable]
  public class OptionGroup : OptionItem, IOptionGroup
  {
    private List<IOptionItem> children;
    //used for direct access by item name
    private IDictionary<string, IOptionItem> nameMap = new Dictionary<string, IOptionItem>();

    /// <summary>
    /// Get or set the value of this item.
    /// </summary>
    /// <remarks>Getting this property's value returns just a readonly list of all children, while
    /// setting the property's  value has no effect and is silently ignored.</remarks>
    public override object Value {
      get { return children.AsReadOnly(); }
      set {}
    }

    /// <summary>
    /// Set the value of an nested item, given it's canonical path.
    /// </summary>
    /// <remarks>The path has the form "name of group1.name of group2....name of item</remarks>
    /// <param name="itemPath"></param>
    /// <param name="value"></param>
    public void SetValue(string itemPath, object value) {
      GetItemByName(itemPath).Value = value;
    }

    /// <summary>
    /// Get the value of an item, given it's canonical path without the item's name and item name.
    /// </summary>
    /// <remarks>The path has the form "name of group1.name of group2....name of item</remarks>
    /// <param name="itemPath">the canonical path without the item's name</param>
    /// <param name="itemName">The name of the item.</param>
    public object GetValue(string itemPath, string itemName) {
      if (itemPath == null || itemPath == "") {
        return this[itemName].Value;
      }
      return GetItemByName(itemPath + "." + itemName).Value;
    }

    /// <summary>
    /// Get the value of an item, given it's canonical path.
    /// </summary>
    /// <remarks>The path has the form "name of group1.name of group2....name of item</remarks>
    /// <param name="itemPath"></param>
    public object GetValue(string itemPath) {
      return GetItemByName(itemPath).Value;
    }

    /// <summary>
    /// Return a readonly list of all children
    /// </summary>
    /// <remarks>This has the same effect as getting the <see cref="Value"/> property</remarks>
    public IList<IOptionItem> Items {
      get { return children.AsReadOnly(); }
    }

    /// <summary>
    /// Always returns <c>typeof(IList&lt;IOptionItem>)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (IList<IOptionItem>); }
    }

    /// <summary>
    /// Retrieve the child item with name <paramref name="index"/>
    /// </summary>
    /// <param name="index">The name of the item to find</param>
    /// <returns>the child item with name <paramref name="index"/>, or <see langword="null"/>
    /// if nno such child exists.</returns>
    public IOptionItem this[string index] {
      get {
        IOptionItem retval;
        nameMap.TryGetValue(index, out retval);
        return retval;
      }
    }

    /// <summary>
    /// Return the number of children.
    /// </summary>
    public int Count {
      get { return children.Count; }
    }

    /// <summary>
    /// Create new instance with the given <paramref name="name"/> and no children.
    /// </summary>
    /// <param name="name">The name of the group</param>
    public OptionGroup(string name)
      : base(name) {
      children = new List<IOptionItem>();
      //Option groups don't support both elements
      SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      SetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE, false);
      }

    /// <summary>
    /// Add a new <see cref="IOptionItem"/> to this group
    /// </summary>
    /// <param name="item"></param>
    public virtual IOptionItem AddOptionItem(IOptionItem item) {
      children.Add(item);
      if (item is IOwnerSettable) {
        ((IOwnerSettable)item).Owner = this;
      }

      nameMap.Add(item.Name, item);
      if (item is OptionGroup) {
        //propagate structure changes             
        ((IOptionGroup) item).StructureChanged += item_StructureChanged;
      }
      //register for child notification events
      item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
      //fire structure change notification event
      OnStructureChanged();
      return item;
    }

    void item_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      OnPropertyChanged((OptionItem) sender,
                        ((OptionValueChangedEventArgs) e).OldValue, ((OptionValueChangedEventArgs) e).NewValue);
    }

    /// <summary>
    /// Remove the specified child.
    /// </summary>
    /// <param name="item">The child to remove</param>
    public virtual void RemoveOptionItem(IOptionItem item) {
      children.Remove(item);
      nameMap.Remove(item.Name); 
      if (item is IOwnerSettable) {
        ((IOwnerSettable)item).Owner = this;
      }

      if (item is OptionGroup) {
        //propagate structure changes             
        ((IOptionGroup) item).StructureChanged -= item_StructureChanged;
      }
      OnStructureChanged();
    }

    private void item_StructureChanged(object sender, StructureChangeEventArgs e) {
      //Console.WriteLine("{0}: Consuming structure change from {1}", this.Name, ((IOptionItem)sender).Name);
      OnStructureChanged();
    }

    /// <summary>
    /// This event gets fired when the structure of the group changes, i.e. when children
    /// are added or removed.
    /// </summary>
    [field : NonSerialized]
    public event StructureChangedHandler StructureChanged;

    /// <summary>
    /// Raises the <see cref="StructureChanged"/> event.
    /// </summary>
    protected void OnStructureChanged() {
      //Console.WriteLine("{0}: Firing structure change", this.Name);
      if (!SuppressEvents && StructureChanged != null) {
        StructureChanged(this, new StructureChangeEventArgs());
      }
    }

    /// <inheritdoc/>
    protected internal override void ReadStateFromItem(IOptionItem savedItem) {
      base.ReadStateFromItem(savedItem);
      OptionGroup newGroup = savedItem as OptionGroup;
      foreach (OptionItem item in children) {
        if (newGroup.nameMap.ContainsKey(item.Name)) {
          IOptionItem backupValue = newGroup[item.Name];
          item.ReadStateFromItem(backupValue);
        }
      }
    }

    /// <summary>
    /// Clear the list of all children
    /// </summary>
    public void Clear() {
      IList<IOptionItem> tmpList = new List<IOptionItem>(children);
      foreach (IOptionItem item in tmpList) {
        if (item is OptionGroup) {
          ((IOptionGroup) item).Clear();
        }
        RemoveOptionItem(item);
      }
    }

    /// <summary>
    /// Retrieve an <see cref="IOptionItem"/> instance from nested groups.
    /// </summary>
    /// <remarks>Path elements are seperated by dots</remarks>
    /// <param name="itemPath"></param>
    /// <returns></returns>
    public IOptionItem GetItemByName(string itemPath) {
      IOptionGroup item = this;
      string[] pathArray = itemPath.Split('.');
      for (int i = 0; i < pathArray.Length - 1; i++) {
        string s = pathArray[i];
        item = item[s] as IOptionGroup;
        if (item == null) {
          return null;
        }
      }
      //last step...
      return item[pathArray[pathArray.Length - 1]];
    }

    /// <summary>
    /// Retrieve an <see cref="OptionGroup"/> instance from nested groups.
    /// </summary>
    /// <remarks>Path elements are seperated by dots</remarks>
    /// <param name="groupPath"></param>
    /// <returns></returns>
    public OptionGroup GetGroupByName(string groupPath) {
      return GetItemByName(groupPath) as OptionGroup;
    }

    /// <summary>
    /// Convenience method to add an <see cref="IntOptionItem"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="IntOptionItem"/></returns>
    public IntOptionItem AddInt(string itemName, int initialValue) {
      IntOptionItem newItem = new IntOptionItem(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add an <see cref="IntOptionItem"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <param name="minValue">The minimum allowed value.</param>
    /// <param name="maxValue">The maximum allowed value.</param>
    /// <returns>A new instance of <see cref="IntOptionItem"/></returns>
    public IOptionItem AddInt(string itemName, int initialValue, int minValue, int maxValue) {
      IntOptionItem newItem = new IntOptionItem(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add an <see cref="IntOptionItem"/> to this group
    /// </summary>
    /// <remarks>The values of this item are constrained to be non negative, with no upper bound.</remarks>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="IntOptionItem"/></returns>
    public IOptionItem AddNonNegativeInt(string itemName, int initialValue) {
      return AddInt(itemName, initialValue, 0, int.MaxValue);
    }

    /// <summary>
    /// Convenience method to add a <see cref="BoolOptionItem"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="BoolOptionItem"/></returns>
    public BoolOptionItem AddBool(string itemName, bool initialValue) {
      BoolOptionItem newItem = new BoolOptionItem(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add a <see cref="DoubleOptionItem"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="DoubleOptionItem"/></returns>
    public DoubleOptionItem AddDouble(string itemName, double initialValue) {
      DoubleOptionItem newItem = new DoubleOptionItem(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add a <see cref="DoubleOptionItem"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <param name="minValue">The minimum allowed value.</param>
    /// <param name="maxValue">The maximum allowed value.</param>
    /// <returns>A new instance of <see cref="DoubleOptionItem"/></returns>
    public IOptionItem AddDouble(string itemName, double initialValue, double minValue, double maxValue) {
      OptionItem newItem = new DoubleOptionItem(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add an <see cref="DoubleOptionItem"/> to this group
    /// </summary>
    /// <remarks>The values of this item are constrained to be non negative, with no upper bound.</remarks>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="IntOptionItem"/></returns>
    public IOptionItem AddNonNegativeDouble(string itemName, int initialValue) {
      return AddDouble(itemName, initialValue, 0, double.MaxValue);
    }

    /// <summary>
    /// Convenience method to add a <see cref="StringOptionItem"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="StringOptionItem"/></returns>
    public StringOptionItem AddString(string itemName, string initialValue) {
      StringOptionItem newItem = new StringOptionItem(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add a <see cref="CollectionOptionItem{T}"/> to this group
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="domain">List of initial values for this item.</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="CollectionOptionItem{T}"/></returns>
    public CollectionOptionItem<T> AddList<T>(string itemName, ICollection<T> domain, T initialValue) {
      CollectionOptionItem<T> newItem = new CollectionOptionItem<T>(itemName, domain, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add a <see cref="GenericOptionItem{T}"/> to this handler
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="GenericOptionItem{T}"/></returns>
    public GenericOptionItem<T> AddGeneric<T>(string itemName, T initialValue) {
      GenericOptionItem<T> newItem = new GenericOptionItem<T>(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add a OptionItem for <see cref="Color"/> values to this handler
    /// </summary>
    /// <param name="itemName">The name of the item</param>
    /// <param name="initialValue">The initial value of the item</param>
    /// <returns>A new instance of <see cref="GenericOptionItem{Color}"/></returns>
    public GenericOptionItem<Color> AddColor(string itemName, Color initialValue) {
      GenericOptionItem<Color> newItem = new GenericOptionItem<Color>(itemName, initialValue);
      AddOptionItem(newItem);
      return newItem;
    }

    /// <summary>
    /// Convenience method to add a <see cref="OptionGroup"/> to this handler
    /// </summary>
    /// <param name="name">The name of the group where this item belongs to. If 
    /// <see langword="null"/>, the item is added directly to the handler itself.</param>
    /// <returns>A new instance of <see cref="OptionGroup"/></returns>
    public OptionGroup AddGroup(string name) {
      OptionGroup retval = new OptionGroup(name);
      AddOptionItem(retval);
      return retval;
    }

    /// <inheritdoc/>
    public override void SaveState(XmlElement parent) {
      XmlDocument owner = parent.OwnerDocument;
      XmlElement elem = owner.CreateElement("Group");
      elem.SetAttribute("name", Name);
      parent.AppendChild(elem);
      foreach (IOptionItem item in children) {
        item.SaveState(elem);
      }
    }

    /// <inheritdoc/>
    public override void RestoreState(XmlElement parent) {
      foreach (XmlNode n in parent.ChildNodes) {
        if (n.NodeType == XmlNodeType.Element) {
          string name = n.Attributes["name"].Value;
          IOptionItem item = this[name];
          if (item != null) {
            item.RestoreState((XmlElement) n);
          }
        }
      }
    }

    /// <inheritdoc/>
    protected override string GetStringValue() {
      return "";
    }

    /// <inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      return null;
    }
  }

  /// <summary>
  /// Event handler for strucure change events
  /// </summary>
  /// <param name="source"></param>
  /// <param name="e"></param> 
  public delegate void StructureChangedHandler(object source, StructureChangeEventArgs e);

  /// <summary>
  /// Specialized EventArgs for use with a <see cref="StructureChangedHandler"/>
  /// </summary>
  public class StructureChangeEventArgs : EventArgs {}
}
