using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity.CBManage;

namespace Venus.Application.Mapping.CBManager
{
    class CBDetailMap: BaseDomainMapping<CBDetailEntity>
    {
        protected override void init()
        {
            this.ToTable("CBDetail");
            this.HasKey(e => e.Id);

            this.Property(e => e.hh).HasMaxLength(8);
            this.Property(e => e.xh).HasMaxLength(4);
            this.Property(e => e.hm).HasMaxLength(20);
            this.Property(e => e.bh).HasMaxLength(6);
            this.Property(e => e.dh).HasMaxLength(20);
            this.Property(e => e.dz).HasMaxLength(40);
        }
    }
}
