using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    [SugarTable("settings")] // 指定数据库表名
    public class settings
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 主键自增
        public int id { get; set; }
        /// <summary>
        /// 协议 api_id
        /// </summary>
        public string api_id { get; set; }
        /// <summary>
        /// 协议 api_hash
        /// </summary>
        public string api_hash { get; set; }
        /// <summary>
        /// 协议账号使用vpn
        /// </summary>
        public int default_vpn_id { get; set; }
        /// <summary>
        /// 默认协议账号列表路劲
        /// </summary>
        public string default_account_path { get; set; }
        /// <summary>
        /// web群发 群发间隔最小时间
        /// </summary>
        public int web_send_min { get; set; }
        /// <summary>
        /// web群发 群发间隔最大时间
        /// </summary>
        public int web_send_max { get; set; }
        /// <summary>
        /// web群发 单账号最大群发数
        /// </summary>
        public int web_send_max_count { get; set; }
        /// <summary>
        /// web群发 群发内容 | 分割
        /// </summary>
        public string web_send_context { get; set; }
        /// <summary>
        /// web群控 最后发送账号
        /// </summary>
        public int web_last_send_count { get; set; }

        /// <summary>
        /// 协议群发 群发间隔最大时间
        /// </summary>

        public int session_send_max { get; set; }
        /// <summary>
        /// 协议群发 群发间隔最小时间
        /// </summary>
        public int session_send_min { get; set; }
        /// <summary>
        /// 协议群发 单账号最大群发数
        /// </summary>
        public int session_send_count { get; set; }
        /// <summary>
        /// 协议群发 群发内容 | 分割
        /// </summary>
        public string session_send_context { get; set; }

        /// <summary>
        /// 协议采集 是否过滤昵称
        /// </summary>
        public bool session_collect_nick_filter_enabled { get; set; }
        /// <summary>
        /// 协议采集 昵称过滤内容
        /// </summary>
        public string session_collect_nick_filter_context { get; set; }
        /// <summary>
        /// 协议采集 是否开启个人简介过滤
        /// </summary>

        public bool session_collect_info_filter_enabled { get; set; }
        /// <summary>
        /// 协议采集 简介过滤内容
        /// </summary>
        public string session_collect_info_filter_context { get; set; }




        /// <summary>
        /// web采集 是否过滤昵称
        /// </summary>
        public bool web_collect_nick_filter_enabled { get; set; }
        /// <summary>
        /// web采集 昵称过滤内容
        /// </summary>
        public string web_collect_nick_filter_context { get; set; }
        /// <summary>
        /// web采集 是否开启个人简介过滤
        /// </summary>

        public bool web_collect_info_filter_enabled { get; set; }
        /// <summary>
        /// web采集 简介过滤内容
        /// </summary>
        public string web_collect_info_filter_context { get; set; }
        /// <summary>
        /// 是否下载头像
        /// </summary>
        public bool session_collect_avatar_enabled { get; set; }
        /// <summary>
        /// 是否过滤管理员
        /// </summary>
        public bool session_collect_filter_manager_enabled { get; set; }
        /// <summary>
        /// 是否过滤bot
        /// </summary>
        public bool session_collect_filter_bot_enabled { get; set; }
        public string activation_code { get; set; }

    }
}
