using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace mq.ui.WageAttendance
{
    public static class AutoFacConfig
    {
        public static void Register()
        {
            #region 写法2
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;

            Assembly repositoryAss = Assembly.Load("mq.dataaccess.sql");
            Assembly servicesAss = Assembly.Load("mq.application.service");
            builder.RegisterAssemblyTypes(repositoryAss).Where(n => n.Name.EndsWith("Repository")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(servicesAss).Where(n => n.Name.EndsWith("Service")).AsImplementedInterfaces();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);//注册api容器需要使用HttpConfiguration对象
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

        }
    }
}