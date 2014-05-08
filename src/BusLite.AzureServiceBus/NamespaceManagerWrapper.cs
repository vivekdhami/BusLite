namespace BusLite.AzureServiceBus
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using TopicDescription = Microsoft.ServiceBus.Messaging.TopicDescription;

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

        public async Task<SubscriptionDescription> CreateSubscription(SubscriptionDescription description, RuleDescription ruleDescription = null)
        {
            SubscriptionDescription subscriptionDescription = ruleDescription == null
                ? await _namespaceManager.CreateSubscriptionAsync(description)
                : await _namespaceManager.CreateSubscriptionAsync(description, ruleDescription);
            return subscriptionDescription;
        }
    }
}