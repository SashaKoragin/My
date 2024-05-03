using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator
{
   public class RunGenerate
    {
        /// <summary>
        /// По умолчанию создает пустой параграф 
        /// </summary>
        /// <param name="textparagraph">Текст по умолчанию null вернет пустой параграв</param>
        /// <param name="fontsize">Размер шрифта по умолчанию 10 20 делить на 2</param>
        /// <param name="justifications">Выравнивание по умолчанию Слева</param>
        /// <param name="style">Стиль текста 0 обычный по умолчанию: 1 Жирный:, 2 Курсив подчеркнутый, 3 Курсив</param>
        /// <param name="leftident">Отступ слева</param>
        /// <param name="breake">Кнопка Enter после вставки</param>
        /// <param name="isfoters">Вставка в Foters</param>
        /// <param name="isContextualSpacing">Не добавлять пробел между обзацами одного и того-ж стиля</param>
        /// <param name="fontClass">Стиль шрифта</param>
        /// <returns>Возвращаем созданный параграф либо пустой либо заполненный</returns>
        public Paragraph RunParagraphGeneratorStandart(string textparagraph = " ", string fontsize = "20", JustificationValues justifications = JustificationValues.Left, int style = 0, string leftident = "0", bool breake = false, bool isfoters = false,  bool isContextualSpacing = true, string fontClass = "Times New Roman")
        {

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            FontSize fontSize = new FontSize { Val = fontsize };
            Run run = new Run();
            RunProperties runProperties = new RunProperties();
            Paragraph paragraph = new Paragraph();

            Indentation indental = new Indentation() { FirstLine = leftident };
            Justification justification = new Justification() { Val = justifications };

                ContextualSpacing spasing = new ContextualSpacing() { Val = isContextualSpacing };
                SpacingBetweenLines sp = new SpacingBetweenLines();
                sp.After = "100";
                paragraphProperties.Append(sp);
                paragraphProperties.Append(spasing);

            Text text = new Text() { Text = textparagraph , Space = SpaceProcessingModeValues.Preserve };

            if (isfoters)
            {
                ParagraphStyleId paragraphStyleId = new ParagraphStyleId() { Val = "Footer" };
                paragraphProperties.Append(paragraphStyleId);
            }
           
            paragraphProperties.Append(indental);
            paragraphProperties.Append(justification);
            paragraph.Append(paragraphProperties);

            RunFonts runFonts = new RunFonts
            {
                Ascii = fontClass,
                HighAnsi = fontClass,
                EastAsia = fontClass,
                ComplexScript = fontClass
            };
         
            if (style != 0)
            {
                AddStyleText(ref runProperties, style);
            }
            FontSizeComplexScript fontSizeComplexScript = new FontSizeComplexScript { Val = fontsize };
            runProperties.Append(runFonts);
            runProperties.Append(fontSize);
            runProperties.Append(fontSizeComplexScript);



            run.Append(runProperties);
            run.Append(text);

            if (breake)
            {
                Break break1 = new Break();
                run.Append(break1);
            }
            paragraph.Append(run);
            return paragraph;
        }

        /// <summary>
        /// Стиль текста
        /// </summary>
        /// <param name="property">Настройки Run</param>
        /// <param name="i">Параметр настройки</param>
        /// <returns></returns>
        private void AddStyleText(ref RunProperties property, int i)
        {
            switch (i)
            {
                case 1:
                    Bold bold = new Bold();
                    property.Append(bold);
                    break;
                case 2:
                    Italic italic = new Italic();
                    Underline underline = new Underline() {Val = UnderlineValues.Single};
                    property.Append(underline);
                    property.Append(italic);
                    break;
                case 3:
                    Italic italic1 = new Italic();
                    property.Append(italic1);
                    break;
            }
        }

        /// <summary>
        /// Пропуск строк 
        /// </summary>
        /// <param name="i">Количество пропускаемых строк</param>
        /// <returns></returns>
        public Paragraph LineBreker(int i = 1)
        {
            Paragraph paragraph = new Paragraph();
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            paragraph.Append(paragraphProperties);
            Run run = new Run();
            for (int j = 0; j < i; j++)
            {
                Break break1 = new Break();
                run.Append(break1);
            }
            paragraph.Append(run);
            return paragraph;
        }
        /// <summary>
        /// Возвращаем форматированный тектс для вставки в параграф
        /// </summary>
        /// <param name="textparagraph">Текст добавленый в параграф</param>
        /// <param name="fontsize">Размер шрифта</param>
        /// <param name="style">Стиль текста 0 обычный по умолчанию: 1 Жирный:, 2 Подчеркнутый</param>
        /// <param name="breaks">Кнопка Enter после вставки</param>
        /// <returns></returns>
        public Run RunText(string textparagraph = " ", string fontsize = "20", int style = 0, bool breaks = false)
        {
            FontSize fontSize = new FontSize { Val = fontsize };
            Run run = new Run();
            RunProperties runProperties = new RunProperties();

            Text text = new Text() { Text = textparagraph, Space = SpaceProcessingModeValues.Preserve };




            RunFonts runFonts = new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                EastAsia = "Times New Roman",
                ComplexScript = "Times New Roman"
            };

            if (style != 0)
            {
                AddStyleText(ref runProperties, style);
            }
            FontSizeComplexScript fontSizeComplexScript = new FontSizeComplexScript { Val = fontsize };
            runProperties.Append(runFonts);
            runProperties.Append(fontSize);
            runProperties.Append(fontSizeComplexScript);
            run.Append(runProperties);
            run.Append(text);
            if (breaks)
            {
                run.Append(new Break());
            }
            return run;
        }
    }
}
