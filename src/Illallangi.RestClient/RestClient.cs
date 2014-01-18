using System;
using System.Collections.Generic;
using Common.Logging;
using Common.Logging.Simple;
using Illallangi.Extensions;
using Newtonsoft.Json;

namespace Illallangi
{
    public class RestClient : IRestClient
    {
        #region Fields

        private readonly ILog currentLog;

        private readonly IEnumerable<KeyValuePair<string, string>> currentDefaultParameters;

        private readonly string currentBaseUrl;

        private Uri currentBaseUri;

        private RestSharp.IRestClient currentRestSharpClient;

        #endregion

        #region Constructor

        public RestClient(string baseUrl, IEnumerable<KeyValuePair<string, string>> defaultParameters = null, ILog log = null)
        {
            this.currentBaseUrl = baseUrl;
            this.currentDefaultParameters = defaultParameters;
            this.currentLog = log ?? new NoOpLogger();

            this.Log.DebugFormat("RestClient(baseUrl={0}, defaultParameters={1}, log = {2}",
                this.BaseUrl,
                this.DefaultParameters,
                this.Log);
        }

        #endregion

        #region Properties

        private string BaseUrl
        {
            get
            {
                return this.currentBaseUrl;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> DefaultParameters
        {
            get
            {
                return this.currentDefaultParameters;
            }
        }
        
        private ILog Log
        {
            get
            {
                return this.currentLog;
            }
        }

        private Uri BaseUri
        {
            get
            {
                return this.currentBaseUri ?? (this.currentBaseUri = new Uri(this.BaseUrl));
            }
        }

        private RestSharp.IRestClient RestSharpClient
        {
            get
            {
                return this.currentRestSharpClient ?? (this.currentRestSharpClient = new RestSharp.RestClient(this.BaseUrl));
            }
        }
        
        #endregion

        #region Methods

        public string GetContent(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null)
        {
            return this.GetContent(uri.TemplateWith(parameters, this.DefaultParameters));
        }

        public string GetContent(Uri uri)
        {
            return this.Execute(uri);
        }

        public T GetObject<T>(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null) where T : new()
        {
            return this.GetObject<T>(uri.TemplateWith(parameters, this.DefaultParameters));
        }

        public T GetObject<T>(Uri uri) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(this.Execute(uri));
        }

        private string Execute(Uri uri)
        {
            // Create Request
            var resource = uri.IsAbsoluteUri ? this.BaseUri.MakeRelativeUri(uri) : uri;
            var request = new RestSharp.RestRequest(resource, RestSharp.Method.GET);

            // Execute Request
            var restResponse = this.RestSharpClient.Execute(request);

            // Return
            return restResponse.Content;
        }

        #endregion
    }
}
