namespace BusLite
{
    public abstract class NamespaceManagerFactoryFixture
    {
        public abstract INamespaceManagerFactory Factory { get; }

        public abstract INamespaceManager CreateNamespaceManager();

        public abstract ITopicClient CreateTopicClient(string path);

        public abstract ISubscriptionClient CreateSubscriptionClient(string path, string name);
    }
}