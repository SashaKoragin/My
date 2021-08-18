using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text.RegularExpressions;
using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.MemoReport;
using EfDatabase.ReportCard;
using EfDatabaseAutomation.Automation.Base;
using EfDatabaseAutomation.Automation.BaseLogica.ModelGetPost;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using LibaryDocumentGenerator.Documents.TemplateExcel;
using LibaryXMLAuto.ReadOrWrite;
using LibraryAutoSupportSto.Support.SupportPostGet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlLibaryIfns.SqlSelect.ImnsKadrsSelect;
using SqlLibaryIfns.SqlZapros.SqlConnections;


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

        /// <summary>
        /// Полная карточка
        /// </summary>
        [TestMethod()]
        public void TestReportNote()
        {
            var model = new EfDatabaseAutomation.Automation.BaseLogica.ModelGetPost.ModelGetPost();
            var card = model.CardUi("7729380970", 2021);
            ReportNote report = new ReportNote();
            report.CreateDocument(@"D:\", card, 2021);
        }
        /// <summary>
        /// Только книги покупок-продаж на банк
        /// </summary>
        [TestMethod()]
        public void TestReportBookSales()
        {
            var model = new EfDatabaseAutomation.Automation.BaseLogica.ModelGetPost.ModelGetPost();
            var card = model.CardUiBookSales("2466118876", 2021);
            TemplateBookSalesBank report = new TemplateBookSalesBank();
            report.CreateDocument(@"D:\", card, 2021);
        }

        [TestMethod()]
        public void FindGroupSecurity()
        {
            var support = new CreateTiсketSupport("7751-00-099", "Qwer1234!!!");
            support.Dispose();
        }
        [TestMethod()]
        public void Test()
        {
            using (var users = new UserPrincipal(new PrincipalContext(ContextType.Domain)))
            {
                users.SamAccountName = "7751-00-451";
                using (var searcher = new PrincipalSearcher(users))
                {
                    if (searcher.FindOne() is UserPrincipal user)
                    {
                        var fullPath = user.DistinguishedName.Replace("\\","").Split(',').Where(x => x.Contains("OU=")).Reverse().Aggregate(
                            (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "/") + next).Replace("OU=", "");
                        //return fullPath;
                    }
                }
            }

        }
        [TestMethod()]
        public void Generate()
        {
            //var nds = new LibaryDocumentGenerator.Documents.TemplateExcel.ReportAskNds();
            //nds.CreateDocument("D:\\Testing\\");
            //g.GenerateQrCode();

            var model = new ModelGetPost();
            var card = model.CardUiAskNds("2466118876", 2021);
            model.Dispose();
            if (card != null)
            {
                ReportAskNds report = new ReportAskNds();
                report.CreateDocument("D:\\Testing\\", card, 2021);
                //return report.FileArray();
            }
        }
        [TestMethod()]
        public void GenerateCard()
        {
            ReportCardModel model = new ReportCardModel();
            ReportCard report = new ReportCard();
            report.CreateDocument("D:\\Testing\\", model, 2021);
        }
        [TestMethod()]
        public void MemoGen()
        {
            MemoReport memo = new MemoReport();
            //   memo.CreateDocument("D:\\Testing\\",null,null);
            ModelMemoReport memoReport = new ModelMemoReport() {SelectParameterDocument = new SelectParameterDocument(){IdUser = 33,NameDocument = "RR",NumberDocument = 4,TabelNumber = "7751-00-099",TypeDocument = 1}};
            SelectSql select = new SelectSql();
            SelectImns selectFrames = new SelectImns();
            SqlConnectionType sql = new SqlConnectionType();
            XmlReadOrWrite xml = new XmlReadOrWrite();
            select.SelectMemoReport(ref memoReport);
            var commandOrders = string.Format(selectFrames.LastOrder, memoReport.UserDepartment.SmallTabelNumber);
            var userOrder = sql.XmlString("Data Source=i7751-app020;Initial Catalog=imns51;Integrated Security=True;MultipleActiveResultSets=True", commandOrders);
            if (userOrder != null)
            {
                userOrder = string.Concat("<Orders>", userOrder, "</Orders>");
                memoReport.UserDepartment.Orders = ((Orders)xml.ReadXmlText(userOrder, typeof(Orders)));
            }
            memo.CreateDocument("D:\\Testing\\", memoReport);
        }
    }
}