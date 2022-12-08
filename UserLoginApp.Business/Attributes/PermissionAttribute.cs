using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using UserLoginApp.Entities.Concrete.Reponse;

namespace UserLoginApp.Business.Attributes
{
    public class PermissionAttr : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _reqRole;
        public PermissionAttr(params string[] reqRole)
        {
            _reqRole = reqRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _userRole = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (_reqRole.Contains(_userRole))
            {
                return;
            }
            context.HttpContext.Response.StatusCode = 403;
            context.Result = new JsonResult(new PermissionErrorResponse{ Message = "Yetkisiz İstek", StatusCode = 403 });
        }
    }
}
