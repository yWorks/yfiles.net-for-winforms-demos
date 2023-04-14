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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Demo.yFiles.Option.Editor;

namespace Demo.yFiles.Option.Handler
{
  /// <summary>
  /// OptionItem for double values.
  /// </summary>
  [Serializable]
  public sealed class DoubleOptionItem : OptionItem
  {
    /// <summary>
    /// Returns <c>typeof(double)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (double); }
    }

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    public DoubleOptionItem(string name) : base(name) {}

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public DoubleOptionItem(string name, object value) : base(name, value) {}

    ///<inheritdoc/>
    protected override string GetStringValue() {
      return XmlConvert.ToString((double) Value);
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      return XmlConvert.ToDouble(value.FirstChild.Value);
    }
  }

  /// <summary>
  /// OptionItem for float values.
  /// </summary>
  [Serializable]
  public sealed class FloatOptionItem : OptionItem
  {
    /// <summary>
    /// Returns <c>typeof(double)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (float); }
    }

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    public FloatOptionItem(string name) : base(name) {}

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public FloatOptionItem(string name, object value) : base(name, value) {}

    ///<inheritdoc/>
    protected override string GetStringValue() {
      return XmlConvert.ToString((float) Value);
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      return XmlConvert.ToSingle(value.FirstChild.Value);
    }
  }

  /// <summary>
  /// OptionItem for string values.
  /// </summary>
  [Serializable]
  public sealed class StringOptionItem : OptionItem
  {
    /// <summary>
    /// Returns <c>typeof(double)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (string); }
    }

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    public StringOptionItem(string name) : base(name) {
      SetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE, false);
    }

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public StringOptionItem(string name, object value) : base(name, value) {
      SetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE, false);
    }

    ///<inheritdoc/>
    protected override string GetStringValue() {
      return Value.ToString();
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      return value.FirstChild.Value;
    }

    ///<inheritdoc/>
    public override void RestoreState(XmlElement elem) {
      XmlAttribute attr = elem.Attributes["isUndefined"];
      if (attr != null) {
        if (XmlConvert.ToBoolean(attr.Value)) {
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
      if(n == null) {
        Value = String.Empty;
      }
      else if (n.NodeType == XmlNodeType.Text) {
        Value = GetValueFromNode(elem);
      }
    }
  }


  /// <summary>
  /// OptionItem for int values.
  /// </summary>
  [Serializable]
  public sealed class IntOptionItem : OptionItem
  {
    /// <summary>
    /// Returns <c>typeof(double)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (int); }
    }

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    public IntOptionItem(string name) : base(name) {}

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public IntOptionItem(string name, object value) : base(name, value) {}

    ///<inheritdoc/>
    protected override string GetStringValue() {
      return XmlConvert.ToString((int) Value);
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      return XmlConvert.ToInt32(value.FirstChild.Value);
    }
  }

  /// <summary>
  /// OptionItem for bool values.
  /// </summary>
  [Serializable]
  public sealed class BoolOptionItem : OptionItem
  {
    /// <summary>
    /// Returns <c>typeof(double)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (bool); }
    }

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    public BoolOptionItem(string name)
      : base(name) {
      SetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE, false);
    }

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public BoolOptionItem(string name, object value)
      : base(name, value) {
      SetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE, false);
    }

    ///<inheritdoc/>
    protected override string GetStringValue() {
      return XmlConvert.ToString((bool) Value);
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      return XmlConvert.ToBoolean(value.FirstChild.Value);
    }
  }


  /// <summary>
  /// This class is a quick'n'dirty hack to create option handlers with specific type constraints.
  /// </summary>
  /// <remarks>The advantage over using ObjectOptionItem is that the more specific type information
  /// allows the framework infrastructure to reuse existing type editors etc. without the need to
  /// explicitly inherit ObjectOptionItem for this type just for overwriting the property attributes.</remarks>
  [Serializable]
  public class GenericOptionItem<T> : OptionItem
  {    
    /// <summary>
    /// Always returns <typeparamref name="T"/>.
    /// </summary>
    public override Type Type {
      get { return typeof (T); }
    }

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    public GenericOptionItem(string name) : base(name) {}

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public GenericOptionItem(string name, object value) : base(name, value) {}

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="attributes">An optional map of attributes for the item</param>
    public GenericOptionItem(string name, IDictionary<string, object> attributes) : base(name, attributes) {}

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    /// <param name="attributes">An optional map of attributes for the item</param>
    public GenericOptionItem(string name, object value, IDictionary<string, object> attributes)
      : base(name, value, attributes) {}

    ///<inheritdoc/>
    protected override string GetStringValue() {
      TypeConverter converter = CreateConverter();
      if (converter != null && converter.CanConvertTo(typeof(string))) {
        return converter.ConvertToString(Value);
      }
      if (Value is double) {
        return XmlConvert.ToString((double) Value);
      }
      if (Value is float) {
        return XmlConvert.ToString((float) Value);
      }
      if (Value is int) {
        return XmlConvert.ToString((int) Value);
      }
      if (Value is bool) {
        return XmlConvert.ToString((bool) Value);
      }
      if (Value is string) {
        return (string) Value;
      }
      return Value.ToString();
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      TypeConverter converter = CreateConverter();
      if(converter != null && converter.CanConvertFrom(typeof(string))) {
        return converter.ConvertFromString(value.FirstChild.Value);
      }
      XmlAttribute typeAttr = value.Attributes["type"];
      if (typeAttr != null) {
        switch (typeAttr.Value) {
          case "System.Double":
            return XmlConvert.ToDouble(value.FirstChild.Value);
          case "System.Single":
            return XmlConvert.ToSingle(value.FirstChild.Value);
          case "System.Int32":
            return XmlConvert.ToInt32(value.FirstChild.Value);
          case "System.Int16":
            return XmlConvert.ToInt16(value.FirstChild.Value);
          case "System.Boolean":
            return XmlConvert.ToBoolean(value.FirstChild.Value);
          case "System.String":
            return value.FirstChild.Value;
        }
      }
      return null;
    }

    internal virtual TypeConverter CreateConverter() {
      TypeConverterAttribute converterAttr =
        TypeDescriptor.GetProperties(this, false)["Value"].Attributes[typeof(TypeConverterAttribute)]
        as TypeConverterAttribute;

      TypeConverter coreConverter;
      
      if (this is ICollectionSupport && converterAttr.ConverterTypeName == "") {
        //special handling for the list converter
        coreConverter = new ListTypeConverter(((ICollectionSupport)this).Domain,
                                              ((ICollectionSupport)this).EntryType,
                                              (bool)
                                              this.GetAttribute(
                                                CollectionOptionItem<object>.USE_ONLY_DOMAIN_ATTRIBUTE),
                                              null, "", null);
      } else if (converterAttr.ConverterTypeName != "") {
        //attribute set, get the converter that is set by the attribute
        coreConverter = TypeDescriptor.GetProperties(this, false)["Value"].Converter;
      } else {
        //no attribute, delegate to base
        coreConverter = TypeDescriptor.GetConverter(this.Type);
      }

      bool supportNull = (bool)this.GetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE);
      bool supportUndefined = (bool)this.GetAttribute(OptionItem.SUPPORT_UNDEFINED_VALUE_ATTRIBUTE);
      string nullValueRepresentation = this.GetAttribute(OptionItem.NULL_VALUE_STRING_ATTRIBUTE) as string;
      //test for overrides
      object converterOverride = this.GetAttribute(OptionItem.CUSTOM_CONVERTER_ATTRIBUTE);
      if (converterOverride != null) {
        if (converterOverride is Type) {
          try {
            TypeConverter o = System.Activator.CreateInstance((Type)converterOverride) as TypeConverter;
            if (o != null) {
              coreConverter = o;
            }
          } catch {
            Trace.WriteLine("Cannot create converter instance");
          }
        } else if (converterOverride is string) {
          try {
            TypeConverter o =
              System.Activator.CreateInstance(Type.GetType((string)converterOverride)) as TypeConverter;
            if (o != null) {
              coreConverter = o;
            }
          } catch {
            Trace.WriteLine("Cannot create converter instance");
          }
        } else {
          Trace.WriteLine("Invalid type for converter attribute");
        }
      }
      if (supportNull || supportUndefined) {
          return new NullableTypeConverter(coreConverter,
                                           supportNull,
                                           supportUndefined,
                                           nullValueRepresentation == null ? "" : nullValueRepresentation);        
      }
      return coreConverter;
    }
  }

  /// <summary>
  /// Specialized OptionItem for Color values
  /// </summary>
  public class ColorOptionItem : GenericOptionItem<Color>
  {
    /// <summary>
    /// Create a new instance with the given name. The initial value is set to <see cref="Color.Empty"/>
    /// </summary>
    /// <param name="name">The name of the item</param>
    public ColorOptionItem(string name) : base(name) {
      SetAttribute(OptionItem.NULL_VALUE_OBJECT, Color.Empty);
    }

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    public ColorOptionItem(string name, object value) : base(name, value) {
      SetAttribute(OptionItem.NULL_VALUE_OBJECT, Color.Empty);
    }

    /// <summary>
    /// Create a new instance with the given name. The initial value is set to <see cref="Color.Empty"/>
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="attributes">An optional map of attributes for the item</param>
    public ColorOptionItem(string name, IDictionary<string, object> attributes)
      : base(name, attributes) {
      SetAttribute(OptionItem.NULL_VALUE_OBJECT, Color.Empty);
    }

    /// <summary>
    /// Create a new instance with the given name and the specified initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="value">The initial value for this item</param>
    /// <param name="attributes">An optional map of attributes for the item</param>
    public ColorOptionItem(string name, object value, IDictionary<string, object> attributes)
      : base(name, value, attributes) {
      SetAttribute(OptionItem.NULL_VALUE_OBJECT, Color.Empty);
    }

    ///<inheritdoc/>
    protected override string GetStringValue() {
      Color c = (Color) Value;
      if (c.IsNamedColor) {
        return c.Name;
      } else {
        return "#" + c.ToArgb().ToString("X");
      }
    }

    ///<inheritdoc/>
    protected override object GetValueFromNode(XmlElement value) {
      Color retval;
      string p = value.FirstChild.Value;
      if (p.StartsWith("#")) {
        retval = Color.FromArgb(Int32.Parse(p.Substring(1), NumberStyles.AllowHexSpecifier));
      } else {
        retval = Color.FromName(p);
      }
      return retval;
    }
  }


  /// <summary>
  /// Interface for option items that support a list of valied values.
  /// </summary>
  internal interface ICollectionSupport
  {
    /// <summary>
    /// The 
    /// </summary>
    IEnumerable Domain { get; }

    Type EntryType { get; }
  }

  /// <summary>
  /// Option item that can have a list of valid entries for the item's value
  /// </summary>
  /// <typeparam name="T">The type of the entries.</typeparam>
  [Serializable]
  public class CollectionOptionItem<T> : GenericOptionItem<T>, ICollectionSupport
  {
    private ICollection<T> _domain;

    /// <summary>
    /// If <see langword="true"/>, values that are not in the lsit of valid values are rejected.
    /// </summary>
    public const string USE_ONLY_DOMAIN_ATTRIBUTE = "CollectionOptionItem.USE_ONLY_DOMAIN_ATTRIBUTE";

    /// <summary>
    /// Create a new instance with the given name and an undefined initial value.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="domain">The list of valid values</param>
    public CollectionOptionItem(string name, ICollection<T> domain)
      : base(name) {
      _domain = domain;
      IEnumerator<T> enumerator = domain.GetEnumerator();
      if (enumerator.MoveNext()) {
        Value = enumerator.Current;
      }
      //by default, both types are not supported
      SetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      SetAttribute(SUPPORT_UNDEFINED_VALUE_ATTRIBUTE, true);
      SetAttribute(USE_ONLY_DOMAIN_ATTRIBUTE, true);
    }


    /// <summary>
    /// Create a new instance with the given name.
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="domain">The list of valid values</param>
    /// <param name="value">The initial value for this item</param>
    public CollectionOptionItem(string name, ICollection<T> domain, object value)
      : base(name) {
      _domain = domain;
      SetAttribute(SUPPORT_NULL_VALUE_ATTRIBUTE, false);
      SetAttribute(SUPPORT_UNDEFINED_VALUE_ATTRIBUTE, true);
      SetAttribute(USE_ONLY_DOMAIN_ATTRIBUTE, true);
      Value = value;
    }

    IEnumerable ICollectionSupport.Domain {
      get { return _domain; }
    }

    Type ICollectionSupport.EntryType {
      get { return typeof (T); }
    }

    /// <summary>
    /// Get or set the value of this item.
    /// </summary>
    //[TypeConverter(typeof(ListTypeConverter))]
    public override object Value {
      get { return base.Value; }
      set {
        if (value is T) {
          object attr = GetAttribute(USE_ONLY_DOMAIN_ATTRIBUTE);
          if (attr != null && attr is bool && (bool) attr) {
            if (_domain.Contains((T) value)) {
              base.Value = value;
            } else {
              throw new ArgumentException("Value " + value + " not in value domain");
            }
          } else {
            base.Value = value;
          }
        } else if (value == null || value.Equals(VALUE_UNDEFINED)) {
          base.Value = value;
        }
          //else if (value == VALUE_UNDEFINED && (bool)GetAttribute(SUPPORT_UNDEFINED_VALUE_ATTRIBUTE))
          //{
          //    base.Value = value;
          //}
        else {
          throw new ArgumentException("Value " + value + " has invalid type");
        }
      }
    }

    /// <summary>
    /// Always returns <c>typeof (ICollection&lt;T>)</c>
    /// </summary>
    public override Type Type {
      get { return typeof (ICollection<T>); }
    }
  }
}
