namespace BusLite.InMemory
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    internal class InMemorySubscriptionClient : ISubscriptionClient
    {
        private readonly Subscription _subscription;
        private readonly int _delay;

        internal InMemorySubscriptionClient(Subscription subscription, int delay)
        {
            _subscription = subscription;
            _delay = delay;
        }

        public async Task<BrokeredMessage> Receive()
        {
            await Task.Delay(_delay);
            return _subscription.Receive();
        }

        public async Task<BrokeredMessage> Receive(TimeSpan serverWaitTime)
        {
            await Task.Delay(_delay);
            return _subscription.Receive(serverWaitTime);
        }
    }
}