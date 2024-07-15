namespace ServerForm
{
    partial class ServerForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ResultTxt = new System.Windows.Forms.TextBox();
            this.FileNameTxt = new System.Windows.Forms.TextBox();
            this.PortTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.IpTxt = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ResultTxt);
            this.groupBox2.Controls.Add(this.FileNameTxt);
            this.groupBox2.Controls.Add(this.PortTxt);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.IpTxt);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 446);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // ResultTxt
            // 
            this.ResultTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResultTxt.Enabled = false;
            this.ResultTxt.Font = new System.Drawing.Font("Consolas", 12F);
            this.ResultTxt.Location = new System.Drawing.Point(25, 247);
            this.ResultTxt.Multiline = true;
            this.ResultTxt.Name = "ResultTxt";
            this.ResultTxt.Size = new System.Drawing.Size(383, 140);
            this.ResultTxt.TabIndex = 19;
            // 
            // FileNameTxt
            // 
            this.FileNameTxt.Enabled = false;
            this.FileNameTxt.Font = new System.Drawing.Font("Consolas", 12F);
            this.FileNameTxt.Location = new System.Drawing.Point(195, 136);
            this.FileNameTxt.Name = "FileNameTxt";
            this.FileNameTxt.Size = new System.Drawing.Size(222, 31);
            this.FileNameTxt.TabIndex = 18;
            // 
            // PortTxt
            // 
            this.PortTxt.Enabled = false;
            this.PortTxt.Font = new System.Drawing.Font("Consolas", 12F);
            this.PortTxt.Location = new System.Drawing.Point(195, 96);
            this.PortTxt.Name = "PortTxt";
            this.PortTxt.Size = new System.Drawing.Size(222, 31);
            this.PortTxt.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 12F);
            this.label6.Location = new System.Drawing.Point(33, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 23);
            this.label6.TabIndex = 16;
            this.label6.Text = "File Name：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 12F);
            this.label7.Location = new System.Drawing.Point(33, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Client Port：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 12F);
            this.label8.Location = new System.Drawing.Point(33, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 23);
            this.label8.TabIndex = 14;
            this.label8.Text = "Client Ip：";
            // 
            // IpTxt
            // 
            this.IpTxt.Enabled = false;
            this.IpTxt.Font = new System.Drawing.Font("Consolas", 12F);
            this.IpTxt.Location = new System.Drawing.Point(195, 56);
            this.IpTxt.Name = "IpTxt";
            this.IpTxt.Size = new System.Drawing.Size(222, 31);
            this.IpTxt.TabIndex = 13;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 468);
            this.Controls.Add(this.groupBox2);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ResultTxt;
        private System.Windows.Forms.TextBox FileNameTxt;
        private System.Windows.Forms.TextBox PortTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox IpTxt;
    }
}

