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
            this.btnSendTest = new System.Windows.Forms.Button();
            this.tbReceiveDataTest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSendTest
            // 
            this.btnSendTest.Location = new System.Drawing.Point(45, 22);
            this.btnSendTest.Name = "btnSendTest";
            this.btnSendTest.Size = new System.Drawing.Size(75, 23);
            this.btnSendTest.TabIndex = 0;
            this.btnSendTest.Text = "发送测试";
            this.btnSendTest.UseVisualStyleBackColor = true;
            this.btnSendTest.Click += new System.EventHandler(this.btnSendTest_Click);
            // 
            // tbReceiveDataTest
            // 
            this.tbReceiveDataTest.Location = new System.Drawing.Point(45, 81);
            this.tbReceiveDataTest.Multiline = true;
            this.tbReceiveDataTest.Name = "tbReceiveDataTest";
            this.tbReceiveDataTest.Size = new System.Drawing.Size(227, 109);
            this.tbReceiveDataTest.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tbReceiveDataTest);
            this.Controls.Add(this.btnSendTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendTest;
        private System.Windows.Forms.TextBox tbReceiveDataTest;
    }
}

