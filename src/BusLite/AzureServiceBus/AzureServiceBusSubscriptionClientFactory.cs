namespace BusLite.AzureServiceBus
{
    using Microsoft.ServiceBus.Messaging;

    public class AzureServiceBusSubscriptionClientFactory : ISubscriptionClientFactory
    {
        public ISubscriptionClient CreateFromConnectionString(string connectionString, string path, string name)
        {
            return new SubscriptionClientWrapper(SubscriptionClient.CreateFromConnectionString(connectionString, path, name));
        }
    }
}