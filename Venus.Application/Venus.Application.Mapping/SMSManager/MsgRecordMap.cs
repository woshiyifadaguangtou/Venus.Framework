using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;

namespace Venus.Application.Mapping
{
    public class MsgRecordMap:BaseDomainMapping<MsgRecordEntity>
    {
        protected override void init()
        {
            this.ToTable("MsgRecord");
            this.HasKey(e => e.ID);
        }
    }
}
