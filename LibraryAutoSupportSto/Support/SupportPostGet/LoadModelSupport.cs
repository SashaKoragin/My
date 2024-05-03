using System;
using System.Linq;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.MailLogicLotus;
using EfDatabaseXsdSupportNalog;
using SqlLibaryIfns.ZaprosSelectNotParam;

namespace LibraryAutoSupportSto.Support.SupportPostGet
{
   public class LoadModelSupport
    {
        /// <summary>
        /// Группы пользователей в AD
        /// </summary>
        private string PathDomainGroup { get; set; }
        /// <summary>
        /// Сохранение отчетов в автомате
        /// </summary>
        private string SaveReport { get; set; }
        public LoadModelSupport(string pathDomainGroup, string saveReportPath)
        {
            PathDomainGroup = pathDomainGroup;
            SaveReport = saveReportPath;
        }

        /// <summary>
        /// Метод создание заявки в СТО
        /// </summary>
        /// <param name="modelSupport">Параметры для создания заявки в ЭПО</param>
        /// <returns></returns>
        public ModelParametrSupport CreateSupportModelSto(ModelParametrSupport modelSupport)
        {
            if (modelSupport.IdCalendarVks != 0)
            {
                var stpCalender = new MailLogicLotus();
                stpCalender.ModifiedCalender(modelSupport.IdCalendarVks);
                stpCalender.Dispose();
            }
            var support = new CreateTiсketSupport(modelSupport.Login, modelSupport.Password);
            var generate = new GenerateParameterSupport(PathDomainGroup);
            var selectReportPassportTechnique = new SelectReportPassportTechnique(null);
            try
            {
                generate.GenerateTemplateUrlParameter(ref modelSupport);
                generate.IsCheckAllParameter(modelSupport.TemplateSupport
                    .Where(param => param.NameStepSupport == "Step2").ToArray());
                var modelParameterInputStep3 = modelSupport.TemplateSupport.Where(temple =>
                    temple.NameStepSupport == "Step3" && temple.TemplateParametrType != null).ToArray();
                if (modelParameterInputStep3.Length > 0)
                {
                    selectReportPassportTechnique.CreateStoParametersStep3(ref modelSupport, SaveReport);
                }
                modelSupport.Step3ResponseSupport = support.CreateFullSupportTax(modelSupport);
                Loggers.Log4NetLogger.Info(new Exception($"Пользователем {modelSupport.Login} создана заявка по теме {modelSupport.TemplateSupport.FirstOrDefault(description => description.NameGuidParametr == "UF_SERVICE_EXTID")?.HelpParameter}"));
                return modelSupport;
            }
            catch (Exception ex)
            {
                Loggers.Log4NetLogger.Error(ex);
                modelSupport.Error = ex.Message;
                Loggers.Log4NetLogger.Info(new Exception($"Пользователем {modelSupport.Login} была вызвана ошибка по теме {modelSupport.TemplateSupport.FirstOrDefault(description => description.NameGuidParametr == "UF_SERVICE_EXTID")?.HelpParameter}"));
                return modelSupport;
            }
            finally
            {
                generate.Dispose();
                support.Steps(support.Logon, "GET");
                support.Dispose();
                selectReportPassportTechnique.Dispose();
            }
        }
    }
}
