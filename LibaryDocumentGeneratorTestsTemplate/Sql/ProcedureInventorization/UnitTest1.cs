using System;
using EfDatabase.Inventarization.BaseLogica.Select;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibaryDocumentGeneratorTestsTemplate.Sql.ProcedureInventorization
{
    [TestClass]
    public class ProcedureInventorization
    {
        [TestMethod]
        public void TestProcedureIpAdress()
        {
            var select = new SelectSql();
            select.ActualIp();
        }
    }
}
