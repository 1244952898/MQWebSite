using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using mq.application.enumlib;
using mq.dataaccess.sql;
using mq.model.extendedentity.employeebg;

namespace mq.application.service
{
    public class ActiveFileDepartmentExtendService : IActiveFileDepartmentExtendService
    {
        private readonly IActiveFileDepartmentExtendRepository _activeFileDepartmentExtendRepository;

        public ActiveFileDepartmentExtendService(IActiveFileDepartmentExtendRepository activeFileDepartmentExtendRepository)
        {
            _activeFileDepartmentExtendRepository = activeFileDepartmentExtendRepository;
        }

        public List<ActiveFileDepartmentExtend> GetList(ActiveFileTypeEnum type = ActiveFileTypeEnum.All)
        {
            try
            {
                //数据过大，可能会慢点，变成存储过程
                string sqlString = @"
                                    SELECT *
                                    FROM V_BG_ActiveFile_Department AS A
                                    LEFT JOIN 
                                    (SELECT DeparementId,COUNT(1) AS 'Count'
                                    FROM T_BG_ActiveFile 
                                    WHERE IsDel=0 AND PublicTime<=GETDATE() {0}
                                    GROUP BY DeparementId) AS B
                                    ON A.DeparementId=B.DeparementId
                                    WHERE IsDel=0 AND PublicTime<=GETDATE() {0}
                                    ORDER BY Lvl DESC,PublicTime DESC
                                    ";
                string whereString = String.Empty;
                DynamicParameters dynParams = new DynamicParameters();
                if (type != ActiveFileTypeEnum.All)
                {
                    whereString = " AND Type=@Type ";
                    dynParams.Add("Type", (int)type);
                }
                sqlString = String.Format(sqlString, whereString);
                var list = _activeFileDepartmentExtendRepository.QueryList(sqlString, dynParams, false);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
