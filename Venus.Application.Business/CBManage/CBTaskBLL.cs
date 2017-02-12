using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venus.Application.IService.CBManage;
using Venus.Application.Service.CBManage;
using Venus.Application.Entity.CBManage;

namespace Venus.Application.Business.CBManage
{
    public class CBTaskBLL
    {
        private ICBTaskService service = new CBTaskService();

        public IEnumerable<CBTaskEntity> GetCBTaskListByCbydm(string cbydm)
        {
            return service.GetCBTask(cbydm);
        }

        public void SaveTask(string keyValue, CBTaskEntity cbTaskEntity)
        {
            try
            {
                service.SaveTask(keyValue, cbTaskEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateCBTask(string keyValue, CBTaskEntity cbTaskEntity)
        {
            try
            {
                service.CreateCBTask(keyValue, cbTaskEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
