using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgVUserAreaRoleDepartmentService : IBgVUserAreaRoleDepartmentService
    {
        private readonly IBgVUserAreaRoleDepartmentRepository _bgVUserAreaRoleDepartmentRepository;
        public BgVUserAreaRoleDepartmentService(IBgVUserAreaRoleDepartmentRepository bgVUserAreaRoleDepartmentRepository)
        {
            _bgVUserAreaRoleDepartmentRepository = bgVUserAreaRoleDepartmentRepository;
        }

        public List<V_User_Area_Role_Department_Shop> GetList()
        {
            try
            {
               return _bgVUserAreaRoleDepartmentRepository.QueryList(null,null).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
