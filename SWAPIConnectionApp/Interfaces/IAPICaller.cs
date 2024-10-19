using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.Interfaces
{
    public interface IAPICaller
    {
        IHttpClientFactory HttpClientFactory { get; }
        string APIGetResponse(string requestUri);
    }
}
