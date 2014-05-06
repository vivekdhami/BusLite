namespace BusLite.Messaging
{
    public interface ITopicDescription
    {
        string Path { get; }

        long MaxSizeInMegabytes { get; set; }
    }
}