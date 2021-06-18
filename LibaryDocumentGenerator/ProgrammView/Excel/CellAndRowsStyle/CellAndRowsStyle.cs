using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;

namespace LibaryDocumentGenerator.ProgrammView.Excel.CellAndRowsStyle
{
   public class CellAndRowsStyle
   {
        /// <summary>
        /// Индекс строки
        /// </summary>
       private UInt32Value IndexRow { get; set; } = 1;
       /// <summary>
       /// Массив букв
       /// </summary>
       /// <returns></returns>
       private IEnumerable<string> GetExcelStrings()
       {
           string[] alphabet = { string.Empty, "A", "B", "C", "D", "E", "F",
               "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
               "U", "V", "W", "X", "Y", "Z" };
           return from c1 in alphabet
               from c2 in alphabet
               from c3 in alphabet.Skip(1)                    //  c3 is never empty
               where c1 == string.Empty || c2 != string.Empty      //  only allow c2 to be empty if c1 is also empty
           select c1 +c2 + c3;
       }

       /// <summary>
       /// Генерация колонок Excel Матрица букв
       /// </summary>
       private List<List<string>> RowNameColumnsMatrix { get; } = new List<List<string>>();

        /// <summary>
        /// Генерация ячеек и строк
        /// </summary>
        /// <returns></returns>
        public void GenerateCell(List<ModelRowFormat> listCellModel, ref ModelXmlExcel model)
        {
            foreach (var modelRowFormat in listCellModel)
            {
                List<string> listArray = new List<string>();
                foreach (var modelCellFormat in modelRowFormat.ModelCell)
                {
                    var enumColumn = GetExcelStrings().Skip(modelCellFormat.IndexCellStart-1).Take(modelCellFormat.IndexCellFinish).ToArray();
                    foreach (var column in enumColumn)
                    {
                        Cell cell = new Cell() {CellReference = $"{column}{IndexRow}", DataType = CellValues.String};
                        listArray.Add($"{column}{IndexRow}");
                        var indexRow = enumColumn.ToList().IndexOf(column) + 1;
                        cell.StyleIndex = modelCellFormat.StyleIndex;
                        if (indexRow==1)
                        {
                            cell.DataType = modelCellFormat.CellFormat;
                            if (modelCellFormat.WidthColumn != null)
                            {
                                Column columnCell = new Column()
                                {
                                    Min = (UInt32)modelCellFormat.IndexCellStart,
                                    Max = (UInt32)modelCellFormat.IndexCellStart,
                                    Width = modelCellFormat.WidthColumn,
                                    CustomWidth = true
                                };
                                model.Сolumns.Append(columnCell);
                            }
                            CellValue cellValue = new CellValue();
                            cellValue.Text = modelCellFormat.ValueCell;
                            cell.Append(cellValue);
                        }
                        model.Cell.Add(cell);
                    }
                }
                Row row = new Row()
                {
                    RowIndex = IndexRow,
                    DyDescent = 0.25D,
                    Height = modelRowFormat.HeightRow,
                    CustomHeight = true,
                };
                foreach (var cell in model.Cell)
                {
                    row.Append(cell);
                }
                model.Cell.Clear();
                model.Row.Add(row);
                IndexRow++;
                RowNameColumnsMatrix.Add(listArray);
            }
        }

        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="listRowModel">Модель строк и ячеек</param>
        /// <param name="model">Модель xml</param>
        public void MergeCells(List<ModelRowFormat> listRowModel, ref ModelXmlExcel model)
        {
            var i = 0;
            foreach (var modelRowFormat in listRowModel)
            {
                foreach (var modelCellFormat in modelRowFormat.ModelCell)
                {
                    if (modelCellFormat.MergeHorizontalInt != 0)
                    {
                        var list = RowNameColumnsMatrix[i].Skip(modelCellFormat.IndexCellStart - 1).Take(modelCellFormat.MergeHorizontalInt).ToList();
                        var first = list.First();
                        var last = list.Last();
                        MergeCell mergeCell = new MergeCell() { Reference = $"{first}:{last}" };
                        model.MergeCells.Append(mergeCell);
                    }
                    if (modelCellFormat.MergeVerticalInt != 0)
                    {
                        var listStart = RowNameColumnsMatrix[i].Skip(modelCellFormat.IndexCellStart - 1).ToList().First();
                        var listFinish = RowNameColumnsMatrix[i + modelCellFormat.MergeVerticalInt].Skip(modelCellFormat.IndexCellStart - 1).ToList().First();
                        MergeCell mergeCell = new MergeCell() { Reference = $"{listStart}:{listFinish}" };
                        model.MergeCells.Append(mergeCell);
                    }
                }
                i++;
            }
        }
        /// <summary>
        /// Генерация набора строк данных 
        /// </summary>
        /// <param name="model">Модель</param>
        /// <returns></returns>
        public void GenerateSheetDataAddRowsList(ref ModelXmlExcel model) 
        {
            foreach (var row in model.Row)
            {
                model.SheetData.Append(row);
            }
        }
   }
}
