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
    public class BgRoleService : IBgRoleService
    {
        private readonly IBgRoleRepository _bgRoleRepository;
        public BgRoleService(IBgRoleRepository bgRoleRepository)
        {
            _bgRoleRepository = bgRoleRepository;
        }

        public List<T_BG_Role> List()
        {
            try
            {
                IEnumerable<T_BG_Role> list = _bgRoleRepository.QueryList(null, null);
                return list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
