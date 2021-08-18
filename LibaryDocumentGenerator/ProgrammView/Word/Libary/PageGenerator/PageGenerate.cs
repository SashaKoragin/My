
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.PageGenerator
{
   public class PageGenerate
    {

        /// <summary>
        /// Настройка страницы и колонтитул на 1 странице 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="mainDocument"></param>
        public void SettingPage(ref Body body, MainDocumentPart mainDocument)
        {
            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 11900U, Height = 16840U };
            PageMargin pageMargin = new PageMargin() { Top = 567, Bottom = 1134, Right = 567, Left = 1134 };
            TitlePage titlePage = new TitlePage();
            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            sectionProperties.Append(titlePage);
            sectionProperties.Append(new FooterReference() { Type = HeaderFooterValues.Default, Id = mainDocument.GetIdOfPart(mainDocument.FooterParts.FirstOrDefault()) });
            sectionProperties.Append(new FooterReference() { Type = HeaderFooterValues.First, Id = mainDocument.GetIdOfPart(mainDocument.FooterParts.FirstOrDefault()) });
            body.Append(sectionProperties);
        }

        /// <summary>
        /// Настройки параграфа с разрывом
        /// </summary>
        /// <param name="body"></param>
        /// <param name="mainDocument"></param>
        public void SettingParagraph(ref Body body, MainDocumentPart mainDocument)
        {
            Paragraph paragraph = new Paragraph();
            ParagraphProperties paragraphProperties = new ParagraphProperties();

            SectionProperties sectionProperties = new SectionProperties();
            FooterReference footerReference = new FooterReference() { Type = HeaderFooterValues.Default, Id = mainDocument.GetIdOfPart(mainDocument.FooterParts.FirstOrDefault()) };
            SectionType sectionType = new SectionType() { Val = SectionMarkValues.Continuous };
            PageMargin pageMargin = new PageMargin() { Top = 1440, Right = 700U, Bottom = -1400, Left = 1470U, Header = 710U, Footer = 700U, Gutter = 0U };

            sectionProperties.Append(footerReference);
            sectionProperties.Append(sectionType);
            sectionProperties.Append(pageMargin);
            paragraphProperties.Append(sectionProperties);
            paragraph.Append(paragraphProperties);
            body.Append(paragraph);
        }
        /// <summary>
        /// Настройка документа в горизонтальную ориентацию
        /// </summary>
        /// <param name="body">Тело документа для настройки</param>
        public void ParamInvoiceDocument(ref Body body)
        {
            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 16800U, Height = 11900U, Orient = PageOrientationValues.Landscape };
            PageMargin pageMargin = new PageMargin() { Top = 500, Right = 500, Bottom = 500, Left = 500, Header = 710U, Footer = 700U, Gutter = 0U };
            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            body.Append(sectionProperties);
        }
        /// <summary>
        /// Обычный документ вертикальное выравнивание
        /// </summary>
        /// <param name="body"></param>
        public void DocumentVerticalMargin(ref Body body)
        {
            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 11907U, Height = 16839U, Orient = PageOrientationValues.Portrait };
            PageMargin pageMargin = new PageMargin() { Top = 500, Right = 500, Bottom = 500, Left = 500 };
            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            body.Append(sectionProperties);
        }
        /// <summary>
        /// Обычный документ вертикальное выравнивание стандартный размер полей или задается
        /// </summary>
        /// <param name="body">Тело документа</param>
        /// <param name="margin">Выравнивание</param>
        public void DocumentVerticalMarginStandart(ref Body body, PageMargin margin = null)
        {
            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 11907U, Height = 16839U, Orient = PageOrientationValues.Portrait };
            if (margin == null)
            {
                margin = new PageMargin() { Top = 567, Right = 567, Bottom = 567, Left = 1135 };
            }
            sectionProperties.Append(pageSize);
            sectionProperties.Append(margin);
            body.Append(sectionProperties);
        }

        /// <summary>
        /// Настройка документа в горизонтальную ориентацию
        /// </summary>
        /// <param name="body">Тело документа для настройки</param>
        /// <param name="margin"></param>
        public void ParamDocumentEditMargin(ref Body body, PageMargin margin)
        {
            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 16800U, Height = 11900U, Orient = PageOrientationValues.Landscape };
            sectionProperties.Append(pageSize);
            sectionProperties.Append(margin);
            body.Append(sectionProperties);
        }

    }
}
