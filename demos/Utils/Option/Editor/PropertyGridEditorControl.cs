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
using System.Drawing;
using System.Windows.Forms;
using Demo.yFiles.Option.Handler;
using Demo.yFiles.Option.View;

namespace Demo.yFiles.Option.Editor
{
  internal partial class PropertyGridEditorControl : EditorControl
  {
    private bool inSortUpdate = false;

    public bool ToolbarVisible {
      get { return propertyGrid1.ToolbarVisible; }
      set { propertyGrid1.ToolbarVisible = value; }
    }

    private IDictionary<string, bool> collapseState = new Dictionary<string, bool>();

    internal PropertyGridEditorControl(IModelView view)
      : base(view) {
      InitializeComponent();
      propertyGrid1.UseCompatibleTextRendering = true;
//      propertyGrid1.ToolbarVisible = false;
      propertyGrid1.PropertySort = PropertySort.Categorized;
      propertyGrid1.SelectedObjects = new IModelView[] {View};
      propertyGrid1.PropertySortChanged += propertyGrid1_PropertySortChanged;
      View.PropertyChanged += view_PropertyChanged;
      View.StatusChanged += view_StatusChanged;
      View.PreContentChange += view_preContentChanged;
      View.PostContentChange += view_postContentChanged;
      }

    private void propertyGrid1_PropertySortChanged(object sender, EventArgs e) {
      PropertySort currentOrder = propertyGrid1.PropertySort;
      if (!inSortUpdate && currentOrder == PropertySort.CategorizedAlphabetical) {
        inSortUpdate = true;
        propertyGrid1.PropertySort = PropertySort.Categorized;
        inSortUpdate = false;
      }
    }


    private void view_postContentChanged(object source, StructureChangeEventArgs e) {
      

      propertyGrid1.SelectedObjects = new IModelView[] {View};
//      propertyGrid1.ExpandAllGridItems();

      GridItem gi = propertyGrid1.SelectedGridItem;
      while (gi.GridItemType != GridItemType.Root) {
        gi = gi.Parent;
      }
      
      CollapseChildren(gi);
    }     
    
    private void view_preContentChanged(object source, StructureChangeEventArgs e) {
      GridItem gi = propertyGrid1.SelectedGridItem;
      while (gi.GridItemType != GridItemType.Root) {
        try {
          gi = gi.Parent;
        }
        catch(ObjectDisposedException) {
          //this means that we have an item which has been built on the fly as a result from a type converter
          //in this case, we just don't save the state...
          return;
        }
      }
      SaveCollapsedState(gi);
    }

    private void SaveCollapsedState(GridItem gi) {
      if (gi == null) {
        return;
      }      
      foreach (GridItem currentItem in gi.GridItems) {
        PropertyDescriptor desc = currentItem.PropertyDescriptor;
        PropertyModelView.OptionItemPropertyDescriptor item = desc as PropertyModelView.OptionItemPropertyDescriptor;
        if (item != null) {
          collapseState[item.FullyQualifiedName] = currentItem.Expanded;
          //scan all children
          
        }
        SaveCollapsedState(currentItem);
      }
    }

    private void CollapseChildren(GridItem gi) {
      if(gi == null) {
        return; 
      }
      foreach(GridItem currentItem in gi.GridItems) {
        PropertyDescriptor desc = currentItem.PropertyDescriptor;
        PropertyModelView.OptionItemPropertyDescriptor item = desc as PropertyModelView.OptionItemPropertyDescriptor;
        if (item != null) {
          bool oldState;
          bool existed = collapseState.TryGetValue(item.FullyQualifiedName, out oldState);
          if(existed) {
            currentItem.Expanded = oldState;
            CollapseChildren(currentItem);
          }
          else {
            object attr = item.GetAttribute(TableEditorFactory.RENDERING_HINTS_ATTRIBUTE);
            if (attr != null) {
              TableEditorFactory.RenderingHints attrValue = (TableEditorFactory.RenderingHints)attr;
              if ((attrValue & TableEditorFactory.RenderingHints.Collapsed) == TableEditorFactory.RenderingHints.Collapsed) {
                currentItem.Expanded = false;
              } else {
                currentItem.Expanded = true;
                //scan all children
                CollapseChildren(currentItem);
              }
            } else {
              currentItem.Expanded = true;
              //scan all children
              CollapseChildren(currentItem);
            }
          }

        } else {
          currentItem.Expanded = true;
          //scan all children
          CollapseChildren(currentItem);
        }
      }
//      if(gi.GridItems.Count > 0) {
//        collapseState.Clear();
//      }
    }

    private void view_ContentChange(object source, StructureChangeEventArgs e) {
      propertyGrid1.SelectedObjects = new object[] {};
      propertyGrid1.ResetBindings();
    }

    private void view_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      propertyGrid1.Refresh();
//      propertyGrid1.ExpandAllGridItems();
    }

    private void view_StatusChanged(object source, ItemStatusEventArgs statusInformation) {
      if (!statusInformation.IndirectChange && statusInformation.EnabledChanged) {
//        propertyGrid1.Refresh();
//        propertyGrid1.ExpandAllGridItems();
      } else if (statusInformation.HiddenChanged) {
        propertyGrid1.SelectedObjects = new IModelView[] {View};
      }
    }

    public override Size GetPreferredSize(Size proposedSize) {
      PropertyModelView propertyModelView = View as PropertyModelView;

      if (propertyModelView != null) {
        int height = (propertyModelView.GetProperties().Count + 
                      propertyModelView.SectionCount + 2) * 20 +
                     propertyGrid1.GetPreferredSize(proposedSize).Height;

        return new Size(propertyGrid1.GetPreferredSize(proposedSize).Width, height);
      }
      return new Size(propertyGrid1.GetPreferredSize(proposedSize).Width, 
                      propertyGrid1.GetPreferredSize(proposedSize).Height);
    }
  }

#if DEBUG  
  internal class InstrumentedPropertyGrid : PropertyGrid {}
#endif  
}