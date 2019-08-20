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
using System.Reflection;
using System.Resources;
using Demo.yFiles.Option.Constraint;
using Demo.yFiles.Option.DataBinding;
using Demo.yFiles.Option.Editor;
using Demo.yFiles.Option.I18N;
using Demo.yFiles.Option.View;
using yWorks.Graph;

namespace Demo.yFiles.Option.Handler
{
  /// <summary>
  /// This class can be used to manage settings and options that belong together
  /// with a consistent interface.
  /// </summary>
  /// <remarks>Each item is represented by an instance of <see cref="IOptionItem"/>. 
  /// To edit values in the handler, you can create editors with subclasses of 
  /// <see cref="EditorFactory"/>.</remarks>
  [Serializable]
  public class OptionHandler : OptionGroup
  {
    private List<IOptionGroup> _groups = new List<IOptionGroup>();
    private IDictionary<string, IOptionGroup> groupMap = new Dictionary<string, IOptionGroup>();
    //todo: make more specific
    [NonSerialized] private List<IModelView> views = new List<IModelView>();

    private static I18NFactory fallBackI18NFactory;

    //not sure if this should be serialized...
    [NonSerialized] private I18NFactory i18nFactory;

    /// <summary>
    /// Gets or sets an <see cref="I18NFactory"/> instance that can be
    /// used for localization of various string values such as item names, tooltips,
    /// button labels, etc.
    /// </summary>
    /// <remarks>If no instance has been set, retrieving this property
    /// returns a default implementation.</remarks>
    public I18NFactory I18nFactory {
      get {
        if (i18nFactory == null) {
          return fallBackI18NFactory;
        }
        return i18nFactory;
      }
      set { i18nFactory = value; }
    }

    internal static I18NFactory FallBackI18NFactory {
      get { return fallBackI18NFactory; }
    }

    static OptionHandler() {
      fallBackI18NFactory = new InternalFallBackI18NFactory();
    }

    //private string _title;

    /// <summary>
    /// Creates a new option handler.
    /// </summary>
    /// <remarks>This option handler always contains one OptionGroup by default, 
    /// with the same name as the title of the handler.</remarks>
    public OptionHandler(string title)
      : base(title) {
      //_title = title;
      //OptionGroup g = new OptionGroup(title);
      //AddOptionGroup(g);
      }

    /// <inheritdoc/>
    public override IOptionItem AddOptionItem(IOptionItem item) {
      if (item is OptionGroup) {
        _groups.Add((OptionGroup) item);
        groupMap[item.Name] = (IOptionGroup) item;
      }
      return base.AddOptionItem(item);
    }

    /// <inheritdoc/>
    public override void RemoveOptionItem(IOptionItem item) {
      if (item is OptionGroup) {
        _groups.Remove((OptionGroup) item);
      }
      base.RemoveOptionItem(item);
    }

    #region convenience methods for item handling

    private void AddItemToGroup(OptionItem item, string groupName) {
      if (groupName == null) {
        this.AddOptionItem(item);
      } else {
        IOptionGroup group;
        if (groupMap.ContainsKey(groupName)) {
          group = groupMap[groupName];
        } else {
          group = AddGroup(groupName);
        }
        group.AddOptionItem(item);
      }
    }

    #endregion

    internal void AddView(PropertyModelView view) {
      if (views == null) {
        views = new List<IModelView>();
      }
      //todo: add notification
      views.Add(view);
      OnViewChanged(view, true);
    }

    internal void RemoveView(PropertyModelView view) {
      views.Remove(view);
      OnViewChanged(view, false);
      //todo: add notification
    }

    #region other convenience methods

    /// <summary>
    /// Return a <b>read-only</b> list of all contained option groups
    /// </summary>  
    public IList<IOptionGroup> Groups {
      get { return _groups.AsReadOnly(); }
    }

    #endregion

    #region internal methods

    internal IList<IModelView> ActiveViews {
      get { return views.AsReadOnly(); }
    }

    #endregion

    #region fallback resource manager

    private class InternalFallBackI18NFactory : I18NFactory
    {
      private ResourceManager rm =
        new ResourceManager("Demo.yFiles.Option.I18N.OptionHandlerI18N", Assembly.GetExecutingAssembly());

      #region I18NFactory Members

      public string GetString(string context, string key) {
        string value = rm.GetString(key);
        return value == null ? key : value;
      }

      #endregion
    }

    #endregion

    #region serialization support

