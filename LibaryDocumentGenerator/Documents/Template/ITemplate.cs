using DocumentFormat.OpenXml.Packaging;
namespace LibaryDocumentGenerator.Documents.Template
{
   public interface ITemplate<in T>
   {
      void CreateDocum(string path, T template, object obj);

      void CreateWord(WordprocessingDocument package, T template, object obj);
   }
}
