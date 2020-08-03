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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.Handler;
using yWorks.Graph;

namespace Demo.yFiles.Option.DataBinding
{
  /// <summary>
  /// A generic implementation of an <see cref="IOptionBuilder"/> 
  /// that uses reflection and <see cref="Attribute"/>s to build the options
  /// of a given subject and type.
  /// </summary>
  /// <remarks>
  /// This implementation depends on the existence of the <see cref="DisplayNameAttribute"/>
  /// at the public <see cref="Type.GetProperties()">properties</see> of the provided type.
  /// That name is used to create the <see cref="IOptionItem"/>s and create the 
  /// <see cref="IOptionBuilderContext.CreateChildContext">child context for nested properties.</see>
  /// </remarks>
  /// <seealso cref="OptionBuilderAttribute"/>.
  /// <seealso cref="AttributeBasedPropertyMapBuilderAttribute"/>
  public class AttributeBasedOptionBuilder : IOptionBuilder
  {
    /// <inheritdoc/>
    public void AddItems(IOptionBuilderContext context, Type subjectType, object subject) {
      Type type1 = subject == null?subjectType:subject.GetType();
      PropertyInfo[] propertyInfos =
        SortProperties(FilterProperties(
          type1.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy),
          context), context);

      foreach (PropertyInfo descriptor in propertyInfos) {
        DisplayNameAttribute displayNameAttribute = GetAttribute<DisplayNameAttribute>(descriptor);
        string propertyName;
        if (displayNameAttribute == null || displayNameAttribute.DisplayName.Length < 1) {
          propertyName = descriptor.Name;
        } else {
          propertyName = displayNameAttribute.DisplayName;
        }
        Type type = descriptor.PropertyType;
        object value = descriptor.GetGetMethod().Invoke(subject, null);

        IOptionBuilder builder = GetBuilder(descriptor, context, subject, type);

        if (builder != null) {
          IOptionBuilderContext childContext = context.CreateChildContext(propertyName);
          builder.AddItems(childContext, type, value);
          ConfigureItem(childContext.Lookup<IOptionGroup>(), descriptor);
        } else {
          IOptionItem item = CreateItem(context, descriptor, type, propertyName, value);
          if (item != null) {
            context.BindItem(item, propertyName);
            ConfigureItem(item, descriptor);
          }
        }
      }
    }

    private IOptionBuilder GetBuilder(PropertyInfo info, IOptionBuilderContext context, object subject, Type type) {
      IOptionBuilder builder = null;
      OptionBuilderAttribute builderAttribute =
        (OptionBuilderAttribute) Attribute.GetCustomAttribute(info, typeof (OptionBuilderAttribute));
      if (builderAttribute != null) {
        builder = Activator.CreateInstance(builderAttribute.OptionBuilderType) as IOptionBuilder;
      } else {
        object value = info.GetGetMethod().Invoke(subject, null);
        builder = context.GetOptionBuilder(type, value);
      }
      return builder;
    }

    private static T GetAttribute<T>(PropertyInfo info) where T : Attribute {
      return (T) Attribute.GetCustomAttribute(info, typeof (T));
    }

    /// <summary>
    /// Configures additional attributes for an option item or option group, such as collapsed state and group visibility.
    /// </summary>
    /// <param name="item">The item or group to configure.</param>
    /// <param name="propertyInfo">The associated property info.</param>
    protected virtual void ConfigureItem(IOptionItem item, PropertyInfo propertyInfo) {
      TableEditorFactory.RenderingHints tableRenderingHints = TableEditorFactory.RenderingHints.Collapsed;
      DefaultEditorFactory.RenderingHints defaultRenderingHints = DefaultEditorFactory.RenderingHints.Invisible;
      ItemRenderingHintsAttribute hintsAttribute =
        (ItemRenderingHintsAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof (ItemRenderingHintsAttribute));
      if (hintsAttribute != null) {
        tableRenderingHints = hintsAttribute.TableRenderingHints;
        defaultRenderingHints = hintsAttribute.DialogRenderingHints;
      }
      item.SetAttribute(TableEditorFactory.RENDERING_HINTS_ATTRIBUTE, tableRenderingHints);
      item.SetAttribute(DefaultEditorFactory.RENDERING_HINTS_ATTRIBUTE, defaultRenderingHints);

      NullableAttribute nullableAttribute =
        (NullableAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof (NullableAttribute));
      if (nullableAttribute != null) {
        item.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, nullableAttribute.IsNullable);
      }

      DescriptionAttribute attribute = GetAttribute<DescriptionAttribute>(propertyInfo);
      if (attribute != null && !attribute.IsDefaultAttribute()) {
        item.SetAttribute(OptionItem.DESCRIPTION_ATTRIBUTE, attribute.Description);
      }

      var customAttributes = Attribute.GetCustomAttributes(propertyInfo);

