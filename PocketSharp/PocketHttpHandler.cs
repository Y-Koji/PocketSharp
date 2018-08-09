using PocketSharp.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PocketSharp
{
    internal class PocketHttpHandler : HttpClientHandler
    {
        private static string AUTH { get; } = "https://getpocket.com/v3/oauth/request";

        private string Token { get; set; }
        private string ConsumerKey { get; set; }
        private string AccessToken { get; set; }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);

            return result;
        }
    }
}
