using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGForm.Models
{
    public class CollectGroupMemberResponse
    {
        public int IncrementalCount { get; set; }//净新增
        public int ExistingMemberCount { get; set; }//重复人数
        public decimal SuccessRate { get; set; }//成功率
        public int MemberTotal { get; set; }//总成员数
    }
}
