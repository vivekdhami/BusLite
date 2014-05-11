namespace BusLite.InMemory
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using BusLite.Helpers;
    using Microsoft.ServiceBus.Messaging;

    internal class Topic
    {
        private readonly int _delayMilliseconds;

        private readonly ConcurrentDictionary<string, Subscription> _subscriptions =
            new ConcurrentDictionary<string, Subscription>();

        public Topic(TopicDescription topicDescription, int delayMilliseconds)
        {
            Description = topicDescription;
            _delayMilliseconds = delayMilliseconds;
        }

        public TopicDescription Description { get; set; }

        public Subscription CreateSubscription(SubscriptionDescription description, RuleDescription ruleDescription)
        {
            return _subscriptions.GetOrAdd(description.Name, s => new Subscription(description, ruleDescription, _delayMilliseconds));
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

        internal InMemoryTopicClient GetTopicClient()
        {
            return new InMemoryTopicClient(this);
        }

        internal InMemorySubscriptionClient GetSubscriptionClient(string name)
        {
            Subscription subscription = _subscriptions[name];
            return subscription.GetSubscriptionClient();
        }

        internal Task Send(BrokeredMessage message)
        {
            Subscription[] subscriptions = _subscriptions.Values.ToArray();
            foreach (var subscription in subscriptions)
            {
                subscription.Send(DataContractSerializerCache.Clone(message));
            }
            return Task.Delay(0);
        }
    }
}