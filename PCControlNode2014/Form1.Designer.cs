namespace PCControlNode2014
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "3",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "4",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "5",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "6",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "7",
            "offline",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "8",
            "offline",
            ""}, -1);
            this.btnSendTest = new System.Windows.Forms.Button();
            this.tbReceiveDataTest = new System.Windows.Forms.TextBox();
            this.lvFrame = new System.Windows.Forms.ListView();
            this.srcIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.srcNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.desNO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CMD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnTestPlay = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.lvDevicesStatus = new System.Windows.Forms.ListView();
            this.device = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendRFID_4_5_1 = new System.Windows.Forms.Button();
            this.btnSendRFID_2_5_1 = new System.Windows.Forms.Button();
            this.btnSendRFID_3_3_2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSendTest
            // 
            this.btnSendTest.Location = new System.Drawing.Point(503, 25);
            this.btnSendTest.Name = "btnSendTest";
            this.btnSendTest.Size = new System.Drawing.Size(75, 23);
            this.btnSendTest.TabIndex = 0;
            this.btnSendTest.Text = "发送测试";
            this.btnSendTest.UseVisualStyleBackColor = true;
            this.btnSendTest.Click += new System.EventHandler(this.btnSendTest_Click);
            // 
            // tbReceiveDataTest
            // 
            this.tbReceiveDataTest.Location = new System.Drawing.Point(472, 78);
            this.tbReceiveDataTest.Multiline = true;
            this.tbReceiveDataTest.Name = "tbReceiveDataTest";
            this.tbReceiveDataTest.Size = new System.Drawing.Size(227, 109);
            this.tbReceiveDataTest.TabIndex = 2;
            // 
            // lvFrame
            // 
            this.lvFrame.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.srcIP,
            this.srcNO,
            this.desNO,
            this.CMD,
            this.detail});
            this.lvFrame.Location = new System.Drawing.Point(12, 12);
            this.lvFrame.Name = "lvFrame";
            this.lvFrame.Size = new System.Drawing.Size(371, 97);
            this.lvFrame.TabIndex = 3;
            this.lvFrame.UseCompatibleStateImageBehavior = false;
            this.lvFrame.View = System.Windows.Forms.View.Details;
            // 
            // srcIP
            // 
            this.srcIP.Text = "srcIP";
            this.srcIP.Width = 49;
            // 
            // srcNO
            // 
            this.srcNO.Text = "srcNO";
            this.srcNO.Width = 47;
            // 
            // desNO
            // 
            this.desNO.Text = "desNO";
            this.desNO.Width = 50;
            // 
            // CMD
            // 
            this.CMD.Text = "CMD";
            this.CMD.Width = 86;
            // 
            // detail
            // 
            this.detail.Text = "detail";
            // 
            // btnTestPlay
            // 
            this.btnTestPlay.Location = new System.Drawing.Point(584, 25);
            this.btnTestPlay.Name = "btnTestPlay";
            this.btnTestPlay.Size = new System.Drawing.Size(75, 23);
            this.btnTestPlay.TabIndex = 4;
            this.btnTestPlay.Text = "播放测试";
            this.btnTestPlay.UseVisualStyleBackColor = true;
            this.btnTestPlay.Click += new System.EventHandler(this.btnTestPlay_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(534, 204);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(75, 23);
            this.axWindowsMediaPlayer1.TabIndex = 5;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // lvDevicesStatus
            // 
            this.lvDevicesStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.device,
            this.status});
            this.lvDevicesStatus.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16});
            this.lvDevicesStatus.Location = new System.Drawing.Point(12, 115);
            this.lvDevicesStatus.Name = "lvDevicesStatus";
            this.lvDevicesStatus.Size = new System.Drawing.Size(371, 192);
            this.lvDevicesStatus.TabIndex = 6;
            this.lvDevicesStatus.UseCompatibleStateImageBehavior = false;
            this.lvDevicesStatus.View = System.Windows.Forms.View.Details;
            // 
            // device
            // 
            this.device.Text = "device";
            this.device.Width = 59;
            // 
            // status
            // 
            this.status.Text = "status";
            this.status.Width = 122;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "陀螺积分";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBox1.Location = new System.Drawing.Point(6, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(54, 20);
            this.comboBox1.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "停止积分";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(472, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 55);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnSendRFID_4_5_1
            // 
            this.btnSendRFID_4_5_1.Location = new System.Drawing.Point(13, 346);
            this.btnSendRFID_4_5_1.Name = "btnSendRFID_4_5_1";
            this.btnSendRFID_4_5_1.Size = new System.Drawing.Size(351, 23);
            this.btnSendRFID_4_5_1.TabIndex = 11;
            this.btnSendRFID_4_5_1.Text = "防止 车4 上XX桥";
            this.btnSendRFID_4_5_1.UseVisualStyleBackColor = true;
            this.btnSendRFID_4_5_1.Click += new System.EventHandler(this.btnSendRFID_4_5_1_Click);
            // 
            // btnSendRFID_2_5_1
            // 
            this.btnSendRFID_2_5_1.Location = new System.Drawing.Point(12, 375);
            this.btnSendRFID_2_5_1.Name = "btnSendRFID_2_5_1";
            this.btnSendRFID_2_5_1.Size = new System.Drawing.Size(351, 23);
            this.btnSendRFID_2_5_1.TabIndex = 12;
            this.btnSendRFID_2_5_1.Text = "防止 车2 上XX桥";
            this.btnSendRFID_2_5_1.UseVisualStyleBackColor = true;
            this.btnSendRFID_2_5_1.Click += new System.EventHandler(this.btnSendRFID_2_5_1_Click);
            // 
            // btnSendRFID_3_3_2
            // 
            this.btnSendRFID_3_3_2.Location = new System.Drawing.Point(12, 404);
            this.btnSendRFID_3_3_2.Name = "btnSendRFID_3_3_2";
            this.btnSendRFID_3_3_2.Size = new System.Drawing.Size(351, 23);
            this.btnSendRFID_3_3_2.TabIndex = 13;
            this.btnSendRFID_3_3_2.Text = "车3 拉起吊桥";
            this.btnSendRFID_3_3_2.UseVisualStyleBackColor = true;
            this.btnSendRFID_3_3_2.Click += new System.EventHandler(this.btnSendRFID_3_3_2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 522);
            this.Controls.Add(this.btnSendRFID_3_3_2);
            this.Controls.Add(this.btnSendRFID_2_5_1);
            this.Controls.Add(this.btnSendRFID_4_5_1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvDevicesStatus);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.btnTestPlay);
            this.Controls.Add(this.lvFrame);
            this.Controls.Add(this.tbReceiveDataTest);
            this.Controls.Add(this.btnSendTest);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendTest;
        private System.Windows.Forms.TextBox tbReceiveDataTest;
        private System.Windows.Forms.ListView lvFrame;
        private System.Windows.Forms.ColumnHeader srcIP;
        private System.Windows.Forms.ColumnHeader srcNO;
        private System.Windows.Forms.ColumnHeader desNO;
        private System.Windows.Forms.ColumnHeader CMD;
        private System.Windows.Forms.ColumnHeader detail;
        private System.Windows.Forms.Button btnTestPlay;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ListView lvDevicesStatus;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader device;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSendRFID_4_5_1;
        private System.Windows.Forms.Button btnSendRFID_2_5_1;
        private System.Windows.Forms.Button btnSendRFID_3_3_2;
    }
}

