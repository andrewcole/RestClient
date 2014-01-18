namespace Illallangi
{
    public interface IRestCache
    {
        IRestCacheEntry Retrieve(string baseUrl, string toString);
        void Delete(IRestCacheEntry cache);
        void Create(string baseUrl, string resource, string eTag, string content);
    }
}