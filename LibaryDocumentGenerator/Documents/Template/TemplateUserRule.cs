using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryXMLAutoModelXmlAuto.OtdelRuleUsers;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class TemplateUserRule
    {

        public string Fullpathdocumentword { get; set; }
        public void CreateDocum(string path, RuleTemplate template, object obj)
        {
            var i = 1;
            foreach (var tempuotdel in template.Otdel)
            {
               Fullpathdocumentword = path + tempuotdel.Number+"_"+tempuotdel.NameOtdel+"_"+tempuotdel.Dates?.ToString("dd.MM.yyyy") +"_"+ i + Constant.WordConstant.Formatword;
               using (WordprocessingDocument package = WordprocessingDocument.Create(Fullpathdocumentword, WordprocessingDocumentType.Document))
               {
                  CreateWord(package, tempuotdel,template.SenderUsers, obj);
                  package.MainDocumentPart.Document.Save();
                  package.Close();
               }

               i++;
            }
        }

        public void CreateWord(WordprocessingDocument package, Otdel templateotdel, SenderUsers senders, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingpage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingpage.ParametrPageHorizontEditMargin(new PageMargin() { Top = 300, Right = 794, Bottom = 100, Left = 794, Header = 300, Footer = 700U, Gutter = 0U }));
            document.Append(documentInvoce.GenerateRuleUserTemplate(templateotdel,senders));
            mainDocumentPart.Document = document;
        }
    }
}
