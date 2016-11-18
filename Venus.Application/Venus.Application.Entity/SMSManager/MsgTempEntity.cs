using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Application.Entity
{
    public class MsgTempEntity
    {

        /// <summary>
        /// TempId
        /// </summary>
        public virtual string TempId
        {
            get;
            set;
        }
        /// <summary>
        /// TempName
        /// </summary>
        public virtual string TempName
        {
            get;
            set;
        }
        /// <summary>
        /// TempType
        /// </summary>
        public virtual string TempType
        {
            get;
            set;
        }
        /// <summary>
        /// TempContent
        /// </summary>
        public virtual string TempContent
        {
            get;
            set;
        }

    }
}
