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
    public class BgEmailService:IBgEmailService
    {
        private readonly IBgEmailRepository _bgEmailRepository;

        public BgEmailService(IBgEmailRepository bgEmailRepository)
        {
            _bgEmailRepository = bgEmailRepository;
        }

        public long Add(T_BG_Email email)
        {
            try
            {
              return _bgEmailRepository.Add(email);
            }
            catch (Exception)
            {
                return -1L;
            }
        }

        public List<T_BG_Email> List(long userId)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_Email>(f => f.SendUserId, Operator.Eq, userId));
                pmain.Predicates.Add(Predicates.Field<T_BG_Email>(f => f.IsDel, Operator.Eq, 0));

                IList<ISort> sort = new List<ISort> { Predicates.Sort<T_BG_Email>(o => o.AddTime, false) };

                var list = _bgEmailRepository.QueryList(pmain, sort);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T_BG_Email Get(long id)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_Email>(f => f.Id, Operator.Eq, id));
               return _bgEmailRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(T_BG_Email email)
        {
            try
            {
                return _bgEmailRepository.Update(email);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
