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
            var reader = new BarcodeReader();
            var result = reader.DecodeMultiple(bitmap);
            if (result != null)
            {
                //txtTypeWebCam.Text = result.BarcodeFormat.ToString();
                //txtContentWebCam.Text = result.Text;
                var sb = new StringBuilder();
                foreach (var item in result)
                {
                    sb.AppendLine($"{item.BarcodeFormat}: {item.Text}");
                }

                txtStatus.Text = sb.ToString() + Environment.NewLine + txtStatus.Text;
                txtStatus.Text = txtStatus.Text.Substring(0, txtStatus.Text.Length > 1000? 1000: txtStatus.Text.Length);
                //txtTypeWebCam.Text = result.BarcodeFormat.ToString();
                //txtContentWebCam.Text = result.Text;
            }
        }

    }
}
