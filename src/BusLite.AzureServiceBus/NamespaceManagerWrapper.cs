namespace BusLite.AzureServiceBus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusLite.AzureServiceBus.Messaging;
    using BusLite.Messaging;
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

        public async Task<ITopicDescription> CreateTopic(ITopicDescription description)
        {
            TopicDescription topicDescription = await _namespaceManager.CreateTopicAsync(description.ToAzureDescription());
            return topicDescription.ToWrapper();
        }

        public Task DeleteTopic(string path)
        {
            return _namespaceManager.DeleteTopicAsync(path);
        }

        public async Task<ITopicDescription> GetTopic(string path)
        {
            TopicDescription description = await _namespaceManager.GetTopicAsync(path);
            return description.ToWrapper();
        }

        public async Task<IEnumerable<ITopicDescription>> GetTopics(string filter = null)
        {
            IEnumerable<TopicDescription> topicDescriptions = await _namespaceManager.GetTopicsAsync(filter);
            return topicDescriptions.Select(t => t.ToWrapper());
        }

        public Task<bool> TopicExists(string path)
        {
            return _namespaceManager.TopicExistsAsync(path);
        }

        public async Task<ITopicDescription> UpdateTopic(ITopicDescription description)
        {
            TopicDescription topicDescription = await _namespaceManager.UpdateTopicAsync(description.ToAzureDescription());
            return topicDescription.ToWrapper();
        }

        public Task<bool> SubscriptionExists(string topicPath, string name)
        {
            return _namespaceManager.SubscriptionExistsAsync(topicPath, name);
        }

        public async Task<ISubscriptionDescription> CreateSubscription(ISubscriptionDescription description, IRuleDescription ruleDescription = null)
        {
            SubscriptionDescription subscriptionDescription = ruleDescription == null
                ? await _namespaceManager.CreateSubscriptionAsync(description.ToAzureDescription())
                : await _namespaceManager.CreateSubscriptionAsync(description.ToAzureDescription(), ruleDescription.ToAzureDescription());
            return subscriptionDescription.ToWrapper();
        }
    }
}