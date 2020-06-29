using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text.RegularExpressions;
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
        [TestMethod()]
        public void FindGroupSecurity()
        {
            //Правовой отдел

            using (var domain = new GroupPrincipal(new PrincipalContext(ContextType.Domain, "regions.tax.nalog.ru", "OU=Groups,OU=IFNS7751,OU=UFNS77,DC=regions,DC=tax,DC=nalog,DC=ru")))
            {
                domain.Description = "Правовой отдел";
                using (var searcher = new PrincipalSearcher(domain))
                {
                    var group = searcher.FindOne() as GroupPrincipal;
                    if (group != null)
                    {
                       //  group.Name;
                    }
                }
            }
          //  null;

            //using (var users = new UserPrincipal(new PrincipalContext(ContextType.Domain)))
            //{
            //    users.SamAccountName = "7751-00-400";
            //    using (var searcher = new PrincipalSearcher(users))
            //    {
            //        if (searcher.FindOne() is UserPrincipal user)
            //        {
            //            var fullPath = user.DistinguishedName.Split(',').Where(x=>x.Contains("OU=")).Reverse().Aggregate(
            //                (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "/") + next).Replace("OU=","");
            //        }
            //    }
            //}

        }
    }
}