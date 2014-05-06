namespace BusLite.AzureServiceBus.Messaging
{
    using BusLite.Messaging;
    using Microsoft.ServiceBus.Messaging;
    using TopicDescription = Microsoft.ServiceBus.Messaging.TopicDescription;
    using SubscriptionDescription = Microsoft.ServiceBus.Messaging.SubscriptionDescription;

    internal static class Extensions
    {
        internal static ITopicDescription ToWrapper(this TopicDescription description)
        {
            return new TopicDescriptionWrapper(description);
        }

        internal static TopicDescription ToAzureDescription(this ITopicDescription description)
        {
            return new TopicDescription(description.Path)
            {
                MaxSizeInMegabytes = description.MaxSizeInMegabytes
            };
        }

        internal static ISubscriptionDescription ToWrapper(this SubscriptionDescription description)
        {
            return new SubscriptionDescriptionWrapper(description);
        }

        internal static SubscriptionDescription ToAzureDescription(this ISubscriptionDescription description)
        {
            return new SubscriptionDescription(description.TopicPath, description.Name);
        }

        internal static RuleDescription ToAzureDescription(this IRuleDescription description)
        {
            return new RuleDescription(description.Name);
        }
    }
}