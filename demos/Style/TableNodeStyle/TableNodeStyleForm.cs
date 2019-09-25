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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Demo.yFiles.Graph.TableNodeStyle.Properties;
using Demo.yFiles.Graph.TableNodeStyle.Style;
using yWorks.Controls;
using yWorks.Controls.Input;
using yWorks.Geometry;
using yWorks.Graph;
using yWorks.Graph.LabelModels;
using yWorks.Graph.Styles;

namespace Demo.yFiles.Graph.TableNodeStyle
{
  /// <summary>
  /// Demo that shows how to create custom visualizations for table styles.
  /// </summary>
  public partial class TableNodeStyleForm : Form
  {
    public TableNodeStyleForm() {
      InitializeComponent();
      graphControl.InputMode = new GraphViewerInputMode();
    }

    public IGraph Graph {
      get { return graphControl.Graph; }
    }

    /// <summary>
    /// Called upon loading of the form.
    /// This method initializes the graph and the input mode.
    /// </summary>
    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      description.LoadFile(new MemoryStream(Resources.description), RichTextBoxStreamType.RichText);
      // initialize the graph
      Graph.SetUndoEngineEnabled(true);
      graphControl.FileOperationsEnabled = true;
      ZoomInButton.SetCommand(Commands.IncreaseZoom, graphControl);
      ZoomOutButton.SetCommand(Commands.DecreaseZoom, graphControl);
      FitContentButton.SetCommand(Commands.FitContent, graphControl);

      SaveMenuItem.SetCommand(Commands.SaveAs, graphControl);
      OpenMenuItem.SetCommand(Commands.Open, graphControl);
      sampleFilesComboBox.SelectedIndex = 0;
    }


     private void sampleFilesComboBox_SelectedIndexChanged(object sender, EventArgs e) {
        switch (sampleFilesComboBox.SelectedIndex) {
          case 0:
            CreateBPMNSampleTable();
            break;
          case 1:
            CreateAlternatingSampleTable();
            break;
          case 2:
            CreateSimpleNodeStyleSampleTable();
            break;
        }
      }


    /// <summary>
    /// Creates a sample file that uses an alternating stripe style
    /// </summary>
    private void CreateAlternatingSampleTable() {
      var table = new Table {Insets = new InsetsD(5, 30, 5, 5)};
      //Declare the defaults for the columns
      table.ColumnDefaults.MinimumSize = table.ColumnDefaults.Size = 200;
      var defaultLabelStyle = new DefaultLabelStyle {
        StringFormat = {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center}
      };
      table.ColumnDefaults.Labels.Style = defaultLabelStyle;
      table.ColumnDefaults.Insets = new InsetsD(5, 30, 5, 0);
      table.ColumnDefaults.Style = new AlternatingStripeStyle
                                     {
                                       EvenStripeDescriptor =
                                         new StripeDescriptor
                                           {
                                             BackgroundBrush = new SolidBrush(Color.FromArgb(255, 153, 164, 187)),
                                             InsetBrush = new SolidBrush(Color.FromArgb(255, 204, 196, 168))
                                           },
                                       OddStripeDescriptor =
                                         new StripeDescriptor
                                           {
                                             BackgroundBrush = new SolidBrush(Color.FromArgb(255, 139, 162, 220)),
                                             InsetBrush = new SolidBrush(Color.FromArgb(255, 186, 194, 212))
                                           }
                                     };
      for (int i = 0; i < 4; ++i) {
        //Create four columns
        IColumn column = table.CreateColumn();
        table.AddLabel(column, "Lane " + i);
      }
      //Create a single row
      table.CreateRow(table.RootRow, 300, null, InsetsD.Empty, new NodeStyleStripeStyleAdapter(new ShapeNodeStyle {Brush = Brushes.Transparent}));

      //Create a single node and bind the table to this node
      Graph.Clear();
      Graph.CreateGroupNode(null, table.Layout.ToRectD(),
        new yWorks.Graph.Styles.TableNodeStyle(table) {
          BackgroundStyle =
              new ShapeNodeStyle { Brush = new SolidBrush(Color.FromArgb(255, 248, 236, 201)) }
        });
      graphControl.FitGraphBounds();
    }

    /// <summary>
    /// Creates a sample that uses an BPMN stripe style
    /// </summary>
    /// <remarks>Since this is a rather complex graph which really needs an automatic layout, load it from a graphml resource instead.</remarks>
    private void CreateBPMNSampleTable() {
      graphControl.ImportFromGraphML("Resources\\BPMN-Style.graphml");
    }

