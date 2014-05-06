namespace BusLite.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemoryNamespace : INamespaceManager
    {
        private readonly ConcurrentDictionary<string, Topic> _topics = new ConcurrentDictionary<string, Topic>(StringComparer.OrdinalIgnoreCase);

        public Task<bool> TopicExists(string path)
        {
            return Task.FromResult(_topics.ContainsKey(path));
        }

        public Task<TopicDescription> CreateTopic(string path)
        {
            return CreateTopic(new TopicDescription(path));
        }

        public Task<TopicDescription> CreateTopic(TopicDescription description)
        {
            Topic topic = _topics.GetOrAdd(description.Path, _ => new Topic(description));
            return Task.FromResult(topic.Description);
        }

        public Task DeleteTopic(string path)
        {
            Topic _;
            _topics.TryRemove(path, out _);
            return Task.FromResult(0);
        }

        public Task<TopicDescription> GetTopic(string path)
        {
            Topic topic;
            if (_topics.TryGetValue(path, out topic))
            {
                return Task.FromResult(topic.Description);
            }
            throw new MessagingEntityNotFoundException(path);
        }

        public Task<IEnumerable<TopicDescription>> GetTopics(string filter = null)
        {
            return Task.FromResult(_topics.Values.Select(t => t.Description));
        }

        public Task<TopicDescription> UpdateTopic(TopicDescription description)
        {
            Topic topic;
            if (!_topics.TryGetValue(description.Path, out topic))
            {
                throw new ArgumentException("Topic does not exist");
            }
            topic.Description = description;
            return Task.FromResult(topic.Description);
        }

        private class Topic
        {
            private TopicDescription _topicDescription;

            public Topic(TopicDescription topicDescription)
            {
                _topicDescription = topicDescription;
            }

            public TopicDescription Description
            {
                get { return _topicDescription; }
                set { _topicDescription = value; }
            }
        }
    }
}