using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WT.RealTime.Dashboard.Infrastructure.Hubs
{
    public class MessageBrokerHub: Hub
    {
        public Task ReciveMessage(string message)
        {
            return Task.CompletedTask;
        }
    }
}
