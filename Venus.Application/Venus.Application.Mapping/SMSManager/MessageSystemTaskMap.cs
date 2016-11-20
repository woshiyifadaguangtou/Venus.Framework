using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;

namespace Venus.Application.Mapping.SMSManager
{
    public class MessageSystemTaskMap : BaseDomainMapping<MessageSystemTaskEntity>
    {
        protected override void init()
        {
            this.ToTable("MessageSystem");
            this.HasKey(e => e.TaskID);
        }
    }
}
