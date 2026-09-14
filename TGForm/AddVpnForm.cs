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
using TGForm.Models;

namespace TGForm
{
    public partial class AddVpnForm : Form
    {
        public  AddVpnForm(Models.vpns model = null)
        {
            InitializeComponent();
            this.Icon = new Icon("icons/2j92t-xs66w-001.ico");
            this.StartPosition = FormStartPosition.CenterScreen;

            if (model != null)
            {
                this.txtHost.Text = model.host;
                this.txtPort.Text = model.port;
                this.txtUserName.Text = model.username;
                this.txtPassword.Text = model.password;
                this.txtGroup.Text = model.group;
                this.txtRemark.Text = model.remark;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var host = this.txtHost.Text.Trim();
            var port = this.txtPort.Text.Trim();
            var remark = this.txtRemark.Text.Trim();

            if (string.IsNullOrEmpty(host))
            {
                MessageBox.Show("请输入主机地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (string.IsNullOrEmpty(port))
            {
                MessageBox.Show("请输入端口！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (string.IsNullOrEmpty(remark))
            {
                MessageBox.Show("请输入备注！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            var username = this.txtUserName.Text.Trim();
            var password = this.txtPassword.Text.Trim();
            var group = this.txtGroup.Text.Trim();

            if (await DbHelper.Db.Queryable<vpns>().Where(it => it.host == host && it.port == port).AnyAsync())
            {
                MessageBox.Show("该代理已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }


            var res = await DbHelper.Db.Insertable(new vpns
            {
                host = host,
                port = port,
                username = username,
                password = password,
                group = group,
                remark = remark,
                create_time = Common.Tools.Instance.GetTimeStamp(),
                Status = true
            }).ExecuteCommandAsync();

            if (res > 0)
            {
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddVpnForm_Load(object sender, EventArgs e)
        {

        }
    }
}
