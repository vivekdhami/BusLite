namespace BusLite.Messaging
{
    public class BusLiteTopicDescription : ITopicDescription
    {
        private readonly string _path;

        public BusLiteTopicDescription(string path)
        {
            _path = path;
        }

        public BusLiteTopicDescription(ITopicDescription description)
        {
            _path = description.Path;
            MaxSizeInMegabytes = description.MaxSizeInMegabytes;
        }

        public string Path { get { return _path; } }

        public long MaxSizeInMegabytes { get; set; }
    }
}