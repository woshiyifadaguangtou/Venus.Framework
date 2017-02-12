using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Data;
using Venus.Data.Repository;
using Venus.Application.Entity.CBManage;
using Venus.Application.IService.CBManage;
namespace Venus.Application.Service.CBManage
{
    public class CBTaskService : RepositoryFactory<CBTaskEntity>, ICBTaskService
    {
        #region 获取数据

        IEnumerable<CBTaskEntity> ICBTaskService.GetCBTask(string cbydm)
        {
            return this.BaseRepository().IQueryable().Where(e => e.cbydm == cbydm);
        }
        #endregion

        #region 验证数据

        bool ICBTaskService.ExistTask(string xh, string fdate)
        {
            return !this.BaseRepository().IQueryable().Any(e => e.xh == xh && e.fdate == fdate);
        }
        
        #endregion

        #region 提交数据
        void ICBTaskService.SaveTask(string keyValue, CBTaskEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                try
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        public void CreateCBTask(string keyValue, CBTaskEntity entity)
        {
            var tempEntity = this.BaseRepository().FindEntity(keyValue);
            if (tempEntity != null)
            {
                return;
            }
            entity.Create();
            this.BaseRepository().Insert(entity);
        }
        #endregion
    }
}
