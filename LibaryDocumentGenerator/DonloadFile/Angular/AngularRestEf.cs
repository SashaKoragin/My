using System;
using System.IO;
using EfDatabase.AddTable.AddKrsb;
using EfDatabase.AddTable.AddOutBdk;
using EfDatabase.AddTable.PredPro;
using EfDatabase.AddTable.AddTemplate;
using LibaryXMLAuto.ModelServiceWcfCommand.AngularModel;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using LibaryXMLAuto.Reports.FullTemplateSheme;
using LibaryXMLAutoModelServiceWcfCommand.TestIfnsService;
using LibaryXMLAutoReports.DelaCreate;
using SqlLibaryIfns.SqlSelect.ModelSqlFullService;
using SqlLibaryIfns.SqlZapros.SqlConnections;

namespace LibaryDocumentGenerator.DonloadFile.Angular
{
  public class AngularRestEf
    {
        /// <summary>
        /// Выгрузка файла из БД
        /// </summary>
        /// <param name="angular">Модель выгрузки файла из БД</param>
        /// <returns></returns>
        public Stream DonloadFile(AngularModelFileDonload angular)
        {
            try
            {
            switch (angular.Id)
            {
                case 1:
                    var savefile = new AddOutBdk();
                    return savefile.DonloadFileOutBdk(angular.Guid);
                case 2:
                    var predproverka = new PredProverkaTempl();
                    return predproverka.DonloadTemplate(1);
                default:
                    return null;
            }
            }
            catch (Exception e)
            {

               Loggers.Log4NetLogger.Error(e);
                return null;
            }
        }
        /// <summary>
        /// Метод сохранения шаблона в БД
        /// </summary>
        /// <param name="angular">Сам шаблон</param>
        /// <returns></returns>
        public string TemplateSave(AngularTemplate angular)
        {
            try
            {
                var save =new AddTemplate();
                return save.SaveTemplate(angular);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return e.Message;
            }
        }
        /// <summary>
        /// Создание процесса Прием КРСБ Наследника Массово
        /// </summary>
        /// <param name="conectstring">Строка соединения</param>
        /// <param name="setting">Настройки</param>
        /// <returns></returns>
        public string CreateKrsb(string conectstring, FullSetting setting)
        {
            try
            {
                var delocreate = new AddKrsb();
                var sqlconnect = new SqlConnectionType();
                SerializeJson serializeJson = new SerializeJson();
                if (setting.DeloPriem.DelaPriem.Count > 0)
                {
                    delocreate.CreateDelo(setting.DeloPriem.DelaPriem);
                    sqlconnect.StartingProcedure<string,string>(conectstring,((ServiceWcf)
                            sqlconnect.SelectFullParametrSqlReader(conectstring,
                                ModelSqlFullService.ProcedureSelectParametr, typeof(ServiceWcf),
                                ModelSqlFullService.ParamCommand("19"))).ServiceWcfCommand.Command);
                   return serializeJson.JsonLibrary((CreateDela)sqlconnect.SelectFullParametrSqlReader<string, string>(conectstring,
                         ((ServiceWcf)
                            sqlconnect.SelectFullParametrSqlReader(conectstring,
                                ModelSqlFullService.ProcedureSelectParametr, typeof(ServiceWcf),
                                ModelSqlFullService.ParamCommand("20"))).ServiceWcfCommand.Command, typeof(CreateDela)));
                }
                Loggers.Log4NetLogger.Error(new Exception("Нет дел для создания процессов!!!"));
                return null;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return null;
            }
        }
    }
}
