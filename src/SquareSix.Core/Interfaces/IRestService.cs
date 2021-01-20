using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public interface IRestService
    {
        Task<RestResponse<T>> PrepareAndSendRequest<T>(HttpMethod httpMethod, Uri uri, object data, CancellationToken cancellationToken, bool addAuthHeader = true, string contentType = "application/json");
    }
}
