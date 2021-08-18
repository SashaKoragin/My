using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.SqlModelSelect;
using EfDatabase.ReportCard;
using EfDatabaseAutomation.Automation.BaseLogica.SqlSelect.ProcedureParametr;
using EfDatabaseAutomation.Automation.BaseLogica.SqlSelect.XsdDTOSheme;
using EfDatabaseErrorInventory;
using EfDatabaseParametrsModel;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using LibaryXMLAuto.ModelXmlAuto.MigrationReport;
using LibaryXMLAuto.ReadOrWrite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlLibaryIfns.SqlSelect.ImnsKadrsSelect;
using SqlLibaryIfns.SqlZapros.SqlConnections;
using TestIFNSLibary.Inventarka;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.ServiceRest;
using Type = System.Type;

namespace LibaryDocumentGeneratorTestsTemplate.Sql.ProcedureInventorization
{
    [TestClass]
    public class ProcedureInventorization
    {
        [TestMethod]
        public void TestProcedureIpAdress()
        {

            var arrLetterStatus = new string[3];
            arrLetterStatus[1] = "B";
            arrLetterStatus[2] = "ОЧ";
            var notCount = new[] { "ОЧ", "B" };
       
        }
        [TestMethod]
        public void TestTemplateRule()
        {

            //var t = CultureInfo.CreateSpecificCulture("ru-Ru").DateTimeFormat.MonthGenitiveNames;
            ServiceRest rest = new ServiceRest();
            var xml = new LibaryXMLAuto.ReadOrWrite.XmlReadOrWrite();
            UserRules rule = (UserRules)xml.ReadXml("D:\\UserRule.xml", typeof(UserRules));
            var t =   rest.GenerateTemplateRule(rule);
        }
        [TestMethod]
        public void TestAct()
        {
            Inventarka inv = new Inventarka();
            inv.CreateJournalAis3(2021);
        }
        /// <summary>
        /// Загрузка информации о ролях и пользователях
        /// </summary>
        [TestMethod]
        public void TestLoadTemplate()
        {
            ServiceRest rest = new ServiceRest();
            var xml = new LibaryXMLAuto.ReadOrWrite.XmlReadOrWrite();
            InfoRuleTemplate infoRuleTemplate = (InfoRuleTemplate)xml.ReadXml("D:\\InfoRuleTemplate.xml", typeof(InfoRuleTemplate));
            var t = rest.LoadInfoTemplateToDataBase(infoRuleTemplate);
        }
        /// <summary>
        /// Тест динамического создания отчета
        /// </summary>
        [TestMethod]
        public void SqlModelProcedureLogic()
        {
            LogicaSelect logic = new LogicaSelect();
            logic.Id = 29;
            logic.IsResultXml = true;
            logic.SelectUser = "Select (Select TableTemplate.Name as Names, TableTemplate.Category as Category, (Select distinct TableSystems.Name+'\'+TableFolders.Name+'\'+TableTasks.Name as Path, TableTasks.TypeTask as Type From TableAllModel Join TableTasks on TableTasks.IdTasks = TableAllModel.IdTasks Join TableFolders on TableFolders.IdFolders = TableAllModel.IdFolders Join TableSystems on TableSystems.IdSystems = TableAllModel.IdSystems Where TableAllModel.IdTemplate = TableTemplate.IdTemplate For Xml Auto,Type) From TableTemplate Where AllTemplateAndTree.IdTemplate = TableTemplate.IdTemplate For Xml Auto,Type) From AllTemplateAndTree  Group by IdTemplate,Name,Category Having IdTemplate = max(IdTemplate) For xml Auto,ROOT('AllTechnicalUsersAndOtdelAndTreeAis3')";
          //var model = (string)typeof(FullSelectModelInventory).GetMethod("SqlModelInventory")
          //      ?.Invoke(new FullSelectModelInventory(), new object[] { logic });
         Type type = Type.GetType($"EfDatabaseParametrsModel.AllTechnicalUsersAndOtdelAndTreeAis3, EfDatabase");
          if (logic.SelectUser != null)
          {

                //  var type = db.GetType($"EfDatabaseErrorInventory.AllTechnics");
                var m = (string) typeof(FullSelectModelInventory).GetMethod("SqlModelInventory")?.MakeGenericMethod(type).Invoke(new FullSelectModelInventory(), new object[] { logic });
              //    if (model.ParametrsSelect.Id == 12)
              //    {
              //        return;
              //    }
              //    return (ModelSelect)typeof(SqlSelect).GetMethod("ResultSelectProcedure")?.MakeGenericMethod(type)
              //        .Invoke(new SqlSelect(), new object[] { model });
              //}
          }
          //    EfDatabaseErrorInventory.AllTechnics п = new EfDatabaseErrorInventory.AllTechnics();
           //   EfDatabase.Inventory.SqlModelSelect.FullSelectModelInventory select = new FullSelectModelInventory();
            //select.SqlModelInventory<AllTechnics>(logic);
        }
        [TestMethod]
        public void TestIsHoliday()
        {
            var dateSign = new DateTime(2021, 1, 3);
            var date = dateSign.AddWorkdays(1);
             date = dateSign.AddWorkdays(-1);
             date = dateSign.AddWorkdays(-3);
             date = dateSign.AddWorkdays(3);

        }
    }
}

