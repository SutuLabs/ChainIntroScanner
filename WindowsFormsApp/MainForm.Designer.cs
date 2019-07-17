namespace WindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picWebCam = new System.Windows.Forms.PictureBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnToggleCamScan = new System.Windows.Forms.Button();
            this.panScanResult = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panWeb = new System.Windows.Forms.Panel();
            this.panWatch = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).BeginInit();
            this.panWatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // picWebCam
            // 
            this.picWebCam.Location = new System.Drawing.Point(8, 3);
            this.picWebCam.Name = "picWebCam";
            this.picWebCam.Size = new System.Drawing.Size(218, 192);
            this.picWebCam.TabIndex = 0;
            this.picWebCam.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(458, 8);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(223, 187);
            this.txtStatus.TabIndex = 1;
            // 
            // btnToggleCamScan
            // 
            this.btnToggleCamScan.Location = new System.Drawing.Point(360, 8);
            this.btnToggleCamScan.Name = "btnToggleCamScan";
            this.btnToggleCamScan.Size = new System.Drawing.Size(73, 42);
            this.btnToggleCamScan.TabIndex = 2;
            this.btnToggleCamScan.Text = "启动/暂停";
            this.btnToggleCamScan.UseVisualStyleBackColor = true;
            this.btnToggleCamScan.Click += new System.EventHandler(this.btnDecodeWebCam_Click);
            // 
            // panScanResult
            // 
            this.panScanResult.Location = new System.Drawing.Point(232, 3);
            this.panScanResult.Name = "panScanResult";
            this.panScanResult.Size = new System.Drawing.Size(122, 192);
            this.panScanResult.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(360, 104);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(73, 42);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(360, 56);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(73, 42);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // panWeb
            // 
            this.panWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panWeb.Location = new System.Drawing.Point(1, 221);
            this.panWeb.Name = "panWeb";
            this.panWeb.Size = new System.Drawing.Size(721, 505);
            this.panWeb.TabIndex = 4;
            // 
            // panWatch
            // 
            this.panWatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panWatch.Controls.Add(this.picWebCam);
            this.panWatch.Controls.Add(this.panScanResult);
            this.panWatch.Controls.Add(this.btnToggleCamScan);
            this.panWatch.Controls.Add(this.btnPrint);
            this.panWatch.Controls.Add(this.txtStatus);
            this.panWatch.Controls.Add(this.btnClear);
            this.panWatch.Location = new System.Drawing.Point(12, 12);
            this.panWatch.Name = "panWatch";
            this.panWatch.Size = new System.Drawing.Size(697, 203);
            this.panWatch.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 725);
            this.Controls.Add(this.panWatch);
            this.Controls.Add(this.panWeb);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).EndInit();
            this.panWatch.ResumeLayout(false);
            this.panWatch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picWebCam;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnToggleCamScan;
        private System.Windows.Forms.FlowLayoutPanel panScanResult;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panWeb;
        private System.Windows.Forms.Panel panWatch;
    }
}

