using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryXMLAutoReports.TemplateSheme;
using Xceed.Words.NET;
using Font = Xceed.Words.NET.Font;

namespace LibaryWordTemplate.CreateTemplate.Word.EndWord
{
   public class EndWord
    {
        /// <summary>
        /// Генерация окончания документа кто подписал
        /// </summary>
        /// <param name="doc">Сформированный документ</param>
        /// <param name="document">Шаблон</param>
        /// <param name="counttable">Данный параметр нужно переделать определить где конец документа</param>
        public void End(ref DocX doc,Document document, int counttable)
        {
            doc.InsertParagraph().SetLineSpacing(LineSpacingType.Line, 3);
            var tablesingle = doc.AddTable(1, 2);
            tablesingle.Design = TableDesign.TableNormal;
            tablesingle.AutoFit = AutoFit.Contents;
            tablesingle.Rows[0].Cells[0].Paragraphs[0].Append(document.NameDocument.Template.Stone.Stone1).Font(new Font("Times New Roman")).FontSize(13d).AppendLine(document.NameDocument.Template.Stone.Stone2).Font( new Font("Times New Roman")).FontSize(13d).Alignment = Alignment.left;
            tablesingle.Rows[0].Cells[1].Paragraphs[0].AppendLine().AppendLine().Append(document.NameDocument.Template.Stone.Stone3).Font(new Font("Times New Roman")).FontSize(13d).Alignment = Alignment.right;
            tablesingle.Rows[0].Cells[1].MarginLeft = 100;
            doc.InsertTable(tablesingle);
            Footers(ref doc,document,counttable);
        }
        /// <summary>
        /// Постановка табуляции в конце документа нужно думеть как найти конец документа
        /// </summary>
        /// <param name="doc">Сформированный документ</param>
        /// <param name="document">Шаблон</param>
        /// <param name="counttable">Данный параметр нужно переделать определить где конец документа</param>
        public void Footers(ref DocX doc, Document document,int counttable)
        {
            doc.AddFooters();
            doc.DifferentFirstPage = true;
            doc.DifferentOddAndEvenPages = true;
            if (counttable > 24)
            {
                doc.Footers.Even.InsertParagraph(document.NameDocument.Template.Stone.Stone4)
                   .Font(new Font("Times New Roman"))
                   .FontSize(10d)
                   .Color(Color.Black)
                   .AppendLine(document.NameDocument.Template.Stone.Stone5)
                   .Font(new Font("Times New Roman"))
                   .FontSize(10d)
                   .Color(Color.Black)
                   .Alignment = Alignment.left;
            }
            else
            {
                doc.Footers.First.InsertParagraph(document.NameDocument.Template.Stone.Stone4)
                   .Font(new Font("Times New Roman"))
                   .FontSize(10d)
                   .Color(Color.Black)
                   .AppendLine(document.NameDocument.Template.Stone.Stone5)
                   .Font(new Font("Times New Roman"))
                   .FontSize(10d)
                   .Color(Color.Black)
                   .Alignment = Alignment.left;
            }
        }
    }
}
