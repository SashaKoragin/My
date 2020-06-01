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


    }
}
