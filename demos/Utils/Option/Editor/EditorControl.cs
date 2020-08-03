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
using System.Windows.Forms;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.I18N;
using Demo.yFiles.Option.View;
using yWorks.Graph;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Base class for <see cref="UserControl"/>s that create a GUI for
  /// an <see cref="OptionHandler"/>.
  /// </summary>
  public abstract partial class EditorControl : UserControl
  {
    private IModelView view;
    private Guid _id;

    /// <summary>
    /// Create a new instance that is bound to an <see cref="IModelView"/> abstraction
    /// of an option handler.
    /// </summary>
    /// <param name="view"><see cref="IModelView"/> abstraction
    /// of an option handler</param>
    protected EditorControl(IModelView view) {
      _id = Guid.NewGuid();
      this.view = view;
      view.AddEditorControl(this);
      InitializeComponent();
    }

    //no outside access to the view
    internal virtual IModelView View {
      get { return view; }
    }

    internal virtual Guid ID {
      get { return _id; }
    }

    /// <summary>
    /// Controls the synchronization mode of this control for
    /// writing back the values to the OptionHandler.
    /// </summary>
    public virtual bool IsAutoCommit {
      get { return View.IsAutoCommit; }
      set { View.IsAutoCommit = value; }
    }

    /// <summary>
    /// Controls the synchronization mode of this control for
    /// external changes.
    /// </summary>
    public virtual bool IsAutoAdopt {
      get { return View.IsAutoAdopt; }
      set { View.IsAutoAdopt = value; }
    }    

    /// <summary>
    /// Write back all values to the underlying OptionHandler
    /// </summary>
    public virtual void CommitValues() {
      View.CommitValues();
    }

    /// <summary>
    /// Write back all values to the underlying OptionHandler
    /// </summary>
    public virtual void ResetValues() {
      View.ResetValues();
    }
    
    /// <summary>
    /// Get all values to the underlying OptionHandler
    /// </summary>
    public virtual void AdoptValues() {
      View.AdoptValues();
    }

    /// <summary>
    /// Returns the <see cref="I18N.I18NFactory"/> that is currently
    /// in effect for the underlying handler
    /// </summary>
    public I18NFactory I18NFactory {
      get { return View.Lookup<I18NFactory>(); }
    }

    /// <summary>
    /// Gets the title for this Control, which may appear as the window title
    /// </summary>
    /// <remarks>This returns the (possibly localized) name of the underlying OptionHandler.</remarks>
    public virtual string Title {
      get {
        if (I18NFactory != null) {
          return I18NFactory.GetString(view.Handler.Name, view.Handler.Name);
        } else {
          return view.Handler.Name;
        }
      }
    }
  }
}