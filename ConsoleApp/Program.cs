using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Datamatrix;
using ZXing.Windows.Compatibility;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //Read();
            Write();
        }

        private static void Write()
        {
            //var writer = new BarcodeWriterPixelData() { Format = BarcodeFormat.QR_CODE };
            //var b = writer.Write("000000000");
            //var writer = new QRCodeWriter();
            var size = 80;
            var margin = 0;
            //var matrix = writer.encode("000000000", BarcodeFormat.QR_CODE, size, size, null);

            for (int i = 0; i < 15; i++)
            {
                var qrCodeWriter = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions { Height = size, Width = size, Margin = margin }
                    //Format = BarcodeFormat.DATA_MATRIX,
                    //Options = new DatamatrixEncodingOptions { Height = size, Width = size, Margin = margin }
                };
                var pixelData = qrCodeWriter.Write($"b.uchaindb.com/{i:00}");
                // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference
                // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB
                using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                //using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                        pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    // save to stream as PNG
                    //bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    bitmap.Save($"00{i}.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private static void Read()
        {
            IBarcodeReader reader = new ZXing.BarcodeReader()
            {
                Options = new ZXing.Common.DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat> {
                        //BarcodeFormat.DATA_MATRIX,
                        BarcodeFormat.QR_CODE,
                    }
                }
            };
            // load a bitmap
            //var barcodeBitmap = (Bitmap)Image.FromFile(@"C:\Users\icer\Source\repos\QrCodeTest\ConsoleApp\bin\Debug\netcoreapp2.2\0011.png");
            var barcodeBitmap = (Bitmap)Image.FromFile(@"C:\Users\icer\Downloads\WIN_20190712_13_56_13_Pro.jpg");
            //var barcodeBitmap = (Bitmap)Image.FromFile(@"..\..\..\qrtest3.png");
            //// detect and decode the barcode inside the bitmap
            //var result = reader.Decode(barcodeBitmap);
            //// do something with the result
            //if (result != null)
            //{
            //    Debugger.Break();
            //    //txtDecoderType.Text = result.BarcodeFormat.ToString();
            //    //txtDecoderContent.Text = result.Text;
            //}

            using (barcodeBitmap)
            {
                LuminanceSource source;
                source = new BitmapLuminanceSource(barcodeBitmap);
                BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
                //var result = new MultiFormatReader().decode(bitmap);
                var result = reader.DecodeMultiple(source);
                //reader.DecodeMultiple()
                if (result != null)
                {
                    Debugger.Break();
                }
            }
        }
    }
}
