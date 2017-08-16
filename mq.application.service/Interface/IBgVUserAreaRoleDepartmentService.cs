using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgVUserAreaRoleDepartmentService
    {
        List<V_User_Area_Role_Department_Shop> GetList();
    }
}
