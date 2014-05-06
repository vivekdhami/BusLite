namespace BusLite
{
    using System.Threading.Tasks;
    using BusLite.Messaging;

    public static class NamespaceManagerExtensions
    {
        public static Task<ITopicDescription> CreateTopic(this INamespaceManager namespaceManager, string path)
        {
            return namespaceManager.CreateTopic(new BusLiteTopicDescription(path));
        }
    }
}