namespace BusLite
{
    using BusLite.InMemory;

    public class BusLiteNamespaceManagerFactoryFixture : NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;

        public BusLiteNamespaceManagerFactoryFixture()
        {
            InMemoryServiceBus inMemoryServiceBus = new InMemoryServiceBus()
                .WithNamespace("buslite.servicebus.windows.net");
            _factory = new InMemoryNamespaceManagerFactory(inMemoryServiceBus);
        }

        public override INamespaceManagerFactory Factory
        {
            get { return _factory; }
        }

        public override INamespaceManager CreateNamespaceManager()
        {
            return _factory
               .CreateFromConnectionString("Endpoint://buslite.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }
    }
}