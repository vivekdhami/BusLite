namespace BusLite.AzureServiceBus
{
    using Microsoft.ServiceBus.Messaging;

    public class AzureServiceBusTopicClientFactory : ITopicClientFactory
    {
        public ITopicClient CreateFromConnectionString(string connectionString, string path)
        {
            return new TopicClientWrapper(TopicClient.CreateFromConnectionString(connectionString, path));
        }
    }
}