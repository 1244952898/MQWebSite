using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity
{
    public class JsonDisplayGuideAptGetListData:JsonBaseEntity
    {
        public List<V_BG_DisplayGuideFile_User> DisplayGuideFileList { get; set; }
    }
}
