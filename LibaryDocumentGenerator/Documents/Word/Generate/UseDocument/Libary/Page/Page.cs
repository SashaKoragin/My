using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Page
{
   public class Page
    {
        /// <summary>
        /// Настройки страницы размеры можно вынести в отдельный класс
        /// </summary>
        /// <returns>Настройки страницы</returns>
        public void SettingPage(ref Body body)
        {
            SectionProperties sectionProperties = new SectionProperties() { RsidR = "003E25F4", RsidSect = "00FC3028" };
            PageSize pageSize = new PageSize() { Width = 11900U, Height = 16840U };
            PageMargin pageMargin = new PageMargin() { Top = 1440, Right = 700U, Bottom = -1400, Left = 1470U, Header = 710U, Footer = 700U, Gutter = 0U };
            FooterReference footerReference = new FooterReference() { Type = HeaderFooterValues.Default, Id = "Rda32440226204264" };
            TitlePage titlePage = new TitlePage();

            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            sectionProperties.Append(titlePage);
            sectionProperties.Append(footerReference);
            body.Append(sectionProperties);
        }

        /// <summary>
        /// Настройка страницы и колонтитул на 1 странице 
        /// при создании явных 2 страниц будет ошибка создания с телефонным номером и ФИО колонтитула
        /// </summary>
        /// <param name="body"></param>
        /// <param name="mainDocument"></param>
        public void SettingPage(ref Body body,MainDocumentPart mainDocument)
        {
           
            SectionProperties sectionProperties = new SectionProperties() ;
            PageSize pageSize = new PageSize() { Width = 11900U, Height = 16840U };

            PageMargin pageMargin = new PageMargin() { Top = 1440, Right = 700U, Bottom = -1400, Left = 1470U, Header = 710U, Footer = 700U, Gutter = 0U };
            TitlePage titlePage = new TitlePage();
            FooterReference footerReference = new FooterReference() { Type = HeaderFooterValues.First, Id = mainDocument.GetIdOfPart(mainDocument.FooterParts.FirstOrDefault()) };

            sectionProperties.Append(pageSize);
            sectionProperties.Append(pageMargin);
            sectionProperties.Append(titlePage);
            sectionProperties.Append(footerReference);

            body.Append(sectionProperties);
        }
    }
}
