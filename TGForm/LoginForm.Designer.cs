namespace TGForm
{
    partial class LoginForm
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
            label1 = new Label();
            txtLoginCode = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 41);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "激活码";
            // 
            // txtLoginCode
            // 
            txtLoginCode.Location = new Point(88, 38);
            txtLoginCode.Name = "txtLoginCode";
            txtLoginCode.Size = new Size(307, 23);
            txtLoginCode.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(88, 82);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(307, 35);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "激 活 程 序";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 144);
            Controls.Add(btnLogin);
            Controls.Add(txtLoginCode);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "LoginForm";
            Text = "激活Telegram工具";
            Load += LoginForm_Load;
            
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtLoginCode;
        private Button btnLogin;
    }
}