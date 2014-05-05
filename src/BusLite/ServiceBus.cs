namespace BusLite
{
    using System;
    using System.Collections.Generic;

    public class ServiceBus
    {
        private readonly HashSet<string> _namespaces = new HashSet<string>(StringComparer.OrdinalIgnoreCase); 

        public ServiceBus WithNamespace(string @namespace)
        {
            _namespaces.Add(@namespace);
            return this;
        }
    }
}