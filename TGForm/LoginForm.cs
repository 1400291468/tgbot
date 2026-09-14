using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TGForm.Common;

namespace TGForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this.Icon = new Icon("icons/2j92t-xs66w-001.ico");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;

                btnLogin.Text = "验证中...";

                var text = txtLoginCode.Text.Trim();
                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("请填写激活码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = await Common.Tools.Instance.activaionCode(text);
                if (result.Status)
                {
                    MessageBox.Show("激活成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();


                }
                else
                {
                    MessageBox.Show($"激活失败,原因:{result.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtLoginCode.SelectAll();
                    txtLoginCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"激活失败,原因:{ex.ToString()}", "激活报错", MessageBoxButtons.OK);
            }

            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "激 活 程 序";
            }

        }




    }
}
