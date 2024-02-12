using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters.ActionFilters
{
    public class CheckModelStateFilter : IActionFilter
    {
        private readonly ILogger<CarsController> _carsLogger;

        public CheckModelStateFilter(ILogger<CarsController> carsLogger)
        {
            _carsLogger = carsLogger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.Controller is CarsController carsController)
            {
                if (!carsController.ModelState.IsValid)
                {
                    string errors = 
                        string.Join(" | ", carsController.ModelState.Values
                        .SelectMany(errors => errors.Errors)
                        .Select(err => err.ErrorMessage));

                    _carsLogger.LogError($"ModelState invalid\n{errors}");
                    context.Result = carsController.Problem(errors, statusCode: 400);
                }
            }
        }
    }
}
