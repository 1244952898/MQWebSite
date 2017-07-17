using System.Web.Optimization;
using mq.application.webmvc;

namespace MQWebSite
{
    public class BundleConfig
    {
        private static string _sourchPath = string.Empty;
        private static string SourchPath
        {
            get
            {
                if (string.IsNullOrEmpty(_sourchPath))
                    _sourchPath = DomainUrlHelper.SourchPath;
                if (!string.IsNullOrWhiteSpace(_sourchPath))
                {
                    _sourchPath = _sourchPath.TrimEnd(new char[] { '/' });
                }
                return BundleConfig._sourchPath;
            }
        }
        private static string _sourchTempPath = string.Empty;
        private static string SourchTempPath
        {
            get
            {
                if (string.IsNullOrEmpty(_sourchTempPath))
                    _sourchTempPath = DomainUrlHelper.SourchTempPath;
                if (!string.IsNullOrWhiteSpace(_sourchTempPath))
                {
                    _sourchTempPath = _sourchTempPath.TrimEnd(new char[] { '/' });
                }
                return BundleConfig._sourchTempPath;
            }
        }

        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string sourchPath = SourchPath;
            string sourceTempPath = SourchTempPath;
            BundleTable.EnableOptimizations = true;//捆绑压缩CDN资源 
            bundles.UseCdn = true;

            #region StyleBundle
            bundles.Add(new StyleBundle("~/css/BuildStyle", string.Format("{0}/css/BuildStyle.css", sourchPath)).Include("~/css/BuildStyle.css"));
            bundles.Add(new StyleBundle("~/css/style", string.Format("{0}/css/style.css", sourchPath)).Include("~/css/style.css"));
            #endregion

            #region StyleBundle
            bundles.Add(new ScriptBundle("~/js/jquery", string.Format("{0}/js/jquery-3.2.1.min.js", sourchPath)).Include("~/js/jquery-3.2.1.min.js"));
            bundles.Add(new ScriptBundle("~/js/building", string.Format("{0}/js/preloader.js", sourchPath)).Include("~/js/preloader.js"));
            bundles.Add(new ScriptBundle("~/js/css_browser_selector", string.Format("{0}/js/css_browser_selector.js", sourchPath)).Include("~/js/css_browser_selector.js"));
            bundles.Add(new ScriptBundle("~/js/plax", string.Format("{0}/js/plax.js", sourchPath)).Include("~/js/plax.js"));
            bundles.Add(new ScriptBundle("~/js/jquery.spritely", string.Format("{0}/js/jquery.spritely-0.6.1.js", sourchPath)).Include("~/js/jquery.spritely-0.6.1.js"));
            bundles.Add(new ScriptBundle("~/js/jquery-animate-css-rotate-scale", string.Format("{0}/js/jquery-animate-css-rotate-scale.js", sourchPath)).Include("~/js/jquery-animate-css-rotate-scale.js"));
            bundles.Add(new ScriptBundle("~/js/script", string.Format("{0}/js/script.js", sourchPath)).Include("~/js/script.js"));

            bundles.Add(new ScriptBundle("~/js/NSW_Index", string.Format("{0}/js/NSW_Index.js", sourchPath)).Include("~/js/NSW_Index.js"));
            bundles.Add(new ScriptBundle("~/js/rollup.min", string.Format("{0}/js/rollup.min.js", sourchPath)).Include("~/js/rollup.min.js"));
            bundles.Add(new ScriptBundle("~/js/jquery.flexslider-min", string.Format("{0}/js/jquery.flexslider-min.js", sourchPath)).Include("~/js/jquery.flexslider-min.js"));
            #endregion
        }
    }
}