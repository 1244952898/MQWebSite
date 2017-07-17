using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.application.common
{
    public class IOHelp
    {
        private static string _strIsWriteLog = System.Configuration.ConfigurationManager.AppSettings["IsWriteLog"];
        private static string _LogRootSavePath = System.Configuration.ConfigurationManager.AppSettings["LogRootSavePath"];
        public static int IntWriteLog
        {
            get
            {
                int ilog = 0;
                int.TryParse(_strIsWriteLog, out ilog);
                return ilog;
            }
        }
        public static bool IsWriteLog
        {
            get
            {
                if (string.IsNullOrEmpty(_strIsWriteLog) || IntWriteLog <= 0)
                    return false;
                else
                    return true;
            }
        }


        #region 写出日志文件
        public static void WriteLog(string content, string type = null, System.Text.Encoding encoding = null)
        {
            if (!IsWriteLog || string.IsNullOrEmpty(content))
                return;
            try
            {
                string strPath = _LogRootSavePath;
                if (string.IsNullOrEmpty(strPath))
                {
                    if (System.Web.HttpContext.Current == null)
                    {
                        strPath = System.Environment.CurrentDirectory.TrimEnd('\\');
                    }
                    else
                    {
                        strPath = System.Web.HttpContext.Current.Server.MapPath("~");
                    }
                }
                strPath = string.Format("{0}\\logs\\{1}_{2}.txt", strPath.TrimEnd('\\'), DateTime.Now.ToString("yyyyMMdd"), type);
                WriteFile(strPath, string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), content), true, encoding);
            }
            catch (Exception ex)
            { }
        }

        public static void WriteLog(string content, int iLog, string type = null, System.Text.Encoding encoding = null)
        {
            if (iLog > IntWriteLog)
                return;
            try
            {
                string strPath = _LogRootSavePath;
                if (string.IsNullOrEmpty(strPath))
                {
                    if (System.Web.HttpContext.Current == null)
                    {
                        strPath = System.Environment.CurrentDirectory.TrimEnd('\\');
                    }
                    else
                    {
                        strPath = System.Web.HttpContext.Current.Server.MapPath("~");
                    }
                }
                strPath = string.Format("{0}\\logs\\{1}_{2}.txt", strPath.TrimEnd('\\'), DateTime.Now.ToString("yyyyMMdd"), type);
                WriteFile(strPath, string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), content), true, encoding);
            }
            catch (Exception ex)
            { }
        }
        #endregion


        #region 写出文件
        public static void WriteFile(string path, string content, bool isAppend, System.Text.Encoding encoding = null)
        {
            string temp = @"\";
            string strOutFileDirectory = path.Substring(0, path.LastIndexOf(temp));
            if (!System.IO.Directory.Exists(strOutFileDirectory))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(strOutFileDirectory);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            try
            {
                if (isAppend)
                {
                    if (!File.Exists(path))
                        CreateFile(path, content);
                    else
                    {
                        encoding = encoding == null ? System.Text.Encoding.UTF8 : encoding;
                        using (StreamWriter sw = new StreamWriter(path, true, encoding))
                        {
                            sw.WriteLine(content);
                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
                else
                    CreateFile(path, content);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public static void CreateFile(string path, string content, System.Text.Encoding encoding = null)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                encoding = encoding == null ? System.Text.Encoding.UTF8 : encoding;
                using (StreamWriter sw = new StreamWriter(path, false, encoding))
                {
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 读取文件
        public static string ReadFile(string path)
        {
            string strResult = "";
            try
            {
                FileInfo file = new FileInfo(path);
                StreamReader sr = file.OpenText();
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {

            }

            return strResult;
        }
        public static string ReadFile(string filePath, string linefeed, System.Text.Encoding encoding = null)
        {
            StringBuilder sb = new StringBuilder();
            encoding = encoding == null ? System.Text.Encoding.UTF8 : encoding;
            linefeed = linefeed == null ? string.Empty : linefeed;
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath, encoding))
                    {
                        while (!sr.EndOfStream)
                        {
                            sb.AppendFormat("{0}{1}", sr.ReadLine(), linefeed);
                        }
                        sr.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
            return sb.ToString();
        }
        #endregion

        #region 保存XML
        public static void WriteXml(System.Xml.Linq.XDocument xdocument, string strXmlFilePath)
        {
            WriteXml(xdocument, strXmlFilePath, Encoding.UTF8, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdocument"></param>
        /// <param name="strXmlFilePath"></param>
        /// <param name="encoding"></param>
        /// <param name="declaration">编写 XML 声明</param>
        /// <param name="indent">该值指示是否缩进元素</param>
        public static void WriteXml(System.Xml.Linq.XDocument xdocument, string strXmlFilePath, Encoding encoding, bool indent)
        {
            StringBuilder sb = new StringBuilder();
            System.Xml.XmlWriterSettings xws = new System.Xml.XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Encoding = encoding;
            xws.Indent = indent;
            try
            {
                sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sb, xws))
                {
                    xdocument.Save(xw);
                }
                using (StreamWriter sw = new StreamWriter(strXmlFilePath))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion
    }
}
