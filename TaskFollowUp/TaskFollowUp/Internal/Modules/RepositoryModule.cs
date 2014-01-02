using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using TaskFollowUp.Data;

namespace TaskFollowUp.Internal.Modules
{
    public class RepositoryModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Database.SetInitializer(new WorkflowEngineContextCreator());
            builder.RegisterType<SprintRepository>()
                .AsSelf()
                .As<IRepository>()
                .WithParameter("context", null)
                .InstancePerHttpRequest();
        }
    }
}