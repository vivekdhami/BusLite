namespace BusLite.InMemory
{
    public class InMemoryNamespaceManagerFactory : INamespaceManagerFactory
    {
        private readonly ServiceBus _bus;

        public InMemoryNamespaceManagerFactory(ServiceBus bus)
        {
            _bus = bus;
        }

        public INamespaceManager Create(string address)
        {
            return new InMemoryNamespaceManager();
        }
    }
}