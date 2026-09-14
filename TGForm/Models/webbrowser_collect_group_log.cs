using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    [SugarTable("webbrowser_collect_group_log")] // 指定数据库表名
    public class webbrowser_collect_group_log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }

        public string peer_id { get; set; }

    }
}
