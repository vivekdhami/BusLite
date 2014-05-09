namespace BusLite
{
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;

    public interface ITopicClient
    {
        Task Send(BrokeredMessage message);
    }
}