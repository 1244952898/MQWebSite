﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgUpFilesService
    {
        long Add(T_BG_UpFiles bgUpFiles);
        long GetListByUserIdAndFileNameAndExt(string originFileName, long? userid, string ext);
    }
}