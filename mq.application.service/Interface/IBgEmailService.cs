using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgEmailService
    {
        long Add(T_BG_Email email);
        List<T_BG_Email> List(long userId);
        T_BG_Email Get(long id);
        bool Update(T_BG_Email email);
    }
}
