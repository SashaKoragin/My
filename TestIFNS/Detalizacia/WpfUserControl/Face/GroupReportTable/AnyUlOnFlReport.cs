using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ClosedXML.Excel;
using GalaSoft.MvvmLight.Threading;


namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.GroupReportTable
{
   public class AnyUlOnFlReport
    {
        public DataSet Generatexsls(DataSet dataset, XLWorkbook reportxlx, string namesave)
        {
            foreach (DataTable dataTable in dataset.Tables)
            {
             DataColumn[] stringColumns = dataTable.Columns.Cast<DataColumn>().Where(c => c.DataType == typeof(string)).ToArray();
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn col in stringColumns)
                    {
                        if (row[col] != DBNull.Value)
                        {
                            row.SetField(col, row.Field<string>(col).Trim());
                        }
                    }
                }
           var worksheet = reportxlx.Worksheets.Add(dataTable.TableName);
           worksheet.Cell("A1").InsertTable(dataTable).Worksheet.Columns().AdjustToContents();
           }
        reportxlx.SaveAs(namesave + ".xlsx");
        return dataset;
        }

        public void GenereteReport(DataSet dataset, Collections.ColectionTab.TabControl tab)
        {
                    try
                    {
                        foreach (DataTable dataTable in dataset.Tables)
                        {
                            lock (tab._lock)
                            {
                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                {
                                    tab.ShemedTabItems.Add(new TabItem
                                    {
                                        Header =
                                            new Label
                                            {
                                                Content = dataTable.TableName,
                                                Background = Brushes.AntiqueWhite,
                                                Foreground = Brushes.Red,
                                                FontSize = 14
                                            },
                                        BorderBrush = Brushes.AntiqueWhite,
                                        Content = new DataGrid()
                                        {
                                            ItemsSource = dataTable.DefaultView,
                                            Background = new System.Windows.Media.LinearGradientBrush
                                            {
                                                StartPoint = new Point(0.5, 0),
                                                EndPoint = new Point(0.5, 1),
                                                GradientStops = new GradientStopCollection
                                                {
                                                    new GradientStop {Offset = 0, Color = Colors.AntiqueWhite},
                                                    new GradientStop {Offset = 1, Color = Colors.Aquamarine}
                                                }
                                            }
                                        }
                                    });
                                });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
        }
    }
}

