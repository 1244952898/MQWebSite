using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace match.application.common
{
    public class INIHelper
    {
        private string _path="";
        public INIHelper(string path) {
            _path = path;
        }
        #region 读取和写入ini文件的操作
        
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 读取配置ini文件
        /// </summary>
        /// <param name="Section">配置段</param>
        /// <param name="Key">键</param>
        /// <param name="innpath">存放物理路径</param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            GetPrivateProfileString(Section, Key, "", temp, 500, _path);
            return temp.ToString();
        }

        /// <summary>
        /// 写入ini文件的操作
        /// </summary>
        /// <param name="Section">配置段</param>
        /// <param name="Key">键</param>
        /// <param name="Value">键值</param>
        /// <param name="inipath">物理路径</param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, _path);
        }
        #endregion
    }
}
