using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.dataaccess.sql.Interface;
using mq.model.dbentity;

namespace mq.dataaccess.sql.Implement
{
    public class BgUserRepository : RepositoryBase<T_BG_User>, IBgUserRepository
    {
    }
}
