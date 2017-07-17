using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.dataaccess.nosql
{
    public class MemcachedHelper
    {
        private static string CacheServers = GetConfigValue("CacheServers");
        //private static MemcachedClient _client = null;
        //private static MemcachedClient CreateClient()
        //{
        //    if (_client == null)
        //    {
        //        _client = new MemcachedClient();
        //    }
        //    return _client;
        //}
        //public static void SetValue(string key, object value)
        //{
        //    MemcachedClient mc = CreateClient();
        //    mc.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, DateTime.Now.AddHours(6));
        //}

        //public static void SetLargeValue(string key, string value)
        //{
        //    if (value.Length > 10000)
        //    {
        //        int index = 0;
        //        string subString = value.Substring(0, 10000);
        //        while (subString.Length > 0)
        //        {
        //            SetValue(key + "_" + index, subString);
        //            if (subString.Length == 10000)
        //            {
        //                index++;
        //                if ((10000 * (index + 1)) > value.Length)
        //                {
        //                    subString = value.Substring(10000 * index);
        //                }
        //                else
        //                {
        //                    subString = value.Substring(10000 * index, 10000);
        //                }
        //            }
        //            else
        //            {
        //                subString = "";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        SetValue(key, value);
        //    }
        //}
        //public static string GetLargeValue(string key)
        //{
        //    int index = 0;
        //    object objValue = GetValue(key + "_" + index);
        //    string value = "";
        //    while (objValue != null)
        //    {
        //        value += objValue.ToString();
        //        index++;
        //        objValue = GetValue(key + "_" + index);
        //    }
        //    return value;
        //}
        //public static object GetValue(string key)
        //{
        //    object value = null;
        //    MemcachedClient mc = CreateClient();
        //    mc.TryGet(key, out value);
        //    return value;
        //}

        //public static void DelValue(string key)
        //{
        //    if (string.IsNullOrEmpty(key))
        //        return;
        //    MemcachedClient mc = CreateClient();
        //    mc.Remove(key);
        //}
        public static string GetConfigValue(string sKey)
        {
            string sValue = null;
            if ((sValue = System.Configuration.ConfigurationManager.AppSettings[sKey]) == null)
            {
                sValue = "";
            }
            return sValue;
        }
    }
}
