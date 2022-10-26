using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabase.ReportXml.ModelComparableUserResult;
using EfDatabaseParametrsModel;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentExcel;

namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
   public class ReportComparableTechResult : ITemplateExcel<ModelComparableAllSystemInventory[]>
   {
       public string FullPathDocumentWord { get; set; }

       /// <summary>
       /// Выгрузка и удаление файла отчета
       /// </summary>
       /// <returns></returns>
       public Stream FileArray()
       {
           var file = File.ReadAllBytes(FullPathDocumentWord);
           File.Delete(FullPathDocumentWord);
           return new MemoryStream(file);
       }

       public void CreateDocument(string path, ModelComparableAllSystemInventory[] template, object obj = null)
       {
           FullPathDocumentWord = path + "Разночтения из баз данных" + Constant.WordConstant.FormatXlsx;
           using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
           {
               CreateExcel(package, template, obj);
               package.Close();
           }
       }

       public void CreateExcel(SpreadsheetDocument document, ModelComparableAllSystemInventory[] template, object obj = null)
       {
           ListExcel excel = new ListExcel(document);
           var list1 = excel.CreateExcelList("Разночтения из баз данных");
           AllDocumentExcel reportExcelUsers = new AllDocumentExcel(excel.Document, excel.WorkBookPart);
           list1.Worksheet = reportExcelUsers.ReportModelComparableTech(template, (Parametrs[])obj);
           excel.WorkBookPart.Workbook.Save();
       }
    }
}
