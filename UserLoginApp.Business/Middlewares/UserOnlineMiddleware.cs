using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using UserLoginApp.Business.Helper.Claims;
using UserLoginApp.Entities.Concrete;

namespace UserLoginApp.Business.Middlewares
{
    public class UserOnlineMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;

        public UserOnlineMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
            var _token = httpContext.Request.Headers.TryGetValue("Authorization", out var token);

            if (_token)
            {
                if (ClaimManager.GetClaim("UserId")!=null)
                {
                    _memoryCache.TryGetValue("Onlines",out List<OnlineUser> _onlines);
                    if (_onlines!=null)
                    {
                        if (_onlines.Find(x => x.Id == ClaimManager.GetClaim("UserId")) == null)
                        {
                            Console.WriteLine(_onlines);
                            var _newOnlineList = _onlines.FindAll(x => DateTime.Now.Subtract(DateTime.Parse(ClaimManager.GetClaim("LoginDate"))).Minutes<30).ToList();

                            _newOnlineList.Add(new OnlineUser
                            {
                                Id = ClaimManager.GetClaim("UserId"),
                                Username = ClaimManager.GetClaim("Username"),
                                LoginDate = ClaimManager.GetClaim("LoginDate")
                            });
                            _memoryCache.Remove("Onlines");
                            _memoryCache.Set("Onlines", _newOnlineList, new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                                Priority = CacheItemPriority.Normal
                            });
                        }
                    }
                    else
                    {
                        _memoryCache.Set("Onlines", new List<OnlineUser>
                        {
                            new OnlineUser
                            {
                                Id = ClaimManager.GetClaim("UserId"),
                                Username = ClaimManager.GetClaim("Username"),
                                LoginDate = ClaimManager.GetClaim("LoginDate")
                            }

                        }, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                            Priority = CacheItemPriority.Normal
                        });
                    }
                }
            }
            
        }

    }
}
