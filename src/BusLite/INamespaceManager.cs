﻿namespace BusLite
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusLite.Messaging;

    public interface INamespaceManager
    {
        Task<ITopicDescription> CreateTopic(ITopicDescription description);

        Task DeleteTopic(string path);

        Task<ITopicDescription> GetTopic(string path);

        Task<IEnumerable<ITopicDescription>> GetTopics(string filter = null);

        Task<bool> TopicExists(string path);

        Task<ITopicDescription> UpdateTopic(ITopicDescription description);

        Task<bool> SubscriptionExists(string topicPath, string name);

        Task<ISubscriptionDescription> CreateSubscription(ISubscriptionDescription description, IRuleDescription ruleDescription = null);
    }
}
