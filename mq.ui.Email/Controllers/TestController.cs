using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace mq.ui.Email.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult A()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            
            string file = @"F:\MQWebSite\MQWebSite.git\trunk\mq.ui.resource\Images\404.png";
            MailMessage message = new MailMessage("1244952898@qq.com", "18710098386@sina.cn", "title", "body");
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this e-mail message.
            message.Attachments.Add(data);
            //Send the message.
            SmtpClient client = new SmtpClient("smtp.qq.com");
            // Add credentials if the SMTP server requires them.
            client.EnableSsl = true;  
            client.UseDefaultCredentials = false;
           // client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式      
            client.Credentials = new System.Net.NetworkCredential("1244952898@qq.com", "naapvvgtiwhlfhgd");
       
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                            ex.ToString());
            }
            // Display the values in the ContentDisposition for the attachment.
            ContentDisposition cd = data.ContentDisposition;
            Console.WriteLine("Content disposition");
            Console.WriteLine(cd.ToString());
            Console.WriteLine("File {0}", cd.FileName);
            Console.WriteLine("Size {0}", cd.Size);
            Console.WriteLine("Creation {0}", cd.CreationDate);
            Console.WriteLine("Modification {0}", cd.ModificationDate);
            Console.WriteLine("Read {0}", cd.ReadDate);
            Console.WriteLine("Inline {0}", cd.Inline);
            Console.WriteLine("Parameters: {0}", cd.Parameters.Count);
            foreach (DictionaryEntry d in cd.Parameters)
            {
                Console.WriteLine("{0} = {1}", d.Key, d.Value);
            }
            data.Dispose();
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult NewMain()
        {
            return View();
        }

        public ActionResult NewMain1()
        {
            return View();
        }  
    }
}