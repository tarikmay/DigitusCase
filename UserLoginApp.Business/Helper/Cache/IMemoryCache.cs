using Microsoft.Extensions.Caching.Memory;

namespace UserLoginApp.Business.Helper.Cache
{
    public interface IMemoryCache : IDisposable
    {
        ICacheEntry CreateEntry(object key);
        void Remove(object key);
        bool TryGetValue(object key, out object value);
    }
}
