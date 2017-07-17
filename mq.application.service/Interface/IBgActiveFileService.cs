using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgActiveFileService
    {
        long AddActiveFile(T_BG_ActiveFile activeFile);
        bool UpdatBath(List<T_BG_ActiveFile> activeFileList);
        T_BG_ActiveFile GetActiveFile(long id = -1);
        List<T_BG_ActiveFile> GetListGroupBy(int pageIndex, int pageSize, out long count);
    }
}
