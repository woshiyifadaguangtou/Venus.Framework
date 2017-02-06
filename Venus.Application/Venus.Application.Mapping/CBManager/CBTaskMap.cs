using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity.CBManager;

namespace Venus.Application.Mapping.CBManager
{
    public class CBTaskMap : BaseDomainMapping<CBTaskEntity>
    {
        protected override void init()
        {
            this.ToTable("CBTask");
            this.HasKey(e => e.TaskId);
        }
    }
}
