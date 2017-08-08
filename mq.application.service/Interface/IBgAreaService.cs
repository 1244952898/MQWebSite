using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgAreaService
    {
        bool Add(T_BG_Area area);
        bool Del(T_BG_Area area);
        bool Update(T_BG_Area area);
        List<T_BG_Area> List();
        T_BG_Area Get(long id);
    }
}
