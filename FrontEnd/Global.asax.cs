using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FrontEnd
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using Infrastructure.Windsor;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        #region Container Bootstrapper

        private static IWindsorContainer _container;
        private static void BootstrapContainer()
        {
            _container = new WindsorContainer();
            _container.Kernel.Resolver.AddSubResolver(new AppSettingsConvention());
            _container.Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        #endregion Container Bootstrapper

        protected void Application_End()
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}