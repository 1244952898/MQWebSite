using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.common
{
    public static class FileHelper
    {
        public static string GetFileHash(string argFilePath)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            using (FileStream fs = new FileStream(argFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", "");
            }
        }

        public static bool DelFile(string filePath,out string result)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                result = "未获得文件地址";
                return false;
            }
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    result = "删除成功"; 
                    return true;
                }
                result = "该文件不存在";
                return true;
            }
            catch (Exception)
            {
                result = "删除文件g异常";
                return false;
            }
        }
    }
}
