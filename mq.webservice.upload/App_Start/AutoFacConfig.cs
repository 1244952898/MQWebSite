using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
namespace MQWebSite.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            #region
            //var builder = new ContainerBuilder();
            //var config = GlobalConfiguration.Configuration;
            //var baseType = typeof(IDependency);
            //var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            ////下面这种写法  在项目重新生成时就报错 
            ////var assemblys =AppDomain.CurrentDomain.GetAssemblies().ToList();
            //builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => baseType.IsAssignableFrom(t) && t != baseType).AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
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


            #region 写法3
            //var builder = new ContainerBuilder();
            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //Assembly repositoryAss = Assembly.Load("mq.dataaccess.sql");
            //Assembly servicesAss = Assembly.Load("mq.application.service");
            //Assembly controllerAss = Assembly.Load("mq.ui.EmployeeWebSite");

            //builder.RegisterAssemblyTypes(repositoryAss).Where(n => n.Name.EndsWith("Repository")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(servicesAss).Where(n => n.Name.EndsWith("Service")).AsImplementedInterfaces();
            //builder.RegisterControllers(controllerAss);
            //builder.RegisterApiControllers(controllerAss);

            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            //Assembly controllerAss = Assembly.Load("mq.ui.EmployeeWebSite");
            ////Type[] rtypes = repositoryAss.GetTypes();
            ////builder.RegisterTypes(rtypes).Where(n => n.Name.EndsWith("RepositoryBase")).AsImplementedInterfaces(); ;
            //builder.RegisterAssemblyTypes(repositoryAss).Where(n => n.Name.EndsWith("Service")).AsImplementedInterfaces();
            ////Type[] stypes = servicesAss.GetTypes();
            ////builder.RegisterTypes(stypes).Where(n => n.Name.EndsWith("Service")).AsImplementedInterfaces(); ;
            //builder.RegisterAssemblyTypes(servicesAss).Where(n => n.Name.EndsWith("Service")).AsImplementedInterfaces();
            //builder.RegisterControllers(controllerAss);
            //builder.RegisterApiControllers(controllerAss);

            //var container = builder.Build();
            ////config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//注册mvc 容器 


            ////DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
        }
    }
}