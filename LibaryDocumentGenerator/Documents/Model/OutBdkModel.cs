using System;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using SqlLibaryIfns.SqlModelReport.Bdk;

namespace LibaryDocumentGenerator.Documents.Model
{
    /// <summary>
    /// Документ OutBdk
    /// </summary>
    public class OutBdkModel : IModel
    {
        public LibaryXMLAutoReports.TemplateSheme.Document DocumentTemplate { get; set; }

        public object Report { get; set; }

        public string PathSave { get; set; }

        public string ConectionStringTemplate { get; set; }

        public OutBdkModel(string connectionstringtemplate, string connectionstringtaxes, string path, FullSetting setting)
        {
            ModelFull modelFull = new ModelFull();
            FileLogica.FileLogica logica = new FileLogica.FileLogica();
            SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate template = new SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate();
            logica.FileDelete(path);
            ConectionStringTemplate = connectionstringtemplate;
            PathSave = path;
            Report = modelFull.ReportBdk(connectionstringtaxes, connectionstringtemplate, setting);
            DocumentTemplate = template.Template(connectionstringtemplate, setting);
        }
    }
}
