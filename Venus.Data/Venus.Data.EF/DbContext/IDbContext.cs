using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Data.EF
{
    public interface IDbContext: IDisposable, IObjectContextAdapter
    {
    }
}
