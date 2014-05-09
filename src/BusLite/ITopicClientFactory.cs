namespace BusLite
{
    public interface ITopicClientFactory
    {
        ITopicClient CreateFromConnectionString(string connectionString, string topicPath);
    }
}