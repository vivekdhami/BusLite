namespace BusLite
{
    using System;
    using BusLite.AzureServiceBus;
    using Microsoft.WindowsAzure;

    public class AzureNamespaceManagerFactoryFixture : NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _namespaceManagerFactory;
        private readonly ITopicClientFactory _topicClientFactory;
        private readonly ISubscriptionClientFactory _subscriptionClientFactory;
        private readonly string _connectionString;

        public AzureNamespaceManagerFactoryFixture()
        {
            _namespaceManagerFactory = new AzureServiceBusNamespaceManagerFactory();
            _topicClientFactory = new AzureServiceBusTopicClientFactory();
            _subscriptionClientFactory = new AzureServiceBusSubscriptionClientFactory();
            _connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            if (_connectionString.Contains("[your namespace]"))
            {
                throw new Exception("Dude, you need to put your azure credentials in app.config else no-go! Also, don't commit your crdentials to git.");
            }
        }

        public override INamespaceManagerFactory Factory
        {
            get { return _namespaceManagerFactory; }
        }

        public override INamespaceManager CreateNamespaceManager()
        {
            return _namespaceManagerFactory
               .CreateFromConnectionString(_connectionString);
        }

        public override ITopicClient CreateTopicClient(string path)
        {
            return _topicClientFactory
                .CreateFromConnectionString(_connectionString, path);
        }

        public override ISubscriptionClient CreateSubscriptionClient(string path, string name)
        {
            return _subscriptionClientFactory
                .CreateFromConnectionString(_connectionString, path, name);
        }
    }
}