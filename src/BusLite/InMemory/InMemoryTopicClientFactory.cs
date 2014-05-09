namespace BusLite.InMemory
{
    using System;
    using BusLite.Helpers;

    public class InMemoryTopicClientFactory : ITopicClientFactory
    {
        private readonly InMemoryServiceBus _inMemoryServiceBus;

        public InMemoryTopicClientFactory(InMemoryServiceBus inMemoryServiceBus)
        {
            _inMemoryServiceBus = inMemoryServiceBus;
        }

        public ITopicClient CreateFromConnectionString(string connectionString, string topicPath)
        {
            Uri uri = ConnectionString.GetUri(connectionString);
            INamespaceManager namespaceManager = _inMemoryServiceBus.GetNamespaceManager(uri.Host);
            return new InMemoryTopicClient();
        }
    }
}