using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using mq.application.service.Interface;
using mq.dataaccess.sql.Interface;
using mq.model.dbentity;

namespace mq.application.service.Implement
{
    public class BgUserService : IBgUserService
    {
        private static IBgUserRepository _bgUserRepository;
        public BgUserService(IBgUserRepository bgUserRepository)
        {
            _bgUserRepository = bgUserRepository;
        }

        public bool Login(string userName, string pwd)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_User>(f => f.Name, Operator.Eq, userName));
            pmain.Predicates.Add(Predicates.Field<T_BG_User>(f => f.PassWord, Operator.Eq, pwd));
            return _bgUserRepository.IsExist(pmain);
        }

        public T_BG_User GetUser(string userName, string pwd)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_User>(f => f.Name, Operator.Eq, userName));
            pmain.Predicates.Add(Predicates.Field<T_BG_User>(f => f.PassWord, Operator.Eq, pwd));
            return _bgUserRepository.GetModel(pmain);
        }

        public T_BG_User GetUserById(long id)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_User>(f => f.ID, Operator.Eq, id));
            return _bgUserRepository.GetModel(pmain);
        }

        public List<T_BG_User> GetList() 
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_User>(f => f.RoleID, Operator.Eq, 1));
            return _bgUserRepository.QueryList(pmain,null).ToList();
        }
    }
}
