using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.application.enumlib;
using mq.dataaccess;
using mq.model.dbentity;

namespace mq.application.service
{
    public interface IBgVActiveFileDepartmentService
    {
        List<V_BG_ActiveFile_Department> GetList(ActiveFileTypeEnum type = ActiveFileTypeEnum.All);

        List<V_BG_ActiveFile_Department> GetListForDel(ActiveFileTypeEnum type = ActiveFileTypeEnum.All);
    }
}
