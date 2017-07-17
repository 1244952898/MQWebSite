using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgDisplayGuideFileService
    {
        long AddDisplayGuideFile(T_BG_DisplayGuideFile displayGuideFile);
        T_BG_DisplayGuideFile GetBgDisplayGuideFile(long id);
    }
}
