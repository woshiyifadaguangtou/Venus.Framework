using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity.CBManage;

namespace Venus.Application.IService.CBManage
{
    interface ICBDetailSerivce
    {
        #region 获取数据
        IEnumerable<CBDetailEntity> GetCBDetailByTaskID(string taskId);
        #endregion

        #region 验证数据

        #endregion

        #region 提交数据
        /// <summary>
        /// 单户提交
        /// </summary>
        /// <param name="hh"></param>
        /// <param name="byds"></param>
        /// <param name="bkdm"></param>
        /// <param name="taskId"></param>
        void PostDataSingle(string hh, int byds, int bkdm, string taskId);
        /// <summary>
        /// 批量提交
        /// </summary>
        /// <param name="detailList"></param>
        void PostDataBatch(List<CBDetailEntity> detailList);
        #endregion

    }
}
