using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgMenuService
    {
        IEnumerable<T_BG_Menu> GetBgMenuByUserId(long userId);
    }
}
