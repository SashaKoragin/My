using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace LibaryWordTemplate.CreateTemplate.Word.MarginDoc
{
   public class MarginDoc
    {
        /// <summary>
        /// Разметка документа по гранницам
        /// </summary>
        /// <param name="doc">Документ</param>
        /// <returns></returns>
        public DocX MarginDocument(DocX doc)
        {
            doc.MarginTop = 70;
            doc.MarginBottom = -70f;
            doc.MarginRight = 40;
            return doc;
        }
    }
}
