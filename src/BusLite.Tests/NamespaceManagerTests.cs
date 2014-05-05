#if AZURE
namespace BusLite.AzureServiceBus
#else
namespace BusLite
#endif
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public class NamespaceManagerTests : IUseFixture<NamespaceManagerFactoryFixture>
    {
        private NamespaceManagerFactoryFixture _fixture;

        [Fact]
        public async Task Can_check_if_topic_exists()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            (await namespaceManager.TopicExists("TestTopic")).Should().BeFalse();
        }

        public void SetFixture(NamespaceManagerFactoryFixture data)
        {
            _fixture = data;
        }
    }
}