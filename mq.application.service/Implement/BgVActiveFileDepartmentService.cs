using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using mq.application.common;
using mq.application.enumlib;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgVActiveFileDepartmentService : IBgVActiveFileDepartmentService
    {
        private IBgVActiveFileDepartmentRepository _activeFileDepartmentRepository;

        public BgVActiveFileDepartmentService(IBgVActiveFileDepartmentRepository activeFileDepartmentRepository)
        {
            _activeFileDepartmentRepository = activeFileDepartmentRepository;

        }

        public List<V_BG_ActiveFile_Department> GetList(ActiveFileTypeEnum type = ActiveFileTypeEnum.All)
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
                var list = _activeFileDepartmentRepository.QueryList(sqlString, dynParams, false);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_BG_ActiveFile_Department> GetListForDel(ActiveFileTypeEnum type = ActiveFileTypeEnum.All)
        {
            try
            {
                string sqlString = @"
                                    SELECT *
                                    FROM V_BG_ActiveFile_Department 
                                    WHERE IsDel=0 AND PublicTime<=GETDATE() {0}
                                    ORDER BY PublicTime DESC
                                   ";
                string whereString = String.Empty;
                DynamicParameters dynParams = new DynamicParameters();
                if (type != ActiveFileTypeEnum.All)
                {
                    whereString = " AND Type=@Type ";
                    dynParams.Add("Type", (int)type);
                }
                sqlString = String.Format(sqlString, whereString);
                var list = _activeFileDepartmentRepository.QueryList(sqlString, dynParams, false);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
