using System;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Paragraphs
{
   public class GenerateRun
    {
        /// <summary>
        /// По умолчанию создает пустой параграф 
        /// </summary>
        /// <param name="textparagraph">Текст по умолчанию null вернет пустой параграв</param>
        /// <param name="fontsize">Размер шрифта по умолчанию 10 20 делить на 2</param>
        /// <param name="justifications">Выравнивание по умолчанию Слева</param>
        /// <param name="style">Стиль текста 0 обычный по умолчанию: 1 Жирный:</param>
        /// <param name="leftident">Отступ слева</param>
        /// <param name="breake">Кнопка Enter после вставки</param>
        /// <returns>Возвращаем созданный параграф либо пустой либо заполненный</returns>
        public Paragraph RunParagraphGeneratorStandart(string textparagraph = null, string fontsize="10", JustificationValues justifications = JustificationValues.Left,int style=0, string leftident = "0", bool breake = false,bool isfoters= false)
        {
            Paragraph paragraph = new Paragraph();
            ParagraphProperties paragraphProperties = new ParagraphProperties();
            if (textparagraph == null)
            {
                paragraph.Append(paragraphProperties);
                return paragraph;
            }
            if (isfoters)
            {
                ParagraphStyleId paragraphStyleId = new ParagraphStyleId() { Val = "Footer" };
                paragraphProperties.Append(paragraphStyleId);
            }
            Indentation indental = new Indentation() { FirstLine = leftident};
            Justification justification = new Justification() { Val = justifications };
            ContextualSpacing spasing = new ContextualSpacing() { Val = true };
            paragraphProperties.Append(spasing);
            paragraphProperties.Append(indental);
            paragraphProperties.Append(justification);
            paragraph.Append(paragraphProperties);

            Run run = new Run();
                RunProperties runProperties = new RunProperties();
                RunFonts runFonts = new RunFonts
                {
                    Ascii = "Times New Roman",
                    HighAnsi = "Times New Roman",
                    EastAsia = "Times New Roman",
                    ComplexScript = "Times New Roman"
                };
                FontSize fontSize = new FontSize {Val = fontsize};
                FontSizeComplexScript fontSizeComplexScript = new FontSizeComplexScript {Val = fontsize};
                
                if (style != 0)
                {
                    AddStyleText(ref runProperties, style);
                }
                
                runProperties.Append(runFonts);
                runProperties.Append(fontSize);
                runProperties.Append(fontSizeComplexScript);

                Text text = new Text();
                text.Text = textparagraph;

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
        private void AddStyleText(ref RunProperties property,int i)
        {
            switch (i)
            {
                case 1:
                    Bold bold = new Bold();
                    property.Append(bold);
                    break;
            }
        }

        /// <summary>
        /// Пропуск строк 
        /// </summary>
        /// <param name="i">Количество прпускаемых строк</param>
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
    }
}
