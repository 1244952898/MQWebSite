using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgPublishFileService
    {
        bool AddPublishFile(T_BG_PublishFile publishFile);
        T_BG_PublishFile GetPublishFileById(long id);
        List<T_BG_PublishFile> GetFlieList(int pageIndex, int pageSize, out long count);
        bool UpdatBath(List<T_BG_PublishFile> fileList);
        bool DelDateById(T_BG_PublishFile publishFile);

        bool UpdatePulishFile(T_BG_PublishFile publishFile);
    }
}
