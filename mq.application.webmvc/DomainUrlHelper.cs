using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.application.common;

namespace mq.application.webmvc
{
    public class DomainUrlHelper
    {
        private static string _sourchPath = string.Empty;
        public static string SourchPath
        {
            get
            {
                if (string.IsNullOrEmpty(_sourchPath))
                    _sourchPath = CommonHelper.GetConfigValue("SourceAction").TrimEnd('/');//
                return _sourchPath;
            }
        }

        private static string _sourchTempPath = string.Empty;

        public static string SourchTempPath
        {
            get
            {
                if (string.IsNullOrEmpty(_sourchTempPath))
                    _sourchTempPath = CommonHelper.GetConfigValue("SourceTempAction").TrimEnd('/');//
                return _sourchTempPath;
            }
        }

        private static string _mqWebSiteBg = string.Empty;
        public static string MqWebSiteBg
        {
            get
            {
                if (string.IsNullOrEmpty(_mqWebSiteBg))
                    _mqWebSiteBg = CommonHelper.GetConfigValue("MqWebSiteBgAction").TrimEnd('/');//
                return _mqWebSiteBg;
            }
        }

        private static string _uploadPath = string.Empty;
        public static string UploadPath
        {
            get
            {
                if (string.IsNullOrEmpty(_uploadPath))
                    _uploadPath = CommonHelper.GetConfigValue("UploadPathAction").TrimEnd('/');
                return _uploadPath;
            }
        }

        private static string _employeeBgPath = string.Empty;
        public static string EmployeeBgPath
        {
            get
            {
                if (string.IsNullOrEmpty(_employeeBgPath))
                    _employeeBgPath = CommonHelper.GetConfigValue("EmployeeBgPathAction").TrimEnd('/');
                return _employeeBgPath;
            }
        }



        private static string _publicMessagePath = string.Empty;
        public static string PublicMessagePath
        {
            get
            {
                if (string.IsNullOrEmpty(_publicMessagePath))
                    _publicMessagePath = CommonHelper.GetConfigValue("PublicMessagePathAction").TrimEnd('/');
                return _publicMessagePath;
            }
        }


    }
}
