namespace TGForm
{
    partial class AddVpnForm
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
            txtHost = new TextBox();
            label1 = new Label();
            label3 = new Label();
            txtUserName = new TextBox();
            label2 = new Label();
            txtPort = new TextBox();
            label4 = new Label();
            txtPassword = new TextBox();
            txtRemark = new TextBox();
            label5 = new Label();
            txtGroup = new TextBox();
            label6 = new Label();
            btnSave = new Button();
            btnCannel = new Button();
            SuspendLayout();
            // 
            // txtHost
            // 
            txtHost.Location = new Point(26, 67);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(215, 23);
            txtHost.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 32);
            label1.Name = "label1";
            label1.Size = new Size(138, 17);
            label1.TabIndex = 1;
            label1.Text = "主机(IPv4 / 域名 / IPv6)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 118);
            label3.Name = "label3";
            label3.Size = new Size(100, 17);
            label3.TabIndex = 4;
            label3.Text = "认证用户名(可选)";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(26, 153);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(215, 23);
            txtUserName.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(259, 32);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 7;
            label2.Text = "端口";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(259, 67);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(215, 23);
            txtPort.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(259, 118);
            label4.Name = "label4";
            label4.Size = new Size(88, 17);
            label4.TabIndex = 9;
            label4.Text = "认证密码(可选)";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(259, 153);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(215, 23);
            txtPassword.TabIndex = 8;
            // 
            // txtRemark
            // 
            txtRemark.Location = new Point(26, 235);
            txtRemark.Name = "txtRemark";
            txtRemark.Size = new Size(215, 23);
            txtRemark.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 200);
            label5.Name = "label5";
            label5.Size = new Size(32, 17);
            label5.TabIndex = 10;
            label5.Text = "备注";
            // 
            // txtGroup
            // 
            txtGroup.Location = new Point(259, 235);
            txtGroup.Name = "txtGroup";
            txtGroup.Size = new Size(215, 23);
            txtGroup.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(259, 200);
            label6.Name = "label6";
            label6.Size = new Size(64, 17);
            label6.TabIndex = 12;
            label6.Text = "标签(可选)";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(26, 284);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(215, 37);
            btnSave.TabIndex = 14;
            btnSave.Text = "保 存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCannel
            // 
            btnCannel.Location = new Point(259, 284);
            btnCannel.Name = "btnCannel";
            btnCannel.Size = new Size(215, 37);
            btnCannel.TabIndex = 15;
            btnCannel.Text = "取 消";
            btnCannel.UseVisualStyleBackColor = true;
            btnCannel.Click += btnCannel_Click;
            // 
            // AddVpnForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 352);
            Controls.Add(btnCannel);
            Controls.Add(btnSave);
            Controls.Add(txtGroup);
            Controls.Add(label6);
            Controls.Add(txtRemark);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(txtPort);
            Controls.Add(txtUserName);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(txtHost);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddVpnForm";
            Text = "添加代理";
            Load += AddVpnForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtHost;
        private Label label1;
        private Label label3;
        private TextBox txtUserName;
        private Label label2;
        private TextBox txtPort;
        private Label label4;
        private TextBox txtPassword;
        private TextBox txtRemark;
        private Label label5;
        private TextBox txtGroup;
        private Label label6;
        private Button btnSave;
        private Button btnCannel;
    }
}