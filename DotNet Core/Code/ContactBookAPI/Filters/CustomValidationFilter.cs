using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactBookAPI.Filters
{
    public class CustomValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the route parameter "ID" exists in the ActionArguments
            if (context.ActionArguments.ContainsKey("ID"))
            {
                var id = context.ActionArguments["ID"];

                // Validate that ID is a positive integer
                if (id is int idValue && idValue <= 0)
                {
                    context.Result = new BadRequestObjectResult("ID must be greater than 0.");
                    return;
                }
            }

            // Optionally, validate the model state for body-based parameters (POST/PUT requests)
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Logic after the action executes (optional)
        }
    }
}
