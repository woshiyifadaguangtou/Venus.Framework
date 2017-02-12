using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.Entity;
using Venus.Application.Entity.CBManage;

namespace Venus.Application.IService.CBManage
{
    public interface ICBTaskService
    {
        #region 获取数据
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="cbydm">抄表员代码</param>
        /// <returns></returns>
        IEnumerable<CBTaskEntity> GetCBTask(string cbydm);

        #endregion


        #region 验证数据
        /// <summary>
        /// 相同册号和抄表日期的任务不能重复
        /// </summary>
        /// <param name="xh">册号</param>
        /// <param name="fdate">账务日期</param>
        /// <returns></returns>
        bool ExistTask(string xh, string fdate);

        #endregion

        #region 提交数据

        void SaveTask(string keyValue, CBTaskEntity cbTaskEntity);

        void CreateCBTask(string keyValue, CBTaskEntity entity);
        #endregion

    }
}
