using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using match.application.common;

namespace mq.application.common
{
    /// <summary>
    /// 根据文件扩展名获取多媒体数据的MIME类型
    /// MIME类型就是设定某种扩展名的文件用一种应用程序来打开的方式类型，当该扩展名文件被访问的时候，浏览器会自动使用指定应用程序来打开。
    /// 多用于指定一些客户端自定义的文件名，以及一些媒体文件打开方式。 　　
    /// </summary>
    public class ContentTypeHelper
    {
        private static string iniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\ErrorCode.ini");
        private static string sectionName = "ContentType";
        private static INIHelper mINIHelper = null;

        public static string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "未定义的错误";
            if (!File.Exists(iniPath))
                return "WEB系统配置错误";
            if (mINIHelper == null)
                mINIHelper = new INIHelper(iniPath);
            return mINIHelper.IniReadValue(sectionName, key);
        }

    }
}
