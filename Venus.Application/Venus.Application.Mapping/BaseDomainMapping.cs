using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;

namespace Venus.Application.Mapping
{
    public class BaseDomainMapping<T>:EntityTypeConfiguration<T> where T :class,new()
    {
        public BaseDomainMapping()
        {
            init();
        }

        protected virtual void init()
        {
            Console.WriteLine("Mapping init");
        }
    }
}
