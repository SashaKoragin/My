﻿using System;
using System.Drawing;
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
        public string GenerateQrCode(string fullPathAndNameFileNotFormat,string templateContent)
        {
            fullPathAndNameFileNotFormat = fullPathAndNameFileNotFormat + WordConstant.FormatPng;
            var options = new QrCodeEncodingOptions()
            {
                GS1Format = false,
                DisableECI = false,
                PureBarcode = false,
                CharacterSet = "UTF-8",
                Width = 120,
                Height = 120,
                Margin = 0
            };
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            writer.Write(templateContent).Save(fullPathAndNameFileNotFormat);
            return fullPathAndNameFileNotFormat;
        }
    }
}
