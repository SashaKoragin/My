using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb;
using EfDatabase.Inventory.ReportXml.ModelAksiok;
using EfDatabase.ModelAksiok.Aksiok;
using LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem;
using Newtonsoft.Json;


namespace LibaryDocumentGeneratorTestsTemplate.Aksiok
{
    [TestClass()]
   public class TestAksiokToModel
   {
       private char[] dictionaryRu = {
           'А','а','Б','б','В','в','Г','г','Д','д','Е','е','Ё','ё',
           'Ж','ж','З','з','И','и','Й','й','К','к','Л','л','М','м',
           'Н','н','О','о','П','п','Р','р','С','с','Т','т','У','у',
           'Ф','ф','Х','х','Ц','ц','Ч','ч','Ш','ш','Щ','щ','Ъ','ъ',
           'Ь','ь','Ы','ы','Э','э','Ю','ю','Я','я','№','«','»'
       };
        public HttpWebRequest Request { get; set; }

        /// <summary>
        /// Ответ с Support
        /// </summary>
        private HttpWebResponse Response { get; set; }

        /// <summary>
        /// Параметры файла
        /// </summary>
        private byte[] DatesBytesFile { get; set; }

        [TestMethod()]
        public void TestDeserializationAksiok()
        {
            var stringsName = "11.11.2021";
            var y = DateTime.ParseExact(stringsName, "dd.MM.yyyy", null);
        }

        [TestMethod()]
        public void TestDeserializationAksiok2()
        {
            var userLogin = "7751-00-099";
            var passwordUser = "Qwerty12345678!";
            var aksiok = new AksiokPostGetSystem(userLogin, passwordUser);
            aksiok.StartUpdateAksiok();
        }
   }
}
