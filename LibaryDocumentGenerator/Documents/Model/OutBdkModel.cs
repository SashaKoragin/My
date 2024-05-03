using System;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;

namespace LibaryDocumentGenerator.Documents.Model
{
    /// <summary>
    /// Документ OutBdk
    /// </summary>
    public class OutBdkModel : IModel
    {
        public LibaryXMLAutoReports.FullTemplateSheme.Document DocumentTemplate { get; set; }

        public object Report { get; set; }

        public string PathSave { get; set; }

        public string ConectionStringTemplate { get; set; }

        public OutBdkModel(string connectionstringtemplate, string connectionstringtaxes, string path, FullSetting setting)
        {
           
        }
    }
}
