using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDocumentGenerator.Documents.Model
{
   public class MigrationReportModel : IModel
    {
        public object Report { get; set; }
        public LibaryXMLAutoReports.FullTemplateSheme.Document DocumentTemplate { get; set; }
        public string PathSave { get; set; }
        public string ConectionStringTemplate { get; set; }

        //public MigrationReportModel(string connectionstringtemplate, string path, object model)
        //{
        //    PathSave = path;
        //    DocumentTemplate = template.Template(connectionstringtemplate, setting);
        //}
    }
}