    /// <summary>
    /// Restore a complete state from an existing option handler instance
    /// </summary>
    /// <param name="backupHandler"></param>
    public void ReadState(OptionHandler backupHandler) {
      ReadStateFromItem(backupHandler);
    }

    #endregion

    /// <summary>
    /// EventBracketing method for content changes.
    /// </summary>
    /// <remarks>When this method is called, all <see cref="ContentChanged"/> events
    /// from this handler are temporarily suppressed, until <see cref="EndContentChange"/>
    /// is called. This can be used for complex changes of the handler's content, when it is not necessary
    /// to raise these events, e.g. when views will be recreated anyway.</remarks>
    public void StartContentChange() {
      isUpdating = true;
      SuppressEvents = true;
      
    }

    /// <summary>
    /// EventBracketing method for content changes.
    /// </summary>
    /// <remarks>When this method is called, <see cref="ContentChanged"/> events
    /// from this handler are not suppressed anymore. Together with <see cref="StartContentChange"/>,
    /// this can be used for complex changes of the handler's content, when it is not necessary
    /// to raise these events, e.g. when views will be recreated anyway.</remarks>
    public void EndContentChange() {
      SuppressEvents = false;
      isUpdating = false;
      OnContentChanged();
    }

    private void OnContentChanged() {
      if (!SuppressEvents && ContentChanged != null) {
        ContentChanged(this, new StructureChangeEventArgs());
      }
    }

    private void OnViewChanged(PropertyModelView view, bool isAdded) {
      if (ViewChanged != null) {
        ViewChanged(this, new ViewChangeEventArgs(isAdded, view));
      }
    }

    /// <summary>
    /// Returns whether the handler is currently updating its structure.
    /// </summary>
    /// <remarks>When this returns true, querying the handler for items is unsafe.</remarks>
    public bool IsUpdating {
      get { return isUpdating; }
    }

    private bool isUpdating = false;

    /// <inheritdoc/>
    [field : NonSerialized]
    public event StructureChangedHandler ContentChanged;

    /// <inheritdoc/>
    [field : NonSerialized]
    internal event ViewChangeHandler ViewChanged;

    /// <summary>
    /// Populates this instance from scratch using a provided selection provider.
    /// </summary>
    /// <remarks>This instance will be cleared, and all constraints on it will be reset. 
    /// The builder inspects the first <see cref="IPropertyItemDescriptor{T}"/> from
    /// <paramref name="selectionProvider"/> and creates an <see cref="IOptionBuilder"/> instance 
    /// that will <see cref="IOptionBuilder.AddItems"/> to this instance via the builder.
    /// </remarks>
    /// <param name="selectionProvider"></param>
    /// <param name="contextLookup">The lookup tunnel through to the created
    /// <see cref="IOptionBuilderContext"/> that will be used to query the <see cref="IOptionBuilder"/>
    /// instances for recursive sets of properties.</param>
    public virtual void BuildFromSelection<T>(ISelectionProvider<T> selectionProvider, IContextLookup contextLookup) {
      StartContentChange();
      try {
        Clear();
        DefaultOptionBuilderContext<T> context;
        context = new DefaultOptionBuilderContext<T>(selectionProvider, this);
        context.ContextLookup = contextLookup;

        ConstraintManager constraintManager = this.Lookup<ConstraintManager>();
        if (constraintManager == null) {
          constraintManager = new ConstraintManager(this);
          this.SetLookup(typeof(ConstraintManager), constraintManager);
        }
        constraintManager.Clear();
        IEnumerator<IPropertyItemDescriptor<T>> enumerator = selectionProvider.Selection.GetEnumerator();
        if (enumerator.MoveNext()) {
          IPropertyItemDescriptor<T> descriptor = enumerator.Current;
          T item;
          item = descriptor.Item;
          IOptionBuilder builder = context.GetOptionBuilder(item);
          if (builder != null) {
            builder.AddItems(context, item.GetType(), item);
          }
        }
      } finally {
        EndContentChange();
      }
    }
  }


  internal delegate void ViewChangeHandler(object source, ViewChangeEventArgs e);

  internal class ViewChangeEventArgs : EventArgs
  {
    private bool isAdded;
    private PropertyModelView view;

    public ViewChangeEventArgs(bool isAdded, PropertyModelView view) {
      this.isAdded = isAdded;
      this.view = view;
    }

    public bool IsAdded {
      get { return isAdded; }
    }

    public PropertyModelView View {
      get { return view; }
    }
  }
}