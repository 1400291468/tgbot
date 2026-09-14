using SqlSugar;

namespace TGForm.Models
{
    [SugarTable("collect_group_member_username")]
    public class collect_group_member_username
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int collect_task_id { get; set; }

        [SugarColumn(IsNullable = false)]
        public long telegram_user_id { get; set; }

        [SugarColumn(Length = 128, IsNullable = false)]
        public string username { get; set; } = string.Empty;

        public long create_time { get; set; }
        /// <summary>
        /// -100机器人bot 
        /// -99管理员ID
        /// -3触发简介非法规则 
        /// -2 触发 昵称非法规则 
        /// -1发送失败 
        /// 0待发送 
        /// 1发送成功 
        /// </summary>
        public int status { get; set; }
        public string msg { get; set; }
        public long send_time { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "BLOB")]
        public byte[]? head_image { get; set; }
        /// <summary>
        /// 1web 2协议
        /// </summary>
        public int SourceType { get; set; }
    }
}