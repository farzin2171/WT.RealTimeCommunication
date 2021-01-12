using System;
using System.Threading;
using WT.RealTime.Domain.Models;

namespace WT.MessageBrokers
{
    public sealed class MessageReceivedEventArgs:EventArgs
    {
        public Message Message { get; }
        public string AcknowledgeToken { get; }
        public CancellationToken CancellationToken { get; }

        public MessageReceivedEventArgs(Message message, string acknowledgeToken, CancellationToken cancellationToken)
        {
            Message = message;
            AcknowledgeToken = acknowledgeToken;
            CancellationToken = cancellationToken;
        }
    }
}
