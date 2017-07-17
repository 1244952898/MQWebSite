using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using mq.application.common;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgUpFilesService:IBgUpFilesService
    {
        IBgUpFilesRepository _bgUpFilesRepository;

        public BgUpFilesService( IBgUpFilesRepository bgUpFilesRepository)
        {
            _bgUpFilesRepository = bgUpFilesRepository;
        }

        public long Add(T_BG_UpFiles bgUpFiles)
        {
           return _bgUpFilesRepository.Add(bgUpFiles);
        }

        public long GetListByUserIdAndFileNameAndExt(string originFileName, long? userid, string ext)
        {
            PredicateGroup pmain = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.fileoriginname, Operator.Eq, originFileName));
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.userid, Operator.Eq, userid));
            pmain.Predicates.Add(Predicates.Field<T_BG_UpFiles>(f => f.ext, Operator.Eq, ext));
           return _bgUpFilesRepository.QueryRecordCount(pmain);
        }
    }
}
