using System;

namespace WT.RealTime.Domain.Exceptions
{
    public sealed class MessageBrokerTypeNotSupportedException:Exception
    {
        public MessageBrokerTypeNotSupportedException() { }
        public MessageBrokerTypeNotSupportedException(string message) : base(message) { }
        public MessageBrokerTypeNotSupportedException(string message, Exception inner) : base(message, inner) { }
        private MessageBrokerTypeNotSupportedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
