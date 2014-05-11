namespace BusLite.Helpers
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    internal static class ObjectExtensions
    {
        private static readonly BinaryFormatter Serializer = new BinaryFormatter();

        internal static T DeepClone<T>(this T source)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, source);
                stream.Position = 0;
                return (T)Serializer.Deserialize(stream);
            }
        }
    }
}