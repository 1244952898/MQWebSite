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
    public class BgDisplayGuideFileService : IBgDisplayGuideFileService
    {
        private readonly IBgDisplayGuideFileRepository _bgDisplayGuideFileRepository;
        public BgDisplayGuideFileService(IBgDisplayGuideFileRepository bgDisplayGuideFileRepository)
        {
            _bgDisplayGuideFileRepository = bgDisplayGuideFileRepository;
        }

        public long AddDisplayGuideFile(T_BG_DisplayGuideFile displayGuideFile)
        {
            try
            {
                return _bgDisplayGuideFileRepository.Add(displayGuideFile);
            }
            catch (Exception)
            {
                return -1L;
            }
        }

        public T_BG_DisplayGuideFile GetBgDisplayGuideFile(long id)
        {
            if (id < 0)
            {
                return null;
            }
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_DisplayGuideFile>(f => f.Id, Operator.Eq, id));
            pmain.Predicates.Add(Predicates.Field<T_BG_DisplayGuideFile>(f => f.IsDel, Operator.Eq, 0));
            return _bgDisplayGuideFileRepository.GetModel(pmain);
        }
    }
}
