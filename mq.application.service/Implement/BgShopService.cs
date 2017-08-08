﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using mq.application.service.Interface;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service.Implement
{
    public class BgShopService : IBgShopService
    {
         private IBgShopRepository _shopRepository;
         public BgShopService(IBgShopRepository shopRepository)
        {
            shopRepository = shopRepository;
        }

         public bool Add(T_BG_Shop shop)
        {
            try
            {
                return _shopRepository.Add(shop) > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

         public bool Del(T_BG_Shop shop)
        {
            try
            {
                return _shopRepository.Delete(shop);
            }
            catch (Exception)
            {
                return false;
            }
        }

         public bool Update(T_BG_Shop shop)
        {
            try
            {
                return _shopRepository.Update(shop);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T_BG_Shop> List(long areaId)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                if (areaId>0)
                {
                    pmain.Predicates.Add(Predicates.Field<T_BG_Shop>(f => f.AreaId, Operator.Eq, areaId));
                }
               
                IList<ISort> sort = new List<ISort> { Predicates.Sort<T_BG_Shop>(o => o.ID, false) };
                var list = _shopRepository.QueryList(pmain, sort);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}