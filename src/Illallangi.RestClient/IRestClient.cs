using System;
using System.Collections.Generic;

namespace Illallangi
{
    public interface IRestClient
    {
        string GetContent(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null);
        
        string GetContent(Uri uri);
        
        T GetObject<T>(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null) where T : new();
        
        T GetObject<T>(Uri uri) where T : new();
    }
}