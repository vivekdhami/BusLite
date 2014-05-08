namespace BusLite
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemoryNamespace : INamespaceManager
    {
        private readonly ConcurrentDictionary<string, Topic> _topics = new ConcurrentDictionary<string, Topic>(StringComparer.OrdinalIgnoreCase);

        public Task<bool> TopicExists(string path)
        {
            return Task.FromResult(_topics.ContainsKey(path));
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
            topic.Description = DataContractSerializerCache.Clone(description);
            topic.Description.Path = description.Path;
            return Task.FromResult(topic.Description);
        }

        public Task<bool> SubscriptionExists(string topicPath, string name)
        {
            Topic topic;
            return Task.FromResult(_topics.TryGetValue(topicPath, out topic) && topic.SubscriptionExists(name));
        }

        public Task<SubscriptionDescription> CreateSubscription(SubscriptionDescription description, RuleDescription ruleDescription = null)
        {
            Topic topic = _topics[description.TopicPath];
            Subscription subscription = topic.CreateSubscription(description, ruleDescription);
            return Task.FromResult(subscription.Description);
        }

        public Task<IEnumerable<SubscriptionDescription>> GetSubscriptions(string topicPath, string filter = null)
        {
            IEnumerable<SubscriptionDescription> subscriptions = _topics
                .Values
                .SelectMany(t => t.GetSubscriptions())
                .Select(s => s.Description);
            return Task.FromResult(subscriptions);
        }

        public Task DeleteSubscription(string topicPath, string name)
        {
            Topic topic;
            if (_topics.TryGetValue(topicPath, out topic))
            {
                topic.DeleteSubscription(name);
            }
            return Task.FromResult(0);
        }

        private class Topic
        {
            private TopicDescription _topicDescription;
            private readonly ConcurrentDictionary<string, Subscription> _subscriptions = new ConcurrentDictionary<string, Subscription>(); 

            public Topic(TopicDescription topicDescription)
            {
                _topicDescription = topicDescription;
            }

            public TopicDescription Description
            {
                get { return _topicDescription; }
                set { _topicDescription = value; }
            }

            public Subscription CreateSubscription(SubscriptionDescription description, RuleDescription ruleDescription)
            {
                return _subscriptions.GetOrAdd(description.Name, s => new Subscription(description, ruleDescription));
            }

            public bool SubscriptionExists(string name)
            {
                return _subscriptions.ContainsKey(name);
            }

            public IEnumerable<Subscription> GetSubscriptions()
            {
                return _subscriptions.Values.ToArray();
            }

            public void DeleteSubscription(string name)
            {
                Subscription _;
                _subscriptions.TryRemove(name, out _);
            }
        }

        private class Subscription
        {
            private readonly SubscriptionDescription _description;
            private readonly RuleDescription _ruleDescription;

            public Subscription(SubscriptionDescription description, RuleDescription ruleDescription)
            {
                _description = description;
                _ruleDescription = ruleDescription;
            }

            public SubscriptionDescription Description
            {
                get { return _description; }
            }

            public RuleDescription RuleDescription
            {
                get { return _ruleDescription; }
            }
        }
    }
}