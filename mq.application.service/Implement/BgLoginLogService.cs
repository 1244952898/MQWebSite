using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.application.service.Interface;
using mq.dataaccess.sql.Interface;
using mq.model.dbentity;

namespace mq.application.service.Implement
{
    public class BgLoginLogService : IBgLoginLogService
    {
        IBgLoginLogRepository _bgLoginLogRepository;

        public BgLoginLogService(IBgLoginLogRepository bgLoginLogRepository)
        {
            _bgLoginLogRepository = bgLoginLogRepository;
        }

        public long Add(T_BG_LoginLog loginLog)
        {
            return _bgLoginLogRepository.Add(loginLog);
        }
    }
}