      foreach (var attrib in customAttributes.OfType<OptionItemAttributeAttribute>()) {
        item.SetAttribute(attrib.Name, attrib.Value);
      }
    }

    /// <summary>
    /// Sorts the list of displayed properties.
    /// </summary>
    /// <remarks>By default, properties are sorted alphabetically by display name.</remarks>
    /// <param name="properties"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected virtual PropertyInfo[] SortProperties(PropertyInfo[] properties, IOptionBuilderContext context) {
      Array.Sort(properties, new DefaultPropertyInfoComparer(context));
      return properties;
    }

    /// <summary>
    /// Filters the list of properties so that only the desired ones are used.
    /// </summary>
    /// <remarks>By default, all properties that have a <see cref="DisplayNameAttribute"/> with a non empty value set are included.</remarks>
    /// <param name="properties"></param>
    /// <param name="context"></param>
    /// <returns></returns>    
    private PropertyInfo[] FilterProperties(PropertyInfo[] properties, IOptionBuilderContext context) {
      List <PropertyInfo> filteredList = new List<PropertyInfo>();
      foreach(var property in properties)
      {
        if (property.CanWrite)
        {
          filteredList.Add(property);
        }
      }
      return filteredList.ToArray();
    }

    /// <summary>
    /// Factory method that creates the option item using the provided parameters.
    /// </summary>
    protected virtual IOptionItem CreateItem(IOptionBuilderContext context, PropertyInfo propertyInfo, Type type,
                                             string description, object value) {
      IOptionItem item = null;
      if (type.IsEnum) {
        Type genericType = typeof (GenericOptionItem<>);
        Type newType = genericType.MakeGenericType(type);
        item = newType.GetConstructor(new Type[] {typeof (string)}).Invoke(new object[] {description}) as IOptionItem;
      } else if (type == typeof (Color)) {
        item = new ColorOptionItem(description);
      } else if (type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom((typeof (ICollection<>)))) {
        Type[] types = type.GetGenericArguments();
        if (types.Length == 1) {
          Type collectionItemType = types[0];
          Type collectionBaseType = typeof (ICollection<>);
          Type collectionType = collectionBaseType.MakeGenericType(collectionItemType);
          Type collectionOptionItemType = typeof (CollectionOptionItem<>);
          item = collectionOptionItemType.MakeGenericType(collectionItemType).GetConstructor(
                   new Type[] {typeof (string), collectionType}).Invoke(new object[] {description, value}) as
                 IOptionItem;
        }
      }

      if (item == null) {
        if (type == typeof (double)) {
          item = new DoubleOptionItem(description);
        } else if (type == typeof (int)) {
          item = new IntOptionItem(description);
        } else if (type == typeof (float)) {
          item = new FloatOptionItem(description);
        } else if (type == typeof (bool)) {
          item = new BoolOptionItem(description);
        } else if (type == typeof (string)) {
          item = new StringOptionItem(description);
        } else {
          Type genericType = typeof (GenericOptionItem<>);
          Type newType = genericType.MakeGenericType(type);
          item =
            newType.GetConstructor(new Type[] {typeof (string)}).Invoke(new object[] {description}) as IOptionItem;
        }
      }
      if (item != null) {
        TypeConverterAttribute converter = GetAttribute<TypeConverterAttribute>(propertyInfo);
        if (converter != null && !converter.IsDefaultAttribute()) {
          try {
            Type typeConverter = Type.GetType(converter.ConverterTypeName);
            if (typeConverter != null) {
              item.SetAttribute(OptionItem.CUSTOM_CONVERTER_ATTRIBUTE, typeConverter);
            }
          } catch (Exception e) {
            Trace.WriteLine("Could not load custom type converter " + e.Message);
          }
        }
        //by default, value types are not nullable
        if (type.IsValueType) {
          item.SetAttribute(OptionItem.SUPPORT_NULL_VALUE_ATTRIBUTE, false);
        }
      }


      return item;
    }

    private class DefaultPropertyInfoComparer : IComparer<PropertyInfo>
    {
      public DefaultPropertyInfoComparer(IOptionBuilderContext context) {
        this.context = context;
      }

      private IOptionBuilderContext context;

      #region IComparer<PropertyInfo> Members

      public int Compare(PropertyInfo x1, PropertyInfo x2) {
        DisplayNameAttribute descriptionAttribute1 = GetAttribute<DisplayNameAttribute>(x1);
        DisplayNameAttribute descriptionAttribute2 = GetAttribute<DisplayNameAttribute>(x2);
        string name1 = descriptionAttribute1 != null && descriptionAttribute1.DisplayName.Length > 0
                        ? descriptionAttribute1.DisplayName
                        : x1.Name;
        string name2 = descriptionAttribute2 != null && descriptionAttribute2.DisplayName.Length > 0
                        ? descriptionAttribute2.DisplayName
                        : x2.Name;
        return name1.CompareTo(name2);
      }

      #endregion
    }
  }

  /// <summary>
  /// The attribute that is used to specify the <see cref="IPropertyMapBuilder"/>
  /// implementation type for a given type or property.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property)]
  public class PropertyMapBuilderAttribute : Attribute
  {
    private readonly Type propertyMapBuilderType;

    /// <summary>
    /// Creates a new instance using the provided type.
    /// </summary>
    /// <param name="propertyMapBuilderType"></param>
    public PropertyMapBuilderAttribute(Type propertyMapBuilderType) {
      this.propertyMapBuilderType = propertyMapBuilderType;
    }

    /// <summary>
    /// Returns the type to use for the <see cref="IPropertyMapBuilder"/> implementation.
    /// </summary>
    public Type PropertyMapBuilderType {
      get { return propertyMapBuilderType; }
    }
  }

  /// <summary>
  /// The attribute that is used to specify that the <see cref="IPropertyMapBuilder"/>
  /// implementation for the annotated type should be created dynamically by
  /// introspecting the type.
  /// </summary>
  /// <seealso cref="AttributeBasedOptionBuilder"/>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property)]
  public class AttributeBasedPropertyMapBuilderAttribute : Attribute
  {
    /// <summary>
    /// Creates the builder for a given type.
    /// </summary>
    /// <param name="t">The type to create an attribute based builder for.</param>
    /// <returns>A builder that is based on the attributes found in the type.</returns>
    public IPropertyMapBuilder CreateBuilder(Type t) {
      return
        (IPropertyMapBuilder)
        (typeof (AttributeBasedPropertyMapBuilder<>).MakeGenericType(t).GetConstructor(Type.EmptyTypes).Invoke(null));
    }
  }

  /// <summary>
  /// The attribute that is used to specify that the <see cref="IPropertyMapBuilder"/>
  /// implementation for the annotated type should be created dynamically by
  /// introspecting the type.
  /// </summary>
  /// <seealso cref="AttributeBasedOptionBuilder"/>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
  public class OptionItemAttributeAttribute : Attribute
  {
    public string Name { get; set; }
    public object Value { get; set; }
  }

  /// <summary>
  /// Determines whether the annotated property may be <see langword="null"/> during the 
  /// creation of an <see cref="AttributeBasedOptionBuilder">attribute based</see>
  /// <see cref="IPropertyMap"/>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class NullableAttribute : Attribute
  {
    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public NullableAttribute(bool isNullable) {
      this.isNullable = isNullable;
    }

    /// <summary>
    /// Whether the value may be set to <see langword="null"/>.
    /// </summary>
    public bool IsNullable {
      get { return isNullable; }
    }

    private bool isNullable;
  }

  /// <summary>
  /// Provides rendering hints for OptionItems.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class ItemRenderingHintsAttribute : Attribute
  {
    /// <summary>
    /// Gets or sets <see cref="TableEditorFactory.RenderingHints"/> for an item in a table view.
    /// </summary>
    /// <remarks>By default, <see cref="TableEditorFactory.RenderingHints.Collapsed"/> is set.</remarks>
    public TableEditorFactory.RenderingHints TableRenderingHints {
      get { return tableRenderingHints; }
      set { tableRenderingHints = value; }
    }

    /// <summary>
    /// Gets or sets <see cref="DefaultEditorFactory.RenderingHints"/> for an item in a dialog view.
    /// </summary>
    /// <remarks>By default, <see cref="DefaultEditorFactory.RenderingHints.Invisible"/> is set.</remarks>
    public DefaultEditorFactory.RenderingHints DialogRenderingHints {
      get { return dialogRenderingHints; }
      set { dialogRenderingHints = value; }
    }

    private TableEditorFactory.RenderingHints tableRenderingHints = TableEditorFactory.RenderingHints.Collapsed;
    private DefaultEditorFactory.RenderingHints dialogRenderingHints = DefaultEditorFactory.RenderingHints.Invisible;
  }


  /// <summary>
  /// Attribute that will be evaluated by <see cref="AttributeBasedPropertyMapBuilderAttribute"/>
  /// during the creation of the <see cref="IPropertyMap"/> to determine
  /// the <see cref="IPropertyBuildContext{TSubject}.Policy"/>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class AssignmentPolicyAttribute : Attribute
  {
    private readonly AssignmentPolicy policy;

    /// <summary>
    /// Creates a new instance using the provided policy.
    /// </summary>
    /// <param name="policy"></param>
    public AssignmentPolicyAttribute(AssignmentPolicy policy) {
      this.policy = policy;
    }

    /// <summary>
    /// Retrieves the policy to use.
    /// </summary>
    public AssignmentPolicy Policy {
      get { return policy; }
    }
  }

  /// <summary>
  /// An enumeration used by <see cref="IPropertyBuildContext{TSubject}.Policy"/>
  /// to determine how the modification of mutable reference values should be performed.
  /// </summary>
  /// <seealso cref="IPropertyBuildContext{TSubject}.SetNewInstance"/>
  public enum AssignmentPolicy
  {
    /// <summary>
    /// The client does not have any preferences, the context itself may decide what to do.
    /// </summary>
    Default,
    /// <summary>
    /// The context may modify the instance, there is no need to 
    /// stay immutable.
    /// </summary>
    ModifyInstance,
    /// <summary>
    /// The context may not be modified, instead a new instance needs to be created that reflects
    /// the modification.
    /// </summary>
    CreateNewInstance
  }
}
