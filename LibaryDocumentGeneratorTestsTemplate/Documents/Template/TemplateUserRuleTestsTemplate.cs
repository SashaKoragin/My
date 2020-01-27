using EfDatabase.Inventory.BaseLogic.AddObjectDb;
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
    }
}