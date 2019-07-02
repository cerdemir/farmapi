using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace farmapi.Filters
{
    public class ModelValidateActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var ret = new Models.ApiResponseModel<ModelStateDictionary>()
                {
                    Success = false,
                    UserExceptionMessage = "Provided data not valid",
                    ExceptionDetails = "Validation error",
                    Data = context.ModelState
                };

                context.Result = new JsonResult(ret)
                {
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //nothing
        }
    }
}