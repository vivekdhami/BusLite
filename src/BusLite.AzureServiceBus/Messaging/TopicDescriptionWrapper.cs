namespace BusLite.AzureServiceBus.Messaging
{
    using BusLite.Messaging;
    using Microsoft.ServiceBus.Messaging;

    public class TopicDescriptionWrapper : ITopicDescription
    {
        private readonly TopicDescription _inner;

        public TopicDescriptionWrapper(TopicDescription inner)
        {
            _inner = inner;
        }

        public string Path
        {
            get { return _inner.Path; }
        }

        public long MaxSizeInMegabytes
        {
            get { return _inner.MaxSizeInMegabytes; }
            set { _inner.MaxSizeInMegabytes = value; }
        }
    }
}