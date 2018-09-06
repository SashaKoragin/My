
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.PageGenerator
{
   public class PageGenerate
    {

        /// <summary>
        /// Настройка страницы и колонтитул на 1 странице 
        /// при создании явных 2 страниц будет ошибка создания с телефонным номером и ФИО колонтитула
        /// </summary>
        /// <param name="body"></param>
        /// <param name="mainDocument"></param>
        /// <param name="count">Параметр от которого зависит колонтитул</param>
        public void SettingPage(ref Body body, MainDocumentPart mainDocument, int count = 0)
        {

            SectionProperties sectionProperties = new SectionProperties();
            PageSize pageSize = new PageSize() { Width = 11900U, Height = 16840U };

            SectionType section = new SectionType() { Val = SectionMarkValues.Continuous };

            PageMargin pageMargin = new PageMargin() { Top = 1440, Right = 700U, Bottom = -1400, Left = 1470U, Header = 710U, Footer = 700U, Gutter = 0U };
            TitlePage titlePage = new TitlePage();

            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            sectionProperties.Append(titlePage);
            sectionProperties.Append(section);
            sectionProperties.Append(AddFooterReference(mainDocument, count));
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
        /// Ссылка на колонтитул добавление его на страницу
        /// Примечание работает только для 2 страниц если есть 3 тоReference надо менять на Even
        /// </summary>
        /// <param name="mainDocument">Документ откуда вытягиваем колонтитул</param>
        /// <param name="count">Колличество от которого зависит на кокой странице колонтитул</param>
        /// <returns></returns>
        public FooterReference AddFooterReference(MainDocumentPart mainDocument, int count = 0)
        {
            if (count > 19)
            {
                return new FooterReference() { Type = HeaderFooterValues.Default, Id = mainDocument.GetIdOfPart(mainDocument.FooterParts.FirstOrDefault()) };
            }
            return new FooterReference() { Type = HeaderFooterValues.First, Id = mainDocument.GetIdOfPart(mainDocument.FooterParts.FirstOrDefault()) };
        }
    }
}
