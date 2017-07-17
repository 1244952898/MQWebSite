using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.common
{
    public class MenuHelper
    {
        public static string IconClass(string MenuName)
        {
            string cls = string.Empty;
            switch (MenuName)
            {
                case "作品管理":
                    cls = "item1";
                    break;
                case "主编管理":
                    cls = "item2";
                    break;
                case "留言管理":
                    cls = "item3";
                    break;
                case "审核管理":
                    cls = "item4";
                    break;
                case "平台设置":
                    cls = "item5";
                    break;
                case "基本信息":
                    cls = "item6";
                    break;
            }
            return cls;
        }
    }
}
