using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using mq.dataaccess.sql;
using mq.dataaccess.sql.Implement;
using mq.dataaccess.sql.Interface;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgMenuService : IBgMenuService
    {
        IBgMenuRepository _bgMenuRepository;
        public BgMenuService(IBgMenuRepository bgMenuRepository)
        {
            _bgMenuRepository = bgMenuRepository;
        }

        public IEnumerable<T_BG_Menu> GetBgMenuByUserId(long userId)
        {
            try
            {
                string sql = @"
                                SELECT C.* FROM T_BG_User as A
                                left join T_BG_Role_Menu as B on a.ID=b.RoleID
                                join T_BG_Menu as C on b.MenuID=c.ID
                                where A.IsDel=0 and b.IsDel=0 and c.IsDel=0 and A.ID=@ID
                                ";
                DynamicParameters dynParams = new DynamicParameters();
                dynParams.Add("@ID", userId);
                return _bgMenuRepository.QueryList(sql, dynParams).ToList();
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }
    }
}
