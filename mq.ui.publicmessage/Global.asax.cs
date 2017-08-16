using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using koala.application.common;
using log4net;
using mq.application.common;
using mq.application.webmvc;
using mq.ui.EmployeeWebSite;
using MQWebSite;
using MQWebSite.App_Start;
using System.Web.Http;
using System.Web.Optimization;

namespace mq.ui.publicmessage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILog logger = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            //独立的log4net.config
            //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            log4net.Config.XmlConfigurator.Configure();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //控制器工厂替换成AutoFac的工厂
            AutofacConfig.Register();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //过滤非法请求或攻击
            if (HttpContext.Current.Request.UserAgent != null && (HttpContext.Current.Request.UserAgent.ToLower().Contains("spider") || HttpContext.Current.Request.UserAgent.ToLower().Contains("yahoo")) && (HttpContext.Current.Request.Url.ToString().ToLower().Contains("/web/searchcollection") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("/web/searcharticle"))) //Web/SearchArticle
            {
                Response.Clear();
                Response.StatusCode = 404;
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Type", "text/html; charset=utf-8");
                Response.Write("别抓了");
                Response.End();
            }

            //
            string AttackContent = string.Empty;
            if (CommonHelper.ValidUrlGetData(out AttackContent) || CommonHelper.ValidUrlPostData(out AttackContent))
            {
                int count = 1;
                Response.Clear();
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Type", "text/html; charset=utf-8");
                Response.Write(string.Format("已非法攻击{0}次,已被记录在案！", count));
                Response.End();
            }

            //对无需登录页面返回
            if (CommonHelper.ExcludeUrl(Request.Url.ToString().ToLower()))
                return;

            if (LoginHelper.isOnline())
            {
                //这里需要权限验证，打开页面是否有权限

                //int pageId = CommonHelper.GetPostValue("cid").ToInt(0);
                //if (pageId > 0)
                //{
                //    if (!PermissionUtils.Right(pageId))
                //    {
                //        if (Request.AcceptTypes != null && Request.AcceptTypes.Any(t => t.Contains("application/json")))
                //        {
                //            Response.Clear();
                //            Response.ContentEncoding = System.Text.Encoding.UTF8;
                //            Response.AddHeader("Content-Type", "application/json; charset=utf-8");
                //            Response.Write("{\"ErrorCode\":\"E1001\",\"ErrorMesssage\":\"您没有权限！\"}");
                //            Response.End();
                //        }
                //        else
                //        {
                //            Response.Redirect(UrlHelper.GenerateUrl("default", "error", "home", null, RouteTable.Routes, Request.RequestContext, true));
                //        }
                //    }
                //}

            }
            else
            {
                if (Request.AcceptTypes != null && Request.AcceptTypes.Any(t => t.Contains("application/json")))
                {
                    Response.Clear();
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("Content-Type", "application/json; charset=utf-8");
                    Response.Write("{\"ErrorCode\":\"E1001\",\"ErrorMesssage\":\"登录超时\"}");
                    Response.End();
                }
                else
                {
                    string url = DomainUrlHelper.PublicMessagePath + "/login/login";
                    //UrlHelper.GenerateUrl("Login", "login", "home", null, RouteTable.Routes, Request.RequestContext, true)
                    Response.Redirect(url);
                }
            }

            #region 登录验证

            //过滤非法请求或攻击
            //string AttackContent = string.Empty;
            //if (CommonHelper.ValidUrlGetData(out AttackContent) || CommonHelper.ValidUrlPostData(out AttackContent))
            //{
            //    int count = 1;
            //    //count = wucssc.AttackLog(IPHelper.GetLoginIp(Request), AttackContent, "个人空间");
            //    Response.Clear();
            //    //Response.StatusCode = 404;
            //    Response.ContentEncoding = System.Text.Encoding.UTF8;
            //    Response.AddHeader("Content-Type", "text/html; charset=utf-8");
            //    Response.Write(string.Format("已非法攻击{0}次,已被记录在案！", count));
            //    Response.End();
            //}

            //图片上传的
            //koala.application.common.UploadifyHelper.ForUploadify();

            /*
            if (CommonHelper.ExcludeUrl(Request.Url.ToString().ToLower()))
                return;

            string mToken = CommonHelper.GetPostValue("token");
            logger.InfoFormat("授权：{0};当前Cookies中的用户名{1};用户ID：{2}", mToken, LoginHelper.UserName, LoginHelper.UserId);
            if (!LoginHelper.isOnline())
            {
                logger.InfoFormat("当前授权：{0};Cookies中的授权{1};", mToken, LoginHelper.Token);
                if (string.IsNullOrEmpty(mToken))
                {
                    mToken = CookieHelper.GetCookie(CommonVariables.LoginSessionSID, false);
                }
                if (!string.IsNullOrEmpty(mToken))
                {
                    UserInifoEntity mResult = null;
                    if (!string.IsNullOrEmpty(mToken))
                    {
                        string ip = IPHelper.GetLoginIp();
                        mResult = wssc.UserTokenFromToken(mToken, ip);
                    }
                    if (null != mResult)
                    {
                        logger.InfoFormat("当前授权：{0};授权用户ID:{1};授权用户名:{2};授权真实用户名:{3};", mToken, mResult.UserID, mResult.UserName, mResult.RealName);
                        LoginHelper.setSession(mToken);
                        logger.InfoFormat("当前授权：{0};更新后：授权用户ID:{1};授权用户名:{2};授权真实用户名:{3};", mToken, LoginHelper.UserId, LoginHelper.UserName, LoginHelper.UserRealName);
                        if (HttpContext.Current.Request.Cookies != null)
                        {
                            HttpCookieCollection collection = HttpContext.Current.Request.Cookies;
                            for (int i = 0; i < collection.AllKeys.Count(); i++)
                            {
                                HttpCookie cookie = collection[i];
                                logger.InfoFormat("当前授权：{0};Cookies中的值Key={1};value={2};domain={3}", mToken, cookie.Name, System.Web.HttpUtility.UrlDecode(cookie.Value), cookie.Domain);
                            }
                        }
                    }
                    else
                    {
                        logger.Info("获取授权内容为空");
                        wssc.UserLogout(mToken, LoginHelper.UID, IPHelper.GetLoginIp());
                        LoginHelper.Logout();
                        LoginHelper.Logout2();
                        CommonHelper.CheckPostToken(Request.UrlReferrer, LoginUrl, AppCode);
                        LoginHelper.CallBackUrl(Request, LoginUrl, AppCode);
                    }
                }
                else
                {
                    LoginHelper.CallBackUrl(Request, LoginUrl, AppCode);
                }
            }*/

            #endregion


        }
    }
}
