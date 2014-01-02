using System;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace TaskFollowUp.Internal.Modules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(ThisAssembly);
            builder.RegisterApiControllers(ThisAssembly);

            builder.RegisterType<PathFinder>().AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterType<FormsAuthenticationService>().AsImplementedInterfaces().InstancePerHttpRequest();
            builder.Register<Func<DateTime>>(x => () => DateTime.Now).As<Func<DateTime>>().SingleInstance();
        }
    }
}