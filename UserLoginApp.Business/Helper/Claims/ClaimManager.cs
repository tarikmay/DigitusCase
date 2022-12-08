using Microsoft.AspNetCore.Http;


namespace UserLoginApp.Business.Helper.Claims
{
    public static class ClaimManager
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void HttpContextAccessorConfig(IHttpContextAccessor context)
        {
            _httpContextAccessor = context;
        }
        public static string GetClaim(string claim)
        {

            var claims = _httpContextAccessor.HttpContext.User.Claims;
            return claims?.FirstOrDefault(x => x.Type.Equals(claim, StringComparison.OrdinalIgnoreCase))?.Value;
        }

    }
}
