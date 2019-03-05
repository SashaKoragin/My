using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;

namespace LibaryDocumentGenerator.ProgrammView.Word.Template.DocMatCen
{
   public class DocMatCen
    {

        public Body GenerateDocMatCen()
        {
            Body body = new Body();
            Table table = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            var rows = new RowGenerate();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            var paragraphGenerate = new RunGenerate();
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Карточка учета материальных ценностей.","30", JustificationValues.Center));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            var ucerej = paragraphGenerate.RunParagraphGeneratorStandart("Учереждение: ", "22");
            ucerej.Append(paragraphGenerate.RunText("Название ", "22", 2));
            body.Append(ucerej);
            var otdel = paragraphGenerate.RunParagraphGeneratorStandart("Структурное подразделение: ", "22");
            otdel.Append(paragraphGenerate.RunText("Отдел", "22", 2));
            body.Append(otdel);
            var matface = paragraphGenerate.RunParagraphGeneratorStandart("Материально ответственнное лицо: ", "22");
            matface.Append(paragraphGenerate.RunText("Кто", "22", 2));
            body.Append(matface);
            var numkab = paragraphGenerate.RunParagraphGeneratorStandart("Номер кабинета: ", "22");
            numkab.Append(paragraphGenerate.RunText("Кабинет", "22", 2));
            body.Append(numkab);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Стелаж", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ячейка", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Единица измерения", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(),2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Цена", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Марка", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Сорт", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Профиль", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Размер", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Норма запаса", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "10", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            table.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("наименование", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("код", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(), 0, 2));
            table.Append(rows.GenerateRow(ref cellcCollection));
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                }
                table.Append(rows.GenerateRow(ref cellcCollection));
            }
            body.Append(table);
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Порядковый номер", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата записи", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Документ", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(),2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Номенклатура", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Приход", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Расход", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Остаток", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Контроль(подпись и дата)", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 1));
            table2.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("дата", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("номенклатура", "20", JustificationValues.Center), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(), 0, 2));
            table2.Append(rows.GenerateRow(ref cellcCollection));
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "1000", TableWidthUnitValues.Dxa, "10", "10", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                }
                table2.Append(rows.GenerateRow(ref cellcCollection));
            }
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
          var klasic =  paragraphGenerate.RunParagraphGeneratorStandart("Класификация: ","22");
            klasic.Append(paragraphGenerate.RunText("Перебор класификаций","22",2));
            body.Append(klasic);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table2);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Должность cczxcxcx dsddsd sdsdsdsds", "22", JustificationValues.Center), "4000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "500", TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "2000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "500", TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Иванов А.И.", "22", JustificationValues.Center), "3000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            table3.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(должность)", "16", JustificationValues.Center), "2000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "2000", TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(подпись)", "16", JustificationValues.Center), "2000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "2000", TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(расшифровка подписи)", "16", JustificationValues.Center), "2000", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            table3.Append(rows.GenerateRow(ref cellcCollection));
            body.Append(table3);
            return body;
        }
    }
}
