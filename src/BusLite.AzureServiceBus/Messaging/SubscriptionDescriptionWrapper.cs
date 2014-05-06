namespace BusLite.AzureServiceBus.Messaging
{
    using BusLite.Messaging;
    using Microsoft.ServiceBus.Messaging;

    public class SubscriptionDescriptionWrapper : ISubscriptionDescription
    {
        private readonly SubscriptionDescription _inner;

        public SubscriptionDescriptionWrapper(SubscriptionDescription inner)
        {
            _inner = inner;
        }

        public string Name
        {
            get { return _inner.Name; }
        }

        public string TopicPath
        {
            get { return _inner.TopicPath; }
        }
    }
}