using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DemoForms.Interfaces;
using DemoForms.Models;
using SquareSix.Core;
using SquareSix.Core.Interfaces;

namespace DemoForms.Services
{
    public class MockApiService : IMockApiService
    {
        private readonly ISquaredRestService _restService;

        public MockApiService()
        {
            _restService = SimpleIOC.Container.Resolve<ISquaredRestService>();
        }

        public async Task<List<CommentModel>> GetCommentModelsAsync()
        {
            try
            {
                var url = "https://5f7cf91d834b5c0016b05b2f.mockapi.io/comments";

                using (var message = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    var response = await _restService.RequestAsync<List<CommentModel>>(message, CancellationToken.None);
                    return response?.Data ?? new List<CommentModel>();
                }
            }
            catch (Exception)
            {
                return new List<CommentModel>();
            }
        }
    }
}
