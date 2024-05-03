using System;
using System.IO;
using EfDatabase.AddTable.AddOutBdk;
using EfDatabase.AddTable.PredPro;
using EfDatabase.AddTable.AddTemplate;
using LibaryXMLAuto.ModelServiceWcfCommand.AngularModel;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAuto.Reports.FullTemplateSheme;

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

    }
}
