namespace BusLite.InMemory
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public class InMemorySubscriptionClient : ISubscriptionClient
    {
        public Task<BrokeredMessage> Receive()
        {
            throw new NotImplementedException();
        }
    }
}