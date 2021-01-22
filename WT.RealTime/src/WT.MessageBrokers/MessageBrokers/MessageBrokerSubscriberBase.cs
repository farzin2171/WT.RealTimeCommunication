using System;
using System.Threading.Tasks;

namespace WT.MessageBrokers
{
    public abstract class MessageBrokerSubscriberBase : IDisposable
    {

        public void Subscribe(Func<MessageReceivedEventArgs,Task> receiveCallback)
        {
            SubscribeCore(receiveCallback);
        }

        public void Acknowledge(string acknowledgetoken)
        {
            AcknowledgeCore(acknowledgetoken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void SubscribeCore(Func<MessageReceivedEventArgs,Task> recivedCallbask);
        protected abstract void AcknowledgeCore(string acknowledgetoken);
        protected abstract void Dispose(bool disposing);
    }
}
