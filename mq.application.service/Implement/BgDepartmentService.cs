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
    public class BgDepartmentService : IBgDepartmentService
    {
        private IBgDepartmentRepository _bgDepartmentRepository;

        public BgDepartmentService(IBgDepartmentRepository bgDepartmentRepository)
        {
            _bgDepartmentRepository = bgDepartmentRepository;
        }

        public List<T_BG_Department> GetListDepartment()
        {
            try
            {
                string strSql = @"
                                    SELECT [Id],[DepartmentName],[AddTime],[IsDel],[Lvl]
                                    FROM T_BG_Department
                                    ORDER BY Lvl DESC
                                    ";
            var list =   _bgDepartmentRepository.QueryList(strSql, null);
            if (list!=null)
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
