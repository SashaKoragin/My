using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EfDatabase.Inventory.Base;
using EfDatabase.ReportCard;
using EfDatabase.ReportXml.ModelComparableUserResult;
using EfDatabaseParametrsModel;
using LibaryDocumentGenerator.ProgrammView.Excel.BorderModel;
using LibaryDocumentGenerator.ProgrammView.Excel.CellAndRowsStyle;
using LibaryDocumentGenerator.ProgrammView.Excel.ColumnModelWidth;
using LibaryDocumentGenerator.ProgrammView.Excel.FontModel;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryXMLAutoModelXmlSql.PreCheck.ModelCard;
using ModelComparableAllSystemInventory = EfDatabase.ReportXml.ModelComparableUserResult.ModelComparableAllSystemInventory;

namespace LibaryDocumentGenerator.ProgrammView.FullDocumentExcel
{
   public class AllDocumentExcel
    {

        public SpreadsheetDocument Document { get; set; }

        public WorkbookPart WorkBookPart { get; set; }
        public AllDocumentExcel(SpreadsheetDocument document, WorkbookPart workBookPart)
        {
            Document = document;
            WorkBookPart = workBookPart;
        }
        /// <summary>
        /// Умный отчет о выборки разночтений из БД Lotus Notes, AD, DKS
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parametersColums">Модель колонок</param>
        /// <returns></returns>
        public Worksheet ReportModelComparableUser(ComparableUserResult[] template, Parametrs[] parametersColums)
        {
            var i = 1; //Начальная колонка
            Worksheet worksheet = new Worksheet();


            CellAndRowsStyle generateCellAndRowsStyle = new CellAndRowsStyle();
            ColumnModelWidth columnModelWidth = new ColumnModelWidth();
            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            FontModel fontModel = new FontModel();
            BorderModel modelBorder = new BorderModel();
            var listValueCell = new List<ModelRowFormat>();
            var modelExcel = new ModelXmlExcel();
            var columnCount = parametersColums.Count(x => x.IsVisible);
           
            var fillOrange = new Fill();
            var fillHeaders1 = new Fill();
            var fillHeaders2 = new Fill();
            var fillHeaders3 = new Fill();
            var fillHeaders4 = new Fill();
            var fillHeaders5 = new Fill();
            var fillHeaders6 = new Fill();
            var fillHeaders7 = new Fill();
            var fillHeaders8 = new Fill();
            var fillHeaders9 = new Fill();
            var fillNotUse1 = new Fill();
            var fillNotUse2 = new Fill();
            var fillNotUse3 = new Fill();
            var fillNotUse4 = new Fill();
            var fillGreen1= new Fill();
            var fillGreen2 = new Fill();
            var fillGreen3 = new Fill();
            var fillGreen4 = new Fill();
            var fillRed1 = new Fill();
            var fillRed2 = new Fill();
            var fillRed3 = new Fill();
            var fillRed4 = new Fill();
 
            fillOrange.Append(new PatternFill() {PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() {Rgb = "FFC000" } });
            fillHeaders1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders5.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders6.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders7.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders8.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders9.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillNotUse1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillNotUse2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillNotUse3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillNotUse4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillGreen1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillGreen2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillGreen3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillGreen4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillRed1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
            fillRed2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
            fillRed3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
            fillRed4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });

