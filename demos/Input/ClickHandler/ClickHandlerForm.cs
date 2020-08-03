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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Demo.yFiles.Graph.Input.ClickHandler.Properties;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.Input.ClickHandler
{
  /// <summary>
  /// Shows how to augment <see cref="IClickHandler"/> with additional functionality.
  /// </summary>
  /// <remarks>
  /// See the description.rtf file or run the application to find out about what this application demonstrates.
  /// </remarks>
  public partial class ClickHandlerForm : Form
  {
    #region Initialization

    public ClickHandlerForm() {
      InitializeComponent();

      // Initialize input mode
      graphControl.InputMode = CreateInputMode();

      // Create sample graph
      graphControl.Graph = CreateSampleGraph();

      // Load description
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
    }

    private void ClickHandlerForm_Load(object sender, EventArgs e) {
      graphControl.FitGraphBounds();
    }

    /// <summary>
    /// Creates a viewer input mode with a custom additional input mode that
    /// handles cursor changes for  <see cref="IClickHandler"/>s.
    /// </summary>
    private static IInputMode CreateInputMode() {
      var gvim = new GraphViewerInputMode {
        SelectableItems = GraphItemTypes.None,
        FocusableItems = GraphItemTypes.None
      };

      // Add our own input mode that handles cursor changes
      var clickHandlerMode = new ClickHandlerHoverInputMode();
      gvim.Add(clickHandlerMode);

      return gvim;
    }

    #endregion

    #region Sample graph

    public IGraph CreateSampleGraph() {
      var graph = new DefaultGraph();

      // Create a few nodes of different height. Some of them show disabled buttons.
      var expandStyle = new GrowShrinkButtonNodeStyleDecorator(new ShinyPlateNodeStyle { Brush = Brushes.DarkOrange });
      graph.CreateNode(new RectD(0, 0, 80, 40), expandStyle);
      graph.CreateNode(new RectD(120, 0, 80, 100), expandStyle);
      graph.CreateNode(new RectD(240, 0, 80, 150), expandStyle);

      return graph;
    }

    #endregion

    #region Application Start

    /// <summary>
    /// The main entry point for the demo.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new ClickHandlerForm());
    }

    #endregion

  }
}
