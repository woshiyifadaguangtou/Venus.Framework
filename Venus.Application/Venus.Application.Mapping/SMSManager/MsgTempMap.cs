using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;
namespace Venus.Application.Mapping
{
    public class MsgTempMap:BaseDomainMapping<MsgTempEntity>
    {
        protected override void init()
        {
            this.ToTable("MsgTemp");
            this.HasKey(e => e.TempId);
        }
    }
}
