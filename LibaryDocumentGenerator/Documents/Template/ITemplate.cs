using DocumentFormat.OpenXml.Packaging;
namespace LibaryDocumentGenerator.Documents.Template
{
   public interface ITemplate
   {
       void CreateDocum(string path, LibaryXMLAutoReports.FullTemplateSheme.Document template, object obj);

      void CreateWord(WordprocessingDocument package, LibaryXMLAutoReports.FullTemplateSheme.Document template, object obj);
   }
}
