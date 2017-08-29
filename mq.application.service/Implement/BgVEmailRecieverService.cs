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
    public class BgVEmailRecieverService : IBgVEmailRecieverService
    {
        private readonly IBgVEmailRecieverRepository _bgVEmailRecieverRepository;
        public BgVEmailRecieverService(IBgVEmailRecieverRepository bgVEmailRecieverRepository)
        {
            _bgVEmailRecieverRepository = bgVEmailRecieverRepository;
        }

        public List<V_BG_Email_Reciever> GetList(string userName)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<V_BG_Email_Reciever>(f => f.RevieverUserId, Operator.Eq, userName));
                pmain.Predicates.Add(Predicates.Field<V_BG_Email_Reciever>(f => f.RevieverIsDel, Operator.Eq, 0));

                IList<ISort> sort = new List<ISort> { Predicates.Sort<V_BG_Email_Reciever>(o => o.AddTime, false) };

                var list = _bgVEmailRecieverRepository.QueryList(pmain, sort);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public V_BG_Email_Reciever Get(long userid)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<V_BG_Email_Reciever>(f => f.RevieverUserId, Operator.Eq, userid));
                pmain.Predicates.Add(Predicates.Field<V_BG_Email_Reciever>(f => f.IsDel, Operator.Eq, 0));

                return _bgVEmailRecieverRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public V_BG_Email_Reciever GetByRecieverMailId(long emailId)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<V_BG_Email_Reciever>(f => f.RecieverEmailId, Operator.Eq, emailId));
                pmain.Predicates.Add(Predicates.Field<V_BG_Email_Reciever>(f => f.IsDel, Operator.Eq, 0));

                return _bgVEmailRecieverRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
