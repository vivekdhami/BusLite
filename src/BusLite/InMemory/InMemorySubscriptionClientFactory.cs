namespace BusLite.InMemory
{
    using System;
    using BusLite.Helpers;

    public class InMemorySubscriptionClientFactory : ISubscriptionClientFactory
    {
        private readonly InMemoryServiceBus _inMemoryServiceBus;

        public InMemorySubscriptionClientFactory(InMemoryServiceBus inMemoryServiceBus)
        {
            _inMemoryServiceBus = inMemoryServiceBus;
        }

        public ISubscriptionClient CreateFromConnectionString(string connectionString, string topicPath, string name)
        {
            Uri uri = ConnectionString.GetUri(connectionString);
            InMemoryNamespace namespaceManager = _inMemoryServiceBus.GetNamespaceManager(uri.Host);

            return namespaceManager.CreateSubscriptionClient(topicPath, name);
        }
    }
}