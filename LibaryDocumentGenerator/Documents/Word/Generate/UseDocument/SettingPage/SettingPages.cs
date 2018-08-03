using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Page;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.SettingPage
{
   public class SettingPages
    {
        /// <summary>
        /// Создания настройки страницы надо создавать первым
        /// </summary>
        /// <param name="mainDocument">Документ</param>
        /// <returns></returns>
        public Body AddSetting(MainDocumentPart mainDocument)
        {
            Body body = new Body();
            Page page = new Page();
            page.SettingPage(ref body, mainDocument);
            return body;
        }
    }
}
