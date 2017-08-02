using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using mq.dataaccess.nosql;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgDisplayPartitionService : IBgDisplayPartitionService
    {
        private IBgDisplayPartitionRepository _bgDisplayPartitionRepository;
        public BgDisplayPartitionService(IBgDisplayPartitionRepository bgDisplayPartitionRepository)
        {
            _bgDisplayPartitionRepository = bgDisplayPartitionRepository;
        }

        public long AddDisplayPartition(T_BG_DisplayPartition displayGuideFile)
        {
            try
            {
                return _bgDisplayPartitionRepository.Add(displayGuideFile);
            }
            catch (Exception)
            {
                return -1L;
            }
        }

        public List<T_BG_DisplayPartition> GetList(bool isBack = true)
        {
            try
            {
                string strSql = @"
                                    SELECT [Id]
                                    ,[UserId]
                                    ,[FileName]
                                    ,[DepartmentId]
                                    ,[AddTime]
                                    ,[PublishTime]
                                    ,[IsDel]
                                    ,[DepartmentName]
                                    ,[OldFileName]
                                    FROM [POS].[dbo].[T_BG_DisplayPartition]
                                    WHERE IsDel=0 {0}
                                    ";
                var wherString = !isBack ? " AND PublishTime <= GETDATE() ORDER BY AddTime DESC" : " ORDER BY PublishTime DESC";
                strSql = String.Format(strSql, wherString);
                var list = _bgDisplayPartitionRepository.QueryList(strSql, null);
                if (list != null)
                {
                    return list.ToList();
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }

        public bool UpdateAndDelMongoDb(T_BG_DisplayPartition displayGuideFile, string mongDbName)
        {
            try
            {
                MongoDBHelper.DelFileByName(mongDbName, displayGuideFile.FileName);
                return _bgDisplayPartitionRepository.Update(displayGuideFile);
            }
            catch (Exception)
            {
                return false;
            }

        }


        public T_BG_DisplayPartition Get(long id)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_DisplayPartition>(f => f.Id, Operator.Eq, id));
                pmain.Predicates.Add(Predicates.Field<T_BG_DisplayPartition>(f => f.IsDel, Operator.Eq, 0));
                return _bgDisplayPartitionRepository.GetModel(pmain);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
