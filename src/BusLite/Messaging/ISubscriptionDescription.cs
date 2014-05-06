namespace BusLite.Messaging
{
    public interface ISubscriptionDescription
    {
        string Name { get; }

        string TopicPath { get; }
    }
}