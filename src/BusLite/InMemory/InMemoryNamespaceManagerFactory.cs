namespace BusLite.InMemory
{
    using System;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;

    public class InMemoryNamespaceManagerFactory : INamespaceManagerFactory
    {
        private readonly InMemoryServiceBus _bus;

        public InMemoryNamespaceManagerFactory(InMemoryServiceBus bus)
        {
            _bus = bus;
        }

        public INamespaceManager CreateFromConnectionString(string connectionString)
        {
            string[] strArray = Regex.Split(connectionString, ";(Endpoint|SharedSecretIssuer|SharedSecretValue|)=",
                RegexOptions.IgnoreCase);
            var nameValueCollection = new NameValueCollection();

            for (int index = 1; index < strArray.Length; index = index + 1 + 1)
            {
                string input1 = strArray[index];
                string input2 = strArray[index + 1];
                nameValueCollection[input1] = input2;
            }
            var uri = new Uri(strArray[0]);
            return _bus.GetNamespaceManager(uri.Host);
        }
    }
}