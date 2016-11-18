using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;

namespace Venus.Application.Mapping.SMSManager
{
    public class MASSetingMap:BaseDomainMapping<MASSetingEntity>
    {
        protected override void init()
        {
            this.ToTable("MASSeting");
            this.HasKey(e=>e.ID);
        }
    }
}
