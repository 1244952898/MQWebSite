using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service.Interface
{
    public interface IBgUserService
    {
        bool Login(string userName, string pwd);
        T_BG_User GetUser(string userName, string pwd);
        T_BG_User GetUserById(long id);
        List<T_BG_User> GetList();
        bool Add(T_BG_User user);
        bool Check(string username);
        bool Update(T_BG_User user);
    }
}
