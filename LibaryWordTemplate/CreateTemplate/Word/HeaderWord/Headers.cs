using System;
using System.Drawing;
using LibaryXMLAutoReports.TemplateSheme;
using Xceed.Words.NET;
using BorderStyle = Xceed.Words.NET.BorderStyle;
using Font = Xceed.Words.NET.Font;

namespace LibaryWordTemplate.CreateTemplate.Word.HeaderWord
{
   public class Headers
    {
        /// <summary>
        /// Шапка документа по умолчанию
        /// </summary>
        /// <param name="doc">Документ созданный документа</param>
        /// <param name="document">Шаблон документа</param>
        /// <param name="n279">Номер инспекции по умолчанию отсутствует</param>
        /// <param name="n280">Наименование плательщика по умолчанию отсутствует</param>
        /// <param name="arms">Сгенерировать герб на документе по умолчанию отсутствует</param>
        /// <returns>Возвращаем документ с шапкой</returns>
        public DocX HeadersWordBdk(DocX doc,Document document, string n279=null,string n280=null, bool arms=false )
        {
            var table = doc.AddTable(4, 4);
            table.AutoFit = AutoFit.Contents;
            table.Design = TableDesign.TableNormal;
            table.Rows[0].MergeCells(0, 2);
            table.Rows[0].Cells[0].Width = 200;
            table.Rows[2].MergeCells(0, 2);
            table.Rows[3].MergeCells(1, 2);
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade1).Bold().Font(new Font("Times New Roman")).FontSize(10d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade2).Font(new Font("Times New Roman")).FontSize(10d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade3).Bold().Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade4).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade5).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade6).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade7).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade8).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade9).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[0].Paragraphs[0].AppendLine(document.NameDocument.Template.Headers.TextHeade10).Font(new Font("Times New Roman")).FontSize(8d).Alignment = Alignment.center;
            table.Rows[0].Cells[1].Paragraphs[0].AppendLine().AppendLine().Font(new Font("Times New Roman")).Append(n280).Font(new Font("Times New Roman")).FontSize(12d).Alignment = Alignment.center;
            table.Rows[0].Cells[1].Paragraphs[0].AppendLine().AppendLine().Font(new Font("Times New Roman")).Append(n279).Font(new Font("Times New Roman")).FontSize(12d).Alignment = Alignment.center;
            table.Rows[1].Cells[0].SetBorder(TableCellBorderType.Bottom, new Border(BorderStyle.Tcbs_single, BorderSize.two, 1, Color.Black));
            table.Rows[1].Cells[1].Paragraphs[0].Append("№").Font(new Font("Times New Roman")).FontSize(12d).Alignment = Alignment.center;
            table.Rows[1].Cells[2].SetBorder(TableCellBorderType.Bottom, new Border(BorderStyle.Tcbs_single, BorderSize.two, 1, Color.Black));
            table.Rows[0].Cells[1].MarginLeft = 60;
            table.Rows[1].Cells[0].MarginLeft = 30;
            table.Rows[1].Cells[2].MarginLeft = 100;
            table.Rows[3].Cells[0].Paragraphs[0].Append("На №").Font(new Font("Times New Roman")).FontSize(12d).Alignment = Alignment.both;
            table.Rows[3].Cells[1].SetBorder(TableCellBorderType.Bottom, new Border(BorderStyle.Tcbs_single, BorderSize.two, 1, Color.Black));
            doc.InsertTable(table);
            return doc;
        }

    }
}
