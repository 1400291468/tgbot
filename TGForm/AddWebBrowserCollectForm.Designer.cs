namespace TGForm
{
    partial class AddWebBrowserCollectForm
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
            btnCannel = new Button();
            btnSave = new Button();
            label2 = new Label();
            txtGroupUserName = new TextBox();
            label1 = new Label();
            cmbWebBrowserAccounts = new ComboBox();
            SuspendLayout();
            // 
            // btnCannel
            // 
            btnCannel.Location = new Point(269, 131);
            btnCannel.Name = "btnCannel";
            btnCannel.Size = new Size(215, 37);
            btnCannel.TabIndex = 33;
            btnCannel.Text = "取 消";
            btnCannel.UseVisualStyleBackColor = true;
            btnCannel.Click += btnCannel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(36, 131);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(215, 37);
            btnSave.TabIndex = 32;
            btnSave.Text = "确 认 保 存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(269, 35);
            label2.Name = "label2";
            label2.Size = new Size(122, 17);
            label2.TabIndex = 28;
            label2.Text = "群组URL/UserName"; 
            // 
            // txtGroupUserName
            // 
            txtGroupUserName.Location = new Point(269, 70);
            txtGroupUserName.Name = "txtGroupUserName";
            txtGroupUserName.Size = new Size(215, 23);
            txtGroupUserName.TabIndex = 27;
            txtGroupUserName.Text = "https://t.me/";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 35);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 26;
            label1.Text = "采集所用账号";
            // 
            // cmbWebBrowserAccounts
            // 
            cmbWebBrowserAccounts.FormattingEnabled = true;
            cmbWebBrowserAccounts.Location = new Point(36, 70);
            cmbWebBrowserAccounts.Name = "cmbWebBrowserAccounts";
            cmbWebBrowserAccounts.Size = new Size(215, 25);
            cmbWebBrowserAccounts.TabIndex = 34;
            // 
            // AddWebBrowserCollectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(536, 198);
            Controls.Add(cmbWebBrowserAccounts);
            Controls.Add(btnCannel);
            Controls.Add(btnSave);
            Controls.Add(label2);
            Controls.Add(txtGroupUserName);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AddWebBrowserCollectForm";
            Text = "新增浏览器采集任务";
            Load += AddWebBrowserCollectForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label label6;
        private Label label5;
        private ComboBox cboVpnlist;
        private Button btnSelectPath;
        private Label label4;
        private TextBox txtPath;
        private Button btnCannel;
        private Button btnSave;
        private Button btnSendCode;
        private Label label3;
        private TextBox txtCode;
        private Label label2;
        private TextBox txtGroupUserName;
        private Label label1;
        private ComboBox cmbWebBrowserAccounts;
    }
}