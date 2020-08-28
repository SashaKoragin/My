using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabaseAutomation.Automation.BaseLogica.SqlSelect.XsdDTOSheme;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;

namespace LibaryDocumentGenerator.ProgrammView.FullDocument
{
   public class GenerateStatementPreCheck
    {
        /// <summary>
        /// Генерация докладной записки для пред проверочного анализа
        /// </summary>
        /// <param name="statements">Карточка плательщика</param>
        /// <returns></returns>
        public Body GenerateStatementNote(Statement statements)
        {
            Body body = new Body();
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            Table table = new Table();
            var paragraphGenerate = new RunGenerate();
            foreach (var statementHead in statements.HeadingStatement)
            {
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(statementHead.NameIndex, "20", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(18.4), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 3, 0, "FFFFCC"));
                table.Append(rows.GenerateRow(ref cellCollection));
                foreach (var statementFull in statementHead.StatementFull)
                {
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(statementFull.VarIndex.ToString()),
                        "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(statementFull.NameParametr),
                        "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(statementFull.ValuesStatement),
                        "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    table.Append(rows.GenerateRow(ref cellCollection));
                }
            }
            body.Append(table);
            return body;
        }
    }
}
