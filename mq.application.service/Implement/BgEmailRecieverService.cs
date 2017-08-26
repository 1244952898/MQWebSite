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
    public class BgEmailRecieverService : IBgEmailRecieverService
    {
        private readonly IBgEmailRecieverRepository _bgEmailRecieverRepository;
        public BgEmailRecieverService(IBgEmailRecieverRepository bgEmailRecieverRepository)
        {
            _bgEmailRecieverRepository = bgEmailRecieverRepository;
        }

        public bool BatchAdd(List<T_BG_EmailReciever> list)
        {
            if (list == null)
            {
                return false;
            }
            try
            {
                _bgEmailRecieverRepository.BatchAdd(list);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public T_BG_EmailReciever Get(long id)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_EmailReciever>(f => f.Id, Operator.Eq, id));
                //pmain.Predicates.Add(Predicates.Field<T_BG_EmailReciever>(f => f.IsDel, Operator.Eq, 0));
                return _bgEmailRecieverRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(T_BG_EmailReciever emailReciever)
        {
            try
            {
                return _bgEmailRecieverRepository.Update(emailReciever);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
