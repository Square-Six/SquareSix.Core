using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FormsSample.Interfaces;
using FormsSample.Models;
using SquareSix.Core.Services;

namespace FormsSample.Services
{
    public class ComentsService : BaseRestService, IComentsService
    {
        protected override TimeSpan TimeoutAfter => TimeSpan.FromSeconds(3);

        public ComentsService()
        {
        }

        public async Task<List<Comment>> GetCommentAsync()
        {
            var uriBuilder = new UriBuilder("https://5f7cf91d834b5c0016b05b2f.mockapi.io/comments");

            using (var message = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri))
            {
                message.Headers.Add("AuthToken", "2342342362465324523452345");

                var response = await RequestAsync<List<Comment>>(message, CancellationToken.None);
                return response?.Data ?? new List<Comment>();
            }
        }
    }
}
