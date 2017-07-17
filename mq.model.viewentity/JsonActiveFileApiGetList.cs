using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;
using mq.model.extendedentity.employeebg;

namespace mq.model.viewentity
{
    public class JsonActiveFileApiGetList : JsonBaseEntity
    {
        public List<V_BG_ActiveFile_Department> List { get; set; }
    }
}
