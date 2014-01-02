using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using TaskFollowUp.Internal.Modules;
using ZeroProximity.DeviceDetection;

namespace TaskFollowUp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IMobileDeviceDetection _mobileDeviceDetection = DeviceDetectionFactory.GetDefaultImplementation();
        protected void Application_Start()
        {
            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("Mobile")
            {
                ContextCondition = (context => _mobileDeviceDetection.Match(context.GetOverriddenUserAgent()).IsMobile)
            });

            AreaRegistration.RegisterAllAreas();


            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterModule<WebModule>();
            builder.RegisterModule<SignalingModule>();
            builder.RegisterModule<RepositoryModule>();

            var container = builder.Build();
            var autofacDependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(autofacDependencyResolver);
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static DateTime LastModified
        {
            get
            {
                var webAssembly = Assembly.GetExecutingAssembly();
                var fileInfo = new FileInfo(webAssembly.Location);
                var utc = fileInfo.LastWriteTimeUtc;
                return new DateTime(utc.Year, utc.Month, utc.Day, utc.Hour, utc.Minute, utc.Second, 0, DateTimeKind.Utc);
            }
        }
    }
}