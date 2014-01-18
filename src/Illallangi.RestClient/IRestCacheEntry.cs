namespace Illallangi
{
    public interface IRestCacheEntry
    {
        string BaseUrl { get; }
        string Resource { get; }
        string ETag { get; }
        string Content { get; }
    }
}