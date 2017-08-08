using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using log4net;
using mq.application.common;
using mq.application.enumlib;
using mq.dataaccess.nosql;
using mq.model.viewentity;

namespace mq.ui.EmployeeWebSite.Controllers
{
    public class ImagesController : Controller
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(ImagesController));

        /// <summary>
        /// 存放mongo图片数据库
        /// </summary>
        private string MongoDbName_Test = "mq_bg_images_test";//机构logo

        /// <summary>
        /// mongodb数据库的名称和下标
        /// </summary>
        private string[] MongoDbNameArray = new string[]{
            ""//0：保留，暂时不能使用
            ,"mq_bg_images_test"//1：机构logo
            ,"mq_bg_"
          };

        #region 通用图片上传接口

        [HttpPost]
        [HttpGet]
        //[Route("Images/{type:int:range(1,20)}/Upload")]
        [OverrideActionFilters]
        public JsonResult UploadImage(int type)
        {
            if (HttpContext.Request.Files.Count > 0)
            {
                string mongodbName = MongoDbNameArray[type];
                if (string.IsNullOrEmpty(mongodbName))
                    return Json(new JsonBaseEntity { ErrorCode = "10000", ErrorMessage = "上传参数错误！" });

                try
                {
                    var file = HttpContext.Request.Files[0];
                    FileExtension[] fileEx = { FileExtension.BMP, FileExtension.JPG, FileExtension.GIF, FileExtension.PNG };
                    if (!FileValidation.IsAllowedExtension(file, fileEx))
                    {
                        return Json(new JsonBaseEntity { ErrorCode = "10000", ErrorMessage = "请上传JPG、JPEG、PNG、BMP格式的图片！" });
                    }

                    string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    newFileName = newFileName.ToUpper();

                    if (file.InputStream.Length > 1024 * 1024 * 1)//大于1M，就等比压缩
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);//将当前流的位置设置为初始位置。很重要，很重要，很重要！！！
                        Image uploaderImg = Image.FromStream(file.InputStream);
                        Image newImage = ImageHelper.UniformScaleDeflate(uploaderImg, 600, 600);// 压缩图片，保证图片的最大宽度为800px，高度按照比例等比压缩。
                        Stream newStream = ImageToByteHelper.ImgToStream(newImage);
                        MongoDBHelper.SetFileByName(mongodbName, newStream, newFileName);
                    }
                    else
                    {
                        MongoDBHelper.SetFileByName(mongodbName, file.InputStream, newFileName);
                    }

                    return Json(new JsonUploadFileEntity { ErrorCode = "10001", ErrorMessage = "上传成功", FileUrl = string.Format("{0}/TuPian/Images/{1}/Show/{2}", "", type, newFileName), FileName = newFileName });
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("function:{0}，location:{1},params:type={2},Message={3},StackTrace={4},Source={5}", "UploadImage", "MongoDBLegacyHelper.SetFileByName", type, ex.Message, ex.StackTrace, ex.Source);
                    return Json(new JsonBaseEntity { ErrorCode = "10000", ErrorMessage = "服务端异常" });
                }

            }
            else
                return Json(new JsonBaseEntity { ErrorCode = "10002", ErrorMessage = "请选择图片" });
        }

        #endregion


        #region 通用的获取图片地址
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">数据库索引下标</param>
        /// <param name="picid">图片文件的文件名，带扩展名</param>
        /// <returns></returns>
        //[Route("Images/{type:int:range(1,20)}/Show/{picid}")]//例子：Images/3/Show/2B158CDB-CDAC-4253-A686-A51AA269EF53.JPG，机构资质
        [AllowAnonymous]
        public ActionResult ShowImage(int type, string picid)
        {
            try
            {
                if (type > MongoDbNameArray.Length)
                {
                    return Json(new JsonBaseEntity { ErrorCode = "10000", ErrorMessage = "上传参数错误！" });
                }
                string mongodbName = MongoDbNameArray[type];
                if (string.IsNullOrEmpty(mongodbName))
                    return Json(new JsonBaseEntity { ErrorCode = "10000", ErrorMessage = "上传参数错误！" });

                if (!string.IsNullOrWhiteSpace(picid) && Regex.IsMatch(picid, @"[\w\W]+.[jpeg|gif|jpg|png|bmp|pic]", RegexOptions.IgnoreCase) && picid.Length > 32 && MongoDBHelper.ExistsFile(mongodbName, picid))
                {
                    try
                    {
                        byte[] filebytearray = MongoDBHelper.GetFileByteArrayByName(mongodbName, picid);
                        //return File(filebytearray, ContentTypeHelper.GetValue(Path.GetExtension(picid)), picid);//下载图片
                        return File(filebytearray, ContentTypeHelper.GetValue(Path.GetExtension(picid)));
                    }
                    catch (Exception ex)
                    {
                        logger.ErrorFormat("function:{0}，location:{1},params:id={2},Message={3},StackTrace={4},Source={5}", "GET", "MongoDBLegacyHelper.GetFileByteArrayByName", picid, ex.Message, ex.StackTrace, ex.Source);
                        return new EmptyResult();
                    }
                }
                else
                {
                    string filename = string.IsNullOrEmpty(picid) ? "beijing1.jpg" : picid;
                    if (filename.Length > 32)
                        filename = "beijing1.jpg";
                    return JavaScript("windows.loaction.href='" + string.Format("{0}/images/personalhomepage/big/{1}", CommonHelper.GetConfigValue("SourceAction"), filename) + "'");
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("function:{0}，location:{1},params:id={2},Message={3},StackTrace={4},Source={5}", "ShowImage", "MongoDBLegacyHelper.ExistsFile", picid, ex.Message, ex.StackTrace, ex.Source);
                return new EmptyResult();
            }
            finally
            {
            }
        }
        #endregion

        public FilePathResult DownloadFilePathResult()
        {
            string filename = @"D:\MQWebSite\PublicFiles\doc\1\1\1_6bb114cf8f424a0489abc172836bacdd.doc";
            return File(filename, "application/doc","");
        }

    }
}