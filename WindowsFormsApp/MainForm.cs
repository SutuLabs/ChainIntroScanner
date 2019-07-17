namespace WindowsFormsApp
{
    using CefSharp;
    using CefSharp.WinForms;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ZXing;

    public partial class MainForm : Form
    {
        private static Dictionary<int, int> mapIdToCheckNumber = new Dictionary<int, int>()
        {
            [0] = 0,
            [1] = 0,
            [2] = 0,
            [3] = 1,
            [4] = 1,
            [5] = 2,
            [6] = 2,
            [7] = 3,
            [8] = 3,
            [9] = 4,
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

        private const string baseUrl = @"site\index.html";
        private const string blankPageUrl = @"about:blank";
        private readonly BarcodeReader barcodeReader;
        private WebCam wCam;
        private Timer webCamTimer;
        private ChromiumWebBrowser webBrowser;
        private bool reportFinished = false;


        //CheckBox idCheck;
        //string id;
        private List<CheckBox> lstChecked = new List<CheckBox>();

        private int[] lstResult = new int[8];
        private string resultName;
        private string resultDesc;
        private double resultSimilarity;
        private string resultUrl;
        private bool resultDone = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeCef();
        }

        private void InitializeCef()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            this.webBrowser = new ChromiumWebBrowser(blankPageUrl);
            this.panWeb.Controls.Add(this.webBrowser);
            this.webBrowser.Dock = DockStyle.Fill;

            // Allow the use of local resources in the browser
            var browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            this.webBrowser.BrowserSettings = browserSettings;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.webBrowser.LoadError += WebBrowser_LoadError;
            this.webBrowser.FrameLoadEnd += WebBrowser_FrameLoadEnd;
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

            this.DisablePrint();
            this.btnDecodeWebCam_Click(this, null);
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

        private void webCamTimer_Tick(object sender, EventArgs e)
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
                        BarcodeFormat.QR_CODE,
                    }
                }
            };
            var result = reader.DecodeMultiple(bitmap);
            if (result != null)
            {
                var sb = new StringBuilder();
                foreach (var item in result)
                {
                    sb.AppendLine($"{item.BarcodeFormat}: {item.Text}");
                    CheckResult(item.Text);
                }

                AddStatus(sb.ToString());
            }
        }

        private void AddStatus(string txt)
        {
            txtStatus.Text = txt + Environment.NewLine + txtStatus.Text;
            txtStatus.Text = txtStatus.Text.Substring(0, txtStatus.Text.Length > 1000 ? 1000 : txtStatus.Text.Length);
        }

        private void CheckResult(string text)
        {
            if (text.StartsWith("id"))
            {
                //this.id = text;
                //this.idCheck.Checked = true;
            }
            else
            {
                var ntext = Regex.Replace(text, "[^0-9]", "");
                if (int.TryParse(ntext, out var number))
                {
                    this.lstChecked[mapIdToCheckNumber[number]].Checked = true;
                    this.lstResult[mapIdToCheckNumber[number]] = number;
                    if (!reportFinished && this.lstChecked.All(_ => _.Checked))
                    {
                        this.FinishReport();
                    }
                }
            }
        }

        private void FinishReport()
        {
            this.reportFinished = true;
            var ids = string.Join(",", this.lstResult);
            var location = Path.GetDirectoryName(Application.ExecutablePath);
            var path = Path.Combine(location, baseUrl);
            var url = $@"{path}#/?a=%5B{ids}%5D&mode=report";
            this.Navigate(url);
        }

        private void ClearAllChecks()
        {
            this.Navigate(blankPageUrl);
            reportFinished = false;
            //this.idCheck.Checked = false;
            for (int i = 0; i < 8; i++)
            {
                this.lstChecked[i].Checked = false;
            }
        }

        private void DisablePrint()
        {
            this.resultDone = false;
            this.btnPrint.Invoke((Action)(() =>
            {
                this.btnPrint.Enabled = false;
            }));
        }

        private void EnablePrint()
        {
            this.resultDone = true;
            this.btnPrint.Invoke((Action)(() =>
            {
                this.btnPrint.Enabled = true;
            }));
        }

        private void WebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (reportFinished && e.Frame.IsMain)
            {
                this.webBrowser.Invoke((Action)(async () =>
                {
                    var ret = await this.webBrowser.GetMainFrame().EvaluateScriptAsync("getSimilarity()");

                    try
                    {
                        dynamic obj = ret.Result;
                        this.resultName = obj.name;
                        this.resultDesc = obj.desc;
                        this.resultSimilarity = obj.similarity;
                        this.resultUrl = obj.url;
                        EnablePrint();
                    }
                    catch (RuntimeBinderException rbex)
                    {
                        this.AddStatus("Cannot resolve dynamic: " + rbex.Message);
                    }
                }));
            }
        }

        private void WebBrowser_LoadError(object sender, LoadErrorEventArgs e)
        {
            if (Debugger.IsAttached) Debugger.Break();
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
            this.DisablePrint();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (!this.resultDone) return;
            new PrintService().Print(this.resultName, this.resultDesc, this.resultSimilarity, this.resultUrl, "XP-58");
            this.ClearAllChecks();
            this.DisablePrint();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}