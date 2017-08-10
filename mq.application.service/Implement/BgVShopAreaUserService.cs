using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgVShopAreaUserService : IBgVShopAreaUserService
    {
        private readonly IBgVShopAreaUserRepository _bgVShopAreaUserRepositoiry;
        public BgVShopAreaUserService(IBgVShopAreaUserRepository bgVShopAreaUserRepositoiry)
        {
            _bgVShopAreaUserRepositoiry = bgVShopAreaUserRepositoiry;
        }

        public List<V_Shop_Area_User> List(long areaId)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                IList<ISort> sort = new List<ISort> { Predicates.Sort<V_Shop_Area_User>(o => o.ID, false) };
                IEnumerable<V_Shop_Area_User> list;
                if (areaId > 0)
                {
                    pmain.Predicates.Add(Predicates.Field<V_Shop_Area_User>(f => f.AreaId, Operator.Eq, areaId));
                    list = _bgVShopAreaUserRepositoiry.QueryList(pmain, sort);
                }
                else
                {
                    list = _bgVShopAreaUserRepositoiry.QueryList(null, sort);
                }

                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
