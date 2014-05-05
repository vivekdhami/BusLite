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
               .CreateFromConnectionString("sb://buslite.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }

        public INamespaceManager CreateNamespaceManagerThatDoesNotExist()
        {
            return _factory
               .CreateFromConnectionString("sb://shouldnotexistever.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }
    }
}