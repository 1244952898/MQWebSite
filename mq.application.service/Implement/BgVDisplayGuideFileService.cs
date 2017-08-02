using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.application.service.Interface;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgVDisplayGuideFileService : IBgVDisplayGuideFileService
    {
        private readonly IBgVDisplayGuideFileRepository _bgVDisplayGuideFileRepository;

        public BgVDisplayGuideFileService(IBgVDisplayGuideFileRepository bgVDisplayGuideFileRepository)
        {
            _bgVDisplayGuideFileRepository = bgVDisplayGuideFileRepository;
        }


        public List<V_BG_DisplayGuideFile_User> GetList(bool isBack=true)
        {
            try
            {
                string strSql = @"
                                    SELECT [Name]
                                    ,[Id]
                                    ,[Title]
                                    ,[FileNewName]
                                    ,[FileOriginName]
                                    ,[FilePath]
                                    ,[FileType]
                                    ,[UserId]
                                    ,[PublishTime]
                                    ,[AddTime]
                                    ,[IsDel]
                                    FROM [POS].[dbo].[V_BG_DisplayGuideFile_User]
                                    WHERE IsDel=0 {0}
                                    ORDER BY AddTime DESC
                                    ";
                string wherString=String.Empty;
                if (!isBack)
                {
                    wherString = " AND PublishTime <= GETDATE() ";
                    strSql = String.Format(strSql, wherString);
                }
                var list = _bgVDisplayGuideFileRepository.QueryList(strSql, null);
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
    }
}
