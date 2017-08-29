using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgVEmailRecieverService
    {
        List<V_BG_Email_Reciever> GetList(string userName);
        V_BG_Email_Reciever Get(long userid);
        V_BG_Email_Reciever GetByRecieverMailId(long emailId);
    }
}
