namespace BusLite.AzureServiceBus
{
    using System;
    using Microsoft.WindowsAzure;

    public class NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;
        private readonly string _azureCredentials;

        public NamespaceManagerFactoryFixture()
        {
            _factory = new AzureServiceBusNamespaceManagerFactory();
            _azureCredentials = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            if (_azureCredentials.Contains("[your namespace]"))
            {
                throw new Exception("Dude, you need to put your azure credentials in app.config else no-go!");
            }
        }

        public INamespaceManagerFactory Factory
        {
            get { return _factory; }
        }

        public INamespaceManager CreateNamespaceManager()
        {
            return _factory
               .CreateFromConnectionString(_azureCredentials);
        }

        public INamespaceManager CreateNamespaceManagerThatDoesNotExist()
        {
            return _factory
               .CreateFromConnectionString("sb://shouldnotexistever.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }
    }
}