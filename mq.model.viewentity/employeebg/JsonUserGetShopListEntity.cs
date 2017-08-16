using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity.employeebg
{
    public class JsonUserGetShopListEntity:JsonBaseEntity
    {
        public List<T_BG_Shop> ShopList { get; set; }
    }
}
