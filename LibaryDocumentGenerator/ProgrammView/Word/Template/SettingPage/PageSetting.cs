using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.PageGenerator;

namespace LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage
{
   public class PageSetting
    {
        /// <summary>
        /// Создания настройки страницы надо создавать первым
        /// </summary>
        /// <param name="mainDocument">Документ</param>
        /// <returns></returns>
        public Body AddSetting(MainDocumentPart mainDocument)
        {
            Body body = new Body();
            var page = new PageGenerate();
            page.SettingPage(ref body, mainDocument);
            return body;
        }
        /// <summary>
        /// Для генерации параграфа с Разрывом
        /// </summary>
        /// <param name="mainDocument">Документ</param>
        /// <returns></returns>
        public Body ParagraphSetting(MainDocumentPart mainDocument)
        {
            Body body = new Body();
            var page = new PageGenerate();
            page.SettingParagraph(ref body, mainDocument);
            return body;
        }
        /// <summary>
        /// Параметры документа горизонтальные
        /// </summary>
        /// <returns></returns>
        public Body ParametrPageHorizont()
        {
            Body body = new Body();
            var page = new PageGenerate();
            page.ParamInvoiceDocument(ref body);
            return body;

        }
        /// <summary>
        /// Параметры вертикального выравнивания
        /// </summary>
        /// <returns></returns>
        public Body DocumentSettingVertical(PageMargin margin = null)
        {
            Body body = new Body();
            var page = new PageGenerate();
            page.DocumentVerticalMarginStandart(ref body,margin);
            return body;
        }
        /// <summary>
        /// Параметры документа горизонтальные
        /// </summary>
        /// <returns></returns>
        public Body ParametrPageHorizontEditMargin(PageMargin margin)
        {
            Body body = new Body();
            var page = new PageGenerate();
            page.ParamDocumentEditMargin(ref body,margin);
            return body;

        }
        /// <summary>
        /// Добавление новой горизонтальной страницы в середине документа
        /// </summary>
        /// <returns></returns>
        public Body AddHorizontPage()
        {
            Body body = new Body();
            var page = new PageGenerate();
            Paragraph paragraph = new Paragraph();
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 11907U, Height = 16839U };
            PageMargin pageMargin = new PageMargin() { Top = 1135, Right = 567, Bottom = 567, Left = 1135 };
            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            paragraphProperties.Append(sectionProperties);
            BookmarkStart bookmarkStart = new BookmarkStart();
            BookmarkEnd bookmarkEnd = new BookmarkEnd();
            paragraph.Append(paragraphProperties);
            paragraph.Append(bookmarkStart);
            paragraph.Append(bookmarkEnd);
            body.Append(paragraph);
            page.ParamDocumentEditMargin(ref body, new PageMargin() { Top = 567, Right = 200, Bottom = 567, Left = 200 });
            return body;
        }
        /// <summary>
        /// Генерация страниц для принтера Zebra
        /// </summary>
        /// <param name="margin">Отступы</param>
        /// <returns></returns>
        public Body AddDocumentZebraPrinter(PageMargin margin = null)
        {
            Body body = new Body();
            var page = new PageGenerate();
            page.ParamDocumentZebraPrinter(ref body, margin);
            return body;
        }
    }
}
