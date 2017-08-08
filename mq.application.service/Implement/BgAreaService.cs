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
    public class BgAreaService : IBgAreaService
    {
        private IBgAreaRepository _areaRepository;
        public BgAreaService(IBgAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public bool Add(T_BG_Area area)
        {
            try
            {
                return _areaRepository.Add(area) > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Del(T_BG_Area area)
        {
            try
            {
                return _areaRepository.Delete(area);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T_BG_Area area)
        {
            try
            {
                return _areaRepository.Update(area);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T_BG_Area> List()
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_Area>(f => f.IsDel, Operator.Eq, 0));
                IList<ISort> sort = new List<ISort> { Predicates.Sort<T_BG_Area>(o => o.AddTime, false) };
                var list = _areaRepository.QueryList(pmain, sort);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T_BG_Area Get(long id)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_Area>(f => f.ID, Operator.Eq, id));
                pmain.Predicates.Add(Predicates.Field<T_BG_Area>(f => f.IsDel, Operator.Eq, 0));
                return _areaRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
