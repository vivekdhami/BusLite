namespace BusLite.AzureServiceBus
{
    using System;
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

        public Task<TopicDescription> CreateTopic(string path)
        {
            return _namespaceManager.CreateTopicAsync(path);
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TopicDescription>> GetTopics(string filter = null)
        {
            return _namespaceManager.GetTopicsAsync(filter);
        }

        public Task<bool> TopicExists(string path)
        {
            return _namespaceManager.TopicExistsAsync(path);
        }

        public Task<TopicDescription> UpdateTopic(TopicDescription description)
        {
            return _namespaceManager.UpdateTopicAsync(description);
        }
    }
}