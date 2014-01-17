using System;
using System.Collections.Generic;
using Tavis.UriTemplates;
using System.Linq;

namespace Illallangi.RestClient
{
    public static class StringUriExtensions
    {
        public static Uri TemplateWith(this string uri, params IEnumerable<KeyValuePair<string, string>>[] parameterArgs)
        {
            var template = new UriTemplate(uri);

            foreach (var parameter in parameterArgs.Where(parameters => null != parameters)
                                                   .SelectMany(parameters => parameters.Where(parameter => null != parameter.Key && null != parameter.Value)))
            {
                template.SetParameter(parameter.Key, parameter.Value);
            }

            var uriString = template.Resolve();
            return new Uri(uriString, UriKind.Relative);
        }
    }
}