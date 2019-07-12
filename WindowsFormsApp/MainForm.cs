using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                        var number = int.Parse(item.Text);
                        this.lstChecked[number].Checked = true;
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

            this.btnDecodeWebCam_Click(this, null);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                this.lstChecked[i].Checked = false;
            }
        }
    }
}
