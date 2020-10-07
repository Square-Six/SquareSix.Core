using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SquareSix.Core.Exceptions;
using SquareSix.Core.Extensions;
using SquareSix.Core.Models;
using Xamarin.Essentials;

namespace SquareSix.Core.Services
{
    public class BaseRestService
    {
        private readonly HttpClient _client;

        protected virtual TimeSpan TimeoutAfter => TimeSpan.FromSeconds(15);

        public BaseRestService()
        {
            _client = new HttpClient
            {
                Timeout = TimeoutAfter
            };
        }

        public async Task<RestResponse<T>> RequestAsync<T>(HttpRequestMessage request, CancellationToken token)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new NetworkException();
            }

            return await _client.RequestAsync<T>(request, token);
        }
    }
}
