using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using M = DocumentFormat.OpenXml.Math;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using V = DocumentFormat.OpenXml.Vml;


namespace DBF
{
   public class Doc
    {
        // Creates a WordprocessingDocument.
        public void CreatePackage(string filePath)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                CreateParts(package);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        // Adds child parts and generates content of the specified part.
        private void CreateParts(WordprocessingDocument document)
        {
            MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
            GenerateMainDocumentPart1Content(mainDocumentPart1);

            StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("Re751382eff734755");
            GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

            DocumentSettingsPart documentSettingsPart1 = mainDocumentPart1.AddNewPart<DocumentSettingsPart>("Re734e8513d18441e");
            GenerateDocumentSettingsPart1Content(documentSettingsPart1);

            FooterPart footerPart1 = mainDocumentPart1.AddNewPart<FooterPart>("Rda32440226204264");
            GenerateFooterPart1Content(footerPart1);

            FooterPart footerPart2 = mainDocumentPart1.AddNewPart<FooterPart>("R89395fbdc60f4fec");
            GenerateFooterPart2Content(footerPart2);

            FooterPart footerPart3 = mainDocumentPart1.AddNewPart<FooterPart>("R669df9439214493b");
            GenerateFooterPart3Content(footerPart3);
        }

        // Generates content of mainDocumentPart1.
        private void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1)
        {

            Document document1 = new Document();
            Body body1 = new Body();

            SectionProperties sectionProperties1 = new SectionProperties() { RsidR = "003E25F4", RsidSect = "00FC3028" };
            PageSize pageSize1 = new PageSize() { Width = (UInt32Value)11906U, Height = (UInt32Value)16838U };
            PageMargin pageMargin1 = new PageMargin() { Top = 1400, Right = (UInt32Value)800U, Bottom = -1400, Left = (UInt32Value)1440U, Header = (UInt32Value)710U, Footer = (UInt32Value)700U, Gutter = (UInt32Value)0U };
            Columns columns1 = new Columns() { Space = "710" };
            DocGrid docGrid1 = new DocGrid() { LinePitch = 360 };
            FooterReference footerReference1 = new FooterReference() { Type = HeaderFooterValues.Default, Id = "Rda32440226204264" };
            FooterReference footerReference2 = new FooterReference() { Type = HeaderFooterValues.Even, Id = "R89395fbdc60f4fec" };
            FooterReference footerReference3 = new FooterReference() { Type = HeaderFooterValues.First, Id = "R669df9439214493b" };
            TitlePage titlePage1 = new TitlePage();

            sectionProperties1.Append(pageSize1);
            sectionProperties1.Append(pageMargin1);
            sectionProperties1.Append(columns1);
            sectionProperties1.Append(docGrid1);
            sectionProperties1.Append(footerReference1);
            sectionProperties1.Append(footerReference2);
            sectionProperties1.Append(footerReference3);
            sectionProperties1.Append(titlePage1);


            Table table1 = new Table();

            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle() { Val = "TableNormal" };
            TableWidth tableWidth1 = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Auto };

            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);


            TableRow tableRow1 = new TableRow();

            TableCell tableCell1 = new TableCell();

            TableCellProperties tableCellProperties1 = new TableCellProperties();
            TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "4000", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan1 = new GridSpan() { Val = 3 };

            tableCellProperties1.Append(tableCellWidth1);
            tableCellProperties1.Append(gridSpan1);

            Paragraph paragraph1 = new Paragraph();

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            Justification justification1 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties1.Append(justification1);

            Run run1 = new Run();

            RunProperties runProperties1 = new RunProperties();
            Bold bold1 = new Bold();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize1 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "20" };

            runProperties1.Append(bold1);
            runProperties1.Append(runFonts1);
            runProperties1.Append(fontSize1);
            runProperties1.Append(fontSizeComplexScript1);
            Break break1 = new Break();

            run1.Append(runProperties1);
            run1.Append(break1);

            Run run2 = new Run();

            RunProperties runProperties2 = new RunProperties();
            Bold bold2 = new Bold();
            RunFonts runFonts2 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize2 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "20" };

            runProperties2.Append(bold2);
            runProperties2.Append(runFonts2);
            runProperties2.Append(fontSize2);
            runProperties2.Append(fontSizeComplexScript2);
            Text text1 = new Text();
            text1.Text = "МИНФИН РОССИИ";

            run2.Append(runProperties2);
            run2.Append(text1);

            Run run3 = new Run();

            RunProperties runProperties3 = new RunProperties();
            RunFonts runFonts3 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize3 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript3 = new FontSizeComplexScript() { Val = "20" };

            runProperties3.Append(runFonts3);
            runProperties3.Append(fontSize3);
            runProperties3.Append(fontSizeComplexScript3);
            Break break2 = new Break();

            run3.Append(runProperties3);
            run3.Append(break2);

            Run run4 = new Run();

            RunProperties runProperties4 = new RunProperties();
            RunFonts runFonts4 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize4 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript4 = new FontSizeComplexScript() { Val = "20" };

            runProperties4.Append(runFonts4);
            runProperties4.Append(fontSize4);
            runProperties4.Append(fontSizeComplexScript4);
            Text text2 = new Text();
            text2.Text = "ФЕДЕРАЛЬНАЯ НАЛОГОВАЯ СЛУЖБА";

            run4.Append(runProperties4);
            run4.Append(text2);

            Run run5 = new Run();

            RunProperties runProperties5 = new RunProperties();
            Bold bold3 = new Bold();
            RunFonts runFonts5 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize5 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript5 = new FontSizeComplexScript() { Val = "16" };

            runProperties5.Append(bold3);
            runProperties5.Append(runFonts5);
            runProperties5.Append(fontSize5);
            runProperties5.Append(fontSizeComplexScript5);
            Break break3 = new Break();

            run5.Append(runProperties5);
            run5.Append(break3);

            Run run6 = new Run();

            RunProperties runProperties6 = new RunProperties();
            Bold bold4 = new Bold();
            RunFonts runFonts6 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize6 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript6 = new FontSizeComplexScript() { Val = "16" };

            runProperties6.Append(bold4);
            runProperties6.Append(runFonts6);
            runProperties6.Append(fontSize6);
            runProperties6.Append(fontSizeComplexScript6);
            Text text3 = new Text();
            text3.Text = "УФНС РОССИИ ПО Г. МОСКВЕ";

            run6.Append(runProperties6);
            run6.Append(text3);

            Run run7 = new Run();

            RunProperties runProperties7 = new RunProperties();
            RunFonts runFonts7 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize7 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript7 = new FontSizeComplexScript() { Val = "16" };

            runProperties7.Append(runFonts7);
            runProperties7.Append(fontSize7);
            runProperties7.Append(fontSizeComplexScript7);
            Break break4 = new Break();

            run7.Append(runProperties7);
            run7.Append(break4);

            Run run8 = new Run();

            RunProperties runProperties8 = new RunProperties();
            RunFonts runFonts8 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize8 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript8 = new FontSizeComplexScript() { Val = "16" };

            runProperties8.Append(runFonts8);
            runProperties8.Append(fontSize8);
            runProperties8.Append(fontSizeComplexScript8);
            Text text4 = new Text();
            text4.Text = "МЕЖРАЙОННАЯ ИНСПЕКЦИЯ";

            run8.Append(runProperties8);
            run8.Append(text4);

            Run run9 = new Run();

            RunProperties runProperties9 = new RunProperties();
            RunFonts runFonts9 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize9 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript9 = new FontSizeComplexScript() { Val = "16" };

            runProperties9.Append(runFonts9);
            runProperties9.Append(fontSize9);
            runProperties9.Append(fontSizeComplexScript9);
            Break break5 = new Break();

            run9.Append(runProperties9);
            run9.Append(break5);

            Run run10 = new Run();

            RunProperties runProperties10 = new RunProperties();
            RunFonts runFonts10 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize10 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript10 = new FontSizeComplexScript() { Val = "16" };

            runProperties10.Append(runFonts10);
            runProperties10.Append(fontSize10);
            runProperties10.Append(fontSizeComplexScript10);
            Text text5 = new Text();
            text5.Text = "ФЕДЕРАЛЬНОЙ НАЛОГОВОЙ СЛУЖБЫ №51";

            run10.Append(runProperties10);
            run10.Append(text5);

            Run run11 = new Run();

            RunProperties runProperties11 = new RunProperties();
            RunFonts runFonts11 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize11 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript11 = new FontSizeComplexScript() { Val = "16" };

            runProperties11.Append(runFonts11);
            runProperties11.Append(fontSize11);
            runProperties11.Append(fontSizeComplexScript11);
            Break break6 = new Break();

            run11.Append(runProperties11);
            run11.Append(break6);

            Run run12 = new Run();

            RunProperties runProperties12 = new RunProperties();
            RunFonts runFonts12 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize12 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript12 = new FontSizeComplexScript() { Val = "16" };

            runProperties12.Append(runFonts12);
            runProperties12.Append(fontSize12);
            runProperties12.Append(fontSizeComplexScript12);
            Text text6 = new Text();
            text6.Text = "ПО Г.МОСКВЕ";

            run12.Append(runProperties12);
            run12.Append(text6);

            Run run13 = new Run();

            RunProperties runProperties13 = new RunProperties();
            RunFonts runFonts13 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize13 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript13 = new FontSizeComplexScript() { Val = "16" };

            runProperties13.Append(runFonts13);
            runProperties13.Append(fontSize13);
            runProperties13.Append(fontSizeComplexScript13);
            Break break7 = new Break();

            run13.Append(runProperties13);
            run13.Append(break7);

            Run run14 = new Run();

            RunProperties runProperties14 = new RunProperties();
            RunFonts runFonts14 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize14 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript14 = new FontSizeComplexScript() { Val = "16" };

            runProperties14.Append(runFonts14);
            runProperties14.Append(fontSize14);
            runProperties14.Append(fontSizeComplexScript14);
            Text text7 = new Text();
            text7.Text = "(Межрайонная ИФНС России №51 по г.Москве)";

            run14.Append(runProperties14);
            run14.Append(text7);

            Run run15 = new Run();

            RunProperties runProperties15 = new RunProperties();
            RunFonts runFonts15 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize15 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript15 = new FontSizeComplexScript() { Val = "16" };

            runProperties15.Append(runFonts15);
            runProperties15.Append(fontSize15);
            runProperties15.Append(fontSizeComplexScript15);
            Break break8 = new Break();

            run15.Append(runProperties15);
            run15.Append(break8);

            Run run16 = new Run();

            RunProperties runProperties16 = new RunProperties();
            RunFonts runFonts16 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize16 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript16 = new FontSizeComplexScript() { Val = "16" };

            runProperties16.Append(runFonts16);
            runProperties16.Append(fontSize16);
            runProperties16.Append(fontSizeComplexScript16);
            Text text8 = new Text();
            text8.Text = "ул. 50 лет Октября, д.6, г. Москва, 119618";

            run16.Append(runProperties16);
            run16.Append(text8);

            Run run17 = new Run();

            RunProperties runProperties17 = new RunProperties();
            RunFonts runFonts17 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize17 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript17 = new FontSizeComplexScript() { Val = "16" };

            runProperties17.Append(runFonts17);
            runProperties17.Append(fontSize17);
            runProperties17.Append(fontSizeComplexScript17);
            Break break9 = new Break();

            run17.Append(runProperties17);
            run17.Append(break9);

            Run run18 = new Run();

            RunProperties runProperties18 = new RunProperties();
            RunFonts runFonts18 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize18 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript18 = new FontSizeComplexScript() { Val = "16" };

            runProperties18.Append(runFonts18);
            runProperties18.Append(fontSize18);
            runProperties18.Append(fontSizeComplexScript18);
            Text text9 = new Text();
            text9.Text = "Телефон: (495)400-00-51; Факс: (495) 400-26-35;";

            run18.Append(runProperties18);
            run18.Append(text9);

            Run run19 = new Run();

            RunProperties runProperties19 = new RunProperties();
            RunFonts runFonts19 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize19 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript19 = new FontSizeComplexScript() { Val = "16" };

            runProperties19.Append(runFonts19);
            runProperties19.Append(fontSize19);
            runProperties19.Append(fontSizeComplexScript19);
            Break break10 = new Break();

            run19.Append(runProperties19);
            run19.Append(break10);

            Run run20 = new Run();

            RunProperties runProperties20 = new RunProperties();
            RunFonts runFonts20 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize20 = new FontSize() { Val = "16" };
            FontSizeComplexScript fontSizeComplexScript20 = new FontSizeComplexScript() { Val = "16" };

            runProperties20.Append(runFonts20);
            runProperties20.Append(fontSize20);
            runProperties20.Append(fontSizeComplexScript20);
            Text text10 = new Text();
            text10.Text = "www.nalog.ru";

            run20.Append(runProperties20);
            run20.Append(text10);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);
            paragraph1.Append(run2);
            paragraph1.Append(run3);
            paragraph1.Append(run4);
            paragraph1.Append(run5);
            paragraph1.Append(run6);
            paragraph1.Append(run7);
            paragraph1.Append(run8);
            paragraph1.Append(run9);
            paragraph1.Append(run10);
            paragraph1.Append(run11);
            paragraph1.Append(run12);
            paragraph1.Append(run13);
            paragraph1.Append(run14);
            paragraph1.Append(run15);
            paragraph1.Append(run16);
            paragraph1.Append(run17);
            paragraph1.Append(run18);
            paragraph1.Append(run19);
            paragraph1.Append(run20);

            tableCell1.Append(tableCellProperties1);
            tableCell1.Append(paragraph1);

            TableCell tableCell2 = new TableCell();

            TableCellProperties tableCellProperties2 = new TableCellProperties();
            TableCellWidth tableCellWidth2 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            TableCellMargin tableCellMargin1 = new TableCellMargin();
            LeftMargin leftMargin1 = new LeftMargin() { Width = "1200", Type = TableWidthUnitValues.Dxa };

            tableCellMargin1.Append(leftMargin1);

            tableCellProperties2.Append(tableCellWidth2);
            tableCellProperties2.Append(tableCellMargin1);

            Paragraph paragraph2 = new Paragraph();

            ParagraphProperties paragraphProperties2 = new ParagraphProperties();
            Justification justification2 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties2.Append(justification2);

            Run run21 = new Run();
            Break break11 = new Break();

            run21.Append(break11);

            Run run22 = new Run();

            RunProperties runProperties21 = new RunProperties();
            RunFonts runFonts21 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };

            runProperties21.Append(runFonts21);
            Break break12 = new Break();

            run22.Append(runProperties21);
            run22.Append(break12);

            Run run23 = new Run();

            RunProperties runProperties22 = new RunProperties();
            RunFonts runFonts22 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize21 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript21 = new FontSizeComplexScript() { Val = "24" };

            runProperties22.Append(runFonts22);
            runProperties22.Append(fontSize21);
            runProperties22.Append(fontSizeComplexScript21);
            Text text11 = new Text();
            text11.Text = "ИФНС России по г.Красногорску Московской области";

            run23.Append(runProperties22);
            run23.Append(text11);

            Run run24 = new Run();
            Break break13 = new Break();

            run24.Append(break13);

            Run run25 = new Run();

            RunProperties runProperties23 = new RunProperties();
            RunFonts runFonts23 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };

            runProperties23.Append(runFonts23);
            Break break14 = new Break();

            run25.Append(runProperties23);
            run25.Append(break14);

            Run run26 = new Run();

            RunProperties runProperties24 = new RunProperties();
            RunFonts runFonts24 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize22 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript22 = new FontSizeComplexScript() { Val = "24" };

            runProperties24.Append(runFonts24);
            runProperties24.Append(fontSize22);
            runProperties24.Append(fontSizeComplexScript22);
            Text text12 = new Text();
            text12.Text = "5024";

            run26.Append(runProperties24);
            run26.Append(text12);

            paragraph2.Append(paragraphProperties2);
            paragraph2.Append(run21);
            paragraph2.Append(run22);
            paragraph2.Append(run23);
            paragraph2.Append(run24);
            paragraph2.Append(run25);
            paragraph2.Append(run26);

            tableCell2.Append(tableCellProperties2);
            tableCell2.Append(paragraph2);

            tableRow1.Append(tableCell1);
            tableRow1.Append(tableCell2);

            TableRow tableRow2 = new TableRow();

            TableCell tableCell3 = new TableCell();

            TableCellProperties tableCellProperties3 = new TableCellProperties();
            TableCellWidth tableCellWidth3 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            TableCellBorders tableCellBorders1 = new TableCellBorders();
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)1U };

            tableCellBorders1.Append(bottomBorder1);

            TableCellMargin tableCellMargin2 = new TableCellMargin();
            LeftMargin leftMargin2 = new LeftMargin() { Width = "600", Type = TableWidthUnitValues.Dxa };

            tableCellMargin2.Append(leftMargin2);

            tableCellProperties3.Append(tableCellWidth3);
            tableCellProperties3.Append(tableCellBorders1);
            tableCellProperties3.Append(tableCellMargin2);

            Paragraph paragraph3 = new Paragraph();
            ParagraphProperties paragraphProperties3 = new ParagraphProperties();

            paragraph3.Append(paragraphProperties3);

            tableCell3.Append(tableCellProperties3);
            tableCell3.Append(paragraph3);

            TableCell tableCell4 = new TableCell();

            TableCellProperties tableCellProperties4 = new TableCellProperties();
            TableCellWidth tableCellWidth4 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties4.Append(tableCellWidth4);

            Paragraph paragraph4 = new Paragraph();

            ParagraphProperties paragraphProperties4 = new ParagraphProperties();
            Justification justification3 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties4.Append(justification3);

            Run run27 = new Run();

            RunProperties runProperties25 = new RunProperties();
            RunFonts runFonts25 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize23 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript23 = new FontSizeComplexScript() { Val = "24" };

            runProperties25.Append(runFonts25);
            runProperties25.Append(fontSize23);
            runProperties25.Append(fontSizeComplexScript23);
            Text text13 = new Text();
            text13.Text = "№";

            run27.Append(runProperties25);
            run27.Append(text13);

            paragraph4.Append(paragraphProperties4);
            paragraph4.Append(run27);

            tableCell4.Append(tableCellProperties4);
            tableCell4.Append(paragraph4);

            TableCell tableCell5 = new TableCell();

            TableCellProperties tableCellProperties5 = new TableCellProperties();
            TableCellWidth tableCellWidth5 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            TableCellBorders tableCellBorders2 = new TableCellBorders();
            BottomBorder bottomBorder2 = new BottomBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)1U };

            tableCellBorders2.Append(bottomBorder2);

            TableCellMargin tableCellMargin3 = new TableCellMargin();
            LeftMargin leftMargin3 = new LeftMargin() { Width = "2000", Type = TableWidthUnitValues.Dxa };

            tableCellMargin3.Append(leftMargin3);

            tableCellProperties5.Append(tableCellWidth5);
            tableCellProperties5.Append(tableCellBorders2);
            tableCellProperties5.Append(tableCellMargin3);

            Paragraph paragraph5 = new Paragraph();
            ParagraphProperties paragraphProperties5 = new ParagraphProperties();

            paragraph5.Append(paragraphProperties5);

            tableCell5.Append(tableCellProperties5);
            tableCell5.Append(paragraph5);

            TableCell tableCell6 = new TableCell();

            TableCellProperties tableCellProperties6 = new TableCellProperties();
            TableCellWidth tableCellWidth6 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties6.Append(tableCellWidth6);

            Paragraph paragraph6 = new Paragraph();
            ParagraphProperties paragraphProperties6 = new ParagraphProperties();

            paragraph6.Append(paragraphProperties6);

            tableCell6.Append(tableCellProperties6);
            tableCell6.Append(paragraph6);

            tableRow2.Append(tableCell3);
            tableRow2.Append(tableCell4);
            tableRow2.Append(tableCell5);
            tableRow2.Append(tableCell6);

            TableRow tableRow3 = new TableRow();

            TableCell tableCell7 = new TableCell();

            TableCellProperties tableCellProperties7 = new TableCellProperties();
            TableCellWidth tableCellWidth7 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };
            GridSpan gridSpan2 = new GridSpan() { Val = 3 };

            tableCellProperties7.Append(tableCellWidth7);
            tableCellProperties7.Append(gridSpan2);

            Paragraph paragraph7 = new Paragraph();
            ParagraphProperties paragraphProperties7 = new ParagraphProperties();

            paragraph7.Append(paragraphProperties7);

            tableCell7.Append(tableCellProperties7);
            tableCell7.Append(paragraph7);

            TableCell tableCell8 = new TableCell();

            TableCellProperties tableCellProperties8 = new TableCellProperties();
            TableCellWidth tableCellWidth8 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties8.Append(tableCellWidth8);

            Paragraph paragraph8 = new Paragraph();
            ParagraphProperties paragraphProperties8 = new ParagraphProperties();

            paragraph8.Append(paragraphProperties8);

            tableCell8.Append(tableCellProperties8);
            tableCell8.Append(paragraph8);

            tableRow3.Append(tableCell7);
            tableRow3.Append(tableCell8);

            TableRow tableRow4 = new TableRow();

            TableCell tableCell9 = new TableCell();

            TableCellProperties tableCellProperties9 = new TableCellProperties();
            TableCellWidth tableCellWidth9 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties9.Append(tableCellWidth9);

            Paragraph paragraph9 = new Paragraph();

            ParagraphProperties paragraphProperties9 = new ParagraphProperties();
            Justification justification4 = new Justification() { Val = JustificationValues.Both };

            paragraphProperties9.Append(justification4);

            Run run28 = new Run();

            RunProperties runProperties26 = new RunProperties();
            RunFonts runFonts26 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize24 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript24 = new FontSizeComplexScript() { Val = "24" };

            runProperties26.Append(runFonts26);
            runProperties26.Append(fontSize24);
            runProperties26.Append(fontSizeComplexScript24);
            Text text14 = new Text();
            text14.Text = "На №";

            run28.Append(runProperties26);
            run28.Append(text14);

            paragraph9.Append(paragraphProperties9);
            paragraph9.Append(run28);

            tableCell9.Append(tableCellProperties9);
            tableCell9.Append(paragraph9);

            TableCell tableCell10 = new TableCell();

            TableCellProperties tableCellProperties10 = new TableCellProperties();
            TableCellWidth tableCellWidth10 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };
            GridSpan gridSpan3 = new GridSpan() { Val = 2 };

            TableCellBorders tableCellBorders3 = new TableCellBorders();
            BottomBorder bottomBorder3 = new BottomBorder() { Val = BorderValues.Single, Color = "000000", Size = (UInt32Value)4U, Space = (UInt32Value)1U };

            tableCellBorders3.Append(bottomBorder3);

            tableCellProperties10.Append(tableCellWidth10);
            tableCellProperties10.Append(gridSpan3);
            tableCellProperties10.Append(tableCellBorders3);

            Paragraph paragraph10 = new Paragraph();
            ParagraphProperties paragraphProperties10 = new ParagraphProperties();

            paragraph10.Append(paragraphProperties10);

            tableCell10.Append(tableCellProperties10);
            tableCell10.Append(paragraph10);

            TableCell tableCell11 = new TableCell();

            TableCellProperties tableCellProperties11 = new TableCellProperties();
            TableCellWidth tableCellWidth11 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties11.Append(tableCellWidth11);

            Paragraph paragraph11 = new Paragraph();
            ParagraphProperties paragraphProperties11 = new ParagraphProperties();

            paragraph11.Append(paragraphProperties11);

            tableCell11.Append(tableCellProperties11);
            tableCell11.Append(paragraph11);

            tableRow4.Append(tableCell9);
            tableRow4.Append(tableCell10);
            tableRow4.Append(tableCell11);

            table1.Append(tableProperties1);
            table1.Append(tableRow1);
            table1.Append(tableRow2);
            table1.Append(tableRow3);
            table1.Append(tableRow4);

            Paragraph paragraph12 = new Paragraph();

            ParagraphProperties paragraphProperties12 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { Line = "1200" };

            paragraphProperties12.Append(spacingBetweenLines1);

            paragraph12.Append(paragraphProperties12);

            Paragraph paragraph13 = new Paragraph();

            ParagraphProperties paragraphProperties13 = new ParagraphProperties();
            Justification justification5 = new Justification() { Val = JustificationValues.Both };

            paragraphProperties13.Append(justification5);

            Run run29 = new Run();

            RunProperties runProperties27 = new RunProperties();
            RunFonts runFonts27 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize25 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript25 = new FontSizeComplexScript() { Val = "26" };

            runProperties27.Append(runFonts27);
            runProperties27.Append(fontSize25);
            runProperties27.Append(fontSizeComplexScript25);

            Text text15 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text15.Text = "          Межрайонная ИФНС России №51 по г. Москве просит в СРОЧНОМ порядке направить Информационное сообщение о приеме / отказе БДК:";

            run29.Append(runProperties27);
            run29.Append(text15);

            paragraph13.Append(paragraphProperties13);
            paragraph13.Append(run29);

            Table table2 = new Table();

            TableProperties tableProperties2 = new TableProperties();
            TableStyle tableStyle2 = new TableStyle() { Val = "TableGrid" };
            TableWidth tableWidth2 = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Auto };
            TableLook tableLook2 = new TableLook() { Val = "04A0" };

            tableProperties2.Append(tableStyle2);
            tableProperties2.Append(tableWidth2);
            tableProperties2.Append(tableLook2);

            TableRow tableRow5 = new TableRow();

            TableCell tableCell12 = new TableCell();

            TableCellProperties tableCellProperties12 = new TableCellProperties();
            TableCellWidth tableCellWidth12 = new TableCellWidth() { Width = "800", Type = TableWidthUnitValues.Dxa };

            tableCellProperties12.Append(tableCellWidth12);

            Paragraph paragraph14 = new Paragraph();

            ParagraphProperties paragraphProperties14 = new ParagraphProperties();
            Justification justification6 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties14.Append(justification6);

            Run run30 = new Run();

            RunProperties runProperties28 = new RunProperties();
            Bold bold5 = new Bold();
            RunFonts runFonts28 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize26 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript26 = new FontSizeComplexScript() { Val = "26" };

            runProperties28.Append(bold5);
            runProperties28.Append(runFonts28);
            runProperties28.Append(fontSize26);
            runProperties28.Append(fontSizeComplexScript26);
            Text text16 = new Text();
            text16.Text = "Дата отправки";

            run30.Append(runProperties28);
            run30.Append(text16);

            paragraph14.Append(paragraphProperties14);
            paragraph14.Append(run30);

            tableCell12.Append(tableCellProperties12);
            tableCell12.Append(paragraph14);

            TableCell tableCell13 = new TableCell();

            TableCellProperties tableCellProperties13 = new TableCellProperties();
            TableCellWidth tableCellWidth13 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties13.Append(tableCellWidth13);

            Paragraph paragraph15 = new Paragraph();

            ParagraphProperties paragraphProperties15 = new ParagraphProperties();
            Justification justification7 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties15.Append(justification7);

            Run run31 = new Run();

            RunProperties runProperties29 = new RunProperties();
            Bold bold6 = new Bold();
            RunFonts runFonts29 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize27 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript27 = new FontSizeComplexScript() { Val = "26" };

            runProperties29.Append(bold6);
            runProperties29.Append(runFonts29);
            runProperties29.Append(fontSize27);
            runProperties29.Append(fontSizeComplexScript27);
            Text text17 = new Text();
            text17.Text = "Наименование контейнера";

            run31.Append(runProperties29);
            run31.Append(text17);

            paragraph15.Append(paragraphProperties15);
            paragraph15.Append(run31);

            tableCell13.Append(tableCellProperties13);
            tableCell13.Append(paragraph15);

            TableCell tableCell14 = new TableCell();

            TableCellProperties tableCellProperties14 = new TableCellProperties();
            TableCellWidth tableCellWidth14 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties14.Append(tableCellWidth14);

            Paragraph paragraph16 = new Paragraph();

            ParagraphProperties paragraphProperties16 = new ParagraphProperties();
            Justification justification8 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties16.Append(justification8);

            Run run32 = new Run();

            RunProperties runProperties30 = new RunProperties();
            Bold bold7 = new Bold();
            RunFonts runFonts30 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize28 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript28 = new FontSizeComplexScript() { Val = "26" };

            runProperties30.Append(bold7);
            runProperties30.Append(runFonts30);
            runProperties30.Append(fontSize28);
            runProperties30.Append(fontSizeComplexScript28);
            Text text18 = new Text();
            text18.Text = "GUID";

            run32.Append(runProperties30);
            run32.Append(text18);

            paragraph16.Append(paragraphProperties16);
            paragraph16.Append(run32);

            tableCell14.Append(tableCellProperties14);
            tableCell14.Append(paragraph16);

            tableRow5.Append(tableCell12);
            tableRow5.Append(tableCell13);
            tableRow5.Append(tableCell14);

            TableRow tableRow6 = new TableRow();

            TableCell tableCell15 = new TableCell();

            TableCellProperties tableCellProperties15 = new TableCellProperties();
            TableCellWidth tableCellWidth15 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties15.Append(tableCellWidth15);

            Paragraph paragraph17 = new Paragraph();

            ParagraphProperties paragraphProperties17 = new ParagraphProperties();
            Justification justification9 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties17.Append(justification9);

            Run run33 = new Run();

            RunProperties runProperties31 = new RunProperties();
            RunFonts runFonts31 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize29 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript29 = new FontSizeComplexScript() { Val = "24" };

            runProperties31.Append(runFonts31);
            runProperties31.Append(fontSize29);
            runProperties31.Append(fontSizeComplexScript29);
            Text text19 = new Text();
            text19.Text = "29-03-2018";

            run33.Append(runProperties31);
            run33.Append(text19);

            paragraph17.Append(paragraphProperties17);
            paragraph17.Append(run33);

            tableCell15.Append(tableCellProperties15);
            tableCell15.Append(paragraph17);

            TableCell tableCell16 = new TableCell();

            TableCellProperties tableCellProperties16 = new TableCellProperties();
            TableCellWidth tableCellWidth16 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties16.Append(tableCellWidth16);

            Paragraph paragraph18 = new Paragraph();

            ParagraphProperties paragraphProperties18 = new ParagraphProperties();
            Justification justification10 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties18.Append(justification10);

            Run run34 = new Run();

            RunProperties runProperties32 = new RunProperties();
            RunFonts runFonts32 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize30 = new FontSize() { Val = "24" };
            FontSizeComplexScript fontSizeComplexScript30 = new FontSizeComplexScript() { Val = "24" };

            runProperties32.Append(runFonts32);
            runProperties32.Append(fontSize30);
            runProperties32.Append(fontSizeComplexScript30);
            Text text20 = new Text();
            text20.Text = "BDK77515024000150020000025386";

            run34.Append(runProperties32);
            run34.Append(text20);

            paragraph18.Append(paragraphProperties18);
            paragraph18.Append(run34);

            tableCell16.Append(tableCellProperties16);
            tableCell16.Append(paragraph18);

            TableCell tableCell17 = new TableCell();

            TableCellProperties tableCellProperties17 = new TableCellProperties();
            TableCellWidth tableCellWidth17 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties17.Append(tableCellWidth17);

            Paragraph paragraph19 = new Paragraph();

            ParagraphProperties paragraphProperties19 = new ParagraphProperties();
            Justification justification11 = new Justification() { Val = JustificationValues.Center };

            paragraphProperties19.Append(justification11);

            Run run35 = new Run();

            RunProperties runProperties33 = new RunProperties();
            RunFonts runFonts33 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize31 = new FontSize() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript31 = new FontSizeComplexScript() { Val = "22" };

            runProperties33.Append(runFonts33);
            runProperties33.Append(fontSize31);
            runProperties33.Append(fontSizeComplexScript31);
            Text text21 = new Text();
            text21.Text = "2FC83627-5DE8-4A82-B22F-DB3EE933D79D";

            run35.Append(runProperties33);
            run35.Append(text21);

            paragraph19.Append(paragraphProperties19);
            paragraph19.Append(run35);

            tableCell17.Append(tableCellProperties17);
            tableCell17.Append(paragraph19);

            tableRow6.Append(tableCell15);
            tableRow6.Append(tableCell16);
            tableRow6.Append(tableCell17);

            table2.Append(tableProperties2);
            table2.Append(tableRow5);
            table2.Append(tableRow6);

            Paragraph paragraph20 = new Paragraph();

            ParagraphProperties paragraphProperties20 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines() { Line = "720" };

            paragraphProperties20.Append(spacingBetweenLines2);

            paragraph20.Append(paragraphProperties20);

            Table table3 = new Table();

            TableProperties tableProperties3 = new TableProperties();
            TableStyle tableStyle3 = new TableStyle() { Val = "TableNormal" };
            TableWidth tableWidth3 = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Auto };
            TableLook tableLook3 = new TableLook() { Val = "04A0" };

            tableProperties3.Append(tableStyle3);
            tableProperties3.Append(tableWidth3);
            tableProperties3.Append(tableLook3);

            TableRow tableRow7 = new TableRow();

            TableCell tableCell18 = new TableCell();

            TableCellProperties tableCellProperties18 = new TableCellProperties();
            TableCellWidth tableCellWidth18 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            tableCellProperties18.Append(tableCellWidth18);

            Paragraph paragraph21 = new Paragraph();
            ParagraphProperties paragraphProperties21 = new ParagraphProperties();

            Run run36 = new Run();

            RunProperties runProperties34 = new RunProperties();
            RunFonts runFonts34 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize32 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript32 = new FontSizeComplexScript() { Val = "26" };

            runProperties34.Append(runFonts34);
            runProperties34.Append(fontSize32);
            runProperties34.Append(fontSizeComplexScript32);
            Text text22 = new Text();
            text22.Text = "Заместитель начальника инспекции";

            run36.Append(runProperties34);
            run36.Append(text22);

            Run run37 = new Run();

            RunProperties runProperties35 = new RunProperties();
            RunFonts runFonts35 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize33 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript33 = new FontSizeComplexScript() { Val = "26" };

            runProperties35.Append(runFonts35);
            runProperties35.Append(fontSize33);
            runProperties35.Append(fontSizeComplexScript33);
            Break break15 = new Break();

            run37.Append(runProperties35);
            run37.Append(break15);

            Run run38 = new Run();

            RunProperties runProperties36 = new RunProperties();
            RunFonts runFonts36 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize34 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript34 = new FontSizeComplexScript() { Val = "26" };

            runProperties36.Append(runFonts36);
            runProperties36.Append(fontSize34);
            runProperties36.Append(fontSizeComplexScript34);
            Text text23 = new Text();
            text23.Text = "Советник государственной гражданской службы Российской Федерации 2 класса";

            run38.Append(runProperties36);
            run38.Append(text23);

            paragraph21.Append(paragraphProperties21);
            paragraph21.Append(run36);
            paragraph21.Append(run37);
            paragraph21.Append(run38);

            tableCell18.Append(tableCellProperties18);
            tableCell18.Append(paragraph21);

            TableCell tableCell19 = new TableCell();

            TableCellProperties tableCellProperties19 = new TableCellProperties();
            TableCellWidth tableCellWidth19 = new TableCellWidth() { Width = "2310", Type = TableWidthUnitValues.Auto };

            TableCellMargin tableCellMargin4 = new TableCellMargin();
            LeftMargin leftMargin4 = new LeftMargin() { Width = "2000", Type = TableWidthUnitValues.Dxa };

            tableCellMargin4.Append(leftMargin4);

            tableCellProperties19.Append(tableCellWidth19);
            tableCellProperties19.Append(tableCellMargin4);

            Paragraph paragraph22 = new Paragraph();

            ParagraphProperties paragraphProperties22 = new ParagraphProperties();
            Justification justification12 = new Justification() { Val = JustificationValues.Right };

            paragraphProperties22.Append(justification12);

            Run run39 = new Run();
            Break break16 = new Break();

            run39.Append(break16);

            Run run40 = new Run();
            Break break17 = new Break();

            run40.Append(break17);

            Run run41 = new Run();

            RunProperties runProperties37 = new RunProperties();
            RunFonts runFonts37 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize35 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript35 = new FontSizeComplexScript() { Val = "26" };

            runProperties37.Append(runFonts37);
            runProperties37.Append(fontSize35);
            runProperties37.Append(fontSizeComplexScript35);
            Text text24 = new Text();
            text24.Text = "В.Ф.Авзалова";

            run41.Append(runProperties37);
            run41.Append(text24);

            paragraph22.Append(paragraphProperties22);
            paragraph22.Append(run39);
            paragraph22.Append(run40);
            paragraph22.Append(run41);

            tableCell19.Append(tableCellProperties19);
            tableCell19.Append(paragraph22);

            tableRow7.Append(tableCell18);
            tableRow7.Append(tableCell19);

            table3.Append(tableProperties3);
            table3.Append(tableRow7);
            SectionProperties sectionProperties2 = new SectionProperties();

            body1.Append(sectionProperties1);
            body1.Append(table1);
            body1.Append(paragraph12);
            body1.Append(paragraph13);
            body1.Append(table2);
            body1.Append(paragraph20);
            body1.Append(table3);
            body1.Append(sectionProperties2);

            document1.Append(body1);

            mainDocumentPart1.Document = document1;
        }

        // Generates content of styleDefinitionsPart1.
        private void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
        {
            Styles styles1 = new Styles();
            styles1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            DocDefaults docDefaults1 = new DocDefaults();

            RunPropertiesDefault runPropertiesDefault1 = new RunPropertiesDefault();

            RunPropertiesBaseStyle runPropertiesBaseStyle1 = new RunPropertiesBaseStyle();
            RunFonts runFonts38 = new RunFonts() { AsciiTheme = ThemeFontValues.MinorHighAnsi, HighAnsiTheme = ThemeFontValues.MinorHighAnsi, EastAsiaTheme = ThemeFontValues.MinorHighAnsi, ComplexScriptTheme = ThemeFontValues.MinorBidi };
            FontSize fontSize36 = new FontSize() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript36 = new FontSizeComplexScript() { Val = "22" };
            Languages languages1 = new Languages() { Val = "ru-RU", EastAsia = "en-US", Bidi = "ar-SA" };

            runPropertiesBaseStyle1.Append(runFonts38);
            runPropertiesBaseStyle1.Append(fontSize36);
            runPropertiesBaseStyle1.Append(fontSizeComplexScript36);
            runPropertiesBaseStyle1.Append(languages1);

            runPropertiesDefault1.Append(runPropertiesBaseStyle1);
            ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

            docDefaults1.Append(runPropertiesDefault1);
            docDefaults1.Append(paragraphPropertiesDefault1);

            LatentStyles latentStyles1 = new LatentStyles() { DefaultLockedState = false, DefaultUiPriority = 99, DefaultSemiHidden = true, DefaultUnhideWhenUsed = true, DefaultPrimaryStyle = false, Count = 267 };
            LatentStyleExceptionInfo latentStyleExceptionInfo1 = new LatentStyleExceptionInfo() { Name = "Normal", UiPriority = 0, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo2 = new LatentStyleExceptionInfo() { Name = "heading 1", UiPriority = 9, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo3 = new LatentStyleExceptionInfo() { Name = "heading 2", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo4 = new LatentStyleExceptionInfo() { Name = "heading 3", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo5 = new LatentStyleExceptionInfo() { Name = "heading 4", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo6 = new LatentStyleExceptionInfo() { Name = "heading 5", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo7 = new LatentStyleExceptionInfo() { Name = "heading 6", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo8 = new LatentStyleExceptionInfo() { Name = "heading 7", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo9 = new LatentStyleExceptionInfo() { Name = "heading 8", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo10 = new LatentStyleExceptionInfo() { Name = "heading 9", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo11 = new LatentStyleExceptionInfo() { Name = "toc 1", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo12 = new LatentStyleExceptionInfo() { Name = "toc 2", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo13 = new LatentStyleExceptionInfo() { Name = "toc 3", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo14 = new LatentStyleExceptionInfo() { Name = "toc 4", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo15 = new LatentStyleExceptionInfo() { Name = "toc 5", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo16 = new LatentStyleExceptionInfo() { Name = "toc 6", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo17 = new LatentStyleExceptionInfo() { Name = "toc 7", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo18 = new LatentStyleExceptionInfo() { Name = "toc 8", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo19 = new LatentStyleExceptionInfo() { Name = "toc 9", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo20 = new LatentStyleExceptionInfo() { Name = "caption", UiPriority = 35, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo21 = new LatentStyleExceptionInfo() { Name = "Title", UiPriority = 10, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo22 = new LatentStyleExceptionInfo() { Name = "Default Paragraph Font", UiPriority = 1 };
            LatentStyleExceptionInfo latentStyleExceptionInfo23 = new LatentStyleExceptionInfo() { Name = "Subtitle", UiPriority = 11, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo24 = new LatentStyleExceptionInfo() { Name = "Strong", UiPriority = 22, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo25 = new LatentStyleExceptionInfo() { Name = "Emphasis", UiPriority = 20, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo26 = new LatentStyleExceptionInfo() { Name = "Table Grid", UiPriority = 59, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo27 = new LatentStyleExceptionInfo() { Name = "Placeholder Text", UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo28 = new LatentStyleExceptionInfo() { Name = "No Spacing", UiPriority = 1, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo29 = new LatentStyleExceptionInfo() { Name = "Light Shading", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo30 = new LatentStyleExceptionInfo() { Name = "Light List", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo31 = new LatentStyleExceptionInfo() { Name = "Light Grid", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo32 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo33 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo34 = new LatentStyleExceptionInfo() { Name = "Medium List 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo35 = new LatentStyleExceptionInfo() { Name = "Medium List 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo36 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo37 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo38 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo39 = new LatentStyleExceptionInfo() { Name = "Dark List", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo40 = new LatentStyleExceptionInfo() { Name = "Colorful Shading", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo41 = new LatentStyleExceptionInfo() { Name = "Colorful List", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo42 = new LatentStyleExceptionInfo() { Name = "Colorful Grid", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo43 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 1", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo44 = new LatentStyleExceptionInfo() { Name = "Light List Accent 1", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo45 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 1", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo46 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo47 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 1", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo48 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo49 = new LatentStyleExceptionInfo() { Name = "Revision", UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo50 = new LatentStyleExceptionInfo() { Name = "List Paragraph", UiPriority = 34, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo51 = new LatentStyleExceptionInfo() { Name = "Quote", UiPriority = 29, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo52 = new LatentStyleExceptionInfo() { Name = "Intense Quote", UiPriority = 30, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo53 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 1", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo54 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo55 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 1", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo56 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 1", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo57 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 1", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo58 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 1", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo59 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 1", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo60 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 1", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo61 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 2", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo62 = new LatentStyleExceptionInfo() { Name = "Light List Accent 2", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo63 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 2", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo64 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 2", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo65 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo66 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 2", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo67 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo68 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 2", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo69 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo70 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 2", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo71 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 2", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo72 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 2", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo73 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 2", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo74 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 2", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo75 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 3", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo76 = new LatentStyleExceptionInfo() { Name = "Light List Accent 3", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo77 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 3", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo78 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 3", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo79 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 3", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo80 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 3", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo81 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 3", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo82 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 3", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo83 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 3", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo84 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo85 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 3", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo86 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 3", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo87 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 3", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo88 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 3", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo89 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 4", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo90 = new LatentStyleExceptionInfo() { Name = "Light List Accent 4", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo91 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 4", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo92 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 4", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo93 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 4", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo94 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 4", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo95 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 4", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo96 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 4", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo97 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 4", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo98 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 4", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo99 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 4", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo100 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 4", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo101 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 4", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo102 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 4", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo103 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 5", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo104 = new LatentStyleExceptionInfo() { Name = "Light List Accent 5", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo105 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 5", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo106 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 5", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo107 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 5", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo108 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 5", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo109 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 5", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo110 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 5", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo111 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 5", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo112 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 5", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo113 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 5", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo114 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 5", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo115 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 5", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo116 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 5", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo117 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 6", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo118 = new LatentStyleExceptionInfo() { Name = "Light List Accent 6", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo119 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 6", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo120 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 6", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo121 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 6", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo122 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 6", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo123 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 6", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo124 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 6", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo125 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 6", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo126 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 6", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo127 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 6", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo128 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 6", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo129 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 6", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo130 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 6", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo131 = new LatentStyleExceptionInfo() { Name = "Subtle Emphasis", UiPriority = 19, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo132 = new LatentStyleExceptionInfo() { Name = "Intense Emphasis", UiPriority = 21, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo133 = new LatentStyleExceptionInfo() { Name = "Subtle Reference", UiPriority = 31, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo134 = new LatentStyleExceptionInfo() { Name = "Intense Reference", UiPriority = 32, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo135 = new LatentStyleExceptionInfo() { Name = "Book Title", UiPriority = 33, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo136 = new LatentStyleExceptionInfo() { Name = "Bibliography", UiPriority = 37 };
            LatentStyleExceptionInfo latentStyleExceptionInfo137 = new LatentStyleExceptionInfo() { Name = "TOC Heading", UiPriority = 39, PrimaryStyle = true };

            latentStyles1.Append(latentStyleExceptionInfo1);
            latentStyles1.Append(latentStyleExceptionInfo2);
            latentStyles1.Append(latentStyleExceptionInfo3);
            latentStyles1.Append(latentStyleExceptionInfo4);
            latentStyles1.Append(latentStyleExceptionInfo5);
            latentStyles1.Append(latentStyleExceptionInfo6);
            latentStyles1.Append(latentStyleExceptionInfo7);
            latentStyles1.Append(latentStyleExceptionInfo8);
            latentStyles1.Append(latentStyleExceptionInfo9);
            latentStyles1.Append(latentStyleExceptionInfo10);
            latentStyles1.Append(latentStyleExceptionInfo11);
            latentStyles1.Append(latentStyleExceptionInfo12);
            latentStyles1.Append(latentStyleExceptionInfo13);
            latentStyles1.Append(latentStyleExceptionInfo14);
            latentStyles1.Append(latentStyleExceptionInfo15);
            latentStyles1.Append(latentStyleExceptionInfo16);
            latentStyles1.Append(latentStyleExceptionInfo17);
            latentStyles1.Append(latentStyleExceptionInfo18);
            latentStyles1.Append(latentStyleExceptionInfo19);
            latentStyles1.Append(latentStyleExceptionInfo20);
            latentStyles1.Append(latentStyleExceptionInfo21);
            latentStyles1.Append(latentStyleExceptionInfo22);
            latentStyles1.Append(latentStyleExceptionInfo23);
            latentStyles1.Append(latentStyleExceptionInfo24);
            latentStyles1.Append(latentStyleExceptionInfo25);
            latentStyles1.Append(latentStyleExceptionInfo26);
            latentStyles1.Append(latentStyleExceptionInfo27);
            latentStyles1.Append(latentStyleExceptionInfo28);
            latentStyles1.Append(latentStyleExceptionInfo29);
            latentStyles1.Append(latentStyleExceptionInfo30);
            latentStyles1.Append(latentStyleExceptionInfo31);
            latentStyles1.Append(latentStyleExceptionInfo32);
            latentStyles1.Append(latentStyleExceptionInfo33);
            latentStyles1.Append(latentStyleExceptionInfo34);
            latentStyles1.Append(latentStyleExceptionInfo35);
            latentStyles1.Append(latentStyleExceptionInfo36);
            latentStyles1.Append(latentStyleExceptionInfo37);
            latentStyles1.Append(latentStyleExceptionInfo38);
            latentStyles1.Append(latentStyleExceptionInfo39);
            latentStyles1.Append(latentStyleExceptionInfo40);
            latentStyles1.Append(latentStyleExceptionInfo41);
            latentStyles1.Append(latentStyleExceptionInfo42);
            latentStyles1.Append(latentStyleExceptionInfo43);
            latentStyles1.Append(latentStyleExceptionInfo44);
            latentStyles1.Append(latentStyleExceptionInfo45);
            latentStyles1.Append(latentStyleExceptionInfo46);
            latentStyles1.Append(latentStyleExceptionInfo47);
            latentStyles1.Append(latentStyleExceptionInfo48);
            latentStyles1.Append(latentStyleExceptionInfo49);
            latentStyles1.Append(latentStyleExceptionInfo50);
            latentStyles1.Append(latentStyleExceptionInfo51);
            latentStyles1.Append(latentStyleExceptionInfo52);
            latentStyles1.Append(latentStyleExceptionInfo53);
            latentStyles1.Append(latentStyleExceptionInfo54);
            latentStyles1.Append(latentStyleExceptionInfo55);
            latentStyles1.Append(latentStyleExceptionInfo56);
            latentStyles1.Append(latentStyleExceptionInfo57);
            latentStyles1.Append(latentStyleExceptionInfo58);
            latentStyles1.Append(latentStyleExceptionInfo59);
            latentStyles1.Append(latentStyleExceptionInfo60);
            latentStyles1.Append(latentStyleExceptionInfo61);
            latentStyles1.Append(latentStyleExceptionInfo62);
            latentStyles1.Append(latentStyleExceptionInfo63);
            latentStyles1.Append(latentStyleExceptionInfo64);
            latentStyles1.Append(latentStyleExceptionInfo65);
            latentStyles1.Append(latentStyleExceptionInfo66);
            latentStyles1.Append(latentStyleExceptionInfo67);
            latentStyles1.Append(latentStyleExceptionInfo68);
            latentStyles1.Append(latentStyleExceptionInfo69);
            latentStyles1.Append(latentStyleExceptionInfo70);
            latentStyles1.Append(latentStyleExceptionInfo71);
            latentStyles1.Append(latentStyleExceptionInfo72);
            latentStyles1.Append(latentStyleExceptionInfo73);
            latentStyles1.Append(latentStyleExceptionInfo74);
            latentStyles1.Append(latentStyleExceptionInfo75);
            latentStyles1.Append(latentStyleExceptionInfo76);
            latentStyles1.Append(latentStyleExceptionInfo77);
            latentStyles1.Append(latentStyleExceptionInfo78);
            latentStyles1.Append(latentStyleExceptionInfo79);
            latentStyles1.Append(latentStyleExceptionInfo80);
            latentStyles1.Append(latentStyleExceptionInfo81);
            latentStyles1.Append(latentStyleExceptionInfo82);
            latentStyles1.Append(latentStyleExceptionInfo83);
            latentStyles1.Append(latentStyleExceptionInfo84);
            latentStyles1.Append(latentStyleExceptionInfo85);
            latentStyles1.Append(latentStyleExceptionInfo86);
            latentStyles1.Append(latentStyleExceptionInfo87);
            latentStyles1.Append(latentStyleExceptionInfo88);
            latentStyles1.Append(latentStyleExceptionInfo89);
            latentStyles1.Append(latentStyleExceptionInfo90);
            latentStyles1.Append(latentStyleExceptionInfo91);
            latentStyles1.Append(latentStyleExceptionInfo92);
            latentStyles1.Append(latentStyleExceptionInfo93);
            latentStyles1.Append(latentStyleExceptionInfo94);
            latentStyles1.Append(latentStyleExceptionInfo95);
            latentStyles1.Append(latentStyleExceptionInfo96);
            latentStyles1.Append(latentStyleExceptionInfo97);
            latentStyles1.Append(latentStyleExceptionInfo98);
            latentStyles1.Append(latentStyleExceptionInfo99);
            latentStyles1.Append(latentStyleExceptionInfo100);
            latentStyles1.Append(latentStyleExceptionInfo101);
            latentStyles1.Append(latentStyleExceptionInfo102);
            latentStyles1.Append(latentStyleExceptionInfo103);
            latentStyles1.Append(latentStyleExceptionInfo104);
            latentStyles1.Append(latentStyleExceptionInfo105);
            latentStyles1.Append(latentStyleExceptionInfo106);
            latentStyles1.Append(latentStyleExceptionInfo107);
            latentStyles1.Append(latentStyleExceptionInfo108);
            latentStyles1.Append(latentStyleExceptionInfo109);
            latentStyles1.Append(latentStyleExceptionInfo110);
            latentStyles1.Append(latentStyleExceptionInfo111);
            latentStyles1.Append(latentStyleExceptionInfo112);
            latentStyles1.Append(latentStyleExceptionInfo113);
            latentStyles1.Append(latentStyleExceptionInfo114);
            latentStyles1.Append(latentStyleExceptionInfo115);
            latentStyles1.Append(latentStyleExceptionInfo116);
            latentStyles1.Append(latentStyleExceptionInfo117);
            latentStyles1.Append(latentStyleExceptionInfo118);
            latentStyles1.Append(latentStyleExceptionInfo119);
            latentStyles1.Append(latentStyleExceptionInfo120);
            latentStyles1.Append(latentStyleExceptionInfo121);
            latentStyles1.Append(latentStyleExceptionInfo122);
            latentStyles1.Append(latentStyleExceptionInfo123);
            latentStyles1.Append(latentStyleExceptionInfo124);
            latentStyles1.Append(latentStyleExceptionInfo125);
            latentStyles1.Append(latentStyleExceptionInfo126);
            latentStyles1.Append(latentStyleExceptionInfo127);
            latentStyles1.Append(latentStyleExceptionInfo128);
            latentStyles1.Append(latentStyleExceptionInfo129);
            latentStyles1.Append(latentStyleExceptionInfo130);
            latentStyles1.Append(latentStyleExceptionInfo131);
            latentStyles1.Append(latentStyleExceptionInfo132);
            latentStyles1.Append(latentStyleExceptionInfo133);
            latentStyles1.Append(latentStyleExceptionInfo134);
            latentStyles1.Append(latentStyleExceptionInfo135);
            latentStyles1.Append(latentStyleExceptionInfo136);
            latentStyles1.Append(latentStyleExceptionInfo137);

            Style style1 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading1" };
            StyleName styleName1 = new StyleName() { Val = "heading 1" };
            BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "Heading1Char" };
            UIPriority uIPriority1 = new UIPriority() { Val = 9 };
            PrimaryStyle primaryStyle1 = new PrimaryStyle();
            Rsid rsid1 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
            KeepNext keepNext1 = new KeepNext();
            KeepLines keepLines1 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines3 = new SpacingBetweenLines() { Before = "480", After = "0" };
            OutlineLevel outlineLevel1 = new OutlineLevel() { Val = 0 };

            styleParagraphProperties1.Append(keepNext1);
            styleParagraphProperties1.Append(keepLines1);
            styleParagraphProperties1.Append(spacingBetweenLines3);
            styleParagraphProperties1.Append(outlineLevel1);

            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            RunFonts runFonts39 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold8 = new Bold();
            BoldComplexScript boldComplexScript1 = new BoldComplexScript();
            Color color1 = new Color() { Val = "365F91", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "BF" };
            FontSize fontSize37 = new FontSize() { Val = "28" };
            FontSizeComplexScript fontSizeComplexScript37 = new FontSizeComplexScript() { Val = "28" };

            styleRunProperties1.Append(runFonts39);
            styleRunProperties1.Append(bold8);
            styleRunProperties1.Append(boldComplexScript1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(fontSize37);
            styleRunProperties1.Append(fontSizeComplexScript37);

            style1.Append(styleName1);
            style1.Append(basedOn1);
            style1.Append(nextParagraphStyle1);
            style1.Append(linkedStyle1);
            style1.Append(uIPriority1);
            style1.Append(primaryStyle1);
            style1.Append(rsid1);
            style1.Append(styleParagraphProperties1);
            style1.Append(styleRunProperties1);

            Style style2 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading2" };
            StyleName styleName2 = new StyleName() { Val = "heading 2" };
            BasedOn basedOn2 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle2 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle2 = new LinkedStyle() { Val = "Heading2Char" };
            UIPriority uIPriority2 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed1 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle2 = new PrimaryStyle();
            Rsid rsid2 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties2 = new StyleParagraphProperties();
            KeepNext keepNext2 = new KeepNext();
            KeepLines keepLines2 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines4 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel2 = new OutlineLevel() { Val = 1 };

            styleParagraphProperties2.Append(keepNext2);
            styleParagraphProperties2.Append(keepLines2);
            styleParagraphProperties2.Append(spacingBetweenLines4);
            styleParagraphProperties2.Append(outlineLevel2);

            StyleRunProperties styleRunProperties2 = new StyleRunProperties();
            RunFonts runFonts40 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold9 = new Bold();
            BoldComplexScript boldComplexScript2 = new BoldComplexScript();
            Color color2 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };
            FontSize fontSize38 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript38 = new FontSizeComplexScript() { Val = "26" };

            styleRunProperties2.Append(runFonts40);
            styleRunProperties2.Append(bold9);
            styleRunProperties2.Append(boldComplexScript2);
            styleRunProperties2.Append(color2);
            styleRunProperties2.Append(fontSize38);
            styleRunProperties2.Append(fontSizeComplexScript38);

            style2.Append(styleName2);
            style2.Append(basedOn2);
            style2.Append(nextParagraphStyle2);
            style2.Append(linkedStyle2);
            style2.Append(uIPriority2);
            style2.Append(unhideWhenUsed1);
            style2.Append(primaryStyle2);
            style2.Append(rsid2);
            style2.Append(styleParagraphProperties2);
            style2.Append(styleRunProperties2);

            Style style3 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading3" };
            StyleName styleName3 = new StyleName() { Val = "heading 3" };
            BasedOn basedOn3 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle3 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle3 = new LinkedStyle() { Val = "Heading3Char" };
            UIPriority uIPriority3 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed2 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle3 = new PrimaryStyle();
            Rsid rsid3 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties3 = new StyleParagraphProperties();
            KeepNext keepNext3 = new KeepNext();
            KeepLines keepLines3 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines5 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel3 = new OutlineLevel() { Val = 2 };

            styleParagraphProperties3.Append(keepNext3);
            styleParagraphProperties3.Append(keepLines3);
            styleParagraphProperties3.Append(spacingBetweenLines5);
            styleParagraphProperties3.Append(outlineLevel3);

            StyleRunProperties styleRunProperties3 = new StyleRunProperties();
            RunFonts runFonts41 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold10 = new Bold();
            BoldComplexScript boldComplexScript3 = new BoldComplexScript();
            Color color3 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };

            styleRunProperties3.Append(runFonts41);
            styleRunProperties3.Append(bold10);
            styleRunProperties3.Append(boldComplexScript3);
            styleRunProperties3.Append(color3);

            style3.Append(styleName3);
            style3.Append(basedOn3);
            style3.Append(nextParagraphStyle3);
            style3.Append(linkedStyle3);
            style3.Append(uIPriority3);
            style3.Append(unhideWhenUsed2);
            style3.Append(primaryStyle3);
            style3.Append(rsid3);
            style3.Append(styleParagraphProperties3);
            style3.Append(styleRunProperties3);

            Style style4 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading4" };
            StyleName styleName4 = new StyleName() { Val = "heading 4" };
            BasedOn basedOn4 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle4 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle4 = new LinkedStyle() { Val = "Heading4Char" };
            UIPriority uIPriority4 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed3 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle4 = new PrimaryStyle();
            Rsid rsid4 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties4 = new StyleParagraphProperties();
            KeepNext keepNext4 = new KeepNext();
            KeepLines keepLines4 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines6 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel4 = new OutlineLevel() { Val = 3 };

            styleParagraphProperties4.Append(keepNext4);
            styleParagraphProperties4.Append(keepLines4);
            styleParagraphProperties4.Append(spacingBetweenLines6);
            styleParagraphProperties4.Append(outlineLevel4);

            StyleRunProperties styleRunProperties4 = new StyleRunProperties();
            RunFonts runFonts42 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold11 = new Bold();
            BoldComplexScript boldComplexScript4 = new BoldComplexScript();
            Italic italic1 = new Italic();
            ItalicComplexScript italicComplexScript1 = new ItalicComplexScript();
            Color color4 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };

            styleRunProperties4.Append(runFonts42);
            styleRunProperties4.Append(bold11);
            styleRunProperties4.Append(boldComplexScript4);
            styleRunProperties4.Append(italic1);
            styleRunProperties4.Append(italicComplexScript1);
            styleRunProperties4.Append(color4);

            style4.Append(styleName4);
            style4.Append(basedOn4);
            style4.Append(nextParagraphStyle4);
            style4.Append(linkedStyle4);
            style4.Append(uIPriority4);
            style4.Append(unhideWhenUsed3);
            style4.Append(primaryStyle4);
            style4.Append(rsid4);
            style4.Append(styleParagraphProperties4);
            style4.Append(styleRunProperties4);

            Style style5 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading5" };
            StyleName styleName5 = new StyleName() { Val = "heading 5" };
            BasedOn basedOn5 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle5 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle5 = new LinkedStyle() { Val = "Heading5Char" };
            UIPriority uIPriority5 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed4 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle5 = new PrimaryStyle();
            Rsid rsid5 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties5 = new StyleParagraphProperties();
            KeepNext keepNext5 = new KeepNext();
            KeepLines keepLines5 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines7 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel5 = new OutlineLevel() { Val = 4 };

            styleParagraphProperties5.Append(keepNext5);
            styleParagraphProperties5.Append(keepLines5);
            styleParagraphProperties5.Append(spacingBetweenLines7);
            styleParagraphProperties5.Append(outlineLevel5);

            StyleRunProperties styleRunProperties5 = new StyleRunProperties();
            RunFonts runFonts43 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Color color5 = new Color() { Val = "243F60", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "7F" };

            styleRunProperties5.Append(runFonts43);
            styleRunProperties5.Append(color5);

            style5.Append(styleName5);
            style5.Append(basedOn5);
            style5.Append(nextParagraphStyle5);
            style5.Append(linkedStyle5);
            style5.Append(uIPriority5);
            style5.Append(unhideWhenUsed4);
            style5.Append(primaryStyle5);
            style5.Append(rsid5);
            style5.Append(styleParagraphProperties5);
            style5.Append(styleRunProperties5);

            Style style6 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading6" };
            StyleName styleName6 = new StyleName() { Val = "heading 6" };
            BasedOn basedOn6 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle6 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle6 = new LinkedStyle() { Val = "Heading6Char" };
            UIPriority uIPriority6 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed5 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle6 = new PrimaryStyle();
            Rsid rsid6 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties6 = new StyleParagraphProperties();
            KeepNext keepNext6 = new KeepNext();
            KeepLines keepLines6 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines8 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel6 = new OutlineLevel() { Val = 5 };

            styleParagraphProperties6.Append(keepNext6);
            styleParagraphProperties6.Append(keepLines6);
            styleParagraphProperties6.Append(spacingBetweenLines8);
            styleParagraphProperties6.Append(outlineLevel6);

            StyleRunProperties styleRunProperties6 = new StyleRunProperties();
            RunFonts runFonts44 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Italic italic2 = new Italic();
            ItalicComplexScript italicComplexScript2 = new ItalicComplexScript();
            Color color6 = new Color() { Val = "243F60", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "7F" };

            styleRunProperties6.Append(runFonts44);
            styleRunProperties6.Append(italic2);
            styleRunProperties6.Append(italicComplexScript2);
            styleRunProperties6.Append(color6);

            style6.Append(styleName6);
            style6.Append(basedOn6);
            style6.Append(nextParagraphStyle6);
            style6.Append(linkedStyle6);
            style6.Append(uIPriority6);
            style6.Append(unhideWhenUsed5);
            style6.Append(primaryStyle6);
            style6.Append(rsid6);
            style6.Append(styleParagraphProperties6);
            style6.Append(styleRunProperties6);

            Style style7 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading7" };
            StyleName styleName7 = new StyleName() { Val = "heading 7" };
            BasedOn basedOn7 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle7 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle7 = new LinkedStyle() { Val = "Heading7Char" };
            UIPriority uIPriority7 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed6 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle7 = new PrimaryStyle();
            Rsid rsid7 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties7 = new StyleParagraphProperties();
            KeepNext keepNext7 = new KeepNext();
            KeepLines keepLines7 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines9 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel7 = new OutlineLevel() { Val = 6 };

            styleParagraphProperties7.Append(keepNext7);
            styleParagraphProperties7.Append(keepLines7);
            styleParagraphProperties7.Append(spacingBetweenLines9);
            styleParagraphProperties7.Append(outlineLevel7);

            StyleRunProperties styleRunProperties7 = new StyleRunProperties();
            RunFonts runFonts45 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Italic italic3 = new Italic();
            ItalicComplexScript italicComplexScript3 = new ItalicComplexScript();
            Color color7 = new Color() { Val = "404040", ThemeColor = ThemeColorValues.Text1, ThemeTint = "BF" };

            styleRunProperties7.Append(runFonts45);
            styleRunProperties7.Append(italic3);
            styleRunProperties7.Append(italicComplexScript3);
            styleRunProperties7.Append(color7);

            style7.Append(styleName7);
            style7.Append(basedOn7);
            style7.Append(nextParagraphStyle7);
            style7.Append(linkedStyle7);
            style7.Append(uIPriority7);
            style7.Append(unhideWhenUsed6);
            style7.Append(primaryStyle7);
            style7.Append(rsid7);
            style7.Append(styleParagraphProperties7);
            style7.Append(styleRunProperties7);

            Style style8 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading8" };
            StyleName styleName8 = new StyleName() { Val = "heading 8" };
            BasedOn basedOn8 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle8 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle8 = new LinkedStyle() { Val = "Heading8Char" };
            UIPriority uIPriority8 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed7 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle8 = new PrimaryStyle();
            Rsid rsid8 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties8 = new StyleParagraphProperties();
            KeepNext keepNext8 = new KeepNext();
            KeepLines keepLines8 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines10 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel8 = new OutlineLevel() { Val = 7 };

            styleParagraphProperties8.Append(keepNext8);
            styleParagraphProperties8.Append(keepLines8);
            styleParagraphProperties8.Append(spacingBetweenLines10);
            styleParagraphProperties8.Append(outlineLevel8);

            StyleRunProperties styleRunProperties8 = new StyleRunProperties();
            RunFonts runFonts46 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Color color8 = new Color() { Val = "404040", ThemeColor = ThemeColorValues.Text1, ThemeTint = "BF" };
            FontSize fontSize39 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript39 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties8.Append(runFonts46);
            styleRunProperties8.Append(color8);
            styleRunProperties8.Append(fontSize39);
            styleRunProperties8.Append(fontSizeComplexScript39);

            style8.Append(styleName8);
            style8.Append(basedOn8);
            style8.Append(nextParagraphStyle8);
            style8.Append(linkedStyle8);
            style8.Append(uIPriority8);
            style8.Append(unhideWhenUsed7);
            style8.Append(primaryStyle8);
            style8.Append(rsid8);
            style8.Append(styleParagraphProperties8);
            style8.Append(styleRunProperties8);

            Style style9 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading9" };
            StyleName styleName9 = new StyleName() { Val = "heading 9" };
            BasedOn basedOn9 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle9 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle9 = new LinkedStyle() { Val = "Heading9Char" };
            UIPriority uIPriority9 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed8 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle9 = new PrimaryStyle();
            Rsid rsid9 = new Rsid() { Val = "00263428" };

            StyleParagraphProperties styleParagraphProperties9 = new StyleParagraphProperties();
            KeepNext keepNext9 = new KeepNext();
            KeepLines keepLines9 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines11 = new SpacingBetweenLines() { Before = "200", After = "0" };
            OutlineLevel outlineLevel9 = new OutlineLevel() { Val = 8 };

            styleParagraphProperties9.Append(keepNext9);
            styleParagraphProperties9.Append(keepLines9);
            styleParagraphProperties9.Append(spacingBetweenLines11);
            styleParagraphProperties9.Append(outlineLevel9);

            StyleRunProperties styleRunProperties9 = new StyleRunProperties();
            RunFonts runFonts47 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Italic italic4 = new Italic();
            ItalicComplexScript italicComplexScript4 = new ItalicComplexScript();
            Color color9 = new Color() { Val = "404040", ThemeColor = ThemeColorValues.Text1, ThemeTint = "BF" };
            FontSize fontSize40 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript40 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties9.Append(runFonts47);
            styleRunProperties9.Append(italic4);
            styleRunProperties9.Append(italicComplexScript4);
            styleRunProperties9.Append(color9);
            styleRunProperties9.Append(fontSize40);
            styleRunProperties9.Append(fontSizeComplexScript40);

            style9.Append(styleName9);
            style9.Append(basedOn9);
            style9.Append(nextParagraphStyle9);
            style9.Append(linkedStyle9);
            style9.Append(uIPriority9);
            style9.Append(unhideWhenUsed8);
            style9.Append(primaryStyle9);
            style9.Append(rsid9);
            style9.Append(styleParagraphProperties9);
            style9.Append(styleRunProperties9);

            Style style10 = new Style() { Type = StyleValues.Character, StyleId = "Heading2Char", CustomStyle = true };
            StyleName styleName10 = new StyleName() { Val = "Heading 2 Char" };
            BasedOn basedOn10 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle10 = new LinkedStyle() { Val = "Heading2" };
            UIPriority uIPriority10 = new UIPriority() { Val = 9 };
            Rsid rsid10 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties10 = new StyleRunProperties();
            RunFonts runFonts48 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold12 = new Bold();
            BoldComplexScript boldComplexScript5 = new BoldComplexScript();
            Color color10 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };
            FontSize fontSize41 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript41 = new FontSizeComplexScript() { Val = "26" };

            styleRunProperties10.Append(runFonts48);
            styleRunProperties10.Append(bold12);
            styleRunProperties10.Append(boldComplexScript5);
            styleRunProperties10.Append(color10);
            styleRunProperties10.Append(fontSize41);
            styleRunProperties10.Append(fontSizeComplexScript41);

            style10.Append(styleName10);
            style10.Append(basedOn10);
            style10.Append(linkedStyle10);
            style10.Append(uIPriority10);
            style10.Append(rsid10);
            style10.Append(styleRunProperties10);

            Style style11 = new Style() { Type = StyleValues.Character, StyleId = "Heading3Char", CustomStyle = true };
            StyleName styleName11 = new StyleName() { Val = "Heading 3 Char" };
            BasedOn basedOn11 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle11 = new LinkedStyle() { Val = "Heading3" };
            UIPriority uIPriority11 = new UIPriority() { Val = 9 };
            Rsid rsid11 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties11 = new StyleRunProperties();
            RunFonts runFonts49 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold13 = new Bold();
            BoldComplexScript boldComplexScript6 = new BoldComplexScript();
            Color color11 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };

            styleRunProperties11.Append(runFonts49);
            styleRunProperties11.Append(bold13);
            styleRunProperties11.Append(boldComplexScript6);
            styleRunProperties11.Append(color11);

            style11.Append(styleName11);
            style11.Append(basedOn11);
            style11.Append(linkedStyle11);
            style11.Append(uIPriority11);
            style11.Append(rsid11);
            style11.Append(styleRunProperties11);

            Style style12 = new Style() { Type = StyleValues.Character, StyleId = "Heading4Char", CustomStyle = true };
            StyleName styleName12 = new StyleName() { Val = "Heading 4 Char" };
            BasedOn basedOn12 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle12 = new LinkedStyle() { Val = "Heading4" };
            UIPriority uIPriority12 = new UIPriority() { Val = 9 };
            Rsid rsid12 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties12 = new StyleRunProperties();
            RunFonts runFonts50 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold14 = new Bold();
            BoldComplexScript boldComplexScript7 = new BoldComplexScript();
            Italic italic5 = new Italic();
            ItalicComplexScript italicComplexScript5 = new ItalicComplexScript();
            Color color12 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };

            styleRunProperties12.Append(runFonts50);
            styleRunProperties12.Append(bold14);
            styleRunProperties12.Append(boldComplexScript7);
            styleRunProperties12.Append(italic5);
            styleRunProperties12.Append(italicComplexScript5);
            styleRunProperties12.Append(color12);

            style12.Append(styleName12);
            style12.Append(basedOn12);
            style12.Append(linkedStyle12);
            style12.Append(uIPriority12);
            style12.Append(rsid12);
            style12.Append(styleRunProperties12);

            Style style13 = new Style() { Type = StyleValues.Character, StyleId = "Heading5Char", CustomStyle = true };
            StyleName styleName13 = new StyleName() { Val = "Heading 5 Char" };
            BasedOn basedOn13 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle13 = new LinkedStyle() { Val = "Heading5" };
            UIPriority uIPriority13 = new UIPriority() { Val = 9 };
            Rsid rsid13 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties13 = new StyleRunProperties();
            RunFonts runFonts51 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Color color13 = new Color() { Val = "243F60", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "7F" };

            styleRunProperties13.Append(runFonts51);
            styleRunProperties13.Append(color13);

            style13.Append(styleName13);
            style13.Append(basedOn13);
            style13.Append(linkedStyle13);
            style13.Append(uIPriority13);
            style13.Append(rsid13);
            style13.Append(styleRunProperties13);

            Style style14 = new Style() { Type = StyleValues.Character, StyleId = "Heading6Char", CustomStyle = true };
            StyleName styleName14 = new StyleName() { Val = "Heading 6 Char" };
            BasedOn basedOn14 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle14 = new LinkedStyle() { Val = "Heading6" };
            UIPriority uIPriority14 = new UIPriority() { Val = 9 };
            Rsid rsid14 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties14 = new StyleRunProperties();
            RunFonts runFonts52 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Italic italic6 = new Italic();
            ItalicComplexScript italicComplexScript6 = new ItalicComplexScript();
            Color color14 = new Color() { Val = "243F60", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "7F" };

            styleRunProperties14.Append(runFonts52);
            styleRunProperties14.Append(italic6);
            styleRunProperties14.Append(italicComplexScript6);
            styleRunProperties14.Append(color14);

            style14.Append(styleName14);
            style14.Append(basedOn14);
            style14.Append(linkedStyle14);
            style14.Append(uIPriority14);
            style14.Append(rsid14);
            style14.Append(styleRunProperties14);

            Style style15 = new Style() { Type = StyleValues.Character, StyleId = "Heading7Char", CustomStyle = true };
            StyleName styleName15 = new StyleName() { Val = "Heading 7 Char" };
            BasedOn basedOn15 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle15 = new LinkedStyle() { Val = "Heading7" };
            UIPriority uIPriority15 = new UIPriority() { Val = 9 };
            Rsid rsid15 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties15 = new StyleRunProperties();
            RunFonts runFonts53 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Italic italic7 = new Italic();
            ItalicComplexScript italicComplexScript7 = new ItalicComplexScript();
            Color color15 = new Color() { Val = "404040", ThemeColor = ThemeColorValues.Text1, ThemeTint = "BF" };

            styleRunProperties15.Append(runFonts53);
            styleRunProperties15.Append(italic7);
            styleRunProperties15.Append(italicComplexScript7);
            styleRunProperties15.Append(color15);

            style15.Append(styleName15);
            style15.Append(basedOn15);
            style15.Append(linkedStyle15);
            style15.Append(uIPriority15);
            style15.Append(rsid15);
            style15.Append(styleRunProperties15);

            Style style16 = new Style() { Type = StyleValues.Character, StyleId = "Heading8Char", CustomStyle = true };
            StyleName styleName16 = new StyleName() { Val = "Heading 8 Char" };
            BasedOn basedOn16 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle16 = new LinkedStyle() { Val = "Heading8" };
            UIPriority uIPriority16 = new UIPriority() { Val = 9 };
            Rsid rsid16 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties16 = new StyleRunProperties();
            RunFonts runFonts54 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Color color16 = new Color() { Val = "404040", ThemeColor = ThemeColorValues.Text1, ThemeTint = "BF" };
            FontSize fontSize42 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript42 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties16.Append(runFonts54);
            styleRunProperties16.Append(color16);
            styleRunProperties16.Append(fontSize42);
            styleRunProperties16.Append(fontSizeComplexScript42);

            style16.Append(styleName16);
            style16.Append(basedOn16);
            style16.Append(linkedStyle16);
            style16.Append(uIPriority16);
            style16.Append(rsid16);
            style16.Append(styleRunProperties16);

            Style style17 = new Style() { Type = StyleValues.Character, StyleId = "Heading9Char", CustomStyle = true };
            StyleName styleName17 = new StyleName() { Val = "Heading 9 Char" };
            BasedOn basedOn17 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle17 = new LinkedStyle() { Val = "Heading9" };
            UIPriority uIPriority17 = new UIPriority() { Val = 9 };
            Rsid rsid17 = new Rsid() { Val = "00263428" };

            StyleRunProperties styleRunProperties17 = new StyleRunProperties();
            RunFonts runFonts55 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Italic italic8 = new Italic();
            ItalicComplexScript italicComplexScript8 = new ItalicComplexScript();
            Color color17 = new Color() { Val = "404040", ThemeColor = ThemeColorValues.Text1, ThemeTint = "BF" };
            FontSize fontSize43 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript43 = new FontSizeComplexScript() { Val = "20" };

            styleRunProperties17.Append(runFonts55);
            styleRunProperties17.Append(italic8);
            styleRunProperties17.Append(italicComplexScript8);
            styleRunProperties17.Append(color17);
            styleRunProperties17.Append(fontSize43);
            styleRunProperties17.Append(fontSizeComplexScript43);

            style17.Append(styleName17);
            style17.Append(basedOn17);
            style17.Append(linkedStyle17);
            style17.Append(uIPriority17);
            style17.Append(rsid17);
            style17.Append(styleRunProperties17);

            Style style18 = new Style() { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true };
            StyleName styleName18 = new StyleName() { Val = "Normal" };
            PrimaryStyle primaryStyle10 = new PrimaryStyle();
            Rsid rsid18 = new Rsid() { Val = "00B3465E" };

            style18.Append(styleName18);
            style18.Append(primaryStyle10);
            style18.Append(rsid18);

            Style style19 = new Style() { Type = StyleValues.Character, StyleId = "DefaultParagraphFont", Default = true };
            StyleName styleName19 = new StyleName() { Val = "Default Paragraph Font" };
            UIPriority uIPriority18 = new UIPriority() { Val = 1 };
            SemiHidden semiHidden1 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed9 = new UnhideWhenUsed();

            style19.Append(styleName19);
            style19.Append(uIPriority18);
            style19.Append(semiHidden1);
            style19.Append(unhideWhenUsed9);

            Style style20 = new Style() { Type = StyleValues.Table, StyleId = "TableNormal", Default = true };
            StyleName styleName20 = new StyleName() { Val = "Normal Table" };
            UIPriority uIPriority19 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden2 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed10 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle11 = new PrimaryStyle();

            StyleTableProperties styleTableProperties1 = new StyleTableProperties();
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            TopMargin topMargin1 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin1 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin1 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin1 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault1.Append(topMargin1);
            tableCellMarginDefault1.Append(tableCellLeftMargin1);
            tableCellMarginDefault1.Append(bottomMargin1);
            tableCellMarginDefault1.Append(tableCellRightMargin1);

            styleTableProperties1.Append(tableIndentation1);
            styleTableProperties1.Append(tableCellMarginDefault1);

            style20.Append(styleName20);
            style20.Append(uIPriority19);
            style20.Append(semiHidden2);
            style20.Append(unhideWhenUsed10);
            style20.Append(primaryStyle11);
            style20.Append(styleTableProperties1);

            Style style21 = new Style() { Type = StyleValues.Numbering, StyleId = "NoList", Default = true };
            StyleName styleName21 = new StyleName() { Val = "No List" };
            UIPriority uIPriority20 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden3 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed11 = new UnhideWhenUsed();

            style21.Append(styleName21);
            style21.Append(uIPriority20);
            style21.Append(semiHidden3);
            style21.Append(unhideWhenUsed11);

            Style style22 = new Style() { Type = StyleValues.Table, StyleId = "TableGrid" };
            StyleName styleName22 = new StyleName() { Val = "Table Grid" };
            BasedOn basedOn18 = new BasedOn() { Val = "TableNormal" };
            UIPriority uIPriority21 = new UIPriority() { Val = 59 };
            Rsid rsid19 = new Rsid() { Val = "008D0119" };

            StyleParagraphProperties styleParagraphProperties10 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines12 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties10.Append(spacingBetweenLines12);

            StyleTableProperties styleTableProperties2 = new StyleTableProperties();
            TableIndentation tableIndentation2 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "000000", ThemeColor = ThemeColorValues.Text1, Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "000000", ThemeColor = ThemeColorValues.Text1, Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder4 = new BottomBorder() { Val = BorderValues.Single, Color = "000000", ThemeColor = ThemeColorValues.Text1, Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Single, Color = "000000", ThemeColor = ThemeColorValues.Text1, Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "000000", ThemeColor = ThemeColorValues.Text1, Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "000000", ThemeColor = ThemeColorValues.Text1, Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(bottomBorder4);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);

            TableCellMarginDefault tableCellMarginDefault2 = new TableCellMarginDefault();
            TopMargin topMargin2 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin2 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin2 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin2 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault2.Append(topMargin2);
            tableCellMarginDefault2.Append(tableCellLeftMargin2);
            tableCellMarginDefault2.Append(bottomMargin2);
            tableCellMarginDefault2.Append(tableCellRightMargin2);

            styleTableProperties2.Append(tableIndentation2);
            styleTableProperties2.Append(tableBorders1);
            styleTableProperties2.Append(tableCellMarginDefault2);

            style22.Append(styleName22);
            style22.Append(basedOn18);
            style22.Append(uIPriority21);
            style22.Append(rsid19);
            style22.Append(styleParagraphProperties10);
            style22.Append(styleTableProperties2);

            styles1.Append(docDefaults1);
            styles1.Append(latentStyles1);
            styles1.Append(style1);
            styles1.Append(style2);
            styles1.Append(style3);
            styles1.Append(style4);
            styles1.Append(style5);
            styles1.Append(style6);
            styles1.Append(style7);
            styles1.Append(style8);
            styles1.Append(style9);
            styles1.Append(style10);
            styles1.Append(style11);
            styles1.Append(style12);
            styles1.Append(style13);
            styles1.Append(style14);
            styles1.Append(style15);
            styles1.Append(style16);
            styles1.Append(style17);
            styles1.Append(style18);
            styles1.Append(style19);
            styles1.Append(style20);
            styles1.Append(style21);
            styles1.Append(style22);

            styleDefinitionsPart1.Styles = styles1;
        }

        // Generates content of documentSettingsPart1.
        private void GenerateDocumentSettingsPart1Content(DocumentSettingsPart documentSettingsPart1)
        {
            Settings settings1 = new Settings();
            //settings1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            //settings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //settings1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            //settings1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            //settings1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            //settings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            //settings1.AddNamespaceDeclaration("sl", "http://schemas.openxmlformats.org/schemaLibrary/2006/main");
            EvenAndOddHeaders evenAndOddHeaders1 = new EvenAndOddHeaders();
            Zoom zoom1 = new Zoom() { Percent = "100" };
            DefaultTabStop defaultTabStop1 = new DefaultTabStop() { Val = 720 };
            CharacterSpacingControl characterSpacingControl1 = new CharacterSpacingControl() { Val = CharacterSpacingValues.DoNotCompress };
            Compatibility compatibility1 = new Compatibility();

            Rsids rsids1 = new Rsids();
            RsidRoot rsidRoot1 = new RsidRoot() { Val = "00217F62" };
            Rsid rsid20 = new Rsid() { Val = "001915A3" };
            Rsid rsid21 = new Rsid() { Val = "00217F62" };
            Rsid rsid22 = new Rsid() { Val = "00A906D8" };
            Rsid rsid23 = new Rsid() { Val = "00AB5A74" };
            Rsid rsid24 = new Rsid() { Val = "00F071AE" };

            rsids1.Append(rsidRoot1);
            rsids1.Append(rsid20);
            rsids1.Append(rsid21);
            rsids1.Append(rsid22);
            rsids1.Append(rsid23);
            rsids1.Append(rsid24);

            M.MathProperties mathProperties1 = new M.MathProperties();
            M.MathFont mathFont1 = new M.MathFont() { Val = "Cambria Math" };
            M.BreakBinary breakBinary1 = new M.BreakBinary() { Val = M.BreakBinaryOperatorValues.Before };
            M.BreakBinarySubtraction breakBinarySubtraction1 = new M.BreakBinarySubtraction() { Val = M.BreakBinarySubtractionValues.MinusMinus };
            M.SmallFraction smallFraction1 = new M.SmallFraction() { Val = M.BooleanValues.Off };
            M.DisplayDefaults displayDefaults1 = new M.DisplayDefaults();
            M.LeftMargin leftMargin5 = new M.LeftMargin() { Val = (UInt32Value)0U };
            M.RightMargin rightMargin1 = new M.RightMargin() { Val = (UInt32Value)0U };
            M.DefaultJustification defaultJustification1 = new M.DefaultJustification() { Val = M.JustificationValues.CenterGroup };
            M.WrapIndent wrapIndent1 = new M.WrapIndent() { Val = (UInt32Value)1440U };
            M.IntegralLimitLocation integralLimitLocation1 = new M.IntegralLimitLocation() { Val = M.LimitLocationValues.SubscriptSuperscript };
            M.NaryLimitLocation naryLimitLocation1 = new M.NaryLimitLocation() { Val = M.LimitLocationValues.UnderOver };

            mathProperties1.Append(mathFont1);
            mathProperties1.Append(breakBinary1);
            mathProperties1.Append(breakBinarySubtraction1);
            mathProperties1.Append(smallFraction1);
            mathProperties1.Append(displayDefaults1);
            mathProperties1.Append(leftMargin5);
            mathProperties1.Append(rightMargin1);
            mathProperties1.Append(defaultJustification1);
            mathProperties1.Append(wrapIndent1);
            mathProperties1.Append(integralLimitLocation1);
            mathProperties1.Append(naryLimitLocation1);
            ThemeFontLanguages themeFontLanguages1 = new ThemeFontLanguages() { Val = "ru-RU", Bidi = "ar-SA" };
            ColorSchemeMapping colorSchemeMapping1 = new ColorSchemeMapping() { Background1 = ColorSchemeIndexValues.Light1, Text1 = ColorSchemeIndexValues.Dark1, Background2 = ColorSchemeIndexValues.Light2, Text2 = ColorSchemeIndexValues.Dark2, Accent1 = ColorSchemeIndexValues.Accent1, Accent2 = ColorSchemeIndexValues.Accent2, Accent3 = ColorSchemeIndexValues.Accent3, Accent4 = ColorSchemeIndexValues.Accent4, Accent5 = ColorSchemeIndexValues.Accent5, Accent6 = ColorSchemeIndexValues.Accent6, Hyperlink = ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = ColorSchemeIndexValues.FollowedHyperlink };

            ShapeDefaults shapeDefaults1 = new ShapeDefaults();
            Ovml.ShapeDefaults shapeDefaults2 = new Ovml.ShapeDefaults() { Extension = V.ExtensionHandlingBehaviorValues.Edit, MaxShapeId = 2050 };

            Ovml.ShapeLayout shapeLayout1 = new Ovml.ShapeLayout() { Extension = V.ExtensionHandlingBehaviorValues.Edit };
            Ovml.ShapeIdMap shapeIdMap1 = new Ovml.ShapeIdMap() { Extension = V.ExtensionHandlingBehaviorValues.Edit, Data = "1" };

            shapeLayout1.Append(shapeIdMap1);

            shapeDefaults1.Append(shapeDefaults2);
            shapeDefaults1.Append(shapeLayout1);
            DecimalSymbol decimalSymbol1 = new DecimalSymbol() { Val = "." };
            ListSeparator listSeparator1 = new ListSeparator() { Val = "," };

            settings1.Append(evenAndOddHeaders1);
            settings1.Append(zoom1);
            settings1.Append(defaultTabStop1);
            settings1.Append(characterSpacingControl1);
            settings1.Append(compatibility1);
            settings1.Append(rsids1);
            settings1.Append(mathProperties1);
            settings1.Append(themeFontLanguages1);
            settings1.Append(colorSchemeMapping1);
            settings1.Append(shapeDefaults1);
            settings1.Append(decimalSymbol1);
            settings1.Append(listSeparator1);

            documentSettingsPart1.Settings = settings1;
        }

        // Generates content of footerPart1.
        private void GenerateFooterPart1Content(FooterPart footerPart1)
        {
            Footer footer1 = new Footer();
            //footer1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            //footer1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            //footer1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //footer1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            //footer1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            //footer1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            //footer1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            //footer1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            //footer1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

            Paragraph paragraph23 = new Paragraph() { RsidParagraphAddition = "009D472B", RsidRunAdditionDefault = "009D472B" };

            ParagraphProperties paragraphProperties23 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId() { Val = "footer" };

            paragraphProperties23.Append(paragraphStyleId1);

            paragraph23.Append(paragraphProperties23);

            footer1.Append(paragraph23);

            footerPart1.Footer = footer1;
        }

        // Generates content of footerPart2.
        private void GenerateFooterPart2Content(FooterPart footerPart2)
        {
            Footer footer2 = new Footer();

            Paragraph paragraph24 = new Paragraph() { RsidParagraphAddition = "009D472B", RsidRunAdditionDefault = "009D472B" };

            ParagraphProperties paragraphProperties24 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId2 = new ParagraphStyleId() { Val = "footer" };

            paragraphProperties24.Append(paragraphStyleId2);

            paragraph24.Append(paragraphProperties24);

            footer2.Append(paragraph24);

            footerPart2.Footer = footer2;
        }

        // Generates content of footerPart3.
        private void GenerateFooterPart3Content(FooterPart footerPart3)
        {
            Footer footer3 = new Footer();
            //footer3.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            //footer3.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            //footer3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //footer3.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            //footer3.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            //footer3.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            //footer3.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            //footer3.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            //footer3.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

            Paragraph paragraph25 = new Paragraph() { RsidParagraphAddition = "009D472B", RsidRunAdditionDefault = "009D472B" };

            ParagraphProperties paragraphProperties25 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId3 = new ParagraphStyleId() { Val = "footer" };

            paragraphProperties25.Append(paragraphStyleId3);

            paragraph25.Append(paragraphProperties25);

            Paragraph paragraph26 = new Paragraph();
            ParagraphProperties paragraphProperties26 = new ParagraphProperties();

            Run run42 = new Run();

            RunProperties runProperties38 = new RunProperties();
            Languages languages2 = new Languages() { Val = "ru-RU" };
            RunFonts runFonts56 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize44 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript44 = new FontSizeComplexScript() { Val = "20" };
            Color color18 = new Color() { Val = "000000" };

            runProperties38.Append(languages2);
            runProperties38.Append(runFonts56);
            runProperties38.Append(fontSize44);
            runProperties38.Append(fontSizeComplexScript44);
            runProperties38.Append(color18);
            Text text25 = new Text();
            text25.Text = "ОИ: Славгородцева И.А.";

            run42.Append(runProperties38);
            run42.Append(text25);

            Run run43 = new Run();

            RunProperties runProperties39 = new RunProperties();
            RunFonts runFonts57 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize45 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript45 = new FontSizeComplexScript() { Val = "20" };
            Color color19 = new Color() { Val = "000000" };

            runProperties39.Append(runFonts57);
            runProperties39.Append(fontSize45);
            runProperties39.Append(fontSizeComplexScript45);
            runProperties39.Append(color19);
            Break break18 = new Break();

            run43.Append(runProperties39);
            run43.Append(break18);

            Run run44 = new Run();

            RunProperties runProperties40 = new RunProperties();
            RunFonts runFonts58 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSize fontSize46 = new FontSize() { Val = "20" };
            FontSizeComplexScript fontSizeComplexScript46 = new FontSizeComplexScript() { Val = "20" };
            Color color20 = new Color() { Val = "000000" };

            runProperties40.Append(runFonts58);
            runProperties40.Append(fontSize46);
            runProperties40.Append(fontSizeComplexScript46);
            runProperties40.Append(color20);
            Text text26 = new Text();
            text26.Text = "Тел.: (495)400-46-96, (77)56-656";

            run44.Append(runProperties40);
            run44.Append(text26);

            paragraph26.Append(paragraphProperties26);
            paragraph26.Append(run42);
            paragraph26.Append(run43);
            paragraph26.Append(run44);

            footer3.Append(paragraph25);
            footer3.Append(paragraph26);

            footerPart3.Footer = footer3;
        }
    }
}
