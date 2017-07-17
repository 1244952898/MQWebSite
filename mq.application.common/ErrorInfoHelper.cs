using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace match.application.common
{
    public class ErrorInfoHelper
    {
        private static string iniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\ErrorCode.ini");
        private static string sectionName = "ErrorMessage";
        private static INIHelper mINIHelper = null;
        
        public static string GetErrorValue(string key)
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
