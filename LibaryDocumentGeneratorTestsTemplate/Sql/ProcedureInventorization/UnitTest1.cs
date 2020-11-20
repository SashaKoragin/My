using System.Linq;
using EfDatabase.Inventory.BaseLogic.Select;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestIFNSLibary.Inventarka;
using TestIFNSLibary.ServiceRest;

namespace LibaryDocumentGeneratorTestsTemplate.Sql.ProcedureInventorization
{
    [TestClass]
    public class ProcedureInventorization
    {
        [TestMethod]
        public void TestProcedureIpAdress()
        {
            //Select auto = new Select();
            //StickerCode sticker = new StickerCode();
            //var technical = auto.SelecTechnic("B80GАGA000847");
            //GenerateBarcode qrCode = new GenerateBarcode();
            //qrCode.GenerateQrCode("", "C:\\Testing\\");
            //sticker.CreateDocum("C:\\Testing\\", technical, null);
            //File.Delete(technical.Name);
           // return sticker.FileArray();
        }
        [TestMethod]
        public void TestTemplateRule()
        {
            ServiceRest rest = new ServiceRest();
            var xml = new LibaryXMLAuto.ReadOrWrite.XmlReadOrWrite();
            UserRules rule = (UserRules)xml.ReadXml("C:\\UserRule.xml", typeof(UserRules));
            var groupelement = rule.User.Where(x => x.Number != "Скрипт").GroupBy(x => new { x.Dates, x.Number, x.Otdel }).Select(x => new { x.Key.Number, x.Key.Dates, x.Key.Otdel }).ToList();
            int i = 0;
            foreach (var gr in groupelement)
            {
                //if (template.Otdel == null)
                //{
                //    template.Otdel = new LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel[groupelement.Count];
                //}
                //template.Otdel[i] = Inventarization.Database.SqlQuery<LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel>(sqlselect.LogicaSelect.SelectUser, new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[0], 1),
                //          new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[1], gr.Otdel.Replace("№ ", "№")),
                //          new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[2], gr.Number)).ToList()[0];
                //template.Otdel[i].Dates = gr.Dates;
                var user = rule.User.Where(userrule => (userrule.Dates == gr.Dates) && (userrule.Number == gr.Number) && (userrule.Otdel == gr.Otdel)).Select(u=>new { u.Dates,u.Fio,u.SysName,u.Dolj,u.Otdel,u.Number}).Distinct().ToList();
                
                //int j = 0;
                foreach (var userule in user)
                {
                  var ruleall =  rule.User.Where( u =>
                     u.Dates == userule.Dates && u.Dolj == userule.Dolj && u.Otdel == userule.Otdel &&
                     u.Fio == userule.Fio && u.SysName == userule.SysName && u.Number == userule.Number).
                     Select(x=>x.Rule).Aggregate((element, next)=>element.Concat(next).ToArray());

                    ruleall.Select(elem => $"{elem.Types}: {elem.Name}").Aggregate(
                        (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : ", ") + next);


                    // var elemt = ruleall.ToList()  //.Select(elem => $"{elem.Types}: {elem.Name}").Aggregate(
                    //    (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : ", ") + next)
                    //if (template.Otdel[i].Users == null)
                    //{
                    //    template.Otdel[i].Users = new LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users[user.Count];
                    //}
                    //template.Otdel[i].Users[j] = Inventarization.Database.SqlQuery<LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users>(sqlselect.LogicaSelect.SelectUser, new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                    //      new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[1], userule.SysName.Split('@')[0]),
                    //      new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).ToList()[0];
                    //template.Otdel[i].Users[j].RuleTemplate = userule.Rule.Select(elem => $"{elem.Types}: {elem.Name}").Aggregate(
                    //    (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : ", ") + next);
                    //template.Otdel[i].Users[j].Pushed = userule.Rule[0].Pushed;
                    //j++;
                }
                i++;
            }
        }
    }
}

