using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgVShopAreaUserService
    {
        List<V_Shop_Area_User> List(long areaId);
    }
}
