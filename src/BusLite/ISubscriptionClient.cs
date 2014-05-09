namespace BusLite
{
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public interface ISubscriptionClient
    {
        Task<BrokeredMessage> Receive();
    }
}