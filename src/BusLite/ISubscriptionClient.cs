namespace BusLite
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public interface ISubscriptionClient
    {
        Task<BrokeredMessage> Receive();

        Task<BrokeredMessage> Receive(TimeSpan serverWaitTime);
    }
}