            var standartStyle = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center);
            var stileFullBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center,true,false,  fillOrange);
            var stileFullBorderCenterColorHeaders = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, true, false, fillHeaders1);

            var stileFullBorderLeftFirstColumnFirstRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders2);
            var stileFullBorderLeftFirstColumnFirstRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse1);
            var stileFullBorderLeftFirstColumnFirstRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen1);
            var stileFullBorderLeftFirstColumnFirstRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed1);
            var stileFullBorderLeftFirstColumnFirstRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders3);

            var stileFullBorderLeftFirstColumnLastRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders4);
            var stileFullBorderLeftFirstColumnLastRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse2);
            var stileFullBorderLeftFirstColumnLastRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen2);
            var stileFullBorderLeftFirstColumnLastRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed2);
            var stileFullBorderLeftFirstColumnLastRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders5);

            var stileFullBorderLeftFirstColumnFirstLastGroupRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders6);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse3);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen3);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed3);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders7);

            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders8);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse4);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen4);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed4);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders9);

            listValueCell.Add(new ModelRowFormat() { HeightRow = 16.50D, ModelCell = new List<ModelCellFormat>()  { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = columnCount, ValueCell = "Анализ учетных данных пользователей из БД: Инвентаризация, Lotus Notes, Active Directory, ДКС", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter,   MergeHorizontalInt = columnCount } } });
            var cellColumns = new List<ModelCellFormat>();
            foreach (var parameters in parametersColums.Where(x=>x.IsVisible))
            {
                var width = columnModelWidth.ListColumnWidthReportComparableUser.First(x => x.NameModel == parameters.NameColumn);
                cellColumns.Add(new ModelCellFormat() { IndexCellStart = i, IndexCellFinish = 1, ValueCell = parameters.Value, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenterColorHeaders, WidthColumn = width.Width });
                i++;
            }
            listValueCell.Add(new ModelRowFormat() { HeightRow = 15.75D,  ModelCell = cellColumns  });
            var countGroupInc = 1;
            try
            {
                var groupData = template.GroupBy(x => new { FullName = x.FullName.Replace('ё', 'е'), x.CountGrooup })
                .Select(x => new { x.Key.FullName, x.Key.CountGrooup }).ToList().OrderByDescending(x => x.CountGrooup);
                foreach (var group in groupData)
                {
                    var inkCountRow = 1; 
                    var countDataGroup = template.Count(x => x.FullName.Replace('ё', 'е') == @group.FullName & x.CountGrooup == @group.CountGrooup);
                    foreach (var data in template.Where(x=>x.FullName.Replace('ё', 'е')  == group.FullName & x.CountGrooup == @group.CountGrooup))
                    {
                        var j = 1;
                        var cellDataColumnsAllTable = new List<ModelCellFormat>();
                        var countColumn = parametersColums.Count(x => x.IsVisible);
                        var modelInventory = template.FirstOrDefault(x => x.FullName.Replace('ё', 'е') == @group.FullName & x.NameSystem == "Инвентаризация" & x.CountGrooup == @group.CountGrooup);
                        foreach (var parametersVisible in parametersColums.Where(x => x.IsVisible))
                        {
                            var value = Convert.ToString(data.GetType().GetProperty(parametersVisible.NameColumn)?.GetValue(data, null));
                            var width = columnModelWidth.ListColumnWidthReportComparableUser.First(x => x.NameModel == parametersVisible.NameColumn);
                            if (modelInventory == null)
                            {
                                //Не сравниваем
                                cellDataColumnsAllTable.Add(j == 1
                                    ? new ModelCellFormat()
                                    {
                                        IndexCellStart = j, IndexCellFinish = 1, ValueCell = value,
                                        CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnLastRow1,
                                        WidthColumn = width.Width
                                    }
                                    : new ModelCellFormat()
                                    {
                                        IndexCellStart = j, IndexCellFinish = 1, ValueCell = value,
                                        CellFormat = CellValues.String, StyleIndex = standartStyle, WidthColumn = width.Width
                                    });
                            }
                            else
                            {
                                var valueInv = Convert.ToString(modelInventory.GetType().GetProperty(parametersVisible.NameColumn)?.GetValue(modelInventory, null));
                                if (inkCountRow != 1 & inkCountRow != countDataGroup)
                                {
                                    if (j == 1)
                                    {
                                        cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnLastRow1, WidthColumn = width.Width });
                                    }
                                    else
                                    {
                                        if (j != countColumn)
                                        {
                                            cellDataColumnsAllTable.Add(width.IsComparable == false
                                                ? new ModelCellFormat()
                                                {
                                                    IndexCellStart = j,
                                                    IndexCellFinish = 1,
                                                    ValueCell = value,
                                                    CellFormat = CellValues.String,
                                                    StyleIndex = stileFullBorderLeftFirstColumnLastRow2,
                                                    WidthColumn = width.Width
                                                }
                                                : new ModelCellFormat()
                                                {
                                                    IndexCellStart = j,
                                                    IndexCellFinish = 1,
                                                    ValueCell = value,
                                                    CellFormat = CellValues.String,
                                                    StyleIndex = (value == valueInv)
                                                        ? stileFullBorderLeftFirstColumnLastRow3
                                                        : stileFullBorderLeftFirstColumnLastRow4,
                                                    WidthColumn = width.Width
                                                });
                                        }
                                        else
                                        {
                                            cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnLastRow5, WidthColumn = width.Width });
                                        }
                                    }
                                }
                                else
                                {
                                    if (inkCountRow == countDataGroup)
                                    {
                                        if (j == 1)
                                        {
                                            cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastGroupRow1, WidthColumn = width.Width });
                                        }
                                        else
                                        {
                                            if (j != countColumn)
                                            {
                                                cellDataColumnsAllTable.Add(width.IsComparable == false
                                                    ? new ModelCellFormat()
                                                    {
                                                        IndexCellStart = j,
                                                        IndexCellFinish = 1,
                                                        ValueCell = value,
                                                        CellFormat = CellValues.String,
                                                        StyleIndex = stileFullBorderLeftFirstColumnFirstLastGroupRow2,
                                                        WidthColumn = width.Width
                                                    }
                                                    : new ModelCellFormat()
                                                    {
                                                        IndexCellStart = j,
                                                        IndexCellFinish = 1,
                                                        ValueCell = value,
                                                        CellFormat = CellValues.String,
                                                        StyleIndex = (value == valueInv)
                                                            ? stileFullBorderLeftFirstColumnFirstLastGroupRow3
                                                            : stileFullBorderLeftFirstColumnFirstLastGroupRow4,
                                                        WidthColumn = width.Width
                                                    });
                                            }
                                            else
                                            {
                                                cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastGroupRow5, WidthColumn = width.Width });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (countGroupInc == 1 & inkCountRow != countDataGroup)
                                        {
                                            if (j == 1)
                                            {
                                                cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstRow1, WidthColumn = width.Width });
                                            }
                                            else
                                            {
                                                if (j != countColumn)
                                                {
                                                    cellDataColumnsAllTable.Add(width.IsComparable == false
                                                        ? new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = stileFullBorderLeftFirstColumnFirstRow2,
                                                            WidthColumn = width.Width
                                                        }
                                                        : new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = (value == valueInv)
                                                                ? stileFullBorderLeftFirstColumnFirstRow3
                                                                : stileFullBorderLeftFirstColumnFirstRow4,
                                                            WidthColumn = width.Width
                                                        });
                                                }
                                                else
                                                {
                                                    cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstRow5, WidthColumn = width.Width });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (j == 1)
                                            {
                                                cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastEndGroupRow1, WidthColumn = width.Width });
                                            }
                                            else
                                            {
                                                if (j != countColumn)
                                                {
                                                    cellDataColumnsAllTable.Add(width.IsComparable == false
                                                        ? new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = stileFullBorderLeftFirstColumnFirstLastEndGroupRow2,
                                                            WidthColumn = width.Width
                                                        }
                                                        : new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = (value == valueInv)
                                                                ? stileFullBorderLeftFirstColumnFirstLastEndGroupRow3
                                                                : stileFullBorderLeftFirstColumnFirstLastEndGroupRow4,
                                                            WidthColumn = width.Width
                                                        });
                                                }
                                                else
                                                {
                                                    cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastEndGroupRow5, WidthColumn = width.Width });
                                                }
                                            }
                                        }
                                    }
                         
                                }
                            }
                            j++;
                        }
                        listValueCell.Add(new ModelRowFormat()
                        {
                            HeightRow = 15.75D,
                            ModelCell = cellDataColumnsAllTable
                        });
                        inkCountRow++;
                    }
                    countGroupInc++;
                }
            }
            catch
            {
                var fillRed = new Fill();
                fillRed.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
                var stileErrorRed = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, true, false, fillRed);
                listValueCell.Add(new ModelRowFormat() { HeightRow = 16.50D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = columnCount, ValueCell = "Ошибка отсутствует один или несколько основных параметров для данного отчета: Наименование системы данных, Полное имя пользователя, Количество в группе", CellFormat = CellValues.String, StyleIndex = stileErrorRed, MergeHorizontalInt = columnCount } } });
            }
            generateCellAndRowsStyle.GenerateCell(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.MergeCells(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.GenerateSheetDataAddRowsList(ref modelExcel);
            worksheet.Append(modelExcel.SheetData);
            worksheet.InsertAfter(modelExcel.MergeCells, worksheet.Elements<SheetData>().First());
            worksheet.InsertAt(modelExcel.Сolumns, 0);
            return worksheet;
        }
        /// <summary>
        /// Умный отчет о выборки разночтений техники из БД Инвентаризация, AD, АКСИОК
        /// </summary>
        /// <param name="template">Модель разкладки</param>
        /// <param name="parametersColums">Модель колонок</param>
        /// <returns></returns>
        public Worksheet ReportModelComparableTech(ModelComparableAllSystemInventory[] template, Parametrs[] parametersColums)
        {
            var i = 1; //Начальная колонка
            Worksheet worksheet = new Worksheet();

            CellAndRowsStyle generateCellAndRowsStyle = new CellAndRowsStyle();
            ColumnModelWidth columnModelWidth = new ColumnModelWidth();

            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            FontModel fontModel = new FontModel();
            BorderModel modelBorder = new BorderModel();
            var listValueCell = new List<ModelRowFormat>();
            var modelExcel = new ModelXmlExcel();
            var columnCount = parametersColums.Count(x => x.IsVisible);

            var fillOrange = new Fill();
            var fillHeaders1 = new Fill();
            var fillHeaders2 = new Fill();
            var fillHeaders3 = new Fill();
            var fillHeaders4 = new Fill();
            var fillHeaders5 = new Fill();
            var fillHeaders6 = new Fill();
            var fillHeaders7 = new Fill();
            var fillHeaders8 = new Fill();
            var fillHeaders9 = new Fill();
            var fillNotUse1 = new Fill();
            var fillNotUse2 = new Fill();
            var fillNotUse3 = new Fill();
            var fillNotUse4 = new Fill();
            var fillGreen1 = new Fill();
            var fillGreen2 = new Fill();
            var fillGreen3 = new Fill();
            var fillGreen4 = new Fill();
            var fillRed1 = new Fill();
            var fillRed2 = new Fill();
            var fillRed3 = new Fill();
            var fillRed4 = new Fill();

            fillOrange.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFC000" } });
            fillHeaders1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders5.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders6.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders7.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders8.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillHeaders9.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFFCC" } });
            fillNotUse1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillNotUse2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillNotUse3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillNotUse4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FFFF99" } });
            fillGreen1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillGreen2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillGreen3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillGreen4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "92D050" } });
            fillRed1.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
            fillRed2.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
            fillRed3.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
            fillRed4.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });

            var standartStyle = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center);
            var stileFullBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, true, false, fillOrange);
            var stileFullBorderCenterColorHeaders = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, true, false, fillHeaders1);

            var stileFullBorderLeftFirstColumnFirstRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders2);
            var stileFullBorderLeftFirstColumnFirstRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse1);
            var stileFullBorderLeftFirstColumnFirstRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen1);
            var stileFullBorderLeftFirstColumnFirstRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed1);
            var stileFullBorderLeftFirstColumnFirstRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders3);

            var stileFullBorderLeftFirstColumnLastRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders4);
            var stileFullBorderLeftFirstColumnLastRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse2);
            var stileFullBorderLeftFirstColumnLastRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen2);
            var stileFullBorderLeftFirstColumnLastRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed2);
            var stileFullBorderLeftFirstColumnLastRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders5);

            var stileFullBorderLeftFirstColumnFirstLastGroupRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders6);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse3);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen3);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed3);
            var stileFullBorderLeftFirstColumnFirstLastGroupRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Double), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders7);

            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow1 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders8);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow2 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillNotUse4);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow3 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillGreen4);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow4 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillRed4);
            var stileFullBorderLeftFirstColumnFirstLastEndGroupRow5 = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Medium, BorderStyleValues.Double, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fillHeaders9);

            listValueCell.Add(new ModelRowFormat() { HeightRow = 16.50D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = columnCount, ValueCell = "Анализ техники из БД: Инвентаризация, Active Directory, АКСИОК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = columnCount } } });
            var cellColumns = new List<ModelCellFormat>();

            foreach (var parameters in parametersColums.Where(x => x.IsVisible))
            {
                var width = columnModelWidth.ListColumnWidthReportComparableTech.First(x => x.NameModel == parameters.NameColumn);
                cellColumns.Add(new ModelCellFormat() { IndexCellStart = i, IndexCellFinish = 1, ValueCell = parameters.Value, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenterColorHeaders, WidthColumn = width.Width });
                i++;
            }
            listValueCell.Add(new ModelRowFormat() { HeightRow = 15.75D, ModelCell = cellColumns });
            var countGroupInc = 1;

            try
            {
                var groupData = template.GroupBy(x => new { x.SerialNumber, x.CountGroup }).Select(x => new { SerialNumber = x.Key.SerialNumber, x.Key.CountGroup }).ToList().OrderByDescending(x => x.CountGroup);
                foreach (var group in groupData)
                {
                    var inkCountRow = 1;
                    var countDataGroup = template.Count(x => x.SerialNumber == @group.SerialNumber & x.CountGroup == @group.CountGroup);
                    foreach (var data in template.Where(x => x.SerialNumber == @group.SerialNumber & x.CountGroup == @group.CountGroup))
                    {
                        var j = 1;
                        var cellDataColumnsAllTable = new List<ModelCellFormat>();
                        var countColumn = parametersColums.Count(x => x.IsVisible);
                        var modelInventory = template.FirstOrDefault(x => x.SerialNumber == @group.SerialNumber & x.NameSystem == "Инвентаризация" & x.CountGroup == @group.CountGroup);
                        foreach (var parametersVisible in parametersColums.Where(x => x.IsVisible))
                        {
                            var value = Convert.ToString(data.GetType().GetProperty(parametersVisible.NameColumn)?.GetValue(data, null));
                            var width = columnModelWidth.ListColumnWidthReportComparableTech.First(x => x.NameModel == parametersVisible.NameColumn);
                            if (modelInventory == null)
                            {
                                //Не сравниваем
                                cellDataColumnsAllTable.Add(j == 1
                                    ? new ModelCellFormat()
                                    {
                                        IndexCellStart = j,
                                        IndexCellFinish = 1,
                                        ValueCell = value,
                                        CellFormat = CellValues.String,
                                        StyleIndex = stileFullBorderLeftFirstColumnLastRow1,
                                        WidthColumn = width.Width
                                    }
                                    : new ModelCellFormat()
                                    {
                                        IndexCellStart = j,
                                        IndexCellFinish = 1,
                                        ValueCell = value,
                                        CellFormat = CellValues.String,
                                        StyleIndex = standartStyle,
                                        WidthColumn = width.Width
                                    });

                            }
                            else
                            {
                                var valueInv = Convert.ToString(modelInventory.GetType().GetProperty(parametersVisible.NameColumn)?.GetValue(modelInventory, null));
                                if (inkCountRow != 1 & inkCountRow != countDataGroup)
                                {
                                    if (j == 1)
                                    {
                                        cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnLastRow1, WidthColumn = width.Width });
                                    }
                                    else
                                    {
                                        if (j != countColumn)
                                        {
                                            cellDataColumnsAllTable.Add(width.IsComparable == false
                                                ? new ModelCellFormat()
                                                {
                                                    IndexCellStart = j,
                                                    IndexCellFinish = 1,
                                                    ValueCell = value,
                                                    CellFormat = CellValues.String,
                                                    StyleIndex = stileFullBorderLeftFirstColumnLastRow2,
                                                    WidthColumn = width.Width
                                                }
                                                : new ModelCellFormat()
                                                {
                                                    IndexCellStart = j,
                                                    IndexCellFinish = 1,
                                                    ValueCell = value,
                                                    CellFormat = CellValues.String,
                                                    StyleIndex = (value == valueInv)
                                                        ? stileFullBorderLeftFirstColumnLastRow3
                                                        : stileFullBorderLeftFirstColumnLastRow4,
                                                    WidthColumn = width.Width
                                                });
                                        }
                                        else
                                        {
                                            cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnLastRow5, WidthColumn = width.Width });
                                        }
                                    }
                                }
                                else
                                {
                                    if (inkCountRow == countDataGroup)
                                    {
                                        if (j == 1)
                                        {
                                            cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastGroupRow1, WidthColumn = width.Width });
                                        }
                                        else
                                        {
                                            if (j != countColumn)
                                            {
                                                cellDataColumnsAllTable.Add(width.IsComparable == false
                                                    ? new ModelCellFormat()
                                                    {
                                                        IndexCellStart = j,
                                                        IndexCellFinish = 1,
                                                        ValueCell = value,
                                                        CellFormat = CellValues.String,
                                                        StyleIndex = stileFullBorderLeftFirstColumnFirstLastGroupRow2,
                                                        WidthColumn = width.Width
                                                    }
                                                    : new ModelCellFormat()
                                                    {
                                                        IndexCellStart = j,
                                                        IndexCellFinish = 1,
                                                        ValueCell = value,
                                                        CellFormat = CellValues.String,
                                                        StyleIndex = (value == valueInv)
                                                            ? stileFullBorderLeftFirstColumnFirstLastGroupRow3
                                                            : stileFullBorderLeftFirstColumnFirstLastGroupRow4,
                                                        WidthColumn = width.Width
                                                    });
                                            }
                                            else
                                            {
                                                cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastGroupRow5, WidthColumn = width.Width });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (countGroupInc == 1 & inkCountRow != countDataGroup)
                                        {
                                            if (j == 1)
                                            {
                                                cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstRow1, WidthColumn = width.Width });
                                            }
                                            else
                                            {
                                                if (j != countColumn)
                                                {
                                                    cellDataColumnsAllTable.Add(width.IsComparable == false
                                                        ? new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = stileFullBorderLeftFirstColumnFirstRow2,
                                                            WidthColumn = width.Width
                                                        }
                                                        : new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = (value == valueInv)
                                                                ? stileFullBorderLeftFirstColumnFirstRow3
                                                                : stileFullBorderLeftFirstColumnFirstRow4,
                                                            WidthColumn = width.Width
                                                        });
                                                }
                                                else
                                                {
                                                    cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstRow5, WidthColumn = width.Width });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (j == 1)
                                            {
                                                cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastEndGroupRow1, WidthColumn = width.Width });
                                            }
                                            else
                                            {
                                                if (j != countColumn)
                                                {
                                                    cellDataColumnsAllTable.Add(width.IsComparable == false
                                                        ? new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = stileFullBorderLeftFirstColumnFirstLastEndGroupRow2,
                                                            WidthColumn = width.Width
                                                        }
                                                        : new ModelCellFormat()
                                                        {
                                                            IndexCellStart = j,
                                                            IndexCellFinish = 1,
                                                            ValueCell = value,
                                                            CellFormat = CellValues.String,
                                                            StyleIndex = (value == valueInv)
                                                                ? stileFullBorderLeftFirstColumnFirstLastEndGroupRow3
                                                                : stileFullBorderLeftFirstColumnFirstLastEndGroupRow4,
                                                            WidthColumn = width.Width
                                                        });
                                                }
                                                else
                                                {
                                                    cellDataColumnsAllTable.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = value, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeftFirstColumnFirstLastEndGroupRow5, WidthColumn = width.Width });
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            j++;
                        }
                        listValueCell.Add(new ModelRowFormat()
                        {
                            HeightRow = 15.75D,
                            ModelCell = cellDataColumnsAllTable
                        });
                        inkCountRow++;
                    }
                    countGroupInc++;
                }
            }
            catch
            {
                var fillRed = new Fill();
                fillRed.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = "FF0000" } });
                var stileErrorRed = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, true, false, fillRed);
                listValueCell.Add(new ModelRowFormat() { HeightRow = 16.50D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = columnCount, ValueCell = "Ошибка отсутствует один или несколько основных параметров для данного отчета: Наименование системы данных, Серийный номер, Количество в группе", CellFormat = CellValues.String, StyleIndex = stileErrorRed, MergeHorizontalInt = columnCount } } });
            }
            generateCellAndRowsStyle.GenerateCell(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.MergeCells(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.GenerateSheetDataAddRowsList(ref modelExcel);
            worksheet.Append(modelExcel.SheetData);
            worksheet.InsertAfter(modelExcel.MergeCells, worksheet.Elements<SheetData>().First());
            worksheet.InsertAt(modelExcel.Сolumns, 0);
            return worksheet;
        }
        /// <summary>
        /// Отчет о доступности серверов 
        /// </summary>
        /// <param name="template">Сервера из БД</param>
        /// <returns></returns>
        public Worksheet ReportStatusIpServer(List<AllIpServerSelect> template)
        {
            Worksheet worksheet = new Worksheet();
            CellAndRowsStyle generateCellAndRowsStyle = new CellAndRowsStyle();
            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            FontModel fontModel = new FontModel();
            BorderModel modelBorder = new BorderModel();
            var listValueCell = new List<ModelRowFormat>();
            var modelExcel = new ModelXmlExcel();
            var stileFullBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center);
            listValueCell.Add(new ModelRowFormat() { HeightRow = 16.50D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 5, ValueCell = "Анализ доступности серверов и окон в налоговом органе!", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 5 } } });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
            {
                new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = "Id", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 4.00D},
                new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = "Наименование сервера", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 45.50D},
                new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = "Ip Адрес", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 20.50D},
                new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = "Полное наименование Ip в БД", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 40.00D},
                new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = "Статус сервера", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 36.00D}
            }
            });
            foreach (var allIpServerSelect in template)
            {
                var fill = new Fill();
                fill.Append(new PatternFill() { PatternType = PatternValues.Solid, ForegroundColor = new ForegroundColor() { Rgb = allIpServerSelect.ColorStatus } });
                var stileFullBorderLeft = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium, BorderStyleValues.Medium), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center, true, false, fill);
                listValueCell.Add(new ModelRowFormat()
                {
                    HeightRow = 15.75D,
                    ModelCell = new List<ModelCellFormat>()
                    {
                        new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = allIpServerSelect.Id.ToString(), CellFormat = CellValues.String, StyleIndex = (uint)stileFullBorderLeft},
                        new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = allIpServerSelect.NameServer, CellFormat = CellValues.String, StyleIndex = (uint)stileFullBorderLeft},
                        new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = allIpServerSelect.IpAdress, CellFormat = CellValues.String, StyleIndex = (uint)stileFullBorderLeft},
                        new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = allIpServerSelect.FullIpAdressDataBase, CellFormat = CellValues.String, StyleIndex = (uint)stileFullBorderLeft},
                        new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = allIpServerSelect.InfoStatusReport, CellFormat = CellValues.String, StyleIndex = (uint)stileFullBorderLeft}
                    }
                });
            }
            generateCellAndRowsStyle.GenerateCell(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.MergeCells(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.GenerateSheetDataAddRowsList(ref modelExcel);
            worksheet.Append(modelExcel.SheetData);
            worksheet.InsertAfter(modelExcel.MergeCells, worksheet.Elements<SheetData>().First());
            worksheet.InsertAt(modelExcel.Сolumns, 0);
            return worksheet;
        }

        /// <summary>
        /// Генерация отчета по АСК НДС Excel
        /// </summary>
        /// <returns></returns>
        /// <param name="cardModelAskNds"></param>
        /// <param name="year"></param>
        public Worksheet ReportNds(CardFaceUl cardModelAskNds, int year)
        {
            Worksheet worksheet = new Worksheet();
            var modelExcel = new ModelXmlExcel();
            CellAndRowsStyle generateCellAndRowsStyle = new CellAndRowsStyle();
            BorderModel modelBorder = new BorderModel();
            FontModel fontModel = new FontModel();
            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            var listValueCell = new List<ModelRowFormat>();
            var stileIsNotBorderRight = style.StyleTimesNewRoman(fontModel.GenerateFont(),null,HorizontalAlignmentValues.Right);
            var stileIsNotBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(), null, HorizontalAlignmentValues.Center);
            var stileFullBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center);
            var stileFullBorderLeft = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorder());
            listValueCell.Add(new ModelRowFormat() { HeightRow = 65.25D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 46, ValueCell = "Приложение № 2 к Регламенту,\nутвержденному приказом\nУФНС России по г.Москве\nот ___________ № ______", CellFormat = CellValues.String, StyleIndex = (uint)stileIsNotBorderRight, MergeHorizontalInt = 46 } } }); //  
            listValueCell.Add(new ModelRowFormat() { HeightRow = 15.00D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 28, ValueCell = "Сравнительный анализ движения денежных средств по счетам налогоплательщика и книг покупок/книг продаж", CellFormat = CellValues.String, StyleIndex = (uint)stileIsNotBorderCenter, MergeHorizontalInt = 28 } } });
            listValueCell.Add(new ModelRowFormat() { HeightRow = 15.00D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 46, ValueCell = null, CellFormat = CellValues.String } } });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
            {
                new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = "№", CellFormat = CellValues.String, StyleIndex = (uint)stileFullBorderCenter, MergeVerticalInt = 2},
                new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = "Наименование контрагента", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeVerticalInt = 2 },
                new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = "ИНН", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeVerticalInt = 2 },
                new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = "КПП", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeVerticalInt = 2},
                new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 8, ValueCell = $"{year-3}.г", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeHorizontalInt = 8 },
                new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 8, ValueCell = $"{year-2}.г", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeHorizontalInt = 8 },
                new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 8, ValueCell = $"{year-1}.г", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeHorizontalInt = 8 },
                new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 8, ValueCell = $"{year}.г", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeHorizontalInt = 8 },
                new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 1, ValueCell = "Описание реально осуществляемой \nэкономической деятельности (реализуемые/приобретаемые \nтовары, работы, услуги и т.п.)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeVerticalInt = 2 },
                new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 9, ValueCell = "Краткое описание контрагента", CellFormat = CellValues.String, StyleIndex =  stileFullBorderCenter, MergeHorizontalInt = 9},
            }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 37.00D,
                ModelCell = new List<ModelCellFormat>()
            {
                new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter },
                new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter },
                new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter },
                new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter },
                new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 4, ValueCell = "Поставщик", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 4, ValueCell = "Покупатель", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 4, ValueCell = "Поставщик", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 4, ValueCell = "Покупатель", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 4, ValueCell = "Поставщик", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 4, ValueCell = "Покупатель", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 4, ValueCell = "Поставщик", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 4, ValueCell = "Покупатель", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 4 },
                new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, },
                new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, ValueCell = "Значимая информация о \nконтрагенте (дата \nрегистрации, выручка, \nдоля вычетов, налоговая \nнагрузка, транзитный \nхарактер платежей и др.)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeVerticalInt = 1},
                new ModelCellFormat() { IndexCellStart = 39, IndexCellFinish = 3, ValueCell = "Численность", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 3 },
                new ModelCellFormat() { IndexCellStart = 42, IndexCellFinish = 1, ValueCell = "Вид \nдеятельности \nконтрагнта",StyleIndex = stileFullBorderCenter, CellFormat = CellValues.String, MergeVerticalInt = 1 },
                new ModelCellFormat() { IndexCellStart = 43, IndexCellFinish = 1, ValueCell = "Риски,присвоенные\n АИС",StyleIndex = stileFullBorderCenter, CellFormat = CellValues.String, MergeVerticalInt = 1 },
                new ModelCellFormat() { IndexCellStart = 44, IndexCellFinish = 3, ValueCell = "Признаки фирмы-\"однодневки\" (массовый \nруководитель,учредитель, адрес)", StyleIndex = stileFullBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 3},
            }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 60.00D,
                ModelCell = new List<ModelCellFormat>()
            {
                new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 6.00D },
                new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 19.00D },
                new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 14.00D },
                new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 14.00D },
                new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D},
                new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 15, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 16, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 18, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 23, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 27, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 28, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 31, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 1, ValueCell = "Сумма тыс. руб.", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 34, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 1, ValueCell = "АСК", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 36, IndexCellFinish = 1, ValueCell = "Удельный вес %", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D },
                new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 30.00D },
                new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 27.00D },
                new ModelCellFormat() { IndexCellStart = 39, IndexCellFinish = 1, ValueCell = "20__", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 9.00D },
                new ModelCellFormat() { IndexCellStart = 40, IndexCellFinish = 1, ValueCell = "20__", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 9.00D },
                new ModelCellFormat() { IndexCellStart = 41, IndexCellFinish = 1, ValueCell = "20__", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 9.00D },
                new ModelCellFormat() { IndexCellStart = 42, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 18.00D },
                new ModelCellFormat() { IndexCellStart = 43, IndexCellFinish = 1, ValueCell = null, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 18.00D },
                new ModelCellFormat() { IndexCellStart = 44, IndexCellFinish = 1, ValueCell = "руководитель\nявляется\nруководителем", CellFormat = CellValues.String,StyleIndex = stileFullBorderCenter, WidthColumn = 15.00D},
                new ModelCellFormat() { IndexCellStart = 45, IndexCellFinish = 1, ValueCell = "учредитель\nявляется\nучредителем", CellFormat = CellValues.String,StyleIndex = stileFullBorderCenter, WidthColumn = 13.00D },
                new ModelCellFormat() { IndexCellStart = 46, IndexCellFinish = 1, ValueCell = "по адресу\nзарегистрировано\nорганизаций", CellFormat = CellValues.String,StyleIndex = stileFullBorderCenter, WidthColumn = 20.00D },
            }
            });

            foreach (var reportAskNds in cardModelAskNds.FullReportAskNds)
            {
                listValueCell.Add(new ModelRowFormat()
                {
                    HeightRow = 15.75D,
                    ModelCell = new List<ModelCellFormat>()
                    {
                        new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = reportAskNds.Id.ToString(), CellFormat = CellValues.Number, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = reportAskNds.Name, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = reportAskNds.Inn, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = reportAskNds.Kpp, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 15, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 16, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 18, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 23, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 27, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 28, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 31, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 34, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 36, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                        new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 10 , CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    }
                });

            }
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 4, ValueCell = "ИТОГО", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft, MergeHorizontalInt = 4  },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft }, 
                    new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 15, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 16, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 18, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 23, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 27, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 28, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 31, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 34, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 36, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 10, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft },
                }
            });
            generateCellAndRowsStyle.GenerateCell(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.MergeCells(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.GenerateSheetDataAddRowsList(ref modelExcel);
            worksheet.Append(modelExcel.SheetData); 
            worksheet.InsertAfter(modelExcel.MergeCells, worksheet.Elements<SheetData>().First());
            worksheet.InsertAt(modelExcel.Сolumns, 0);
            return worksheet;
        }

        /// <summary>
        /// Шаблон для табелей заполнение и выдача отчета
        /// </summary>
        /// <returns></returns>
        /// <param name="model">Модель табеля</param>
        /// <returns></returns>
        public Worksheet ReportCard(ReportCardModel model)
        {
            Worksheet worksheet = new Worksheet();
            var countDaysCell = DateTime.DaysInMonth(model.SettingParameters.Year, model.SettingParameters.Mouth.NumberMouth);
            var countHead = countDaysCell + 2 + 6; //Количество получившихся заголовков
            var listModelCellGenerate = new List<ModelCellFormat>();
            var listModelCellGenerateNumeric = new List<ModelCellFormat>();
            ColumnModelWidth columnModelWidth = new ColumnModelWidth();
            BorderModel modelBorder = new BorderModel();
            FontModel fontModel = new FontModel();
            var modelExcel = new ModelXmlExcel();
            CellAndRowsStyle generateCellAndRowsStyle = new CellAndRowsStyle();
            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            var stileIsFullBorderCenterBottomBold = style.StyleTimesNewRoman(fontModel.GenerateFont(8D,true), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Center,VerticalAlignmentValues.Top);
            var stileIsFullBorderCenterCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(10D), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center);
            var stileIsFullBorderCenterBottom = style.StyleTimesNewRoman(fontModel.GenerateFont(10D), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Top);
            var stileIsNotBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(9D), null,HorizontalAlignmentValues.Center);
            var stileIsNotBorderCenterFontNine = style.StyleTimesNewRoman(fontModel.GenerateFont(8D), null, HorizontalAlignmentValues.Center );
            var stileIsNotBorderLeft = style.StyleTimesNewRoman(fontModel.GenerateFont(10D));
            var stileIsNotBorderRight = style.StyleTimesNewRoman(fontModel.GenerateFont(10D), modelBorder.GenerateBottomBorder(), HorizontalAlignmentValues.Right);
            var stileIsBorderBottomCenter12 = style.StyleTimesNewRoman(fontModel.GenerateFont(12D), modelBorder.GenerateBottomBorder(), HorizontalAlignmentValues.Center);
            var stileIsBorderBottomCenter10 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D), modelBorder.GenerateBottomBorder(), HorizontalAlignmentValues.Center);
            var stileIsBorderBottomLeft12 = style.StyleTimesNewRoman(fontModel.GenerateFont(12D), modelBorder.GenerateBottomBorder());
            var stileIsNotBorderRight10 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D), null, HorizontalAlignmentValues.Right);
            var stileIsNotBorderRight12 = style.StyleTimesNewRoman(fontModel.GenerateFont(12D), null, HorizontalAlignmentValues.Right);
            var stileIsNotBorderCenterBold = style.StyleTimesNewRoman(fontModel.GenerateFont(12D,true), null, HorizontalAlignmentValues.Center);

            var stileCol1 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Right, VerticalAlignmentValues.Top);
            var stileCol2 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Top);
            var stileCol31 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin,BorderStyleValues.Thin,BorderStyleValues.Thin,BorderStyleValues.None), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Top);
            var stileCol32 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.None, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Top);
            var stileCol4 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Top);
            var stileCol5 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D,true), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Right, VerticalAlignmentValues.Top);
            var stileCol6 = style.StyleTimesNewRoman(fontModel.GenerateFont(7D, true), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Right, VerticalAlignmentValues.Top);
            var stileCol7 = style.StyleTimesNewRoman(fontModel.GenerateFont(7D), modelBorder.GenerateStandardFullBorder(), HorizontalAlignmentValues.Right, VerticalAlignmentValues.Top,false,true);

            var stileT1 = style.StyleTimesNewRoman(fontModel.GenerateFont(12D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin,
                    BorderStyleValues.Thin, BorderStyleValues.Medium), HorizontalAlignmentValues.Center);
            var stileT2 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium,
                    BorderStyleValues.Medium, BorderStyleValues.Thin), HorizontalAlignmentValues.Center);
            var stileT3 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium,
                    BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Center);
            var stileT4 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Medium, BorderStyleValues.Medium,
                    BorderStyleValues.Thin, BorderStyleValues.Medium), HorizontalAlignmentValues.Center);
            var styleMedium1 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.MediumDashDot, BorderStyleValues.None,
                    BorderStyleValues.MediumDashDot, BorderStyleValues.None));
            var styleMedium2 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.None, BorderStyleValues.None,
                    BorderStyleValues.MediumDashDot, BorderStyleValues.None));
            var styleMedium3 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.None, BorderStyleValues.MediumDashDot,
                    BorderStyleValues.MediumDashDot, BorderStyleValues.None));
            var styleMedium4 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.MediumDashDot, BorderStyleValues.None,
                    BorderStyleValues.None, BorderStyleValues.None),HorizontalAlignmentValues.Center);
            var styleMedium5 = style.StyleTimesNewRoman(fontModel.GenerateFont(9D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.None, BorderStyleValues.MediumDashDot,
                    BorderStyleValues.None, BorderStyleValues.None));
            var styleMediumR5 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.None, BorderStyleValues.MediumDashDot,
                    BorderStyleValues.None, BorderStyleValues.Thin));
            var styleMedium6 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.MediumDashDot, BorderStyleValues.None,
                    BorderStyleValues.None, BorderStyleValues.MediumDashDot));
            var styleMedium7 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.None, BorderStyleValues.MediumDashDot,
                    BorderStyleValues.None, BorderStyleValues.MediumDashDot));
            var styleMedium8 = style.StyleTimesNewRoman(fontModel.GenerateFont(10D),
                modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.None, BorderStyleValues.None,
                    BorderStyleValues.None, BorderStyleValues.MediumDashDot));
            var listValueCell = new List<ModelRowFormat>();
            generateCellAndRowsStyle.GenerateEmptyCell(ref listValueCell, 3.75D, 42, columnModelWidth.ListColumnWidthReportCard);
            listValueCell.Add(new ModelRowFormat() { HeightRow = 12.75D, ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 24, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 13,ValueCell = "У Т В Е Р Ж Д А Ю", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderCenter, MergeHorizontalInt = 15 }
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 20, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 4,ValueCell = "Руководитель", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderLeft, MergeHorizontalInt = 4 }
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 20, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 4, ValueCell = "учреждения", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderLeft, MergeHorizontalInt = 4 },
                    new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 1, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 3, CellFormat = CellValues.String, StyleIndex = stileIsNotBorderRight },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 8, ValueCell = model.SettingParameters.LeaderN.NameFaceLeader,  CellFormat = CellValues.String, StyleIndex = stileIsNotBorderRight, MergeHorizontalInt = 10 }
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 25, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 3, ValueCell = "(подпись)", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderCenterFontNine, MergeHorizontalInt = 3 },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 8, ValueCell = "(расшифровка подписи)",  CellFormat = CellValues.String, StyleIndex = stileIsNotBorderCenterFontNine, MergeHorizontalInt = 10 }
                }
            });
            generateCellAndRowsStyle.GenerateEmptyCell(ref listValueCell, 12.75D, 42);
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 26, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 27, IndexCellFinish = 11, ValueCell = "\"     \"_________________   ______г.", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderCenter, MergeHorizontalInt = 13 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 29, ValueCell = $"Табель  № {model.SettingParameters.LeaderD.CodeDepartment}_{model.SettingParameters.Mouth.NumberMouthString}", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderCenterBold, MergeHorizontalInt = 30 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 29, ValueCell = "учета использования рабочего времени", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderCenterBold, MergeHorizontalInt = 30 },
                }
            });

            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 34, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = "КОДЫ", CellFormat = CellValues.String, StyleIndex = stileT1, MergeHorizontalInt = 4 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 29, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 5, ValueCell = "Форма  по ОКУД", CellFormat = CellValues.String, MergeHorizontalInt = 5, StyleIndex = stileIsNotBorderRight10 },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = "0504421", CellFormat = CellValues.Number, StyleIndex = stileT2, MergeHorizontalInt = 4 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 5, ValueCell = model.SettingParameters.Type.IdType == 0 ? "за период с 1 по 15" : $"за период с 1 по {countDaysCell}", CellFormat = CellValues.String,  StyleIndex = stileIsNotBorderRight12,  MergeHorizontalInt = 5  },
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 5, ValueCell = CultureInfo.CreateSpecificCulture("ru-Ru").DateTimeFormat.MonthGenitiveNames[model.SettingParameters.Mouth.NumberMouth - 1].ToLower(), CellFormat = CellValues.String, StyleIndex = stileIsBorderBottomCenter12, MergeHorizontalInt = 5 },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 3, ValueCell = model.SettingParameters.Year.ToString(), CellFormat = CellValues.Number, StyleIndex = stileIsBorderBottomCenter12, MergeHorizontalInt = 3  },
                    new ModelCellFormat() { IndexCellStart = 16, IndexCellFinish = 1, ValueCell = "г", CellFormat = CellValues.String, StyleIndex = stileIsNotBorderLeft },
                    new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 16, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 2,ValueCell = "Дата", CellFormat = CellValues.String, MergeHorizontalInt = 2, StyleIndex = stileIsNotBorderRight10  },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = $"{countDaysCell}.{model.SettingParameters.Mouth.NumberMouthString}.{model.SettingParameters.Year}", CellFormat = CellValues.String, StyleIndex = stileT3, MergeHorizontalInt = 4 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 34, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = "09770165", CellFormat = CellValues.Number, StyleIndex = stileT3, MergeVerticalInt = 1, MergeHorizontalSquare = 4},
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 17.25D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 4,ValueCell = "Учреждение", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.String, MergeHorizontalInt = 4 },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 24,ValueCell = model.SettingParameters.LeaderN.NameFullOrganization, StyleIndex = stileIsBorderBottomLeft12, CellFormat = CellValues.String, MergeHorizontalInt = 24 },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 3,  CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 3,ValueCell = "по ОКПО",  CellFormat = CellValues.String, MergeHorizontalInt = 3, StyleIndex = stileIsNotBorderRight10},
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, CellFormat = CellValues.String, StyleIndex = stileT3},
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 17.25D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 4, ValueCell = "Структурное подразделение", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.String, MergeHorizontalInt = 4 },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 24, ValueCell = model.SettingParameters.LeaderD.NameDepartment, StyleIndex = stileIsBorderBottomLeft12, CellFormat = CellValues.String, MergeHorizontalInt = 24 },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 6,  CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = model.SettingParameters.LeaderD.CodeDepartment, CellFormat = CellValues.String, StyleIndex = stileT3, MergeHorizontalInt = 4},
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 17.25D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 4, ValueCell = "Вид табеля", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.String, MergeHorizontalInt = 4 },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 17, ValueCell = model.SettingParameters.View.NameView.ToLower(), StyleIndex = stileIsBorderBottomLeft12, CellFormat = CellValues.String, MergeHorizontalInt = 17 },
                    new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 6,  CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 28, IndexCellFinish = 7,ValueCell = "Номер корректировки",   CellFormat = CellValues.String, MergeHorizontalInt = 7, StyleIndex = stileIsNotBorderRight10},
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = model.SettingParameters.View.IdView.ToString(), CellFormat = CellValues.Number, StyleIndex = stileT3, MergeHorizontalInt = 4},
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 4, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 17, ValueCell = "(первичный - 0; корректирующий - 1, 2 и т. д.)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 17 },
                    new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 4,  CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 9,ValueCell = "Дата формирования документа",   CellFormat = CellValues.String, MergeHorizontalInt = 9, StyleIndex = stileIsNotBorderRight10},
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 4, ValueCell = DateTime.Now.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex =  stileT4, MergeHorizontalInt = 4},
                }
            });
            generateCellAndRowsStyle.GenerateEmptyCell(ref listValueCell, 12.75D, 42);

            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, ValueCell = "Фамилия, имя,\nотчество", StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String, MergeVerticalInt = 3, MergeHorizontalSquare = 2},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 2, ValueCell = "Учетный\nномер", StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String, MergeVerticalInt = 2, MergeHorizontalSquare = 2},
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 2, ValueCell = "Должность\n(профессия)", StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String, MergeVerticalInt = 3, MergeHorizontalSquare = 2},
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = countDaysCell+2, ValueCell = "Числа месяца", StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String, MergeHorizontalInt = countDaysCell+2},
                }
            });

            listModelCellGenerate.Add(new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String });
            listModelCellGenerate.Add(new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String });
            listModelCellGenerate.Add(new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String });

            var indexStart = 7;
            for (int i = 1; i <= countDaysCell; i++)
            {
                if (i == 16)
                {
                    listModelCellGenerate.Add(new ModelCellFormat() { IndexCellStart = indexStart, ValueCell = "Итого дней (часов) явок (неявок) с 1 по 15", IndexCellFinish = 1, StyleIndex = stileIsFullBorderCenterBottomBold, CellFormat = CellValues.String, MergeVerticalInt = 2 });
                    indexStart++;
                }
                listModelCellGenerate.Add(new ModelCellFormat() { IndexCellStart = indexStart, ValueCell = i.ToString(), IndexCellFinish = 1, StyleIndex = stileIsFullBorderCenterBottom, CellFormat = CellValues.Number, MergeVerticalInt = 2 });
                indexStart++;
            }
            listModelCellGenerate.Add(new ModelCellFormat() { IndexCellStart = indexStart, ValueCell = "Итого дней (часов) явок (неявок) за месяц", IndexCellFinish = 1, StyleIndex = stileIsFullBorderCenterBottomBold, CellFormat = CellValues.String, MergeVerticalInt = 2, WidthColumn = ColumnModelWidth.FormulaWidth(34) });

            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.00D,
                ModelCell = listModelCellGenerate
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 39.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = countDaysCell+2, StyleIndex = stileIsFullBorderCenterBottom, CellFormat = CellValues.String},
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 63.00D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 2, StyleIndex = stileIsFullBorderCenterCenter, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = countDaysCell+2, StyleIndex = stileIsFullBorderCenterBottom, CellFormat = CellValues.String},
                }
            });
            var indexCell = 1;
            for (var j = 1; j <= countHead; j++)
            {
                switch (j)
                {
                    case 1:
                    case 5:
                        listModelCellGenerateNumeric.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 2, ValueCell = indexCell.ToString(), StyleIndex = stileIsFullBorderCenterBottom, CellFormat = CellValues.Number, MergeHorizontalInt = 2 });
                        j += 2;
                        indexCell++;
                        break;
                }
                listModelCellGenerateNumeric.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = indexCell.ToString(), StyleIndex = stileIsFullBorderCenterBottom, CellFormat = CellValues.Number });
                indexCell++;
            }
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = listModelCellGenerateNumeric
            });

            var iNumber = 1;
            var notCount = new[] { "ОЧ","ОТ","Б","А","ОД","ОУ","К" }; //Не считать
            bool exitTypeModelReport = true;
            foreach (var usersReportCard in model.SettingParameters.UsersReportCard)
            {
                var listNewModel1 = new List<ModelCellFormat>();
                var listNewModel2 = new List<ModelCellFormat>();
                var dateTime = new DateTime(model.SettingParameters.Year, model.SettingParameters.Mouth.NumberMouth, 1);
                var arrTime = new double[countHead];
                var arrLetterStatus = new string[countHead];
                
                for (var j = 1; j <= countHead; j++)
                {
                    switch (j)
                    {
                        case 1:
                            listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = iNumber.ToString(), StyleIndex = stileCol1, CellFormat = CellValues.Number, MergeVerticalInt = 1});
                            listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol1, CellFormat = CellValues.Number });
                            break;
                        case 2:
                            listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = usersReportCard.Fio.Replace(" ", "\n"), StyleIndex = stileCol2, CellFormat = CellValues.String, MergeVerticalInt = 1 });
                            listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol2, CellFormat = CellValues.String });
                            break;
                        case 3:
                            listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = usersReportCard.Tab_num, StyleIndex = stileCol2, CellFormat = CellValues.String, MergeVerticalInt = 1 });
                            listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol2, CellFormat = CellValues.String });
                            break;
                        case 4:
                            listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol31, CellFormat = CellValues.String });
                            listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol32, CellFormat = CellValues.String });
                            break;
                        case 5:
                            listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 2, ValueCell = usersReportCard.New_post, StyleIndex = stileCol4, CellFormat = CellValues.String, MergeVerticalInt = 1, MergeHorizontalSquare = 2});
                            listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 2, StyleIndex = stileCol4, CellFormat = CellValues.Number });
                            break;
                    }
                    if (j > 6)
                    {
                        if (exitTypeModelReport)
                        {
                            if (j == 22)
                            {
                                listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = arrTime.Sum() == 0.00 ? null : $"{arrTime.Sum()}.00", StyleIndex = stileCol6, CellFormat = CellValues.String });
                                listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = arrLetterStatus.Count(x => notCount.Any(elem => elem.Equals(x))) == 0 ? null : arrLetterStatus.Count(x => notCount.Any(elem => elem.Equals(x))).ToString(), StyleIndex = stileCol5, CellFormat = CellValues.Number });
                                exitTypeModelReport = model.SettingParameters.Type.IdType == 1;
                            }
                            else if (j == countHead)
                            {
                                listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = arrTime.Sum() == 0.00 ? null : $"{arrTime.Sum()}.00", StyleIndex = stileCol6, CellFormat = CellValues.String });
                                listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = arrLetterStatus.Count(x => notCount.Any(elem => elem.Equals(x))) == 0 ? null : arrLetterStatus.Count(x => notCount.Any(elem => elem.Equals(x))).ToString(), StyleIndex = stileCol5, CellFormat = CellValues.Number });
                            }
                            else
                            {
                                arrTime[j] = GetDateLogicTimeWork(usersReportCard, model.SettingParameters.Holidays, dateTime);
                                arrLetterStatus[j] = GetDateLogicStatus(usersReportCard, model.SettingParameters.Holidays, dateTime);
                                listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = arrTime[j] == 0.00 ? null : $"{arrTime[j]}.00", StyleIndex = stileCol7, CellFormat = CellValues.String });
                                listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, ValueCell = arrLetterStatus[j], StyleIndex = stileCol2, CellFormat = CellValues.String });
                                dateTime = dateTime.AddDays(1);
                            }
                        }
                        else
                        {
                            listNewModel1.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol2, CellFormat = CellValues.Number });
                            listNewModel2.Add(new ModelCellFormat() { IndexCellStart = j, IndexCellFinish = 1, StyleIndex = stileCol2, CellFormat = CellValues.String });
                        }
                    }
                }
                listValueCell.Add(new ModelRowFormat()
                {
                    HeightRow = 18.75D,
                    ModelCell = listNewModel1
                });
                listValueCell.Add(new ModelRowFormat()
                {
                    HeightRow = 18.75D,
                    ModelCell = listNewModel2
                });
                iNumber++;
                exitTypeModelReport = true;
            }

            generateCellAndRowsStyle.GenerateEmptyCell(ref listValueCell, 18.75D, 42);
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 27.00D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, ValueCell ="Ответственный\nИсполнитель", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String, MergeHorizontalInt = 2},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 3, ValueCell = model.SettingParameters.LeaderD.DoljLeaderDepartment, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 3},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 4, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 4},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 7, ValueCell = model.SettingParameters.LeaderD.NameLeaderDepartment, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 7},
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium1 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String, StyleIndex = styleMedium2 },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium3 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 3, ValueCell = "(должность)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 3},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 4, ValueCell = "(подпись)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 4},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 7, ValueCell ="(расшифровка подписи)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 7},
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 14, ValueCell = "Отметка бухгалтерии о принятии настоящего табеля", CellFormat =  CellValues.String, StyleIndex = stileIsNotBorderLeft, MergeHorizontalInt = 14 },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 3, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 19, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, ValueCell ="Исполнитель", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String, MergeHorizontalInt = 2},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 3, ValueCell = model.SettingParameters.LeaderD.DoljIspDepartment, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 3},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 4, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 4},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 7, ValueCell = model.SettingParameters.LeaderD.NameIspDepartment, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 7},
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 3, ValueCell = "(должность)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 3},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 4, ValueCell = "(подпись)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 4},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 7, ValueCell ="(расшифровка подписи)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 7},
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat =  CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 19, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 19, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, ValueCell ="Работник кадровой", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String, MergeHorizontalInt = 2},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 3, ValueCell = model.SettingParameters.LeaderKadr.DoljKadr, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 3},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 4, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 4},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 7, ValueCell = model.SettingParameters.LeaderKadr.NameKadr, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String, MergeHorizontalInt = 7},
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 4, ValueCell = "Исполнитель", CellFormat = CellValues.String, StyleIndex = styleMedium4, MergeHorizontalInt = 4 },
                    new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 5, CellFormat = CellValues.String, StyleIndex = stileIsBorderBottomCenter10 },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 2, CellFormat = CellValues.String, StyleIndex = stileIsBorderBottomCenter10 },
                    new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 5, CellFormat = CellValues.String, StyleIndex = stileIsBorderBottomCenter10 },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMediumR5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 2, ValueCell ="службы", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String, MergeHorizontalInt = 2},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 3, ValueCell ="(должность)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 3},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 4, ValueCell = "(подпись)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 4},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 7, ValueCell ="(расшифровка подписи)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 7},
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4},
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 3, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 5, ValueCell = "(должность)", StyleIndex = stileIsNotBorderCenter,  CellFormat = CellValues.String, MergeHorizontalInt = 5 },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 4, ValueCell = "(подпись)", StyleIndex = stileIsNotBorderCenter, CellFormat = CellValues.String, MergeHorizontalInt = 4 },
                    new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 6, ValueCell = "(расшифровка подписи)", CellFormat = CellValues.String, StyleIndex = styleMedium5, MergeHorizontalInt = 6 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 19, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 },
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell ="\"", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell ="\"", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 6, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell ="20", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.Number },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell ="г.", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 6, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium4},
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 1, ValueCell ="\"", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 1, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 23, IndexCellFinish = 1, ValueCell ="\"", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 6, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 1, ValueCell ="20", StyleIndex = stileIsNotBorderRight10, CellFormat = CellValues.Number },
                    new ModelCellFormat() { IndexCellStart = 31, IndexCellFinish = 1, StyleIndex = stileIsBorderBottomCenter10, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, ValueCell ="г.", StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 5, CellFormat = CellValues.String },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium5 }
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 12.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 19, StyleIndex = stileIsNotBorderLeft, CellFormat = CellValues.String},
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium6 },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 17, CellFormat = CellValues.String, StyleIndex = styleMedium8 },
                    new ModelCellFormat() { IndexCellStart = 38, IndexCellFinish = 1, CellFormat = CellValues.String, StyleIndex = styleMedium7 },
                }
            });

            generateCellAndRowsStyle.GenerateCell(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.MergeCells(listValueCell, ref modelExcel);
            generateCellAndRowsStyle.GenerateSheetDataAddRowsList(ref modelExcel);
            worksheet.Append(modelExcel.SheetData);
            worksheet.InsertAfter(modelExcel.MergeCells, worksheet.Elements<SheetData>().First());
            worksheet.InsertAt(modelExcel.Сolumns, 0);
            return worksheet;
        }





        /// <summary>
        /// Проверка максимально суммы
        /// </summary>
        /// <param name="arrDecimal">Массив значений</param>
        /// <returns></returns>
        private decimal IsSummZero(decimal[] arrDecimal)
        {
            return arrDecimal.Sum().Equals(0) ? (decimal)1.00 : arrDecimal.Sum();
        }
        /// <summary>
        /// Проверка статусов
        /// </summary>
        /// <param name="userCard">Пользователь</param>
        /// <param name="holiDays">Праздничные дни</param>
        /// <param name="date">Дата для проверки</param>
        /// <returns></returns>
        private string GetDateLogicStatus(UsersReportCard userCard, Holidays[] holiDays, DateTime date) 
        {

            if (userCard.Status_link == 5)
            {
                if (userCard.ItemVacation != null)
                {
                    foreach (var itemVacation in userCard.ItemVacation)
                    {
                        if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end && (itemVacation.TypeVacation.Code == "20" || itemVacation.TypeVacation.Code == "21"))
                        {
                            return "А";
                        }
                        if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end && itemVacation.TypeVacation.Code == "40")
                        {
                            return "Р";
                        }
                    }
                }
                return "ОЧ";
            }
            if (userCard.Date_out != DateTime.MinValue)
            {
                if (date > userCard.Date_out) //Ошибка если уволен то больше на примере Севастьянова 21 включительно и на премере перевода Андрей Август
                {
                    return "-";
                }
            }
            if (date < userCard.Date_in)
            {
                return "-";
            }
            if (userCard.ItemVacation != null)
            {
                foreach (var itemVacation in userCard.ItemVacation)
                {
                    if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end && (itemVacation.TypeVacation.Code == "20" || itemVacation.TypeVacation.Code == "21"))
                    {
                        return "А";
                    }
                    if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end && itemVacation.TypeVacation.Code == "54")
                    {
                        return "ОУ";
                    }
                    if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end && (itemVacation.TypeVacation.Code == "13"|| itemVacation.TypeVacation.Code == "16"))
                    {
                        return "ОД";
                    }
                    if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end && (itemVacation.TypeVacation.Code == "42" || itemVacation.TypeVacation.Code == "43"))
                    {
                        return "ОЧ";
                    }
                    if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end)
                    {
                        return "ОТ";
                    }
                }
            }
            if (userCard.Disability != null)
            {
                foreach (var itemDisability in userCard.Disability)
                {
                    if (date >= itemDisability.Date_start_disability && date <= itemDisability.Date_finish_disability)
                    {
                        return "Б";
                    }
                }
            }
            if (userCard.Business != null)
            {
                foreach (var itemBusiness in userCard.Business)
                {
                    if (date >= itemBusiness.BusinessStart && date <= itemBusiness.BusinessFinish)
                    {
                        return "К";
                    }
                }
            }
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                {
                    return null;
                }
                return "В";
            }
            if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == true))
            {
                return "B";
            }
            if (userCard.Link_Gr == 6)
            {
                return "Я";
            }
            return null;
        }

        /// <summary>
        /// Проверка статусов и проставление времени
        /// </summary>
        /// <param name="userCard">Пользователь</param>
        /// <param name="holiDays">Праздничные дни</param>
        /// <param name="date">Дата для проверки</param>
        /// <returns></returns>
        private double GetDateLogicTimeWork(UsersReportCard userCard, Holidays[] holiDays, DateTime date)
        {

            if (userCard.Date_out != DateTime.MinValue)
            {
                if (date > userCard.Date_out)
                {
                    return 0.00;
                }
            }
            if (date < userCard.Date_in)
            {
                return 0.00;
            }
            if (userCard.Link_Gr == 6)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                    {
                        return 5.00;
                    }
                    return 0.00;
                }
                if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == true))
                {
                    return 0.00;
                }
                return 5.00;
            }
            if (userCard.Status_link == 5)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                    {
                        return  7.00;
                    }
                    return 0.00;
                }
                if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == true))
                {
                    return 0.00;
                }
                return (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false) ? 7.00 : 8.00);
            }
            if (userCard.ItemVacation != null)
            {
                foreach (var itemVacation in userCard.ItemVacation)
                {
                    if (date >= itemVacation.Date_begin && date <= itemVacation.Date_end)
                    {
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                            {
                                return 7.00;
                            }
                            return 0.00;
                        }
                        if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                        {
                            return 7.00;
                        }
                        if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == true))
                        {
                            return 0.00;
                        }
                        return 8.00;
                    }
                }
            }
            if (userCard.Disability != null)
            {
                foreach (var itemVacation in userCard.Disability)
                {
                    if (date >= itemVacation.Date_start_disability && date <= itemVacation.Date_finish_disability)
                    {
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                            {
                                return 7.00;
                            }
                            return 0.00;
                        }
                        if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                        {
                            return 7.00;
                        }
                        if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == true))
                        {
                            return 0.00;
                        }
                        return 8.00;
                    }
                }
            }
            if (userCard.Business != null)
            {
                foreach (var itemBusiness in userCard.Business)
                {
                    if (date >= itemBusiness.BusinessStart && date <= itemBusiness.BusinessFinish)
                    {
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                            {
                                return 7.00;
                            }
                            return 0.00;
                        }
                        if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == false))
                        {
                            return 7.00;
                        }
                        if (holiDays.Any(x => x.DateTime_Holiday == date && x.IS_HOLIDAY == true))
                        {
                            return 0.00;
                        }
                        return 8.00;
                    }
                }
            }
            return 0.00;
        }
        /// <summary>
        /// Расчет длины ячейки 
        /// </summary>
        /// <param name="countCharacters">Количество символов</param>
        /// <param name="maxWidthCharacters">Длина буквы в Excel</param>
        /// <returns></returns>
        private double? Truncate(int? countCharacters, int maxWidthCharacters)
        {
            if (countCharacters == 0)
            {
                return null;
            }
            var width = countCharacters * maxWidthCharacters + 5 / maxWidthCharacters * 256 / 256;
            return width;
        }
    }
}
