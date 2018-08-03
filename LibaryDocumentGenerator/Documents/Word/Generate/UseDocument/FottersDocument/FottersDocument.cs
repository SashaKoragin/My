using System.Collections.Generic;
using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.FottersGenerator;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Page;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Paragraphs;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.FottersDocument
{
   public class FottersDocument
    {
        /// <summary>
        ///Создание колонтитула
        /// </summary>
        /// <param name="mainDocument">Документ</param>
        /// <param name="tamplate">Шаблон</param>
        public void FottersAddDocument(MainDocumentPart mainDocument, LibaryXMLAutoReports.TemplateSheme.Template tamplate)
        {
            var fotters = FottersAdd.AddFotters(mainDocument);
            GenerateRun paragraph = new GenerateRun();
            Footer footer = new Footer();
            footer.Append(paragraph.RunParagraphGeneratorStandart(tamplate.Stone.Stone4, "20",JustificationValues.Left,0,"0",false,true));
            footer.Append(paragraph.RunParagraphGeneratorStandart(tamplate.Stone.Stone5, "20", JustificationValues.Left, 0, "0", false, true));
            fotters.Footer = footer;
        }

    }
}
