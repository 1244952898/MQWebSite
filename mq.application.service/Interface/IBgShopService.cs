using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service.Interface
{
    public interface IBgShopService
    {
        bool Add(T_BG_Shop shop);
        bool Del(T_BG_Shop shop);
        bool Update(T_BG_Shop shop);
        List<T_BG_Shop> List(long areaId);
        T_BG_Shop Get(long id);
    }
}
