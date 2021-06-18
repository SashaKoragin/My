using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LibaryDocumentGenerator.ProgrammView.Excel.Library
{


    public class ModelRowFormat
    {
        /// <summary>
        /// Размер колонки
        /// </summary>
        public DoubleValue HeightRow { get; set; } = null;
        /// <summary>
        /// Модель ячеек
        /// </summary>
        public List<ModelCellFormat> ModelCell { get; set; } = new List<ModelCellFormat>();
    }


    public class ModelCellFormat
    {
        /// <summary>
        /// Индекс вставки начала
        /// </summary>
        public int IndexCellStart { get; set; }
        /// <summary>
        /// Индекс вставки окончания
        /// </summary>
        public int IndexCellFinish { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string ValueCell { get; set; }
        /// <summary>
        /// Формат ячейки
        /// </summary>
        public CellValues CellFormat { get; set; }
        /// <summary>
        /// Количество объединяющих ячеек по горизонтали
        /// </summary>
        public int MergeHorizontalInt { get; set; } = 0;
        /// <summary>
        /// Количество объединяющих ячеек по вертикале
        /// </summary>
        public int MergeVerticalInt { get; set; } = 0;
        /// <summary>
        /// Размер колонки
        /// </summary>
        public DoubleValue WidthColumn { get; set; } = null;
        /// <summary>
        /// Индекс стиля
        /// </summary>
        public UInt32Value StyleIndex { get; set; } = null;


    }
    /// <summary>
    /// Генерация Excel
    /// </summary>
    public class ModelXmlExcel
    {
        /// <summary>
        /// Лист ячеек
        /// </summary>
        public List<Cell> Cell { get; set; } = new List<Cell>();
        /// <summary>
        /// Лист строк
        /// </summary>
        public List<Row> Row { get; set; } = new List<Row>();
        /// <summary>
        /// Колонки
        /// </summary>
        public Columns Сolumns = new Columns();
        /// <summary>
        /// Объединение ячеек
        /// </summary>
        public MergeCells MergeCells = new MergeCells();
        /// <summary>
        /// Генерация листа
        /// </summary>
        public SheetData SheetData = new SheetData();
    }

}
