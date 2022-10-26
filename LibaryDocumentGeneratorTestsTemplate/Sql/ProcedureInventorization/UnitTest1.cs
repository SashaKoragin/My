using System;
using System.Linq;
using EfDatabase.Inventory.BaseLogic.ProcessSynchronization;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.SqlModelSelect;
using EfDatabaseParametrsModel;
using EfDatabaseXsdSupportNalog;
using LibaryDocumentGeneratorTestsTemplate.SoapBynary;
using LibaryXMLAuto.ModelXmlAuto.MigrationReport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestIFNSLibary.Inventarka;
using TestIFNSLibary.ServiceRest;
using SqlLibaryIfns.PingIp;
using Type = System.Type;

namespace LibaryDocumentGeneratorTestsTemplate.Sql.ProcedureInventorization
{
    [TestClass]
    public class ProcedureInventorization
    {

        [TestMethod]
        public void TestBool()
        {

                try
                {
                   // var selectEvent = new Select();
                    var eventParameters = "	\\\\i7751-sys030; 10.177.172.; 7751-00-099; Qwerty12345!";
                    //DateTime date = DateTime.Now;
                    //if (date.Hour == eventParameters.HoursX && date.Minute == eventParameters.MinutesX)
                    //{
                        try
                        {
                             Inventarka inventory = new Inventarka();
                            var parameter = eventParameters.Split(';');
                            var processFindPrintServer = new ProcessSynchronizationPrintServer(parameter[0].Trim(), parameter[1].Trim());
                            var result = processFindPrintServer.SynchronizationPrintServerStart();
                            if (result != null)
                            {
                                var resultFilter = result.Where(r => r.StatusFindPrintServerAndSynchronization == 1).ToList();
                                var selectModel = new Select();
                                var addObjectDb = new EfDatabase.Inventory.BaseLogic.AddObjectDb.AddObjectDb();
                                var idUser = selectModel.SelectIdUser(parameter[2].Trim());
                                var typeModel = new[] { 2, 5, 7, 8, 27 }; //Типы моделей больших аппаратов
                                foreach (var synchronizationPrintServer in resultFilter)
                                {
                                    try
                                    {
                                        var allSerialNumberModel = selectModel.TypeModelAndIdSelect(synchronizationPrintServer.SerNumberPrintServer);
                                        if ((synchronizationPrintServer.IsTonerLow &&
                                             synchronizationPrintServer.HasToner &&
                                             !synchronizationPrintServer.IsSupportApplication && allSerialNumberModel != null)
                                            ^
                                            (!synchronizationPrintServer.IsTonerLow &&
                                             !synchronizationPrintServer.HasToner &&
                                             !synchronizationPrintServer.IsSupportApplication && allSerialNumberModel != null))
                                        {

                                            //Создать заявку
                                            var modelSto = new ModelParametrSupport()
                                            {
                                                Discription = $"Добрый день! Требуется замена ТОНЕРА на {allSerialNumberModel.Item} {allSerialNumberModel.NameModel} в каб. {allSerialNumberModel.NumberKabinet} сер.№ {allSerialNumberModel.SerNum} , сервисный.№ {allSerialNumberModel.ServiceNum} ,инв.№ {allSerialNumberModel.InventarNum}",
                                                IdMfu = allSerialNumberModel.Id,
                                                IdUser = idUser,
                                                Login = parameter[2].Trim(),
                                                Password = parameter[3].Trim(),
                                                IdTemplate = (allSerialNumberModel.Item == "МФУ") ? 6 : 5
                                            };
                                            var resultStep3 = inventory.ServiceSupport(modelSto);
                                            synchronizationPrintServer.IsSupportApplication = true;
                                            synchronizationPrintServer.DateCreateSupportApplication = DateTime.Now;
                                            if (resultStep3.Result.Step3ResponseSupport != null)
                                            {
                                                //  Loggers.Log4NetLogger.Info(new Exception($"Создали автоматически заявку на СТП! Для оборудования {allSerialNumberModel.NameModel} сер.№ {allSerialNumberModel.SerNum}"));
                                            }

                                        }
                                        if (!synchronizationPrintServer.IsTonerLow && synchronizationPrintServer.HasToner)
                                        {
                                            //Обнулить результат
                                            synchronizationPrintServer.IsSupportApplication = false;
                                            synchronizationPrintServer.DateCreateSupportApplication = null;
                                        }
                                        addObjectDb.UpdateSynchronizationPrintServer(synchronizationPrintServer);
                                    }
                                    catch (Exception exception)
                                    {
                                        //  Loggers.Log4NetLogger.Error(exception);
                                    }
                                }
                                selectModel.Dispose();
                                addObjectDb.Dispose();
                            }
                        }
                        catch (Exception exception)
                        {
                           // Loggers.Log4NetLogger.Error(exception);
                        }
                   // }
                  //  selectEvent.Dispose();
                }
                catch (Exception ex)
                {
                    // Loggers.Log4NetLogger.Error(ex);
                }
        }

