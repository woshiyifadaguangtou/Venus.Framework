using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Application.Entity.CBManage
{
    public class CBTaskEntity:BaseEntity
    {
        public string  TaskId { get; set; }
        
        public string xh { get; set; }
        
        public string fdate { get; set; }
       
        public DateTime cbrq { get; set; }

        public string cbydm { get; set; }
        /// <summary>
        /// 0- 未完成 1-已完成 2-已同步
        /// </summary>
        public int state { get; set; }

        public int detail_count { get; set; }
    }
}
