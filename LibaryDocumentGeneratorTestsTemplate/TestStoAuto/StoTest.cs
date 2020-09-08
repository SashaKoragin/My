using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabaseXsdSupportNalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestIFNSLibary.Inventarka;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.TimeEvent;

namespace LibaryDocumentGeneratorTestsTemplate.TestStoAuto
{
    [TestClass]
   public class StoTest
    {

        [TestMethod]
        public void TestAutoSto()
        {
            DateTime date = DateTime.Now;

                    if (date.Day == 08)
                    {
                        var description = @"Добрый день!" +
                                          "Требуется замена ТОНЕРА на МФУ Xerox VersaLink B7030 в каб.237 сер.№3399683695," +
                                          "серв.№77068-4-403-3399683695," +
                                          "инв.№22-100135(по договоренности с менеджером Денисом).";
                            var modelSto = new ModelParametrSupport()
                            {
                                Discription = description,
                                IdMfu = 50,
                                IdUser = 96,
                                Login = "7751-00-400",
                                Password = "Acr1at1ve$$",
                                IdTemplate = 7
                            };
                            Inventarka inventory = new Inventarka();
                            var models = inventory.ServiceSupport(modelSupport: modelSto);
                            if (models.Result.Step3ResponseSupport != null)
                            {
                               
                            }
                    }
        }
    }
}
