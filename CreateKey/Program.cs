using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;

namespace CreateKey
{
    class Program
    {
        static void Main(string[] args)
        {
            Product p=new Product();

            p.Validate2();

            int b = 32;
            ThreadStart pts = new ThreadStart(() =>
            {
                for (int i = 0; i < b; i++)
                {
                    Console.WriteLine("TTTT:" + i);
                }
            });
            Thread t = new Thread(pts);
            t.Start();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("M:" + i);
            }
            Console.ReadLine();
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //using (StreamWriter writer = new StreamWriter("PrivateKey.xml"))  //这个文件要保密...
            //{
            //    writer.WriteLine(rsa.ToXmlString(true));
            //}
            //using (StreamWriter writer = new StreamWriter("PublicKey.xml"))
            //{
            //    writer.WriteLine(rsa.ToXmlString(false));
            //}
        }

        private static void A(object o)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(o+"T:"+i);
            }
        }
    }

    public class Num
    {
        private int A;

        public Num(int a)
        {
            this.A = a;
        }

        public void B()
        {
            for (int i = 0; i < A; i++)
            {
                Console.WriteLine("T:" + i);
            }
        }
    }
}
