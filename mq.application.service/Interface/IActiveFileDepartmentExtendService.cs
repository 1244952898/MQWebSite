using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.application.enumlib;
using mq.model.extendedentity.employeebg;

namespace mq.application.service
{
    public interface IActiveFileDepartmentExtendService
    {
        List<ActiveFileDepartmentExtend> GetList(ActiveFileTypeEnum type = ActiveFileTypeEnum.All);
    }
}
