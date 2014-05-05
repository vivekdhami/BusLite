namespace BusLite
{
    using BusLite.InMemory;

    public class NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;

        public NamespaceManagerFactoryFixture()
        {
            _factory = new InMemoryNamespaceManagerFactory(new ServiceBus());
        }

        public INamespaceManagerFactory Factory
        {
            get { return _factory; }
        }

        public INamespaceManager CreateNamespaceManager()
        {
            return _factory
               .Create("sb://damotest.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }

        public INamespaceManager CreateNamespaceManagerThatDoesNotExist()
        {
            return _factory
               .Create("sb://shouldnotexistever.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }
    }
}