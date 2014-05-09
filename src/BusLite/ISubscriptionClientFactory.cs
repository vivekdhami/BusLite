namespace BusLite
{
    public interface ISubscriptionClientFactory
    {
        ISubscriptionClient CreateFromConnectionString(string connectionString, string topicPath, string name);
    }
}