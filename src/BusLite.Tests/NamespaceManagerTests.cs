#if AZURE
namespace BusLite.AzureServiceBus
#else
namespace BusLite
#endif
{
    using System;
    using System.Threading.Tasks;
    using BusLite.Messaging;
    using FluentAssertions;
    using Microsoft.ServiceBus.Messaging;
    using Xunit;

    public class NamespaceManagerTests : IUseFixture<NamespaceManagerFactoryFixture>, IDisposable
    {
        private NamespaceManagerFactoryFixture _fixture;

        [Fact]
        public async Task Can_check_if_topic_exists()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            (await namespaceManager.TopicExists("test/topic")).Should().BeFalse();
        }

        [Fact]
        public async Task Can_create_topic()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string path = "test/createtopic";
            ITopicDescription description = await namespaceManager.CreateTopic(path);

            description.Path.Should().Be(path);
        }

        [Fact]
        public async Task Can_delete_topic()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string path = "test/deletetopic";
            await namespaceManager.CreateTopic(path);

            await namespaceManager.DeleteTopic(path);

            (await namespaceManager.TopicExists(path)).Should().BeFalse();
        }

        [Fact]
        public void When_topic_does_not_exist_and_get_topic_then_should_get_null()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string path = "test/doesnotexisttopic";

            Func<Task> act = () => namespaceManager.GetTopic(path);

            act.ShouldThrow<MessagingEntityNotFoundException>();
        }

        [Fact]
        public async Task When_topic_exists_and_get_topic_then_should_get_description()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string path = "test/topicexists";
            await namespaceManager.CreateTopic(path);

            ITopicDescription description = await namespaceManager.GetTopic(path);

            description.Should().NotBeNull();
        }
        [Fact]
        public async Task Can_update_topic()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string path = "test/topicupdate1";
            ITopicDescription originalDescripton = await namespaceManager.CreateTopic(path);

            originalDescripton.MaxSizeInMegabytes = originalDescripton.MaxSizeInMegabytes + 1;

            ITopicDescription updatedDescription = await namespaceManager.UpdateTopic(originalDescripton);

            updatedDescription.Should().NotBeNull();
            updatedDescription.GetHashCode().Should().NotBe(originalDescripton.GetHashCode());
        }

        [Fact]
        public async Task Can_subscribe_to_a_topic()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string path = "test/subscriptiontest";


            if (!await namespaceManager.SubscriptionExists(path, "AllMessages"))
            {
                await namespaceManager.CreateSubscription(path, "AllMessages");
            }
        }

        public void SetFixture(NamespaceManagerFactoryFixture data)
        {
            _fixture = data;
        }

        public void Dispose()
        {
            CleanUp().Wait(TimeSpan.FromSeconds(10));
        }

        private async Task CleanUp()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            foreach (var topic in await namespaceManager.GetTopics())
            {
                await namespaceManager.DeleteTopic(topic.Path);
            }
        }
    }
}