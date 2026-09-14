using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{

    [SugarTable("webbrowser_collect_username")]
    public class webbrowser_collect_username
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }

        public string username { get; set; }

        public int account_id { get; set; }

        public string msg { get; set; }
        public long create_time { get; set; }
        public int collect_time { get; set; }

        public long group_total_count { get; set; }
        /// <summary>
        /// 0待处理 1采集中 2采集完成 -1采集失败
        /// </summary>
        public int status { get; set; }
        public int collect_success_count { get; set; }


    }
}
