using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgVPublishFileUserService
    {
        List<V_BG_PublishFile_User> GetFlieList(int pageIndex, int pageSize, out long count);
        V_BG_PublishFile_User GetFlieById(long id);
        V_BG_PublishFile_User GetFlieByIdPre(DateTime dateTime, long id);
        V_BG_PublishFile_User GetFlieByIdNext(DateTime dateTime, long id);
    }
}
