using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabaseXsdQrCodeModel;
using EfDatabaseXsdSupportNalog;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
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
        [TestMethod]
        public void CreateQrCodeOffice()
        {
            Select auto = new Select();
            QrCodeOffice office = auto.SelectOffice("357", true);
            GenerateBarcode qrCode = new GenerateBarcode();
            OfficeStikerCode stickerQrOffice = new OfficeStikerCode();
            //Создание qr кодов
            foreach (var qrCodeOffice in office.Kabinet)
            {
                qrCodeOffice.FullPathPng = qrCode.GenerateQrCode("C:\\Testing\\" + qrCodeOffice.IdNumberKabinet, qrCodeOffice.NumberKabinet);
            }
            stickerQrOffice.CreateDocum("C:\\Testing\\QrCodeOffice", office);
            //Удаление всех png
            foreach (var cabinet in office.Kabinet)
            {
                File.Delete(cabinet.FullPathPng);
            }
        }
    }
}
