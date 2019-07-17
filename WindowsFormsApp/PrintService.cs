using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

namespace WindowsFormsApp
{
    public class PrintService
    {
        private string resultName;
        private string resultDesc;
        private double resultSimilarity;
        private string resultUrl;

        public void Print(string name, string desc, double similarity, string url, string defaultPrinter = null)
        {
            this.resultName = name;
            this.resultDesc = desc;
            this.resultSimilarity = similarity;
            this.resultUrl = url;

            var pdoc = new PrintDocument();
            pdoc.PrintPage += new PrintPageEventHandler(this.PrintDrawingHandler);
            var pdlg = new PrintDialog();
            pdlg.AllowSomePages = true;
            pdlg.ShowHelp = true;
            pdlg.Document = pdoc;
            if (string.IsNullOrWhiteSpace(defaultPrinter))
            {
                var result = pdlg.ShowDialog();
                if (result != DialogResult.OK)
                    return;
            }
            else
            {
                pdoc.PrinterSettings.PrinterName = defaultPrinter;
            }
            pdoc.Print();
        }

        private Bitmap GenQrCode(int size, string url)
        {
            var margin = 0;

            var qrCodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = size,
                    Width = size,
                    Margin = margin,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
                }
            };
            var pixelData = qrCodeWriter.Write(url);
            //var pixelData = qrCodeWriter.Write($"http://b.uchaindb.com/#/?a=%5B{ids}%5D&mode=arch");
            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, pixelData.Width, pixelData.Height),
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

            return bitmap;
        }

        private void PrintDrawingHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            var yhsmall = new Font("Microsoft YaHei", 7);
            var yhnormal = new Font("Microsoft YaHei", 8);
            var yhbig = new Font("Microsoft YaHei", 13);

            var g = ppeArgs.Graphics;
            float yPos = 0;
            float leftMargin = 0;
            float topMargin = 0;

            int paperWidth = 178;
            int qrcodesize = 130;


            yPos += topMargin;

            var height = 0;

            height = DrawText("与你的选择最相似的是:", yhsmall, g, yPos, leftMargin, paperWidth, 20);
            yPos += height;

            height = DrawText(this.resultName, yhbig, g, yPos, leftMargin, paperWidth, 20);
            yPos += height;

            height = DrawText($"相似度：{this.resultSimilarity * 100}%", yhsmall, g, yPos, leftMargin, paperWidth, 20);
            yPos += height;

            height = DrawText(this.resultDesc, yhnormal, g, yPos, leftMargin, 189, 200);
            yPos += height;

            var qrcode = GenQrCode(qrcodesize, this.resultUrl);
            g.DrawImage(qrcode, new PointF(leftMargin + (paperWidth - qrcodesize) / 2, yPos));
            yPos += qrcodesize;

            height = DrawText("（扫描二维码查看完整详细的报告） ", yhsmall, g, yPos, leftMargin + 10, paperWidth, 20);
            yPos += height;

            ppeArgs.HasMorePages = false;
        }

        private int DrawText(string text, Font font, Graphics g, float y, float x, int width, int maxHeight = 0, StringAlignment alignment = StringAlignment.Near)
        {
            var size = TextRenderer.MeasureText(text, font, new Size(width, maxHeight), TextFormatFlags.WordBreak);
            g.DrawString(text, font, Brushes.Black, new RectangleF(x, y, size.Width, size.Height),
                new StringFormat() { Alignment = alignment });
            return size.Height;
        }
    }
}
