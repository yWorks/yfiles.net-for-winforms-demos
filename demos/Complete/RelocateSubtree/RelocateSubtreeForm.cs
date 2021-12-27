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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Graph;
using yWorks.Graph.Styles;
using yWorks.Layout.Partial;

namespace Demo.yFiles.Layout.RelocateSubtree
{
  /// <summary>
  /// A demo that shows how to interactively relocate subtrees from one parent to another using
  /// <see cref="ClearAreaLayout"/> to make space at the desired location.
  /// </summary>
  public partial class RelocateSubtreeForm : Form
  {
    /// <summary>
    /// Returns the GraphControl instance used in the form.
    /// </summary>
    private GraphControl GraphControl {
      get { return graphControl; }
    }

    /// <summary>
    /// Gets the currently registered IGraph instance from the GraphControl.
    /// </summary>
    private IGraph Graph {
      get { return GraphControl.Graph; }
    }
    
    public RelocateSubtreeForm() {
      InitializeComponent();
      // load description
      description.LoadFile(new MemoryStream(Properties.Resources.description), RichTextBoxStreamType.RichText);

      // set commands to buttons and menu items
      zoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      zoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl); 
      zoomFitButton.SetCommand(Commands.FitContent, graphControl);
    }

    private void OnLoad(object sender, EventArgs e) {
      InitializeInputModes();
      InitializeGraph();
    }

    /// <summary>
    /// Registers the <see cref="GraphEditorInputMode"/> as the <see cref="CanvasControl.InputMode"/>
    /// and initializes an <see cref="IPositionHandler"/> that moves a node and its subtree.
    /// </summary>
    private void InitializeInputModes() {
      // create a GraphEditorInputMode instance
      var editMode = new GraphEditorInputMode {
          MoveUnselectedInputMode = { Enabled = true },
          MovableItems = GraphItemTypes.Node,
          SelectableItems = GraphItemTypes.None,
          AllowCreateBend = false,
      };

      // use special position handler to moves a node and its subtree
      Graph.GetDecorator().NodeDecorator.PositionHandlerDecorator
           .SetImplementationWrapper((node, handler) => new SubtreePositionHandler(node, handler));

      // and install the edit mode into the canvas
      GraphControl.InputMode = editMode;
    }

    /// <summary>
    /// Initializes styles and loads a sample graph.
    /// </summary>
    protected virtual void InitializeGraph() {
      GraphControl.Graph.NodeDefaults.Style = new ShinyPlateNodeStyle {Brush = Brushes.Orange};
      GraphControl.ImportFromGraphML("Resources\\tree.graphml");
    }
        
    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new RelocateSubtreeForm());
    }

    #endregion

  }
}
