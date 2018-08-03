using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.FottersGenerator
{
   public class FottersAdd
   {

        /// <summary>
        /// Создать путь к колонтитулу
        /// </summary>
        /// <param name="package">Документ</param>
        /// <returns></returns>
        public static FooterPart AddFotters(MainDocumentPart mainDocument)
        {
            FooterPart foters = mainDocument.AddNewPart<FooterPart>();
            return foters;
        }


    }
}
