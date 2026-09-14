using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{

    [SugarTable("account_check_log")] // 指定数据库表名
    public class account_check_log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }
        public string account { get; set; }
        public long time { get; set; }
        public string msg { get; set; }
        /// <summary>
        /// 1成功 -1封号 -2频繁 0未知
        /// </summary>
        public int status { get; set; }
    }
}
