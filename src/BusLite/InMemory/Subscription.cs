namespace BusLite.InMemory
{
    using System;
    using System.Collections.Concurrent;
    using Microsoft.ServiceBus.Messaging;

    internal class Subscription
    {
        private readonly SubscriptionDescription _description;
        private readonly RuleDescription _ruleDescription;
        private readonly int _delayMilliseconds;
        private readonly BlockingCollection<BrokeredMessage> _brokeredMessages = new BlockingCollection<BrokeredMessage>(); 

        public Subscription(SubscriptionDescription description, RuleDescription ruleDescription, int delayMilliseconds)
        {
            _description = description;
            _ruleDescription = ruleDescription;
            _delayMilliseconds = delayMilliseconds;
        }

        public SubscriptionDescription Description
        {
            get { return _description; }
        }

        public RuleDescription RuleDescription
        {
            get { return _ruleDescription; }
        }

        internal void Send(BrokeredMessage brokeredMessage)
        {
            _brokeredMessages.Add(brokeredMessage);
        }

        internal BrokeredMessage Receive()
        {
            return _brokeredMessages.Take();
        }

        internal BrokeredMessage Receive(TimeSpan serverWaitTime)
        {
            BrokeredMessage message;
            if (_brokeredMessages.TryTake(out message, serverWaitTime))
            {
                return message;
            }
            throw new TimeoutException();
        }

        internal InMemorySubscriptionClient GetSubscriptionClient()
        {
            return new InMemorySubscriptionClient(this, _delayMilliseconds);
        }
    }
}