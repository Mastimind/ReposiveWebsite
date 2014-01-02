using System;
using SignalR;
using TaskFollowUp.Models;

namespace TaskFollowUp.Internal.Signaling
{
    public class SprintNotification
    {
        private readonly static Lazy<SprintNotification> LocalInstance = new Lazy<SprintNotification>(() => new SprintNotification());

        public static SprintNotification Instance
        {
            get
            {
                return LocalInstance.Value;
            }
        }

        public void NotifyRefresh(SprintModel model)
        {
            var clients = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<SprintHub>();
            clients.Clients.refresh(model);
        }

        public void NotifyDelete(SprintModel model)
        {
            var clients = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<SprintHub>();
            clients.Clients.remove(model);
        }
    }
}