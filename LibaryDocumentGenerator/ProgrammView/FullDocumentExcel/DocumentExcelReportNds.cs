using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using LibaryDocumentGenerator.ProgrammView.Excel.CellAndRowsStyle;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryXMLAutoModelXmlSql.PreCheck.ModelCard;

namespace LibaryDocumentGenerator.ProgrammView.FullDocumentExcel
{
   public class DocumentExcelReportNds
    {

        public SpreadsheetDocument Document { get; set; }

        public WorkbookPart WorkBookPart { get; set; }
        public DocumentExcelReportNds(SpreadsheetDocument document, WorkbookPart workBookPart)
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
            StyleCellsAndGenerateText style = new StyleCellsAndGenerateText(Document, WorkBookPart);
            var listValueCell = new List<ModelRowFormat>();
            var stileIsNotBorderRight = style.StyleTimesNewRoman(HorizontalAlignmentValues.Right);
            var stileIsNotBorderCenter = style.StyleTimesNewRoman(HorizontalAlignmentValues.Center);
            var stileFullBorderCenter = style.StyleTimesNewRoman(HorizontalAlignmentValues.Center, VerticalAlignmentValues.Center, false, true);
            var stileFullBorderRight = style.StyleTimesNewRoman(HorizontalAlignmentValues.Left, VerticalAlignmentValues.Bottom,false, true);
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
                        new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 1, ValueCell = reportAskNds.Id.ToString(), CellFormat = CellValues.Number, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 2, IndexCellFinish = 1, ValueCell = reportAskNds.Name, CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 3, IndexCellFinish = 1, ValueCell = reportAskNds.Inn, CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 4, IndexCellFinish = 1, ValueCell = reportAskNds.Kpp, CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm1.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm1 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm1).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 15, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 16, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 18, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm2.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm2 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm2).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 23, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 27, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm3.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 28, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm3 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm3).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankSalesSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankSalesSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 31, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookSalesSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookSalesSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 1, ValueCell = reportAskNds.YearCashBankPurchaseSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 34, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearCashBankPurchaseSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 1, ValueCell = reportAskNds.YearBookPurchaseSumm4.ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 36, IndexCellFinish = 1, ValueCell = $"{decimal.Round(100 * reportAskNds.YearBookPurchaseSumm4 / IsSummZero(cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm4).ToArray()), 2)}%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                        new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 10 , CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    }
                });

            }
            listValueCell.Add(new ModelRowFormat()
            {
                HeightRow = 15.75D,
                ModelCell = new List<ModelCellFormat>()
                {
                    new ModelCellFormat() { IndexCellStart = 1, IndexCellFinish = 4, ValueCell = "ИТОГО", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight, MergeHorizontalInt = 4  },
                    new ModelCellFormat() { IndexCellStart = 5, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 6, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 7, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight }, 
                    new ModelCellFormat() { IndexCellStart = 8, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 9, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 10, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 11, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm1).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 12, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 13, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 14, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 15, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 16, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 17, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 18, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 19, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm2).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 20, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 21, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 22, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 23, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 24, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 25, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 26, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 27, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm3).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 28, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 29, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankSalesSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 30, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 31, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookSalesSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 32, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 33, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearCashBankPurchaseSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 34, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 35, IndexCellFinish = 1, ValueCell = cardModelAskNds.FullReportAskNds.Select(x => x.YearBookPurchaseSumm4).Sum().ToString(CultureInfo.InvariantCulture), CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 36, IndexCellFinish = 1, ValueCell = "100%", CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
                    new ModelCellFormat() { IndexCellStart = 37, IndexCellFinish = 10, CellFormat = CellValues.String, StyleIndex = stileFullBorderRight },
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
    }
}
