namespace BusLite
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public interface INamespaceManager
    {
        Task<TopicDescription> CreateTopic(string path);

        Task<TopicDescription> CreateTopic(TopicDescription description);

        Task DeleteTopic(string path);

        Task<TopicDescription> GetTopic(string path);

        Task<IEnumerable<TopicDescription>> GetTopics(string filter = null);

        Task<bool> TopicExists(string path);

        Task<TopicDescription> UpdateTopic(TopicDescription description);
    }
}
