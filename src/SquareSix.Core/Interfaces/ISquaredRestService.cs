using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SquareSix.Core.Interfaces
{
    public interface ISquaredRestService
    {
        Task<RestResponse<T>> RequestAsync<T>(HttpRequestMessage request, CancellationToken token);
    }
}
