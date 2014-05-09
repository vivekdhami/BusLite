namespace BusLite
{
    using System;
    using BusLite.AzureServiceBus;
    using Microsoft.WindowsAzure;

    public class AzureNamespaceManagerFactoryFixture : NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;
        private readonly string _azureCredentials;

        public AzureNamespaceManagerFactoryFixture()
        {
            _factory = new AzureServiceBus.AzureServiceBusNamespaceManagerFactory();
            _azureCredentials = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            if (_azureCredentials.Contains("[your namespace]"))
            {
                throw new Exception("Dude, you need to put your azure credentials in app.config else no-go! Also, don't commit your crdentials to git.");
            }
        }

        public override INamespaceManagerFactory Factory
        {
            get { return _factory; }
        }

        public override INamespaceManager CreateNamespaceManager()
        {
            return _factory
               .CreateFromConnectionString(_azureCredentials);
        }
    }
}