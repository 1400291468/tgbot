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
    public partial class AddWebBrowserCollectForm : Form
    {
        private MainForm _parentForm;
        public AddWebBrowserCollectForm(MainForm parentForm)
        {
            InitializeComponent();
            this.Icon = new Icon("icons/2j92t-xs66w-001.ico");
            this.StartPosition = FormStartPosition.CenterScreen;
            _parentForm = parentForm;
        }

        private async void AddWebBrowserCollectForm_Load(object sender, EventArgs e)
        {
            try
            {
                var list = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().Where(it => it.phonenumber.Length > 0).ToListAsync();

                cmbWebBrowserAccounts.DropDownStyle = ComboBoxStyle.DropDownList;
                foreach (var item in list)
                {
                    cmbWebBrowserAccounts.Items.Add(item.phonenumber);
                }
                if (list.Count > 0)
                {
                    cmbWebBrowserAccounts.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show($"请先在浏览器添加账号", "错误");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化异常{ex.ToString()}", "错误");
            }

        }


        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var account = await DbHelper.Db.Queryable<Models.webbrowser_accounts>().Where(it => it.phonenumber == cmbWebBrowserAccounts.SelectedItem.ToString()).FirstAsync();

            if (account == null)
            {
                MessageBox.Show($"所选账号不存在,请确认选择账号,或联系管理员", "错误");
                return;
            }

            var username = txtGroupUserName.Text.Trim();
            username = (username.Replace("https://t.me/", ""));
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("请输入群组用户名", "提示");
                return;
            }

            var collect = new Models.webbrowser_collect_username()
            {
                account_id = account.id,
                collect_time = 0,
                create_time = Common.Tools.Instance.GetTimeStamp(),
                group_total_count = 0,
                msg = "",
                status = 0,
                username = username,
                collect_success_count = 0
            };

            var res = await DbHelper.Db.Insertable(collect).ExecuteCommandAsync();

            if (res > 0)
            {
                MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _parentForm.LoadWebCollectGroup();
            }
            else
            {
                MessageBox.Show("添加失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     
    }
}
