using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using EfDatabase.XsdBookAccounting;
using EfDatabaseInvoice;
using EfDatabaseUploadFile;
using ZXing;
using LibaryDocumentGenerator.Documents.Constant;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

namespace LibaryDocumentGenerator.Barcode
{
   public class GenerateBarcode
    {
        /// <summary>
        /// Генерация штрихкода на книги учета материальных ценностей
        /// </summary>
        /// <param name="barecode">Параметры кода</param>
        /// <param name="path">Путь сохранения</param>
        public void GenerateBookCode(BareCodeBook barecode, string path)
        {
            barecode.FullPathSave = path + barecode.NameModel + WordConstant.FormatPng;
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options =
                {
                    PureBarcode = true,
                    Margin = 0,
                    Height = 75,
                    Width = 200
                }
            };
            writer.Write(barecode.Id.ToString()).Save(barecode.FullPathSave);
        }

        /// <summary>
        /// Генерация шрихкода
        /// </summary>
        /// <param name="report">Отчет для гениерации</param>
        /// <param name="path">Путь к сохранению</param>
        public void GenerateCode(ref Report report,string path)
        {
            report.Main.Barcode.PathBarcode = path + report.Main.Received.NameUser + WordConstant.FormatPng;
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options =
                {
                    PureBarcode = true,
                    Margin = 0,
                    Height = 75,
                    Width = 200
                }
            };
            writer.Write(report.Main.Barcode.Id.ToString()).Save(report.Main.Barcode.PathBarcode);
        }
        /// <summary>
        /// Разпознование кодов в PNG файле
        /// </summary>
        /// <param name="upload">Модель файла</param>
        /// <param name="pathsavepng">Путь сохранения PNG</param>
        public void DecodeBarCodePng(ref Upload upload, string pathsavepng)
        {
            var path = Tiff2Pngs(upload.BlobFile, pathsavepng);
            foreach (var p in path)
            {
                using (Bitmap bitmap = new Bitmap(p))
                {
                    BarcodeReader reader = new BarcodeReader { AutoRotate = true, Options = new DecodingOptions() { TryHarder = true } };
                    Result result = reader.Decode(bitmap);
                    if (result != null)
                    {
                        upload.IdDocument = Convert.ToInt32(result.Text);
                    }
                }
                File.Delete(p);
            }
        }

        /// <summary>
        /// Конвертер из tif в PNG
        /// </summary>
        /// <param name="bytestream">Массив байтов</param>
        /// <param name="pathsavepng">Путь сохранения png файла</param>
        /// <returns></returns>
        private string[] Tiff2Pngs(byte[] bytestream, string pathsavepng)
        {
            string[] pngPaths;
            using (Stream strfile = new MemoryStream(bytestream))
            {
                TiffBitmapDecoder tifDecoder = new TiffBitmapDecoder(strfile,BitmapCreateOptions.PreservePixelFormat,BitmapCacheOption.Default);
                pngPaths = new string[tifDecoder.Frames.Count];
                for (int i = 0; i < tifDecoder.Frames.Count; i++)
                {
                    PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(tifDecoder.Frames[i]);
                    string pngPath = pathsavepng + i + ".png";
                    pngPaths[i] = pngPath;
                    using (FileStream pngFs = new FileStream(pngPath, FileMode.Create))
                    {
                        pngEncoder.Save(pngFs);
                    }
                }
            }
            return pngPaths;
        }
        /// <summary>
        /// Генерация QR CODE
        /// </summary>
        /// <param name="fullPathAndNameFileNotFormat">Путь сохранения</param>
        /// <param name="templateContent"></param>
        /// <returns>Полный путь сохранения</returns>
        public string GenerateQrCode(string fullPathAndNameFileNotFormat, string templateContent)
        {
            fullPathAndNameFileNotFormat = fullPathAndNameFileNotFormat + WordConstant.FormatPng;
            var options = new QrCodeEncodingOptions()
            {
                GS1Format = false,
                DisableECI = false,
                PureBarcode = false,
                CharacterSet = "UTF-8",
                Width = 200,
                Height = 200,
                Margin = 0
                
            };
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            writer.Write(templateContent).Save(fullPathAndNameFileNotFormat);
            return fullPathAndNameFileNotFormat;
        }
        /// <summary>
        /// Генерация штрих-кода для этикеток
        /// </summary>
        /// <param name="fullPathAndNameFileNotFormat">Путь сохранения</param>
        /// <param name="inventoryNumber"></param>
        /// <returns></returns>
        public string GenerateCode128(string fullPathAndNameFileNotFormat, string inventoryNumber)
        {
            fullPathAndNameFileNotFormat = fullPathAndNameFileNotFormat + WordConstant.FormatPng;
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options =
                {
                    Width = 200
                }
            };
            var bmp = writer.Write(inventoryNumber);
            ScaleImages(bmp, 500, 1000).Save(fullPathAndNameFileNotFormat, ImageFormat.Png);
            return fullPathAndNameFileNotFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPathSave"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        public string GeneratePdf417(string fullPathSave, string templateCode)
        {
            fullPathSave = fullPathSave + templateCode + WordConstant.FormatPng;
            var writer = new BarcodeWriter()
            {
                Format = BarcodeFormat.PDF_417,
                Options =
                {
                    PureBarcode = true,
                    Margin = 0,
                    Width = 1000,
                    Height = 750
                },
            };
            var bmp = writer.Write(templateCode);
            ScaleImages(bmp, 500, 1000).Save(fullPathSave, ImageFormat.Png);
            return fullPathSave;
        }


        private Bitmap ScaleImages(Bitmap bmp, double maxWidth, double maxHeight)
        {
            var ratioX = maxWidth / bmp.Width;
            var ratioY = maxHeight / bmp.Height;
            var ratioMin = Math.Min(ratioX, ratioY);
            var ratioMax = Math.Max(ratioX, ratioY);
            var newWidth = (int)(bmp.Width * ratioMin);
            var newHeight = (int)(bmp.Height * ratioMax);
            var newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Default;

                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

    }
}
