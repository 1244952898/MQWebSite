using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mq.application.enumlib;

namespace mq.application.common
{
    public static class OriginalSaveHelper
    {
        static readonly string PublicFileCollectionRoot = CommonHelper.GetConfigValue("PublicFileUploadDocumentRoot").ToString("");
        static readonly string ActiveFileCollectionRoot = CommonHelper.GetConfigValue("ActiveFileUploadDocumentRoot").ToString("");
        static readonly string GuideFileCollectionRoot = CommonHelper.GetConfigValue("GuideFileUploadDocumentRoot").ToString("");

        /// <summary>
        /// 生成保存的文件名
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="filename">文件名中不能有扩展名</param>
        /// <param name="fileExt">扩展名不能为空</param>
        /// <returns></returns>
        public static string CreateNewSaveFileName(string pre, string filename, string fileExt)
        {
            return string.Format("{0}_{1}.{2}", pre, filename, fileExt);
        }

        /// <summary>
        /// 获取原创文件存储路径
        /// </summary>
        /// <param name="filename">文件名的扩展名可有可无</param>
        /// <param name="fileEx">扩展名最好不为空,默认为doc</param>
        /// <param name="type">上传类别 用户素材：OrignalTypeEnum:</param>
        /// <returns></returns>
        public static string GetSavePath(string filename, string fileEx, string type = "PublicFile")
        {
            //D:\ZhiKuSites\document\doc\14\14_32609c222ebf497997c216862f4840b2.doc
            if (string.IsNullOrWhiteSpace(filename))
                return string.Empty;
            string strRoot = string.Empty;
            type = type ?? string.Empty;


            //多个类型文件上传存放位置
            if (type.Equals(OrignalTypeEnum.ActiveFile.ToString(), StringComparison.InvariantCultureIgnoreCase) || type == "1")
            {
                strRoot = ActiveFileCollectionRoot.TrimEnd('\\');
            }
            else if (type.Equals(OrignalTypeEnum.GuideFile.ToString(), StringComparison.InvariantCultureIgnoreCase) || type == "2")
            {
                strRoot = GuideFileCollectionRoot.TrimEnd('\\');
            }
            else
            {
                //默认放到这个下面
                strRoot = PublicFileCollectionRoot.TrimEnd('\\');
            }
            if (!string.IsNullOrWhiteSpace(fileEx))
            {
                strRoot += @"\" + fileEx;
            }
            if (filename.IndexOf("_") > 0)
            {
                long lPre = 0;
                if (!Int64.TryParse(filename.Substring(0, filename.IndexOf("_")), out lPre))
                    lPre = 0;
                strRoot += string.Format(@"\{0}\{1}", lPre % 1000, lPre);
            }
            return strRoot;
        }
    }
}
