using System;
using System.Collections.Generic;

namespace Illallangi
{
    public interface IRestClient
    {
        string GetContent(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null, CacheMode cacheMode = CacheMode.Enabled);

        string GetContent(Uri uri, CacheMode cacheMode = CacheMode.Enabled);

        T GetObject<T>(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null, CacheMode cacheMode = CacheMode.Enabled) where T : new();

        T GetObject<T>(Uri uri, CacheMode cacheMode = CacheMode.Enabled) where T : new();
    }
}