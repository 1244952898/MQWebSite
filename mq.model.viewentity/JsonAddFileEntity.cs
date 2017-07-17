using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity
{
    public class JsonAddFileEntity:JsonBaseEntity
    {
        public T_BG_ActiveFile ActiveFile { get; set; }

        public string PublishTime { get; set; }
    }
}
