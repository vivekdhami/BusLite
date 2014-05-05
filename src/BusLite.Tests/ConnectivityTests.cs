#if AZURE
namespace BusLite.AzureServiceBus
#else
namespace BusLite
#endif
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;
    using Xunit;

    public class ConnectivityTests : IUseFixture<NamespaceManagerFactoryFixture>
    {
        private NamespaceManagerFactoryFixture _fixture;

        [Fact]
        public async Task When_namespace_does_not_exist()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManagerThatDoesNotExist();
            IEnumerable<TopicDescription> topics = await namespaceManager.GetTopics();
        }

        public void SetFixture(NamespaceManagerFactoryFixture data)
        {
            _fixture = data;
        }
    }
}