Visual Studio 已向项目“AngularJsDemo”添加 ASP.NET Web API 2 的 全部集合 个依赖项。

项目中的 Global.asax.cs 文件可能需要其他更改才能启用 ASP.NET Web API。

1. 添加以下命名空间引用:

    using System.Web.Http;
    using System.Web.Routing;

2. 如果代码尚未定义 Application_Start 方法，请添加以下方法:

    protected void Application_Start()
    {
    }

3. 在 Application_Start 方法的开头添加以下行:

    GlobalConfiguration.Configure(WebApiConfig.Register);