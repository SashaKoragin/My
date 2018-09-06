using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.FottersGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;

namespace LibaryDocumentGenerator.ProgrammView.Word.Template.FottersDocument
{
   public class Fotters
    {
        /// <summary>
        ///Создание колонтитула
        /// </summary>
        /// <param name="mainDocument">Документ</param>
        /// <param name="tamplate">Шаблон</param>
        public void FottersAddDocument(MainDocumentPart mainDocument, LibaryXMLAutoReports.TemplateSheme.Template tamplate)
        {
            var fotters = FotterGenerate.AddFotters(mainDocument);
           var paragraph = new RunGenerate();
            Footer footer = new Footer();
            footer.Append(paragraph.RunParagraphGeneratorStandart(tamplate.Stone.Stone4, "20", JustificationValues.Left, 0, "0", false, true));
            footer.Append(paragraph.RunParagraphGeneratorStandart(tamplate.Stone.Stone5, "20", JustificationValues.Left, 0, "0", false, true));
            fotters.Footer = footer;
        }
    }
}
