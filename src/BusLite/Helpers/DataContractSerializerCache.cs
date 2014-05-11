namespace BusLite.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Runtime.Serialization;

    internal static class DataContractSerializerCache
    {
        private static readonly ConcurrentDictionary<Type, DataContractSerializer> Cache = new ConcurrentDictionary<Type, DataContractSerializer>();

        internal static T Clone<T>(T source)
        {
            using (var stream = new MemoryStream())
            {
                DataContractSerializer serializer = Cache.GetOrAdd(typeof (T), t => new DataContractSerializer(t));
                serializer.WriteObject(stream, source);
                stream.Position = 0;
                return (T) serializer.ReadObject(stream);
            }
        }
    }
}