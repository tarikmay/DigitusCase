using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using UserLoginApp.Business.Interfaces;
using UserLoginApp.Entities.Concrete;

namespace UserLoginApp.Business.Middlewares
{
    public class UserRequestTimeMiddleware
    {
        private readonly RequestDelegate _next;



        public UserRequestTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestTimeService requestTimeService)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();
            if (context.Response.StatusCode==200)
            {
                requestTimeService.Add(new RequestTime { RequestTimeMs = watch.ElapsedMilliseconds, RequestUrl = context.Request.Path.Value });
            }
        }
    }
}
