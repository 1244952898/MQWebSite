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
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public T_User Gt(string id)
        {
            PredicateGroup predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            var predicate = Predicates.Field<T_User>(f => f.Code, Operator.Eq, id);
            predicateGroup.Predicates.Add(predicate);
           return _userRepository.GetModel(predicateGroup);
        }
    }
}
