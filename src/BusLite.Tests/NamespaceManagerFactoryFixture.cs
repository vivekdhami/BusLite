namespace BusLite
{
    using BusLite.InMemory;

    public class NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;

        public NamespaceManagerFactoryFixture()
        {
            ServiceBus serviceBus = new ServiceBus()
                .WithNamespace("buslite");
            _factory = new InMemoryNamespaceManagerFactory(serviceBus);
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