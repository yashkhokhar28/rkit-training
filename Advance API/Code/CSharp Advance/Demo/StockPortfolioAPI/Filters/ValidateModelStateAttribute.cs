using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StockPortfolioAPI.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Check if the model state is valid
            if (!actionContext.ModelState.IsValid)
            {
                var errorMessages = actionContext.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Return a bad request response with the validation errors
                var response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    IsError = true,
                    Message = "Invalid input data.",
                    Data = errorMessages
                });

                actionContext.Response = response;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
