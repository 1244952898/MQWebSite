using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgVPublishFileUserService : IBgVPublishFileUserService
    {
        private IBgVPublishFileUserRepository _bgVPublishFileUserRepository;
        public BgVPublishFileUserService(IBgVPublishFileUserRepository bgVPublishFileUserRepository)
        {
            _bgVPublishFileUserRepository = bgVPublishFileUserRepository;
        }

        public List<V_BG_PublishFile_User> GetFlieList(int pageIndex, int pageSize, out long count)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                //视图已经筛选过了
                pmain.Predicates.Add(Predicates.Field<V_BG_PublishFile_User>(f => f.PublishState, Operator.Eq, 1));
                IList<ISort> sort = new List<ISort>
                {
                    Predicates.Sort<V_BG_PublishFile_User>(o => o.PublishTime, false), 
                    Predicates.Sort<V_BG_PublishFile_User>(o => o.Id, false)
                };
                var list = _bgVPublishFileUserRepository.QueryList(pmain, sort, pageIndex, pageSize, out count);
                if (list != null)
                {
                    return list.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                count = 0;
                return null;
            }
        }

        public V_BG_PublishFile_User GetFlieById(long id)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<V_BG_PublishFile_User>(f => f.Id, Operator.Eq, id));
                return _bgVPublishFileUserRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public V_BG_PublishFile_User GetFlieByIdPre(DateTime dateTime, long id)
        {
            try
            {
                string sqlString = @"
                    SELECT TOP 1 * FROM V_BG_PublishFile_User
                    WHERE PublishTime>@dateTime AND Id!=@id
                    ORDER BY PublishTime asc,id asc
                    ";
                DynamicParameters dynParams = new DynamicParameters();
                dynParams.Add("dateTime", dateTime);
                dynParams.Add("id", id);
                return _bgVPublishFileUserRepository.GetModel(sqlString, dynParams);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public V_BG_PublishFile_User GetFlieByIdNext(DateTime dateTime, long id)
        {
            try
            {
                string sqlString = @"
                                    select top 1  *  from (
                                     SELECT  * FROM V_BG_PublishFile_User
                                    WHERE  PublishTime>=@dateTime AND Id!=@id
                                    ) as a
                                    order by a.PublishTime desc,a.id desc
                    ";
                DynamicParameters dynParams = new DynamicParameters();
                dynParams.Add("dateTime", dateTime);
                dynParams.Add("id", id);
                return _bgVPublishFileUserRepository.GetModel(sqlString, dynParams);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
