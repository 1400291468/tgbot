using Starksoft.Net.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TGForm.Common;
using TGForm.Models;
using WTelegram;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TGForm
{
    public partial class AddAccountForm : Form
    {

        private static Models.settings settings = null;
        private WTelegram.Client _client;

        private TaskCompletionSource<string> _verificationCodeTcs;  // 用于等待验证码
        private List<Models.vpns> vList;

        public AddAccountForm()
        {
            InitializeComponent();
            this.Icon = new Icon("icons/2j92t-xs66w-001.ico");

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void btnSendCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSendCode.Enabled = false;
                if (string.IsNullOrEmpty(txtAccount.Text.Trim()))
                {
                    MessageBox.Show("请输入手机号！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (settings == null)
                {
                    MessageBox.Show("请先设置TG API ID和API HASH", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }





                var phoneNumber = txtAccount.Text.Trim();
                var password = txtPassword.Text;
                var sessionDirectory = txtPath.Text.Trim();

                Logger.Log("正在初始化 Client");
                _client = new WTelegram.Client(what => Config(what, phoneNumber, password, sessionDirectory));


                var vpn = vList.Where(it => it.host + "" + ":" + it.port == cboVpnlist.SelectedItem.ToString()).FirstOrDefault();

                if (vpn != null)
                {
                    _client.TcpHandler = async (address, port) =>
                    {
                        var proxy = new Socks5ProxyClient(vpn.host, Convert.ToInt32(vpn.port));
                        return proxy.CreateConnection(address, port);
                    };
                }

                var user = await Task.Run(() => _client.LoginUserIfNeeded());


                if (user != null)
                {
                    MessageBox.Show($"登录成功！\n用户ID: {user.id}\n用户名: {user.username ?? "无"}",
                        "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.Log($"登录成功！\n用户ID: {user.id}\n用户名: {user.username ?? "无"}");


                    DialogResult = DialogResult.OK;
                }
                _client?.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Log($"发送验证码时发生错误：{ex.Message}", LogLevel.Error);

            }
            finally
            {
                _client?.Dispose();
                this.btnSendCode.Enabled = true;
            }

        }

        private string WaitForVerificationCode()
        {
            _verificationCodeTcs = new TaskCompletionSource<string>(
                TaskCreationOptions.RunContinuationsAsynchronously);

            BeginInvoke((MethodInvoker)delegate
            {
                txtCode.Clear();
                txtCode.Focus();
            });

            return _verificationCodeTcs.Task.GetAwaiter().GetResult();
        }


        string Config(string what, string phoneNumber, string password, string sessionDirectory)
        {
            Logger.Log(what, LogLevel.Info);
            switch (what)
            {
                case "api_id": return settings.api_id;
                case "api_hash": return settings.api_hash;
                case "phone_number":
                    {
                        Logger.Log("📱 准备发送验证码...", LogLevel.Info);
                        return phoneNumber;
                    }
                case "verification_code": return WaitForVerificationCode();
                case "password": return password;     // if user has enabled 2FA
                case "session_pathname":
                    {
                        var path = string.IsNullOrEmpty(sessionDirectory)
                            ? Path.Combine(Application.StartupPath, "session", $"{phoneNumber}.session")
                            : Path.Combine(sessionDirectory, "session", $"{phoneNumber}.session");
                        return path;
                    }  // to save/load session
                default: return null;                  // let WTelegramClient decide the default config
            }
        }


        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            // 创建文件夹选择对话框
            using (var folderDialog = new FolderBrowserDialog())
            {
                // 设置对话框标题
                folderDialog.Description = "请选择文件夹";

                // 设置起始目录（默认桌面）
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                // 显示新文件夹按钮（允许用户创建新文件夹）
                folderDialog.ShowNewFolderButton = true;

                // 显示对话框
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取选中的文件夹路径
                    string selectedPath = folderDialog.SelectedPath;

                    // 显示路径
                    txtPath.Text = selectedPath;

                }
            }
        }

        private async void AddAccountForm_Load(object sender, EventArgs e)
        {
            vList = await DbHelper.Db.Queryable<Models.vpns>().Where(it => it.Status).ToListAsync();

            var dom = cboVpnlist;
            dom.DropDownStyle = ComboBoxStyle.DropDownList;
            dom.Items.Add("本机");
            foreach (var item in vList)
            {
                dom.Items.Add(item.host + ":" + item.port);
            }



            settings = await DbHelper.Db.Queryable<Models.settings>().FirstAsync();

            if (settings == null)
            {
                MessageBox.Show("请先设置TG API ID和API HASH", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (settings.default_vpn_id != 0)
            {
                var index = vList.FindIndex(it => it.id == settings.default_vpn_id);

                dom.SelectedIndex = index + 1;
            }
            else
            {
                dom.SelectedIndex = 0;
            }
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCode.Text.Trim();

                if (string.IsNullOrEmpty(code))
                {
                    MessageBox.Show("请输入验证码！");
                    return;
                }

                _verificationCodeTcs?.SetResult(code);
            }
            catch (Exception)
            {

            }




        }

        private void AddAccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 释放资源
            _client?.Dispose();
        }
    }
}
