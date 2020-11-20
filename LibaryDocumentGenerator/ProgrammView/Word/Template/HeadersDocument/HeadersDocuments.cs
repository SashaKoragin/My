using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.Drawing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;


namespace LibaryDocumentGenerator.ProgrammView.Word.Template.HeadersDocument
{
   public class HeadersDocuments
    {
        /// <summary>
        /// Создание Шапки документа ИФНС 51 Шаблон №1 без герба
        /// </summary>
        /// <param name="tamplate">Шаблон шапки</param>
        /// <param name="n279">Номер инспекции</param>
        /// <param name="n280">Наименование плательщика</param>
        /// <param name="senderOtd">В какой отдел направляется</param>
        /// <returns></returns>
        public Body DocumentsHeaders(LibaryXMLAutoReports.FullTemplateSheme.Document tamplate, string n279 = null, string n280 = null,string senderOtd = null)
        {
            Body body = new Body();
            Table table = new Table();
            var rows = new RowGenerate();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            ObservableCollection<Paragraph> paragraphcCollection = new ObservableCollection<Paragraph>();
            var paragraphGenerate = new RunGenerate();
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade1, "20", JustificationValues.Center, 1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade2, "20", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade3, "20", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade4, "20", JustificationValues.Center, 1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade5, "16", JustificationValues.Center, 1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade6, "16", JustificationValues.Center, 1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade7, "16", JustificationValues.Center, 0, "0", false, false, false));
            if (tamplate.Templates.Headers.TextHeade8.Length > 40)
            {
                var t1 = tamplate.Templates.Headers.TextHeade8.Substring(0, 40);
                var t2 = tamplate.Templates.Headers.TextHeade8.Substring(40);
                paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(t1, "16",
                    JustificationValues.Center));
                paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(t2, "16",
                    JustificationValues.Center));
            }
            else
            {
                paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade8, "16", JustificationValues.Center));
            }
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade9, "16", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Headers.TextHeade10, "16", JustificationValues.Center));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, "100", TableWidthUnitValues.Auto, "0", "200", TableVerticalAlignmentValues.Top, null, 4));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart());
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(n280, "26", JustificationValues.Center,1,"0",false,false,false));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart($"({n279})", "26", JustificationValues.Center, 0, "0", false, false, false));
            if (senderOtd != null)
            {
                paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(senderOtd, "26", JustificationValues.Center));
            }
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, "5500", TableWidthUnitValues.Dxa, "1500", "0", TableVerticalAlignmentValues.Center));
            table.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(1), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(1), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№", "24", JustificationValues.Center), "0", TableWidthUnitValues.Nil));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Nil, "0", "1500", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorder()));
            table.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("На №", "24"), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(),3));
 
            table.Append(rows.GenerateRow(ref cellcCollection));
            body.Append(table);
            body.Append(paragraphGenerate.LineBreker(3));
            return body;
        }
        /// <summary>
        /// Шапка инспекции документа
        /// </summary>
        /// <returns></returns>
        /// <param name="template">Шаблон шапки</param>
        /// <param name="n279">Номер инспекции</param>
        /// <param name="n280">Наименование плательщика</param>
        /// <param name="senderOtd">В какой отдел направляется</param>
        public Body HeaderDocumentIfns(LibaryXMLAutoReports.FullTemplateSheme.Document template, MainDocumentPart mainDocumentPart, string n279 = null, string n280 = null, string senderOtd = null)
        {
            Body body = new Body();
            AddDriwing driving = new AddDriwing();
            Table table = new Table();
            var rows = new RowGenerate();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            ObservableCollection<Paragraph> paragraphCollection = new ObservableCollection<Paragraph>();
            var paragraphGenerate = new RunGenerate();

            ImagePart image = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (Stream file = new MemoryStream(template.Templates.Headers.ImageForm))
            {
                image.FeedData(file);
            }
            var emblem = paragraphGenerate.RunParagraphGeneratorStandart("","20",JustificationValues.Center);
            emblem.Append(driving.AddImageToParagraph(mainDocumentPart.GetIdOfPart(image), 650000L, 650000L));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade1, "20", JustificationValues.Center));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade2, "20", JustificationValues.Center));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade3, "16", JustificationValues.Center));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade4, "16", JustificationValues.Center, 1));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade5, "16", JustificationValues.Center, 1));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade6, "16", JustificationValues.Center, 1));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade7, "16", JustificationValues.Center, 0, "0", false, false, false));
            if (template.Templates.Headers.TextHeade9.Length > 40)
            {
                var t1 = template.Templates.Headers.TextHeade9.Substring(0, 40);
                var t2 = template.Templates.Headers.TextHeade9.Substring(40);
                paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(t1, "16",
                    JustificationValues.Center));
                paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(t2, "16",
                    JustificationValues.Center));
            }
            else
            {
                paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade9, "16", JustificationValues.Center));
            }
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade10, "16", JustificationValues.Center));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(template.Templates.Headers.TextHeade11, "16", JustificationValues.Center));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(1.5), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.9), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4.2), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(1.9), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null, 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(8.3), TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.002)));
            cellcCollection.Add(CellGenerate.GenerateCell(emblem, "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, null, 4));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, null, 0,1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, null));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(1.8)));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphCollection, "0", TableWidthUnitValues.Auto,"0","0",TableVerticalAlignmentValues.Bottom,null,4));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));

            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart());
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(n280, "26", JustificationValues.Center, 1, "0", false, false, false));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart($"({n279})", "26", JustificationValues.Center, 0, "0", false, false, false));
            if (senderOtd != null)
            {
                var splitDepartment = senderOtd.Split('/');
                foreach (var department in splitDepartment)
                {
                    paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(department, "22", JustificationValues.Center));
                }
            }
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphCollection, "0", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, null, 0, 1,1));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(4.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(), 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№", "24",JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2)); 
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.7)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.4)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("На №","24",JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(), 3));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.7)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 3));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.4)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 4));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 0, 2));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(1.8)));
            body.Append(table);
            return body;
        }
    }
}
