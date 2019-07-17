using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        private ChromiumWebBrowser webBrowser;
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

        private static Dictionary<int, string> mapIdToName = new Dictionary<int, string>()
        {
            [0] = "代币机制",
            [1] = "账本模式",
            [2] = "数字摘要",
            [3] = "数字签名",
            [4] = "智能合约",
            [5] = "权限控制",
            [6] = "接口设计",
            [7] = "共识机制",
        };


        public MainForm()
        {
            InitializeComponent();

            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            this.webBrowser = new ChromiumWebBrowser(@"about:blank");
            //this.webBrowser = new ChromiumWebBrowser(@"C:\Work\1-Blockchain\School\ChainIntro\dist\index.html");
            this.panWeb.Controls.Add(this.webBrowser);
            this.webBrowser.Dock = DockStyle.Fill;

            // Allow the use of local resources in the browser
            var browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            this.webBrowser.BrowserSettings = browserSettings;
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

        async void webCamTimer_Tick(object sender, EventArgs e)
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
                    await CheckResultAsync(item.Text);
                }

                txtStatus.Text = sb.ToString() + Environment.NewLine + txtStatus.Text;
                txtStatus.Text = txtStatus.Text.Substring(0, txtStatus.Text.Length > 1000 ? 1000 : txtStatus.Text.Length);
                //txtTypeWebCam.Text = result.BarcodeFormat.ToString();
                //txtContentWebCam.Text = result.Text;
            }
        }

        private async Task CheckResultAsync(string text)
        {
            if (text.StartsWith("id"))
            {
                //this.id = text;
                //this.idCheck.Checked = true;
            }
            else
            {
                var ntext = text.Replace("b.uchaindb.com/", "");
                if (int.TryParse(ntext, out var number))
                {
                    this.lstChecked[mapIdToCheckNumber[number]].Checked = true;
                    this.lstResult[mapIdToCheckNumber[number]] = number;
                    if (!reportFinished && this.lstChecked.All(_ => _.Checked))
                    {
                        await this.FinishReportAsync();
                    }
                }
            }
        }

        bool reportFinished = false;
        string ids = null;
        private async Task FinishReportAsync()
        {
            this.reportFinished = true;
            this.ids = string.Join(",", this.lstResult);
            var url = $@"C:\Work\1-Blockchain\School\ChainIntro\dist\index.html#/?a=%5B{ids}%5D&mode=report";
            this.Navigate(url);
        }

        private void ClearAllChecks()
        {
            this.Navigate(@"about:blank");
            reportFinished = false;
            //this.idCheck.Checked = false;
            for (int i = 0; i < 8; i++)
            {
                this.lstChecked[i].Checked = false;
            }
        }

        List<CheckBox> lstChecked = new List<CheckBox>();
        int[] lstResult = new int[8];
        //CheckBox idCheck;
        //string id;

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.webBrowser.IsBrowserInitializedChanged += WebBrowser_IsBrowserInitializedChanged;
            this.webBrowser.LoadError += WebBrowser_LoadError;
            this.webBrowser.FrameLoadEnd += WebBrowser_FrameLoadEnd;
            this.webBrowser.LoadingStateChanged += WebBrowser_LoadingStateChanged;
            //this.idCheck = new CheckBox()
            //{
            //    Text = "id",
            //    Enabled = false,
            //    Checked = false,
            //};
            //this.panScanResult.Controls.Add(this.idCheck);

            for (int i = 0; i < 8; i++)
            {
                var checkbox = new CheckBox()
                {
                    Text = mapIdToName[i],
                    Enabled = false,
                    Checked = false,
                    BackColor = Color.Wheat,
                    Margin = new Padding(0),
                };
                this.lstChecked.Add(checkbox);
                this.panScanResult.Controls.Add(checkbox);
            }

            this.btnDecodeWebCam_Click(this, null);
        }

        private string resultName;
        private string resultDesc;
        private double resultSimilarity;
        private string resultUrl;
        private bool resultDone = false;
        private void WebBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
        }

        private void WebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (reportFinished && e.Frame.IsMain)
            {
                this.webBrowser.Invoke((Action)(async () =>
                {
                    var ret = await this.webBrowser.GetMainFrame().EvaluateScriptAsync("getSimilarity()");

                    dynamic obj = ret.Result;
                    this.resultName = obj.name;
                    this.resultDesc = obj.desc;
                    this.resultSimilarity = obj.similarity;
                    this.resultUrl = obj.url;
                    this.resultDone = true;
                }));

            }
        }

        private void WebBrowser_LoadError(object sender, LoadErrorEventArgs e)
        {
            if (Debugger.IsAttached) Debugger.Break();
        }

        private void WebBrowser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            //this.webBrowser.Load(@"file:///C:/Work/1-Blockchain/School/ChainIntro/dist/index.html#/?a=%5B1,3,8,10,6,12,14,16%5D&e=%5B%5D&mode=arch");
            //this.webBrowser.Load(@"C:\Work\1-Blockchain\School\ChainIntro\dist\index.html#/?a=%5B1,3,8,10,6,12,14,16%5D&e=%5B%5D&mode=arch");
            //this.webBrowser.ShowDevTools();
        }

        private void Navigate(string url)
        {
            this.webBrowser.Invoke((Action)(() =>
            {
                this.webBrowser.Load(url);
            }));

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.ClearAllChecks();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (!this.resultDone) return;
            new PrintService().Print(this.resultName, this.resultDesc, this.resultSimilarity, this.resultUrl, "XP-58");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
