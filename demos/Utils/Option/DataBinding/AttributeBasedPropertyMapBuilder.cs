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

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Demo.yFiles.Option.DataBinding
{
  internal class AttributeBasedPropertyMapBuilder<TSubject> : PropertyMapBuilderBase<TSubject> where TSubject : class 
  {
    public AttributeBasedPropertyMapBuilder() : base(true) {}

    protected override void BuildPropertyMapImpl(IPropertyBuildContext<TSubject> builder) {

      MethodInfo[] infos = typeof(IPropertyBuildContext<>).MakeGenericType(typeof(TSubject)).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
      MethodInfo buildChildContextMethod = null;
      foreach (MethodInfo info in infos) {
        if (info.IsGenericMethod && info.ReturnType.IsGenericType) {
          buildChildContextMethod = info;
          break;
        }
      }
      if (buildChildContextMethod == null) {
        throw new InvalidDataException("Method not found!");
      }

      TSubject instance = builder.CurrentInstance;

      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
      foreach (PropertyDescriptor d  in properties) {
        PropertyDescriptor descriptor = d;

        AttributeBuilderAttribute mapAttribute =
          (AttributeBuilderAttribute)descriptor.Attributes[typeof(AttributeBuilderAttribute)];
        if (mapAttribute != null && mapAttribute.Invisible) {
          continue;
        }

        DisplayNameAttribute displayNameAttribute =
          (DisplayNameAttribute) descriptor.Attributes[typeof (DisplayNameAttribute)];
        string propertyName;
        if (displayNameAttribute == null || displayNameAttribute.DisplayName.Length < 1) {
          propertyName = descriptor.Name;
        } else {
          propertyName = displayNameAttribute.DisplayName;
        }
        AssignmentPolicyAttribute assignmentPolicyAttribute =
          (AssignmentPolicyAttribute) descriptor.Attributes[typeof (AssignmentPolicyAttribute)];
        Type propertyType = descriptor.PropertyType;

        NullableAttribute nullableAttribute = (NullableAttribute) descriptor.Attributes[typeof (NullableAttribute)];
        bool nullable = nullableAttribute == null || nullableAttribute.IsNullable;

        AssignmentPolicy policy;

        if (assignmentPolicyAttribute != null && builder.Policy == AssignmentPolicy.Default) {
          policy = assignmentPolicyAttribute.Policy;
        } else {
          policy = builder.Policy == AssignmentPolicy.Default ? AssignmentPolicy.ModifyInstance : builder.Policy;
        }

        object childbuilder;
        object currentPropertyValue = descriptor.GetValue(builder.CurrentInstance);

        if (currentPropertyValue == null && nullable) {          
          //todo: make this dependent on yet-to-implement nullable attribute
          childbuilder = builder.GetPropertyMapBuilder(propertyType, currentPropertyValue);
        }
        else {
          childbuilder = builder.GetPropertyMapBuilder(currentPropertyValue);
        }

        if (childbuilder != null) {

          // Create the SetInstanceDelegate that uses the builders' CurrentInstance for the set operation
          Delegate setMemberInstanceDelegate;
          {
            Type helperSetInstanceDelegateType = typeof(HelperSetInstanceDelegate<,>)
              .MakeGenericType(propertyType, typeof(TSubject));
            ConstructorInfo setterConstructorInfo = helperSetInstanceDelegateType
              .GetConstructor(new Type[] { typeof(PropertyDescriptor), typeof(IPropertyBuildContext<TSubject>) });
            object helperSetInstanceDelegateInstance = setterConstructorInfo
              .Invoke(new object[] { descriptor, builder });

            MethodInfo method = helperSetInstanceDelegateType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)[0];
            setMemberInstanceDelegate = Delegate.CreateDelegate(typeof(SetInstanceDelegate<>).MakeGenericType(propertyType),
                                                                helperSetInstanceDelegateInstance, method);

          }

          // Create the SetInstanceDelegate that uses the builders' CurrentInstance for the set operation
          Delegate getMemberInstanceDelegate;
          {
            Type helperInstanceDelegateType = typeof(HelperGetInstanceDelegate<,>)
              .MakeGenericType(propertyType, typeof(TSubject));
            ConstructorInfo getterConstructorInfo = helperInstanceDelegateType
              .GetConstructor(new Type[] { typeof(PropertyDescriptor), typeof(IPropertyBuildContext<TSubject>) });
            object helperGetInstanceDelegateInstance = getterConstructorInfo
              .Invoke(new object[] { descriptor, builder });

            MethodInfo method = helperInstanceDelegateType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)[0];
            getMemberInstanceDelegate = Delegate.CreateDelegate(typeof(GetInstanceDelegate<>).MakeGenericType(propertyType),
                                                                helperGetInstanceDelegateInstance, method);

          }
          object childContext = buildChildContextMethod.MakeGenericMethod(propertyType).Invoke(builder,
                                                                  new object[]{propertyName,getMemberInstanceDelegate,
                                                                                               setMemberInstanceDelegate, 
                                                                                               policy});

          MethodInfo buildPropertyMapMethod = typeof(IPropertyMapBuilder).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public |
                                                                      BindingFlags.Instance)[0];

          buildPropertyMapMethod.MakeGenericMethod(propertyType).Invoke(childbuilder, new object[] { childContext });
        } else {
          builder.AddEntry(propertyName, new ReflectionGetter<TSubject>(descriptor, builder),
                           new ReflectionSetter<TSubject>(descriptor, builder));
        }
      }
    }

    private class ReflectionGetter<T> : IValueGetter where T : class
    {
      public ReflectionGetter(PropertyDescriptor getInstance, IPropertyBuildContext<T> context) {
        this.getInstance = getInstance;
        this.context = context;
      }

      private PropertyDescriptor getInstance;
      private readonly IPropertyBuildContext<T> context;

      #region IValueGetter Members

      public object GetValue() {
        return getInstance.GetValue(context.CurrentInstance);
      }

      public bool CanGet() {
        return getInstance != null;
      }

      #endregion
    }

    private class ReflectionSetter<T> : IValueSetter where T : class
    {
      public ReflectionSetter(PropertyDescriptor setInstance, IPropertyBuildContext<T> context) {
        this.setInstance = setInstance;
        this.context = context;
      }

      private PropertyDescriptor setInstance;
      private readonly IPropertyBuildContext<T> context;

      #region IValueGetter Members

      public bool CanSet() {
        return setInstance != null && !setInstance.IsReadOnly;
      }

      #endregion

      public void SetValue(object value) {
        setInstance.SetValue(context.CurrentInstance, value);
      }
    }
  }

  internal sealed class HelperGetInstanceDelegate<TChild, TSubject> where TSubject : class
  {
    private PropertyDescriptor descriptor;
    private IPropertyBuildContext<TSubject> context;

    public HelperGetInstanceDelegate(PropertyDescriptor descriptor, IPropertyBuildContext<TSubject> context) {
      this.descriptor = descriptor;
      this.context = context;
    }

    /// <summary>
    /// DO NOT REMOVE - USED THROUGH REFLECTION ABOVE!!!
    /// </summary>
    public TChild GetInstance() {
      return (TChild)descriptor.GetValue(context.CurrentInstance);
    }
  }

  internal sealed class HelperSetInstanceDelegate<TChild, TSubject> where TSubject : class
  {
    private PropertyDescriptor descriptor;
    private IPropertyBuildContext<TSubject> context;

    public HelperSetInstanceDelegate(PropertyDescriptor descriptor, IPropertyBuildContext<TSubject> context) {
      this.descriptor = descriptor;
      this.context = context;
    }

    /// <summary>
    /// DO NOT REMOVE - USED THROUGH REFLECTION ABOVE!!!
    /// </summary>
    public void SetValue(TChild value) {
      if (!descriptor.IsReadOnly) {
        descriptor.SetValue(context.CurrentInstance, value);
      }
    }
  }

  /// <summary>
  /// Specifies whether the item should not be shown.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class AttributeBuilderAttribute: Attribute
  {
    private bool invisible = false;

    /// <summary>
    /// Gets or sets a value indicating whether the adorned item is invisible.
    /// </summary>
    public bool Invisible {
      get { return invisible; }
      set { invisible = value; }
    }
  }
}