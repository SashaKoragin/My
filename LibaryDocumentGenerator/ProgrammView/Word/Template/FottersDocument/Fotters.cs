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
        /// <param name="isp">Шаблон</param>
        /// <param name="phone">Шаблон</param>
        public void FottersAddDocument(MainDocumentPart mainDocument, string isp, string phone)
        {
            var fotters = FotterGenerate.AddFotters(mainDocument);
            var paragraph = new RunGenerate();
            Footer footer = new Footer();
            var par1 = paragraph.RunParagraphGeneratorStandart();
            par1.Append(FormulandFooters(isp));
            var par2 = paragraph.RunParagraphGeneratorStandart();
            par2.Append(FormulandFooters(phone));
            footer.Append(par1);
            footer.Append(par2);
            fotters.Footer = footer;
        }
        /// <summary>
        /// Формула простановки формулы на последнюю страницу
        /// </summary>
        /// <param name="footerstext"></param>
        /// <returns></returns>
        private Run FormulandFooters(string footerstext)
        {
            Run run = new Run();
            FieldChar field = new FieldChar() { FieldCharType = FieldCharValues.Begin };
            FieldCode code = new FieldCode() { Text = "IF" };
            FieldChar field1 = new FieldChar() { FieldCharType = FieldCharValues.Begin };
            FieldCode code1 = new FieldCode() { Text = "PAGE" };
            FieldChar field2 = new FieldChar() { FieldCharType = FieldCharValues.Separate };
            FieldChar field6 = new FieldChar() { FieldCharType = FieldCharValues.End };
            FieldCode code2 = new FieldCode() { Text = "=" };
            FieldChar field3 = new FieldChar() { FieldCharType = FieldCharValues.Begin };
            FieldCode code3 = new FieldCode() { Text = "NUMPAGES" };
            FieldChar field4 = new FieldChar() { FieldCharType = FieldCharValues.Separate };
            FieldChar field5 = new FieldChar() { FieldCharType = FieldCharValues.End };
            FieldCode code5 = new FieldCode() { Text = "\"" };
            FieldCode code6 = new FieldCode() { Text = footerstext };
            FieldCode code7 = new FieldCode() { Text = "\"" };
            FieldCode code8 = new FieldCode() { Text = "\"\"" };
            FieldChar field7 = new FieldChar() { FieldCharType = FieldCharValues.Separate };
            FieldChar field8 = new FieldChar() { FieldCharType = FieldCharValues.End };
            RunProperties runProperties = new RunProperties();
            NoProof noProof = new NoProof();
            runProperties.Append(noProof);
            run.Append(field);
            run.Append(code);
            run.Append(field1);
            run.Append(code1);
            run.Append(field2);
            run.Append(field6);
            run.Append(code2);
            run.Append(field3);
            run.Append(code3);
            run.Append(field4);
            run.Append(field5);
            run.Append(code5);
            run.Append(code6);
            run.Append(code7);
            run.Append(code8);
            run.Append(field7);
            run.Append(field8);
            run.Append(runProperties);
            return run;
        }
    }
}
