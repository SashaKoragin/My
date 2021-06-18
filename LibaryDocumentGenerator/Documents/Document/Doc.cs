using System;
using EfDatabase.AddTable.AddOutBdk;
using LibaryDocumentGenerator.Documents.Model;
using LibaryDocumentGenerator.Documents.Template;
using LibaryXMLAutoReports.ReportsBdk;

namespace LibaryDocumentGenerator.Documents.Document
{
    public class Doc
    {
        /// <summary>
        /// Интерфейс моделей данных
        /// </summary>
        public IModel Model { get; set; }
        /// <summary>
        /// Интерфейс шаблонов данных
        /// </summary>
        public ITemplate<LibaryXMLAutoReports.FullTemplateSheme.Document> Template { get; set; }
        /// <summary>
        /// Метод генерации документа OutBdk нужно добавлять новые методы на генерацию новых документов
        /// </summary>
        public void DocumentOutBdk()
        {
            var data = (Report)Model.Report;
            foreach (var fn71 in data.FN71)
            {
                try
                {
                    string fullpath = Model.PathSave +fn71.N279 + "_CountBDK_" + fn71.FN1723_2.Length + Constant.WordConstant.FormatWord;
                    Template.CreateDocument(fullpath, Model.DocumentTemplate, fn71);
                    var savefile = new AddOutBdk();
                    savefile.SaveFile(fullpath, Model.DocumentTemplate.IdNamedocument);
                    savefile.SaveReestr(fn71);
                    savefile.Dispose();
                }
                catch (Exception e)
                {
                    Loggers.Log4NetLogger.Error(e);
                }
            }
        }
    }
}