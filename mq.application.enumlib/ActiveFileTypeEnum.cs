using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.enumlib
{
    public enum ActiveFileTypeEnum
    {
        All=0,
        /// <summary>
        /// 发布
        /// </summary>
        Publish = 1,
        /// <summary>
        /// 办公室
        /// </summary>
        Office = 2,
        /// <summary>
        /// 店铺
        /// </summary>
        Shop = 3,
    }
}
