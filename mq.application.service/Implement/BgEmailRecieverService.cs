using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.dataaccess.sql;
using mq.model.dbentity;

namespace mq.application.service
{
    public class BgEmailRecieverService : IBgEmailRecieverService
    {
        private readonly IBgEmailRecieverRepository _bgEmailRecieverRepository;
        public BgEmailRecieverService(IBgEmailRecieverRepository bgEmailRecieverRepository)
        {
            _bgEmailRecieverRepository = bgEmailRecieverRepository;
        }

        public bool BatchAdd(List<T_BG_EmailReciever> list)
        {
            if (list == null)
            {
                return false;
            }
            try
            {
                _bgEmailRecieverRepository.BatchAdd(list);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
