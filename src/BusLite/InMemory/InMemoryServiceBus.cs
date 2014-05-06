namespace BusLite.InMemory
{
    using System;
    using System.Collections.Generic;

    public class InMemoryServiceBus
    {
        private readonly Dictionary<string, InMemoryNamespace> _namespaces = new Dictionary<string, InMemoryNamespace>(StringComparer.OrdinalIgnoreCase);

        public InMemoryServiceBus WithNamespace(string @namespace)
        {
            _namespaces.Add(@namespace, new InMemoryNamespace());
            return this;
        }

        public INamespaceManager GetNamespaceManager(string @namespace)
        {
            return _namespaces[@namespace];
        }
    }
}