        [TestMethod]
        public void Test()
        {

            //var t = Convert.FromBase64String("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");
            //using (MemoryStream ms = new MemoryStream(t))
            //{
            //    StreamReader readStream = new StreamReader(ms);
            //    BinaryWriter b = new BinaryWriter(readStream.BaseStream);
            //  //  string data = readStream.ReadToEnd();
            //    result.Write(b);
            //   var tt = result.ToResult();
            //}


           
            NBFSNet NBFS = new NBFSNet();
            //   var filetext = File.ReadAllText("C:\\Users\\7751-00-099\\Desktop\\binary.txt");
            //  var en = Convert.ToBase64String(NBFS.EncodeBinaryXML(System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String("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"))));
            // var b = Convert.FromBase64String("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");
         //  var y = BitConverter.ToString(t);
         //   string r = Encoding.UTF8.GetString(t);

            //using (var receiveStream = Response.GetResponseStream())
            //{
            //    StreamReader readStream  new StreamReader(receiveStream);
            //    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet) ? new StreamReader(receiveStream) : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
            //    string data = readStream.ReadToEnd();
            //    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            //    document.LoadHtml(data);
            //    HtmlNode formNode = document.DocumentNode.SelectSingleNode(findNode);

            //    inputParameter = formNode.Elements("input").Where(type => type.GetAttributeValue("type", "hidden") == "hidden").Cast<HtmlNode>()
            //        .Select(elem => $"{elem.GetAttributeValue("name", "default")}={elem.GetAttributeValue("value", "default")}")
            //        .Aggregate((element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "&") + next);
            //    if (isStep3)
            //    {
            //        inputParameterStep3 = formNode.Elements("input").Where(type => type.GetAttributeValue("type", "hidden") == "hidden").Cast<HtmlNode>()
            //            .Select(elem => $"------WebKitFormBoundaryTwIa3JXXvh1tOcrg\r\nContent-Disposition: form-data; name=\"{elem.GetAttributeValue("name", "default")}\"\r\n\r\n{elem.GetAttributeValue("value", "default")}")
            //            .Aggregate((element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "\r\n") + next);
            //    }
            //    readStream.Close();
            //    receiveStream.Close();
            //}


            var dec = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(NBFS.DecodeBinaryXML(
             Convert.FromBase64String("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"))));
            //H4sIAJuh5mEA / + 
        }

        [TestMethod]
        public void TestProcedureIpAdress()
        {

            //var arrLetterStatus = new string[3];
            //arrLetterStatus[1] = "B";
            //arrLetterStatus[2] = "ОЧ";
            //var notCount = new[] { "ОЧ", "B" };
            var selectEvent = new Select();

            var process = selectEvent.SelectProcessAndParameters(1);
            DateTime date = DateTime.Now;
            //  if (date.Hour == eventParameters.HoursX && date.Minute == eventParameters.MinutesX)
            //   {
                var parameters = process.ProcessAndParameters.Select(x => x.ParameterEventProcess).ToList();
       
         //   }
            selectEvent.Dispose();
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
            inv.CreateJournalAis3(2021,23,true);
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

