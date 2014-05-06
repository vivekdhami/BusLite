namespace BusLite
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusLite.Messaging;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemoryNamespace : INamespaceManager
    {
        private readonly ConcurrentDictionary<string, Topic> _topics = new ConcurrentDictionary<string, Topic>(StringComparer.OrdinalIgnoreCase);

        public Task<bool> TopicExists(string path)
        {
            return Task.FromResult(_topics.ContainsKey(path));
        }

        public Task<ITopicDescription> CreateTopic(ITopicDescription description)
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

        public Task<ITopicDescription> GetTopic(string path)
        {
            Topic topic;
            if (_topics.TryGetValue(path, out topic))
            {
                return Task.FromResult(topic.Description);
            }
            throw new MessagingEntityNotFoundException(path);
        }

        public Task<IEnumerable<ITopicDescription>> GetTopics(string filter = null)
        {
            return Task.FromResult(_topics.Values.Select(t => t.Description));
        }

        public Task<ITopicDescription> UpdateTopic(ITopicDescription description)
        {
            Topic topic;
            if (!_topics.TryGetValue(description.Path, out topic))
            {
                throw new ArgumentException("Topic does not exist");
            }
            
            topic.Description = new BusLiteTopicDescription(description);
            return Task.FromResult(topic.Description);
        }

        public Task<bool> SubscriptionExists(string topicPath, string name)
        {
            throw new NotImplementedException();
        }

        public Task<ISubscriptionDescription> CreateSubscription(ISubscriptionDescription description, IRuleDescription ruleDescription = null)
        {
            throw new NotImplementedException();
        }

        private class Topic
        {
            private ITopicDescription _topicDescription;

            public Topic(ITopicDescription topicDescription)
            {
                _topicDescription = topicDescription;
            }

            public ITopicDescription Description
            {
                get { return _topicDescription; }
                set { _topicDescription = value; }
            }
        }
    }
}