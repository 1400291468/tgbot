namespace TGForm
{
    partial class AddAccountForm
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
            label2 = new Label();
            txtPassword = new TextBox();
            label1 = new Label();
            txtAccount = new TextBox();
            label3 = new Label();
            txtCode = new TextBox();
            btnSendCode = new Button();
            btnCannel = new Button();
            btnSave = new Button();
            btnSelectPath = new Button();
            label4 = new Label();
            txtPath = new TextBox();
            cboVpnlist = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 31);
            label2.Name = "label2";
            label2.Size = new Size(160, 17);
            label2.TabIndex = 11;
            label2.Text = "二次验证密码(打开时才填写)";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(265, 66);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(215, 23);
            txtPassword.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 31);
            label1.Name = "label1";
            label1.Size = new Size(88, 17);
            label1.TabIndex = 9;
            label1.Text = "账号(需带区号)";
            // 
            // txtAccount
            // 
            txtAccount.Location = new Point(32, 66);
            txtAccount.Name = "txtAccount";
            txtAccount.Size = new Size(215, 23);
            txtAccount.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 111);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 13;
            label3.Text = "验证码";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(32, 146);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(117, 23);
            txtCode.TabIndex = 12;
            // 
            // btnSendCode
            // 
            btnSendCode.Location = new Point(155, 146);
            btnSendCode.Name = "btnSendCode";
            btnSendCode.Size = new Size(92, 23);
            btnSendCode.TabIndex = 14;
            btnSendCode.Text = "发送验证码";
            btnSendCode.UseVisualStyleBackColor = true;
            btnSendCode.Click += btnSendCode_Click;
            // 
            // btnCannel
            // 
            btnCannel.Location = new Point(265, 285);
            btnCannel.Name = "btnCannel";
            btnCannel.Size = new Size(215, 37);
            btnCannel.TabIndex = 18;
            btnCannel.Text = "取 消";
            btnCannel.UseVisualStyleBackColor = true;
            btnCannel.Click += btnCannel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(32, 285);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(215, 37);
            btnSave.TabIndex = 17;
            btnSave.Text = "验 证 验 证 码";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnSelectPath
            // 
            btnSelectPath.Location = new Point(406, 146);
            btnSelectPath.Name = "btnSelectPath";
            btnSelectPath.Size = new Size(74, 23);
            btnSelectPath.TabIndex = 21;
            btnSelectPath.Text = "选择路劲";
            btnSelectPath.UseVisualStyleBackColor = true;
            btnSelectPath.Click += btnSelectPath_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(265, 111);
            label4.Name = "label4";
            label4.Size = new Size(244, 17);
            label4.TabIndex = 20;
            label4.Text = "保存路劲(选填 默认值当前程序Session目录)";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(265, 146);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(135, 23);
            txtPath.TabIndex = 19;
            // 
            // cboVpnlist
            // 
            cboVpnlist.FormattingEnabled = true;
            cboVpnlist.Location = new Point(32, 231);
            cboVpnlist.Name = "cboVpnlist";
            cboVpnlist.Size = new Size(215, 25);
            cboVpnlist.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(32, 190);
            label5.Name = "label5";
            label5.Size = new Size(188, 17);
            label5.TabIndex = 23;
            label5.Text = "代理选择(选填 默认使用本地网络)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(77, 111);
            label6.Name = "label6";
            label6.Size = new Size(12, 17);
            label6.TabIndex = 24;
            label6.Text = "\u007f";
            // 
            // AddAccountForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 364);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(cboVpnlist);
            Controls.Add(btnSelectPath);
            Controls.Add(label4);
            Controls.Add(txtPath);
            Controls.Add(btnCannel);
            Controls.Add(btnSave);
            Controls.Add(btnSendCode);
            Controls.Add(label3);
            Controls.Add(txtCode);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(label1);
            Controls.Add(txtAccount);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddAccountForm";
            Text = "AddAccountForm";
            FormClosed += AddAccountForm_FormClosed;
            Load += AddAccountForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox txtPassword;
        private Label label1;
        private TextBox txtAccount;
        private Label label3;
        private TextBox txtCode;
        private Button btnSendCode;
        private Button btnCannel;
        private Button btnSave;
        private Button btnSelectPath;
        private Label label4;
        private TextBox txtPath;
        private ComboBox cboVpnlist;
        private Label label5;
        private Label label6;
    }
}