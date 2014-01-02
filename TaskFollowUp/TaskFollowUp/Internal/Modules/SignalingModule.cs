
using Autofac;
using TaskFollowUp.Internal.Signaling;

namespace TaskFollowUp.Internal.Modules
{
    public class SignalingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => SprintNotification.Instance)
                .As<SprintNotification>()
                .SingleInstance();
        }
    }
}