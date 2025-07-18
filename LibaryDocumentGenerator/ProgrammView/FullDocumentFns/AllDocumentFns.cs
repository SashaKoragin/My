using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EfDatabaseAutomation.Automation.BaseLogica.FnsModel.ModelMonitoringNalog;
using LibaryDocumentGenerator.ProgrammView.Excel.BorderModel;
using LibaryDocumentGenerator.ProgrammView.Excel.CellAndRowsStyle;
using LibaryDocumentGenerator.ProgrammView.Excel.FontModel;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;

namespace LibaryDocumentGenerator.ProgrammView.FullDocumentFns
{
    public class AllDocumentFns
    {
        private SpreadsheetDocument Document { get; set; }

        private WorkbookPart WorkBookPart { get; set; }
        public AllDocumentFns(SpreadsheetDocument document, WorkbookPart workBookPart)
        {
            Document = document;
            WorkBookPart = workBookPart;
        }


        public Worksheet CreateReportMonitoringNalog(List<ModelSvodDataBase> template, string nameReport)
        {
            Worksheet worksheet = new Worksheet();
            CellAndRowsStyle generateCellAndRowsStyle = new CellAndRowsStyle();
            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            FontModel fontModel = new FontModel();
            BorderModel modelBorder = new BorderModel();
            var listValueCell = new List<ModelRowFormat>();
            var modelExcel = new ModelXmlExcel();

            var stileFullBorderCenter = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center);
            var stileFullBorderLeft = style.StyleTimesNewRoman(fontModel.GenerateFont(), modelBorder.GenerateStandardFullBorderSetting(BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin, BorderStyleValues.Thin), HorizontalAlignmentValues.Left, VerticalAlignmentValues.Center);

            listValueCell.Add(new ModelRowFormat() { HeightRow = 16.50D, ModelCell = new List<ModelCellFormat>() { new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 14, ValueCell = nameReport, CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, MergeHorizontalInt = 14 } } });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 70.50D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = "ИНН (вет// ЦУН 02./2.04)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 22.70D},
                    new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = "ФИО (ветка ЦУН 02./2.04)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 57.20D},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = "Дата постановки на учет  (ветка ЦУН 02./2.04)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = "Дата снятия с учета (ветка ЦУН 02./2.04)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = "Причина снятия с учета (ветка ЦУН 02./2.04)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 100.50D},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = "Дата начала применения ЕСХН (ветка НА 201/03)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = "Дата окончания применения ЕСХН (ветка НА 201/03)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = "Дата снятия с учета (ликвидация,  реорганизация) (ветка НА 201/03)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = "Дата начала применения УСН (ветка НА 202/03)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = "Дата окончания применения УСН (ветка НА 202/03)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = "Дата снятия с учета (ликвидация,  реорганизация) (ветка НА 202/03)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = "Дата начала действия патента (ветка НА 203/031)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = "Дата окончания действия патента (ветка НА 203/031)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D},
                    new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = "Дата отмены действия (ветка НА 203/031)", CellFormat = CellValues.String, StyleIndex = stileFullBorderCenter, WidthColumn = 24.70D}
                }
            });
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = "1", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = "2", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = "3", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = "4", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = "5", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = "6", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = "7", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = "8", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = "9", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = "10", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = "11", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = "12", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = "13", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter},
                    new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = "14", CellFormat = CellValues.Number, StyleIndex = stileFullBorderCenter}
                }
            });
            foreach (var modelSvodDataBase in template)
            {
                listValueCell.Add(new ModelRowFormat()
                {
                    HeightRow = 15.75D,
                    ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = modelSvodDataBase.Inn, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = modelSvodDataBase.Fio, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateStartTsun?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateFinishTsun?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = modelSvodDataBase.TypeStop, CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateStartEshn?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateFinishEshn?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateStopEshn?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateStartUsn?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateFinishUsn?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateStopUsn?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateStartPatent?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateFinishPatent?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft},
                    new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = modelSvodDataBase.DateCancelPatent?.ToString("dd.MM.yyyy"), CellFormat = CellValues.String, StyleIndex = stileFullBorderLeft}
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
    }
}
