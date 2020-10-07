using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;

namespace SquareSix.Core.Services
{
    public interface ISecureCacheService
    {
        void Setup(string cacheName);
        Task InsertWithKeyAsync<T>(T item, string key);
        Task<T> GetFromKeyAsync<T>(string key);
        Task InvalidateAsync<T>(string key);
        Task InvalidateAllAsync();
        Task Flush();
        Task<IEnumerable<T>> GetAllByTypeAsync<T>();
    }

    public class SecureCacheService : ISecureCacheService
    {
        public void Setup(string cacheName)
        {
            Registrations.Start(cacheName);
            BlobCache.ApplicationName = cacheName;
            BlobCache.Secure.Vacuum();
        }

        public async Task<IEnumerable<T>> GetAllByTypeAsync<T>()
        {
            try
            {
                var results = await BlobCache.Secure.GetAllObjects<T>();
                return results;
            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task InsertWithKeyAsync<T>(T item, string key)
        {
            await BlobCache.Secure.InsertObject(key, item);
        }

        public async Task<T> GetFromKeyAsync<T>(string key)
        {
            try
            {
                return await BlobCache.Secure.GetObject<T>(key);
            }
            catch
            {
                return default;
            }
        }

        public async Task InvalidateAsync<T>(string key)
        {
            await BlobCache.Secure.InvalidateObject<T>(key);
        }

        public async Task InvalidateAllAsync()
        {
            await BlobCache.Secure.InvalidateAll();
        }

        public async Task Flush()
        {
            await BlobCache.Secure.Flush();
        }
    }
}
