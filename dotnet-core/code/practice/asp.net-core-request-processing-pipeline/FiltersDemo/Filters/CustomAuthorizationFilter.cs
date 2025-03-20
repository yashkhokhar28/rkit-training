using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FiltersDemo.Filters
{
    /// <summary>
    /// CustomAuthorizationFilter class implements the IAuthorizationFilter interface to perform custom authorization logic.
    /// </summary>
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// This method is called early in the filter pipeline to confirm the request is authorized.
        /// </summary>
        /// <param name="context">The context for the authorization filter.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated
            bool isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;

            // If the user is not authenticated, set the result to Unauthorized
            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}