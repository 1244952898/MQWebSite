using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
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

        public bool UpdateDisplayGuideFile(T_BG_DisplayGuideFile displayGuideFile)
        {
            try
            {
                return _bgDisplayGuideFileRepository.Update(displayGuideFile);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T_BG_DisplayGuideFile> GetList()
        {
            try
            {
                string strSql = @"
                                    SELECT [Id],[Title],[FileNewName],[FileOriginName],
                                    [FilePath],[FileType],[UserId],[PublishTime],[AddTime],[IsDel]
                                    FROM [POS].[dbo].[T_BG_DisplayGuideFile]
                                    WHERE IsDel=0
                                    ORDER BY AddTime DESC
                                    ";
                var list = _bgDisplayGuideFileRepository.QueryList(strSql, null);
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

        public bool DelGuideFileAndUploadFile(string newFileName)
        {
            if (string.IsNullOrEmpty(newFileName))
                return false;
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("fileNewName", newFileName);
                bool result = _bgDisplayGuideFileRepository.ExcuteProcedure("DelUpFileAndDisplayGuideFileByFileNewName", parameters) > 0;
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
