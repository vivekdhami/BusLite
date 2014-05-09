namespace BusLite.AzureServiceBus
{
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public class SubscriptionClientWrapper : ISubscriptionClient
    {
        private readonly SubscriptionClient _inner;

        public SubscriptionClientWrapper(SubscriptionClient inner)
        {
            _inner = inner;
        }

        public Task<BrokeredMessage> Receive()
        {
            return _inner.ReceiveAsync();
        }
    }
}