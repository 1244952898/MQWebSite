using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity.employeebg
{
    public class JsonShopGetShopEntity : JsonBaseEntity
    {
        public T_BG_Shop Shop { get; set; }

        public string OpenDate { get; set; }
    }
}
