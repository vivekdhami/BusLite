namespace BusLite.AzureServiceBus.Messaging
{
    using BusLite.Messaging;
    using Microsoft.ServiceBus.Messaging;

    public class RuleDescriptionWrapper : IRuleDescription
    {
        private readonly RuleDescription _inner;

        public RuleDescriptionWrapper(RuleDescription inner)
        {
            _inner = inner;
        }

        public string Name { get { return _inner.Name; }}
    }
}