namespace BusLite
{
    using System;
    using System.Collections.Generic;

    public class InMemoryServiceBus
    {
        private readonly int _delayMilliseconds;
        private readonly Dictionary<string, InMemoryNamespace> _namespaces = new Dictionary<string, InMemoryNamespace>(StringComparer.OrdinalIgnoreCase);

        public InMemoryServiceBus(int delayMilliseconds = 1)
        {
            _delayMilliseconds = delayMilliseconds;
        }

        public InMemoryServiceBus WithNamespace(string @namespace)
        {
            _namespaces.Add(@namespace, new InMemoryNamespace(_delayMilliseconds));
            return this;
        }

        public INamespaceManager GetNamespaceManager(string @namespace)
        {
            return _namespaces[@namespace];
        }
    }
}