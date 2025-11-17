using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCDemoLab.Filters
{
    public class MyFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
            // Executed before execution of an action method
            //throw new NotImplementedException();
            // Get the Controller name
            var controllerName = context.RouteData.Values["controller"].ToString();

            // Get the Action name
            var actionName = context.RouteData.Values["action"].ToString();
            // Log or use the controller and action names
            Console.WriteLine($"CustomActionFilter: OnActionExecuting - Controller: {controllerName}, Action: {actionName}");

            // You can pass the controller and action names to the ViewBag or HttpContext for use in views
            context.HttpContext.Items["ControllerName"] = controllerName;
            context.HttpContext.Items["ActionName"] = actionName;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("OnActionExecuting");
            // Get the Controller name
            var controllerName = context.RouteData.Values["controller"].ToString();

            // Get the Action name
            var actionName = context.RouteData.Values["action"].ToString();
            // Log or use the controller and action names
            Console.WriteLine($"CustomActionFilter: OnActionExecuting - Controller: {controllerName}, Action: {actionName}");

            // You can pass the controller and action names to the ViewBag or HttpContext for use in views
            context.HttpContext.Items["ControllerName"] = controllerName;
            context.HttpContext.Items["ActionName"] = actionName;
        }
    }
}
