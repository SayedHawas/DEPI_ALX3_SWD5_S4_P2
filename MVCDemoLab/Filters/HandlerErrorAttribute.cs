using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCDemoLab.Filters
{
    public class HandlerErrorAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ViewResult result = new ViewResult();
            result.ViewName = "Error";
            context.Result = result;
        }
    }
}
