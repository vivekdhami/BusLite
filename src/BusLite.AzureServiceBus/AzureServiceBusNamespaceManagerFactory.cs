namespace BusLite.AzureServiceBus
{
    using Microsoft.ServiceBus;

    public class AzureServiceBusNamespaceManagerFactory : INamespaceManagerFactory
    {
        public INamespaceManager Create(string address)
        {
            return new NamespaceManagerWrapper(new NamespaceManager(address));
        }
    }
}