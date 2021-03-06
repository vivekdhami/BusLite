﻿namespace BusLite
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.ServiceBus.Messaging;
    using Xunit;

    public abstract class TopicTestsBase<T> : IUseFixture<T>, IDisposable where T : NamespaceManagerFactoryFixture, new()
    {
        private T _fixture;

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
            TopicDescription description = await namespaceManager.CreateTopic(path);

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

            TopicDescription description = await namespaceManager.GetTopic(path);

            description.Should().NotBeNull();
        }

        [Fact]
        public async Task Can_update_topic()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string topicPath = "test/topicupdate1";
            TopicDescription originalDescripton = await namespaceManager.CreateTopic(topicPath);

            originalDescripton.MaxSizeInMegabytes = originalDescripton.MaxSizeInMegabytes + 1;

            TopicDescription updatedDescription = await namespaceManager.UpdateTopic(originalDescripton);

            updatedDescription.Should().NotBeNull();
            updatedDescription.GetHashCode().Should().NotBe(originalDescripton.GetHashCode());
        }

        [Fact]
        public async Task Can_subscribe_to_a_topic()
        {
            INamespaceManager namespaceManager = _fixture.CreateNamespaceManager();
            const string topicPath = "test/subscriptiontest";
            const string subscriptionName1 = "sub1";
            const string subscriptionName2 = "sub2";
            await namespaceManager.CreateTopic(topicPath);
            if (!await namespaceManager.SubscriptionExists(topicPath, subscriptionName1))
            {
                await namespaceManager.CreateSubscription(topicPath, subscriptionName1);
            }

            if (!await namespaceManager.SubscriptionExists(topicPath, subscriptionName2))
            {
                await namespaceManager.CreateSubscription(topicPath, subscriptionName2);
            }

            ITopicClient topicClient = _fixture.CreateTopicClient(topicPath);
            await topicClient.Send(new BrokeredMessage());

            ISubscriptionClient subscriptionClient1 = _fixture.CreateSubscriptionClient(topicPath, subscriptionName1);
            BrokeredMessage brokeredMessage = await subscriptionClient1.Receive(TimeSpan.FromSeconds(5));

            await brokeredMessage.CompleteAsync();

            ISubscriptionClient subscriptionClient2 = _fixture.CreateSubscriptionClient(topicPath, subscriptionName2);
            BrokeredMessage brokeredMessage2 = await subscriptionClient2.Receive(TimeSpan.FromSeconds(5));
            await brokeredMessage2.CompleteAsync();
        }

        public void SetFixture(T data)
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
                foreach (var subscription in await namespaceManager.GetSubscriptions(topic.Path))
                {
                    await namespaceManager.DeleteSubscription(subscription.TopicPath, subscription.Name);
                }
                await namespaceManager.DeleteTopic(topic.Path);
            }
        }
    }

    public class BusLiteTopicTests : TopicTestsBase<BusLiteNamespaceManagerFactoryFixture>
    {}

    public class AzureTopicTests : TopicTestsBase<AzureNamespaceManagerFactoryFixture>
    {}
}