namespace BusLite.InMemory
{
    using System;
    using System.Collections.Generic;

    public class InMemoryServiceBus
    {
        private readonly int _delayMilliseconds;

        private readonly Dictionary<string, InMemoryNamespace> _namespaces =
            new Dictionary<string, InMemoryNamespace>(StringComparer.OrdinalIgnoreCase);

        public InMemoryServiceBus(int delayMilliseconds = 1)
        {
            _delayMilliseconds = delayMilliseconds;
        }

        public int DelayMilliseconds
        {
            get { return _delayMilliseconds; }
        }

        public InMemoryServiceBus WithNamespace(string @namespace)
        {
            _namespaces.Add(@namespace, new InMemoryNamespace(_delayMilliseconds));
            return this;
        }

        internal InMemoryNamespace GetNamespaceManager(string @namespace)
        {
            return _namespaces[@namespace];
        }
    }
}