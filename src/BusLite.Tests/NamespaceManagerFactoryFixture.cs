﻿namespace BusLite
{
    public abstract class NamespaceManagerFactoryFixture
    {
        public abstract INamespaceManagerFactory Factory { get; }

        public abstract INamespaceManager CreateNamespaceManager();
    }
}