using Microsoft.Extensions.Logging;
using SWAPIConnectionApp.FileAccess;
using SWAPIConnectionApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.API
{
    abstract public class APICaller : IAPICaller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IHttpClientFactory HttpClientFactory
        {
            get
            {
                return _httpClientFactory; 
            }
        }

        public APICaller (IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _httpClientFactory = httpClientFactory;
        }

        public string APIGetResponse(string requestUri)
        {
            return APIGetResponseAsync(requestUri).Result;
        }

        private Task<string> APIGetResponseAsync(string requestUri)
        {
            var httpClient = _httpClientFactory.CreateClient();

            return httpClient.GetStringAsync(requestUri);
        }
    }
}
