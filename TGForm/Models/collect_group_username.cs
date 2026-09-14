using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    [SugarTable("collect_group_username")] // 指定数据库表名
    public class collect_group_username
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 0:未采集 1开始采集 2采集完成 -1报错采集失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 采集用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 采集时间
        /// </summary>
        public long collect_time { get; set; }
        /// <summary>
        /// 群组总人数
        /// </summary>
        public long group_total_count { get; set; }
        /// <summary>
        /// 群Id
        /// </summary>
        public long group_id { get; set; }
        /// <summary>
        /// 采集使用账号
        /// </summary>
        public string collect_phone { get; set; }

    }
}
