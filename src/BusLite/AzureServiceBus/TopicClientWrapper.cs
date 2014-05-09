namespace BusLite.AzureServiceBus
{
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public class TopicClientWrapper : ITopicClient
    {
        private readonly TopicClient _inner;

        public TopicClientWrapper(TopicClient inner)
        {
            _inner = inner;
        }

        public Task Send(BrokeredMessage message)
        {
            return _inner.SendAsync(message);
        }
    }
}