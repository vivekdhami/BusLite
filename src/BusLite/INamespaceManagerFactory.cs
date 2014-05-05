namespace BusLite
{
    public interface INamespaceManagerFactory
    {
        INamespaceManager Create(string address);
    }
}