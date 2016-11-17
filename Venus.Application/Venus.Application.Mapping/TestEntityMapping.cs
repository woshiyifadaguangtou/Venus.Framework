using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;

namespace Venus.Application.Mapping
{
    public class TestEntityMapping: BaseDomainMapping <TestEntity>
    {
        protected override void init()
        {
            this.ToTable("Test");
            this.HasKey(t => t.Id);
        }
    }
}
