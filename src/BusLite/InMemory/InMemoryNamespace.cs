namespace BusLite.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusLite.Helpers;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemoryNamespace : INamespaceManager
    {
        private readonly int _delayMilliseconds;

        private readonly ConcurrentDictionary<string, Topic> _topics =
            new ConcurrentDictionary<string, Topic>(StringComparer.OrdinalIgnoreCase);

        public InMemoryNamespace(int delayMilliseconds)
        {
            _delayMilliseconds = delayMilliseconds;
        }

        public Task<bool> TopicExists(string path)
        {
            return Return(_topics.ContainsKey(path));
        }

        public Task<TopicDescription> CreateTopic(TopicDescription description)
        {
            Topic topic = _topics.GetOrAdd(description.Path, _ => new Topic(description, _delayMilliseconds));
            return Return(topic.Description);
        }

        public Task DeleteTopic(string path)
        {
            Topic _;
            _topics.TryRemove(path, out _);
            return Return();
        }

        public Task<TopicDescription> GetTopic(string path)
        {
            Topic topic;
            if (_topics.TryGetValue(path, out topic))
            {
                return Return(topic.Description);
            }
            throw new MessagingEntityNotFoundException(path);
        }

        public Task<IEnumerable<TopicDescription>> GetTopics(string filter = null)
        {
            return Return(_topics.Values.Select(t => t.Description));
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
            return Return(topic.Description);
        }

        public Task<bool> SubscriptionExists(string topicPath, string name)
        {
            Topic topic;
            return Return(_topics.TryGetValue(topicPath, out topic) && topic.SubscriptionExists(name));
        }

        public Task<SubscriptionDescription> CreateSubscription(SubscriptionDescription description,
            RuleDescription ruleDescription = null)
        {
            Topic topic = _topics[description.TopicPath];
            Subscription subscription = topic.CreateSubscription(description, ruleDescription);
            return Return(subscription.Description);
        }

        public Task<IEnumerable<SubscriptionDescription>> GetSubscriptions(string topicPath, string filter = null)
        {
            IEnumerable<SubscriptionDescription> subscriptions = _topics
                .Values
                .SelectMany(t => t.GetSubscriptions())
                .Select(s => s.Description);
            return Return(subscriptions);
        }

        public Task DeleteSubscription(string topicPath, string name)
        {
            Topic topic;
            if (_topics.TryGetValue(topicPath, out topic))
            {
                topic.DeleteSubscription(name);
            }
            return Return();
        }

        private Task Return()
        {
            return Task.Delay(_delayMilliseconds);
        }

        private async Task<T> Return<T>(T result)
        {
            await Task.Delay(_delayMilliseconds);
            return result;
        }

        internal InMemoryTopicClient GetInMemoryTopicClient(string topicPath)
        {
            Topic topic = _topics[topicPath];
            return topic.GetTopicClient();
        }

        internal InMemorySubscriptionClient CreateSubscriptionClient(string topicPath, string name)
        {
            Topic topic = _topics[topicPath];
            return topic.GetSubscriptionClient(name);
        }
    }
}