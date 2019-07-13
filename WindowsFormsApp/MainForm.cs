using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private WebCam wCam;
        private Timer webCamTimer;
        private readonly BarcodeReader barcodeReader;
        private static Dictionary<int, int> mapIdToCheckNumber = new Dictionary<int, int>()
        {
            [0] = 0,
            [1] = 0,
            [2] = 0,
            [3] = 1,
            [4] = 1,
            [5] = 2,
            [6] = 2,
            [7] = 2,
            [8] = 3,
            [9] = 3,
            [10] = 4,
            [11] = 4,
            [12] = 5,
            [13] = 5,
            [14] = 6,
            [15] = 6,
            [16] = 7,
            [17] = 7,
            [18] = 7,
            [19] = 7,
        };

        public MainForm()
        {
            InitializeComponent();
        }


        private void btnDecodeWebCam_Click(object sender, EventArgs e)
        {
            if (wCam == null)
            {
                wCam = new WebCam { Container = picWebCam };

                wCam.OpenConnection();

                webCamTimer = new Timer();
                webCamTimer.Tick += webCamTimer_Tick;
                webCamTimer.Interval = 200;
                webCamTimer.Start();
            }
            else
            {
                webCamTimer.Stop();
                webCamTimer = null;
                wCam.Dispose();
                wCam = null;
            }
        }

        void webCamTimer_Tick(object sender, EventArgs e)
        {
            var bitmap = wCam.GetCurrentImage();
            if (bitmap == null)
                return;
            var reader = new BarcodeReader()
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
            var result = reader.DecodeMultiple(bitmap);
            if (result != null)
            {
                //txtTypeWebCam.Text = result.BarcodeFormat.ToString();
                //txtContentWebCam.Text = result.Text;
                var sb = new StringBuilder();
                foreach (var item in result)
                {
                    sb.AppendLine($"{item.BarcodeFormat}: {item.Text}");
                    if (item.Text.StartsWith("id"))
                    {
                        this.id = item.Text;
                        this.idCheck.Checked = true;
                        for (int i = 0; i < 8; i++)
                        {
                            this.lstChecked[i].Checked = false;
                        }
                    }
                    else
                    {
                        var text = item.Text.Replace("b.uchaindb.com/", "");
                        var number = int.Parse(text);
                        this.lstChecked[mapIdToCheckNumber[number]].Checked = true;
                    }
                }

                txtStatus.Text = sb.ToString() + Environment.NewLine + txtStatus.Text;
                txtStatus.Text = txtStatus.Text.Substring(0, txtStatus.Text.Length > 1000 ? 1000 : txtStatus.Text.Length);
                //txtTypeWebCam.Text = result.BarcodeFormat.ToString();
                //txtContentWebCam.Text = result.Text;
            }
        }

        List<CheckBox> lstChecked = new List<CheckBox>();
        CheckBox idCheck;
        string id;
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.idCheck = new CheckBox()
            {
                Text = "id",
                Enabled = false,
                Checked = false,
            };
            this.panScanResult.Controls.Add(this.idCheck);

            for (int i = 0; i < 8; i++)
            {
                var checkbox = new CheckBox()
                {
                    Text = $"Num {i}",
                    Enabled = false,
                    Checked = false,
                };
                this.lstChecked.Add(checkbox);
                this.panScanResult.Controls.Add(checkbox);
            }

            //this.btnDecodeWebCam_Click(this, null);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                this.lstChecked[i].Checked = false;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            var pdoc = new PrintDocument();
            pdoc.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);
            var pdlg = new PrintDialog();
            pdlg.AllowSomePages = true;
            pdlg.ShowHelp = true;
            pdlg.Document = pdoc;
            var result = pdlg.ShowDialog();
            pdoc.Print();
        }
        private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            var verdana10Font = new Font("Verdana", 6);

            //Get the Graphics object
            Graphics g = ppeArgs.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs
            //float leftMargin = ppeArgs.MarginBounds.Left;
            //float topMargin = ppeArgs.MarginBounds.Top;
            float leftMargin = 0;
            float topMargin = 0;
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font
            linesPerPage = ppeArgs.MarginBounds.Height / verdana10Font.GetHeight(g);

            var reader = new StreamReader(GenerateStreamFromString("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "));

            //Now read lines one by one, using StreamReader
            while (count < linesPerPage && ((line = reader.ReadLine()) != null))
            {
                //Calculate the starting position
                yPos = topMargin + (count * verdana10Font.GetHeight(g));
                //Draw text
                g.DrawString(line, verdana10Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                //Move to next line
                count++;
            }
            //If PrintPageEventArgs has more pages to print
            if (line != null)
            {
                ppeArgs.HasMorePages = true;
            }
            else
            {
                ppeArgs.HasMorePages = false;
            }
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
