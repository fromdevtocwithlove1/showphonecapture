namespace ScrcpyController
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.connectionPanel = new System.Windows.Forms.Panel();
            this.lblConnectionHint = new System.Windows.Forms.Label();
            this.txtDeviceIP = new System.Windows.Forms.TextBox();
            this.lblDeviceIP = new System.Windows.Forms.Label();
            this.txtPairEndpoint = new System.Windows.Forms.TextBox();
            this.lblPairEndpoint = new System.Windows.Forms.Label();
            this.txtPairCode = new System.Windows.Forms.TextBox();
            this.lblPairCode = new System.Windows.Forms.Label();
            this.chkTurnOffScreen = new System.Windows.Forms.CheckBox();
            this.chk720p = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSetupTcpip = new System.Windows.Forms.Button();
            this.btnCheckDevices = new System.Windows.Forms.Button();
            this.btnPushFile = new System.Windows.Forms.Button();
            this.btnConnectUSB = new System.Windows.Forms.Button();
            this.btnPair = new System.Windows.Forms.Button();
            this.historyPanel = new System.Windows.Forms.Panel();
            this.lstIPHistory = new System.Windows.Forms.ListBox();
            this.lblHistoryHint = new System.Windows.Forms.Label();
            this.lblHistory = new System.Windows.Forms.Label();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.logPanel = new System.Windows.Forms.Panel();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblStatusHint = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.connectionPanel.SuspendLayout();
            this.historyPanel.SuspendLayout();
            this.logPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Location = new System.Drawing.Point(20, 18);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(740, 96);
            this.headerPanel.TabIndex = 14;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(103)))), ((int)(((byte)(128)))));
            this.lblSubtitle.Location = new System.Drawing.Point(26, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(500, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Kết nối điện thoại qua ADB WiFi, mở Scrcpy và gửi ảnh/video vào DCIM nhanh chóng";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(28)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(284, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ShowPhone Capture";
            // 
            // connectionPanel
            // 
            this.connectionPanel.BackColor = System.Drawing.Color.White;
            this.connectionPanel.Controls.Add(this.lblConnectionHint);
            this.connectionPanel.Controls.Add(this.txtDeviceIP);
            this.connectionPanel.Controls.Add(this.lblDeviceIP);
            this.connectionPanel.Controls.Add(this.txtPairEndpoint);
            this.connectionPanel.Controls.Add(this.lblPairEndpoint);
            this.connectionPanel.Controls.Add(this.txtPairCode);
            this.connectionPanel.Controls.Add(this.lblPairCode);
            this.connectionPanel.Controls.Add(this.chkTurnOffScreen);
            this.connectionPanel.Controls.Add(this.chk720p);
            this.connectionPanel.Controls.Add(this.btnConnect);
            this.connectionPanel.Controls.Add(this.btnRefresh);
            this.connectionPanel.Controls.Add(this.btnSetupTcpip);
            this.connectionPanel.Controls.Add(this.btnCheckDevices);
            this.connectionPanel.Controls.Add(this.btnPushFile);
            this.connectionPanel.Controls.Add(this.btnConnectUSB);
            this.connectionPanel.Controls.Add(this.btnPair);
            this.connectionPanel.Location = new System.Drawing.Point(20, 130);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Size = new System.Drawing.Size(450, 442);
            this.connectionPanel.TabIndex = 15;
            // 
            // lblConnectionHint
            // 
            this.lblConnectionHint.AutoSize = true;
            this.lblConnectionHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConnectionHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(113)))), ((int)(((byte)(137)))));
            this.lblConnectionHint.Location = new System.Drawing.Point(24, 22);
            this.lblConnectionHint.Name = "lblConnectionHint";
            this.lblConnectionHint.Size = new System.Drawing.Size(268, 15);
            this.lblConnectionHint.TabIndex = 13;
            this.lblConnectionHint.Text = "Nhập IP thiết bị hoặc chọn nhanh từ lịch sử bên phải";
            // 
            // txtDeviceIP
            // 
            this.txtDeviceIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtDeviceIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceIP.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.txtDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtDeviceIP.Location = new System.Drawing.Point(104, 52);
            this.txtDeviceIP.Name = "txtDeviceIP";
            this.txtDeviceIP.Size = new System.Drawing.Size(308, 31);
            this.txtDeviceIP.TabIndex = 0;
            this.txtDeviceIP.Text = "192.168.1.100";
            // 
            // lblDeviceIP
            // 
            this.lblDeviceIP.AutoSize = true;
            this.lblDeviceIP.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(68)))));
            this.lblDeviceIP.Location = new System.Drawing.Point(24, 58);
            this.lblDeviceIP.Name = "lblDeviceIP";
            this.lblDeviceIP.Size = new System.Drawing.Size(66, 19);
            this.lblDeviceIP.TabIndex = 1;
            this.lblDeviceIP.Text = "IP hoặc IP:cổng";
            // 
            // lblPairEndpoint
            // 
            this.lblPairEndpoint.AutoSize = true;
            this.lblPairEndpoint.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblPairEndpoint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(68)))));
            this.lblPairEndpoint.Location = new System.Drawing.Point(24, 96);
            this.lblPairEndpoint.Name = "lblPairEndpoint";
            this.lblPairEndpoint.Size = new System.Drawing.Size(73, 19);
            this.lblPairEndpoint.TabIndex = 18;
            this.lblPairEndpoint.Text = "Pair IP:cổng";
            // 
            // txtPairEndpoint
            // 
            this.txtPairEndpoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtPairEndpoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPairEndpoint.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.txtPairEndpoint.Location = new System.Drawing.Point(104, 92);
            this.txtPairEndpoint.Name = "txtPairEndpoint";
            this.txtPairEndpoint.Size = new System.Drawing.Size(165, 27);
            this.txtPairEndpoint.TabIndex = 19;
            this.txtPairEndpoint.PlaceholderText = "192.168.1.100:37123";
            // 
            // lblPairCode
            // 
            this.lblPairCode.AutoSize = true;
            this.lblPairCode.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblPairCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(68)))));
            this.lblPairCode.Location = new System.Drawing.Point(280, 96);
            this.lblPairCode.Name = "lblPairCode";
            this.lblPairCode.Size = new System.Drawing.Size(34, 19);
            this.lblPairCode.TabIndex = 20;
            this.lblPairCode.Text = "Mã";
            // 
            // txtPairCode
            // 
            this.txtPairCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtPairCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPairCode.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.txtPairCode.Location = new System.Drawing.Point(319, 92);
            this.txtPairCode.Name = "txtPairCode";
            this.txtPairCode.Size = new System.Drawing.Size(93, 27);
            this.txtPairCode.TabIndex = 21;
            this.txtPairCode.MaxLength = 12;
            this.txtPairCode.PasswordChar = '•';
            // 
            // chkTurnOffScreen
            // 
            this.chkTurnOffScreen.AutoSize = true;
            this.chkTurnOffScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTurnOffScreen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkTurnOffScreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(56)))), ((int)(((byte)(74)))));
            this.chkTurnOffScreen.Location = new System.Drawing.Point(28, 130);
            this.chkTurnOffScreen.Name = "chkTurnOffScreen";
            this.chkTurnOffScreen.Size = new System.Drawing.Size(180, 23);
            this.chkTurnOffScreen.TabIndex = 2;
            this.chkTurnOffScreen.Text = "Tắt màn hình khi kết nối";
            this.chkTurnOffScreen.UseVisualStyleBackColor = true;
            // 
            // chk720p
            // 
            this.chk720p.AutoSize = true;
            this.chk720p.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk720p.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chk720p.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(56)))), ((int)(((byte)(74)))));
            this.chk720p.Location = new System.Drawing.Point(240, 130);
            this.chk720p.Name = "chk720p";
            this.chk720p.Size = new System.Drawing.Size(133, 23);
            this.chk720p.TabIndex = 3;
            this.chk720p.Text = "Giới hạn 720p";
            this.chk720p.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(28, 174);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(148, 50);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Kết nối Scrcpy";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(182, 174);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 50);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "↺";
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSetupTcpip
            // 
            this.btnSetupTcpip.Location = new System.Drawing.Point(226, 174);
            this.btnSetupTcpip.Name = "btnSetupTcpip";
            this.btnSetupTcpip.Size = new System.Drawing.Size(186, 50);
            this.btnSetupTcpip.TabIndex = 5;
            this.btnSetupTcpip.Text = "Cài đặt TCP/IP";
            this.btnSetupTcpip.Click += new System.EventHandler(this.btnSetupTcpip_Click);
            // 
            // btnCheckDevices
            // 
            this.btnCheckDevices.Location = new System.Drawing.Point(28, 240);
            this.btnCheckDevices.Name = "btnCheckDevices";
            this.btnCheckDevices.Size = new System.Drawing.Size(186, 36);
            this.btnCheckDevices.TabIndex = 11;
            this.btnCheckDevices.Text = "Kiểm tra USB";
            this.btnCheckDevices.Click += new System.EventHandler(this.btnCheckDevices_Click);
            // 
            // btnPushFile
            // 
            this.btnPushFile.Location = new System.Drawing.Point(226, 240);
            this.btnPushFile.Name = "btnPushFile";
            this.btnPushFile.Size = new System.Drawing.Size(186, 36);
            this.btnPushFile.TabIndex = 12;
            this.btnPushFile.Text = "Gửi file vào DCIM";
            this.btnPushFile.Click += new System.EventHandler(this.btnPushFile_Click);
            // 
            // btnConnectUSB
            // 
            this.btnConnectUSB.Location = new System.Drawing.Point(28, 334);
            this.btnConnectUSB.Name = "btnConnectUSB";
            this.btnConnectUSB.Size = new System.Drawing.Size(384, 34);
            this.btnConnectUSB.TabIndex = 16;
            this.btnConnectUSB.Text = "⚡ Kết nối nhanh qua dây USB";
            this.btnConnectUSB.Click += new System.EventHandler(this.btnConnectUSB_Click);
            // 
            // btnPair
            // 
            this.btnPair.Location = new System.Drawing.Point(28, 376);
            this.btnPair.Name = "btnPair";
            this.btnPair.Size = new System.Drawing.Size(384, 34);
            this.btnPair.TabIndex = 22;
            this.btnPair.Text = "Ghép đôi ADB";
            this.btnPair.Click += new System.EventHandler(this.btnPair_Click);
            // 
            // historyPanel
            // 
            this.historyPanel.BackColor = System.Drawing.Color.White;
            this.historyPanel.Controls.Add(this.lstIPHistory);
            this.historyPanel.Controls.Add(this.lblHistoryHint);
            this.historyPanel.Controls.Add(this.lblHistory);
            this.historyPanel.Controls.Add(this.btnClearHistory);
            this.historyPanel.Location = new System.Drawing.Point(488, 130);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(272, 308);
            this.historyPanel.TabIndex = 16;
            // 
            // lstIPHistory
            // 
            this.lstIPHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lstIPHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstIPHistory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstIPHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lstIPHistory.FormattingEnabled = true;
            this.lstIPHistory.ItemHeight = 17;
            this.lstIPHistory.Location = new System.Drawing.Point(22, 76);
            this.lstIPHistory.Name = "lstIPHistory";
            this.lstIPHistory.Size = new System.Drawing.Size(228, 153);
            this.lstIPHistory.TabIndex = 6;
            this.lstIPHistory.Click += new System.EventHandler(this.lstIPHistory_Click);
            this.lstIPHistory.DoubleClick += new System.EventHandler(this.lstIPHistory_DoubleClick);
            // 
            // lblHistoryHint
            // 
            this.lblHistoryHint.AutoSize = true;
            this.lblHistoryHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHistoryHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(113)))), ((int)(((byte)(137)))));
            this.lblHistoryHint.Location = new System.Drawing.Point(22, 46);
            this.lblHistoryHint.Name = "lblHistoryHint";
            this.lblHistoryHint.Size = new System.Drawing.Size(166, 15);
            this.lblHistoryHint.TabIndex = 14;
            this.lblHistoryHint.Text = "Nhấp đúp để chọn thiết bị cũ";
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(28)))), ((int)(((byte)(45)))));
            this.lblHistory.Location = new System.Drawing.Point(20, 18);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(107, 25);
            this.lblHistory.TabIndex = 13;
            this.lblHistory.Text = "Thiết bị gần";
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(180, 18);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(70, 28);
            this.btnClearHistory.TabIndex = 10;
            this.btnClearHistory.Text = "Xóa";
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // logPanel
            // 
            this.logPanel.BackColor = System.Drawing.Color.White;
            this.logPanel.Controls.Add(this.txtOutput);
            this.logPanel.Controls.Add(this.lblStatusHint);
            this.logPanel.Controls.Add(this.lblStatus);
            this.logPanel.Location = new System.Drawing.Point(20, 590);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(740, 202);
            this.logPanel.TabIndex = 17;
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.Font = new System.Drawing.Font("Cascadia Mono", 9F);
            this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.txtOutput.Location = new System.Drawing.Point(22, 58);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(696, 124);
            this.txtOutput.TabIndex = 8;
            // 
            // lblStatusHint
            // 
            this.lblStatusHint.AutoSize = true;
            this.lblStatusHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(113)))), ((int)(((byte)(137)))));
            this.lblStatusHint.Location = new System.Drawing.Point(125, 27);
            this.lblStatusHint.Name = "lblStatusHint";
            this.lblStatusHint.Size = new System.Drawing.Size(301, 15);
            this.lblStatusHint.TabIndex = 15;
            this.lblStatusHint.Text = "Theo dõi tiến trình ADB, Scrcpy và thao tác gửi file tại đây";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(28)))), ((int)(((byte)(45)))));
            this.lblStatus.Location = new System.Drawing.Point(22, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(97, 25);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Nhật ký";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(784, 813);
            this.Controls.Add(this.logPanel);
            this.Controls.Add(this.historyPanel);
            this.Controls.Add(this.connectionPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowPhone Capture - Scrcpy Controller";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.connectionPanel.ResumeLayout(false);
            this.connectionPanel.PerformLayout();
            this.historyPanel.ResumeLayout(false);
            this.historyPanel.PerformLayout();
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
            this.ResumeLayout(false);

            StyleButton(this.btnConnect, System.Drawing.Color.FromArgb(73, 126, 255), System.Drawing.Color.White, 0);
            StyleButton(this.btnRefresh, System.Drawing.Color.FromArgb(239, 68, 68), System.Drawing.Color.White, 0);
            StyleButton(this.btnSetupTcpip, System.Drawing.Color.FromArgb(20, 184, 166), System.Drawing.Color.White, 0);
            StyleButton(this.btnCheckDevices, System.Drawing.Color.FromArgb(245, 158, 11), System.Drawing.Color.White, 0);
            StyleButton(this.btnPushFile, System.Drawing.Color.FromArgb(168, 85, 247), System.Drawing.Color.White, 0);
            StyleButton(this.btnConnectUSB, System.Drawing.Color.FromArgb(34, 197, 94), System.Drawing.Color.White, 0);
            StyleButton(this.btnPair, System.Drawing.Color.FromArgb(14, 165, 233), System.Drawing.Color.White, 0);
            StyleButton(this.btnClearHistory, System.Drawing.Color.FromArgb(226, 232, 240), System.Drawing.Color.FromArgb(51, 65, 85), 0);
        }

        private static void StyleButton(System.Windows.Forms.Button button, System.Drawing.Color backColor, System.Drawing.Color foreColor, int borderSize)
        {
            button.BackColor = backColor;
            button.FlatAppearance.BorderSize = borderSize;
            button.FlatAppearance.MouseDownBackColor = System.Windows.Forms.ControlPaint.Dark(backColor);
            button.FlatAppearance.MouseOverBackColor = System.Windows.Forms.ControlPaint.Light(backColor);
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            button.ForeColor = foreColor;
            button.UseVisualStyleBackColor = false;
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel connectionPanel;
        private System.Windows.Forms.Label lblConnectionHint;
        private System.Windows.Forms.TextBox txtDeviceIP;
        private System.Windows.Forms.Label lblDeviceIP;
        private System.Windows.Forms.TextBox txtPairEndpoint;
        private System.Windows.Forms.Label lblPairEndpoint;
        private System.Windows.Forms.TextBox txtPairCode;
        private System.Windows.Forms.Label lblPairCode;
        private System.Windows.Forms.CheckBox chkTurnOffScreen;
        private System.Windows.Forms.CheckBox chk720p;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSetupTcpip;
        private System.Windows.Forms.Button btnCheckDevices;
        private System.Windows.Forms.Button btnPushFile;
        private System.Windows.Forms.Button btnConnectUSB;
        private System.Windows.Forms.Button btnPair;
        private System.Windows.Forms.Panel historyPanel;
        private System.Windows.Forms.ListBox lstIPHistory;
        private System.Windows.Forms.Label lblHistoryHint;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Panel logPanel;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblStatusHint;
        private System.Windows.Forms.Label lblStatus;
    }
}
