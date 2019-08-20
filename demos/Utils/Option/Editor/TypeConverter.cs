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
using System.Globalization;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Class that decorates an existing type converter for Undefined/null value filtering
  /// </summary>
  /// 
  internal class NullableTypeConverter : TypeConverter
  {
    //todo: string value handling is not always correct (converter not called at all???)
    private TypeConverter _delegateConverter;
    private StandardValuesCollection standardValuesCache;
    private bool supportNull;
    private bool supportUndefined;
    private string nullValueStringRepresentation;

    public NullableTypeConverter(TypeConverter delegateConverter, bool supportNull,
                                 bool supportUndefined, string nullValueStringRepresentation)
      : base() {
      _delegateConverter = delegateConverter;
      this.supportUndefined = supportUndefined;
      this.supportNull = supportNull;
      this.nullValueStringRepresentation = nullValueStringRepresentation;

      standardValuesCache = CreateStandardValues();
      }

    private StandardValuesCollection CreateStandardValues() {
      if (GetStandardValuesSupported()) {
        if (supportNull) {
          List<object> tmpList = new List<object>();
          tmpList.Add(null);

          ICollection delegatedList = _delegateConverter.GetStandardValues();
          if (delegatedList != null) {
            foreach (object item in delegatedList) {
              tmpList.Add(item);
            }
          }
          return new StandardValuesCollection(tmpList);
        } else {
          return new StandardValuesCollection(_delegateConverter.GetStandardValues());
        }
      }
      return null;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
      if (supportUndefined && sourceType == typeof (OptionItem.UndefinedValueType)) {
        return true;
      }
      return _delegateConverter.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
      //todo: make this configurable/i18n
      if (supportNull && (value.ToString() == nullValueStringRepresentation)) {
        return null;
      }
      //todo: forbid this value for input without throwing an exception (perhaps revert?)!
      //todo: This is not correct for string values!
      //we probably need support in the UIEditor...
//            if (supportUndefined && value.ToString() == "(Undefined)")
//            {
//                return OptionItem.VALUE_UNDEFINED;
//          }
      if (supportUndefined && value == OptionItem.VALUE_UNDEFINED) {
        return OptionItem.VALUE_UNDEFINED;
      }
      return _delegateConverter.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
      return _delegateConverter.CanConvertTo(context, destinationType);
    }


    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                     Type destinationType) {
      //todo: make this configurable
      if (supportNull && (value == null || value.GetType() == typeof (DBNull))
          && destinationType == typeof (string)) {
        //todo: make this configurable
        return nullValueStringRepresentation;
      }
      if (supportNull && value is string && value.Equals(nullValueStringRepresentation)
          && destinationType == typeof (string)) {
        //todo: make this configurable
        return nullValueStringRepresentation;
      }
      if (supportUndefined && value == OptionItem.VALUE_UNDEFINED && destinationType == typeof (string)) {
        return "";
      }
      return _delegateConverter.ConvertTo(value, destinationType);
                                     }

    public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues) {
      return _delegateConverter.CreateInstance(context, propertyValues);
    }

    public Type CoreType {
      get { return _delegateConverter.GetType(); }
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
      return _delegateConverter.GetStandardValuesSupported(context);
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
      return standardValuesCache;
    }

    ///<summary>
    ///Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exclusive list of possible values, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"></see> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exhaustive list of possible values; false if other values are possible.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
      return _delegateConverter.GetStandardValuesExclusive(context);
    }
  }

  /// <summary>
  /// Class that decorates an existing type converter for Undefined/null value filtering
  /// </summary>
  /// 
  internal class ExpandableNullableTypeConverter : ExpandableObjectConverter
  {
    //todo: string value handling is not always correct (converter not called at all???)
    private TypeConverter _delegateConverter;
    private StandardValuesCollection standardValuesCache;
    private bool supportNull;
    private bool supportUndefined;
    private string nullValueStringRepresentation;

    public ExpandableNullableTypeConverter(TypeConverter delegateConverter, bool supportNull, bool supportUndefined,
                                           string nullValueStringRepresentation)
      : base() {
      _delegateConverter = delegateConverter;
      this.supportUndefined = supportUndefined;
      this.supportNull = supportNull;
      this.nullValueStringRepresentation = nullValueStringRepresentation;
      standardValuesCache = CreateStandardValues();
      }

    private StandardValuesCollection CreateStandardValues() {
      if (GetStandardValuesSupported()) {
        if (supportNull) {
          List<object> tmpList = new List<object>();
          tmpList.Add(null);

          ICollection delegatedList = _delegateConverter.GetStandardValues();
          if (delegatedList != null) {
            foreach (object item in delegatedList) {
              tmpList.Add(item);
            }
          }
          return new StandardValuesCollection(tmpList);
        } else {
          return new StandardValuesCollection(_delegateConverter.GetStandardValues());
        }
      }
      return null;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
      if (sourceType == typeof (string)) {
        return true;
      }
      return _delegateConverter.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
      //todo: make this configurable/i18n
      if (supportNull && (value.ToString() == nullValueStringRepresentation
                          || value.ToString() == "")) {
        return null;
      }
      //todo: forbid this value for input without throwing an exception (perhaps revert?)!
      //todo: This is not correct for string values!
      //we probably need support in the UIEditor...
      //            if (supportUndefined && value.ToString() == "(Undefined)")
      //            {
      //                return OptionItem.VALUE_UNDEFINED;
      //          }

      try {
        object from = _delegateConverter.ConvertFrom(context, culture, value);

        return from;
      } catch (NotSupportedException) {
        throw new Exception(value.ToString() + " is not valid for property");
      }
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
      return _delegateConverter.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                     Type destinationType) {
      //todo: make this configurable
      if (supportNull && (value == null || value.GetType() == typeof (DBNull))
          && destinationType == typeof (string)) {
        //todo: make this configurable
        return nullValueStringRepresentation;
      }
      if (supportUndefined && value == OptionItem.VALUE_UNDEFINED && destinationType == typeof (string)) {
        return "";
      }
      return _delegateConverter.ConvertTo(context, culture, value, destinationType);
                                     }

    public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues) {
      return _delegateConverter.CreateInstance(context, propertyValues);
    }

    public Type CoreType {
      get { return _delegateConverter.GetType(); }
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
      return _delegateConverter.GetStandardValuesSupported(context);
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
      return standardValuesCache;
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
      if (context != null) {
        PropertyDescriptor desc = context.PropertyDescriptor;
        object value = desc.GetValue(context.Instance);
        if (value == OptionItem.VALUE_UNDEFINED) {
          return false;
        }
      }

      return _delegateConverter.GetCreateInstanceSupported(context);
    }

    public override bool IsValid(ITypeDescriptorContext context, object value) {
      return _delegateConverter.IsValid(context, value);
    }

    ///<summary>
    ///Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exclusive list of possible values, using the specified context.
    ///</summary>
    ///<returns>
    ///true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"></see> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exhaustive list of possible values; false if other values are possible.
    ///</returns>
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
      return _delegateConverter.GetStandardValuesExclusive(context);
    }

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value,
                                                               Attribute[] attributes) {
      return _delegateConverter.GetProperties(context, value, attributes);
                                                               }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
      if (context != null) {
        PropertyDescriptor desc = context.PropertyDescriptor;
        object value = desc.GetValue(context.Instance);
        if (value == OptionItem.VALUE_UNDEFINED) {
          return false;
        }
      }

      return _delegateConverter.GetPropertiesSupported(context);
    }
  }

  internal class ListTypeConverter : TypeConverter
  {
    private StandardValuesCollection cachedStandardValues;
    private Type t;
    private bool listValuesExclusive;
    private IDictionary<string, object> i18nMap;
    private I18NFactory i18NFactory;
    private string i18nPrefix;
    private string context;
    ArrayList i18nList = new ArrayList();

    public ListTypeConverter(IEnumerable domain, Type t, bool listValuesExclusive,
                             I18NFactory i18NFactory, string i18nprefix, string context) {
      this.t = t;
      i18nMap = new Dictionary<string, object>();

      this.listValuesExclusive = listValuesExclusive;
      this.i18NFactory = i18NFactory;
      this.i18nPrefix = i18nprefix;
      this.context = context;

      TypeConverter coreConverter = TypeDescriptor.GetConverter(t);
      foreach (object o in domain) {
        if (coreConverter != null && coreConverter.CanConvertTo(typeof (string))) {
          //we need this for a  human readable description
          string interpretedValue;
          if (i18NFactory != null && !string.IsNullOrEmpty(i18nprefix)) {
            interpretedValue = PropertyModelView.GetLocalizedString(i18NFactory, context, i18nprefix + ".VALUE.", coreConverter.ConvertToString(o));
          } else {
            interpretedValue = coreConverter.ConvertToString(o);
          }
          i18nList.Add(interpretedValue);
          i18nMap[interpretedValue] = o;
        } else {
          i18nList.Add(o);
        }
      }
      //return new StandardValuesCollection(tmp);

      cachedStandardValues = new StandardValuesCollection(i18nList);
                             }

    ///<summary>
    ///Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> should be called to find a common set of values the object supports; otherwise, false.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
      return true;
    }

    ///<summary>
    ///Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
    ///</summary>
    ///
    ///<returns>
    ///A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"></see> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
      //Duh! StandardValuesCollection cannot be filled with IEnumeration directly
      return cachedStandardValues;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
      return sourceType == typeof (string);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
      TypeConverter coreConverter = TypeDescriptor.GetConverter(t);
      if (coreConverter != null && coreConverter.CanConvertFrom(typeof (string)) && value is string) {
        if (i18nMap.ContainsKey((string) value)) {
          return i18nMap[(string) value];
        } else {
          return coreConverter.ConvertFromString((string) value);
        }
      }
      return value;
    }

    ///<summary>
    ///Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exclusive list of possible values, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"></see> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exhaustive list of possible values; false if other values are possible.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
      return listValuesExclusive;
    }

    ///<summary>
    ///Returns whether this converter can convert the object to the specified type, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if this converter can perform the conversion; otherwise, false.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    ///<param name="destinationType">A <see cref="T:System.Type"></see> that represents the type you want to convert to. </param>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
      return destinationType == typeof (string);
    }

    ///<summary>
    ///Converts the given value object to the specified type, using the specified context and culture information.
    ///</summary>
    ///
    ///<returns>
    ///An <see cref="T:System.Object"></see> that represents the converted value.
    ///</returns>
    ///
    ///<param name="culture">A <see cref="T:System.Globalization.CultureInfo"></see>. If null is passed, the current culture is assumed. </param>
    ///<param name="descContext">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    ///<param name="destinationType">The <see cref="T:System.Type"></see> to convert the value parameter to. </param>
    ///<param name="value">The <see cref="T:System.Object"></see> to convert. </param>
    ///<exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
    ///<exception cref="T:System.ArgumentNullException">The destinationType parameter is null. </exception>
    public override object ConvertTo(ITypeDescriptorContext descContext, CultureInfo culture, object value,
                                     Type destinationType) {
      if (destinationType == typeof (string)) {
        TypeConverter coreConverter = TypeDescriptor.GetConverter(t);
        if (coreConverter != null && coreConverter.CanConvertTo(typeof (string))) {
          if (value is string && i18nList.Contains(value)) {
            return value;
          }
          //we need this for a  human readable description
          string interpretedValue;
          if (i18NFactory != null && i18nPrefix != null && i18nPrefix != "") {
            interpretedValue =
              PropertyModelView.GetLocalizedString(i18NFactory, context, i18nPrefix + ".VALUE.", coreConverter.ConvertToString(value));
          } else {
            interpretedValue = coreConverter.ConvertToString(value);
          }
//                    Console.WriteLine("Value "+value+", i18n: "+interpretedValue);
          return interpretedValue;
        }
      }
      return null;
                                     }
  }

  internal class I18NTypeConverter : TypeConverter
  {
    private StandardValuesCollection cachedStandardValues;
    private bool listValuesExclusive;

    public I18NTypeConverter(ICollection<string> domain, bool listValuesExclusive) {
      this.listValuesExclusive = listValuesExclusive;
      ArrayList tmpList = new ArrayList();
      foreach (string s in domain) {
        tmpList.Add(s);
      }
      cachedStandardValues = new StandardValuesCollection(tmpList);
    }

    ///<summary>
    ///Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> should be called to find a common set of values the object supports; otherwise, false.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
      return true;
    }

    ///<summary>
    ///Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
    ///</summary>
    ///
    ///<returns>
    ///A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"></see> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
      //Duh! StandardValuesCollection cannot be filled with IEnumeration directly
      return cachedStandardValues;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
      return sourceType == typeof (string);
    }

    ///<summary>
    ///Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exclusive list of possible values, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection"></see> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues"></see> is an exhaustive list of possible values; false if other values are possible.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
      return listValuesExclusive;
    }

    ///<summary>
    ///Returns whether this converter can convert the object to the specified type, using the specified context.
    ///</summary>
    ///
    ///<returns>
    ///true if this converter can perform the conversion; otherwise, false.
    ///</returns>
    ///
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    ///<param name="destinationType">A <see cref="T:System.Type"></see> that represents the type you want to convert to. </param>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
      return destinationType == typeof (string);
    }

    ///<summary>
    ///Converts the given object to the type of this converter, using the specified context and culture information.
    ///</summary>
    ///
    ///<returns>
    ///An <see cref="T:System.Object"></see> that represents the converted value.
    ///</returns>
    ///
    ///<param name="culture">The <see cref="T:System.Globalization.CultureInfo"></see> to use as the current culture. </param>
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    ///<param name="value">The <see cref="T:System.Object"></see> to convert. </param>
    ///<exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
      return value;
    }

    ///<summary>
    ///Converts the given value object to the specified type, using the specified context and culture information.
    ///</summary>
    ///
    ///<returns>
    ///An <see cref="T:System.Object"></see> that represents the converted value.
    ///</returns>
    ///
    ///<param name="culture">A <see cref="T:System.Globalization.CultureInfo"></see>. If null is passed, the current culture is assumed. </param>
    ///<param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
    ///<param name="destinationType">The <see cref="T:System.Type"></see> to convert the value parameter to. </param>
    ///<param name="value">The <see cref="T:System.Object"></see> to convert. </param>
    ///<exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
    ///<exception cref="T:System.ArgumentNullException">The destinationType parameter is null. </exception>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                     Type destinationType) {
      return value;
                                     }
  }
}