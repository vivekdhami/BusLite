namespace BusLite.AzureServiceBus
{
    using System;
    using System.IO;

    public class NamespaceManagerFactoryFixture
    {
        private readonly INamespaceManagerFactory _factory;
        private readonly string _azureCredentials;

        public NamespaceManagerFactoryFixture()
        {
            _factory = new AzureServiceBusNamespaceManagerFactory();
            using (var sr = new StreamReader("AzureServiceBusCredentials.txt"))
            {
                _azureCredentials = sr.ReadLine();
                if (_azureCredentials == "sb://[yourtestnamespace].servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret")
                {
                    throw new Exception("Dude, you need to put your azure credentials in AzureServiceBusCredentials.txt else no-go!");
                }
            }
        }

        public INamespaceManagerFactory Factory
        {
            get { return _factory; }
        }

        public INamespaceManager CreateNamespaceManager()
        {
            return _factory
               .Create(_azureCredentials);
        }

        public INamespaceManager CreateNamespaceManagerThatDoesNotExist()
        {
            return _factory
               .Create("sb://shouldnotexistever.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=secret");
        }
    }
}