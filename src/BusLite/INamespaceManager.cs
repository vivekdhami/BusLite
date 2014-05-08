namespace BusLite
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public interface INamespaceManager
    {
        Task<TopicDescription> CreateTopic(TopicDescription description);

        Task DeleteTopic(string path);

        Task<TopicDescription> GetTopic(string path);

        Task<IEnumerable<TopicDescription>> GetTopics(string filter = null);

        Task<bool> TopicExists(string path);

        Task<TopicDescription> UpdateTopic(TopicDescription description);

        Task<bool> SubscriptionExists(string topicPath, string name);

        Task<SubscriptionDescription> CreateSubscription(SubscriptionDescription description, RuleDescription ruleDescription = null);

        Task<IEnumerable<SubscriptionDescription>> GetSubscriptions(string topicPath, string filter = null);

        Task DeleteSubscription(string topicPath, string name);
    }
}
