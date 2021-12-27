/****************************************************************************
 ** 
 ** This demo file is part of yFiles.NET 5.4.
 ** Copyright (c) 2000-2021 by yWorks GmbH, Vor dem Kreuzberg 28,
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
  internal partial class DialogEditorControl : EditorControl, IStatusChangeSupport
  {
    #region private members

    #region global controls

    #endregion

    private BindingList<PropertyModelView> _registeredViews = new BindingList<PropertyModelView>();
    private Control sectionControl;

    private IDictionary<Guid, Control> topLevelControlMap =
      new Dictionary<Guid, Control>();

    private int minWidth = 0;
    private int minHeight = 0;

    #endregion

    public DialogEditorControl(IModelView view, bool multiLine) : base(view) {
      this.multiLine = multiLine;
      RegisterView();
      InitializeComponent();
    }

    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private readonly System.ComponentModel.IContainer components = null;

    private Control topControl;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      View.StatusChanged -= view_StatusChanged;
      View.PostContentChange -= view_ContentChanged;
      topLevelControlMap.Clear();
      _registeredViews.Clear();
      base.Dispose(disposing);
    }

    private void RegisterView() {
      PropertyModelView propertyModelView = View as PropertyModelView;
      if (propertyModelView != null) {
        _registeredViews.Add(propertyModelView);
        View.StatusChanged += view_StatusChanged;
        View.PostContentChange += view_ContentChanged;
      }
    }

    private void view_ContentChanged(object source, StructureChangeEventArgs e) {
      this.SuspendLayout();
      sectionControl.Dispose();
      this.Controls.Remove(sectionControl);
      topLevelControlMap.Clear();
      CreateEditors();
      this.ResumeLayout(true);
    }

    private void view_StatusChanged(object source, ItemStatusEventArgs statusInformation) {
      if (statusInformation.EnabledChanged) {
        if (topLevelControlMap.ContainsKey((Guid) source)) {
          Control c = topLevelControlMap[(Guid) source];
          c.Enabled = statusInformation.Enabled;
        }
        if (StatusChanged != null) {
          //propagate to subscribers
          StatusChanged(source, statusInformation);
        }
      }
    }

    public event ItemStatusHandler StatusChanged;


    public override DockStyle Dock {
      get { return base.Dock; }
      set {
        base.Dock = value;
        if (topControl != null) {
          topControl.Dock = value;
          topControl.PerformLayout();
        }
      }
    }

    private bool multiLine;

    #region private helper methods

    private void CreateEditors() {
      SuspendLayout();
      PropertyModelView propertyModelView = _registeredViews[0];
      int nonemptyGroups = 0;

      foreach (PropertyDescriptor property in propertyModelView.GetProperties()) {
        if (property.GetChildProperties().Count > 0) {
          ++nonemptyGroups;
        }
      }

      if (nonemptyGroups < 2 || propertyModelView.SectionCount < 2) {
        bool drawBox = false;
        PropertyDescriptorCollection properties = propertyModelView.GetProperties();
        DialogSectionControl clientControl =
          new DialogSectionControl(this, properties, drawBox, this.Title);
        topLevelControlMap[View.Handler.ID] = clientControl;
        this.Controls.Add(clientControl);
        sectionControl = clientControl;
        minHeight = clientControl.PreferredSize.Height;
        minWidth = clientControl.PreferredSize.Width;
        topControl = clientControl;
      } else {
        TabControl multipleCategoryTabControl = new TabControl(){ Multiline = multiLine };
        if(multiLine) {
          multipleCategoryTabControl.SizeMode = TabSizeMode.FillToRight;
        }
        Controls.Add(multipleCategoryTabControl);
        sectionControl = multipleCategoryTabControl;
        PropertyDescriptorCollection properties = propertyModelView.GetProperties();
        foreach (PropertyDescriptor property in properties) {
          if (property.GetChildProperties().Count == 0) {
            continue;
          }
          string category = property.Category;
          string name = category.TrimStart('\r');
          TabPage page = new TabPage(name);
          page.Name = name;
          DialogSectionControl clientControl =
            new DialogSectionControl(this, property.GetChildProperties(), property, false);
          page.Controls.Add(clientControl);
          topLevelControlMap[
            ((PropertyModelView.OptionItemPropertyDescriptor)
             ((PropertyModelView.OptionItemPropertyDescriptor) property).Owner).ID] = clientControl;
          page.Dock = DockStyle.Fill;
          multipleCategoryTabControl.TabPages.Add(page);
          minHeight = Math.Max(minHeight, clientControl.Height);
          minWidth = Math.Max(minWidth, clientControl.PreferredSize.Width);
        }

        minWidth += 3;
        int tabSize = 0;
        if (multiLine) {
          for (int i = 0; i < multipleCategoryTabControl.TabCount; ++i) {
            var rect = multipleCategoryTabControl.GetTabRect(i);
            tabSize = Math.Max(tabSize, rect.Location.Y);
          }
        } else {
          tabSize = multipleCategoryTabControl.ItemSize.Height;
        }
        minHeight += tabSize + multipleCategoryTabControl.Margin.Vertical;

        multipleCategoryTabControl.Width = minWidth;
        multipleCategoryTabControl.Height = minHeight;
        topControl = multipleCategoryTabControl;
        multipleCategoryTabControl.Dock = DockStyle.Fill;
      }

      ResumeLayout(false);
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


//    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
//      base.SetBoundsCore(x, y, width, height, specified);
//    }

    #endregion
  }
}