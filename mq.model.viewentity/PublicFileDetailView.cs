using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.model.viewentity
{
    public class PublicFileDetailView
    {
        public V_BG_PublishFile_User BgPublishFileUser { get; set; }
        public V_BG_PublishFile_User BgPublishFileUserPre { get; set; }
        public V_BG_PublishFile_User BgPublishFileUserNext { get; set; }
    }
}
