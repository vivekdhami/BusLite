namespace BusLite
{
    using BusLite.InMemory;

    public class BusLiteNamespaceManagerFactoryFixture : NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _namespaceManagerFactory;
        private readonly InMemoryTopicClientFactory _topicClientFactory;
        private const string ConnectionString = "Endpoint://buslite.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret";

        public BusLiteNamespaceManagerFactoryFixture()
        {
            InMemoryServiceBus inMemoryServiceBus = new InMemoryServiceBus()
                .WithNamespace("buslite.servicebus.windows.net");
            _namespaceManagerFactory = new InMemoryNamespaceManagerFactory(inMemoryServiceBus);
            _topicClientFactory = new InMemoryTopicClientFactory(inMemoryServiceBus);
        }

        public override INamespaceManagerFactory Factory
        {
            get { return _namespaceManagerFactory; }
        }

        public override INamespaceManager CreateNamespaceManager()
        {
            return _namespaceManagerFactory
               .CreateFromConnectionString(ConnectionString);
        }

        public override ITopicClient CreateTopicClient(string path)
        {
            return _topicClientFactory
                .CreateFromConnectionString(ConnectionString, path);
        }
    }
}