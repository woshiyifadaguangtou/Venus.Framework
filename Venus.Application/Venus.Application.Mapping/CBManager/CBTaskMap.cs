using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity.CBManage;

namespace Venus.Application.Mapping.CBManager
{
    public class CBTaskMap : BaseDomainMapping<CBTaskEntity>
    {
        protected override void init()
        {
            this.ToTable("CBTask");
            this.HasKey(e => e.TaskId);
            this.Property(t => t.xh).HasMaxLength(8);
            this.Property(t => t.fdate).HasMaxLength(6);
            this.Property(t => t.cbydm).HasMaxLength(10);
        }
    }
}
