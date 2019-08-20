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
using System.ComponentModel;
using System.Windows.Forms;
using Demo.yFiles.Option.Handler;
using System.Reflection;

namespace Demo.yFiles.Option.Editor
{
  /// <summary>
  /// Interface for controls that can be used in a control created by <see cref="DefaultEditorFactory"/>.
  /// </summary>
  /// <remarks>This specifies a well known <see cref="Value"/> property where the
  /// dialog can automatically bind to.</remarks>
  public interface IDialogItemControl: INotifyPropertyChanged
  {
    /// <summary>
    /// Gets or sets the value that is currently contained in the control.
    /// </summary>
    object Value { get; set; }
  }

  internal sealed class CheckBoxWrapper : CheckBox, IDialogItemControl
  {
    private object _value;
    private bool inUpdate = false;

    [ObfuscationAttribute(Exclude = true, StripAfterObfuscation = false)]
    public object Value {
      get { return _value; }
      set {
        _value = value;
        inUpdate = true;
        if (value == OptionItem.VALUE_UNDEFINED) {
          this.ThreeState = true;
          CheckState = CheckState.Indeterminate;
        }
        if (ThreeState) {
          if (value == OptionItem.VALUE_UNDEFINED) {
//                        CheckState = CheckState.Indeterminate;
          } else {
            if ((bool) value) {
              CheckState = CheckState.Checked;
            } else {
              CheckState = CheckState.Unchecked;
            }
          }
        } else {
          Checked = (bool) value;
        }
        inUpdate = false;
      }
    }

    private void SetState() {
      object newVal;
      if (ThreeState) {
        switch (CheckState) {
          case CheckState.Checked:
            newVal = true;
            break;
          case CheckState.Indeterminate:
            newVal = OptionItem.VALUE_UNDEFINED;
            break;
          case CheckState.Unchecked:
            newVal = false;
            break;
          default:
            newVal = null;
            break;
        }
      } else {
        newVal = Checked;
      }
      if (newVal != _value) {
        _value = newVal;

        ThreeState = false;
        if (PropertyChanged != null) {
          PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }
      }
    }

    ///<summary>
    ///Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged"></see> event.
    ///</summary>
    ///
    ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
    protected override void OnCheckedChanged(EventArgs e) {
      base.OnCheckedChanged(e);

      if (!inUpdate) {
        SetState();
      }
    }

    ///<summary>
    ///Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckStateChanged"></see> event.
    ///</summary>
    ///
    ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
    protected override void OnCheckStateChanged(EventArgs e) {
      base.OnCheckStateChanged(e);

      if (!inUpdate) {
        SetState();
      }
    }

    ///<summary>
    ///Occurs when a property value changes.
    ///</summary>
    ///
    public event PropertyChangedEventHandler PropertyChanged;
  }
}