using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgEmailRecieverService
    {
        bool BatchAdd(List<T_BG_EmailReciever> list);
        T_BG_EmailReciever Get(long id);
        bool Update(T_BG_EmailReciever emailReciever);
    }
}
