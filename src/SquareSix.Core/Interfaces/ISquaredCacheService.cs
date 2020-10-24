using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public interface ISquaredCacheService
    {
        void Setup(string cacheName);
        Task InsertWithKeyAsync<T>(T item, string key);
        Task<T> GetFromKeyAsync<T>(string key);
        Task InvalidateAsync<T>(string key);
        Task InvalidateAllAsync();
        Task Flush();
        Task<IEnumerable<T>> GetAllByTypeAsync<T>();
    }
}
