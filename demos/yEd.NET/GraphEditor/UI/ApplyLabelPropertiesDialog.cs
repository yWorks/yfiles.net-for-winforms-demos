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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Demo.yFiles.GraphEditor.UI
{

  public partial class ApplyLabelPropertiesDialog : Form
  {
    public bool ApplyNodeLabelConfiguration {
      get { return applyNodeLabelConfiguration; }
      set { applyNodeLabelConfiguration = value; }
    }
    
    public bool ApplyGroupNodeLabelConfiguration {
      get { return applyGroupNodeLabelConfiguration; }
      set { applyGroupNodeLabelConfiguration = value; }
    }

    public bool ApplyEdgeLabelConfiguration {
      get { return applyEdgeLabelConfiguration; }
      set { applyEdgeLabelConfiguration = value; }
    }

    public bool ApplyStyle {
      get { return applyStyle; }
      set { applyStyle = value; }
    }

    public bool ApplyLabelModel {
      get { return applyLabelModel; }
      set { applyLabelModel = value; }
    }

    private bool applyNodeLabelConfiguration = false;
    private bool applyGroupNodeLabelConfiguration = false;
    private bool applyEdgeLabelConfiguration = false;
    private bool applyStyle = false;
    private bool applyLabelModel = false;

    public ApplyLabelPropertiesDialog() {
      InitializeComponent();      
    }

    private void nodeLabelsCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyNodeLabelConfiguration = nodeLabelsCheckBox.Checked;
    }

    private void edgeLabelsCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyEdgeLabelConfiguration = edgeLabelsCheckBox.Checked;
    }

    private void styleCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyStyle = styleCheckBox.Checked;
    }

    private void modelCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyLabelModel = modelCheckBox.Checked;
    }

    private void okButton_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e) {
      this.Close();
    }


    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      nodeLabelsCheckBox.Checked = ApplyNodeLabelConfiguration;
      edgeLabelsCheckBox.Checked = ApplyEdgeLabelConfiguration;
      groupNodeLabelsCheckbox.Checked = ApplyGroupNodeLabelConfiguration;
      styleCheckBox.Checked = ApplyStyle;
      modelCheckBox.Checked = ApplyLabelModel;
    }

    private void groupNodeLabelsCheckbox_CheckedChanged(object sender, EventArgs e) {
      ApplyGroupNodeLabelConfiguration = groupNodeLabelsCheckbox.Checked;
    }
  }
}