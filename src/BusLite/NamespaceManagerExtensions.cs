namespace BusLite
{
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public static class NamespaceManagerExtensions
    {
        public static Task<TopicDescription> CreateTopic(this INamespaceManager namespaceManager, string path)
        {
            return namespaceManager.CreateTopic(new TopicDescription(path));
        }

        public static Task<SubscriptionDescription> CreateSubscription(
            this INamespaceManager namespaceManager,
            string topicPath,
            string name)
        {
            return namespaceManager.CreateSubscription(new SubscriptionDescription(topicPath, name));
        }

        public static Task<SubscriptionDescription> CreateSubscription(
            this INamespaceManager namespaceManager,
            SubscriptionDescription description,
            Filter filter)
        {
            return namespaceManager.CreateSubscription(description, new RuleDescription(filter));
        }

        public static Task<SubscriptionDescription> CreateSubscription(
            this INamespaceManager namespaceManager,
            string topicPath,
            string name,
            Filter filter)
        {
            return namespaceManager.CreateSubscription(new SubscriptionDescription(topicPath, name), filter);
        }

        public static Task<SubscriptionDescription> CreateSubscription(
            this INamespaceManager namespaceManager,
            string topicPath,
            string name,
            RuleDescription ruleDescription)
        {
            return namespaceManager.CreateSubscription(new SubscriptionDescription(topicPath, name), ruleDescription);
        }
    }
}