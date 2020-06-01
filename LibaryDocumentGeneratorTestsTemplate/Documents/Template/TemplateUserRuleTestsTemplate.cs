using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabaseAutomation.Automation.Base;
using LibaryDocumentGenerator.Documents.Template;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LibaryDocumentGeneratorTestsTemplate.Documents.Template
{
    [TestClass()]
    public class TemplateUserRuleTestsTemplate
    {
        [TestMethod()]
        public void CreateDocumTestTemplate()
        {
            var temp = new TemplateUserRule();
            temp.CreateDocum(@"C:\", null, null);
        }
        [TestMethod()]
        public void Inven()
        {
            AddObjectDb add =new AddObjectDb();
            add.IsProcessComplete(1,false);
        }
        [TestMethod()]
        public void TestReportNote()
        {
            var model = new EfDatabaseAutomation.Automation.BaseLogica.ModelGetPost.ModelGetPost();
            var card = model.CardUi("9721015120");
            ReportNote report = new ReportNote();
            report.CreateDocum(@"D:\", card, null);
        }
    }
}