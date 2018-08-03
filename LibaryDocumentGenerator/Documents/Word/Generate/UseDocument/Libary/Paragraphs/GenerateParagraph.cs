using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Paragraphs
{
   public class GenerateParagraph
    {
        /// <summary>
        /// Создает и настраивает Параграф выполнить 1 раз
        /// </summary>
        /// <param name="justifications">Выравнивание</param>
        /// <param name="leftident">Отступ слева</param>
        /// <returns></returns>
        public Paragraph GeneratorParagraph(JustificationValues justifications,string leftident = "0")
        {
            Paragraph paragraph = new Paragraph();
            Indentation indental = new Indentation() {Left = leftident};
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            Justification justification = new Justification() { Val = justifications };
            paragraphProperties.Append(indental);
            paragraphProperties.Append(justification);
            paragraph.Append(paragraphProperties);
            return paragraph;
        }
    }
}
