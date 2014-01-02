using System.Threading.Tasks;
using SignalR.Hubs;

namespace TaskFollowUp.Internal.Signaling
{
    [HubName("sprintHub")]
    public class SprintHub : Hub, IDisconnect
    {
        private readonly SprintNotification _sprints;

        public SprintHub(SprintNotification sprints)
        {
            _sprints = sprints;
        }

        public SprintHub()
            : this(SprintNotification.Instance)
        {
        }

        public System.Threading.Tasks.Task Disconnect()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        public void Login(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }

    }
}