using System;
using System.Diagnostics;
using System.IO;
using EfDatabase.Inventarization.BaseLogica.AddObjectDb;
using LibaryDocumentGenerator.Documents.Template;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestIFNSLibary.Inventarka;

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
            EfDatabase.Inventarization.BaseLogica.AddObjectDb.AddObjectDb add =new AddObjectDb();
            add.IsProcessComplete(1,false);
        }
    }
}