    /// <summary>
    /// Creates a sample graph that uses a simple custom style for the stripes
    /// </summary>
    /// <remarks>This sample also shows how to created nested stripes</remarks>
    private void CreateSimpleNodeStyleSampleTable() {
      var table = new Table {Insets = new InsetsD(0, 30, 0, 0)};
      //Declare the defaults for the columns
      table.ColumnDefaults.MinimumSize = table.ColumnDefaults.Size = 500;
      table.ColumnDefaults.Insets = new InsetsD(5, 30, 5, 30);
      var defaultLabelStyle = new DefaultLabelStyle {
        StringFormat = { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
      };
      table.ColumnDefaults.Labels.Style = defaultLabelStyle;
      table.ColumnDefaults.Style = new NodeStyleStripeStyleAdapter(new ShapeNodeStyle{Shape = ShapeNodeShape.Rectangle, Pen = Pens.Black, Brush = Brushes.Transparent});

      for (int i = 0; i < 2; ++i) {
        //Create three columns
        IColumn column = table.CreateColumn();
        table.AddLabel(column, "Milestone " + i);
      }
      //Declare the defaults for the rows
      table.RowDefaults.MinimumSize = 50;
      table.RowDefaults.Insets = new InsetsD(30, 0, 0, 0);
      table.RowDefaults.Labels.Style = defaultLabelStyle;
      table.RowDefaults.Style = new NodeStyleStripeStyleAdapter(new ShapeNodeStyle { Shape = ShapeNodeShape.Rectangle, Pen = Pens.Black, Brush = Brushes.LightBlue });
      var nestedStyle1 = new NodeStyleStripeStyleAdapter(new ShapeNodeStyle { Shape = ShapeNodeShape.Rectangle, Pen = Pens.Black, Brush = Brushes.LightCyan });
      var rootRowStyle = new NodeStyleStripeStyleAdapter(new ShapeNodeStyle { Shape = ShapeNodeShape.Rectangle, Pen = Pens.Black, Brush = Brushes.LightCyan });

      IRow row_1 = table.CreateRow();
      table.SetStripeInsets(row_1, new InsetsD(30, 30, 0, 30));
      table.AddLabel(row_1, "Lane 1");
      //Nested rows for the first lane, level 1
      IRow row_1_1 = table.CreateRow(row_1, 150);
      table.SetStyle(row_1_1, nestedStyle1);
      table.AddLabel(row_1_1, "Lane 1.1");

      //Nested rows for the first lane, level 2
      IRow row_1_1_1 = table.CreateRow(row_1_1, 150);
      table.AddLabel(row_1_1_1, "Lane 1.1.1");

      IRow row_1_1_2 = table.CreateRow(row_1_1, 70);
      table.AddLabel(row_1_1_2, "Lane 1.1.2");

      IRow row_1_1_3 = table.CreateRow(row_1_1, 70);
      table.AddLabel(row_1_1_3, "Lane 1.1.3");

      //Another nested row on the first level
      IRow row_1_2 = table.CreateRow(row_1, 200);
      table.AddLabel(row_1_2, "Lane 1.2");

      IRow row_2 = table.CreateRow();
      table.SetStyle(row_2, rootRowStyle);
      table.AddLabel(row_2, "Lane 2");
      //Another nested row on the first level
      IRow row_2_1 = table.CreateRow(row_2, 150);
      table.AddLabel(row_2_1, "Lane 2.2");
      //Another nested row on the first level
      IRow row_2_2 = table.CreateRow(row_2, 150);
      table.AddLabel(row_2_2, "Lane 2.2");

      //Create a single node and bind the table to this node
      Graph.Clear();
      var node = Graph.CreateGroupNode(null, table.Layout.ToRectD(),
                                    new yWorks.Graph.Styles.TableNodeStyle(table)
                                      {
                                        BackgroundStyle =
                                          new ShapeNodeStyle {Brush = new SolidBrush(Color.FromArgb(236, 245, 255))},
                                        TableRenderingOrder = TableRenderingOrder.RowsFirst
                                      });
      Graph.AddLabel(node, "Pool 1", InteriorLabelModel.North);
      graphControl.FitGraphBounds();
    }

    /// <summary>
    /// Exit the demo.
    /// </summary>
    private void ExitMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }


    #region Main

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.Run(new TableNodeStyleForm());
    }

    #endregion

  }
}
