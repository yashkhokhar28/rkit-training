using StockPortfolioAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StockPortfolioAPI.Filters
{
    public class JWTAuthorizationFilter : AuthorizationFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public JWTAuthorizationFilter(params string[] roles)
        {
            _allowedRoles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the Authorization header is present
            if (actionContext.Request.Headers.Authorization == null ||
                string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Authorization token is missing.");
                return;
            }

            string token = actionContext.Request.Headers.Authorization.Parameter;

            try
            {
                // Validate the JWT token
                if (!JWTHelper.ValidateJWTToken(token, out string payload))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid or expired token.");
                    return;
                }

                // Deserialize the token payload
                var tokenData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(payload);
                if (tokenData == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token payload is malformed.");
                    return;
                }

                // Extract role from the token
                string userRole = tokenData.role?.ToString();
                if (string.IsNullOrEmpty(userRole))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Role information missing in token.");
                    return;
                }

                // Check if the user's role is allowed
                if (_allowedRoles != null && _allowedRoles.Length > 0 && !_allowedRoles.Contains(userRole, StringComparer.OrdinalIgnoreCase))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Access denied due to insufficient role.");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (use a logging framework like NLog, Serilog, or log4net for better error handling)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "An error occurred while validating the token.");
                // Optionally log the error: Log.Error(ex, "Token validation failed.");
            }
        }
    }
}