using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisDemo
{
    static class Program
    {
        static void Main(string[] args)
        {
            string key = "a";
            string str = "aaaaaaa";
            bool result = StackExchangeRedisHelper.StringSet(key, str);

            string gtstr = StackExchangeRedisHelper.StringGet(key);
            Console.WriteLine(gtstr);
            Console.ReadLine();
        }
    }
}
