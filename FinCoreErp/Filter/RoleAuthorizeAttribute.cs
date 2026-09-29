using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinCoreErp.Filter
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] allowedRoles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            allowedRoles = roles;
        }

        public override void OnActionExecuting(
            ActionExecutingContext context)
        {
            var roleName = context.HttpContext.Session
                .GetString("RoleName");

            if (string.IsNullOrEmpty(roleName))
            {
                context.Result = new RedirectToActionResult("Login","Auth",null);

                return;
            }

            if (!allowedRoles.Contains(roleName))
            {
                context.Result = new RedirectToActionResult("AccessDenied","Auth",null);

                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
