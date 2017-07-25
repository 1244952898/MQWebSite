using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity
{
    public class JsonDisplayGuideEntity:JsonBaseEntity
    {
        public T_BG_DisplayGuideFile File { get; set; }
        public string PublishTime { get; set; }
    }
}
