namespace BusLite.AzureServiceBus.Messaging
{
    using BusLite.Messaging;
    using TopicDescription = Microsoft.ServiceBus.Messaging.TopicDescription;

    internal static class TopicDescriptionExtensions
    {
        internal static ITopicDescription ToWrapper(this TopicDescription description)
        {
            return new TopicDescriptionWrapper(description)
            {
                MaxSizeInMegabytes = description.MaxSizeInMegabytes
            };
        }

        internal static TopicDescription ToAzureTopicDescription(this ITopicDescription description)
        {
            return new TopicDescription(description.Path)
            {
                MaxSizeInMegabytes = description.MaxSizeInMegabytes
            };
        }
    }
}