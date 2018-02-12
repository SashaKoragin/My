using System;
using System.IO;
using System.Text;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.AddItem;
using OfficeOpenXml;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.Convert
{
    class Convettxt
    {
        public ExcelWorksheet ConvtxtFull(ZnachView path,ExcelPackage excel)
        {
            int i = 0;
            using (var st = new StreamReader(path.Pathfile,Encoding.Default))
            {
                    var worksheet = excel.Workbook.Worksheets.Add(path.Namefile);
                    while (!st.EndOfStream)
                    {
                        var readLine = st.ReadLine();
                        if (readLine != "")
                        {
                            i++;
                            worksheet.Cells[String.Format("A{0}", i)].Value = readLine;
                        }
                    }
                worksheet.Cells.AutoFitColumns();
                st.Dispose();
                return worksheet;
            }
        }

        public ExcelWorksheet ConvtxtScalar(ZnachView path, ExcelPackage excel, String scalare)
        {
            var i = 0;
            var scal = System.Convert.ToChar(scalare);
            using (var st = new StreamReader(path.Pathfile, Encoding.Default))
            {
                var worksheet = excel.Workbook.Worksheets.Add(path.Namefile);
       
                while (!st.EndOfStream)
                {
                    var readLine = st.ReadLine();
                    if (readLine != null)
                    {
                        i++;
                        
                        worksheet.Cells[String.Format("A{0}", i)].Value = readLine;
                        foreach (var cell in worksheet.Cells[String.Format("A{0}", i)])
                        {
                            var splittedValues = ((String)cell.Value).Split(scal);
                            cell.Value = splittedValues[0];
                            for (var j = 0; j < splittedValues.Length; j++)
                            {
                                worksheet.Cells[cell.Start.Row, cell.Start.Column + j].Value = splittedValues[j];
                            }
                        }
                    }
                }
                worksheet.Cells.AutoFitColumns();
                st.Dispose();
                return worksheet;
            }
        }
    }
}
