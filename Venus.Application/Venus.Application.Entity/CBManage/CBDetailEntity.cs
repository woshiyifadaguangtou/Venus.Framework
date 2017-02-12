using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Application.Entity.CBManage
{
    public class CBDetailEntity:BaseEntity
    {
        public int TaskId { get; set; }

        public string Id { get; set; }
        public string xh { get; set; }
        public string hh { get; set; }
        public string hm { get; set; }

        public string bh { get; set; }

        public string dh { get; set; }

        public string dz { get; set; }

        public int byds { get; set; }

        public int bcsl { get; set; }

        public int scds { get; set; }

        public int bkdm { get; set; }

        public decimal zje { get; set; }

        /// <summary>
        /// 累欠
        /// </summary>
        public decimal lq { get; set; }

        public decimal dj { get; set; }

        public decimal sf { get; set;}

        public DateTime cbrq { get; set; }

        public string bs { get; set; }

        public int hbsl { get; set; }

        public int tbsl { get; set; }

        public int jmsl { get; set; }

        public int fsl { get; set; }

        public string skfsname { get; set; }
        
        public decimal pwf { get; set; }

        public decimal ljf { get; set;}

        public decimal jyf { get; set; }

        public decimal glf { get; set; }

        public decimal zkf { get; set; }

        public int jtsl1 {get;set;}

        public int jtsl2 { get; set; }
        public int jtsl3 { get; set; }

        public decimal jtsf1 { get; set; }

        public decimal jtsf2 { get; set; }
        public decimal jtsf3 { get; set; }

        public string bz { get; set; }
    }
}
