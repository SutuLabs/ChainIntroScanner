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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.picWebCam = new System.Windows.Forms.PictureBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnToggleCamScan = new System.Windows.Forms.Button();
            this.panScanResult = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panWeb = new System.Windows.Forms.Panel();
            this.panWatch = new System.Windows.Forms.Panel();
            this.btnToggleStatus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).BeginInit();
            this.panWatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // picWebCam
            // 
            this.picWebCam.Location = new System.Drawing.Point(12, 4);
            this.picWebCam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picWebCam.Name = "picWebCam";
            this.picWebCam.Size = new System.Drawing.Size(302, 266);
            this.picWebCam.TabIndex = 0;
            this.picWebCam.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(12, 602);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(300, 138);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.Visible = false;
            // 
            // btnToggleCamScan
            // 
            this.btnToggleCamScan.Location = new System.Drawing.Point(204, 285);
            this.btnToggleCamScan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnToggleCamScan.Name = "btnToggleCamScan";
            this.btnToggleCamScan.Size = new System.Drawing.Size(110, 58);
            this.btnToggleCamScan.TabIndex = 2;
            this.btnToggleCamScan.Text = "启动/暂停";
            this.btnToggleCamScan.UseVisualStyleBackColor = true;
            this.btnToggleCamScan.Click += new System.EventHandler(this.btnDecodeWebCam_Click);
            // 
            // panScanResult
            // 
            this.panScanResult.Location = new System.Drawing.Point(12, 278);
            this.panScanResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panScanResult.Name = "panScanResult";
            this.panScanResult.Size = new System.Drawing.Size(183, 316);
            this.panScanResult.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(204, 418);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 58);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPrint.Location = new System.Drawing.Point(204, 352);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 58);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // panWeb
            // 
            this.panWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panWeb.Location = new System.Drawing.Point(356, 17);
            this.panWeb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panWeb.Name = "panWeb";
            this.panWeb.Size = new System.Drawing.Size(996, 759);
            this.panWeb.TabIndex = 4;
            // 
            // panWatch
            // 
            this.panWatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panWatch.Controls.Add(this.picWebCam);
            this.panWatch.Controls.Add(this.panScanResult);
            this.panWatch.Controls.Add(this.btnToggleCamScan);
            this.panWatch.Controls.Add(this.btnPrint);
            this.panWatch.Controls.Add(this.txtStatus);
            this.panWatch.Controls.Add(this.btnToggleStatus);
            this.panWatch.Controls.Add(this.btnClear);
            this.panWatch.Location = new System.Drawing.Point(18, 17);
            this.panWatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panWatch.Name = "panWatch";
            this.panWatch.Size = new System.Drawing.Size(328, 759);
            this.panWatch.TabIndex = 4;
            // 
            // btnToggleStatus
            // 
            this.btnToggleStatus.Location = new System.Drawing.Point(202, 562);
            this.btnToggleStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnToggleStatus.Name = "btnToggleStatus";
            this.btnToggleStatus.Size = new System.Drawing.Size(110, 32);
            this.btnToggleStatus.TabIndex = 2;
            this.btnToggleStatus.Text = "↓↓↓";
            this.btnToggleStatus.UseVisualStyleBackColor = true;
            this.btnToggleStatus.Click += new System.EventHandler(this.BtnToggleStatus_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 792);
            this.Controls.Add(this.panWatch);
            this.Controls.Add(this.panWeb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "区块链架构设计工具";
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
        private System.Windows.Forms.Button btnToggleStatus;
    }
}

