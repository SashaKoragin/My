using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EfDatabase.ReportCard;
using LibaryDocumentGenerator.ProgrammView.Excel.BorderModel;
using LibaryDocumentGenerator.ProgrammView.Excel.CellAndRowsStyle;
using LibaryDocumentGenerator.ProgrammView.Excel.ColumnModelWidth;
using LibaryDocumentGenerator.ProgrammView.Excel.FontModel;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryXMLAutoModelXmlSql.PreCheck.ModelCard;

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
                if (date >= userCard.Date_out)
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
    }
}
