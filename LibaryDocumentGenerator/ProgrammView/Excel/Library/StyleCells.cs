using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace LibaryDocumentGenerator.ProgrammView.Excel.Library
{
    public class StyleCellsAndGenerateText
    {
        public SpreadsheetDocument Document { get; set; }

        public WorkbookPart WorkBookPart { get; set; }

        public WorkbookStylesPart WorkbookStylesPart { get; set; }
        public StyleCellsAndGenerateText(SpreadsheetDocument document, WorkbookPart workBookPart)
        {
            Document = document;
            WorkBookPart = workBookPart;
            WorkbookStylesPart = document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            Stylesheet stylesheet = new Stylesheet();
            CellFormats cellFormats = new CellFormats() { Count = 0U };
            Fonts fonts = new Fonts() { Count = 0U, KnownFonts = true };
            Borders borders = new Borders() { Count = 0U };
            Fills fills = new Fills() { Count = 0U };
            stylesheet.Append(fonts);
            stylesheet.Append(fills);
            stylesheet.Append(borders);
            stylesheet.Append(cellFormats);
            WorkbookStylesPart.Stylesheet = stylesheet;
            WorkbookStylesPart.Stylesheet.Save();
            GenerateDefaultStyleIndexZeroCellFormat();
        }
        /// <summary>
        /// Генерация текста для Shared
        /// </summary>
        /// <param name="textGeneratedArray">Массив текста для SharedString и Value = "0,1, и т д" </param>
        public void GenerateSharedStringTablePartContent(string[] textGeneratedArray)
        {
            SharedStringTablePart sharedStringTablePart = WorkBookPart.AddNewPart<SharedStringTablePart>();
            SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = Convert.ToUInt32(textGeneratedArray.Length), UniqueCount = Convert.ToUInt32(textGeneratedArray.Length) };
            foreach (var textGenerated in textGeneratedArray)
            {
                SharedStringItem sharedStringItem1 = new SharedStringItem();
                Text text1 = new Text();
                text1.Text = textGenerated;

                sharedStringItem1.Append(text1);

                sharedStringTable1.Append(sharedStringItem1);
            }
            sharedStringTablePart.SharedStringTable = sharedStringTable1;
        }

        /// <summary>
        /// Привязка к шрифту и указывать полная ли  граница или нет! 
        /// </summary>
        /// <param name="valueAlignment">Выравнивание по Горизонтали</param>
        /// <param name="valueVerticalAlignment">Выравнивание по Вертикале</param>
        /// <param name="isFill">Наполнение Fill</param>
        /// <param name="isBorderFull">Проставлять ли границу</param>
        /// <returns></returns>
        public uint StyleTimesNewRoman(HorizontalAlignmentValues valueAlignment = HorizontalAlignmentValues.Left, VerticalAlignmentValues valueVerticalAlignment = VerticalAlignmentValues.Bottom, bool isFill = false, bool isBorderFull = false)
        {
            uint idBorder = 0;
            uint idFill = 0;
            uint idFont = 0;
            idFont = InsertFont(GenerateFont());
            if (isFill)
            {
                idFill = InsertFill(GenerateFill());
            }
            if (isBorderFull)
            {
                idBorder = InsertBorder(GenerateBorder());
            }
            return InsertCellFormat(GenerateCellFormat(valueAlignment, valueVerticalAlignment, idFont, idBorder, idFill));
        }

        /// <summary>
        /// Добавление формата ячейки
        /// </summary>
        /// <param name="valueHorizontalAlignment">Выравнивание по горизонтали</param>
        /// <param name="valueVerticalAlignment">Выравнивание по горизонтали</param>
        /// <param name="fontId">Ссылка на шрифт</param>
        /// <param name="borderId">Ссылка на границу</param>
        /// <param name="fillId">Ссылка на fill</param>
        /// <returns></returns>
        public CellFormat GenerateCellFormat(HorizontalAlignmentValues valueHorizontalAlignment = HorizontalAlignmentValues.Left, VerticalAlignmentValues valueVerticalAlignment = VerticalAlignmentValues.Bottom, uint fontId = 0, uint borderId = 0, uint fillId = 0)
        {
            CellFormat cellFormat = new CellFormat(new Alignment() { WrapText = true, Horizontal = valueHorizontalAlignment, Vertical = valueVerticalAlignment }) 
            {
                ApplyAlignment = true,
            };
            if (fontId != 0)
            {
                
                cellFormat.FontId = fontId;
                cellFormat.ApplyFont = true;
            }
            if (borderId != 0)
            {
                cellFormat.BorderId = borderId;
                cellFormat.ApplyBorder = true;
            }
            if (fillId != 0)
            {
                cellFormat.FillId = fillId;
                cellFormat.ApplyFill = true;
            }
            return cellFormat;
        }


        /// <summary>
        /// Генерировать наполнение
        /// </summary>
        /// <returns></returns>
        public Fill GenerateFill()
        {
            Fill fill = new Fill();
            PatternFill patternFill = new PatternFill()
            {
                PatternType = PatternValues.None
            };
            fill.Append(patternFill);
            return fill;
        }
        /// <summary>
        /// Полная граница одинарная линия цвет черный
        /// </summary>
        /// <returns></returns>
        public Border GenerateBorder()
        {
            Border border = new Border();
            LeftBorder leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color = new Color() { Indexed = (UInt32Value)64U };

            leftBorder.Append(color);

            RightBorder rightBorder = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color1 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder.Append(color1);

            TopBorder topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color2 = new Color() { Indexed = (UInt32Value)64U };

            topBorder.Append(color2);

            BottomBorder bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder.Append(color3);
            DiagonalBorder diagonalBorder = new DiagonalBorder();

            border.Append(leftBorder);
            border.Append(rightBorder);
            border.Append(topBorder);
            border.Append(bottomBorder);
            border.Append(diagonalBorder);
            return border;
        }
        /// <summary>
        /// Добавление шрифта Times New Roman
        /// </summary>
        /// <returns></returns>
        public Font GenerateFont()
        {
            Font font = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color colorfont = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Times New Roman" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 1 };
            FontCharSet fontCharSet1 = new FontCharSet() { Val = 204 };
            font.Append(fontSize1);
            font.Append(colorfont);
            font.Append(fontName1);
            font.Append(fontFamilyNumbering1);
            font.Append(fontCharSet1);
            return font;
        }

        /// <summary>
        /// Формат ячейки
        /// </summary>
        /// <param name="cellFormat">Добавление формат ячейки</param>
        /// <returns></returns>
        public uint InsertCellFormat(CellFormat cellFormat)
        {
            var cells = WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First();
            cells.Append(cellFormat);
            return (uint)cells.Count++;
        }

        /// <summary>
        /// Формат ячейки
        /// </summary>
        /// <param name="font">Добавление Font</param>
        /// <returns></returns>
        public uint InsertFont(Font font)
        {
            var fonts = WorkbookStylesPart.Stylesheet.Elements<Fonts>().First();
            fonts.Append(font);
            return (uint)fonts.Count++;
        }

        /// <summary>
        /// Формат ячейки
        /// </summary>
        /// <param name="fill">Добавление Fill</param>
        /// <returns></returns>
        public uint InsertFill(Fill fill)
        {
            var fills = WorkbookStylesPart.Stylesheet.Elements<Fills>().First();
            fills.Append(fill);
            return (uint)fills.Count++;
        }
        /// <summary>
        /// Добавление границ
        /// </summary>
        /// <param name="border">Добавление Border</param>
        /// <returns></returns>
        public uint InsertBorder(Border border)
        {
            var borders = WorkbookStylesPart.Stylesheet.Elements<Borders>().First();
            borders.Append(border);
            return (uint)borders.Count++;
        }

        /// <summary>
        /// Генерация default CellFormat
        /// </summary>
        /// <returns></returns>
        private void GenerateDefaultStyleIndexZeroCellFormat()
        {
            InsertCellFormat(new CellFormat());
            InsertFont(new Font());
            InsertFill(new Fill());
            InsertBorder(new Border());
        }

    }
}
