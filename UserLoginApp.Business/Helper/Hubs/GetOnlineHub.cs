using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace UserLoginApp.Business.Helper.Hubs
{
    public class GetOnlineHub : Hub
    {
        private static int onlineCount;
        private readonly IMemoryCache _memoryCache;
        public GetOnlineHub(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            Console.WriteLine("asd");
        }

        public override async Task OnConnectedAsync()
        {
            onlineCount++;
            Console.WriteLine(onlineCount);
            _memoryCache.Set("GetOnlineCount", onlineCount, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });

             await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine(onlineCount);
            onlineCount--;

            _memoryCache.Set("GetOnlineCount", onlineCount, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });
            await  base.OnDisconnectedAsync(exception);
        }
    }
}
