using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactBookAPI.Filters
{
    /// <summary>
    /// Custom validation filter to validate route parameters and model state.
    /// </summary>
    public class CustomValidationFilter : IActionFilter
    {
        /// <summary>
        /// Called before the action executes. Validates the route parameter "ID" and the model state.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the route parameter "ID" exists in the ActionArguments
            if (context.ActionArguments.ContainsKey("ID"))
            {
                var id = context.ActionArguments["ID"];

                // Validate that ID is a positive integer
                if (id is int idValue && idValue <= 0)
                {
                    // Return a bad request response if the ID is not valid
                    context.Result = new BadRequestObjectResult("ID must be greater than 0.");
                    return;
                }
            }
        }

        /// <summary>
        /// Called after the action executes. This method is optional and can be used to add logic after the action executes.
        /// </summary>
        /// <param name="context">The action executed context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Logic after the action executes
        }
    }
}