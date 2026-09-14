using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    [SugarTable("webbrowser_accounts")] // 指定数据库表名
    public class webbrowser_accounts
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }

        public string phonenumber { get; set; }
        public long login_time { get; set; }
        public string localstorage { get; set; }
        public int last_send_date { get; set; }
        public long last_send_time { get; set; }
        public int today_send_count { get; set; }

    }
}
