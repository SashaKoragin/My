using System.IO;
using DocumentFormat.OpenXml.Packaging;
namespace LibaryDocumentGenerator.Documents.Template
{
   public interface ITemplate<in T>
   {

      string FullPathDocumentWord { get; set; }
      Stream FileArray();
      void CreateDocument(string path, T template, object obj = null);

      void CreateWord(WordprocessingDocument package, T template, object obj = null);
   }
}
