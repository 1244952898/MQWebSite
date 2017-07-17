using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using koala.application.common;

namespace mq.application.common
{
    public class EncryptHelper
    {
        //public static string getSignParam()
        //{
        //    string timestamp = DateTime.Now.Ticks.ToString();
        //    string strString = LoginHelper.SecretKey;
        //    string sign = Sign(string.Format("{0}-$-{1}", strString, timestamp));
        //    return "sign=" + sign + "*" + timestamp;
        //}

        //public static string Sign(string strString)
        //{
        //    string des = DES3Encrypt(strString, encryptKey);
        //    des = Md5(des);
        //    return des.ToLower();
        //}

    }
}
