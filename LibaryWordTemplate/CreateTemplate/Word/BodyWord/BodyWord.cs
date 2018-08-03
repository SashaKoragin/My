using LibaryXMLAutoReports.ReportsBdk;
using LibaryXMLAutoReports.TemplateSheme;
using Xceed.Words.NET;

namespace LibaryWordTemplate.CreateTemplate.Word.BodyWord
{
   public class BodyWord
    {
        /// <summary>
        /// Генерация наполнения документа
        /// </summary>
        /// <param name="doc">Сгенерированный документ </param>
        /// <param name="document">Наш шаблон</param>
        /// <returns></returns>
        public DocX BodyText(DocX doc,Document document)
        {

            doc.InsertParagraph().SetLineSpacing(LineSpacingType.Line, 5);
            doc.InsertParagraph()
                    .Append(document.NameDocument.Template.Body.BodyGl1)
                    .Font(new Font("Times New Roman"))
                    .FontSize(13d)
                    .Alignment = Alignment.both;
            return doc;
        }
        /// <summary>
        /// Генерация Таблицы для БДК
        /// </summary>
        /// <param name="doc">Документ </param>
        /// <param name="fn17232">БДК список массив</param>
        public void BodyTable(ref DocX doc,FN1723_2[] fn17232)
        {
            var t = fn17232.Length;
            var t2 = fn17232.GetType().GetProperties().Length;
            var tablebdk = doc.AddTable(fn17232.Length+1, 3);
            var i = 1;
            tablebdk.AutoFit = AutoFit.Contents;
            tablebdk.Rows[0].Cells[0].Width = 40;
                tablebdk.Rows[0].Cells[0].Paragraphs[0].Append("Дата отправки").Bold().Font(new Font("Times New Roman")).FontSize(13d).Alignment = Alignment.center;
                tablebdk.Rows[0].Cells[1].Paragraphs[0].Append("Наименование контейнера").Bold().Font(new Font("Times New Roman")).FontSize(13d).Alignment = Alignment.center;
                tablebdk.Rows[0].Cells[2].Paragraphs[0].Append("GUID").Bold().Font(new Font("Times New Roman")).FontSize(13d).Alignment = Alignment.center;
            foreach (var fn172321 in fn17232)
            {
                tablebdk.Rows[i].Cells[0].Paragraphs[0].Append(fn172321.D85.ToString("dd-MM-yyyy")).Font(new Font("Times New Roman")).FontSize(12d).Alignment = Alignment.center;
                tablebdk.Rows[i].Cells[1].Paragraphs[0].Append(fn172321.D981).Font(new Font("Times New Roman")).FontSize(12d).Alignment = Alignment.center;
                tablebdk.Rows[i].Cells[2].Paragraphs[0].Append(fn172321.GUID).Font(new Font("Times New Roman")).FontSize(11d).Alignment = Alignment.center;
                i++;
            }
          doc.InsertTable(tablebdk);
        }
    }
}
