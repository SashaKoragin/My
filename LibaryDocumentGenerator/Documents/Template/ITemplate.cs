using DocumentFormat.OpenXml.Packaging;
namespace LibaryDocumentGenerator.Documents.Template
{
   public interface ITemplate
   {
       void CreateDocum(string path, LibaryXMLAutoReports.TemplateSheme.Template template, object obj);

      void CreateWord(WordprocessingDocument package, LibaryXMLAutoReports.TemplateSheme.Template template, object obj);
   }
}
