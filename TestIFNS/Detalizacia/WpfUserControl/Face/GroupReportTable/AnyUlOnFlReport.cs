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


namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.GroupReportTable
{
   public class AnyUlOnFlReport
    {
       
        public  XLWorkbook GenereteReport(DataSet dataset, XLWorkbook reportxlx, Collections.ColectionTab.TabControl tab)
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
                tab.ShemedTabItems.Add(new TabItem {Header = new Label { Content = dataTable.TableName, Background = Brushes.AntiqueWhite, Foreground = Brushes.Red, FontSize = 14}, BorderBrush =Brushes.AntiqueWhite, Content = new DataGrid()
                { ItemsSource = dataTable.DefaultView,
                    Background =new System.Windows.Media.LinearGradientBrush
                    { StartPoint = new Point(0.5,0), EndPoint = new Point(0.5,1), GradientStops =new GradientStopCollection
                    { new GradientStop { Offset = 0, Color = Colors.AntiqueWhite},
                      new GradientStop { Offset = 1, Color = Colors.Aquamarine } } }} }  );
                var worksheet = reportxlx.Worksheets.Add(dataTable.TableName);
                worksheet.Cell("A1").InsertTable(dataTable).Worksheet.Columns().AdjustToContents();
            }
            return reportxlx;
        }
    }
}
