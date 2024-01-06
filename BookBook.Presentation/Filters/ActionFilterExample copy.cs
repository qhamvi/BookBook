﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace BookBook.Presentation;

public class GlobalActionFilterExample : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
         // Do something before the action executes.
         Console.WriteLine(
            $"- {nameof(GlobalActionFilterExample)}.{nameof(OnActionExecuting)}");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do something after the action executes.
        Console.WriteLine(
            $"- {nameof(GlobalActionFilterExample)}.{nameof(OnActionExecuted)}");
    }


}
