namespace BusLite.AzureServiceBus
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    internal class NamespaceManagerWrapper : INamespaceManager
    {
        private readonly NamespaceManager _namespaceManager;

        internal NamespaceManagerWrapper(NamespaceManager namespaceManager)
        {
            _namespaceManager = namespaceManager;
        }

        public Task<TopicDescription> CreateTopic(TopicDescription description)
        {
            return _namespaceManager.CreateTopicAsync(description);
        }

        public Task DeleteTopic(string path)
        {
            return _namespaceManager.DeleteTopicAsync(path);
        }

        public Task<TopicDescription> GetTopic(string path)
        {
            return _namespaceManager.GetTopicAsync(path);
        }

        public Task<IEnumerable<TopicDescription>> GetTopics(string filter = null)
        {
            return _namespaceManager.GetTopicsAsync(filter);
        }

        public Task<bool> TopicExists(string path)
        {
            return _namespaceManager.TopicExistsAsync(path);
        }

        public async Task<TopicDescription> UpdateTopic(TopicDescription description)
        {
            return await _namespaceManager.UpdateTopicAsync(description);
        }

        public Task<bool> SubscriptionExists(string topicPath, string name)
        {
            return _namespaceManager.SubscriptionExistsAsync(topicPath, name);
        }

        public Task<SubscriptionDescription> CreateSubscription(SubscriptionDescription description, RuleDescription ruleDescription = null)
        {
           return ruleDescription == null
                ? _namespaceManager.CreateSubscriptionAsync(description)
                : _namespaceManager.CreateSubscriptionAsync(description, ruleDescription);
        }

        public Task<IEnumerable<SubscriptionDescription>> GetSubscriptions(string topicPath, string filter = null)
        {
           return filter == null
                ? _namespaceManager.GetSubscriptionsAsync(topicPath)
                : _namespaceManager.GetSubscriptionsAsync(topicPath, filter);
        }

        public Task DeleteSubscription(string topicPath, string name)
        {
            return _namespaceManager.DeleteSubscriptionAsync(topicPath, name);
        }
    }
}