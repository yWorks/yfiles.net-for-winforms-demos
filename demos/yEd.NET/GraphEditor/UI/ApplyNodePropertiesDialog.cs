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
using System.Windows.Forms;

namespace Demo.yFiles.GraphEditor.UI
{
  public partial class ApplyNodePropertiesDialog : Form
  {
    public bool ApplyStyleToNormalNodes {
      get { return applyLabelStyleToNormalNodes; }
      set { applyLabelStyleToNormalNodes = value; }
    }

    public bool ApplyStyleToGroupNodes {
      get { return applyLabelStyleToGroupNodes; }
      set { applyLabelStyleToGroupNodes = value; }
    }

    public bool ApplyStyle {
      get { return applyStyle; }
      set { applyStyle = value; }
    }

    public bool ApplyLabelConfiguration {
      get { return applyLabelConfiguration; }
      set { applyLabelConfiguration = value; }
    }

    public bool ApplySize {
      get { return applySize; }
      set { applySize = value; }
    }

    private bool applyLabelStyleToNormalNodes = false;
    private bool applyLabelStyleToGroupNodes = false;
    private bool applyLabelConfiguration = false;
    private bool applyStyle = false;
    private bool applySize = false;

    public ApplyNodePropertiesDialog() {
      InitializeComponent();
    }

    private void nodeLabelsCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyStyleToNormalNodes = normalNodesCheckBox.Checked;
    }

    private void edgeLabelsCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyStyleToGroupNodes = groupNodesCheckBox.Checked;
    }

    private void styleCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplyStyle = styleCheckBox.Checked;
    }

    private void modelCheckBox_CheckedChanged(object sender, EventArgs e) {
      ApplySize = geometryCheckBox.Checked;
    }

    private void okButton_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e) {
      this.Close();
    }


    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      normalNodesCheckBox.Checked = ApplyStyleToNormalNodes;
      groupNodesCheckBox.Checked = ApplyStyleToGroupNodes;
      labelConfigurationCheckbox.Checked = ApplyLabelConfiguration;
      styleCheckBox.Checked = ApplyStyle;
      geometryCheckBox.Checked = ApplySize;
    }

    private void labelConfigurationCheckbox_CheckedChanged(object sender, EventArgs e) {
      ApplyLabelConfiguration = labelConfigurationCheckbox.Checked;
    }
  }
}