namespace BookBook.API;

public class GlobalRoutePrefixMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _routePrefix;
    /// <summary>
    /// Middleware configure global route (path base)
    /// </summary>
    /// <param name="next"></param>
    /// <param name="routePrefix"></param>
    public GlobalRoutePrefixMiddleware(RequestDelegate next, string routePrefix)
    {
        _next = next;
        _routePrefix = routePrefix;
    }
    /// <summary>
    /// Configure HttpContext PathBase
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.PathBase = new PathString(_routePrefix);
        await _next(context);
    }
}
