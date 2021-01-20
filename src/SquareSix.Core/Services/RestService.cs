using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace SquareSix.Core
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;

        protected virtual TimeSpan TimeoutAfter => TimeSpan.FromSeconds(15);

        public RestService()
        {
            _client = new HttpClient
            {
                Timeout = TimeoutAfter
            };
        }

        private async Task EnsureRequestHeaders(HttpRequestMessage msg)
        {
            if (SimpleIOC.Container.ContainsKey<IAuthorizationHeaderService>())
            {
                var headerService = SimpleIOC.Container.Resolve<IAuthorizationHeaderService>();
                var authHeaders = await headerService.GetAuthorizationHeaders();
                if (authHeaders?.Any() ?? false)
                {
                    foreach (var kvp in authHeaders)
                    {
                        if (string.IsNullOrEmpty(kvp.Key) || string.IsNullOrEmpty(kvp.Value))
                        {
                            continue;
                        }

                        msg.Headers.Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        public async Task<RestResponse<T>> PrepareAndSendRequest<T>(HttpMethod httpMethod, Uri uri, object data, CancellationToken cancellationToken, bool addAuthHeader = true, string contentType = "application/json")
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NetworkException();
            }

            using (var message = new HttpRequestMessage(httpMethod, uri))
            {
                if (data != null)
                {
                    var content = JsonConvert.SerializeObject(data);
                    message.Content = new StringContent(content, Encoding.UTF8, contentType);
                }

                if (addAuthHeader)
                {
                    await EnsureRequestHeaders(message);
                }

                return await _client.RequestAsync<T>(message, cancellationToken);
            }
        }
    }
}
