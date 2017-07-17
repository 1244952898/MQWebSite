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
    public class BgActiveFileService : IBgActiveFileService
    {
        private readonly IBgActiveFileRepository _bgActiveFileRepository;
        public BgActiveFileService(IBgActiveFileRepository bgActiveFileRepository)
        {
            _bgActiveFileRepository = bgActiveFileRepository;
        }

        public long AddActiveFile(T_BG_ActiveFile activeFile)
        {
            try
            {
                return _bgActiveFileRepository.Add(activeFile);
            }
            catch (Exception ex)
            {
                return -1L;
            }
        }

        public bool UpdatBath(List<T_BG_ActiveFile> activeFileList)
        {
            if (activeFileList == null || activeFileList.Count <= 0)
            {
                return false;
            }
            string sqlString = @" UPDATE T_BG_ActiveFile SET IsDel=1 WHERE iD=@Id";
            return _bgActiveFileRepository.BatchUpdate(sqlString, activeFileList);
        }

        public T_BG_ActiveFile GetActiveFile(long id = -1)
        {
            if (id < 0)
            {
                return null;
            }
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_ActiveFile>(f => f.Id, Operator.Eq, id));
            pmain.Predicates.Add(Predicates.Field<T_BG_ActiveFile>(f => f.IsDel, Operator.Eq, 0));
            return _bgActiveFileRepository.GetModel(pmain);
        }

        public List<T_BG_ActiveFile> GetListGroupBy(int pageIndex, int pageSize, out long count)
        {
            try
            {
                string sqlStr =String.Format(@"select * from T_BG_ActiveFile limit {0},{1}",(pageIndex-1)*pageSize,pageSize); 
                DynamicParameters pars=new DynamicParameters();
                var list = _bgActiveFileRepository.QueryList(sqlStr, pars);
                var bgActiveFiles = list as T_BG_ActiveFile[] ?? list.ToArray();
                count = bgActiveFiles.Length;
                return bgActiveFiles.ToList();
            }
            catch (Exception)
            {
                // ignored
            }
            count = -1;
            return null;
        }

    }
}
