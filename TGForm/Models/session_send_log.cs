using Microsoft.VisualBasic.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    [SugarTable("session_send_log")] // 指定数据库表名
    public class session_send_log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }
        /// <summary>
        /// 发送日期
        /// </summary>
        public int send_date { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public long send_time { get; set; }
        /// <summary>
        /// 发送类型 1web 2session
        /// </summary>
        public int send_type { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string send_message { get; set; }
        /// <summary>
        /// 1成功 -1失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 发送到 username
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 消息 给用户看的
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 发送者手机号
        /// </summary>
        public string send_phone { get; set; }
    }
}
