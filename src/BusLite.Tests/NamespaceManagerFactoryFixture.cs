namespace BusLite
{
    public class NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;

        public NamespaceManagerFactoryFixture()
        {
            InMemoryServiceBus inMemoryServiceBus = new InMemoryServiceBus()
                .WithNamespace("buslite.servicebus.windows.net");
            _factory = new InMemoryNamespaceManagerFactory(inMemoryServiceBus);
        }

        public INamespaceManagerFactory Factory
        {
            get { return _factory; }
        }

        public INamespaceManager CreateNamespaceManager()
        {
            return _factory
               .CreateFromConnectionString("Endpoint://buslite.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }
    }
}