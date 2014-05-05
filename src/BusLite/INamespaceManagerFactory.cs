namespace BusLite
{
    public interface INamespaceManagerFactory
    {
        INamespaceManager CreateFromConnectionString(string connectionString);
    }
}