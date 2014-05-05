namespace BusLite.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemoryNamespaceManager : INamespaceManager
    {
        public Task<bool> TopicExists(string path)
        {
            throw new NotImplementedException();
        }

        public Task<TopicDescription> CreateTopic(string path)
        {
            throw new NotImplementedException();
        }

        public Task<TopicDescription> CreateTopic(TopicDescription description)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTopic(string path)
        {
            throw new NotImplementedException();
        }

        public Task<TopicDescription> GetTopic(string path)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TopicDescription>> GetTopics(string filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<TopicDescription> UpdateTopic(TopicDescription description)
        {
            throw new NotImplementedException();
        }
    }
}