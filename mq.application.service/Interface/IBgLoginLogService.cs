using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service.Interface
{
    public interface IBgLoginLogService
    {
        long Add(T_BG_LoginLog loginLog);
    }
}
