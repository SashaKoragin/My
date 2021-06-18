using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;

namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
   public interface ITemplateExcel<in T>
    {
        string FullPathDocumentWord { get; set; }
        Stream FileArray();

        void CreateDocument(string path, T template, object obj = null);

        void CreateExcel(SpreadsheetDocument document, T template, object obj = null);
    }
}
