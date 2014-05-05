namespace BusLite.InMemory
{
    public class InMemoryNamespaceManagerFactory : INamespaceManagerFactory
    {
        private readonly ServiceBus _bus;

        public InMemoryNamespaceManagerFactory(ServiceBus bus)
        {
            _bus = bus;
        }

        public INamespaceManager CreateFromConnectionString(string connectionString)
        {
            return new InMemoryNamespaceManager();
        }
    }
}