namespace BusLite.InMemory
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public class InMemoryTopicClient : ITopicClient
    {
        public InMemoryTopicClient()
        {
        }

        public Task Send(BrokeredMessage message)
        {
            throw new NotImplementedException();
        }
    }
}