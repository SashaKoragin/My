using System;
using DocumentFormat.OpenXml.Packaging;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.FottersGenerator
{
   public class FotterGenerate
    {
        /// <summary>
        /// Создать ссылку на колонтитулу
        /// </summary>
        /// <param name="mainDocument">Документ</param>
        /// <returns></returns>
        public static FooterPart AddFotters(MainDocumentPart mainDocument)
        {
            FooterPart foters = mainDocument.AddNewPart<FooterPart>();
            return foters;
        }
    }
}
