using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ninject.Extensions.Logging;

namespace Illallangi.RestClient
{
    public class RestClientBase
    {
        #region Fields

        private readonly ILogger currentLogger;

        private readonly IEnumerable<KeyValuePair<string, string>> currentDefaultParameters;

        private readonly Uri currentBaseUri;

        private readonly RestSharp.IRestClient currentRestClient;

        #endregion

        #region Constructor

        protected RestClientBase(ILogger logger, string baseUrl, IEnumerable<KeyValuePair<string, string>> defaultParameters)
        {
            this.currentLogger = logger;
            this.currentBaseUri = new Uri(baseUrl);
            this.currentDefaultParameters = defaultParameters;
            this.currentRestClient = new RestSharp.RestClient(baseUrl);
        }

        #endregion

        #region Properties

        private ILogger Logger
        {
            get
            {
                return this.currentLogger;
            }
        }

        private RestSharp.IRestClient RestClient
        {
            get
            {
                return this.currentRestClient;
            }
        }

        private Uri BaseUri
        {
            get
            {
                return this.currentBaseUri;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> DefaultParameters
        {
            get
            {
                return this.currentDefaultParameters;
            }
        }

        #endregion

        #region Methods

        protected string GetContent(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null)
        {
            return this.GetContent(uri.TemplateWith(parameters, this.DefaultParameters));
        }

        protected string GetContent(Uri uri)
        {
            return this.Execute(uri);
        }

        protected T GetObject<T>(string uri, IEnumerable<KeyValuePair<string, string>> parameters = null) where T : new()
        {
            return this.GetObject<T>(uri.TemplateWith(parameters, this.DefaultParameters));
        }

        protected T GetObject<T>(Uri uri) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(this.Execute(uri));
        }

        private string Execute(Uri uri)
        {
            // Create Request
            var resource = uri.IsAbsoluteUri ? this.BaseUri.MakeRelativeUri(uri) : uri;
            var request = new RestSharp.RestRequest(resource, RestSharp.Method.GET);

            // Execute Request
            var restResponse = this.RestClient.Execute(request);

            // Return
            return restResponse.Content;
        }

        #endregion
    }
}
