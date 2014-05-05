namespace BusLite.AzureServiceBus
{
    using Microsoft.ServiceBus;

    public class AzureServiceBusNamespaceManagerFactory : INamespaceManagerFactory
    {
        public INamespaceManager CreateFromConnectionString(string connectionString)
        {
            return new NamespaceManagerWrapper(NamespaceManager.CreateFromConnectionString(connectionString));
        }
    }
}