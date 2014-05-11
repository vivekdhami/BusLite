namespace BusLite.InMemory
{
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemoryTopicClient : ITopicClient
    {
        private readonly Topic _topic;

        public InMemoryTopicClient(Topic topic)
        {
            _topic = topic;
        }

        public Task Send(BrokeredMessage message)
        {
            return _topic.Send(message);
        }
    }
}