namespace BusLite.InMemory
{
    using System;
    using BusLite.Helpers;

    public class InMemoryNamespaceManagerFactory : INamespaceManagerFactory
    {
        private readonly InMemoryServiceBus _bus;

        public InMemoryNamespaceManagerFactory(InMemoryServiceBus bus)
        {
            _bus = bus;
        }

        public INamespaceManager CreateFromConnectionString(string connectionString)
        {
            Uri uri = ConnectionString.GetUri(connectionString);
            return _bus.GetNamespaceManager(uri.Host);
        }
    }
}