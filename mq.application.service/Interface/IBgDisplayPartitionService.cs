using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgDisplayPartitionService
    {
        long AddDisplayPartition(T_BG_DisplayPartition displayGuideFile);
        List<T_BG_DisplayPartition> GetList(bool isBack = true);
        bool UpdateAndDelMongoDb(T_BG_DisplayPartition displayGuideFile, string mongDbName);

        T_BG_DisplayPartition Get(long id);
    }
}
