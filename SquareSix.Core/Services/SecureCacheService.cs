using System;
using System.Threading.Tasks;

namespace SquareSix.Core.Services
{
    public interface ISecureCacheService
    {
        Task<T> GetFromKeyAsync<T>(string key);
    }

    public class SecureCacheService : ISecureCacheService
    {
        public SecureCacheService()
        {
        }

        public Task<T> GetFromKeyAsync<T>(string key)
        {
            return default;
        }
    }
}
