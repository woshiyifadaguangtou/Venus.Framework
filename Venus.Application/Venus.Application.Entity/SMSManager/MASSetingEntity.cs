using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Application.Entity.SMSManager
{
    public class MASSetingEntity
    {

        public int ID { get; set; }
        /// <summary>
        /// 目标IP
        /// </summary>
        public string imIP { get; set; }
        public string LoginName { get; set; }
        public string LoginPWD { get; set; }
        /// <summary>
        /// API代码
        /// </summary>
        public string ApiCode { get; set; }
        /// <summary>
        /// MAS机数据库名
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }
    }
}
