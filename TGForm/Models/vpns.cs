using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    [SugarTable("vpns")] // 指定数据库表名
    public class vpns
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }
        public long create_time { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? remark { get; set; }
        public string? group { get; set; }
        public bool Status { get; set; }
    }
}
