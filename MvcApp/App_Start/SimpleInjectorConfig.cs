using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DependencyResolver;
using System.Reflection;
using SimpleInjector.Integration.Web.Mvc;
using BLL.Interfaces.Services;
using NLog;

namespace MvcApp.App_Start
{
    /// <summary>
    ///  Provides methods to configurate dependency injection
    /// </summary>
    public static class SimpleInjectorConfig
    {
        /// <summary>
        /// Configures the DI container.
        /// </summary>
        public static void ConfigureContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.ConfigureResolver();
            container.RegisterSingleton<ILogger>(LogManager.GetCurrentClassLogger());
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}