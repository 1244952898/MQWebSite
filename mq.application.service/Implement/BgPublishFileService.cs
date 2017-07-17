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
    public class BgPublishFileService : IBgPublishFileService
    {
        private IBgPublishFileRepository _bgPublishFileRepository;
        public BgPublishFileService(IBgPublishFileRepository bgPublishFileRepository)
        {
            _bgPublishFileRepository = bgPublishFileRepository;
        }

        public bool AddPublishFile(T_BG_PublishFile publishFile)
        {
            try
            {
                long result = _bgPublishFileRepository.Add(publishFile);
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T_BG_PublishFile GetPublishFileById(long id)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_PublishFile>(f => f.Id, Operator.Eq, id));
            pmain.Predicates.Add(Predicates.Field<T_BG_PublishFile>(f => f.IsDel, Operator.Eq, 0));
            pmain.Predicates.Add(Predicates.Field<T_BG_PublishFile>(f => f.PublishState, Operator.Eq, 1));
            return _bgPublishFileRepository.GetModel(pmain);
        }

        public List<T_BG_PublishFile> GetFlieList(int pageIndex, int pageSize, out long count)
        {
            try
            {
                PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pmain.Predicates.Add(Predicates.Field<T_BG_PublishFile>(f => f.IsDel, Operator.Eq, 0));
                pmain.Predicates.Add(Predicates.Field<T_BG_PublishFile>(f => f.PublishState, Operator.Eq, 1));
                IList<ISort> sort = new List<ISort> { Predicates.Sort<T_BG_PublishFile>(o => o.PublishTime, false) };
                var list = _bgPublishFileRepository.QueryList(pmain, sort, pageIndex, pageSize, out count);
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

        public bool UpdatBath(List<T_BG_PublishFile> fileList)
        {
            if (fileList == null || fileList.Count <= 0)
            {
                return false;
            }
            string sqlString = @" UPDATE T_BG_PublishFile SET IsDel=1 WHERE Id=@Id";
            return _bgPublishFileRepository.BatchUpdate(sqlString, fileList);
        }

        public bool DelDateById(T_BG_PublishFile publishFile)
        {
            if (publishFile == null)
                return false;
            string sqlString = @" UPDATE T_BG_PublishFile SET IsDel=1 WHERE Id=@Id";
            return _bgPublishFileRepository.Update(sqlString, publishFile);
        }

        public bool UpdatePulishFile(T_BG_PublishFile publishFile)
        {
            if (publishFile==null)
            {
                return false;
            }
            return _bgPublishFileRepository.Update(publishFile);
        }
    }
}
