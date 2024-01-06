using Microsoft.AspNetCore.Mvc.Filters;

namespace BookBook.Presentation;

public class ActionFilterExample : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
         // Do something before the action executes.
         Console.WriteLine(
            $"- {nameof(ActionFilterExample)}.{nameof(OnActionExecuting)}");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
        Console.WriteLine(
            $"- {nameof(ActionFilterExample)}.{nameof(OnActionExecuted)}");
    }


}
