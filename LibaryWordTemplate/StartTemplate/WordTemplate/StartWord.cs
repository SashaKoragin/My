using System;
using LibaryWordTemplate.CreateTemplate.Word.BodyWord;
using LibaryWordTemplate.CreateTemplate.Word.EndWord;
using LibaryWordTemplate.CreateTemplate.Word.MarginDoc;
using Xceed.Words.NET;
using LibaryWordTemplate.Template.ModelTemplate;

namespace LibaryWordTemplate.StartTemplate.WordTemplate
{
   public class StartWord
    {
        /// <summary>
        /// Старт сбора шаблона для документа отправки БДК
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения с Шаблонами на сервере</param>
        /// <param name="connectionstringtaxes">Строка соединения с данными</param>
        /// <param name="path">Путь сохранения документов</param>
        public void StartWordBdk(string connectionstringtemplate, string connectionstringtaxes, string path)
        {
            try
            {
                CreateTemplate.Word.HeaderWord.Headers headers = new CreateTemplate.Word.HeaderWord.Headers();
                BodyWord body = new BodyWord();
                EndWord end = new EndWord();
                MarginDoc margin = new MarginDoc();
                ModelTemplate model = new ModelTemplate();
                var templatemodel = model.ModelOutBdk(connectionstringtemplate, connectionstringtaxes, path);
                foreach (var fn71 in templatemodel.Report.FN71)
                {
                    try
                    {
                        string namepath = templatemodel.PathSave + fn71.N279 + "_CountBDK_" + fn71.FN1723_2.Length +
                                          model.ConstWord().Formatword;
                        var doc =
                            body.BodyText(
                                headers.HeadersWordBdk(margin.MarginDocument(DocX.Create(namepath)),
                                    templatemodel.DocumentTemplate, fn71.N279, fn71.N280),
                                templatemodel.DocumentTemplate);
                        body.BodyTable(ref doc, fn71.FN1723_2);
                        end.End(ref doc, templatemodel.DocumentTemplate, fn71.FN1723_2.Length);
                        doc.Save();
                    }
                    catch (Exception ex)
                    {
                        Loggers.Log4NetLogger.Error(ex);
                    }
                }
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
    }
}
