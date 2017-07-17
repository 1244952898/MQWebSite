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
    }
}
