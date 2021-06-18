using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LibaryDocumentGenerator.ProgrammView.Excel.Library
{
   public class ListExcel
   {
        private UInt32Value SheetId { get; set; } = 1;
        public SpreadsheetDocument Document { get; set; }
        private Sheets Sheets { get; set; }
        public WorkbookPart WorkBookPart { get; set; }



        /// <summary>
        /// Начальное создание документа
        /// </summary>
        /// <param name="document"></param>
        public ListExcel(SpreadsheetDocument document)
        {
            Document = document;
            WorkBookPart = Document.AddWorkbookPart();
            WorkBookPart.Workbook = new Workbook();
            Sheets = Document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
        }

        /// <summary>
        /// Создать лист
        /// </summary>
        public WorksheetPart CreateExcelList(string nameList)
        {
            WorksheetPart worksheetPart = WorkBookPart.AddNewPart<WorksheetPart>();

            Sheet sheet = new Sheet()
            {
                Id = Document.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = SheetId,
                Name = nameList
            };
            Sheets.Append(sheet);
            SheetId++;
            return worksheetPart;
        }


    }
}
