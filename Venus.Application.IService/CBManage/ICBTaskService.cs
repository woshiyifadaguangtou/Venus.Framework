using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;
using Venus.Application.Entity.CBManager;

namespace Venus.Application.IService.CBManage
{
    public interface ICBTaskService
    {
        #region 获取数据

        /// <summary>
        ///  根据任务ID获取抄表明细
        /// </summary>
        /// <returns></returns>
        IEnumerable<CBDetailEntity> GetCBDetailByTaskId(int TaskId);
        /// <summary>
        /// 根据册号获取抄表明细
        /// </summary>
        /// <param name="xh"></param>
        /// <returns></returns>
        IEnumerable<CBDetailEntity> GetCBDetailByXh(string xh);
        #endregion


        #region 验证数据
        #endregion

        #region 提交数据
        #endregion

    }
}
