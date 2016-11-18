using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Application.Entity
{
    public class MessageSystemTaskEntity
    {

        /// <summary>
        /// TaskID
        /// </summary>
        public virtual int TaskID
        {
            get;
            set;
        }
        /// <summary>
        /// TaskType
        /// </summary>
        public virtual string TaskType
        {
            get;
            set;
        }
        /// <summary>
        /// MsgCount
        /// </summary>
        public virtual int MsgCount
        {
            get;
            set;
        }
        /// <summary>
        /// TaskState
        /// </summary>
        public virtual string TaskState
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>
        public virtual DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// StarTime
        /// </summary>
        public virtual DateTime StarTime
        {
            get;
            set;
        }
        /// <summary>
        /// StopTime
        /// </summary>
        public virtual DateTime StopTime
        {
            get;
            set;
        }
        /// <summary>
        /// MsgContext
        /// </summary>
        public virtual string MsgContext
        {
            get;
            set;
        }
        /// <summary>
        /// MinMoney
        /// </summary>
        public virtual decimal? MinMoney
        {
            get;
            set;
        }
        /// <summary>
        /// MaxMoney
        /// </summary>
        public virtual decimal? MaxMoney
        {
            get;
            set;
        }
        /// <summary>
        /// MinUserNo
        /// </summary>
        public virtual string MinUserNo
        {
            get;
            set;
        }
        /// <summary>
        /// MaxUserNo
        /// </summary>
        public virtual string MaxUserNo
        {
            get;
            set;
        }

    }
}
