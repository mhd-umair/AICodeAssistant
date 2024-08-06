using Microsoft.AspNetCore.Antiforgery;

namespace AICodeAssistant.Middleware
{
    public class AntiforgeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;

        public AntiforgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                var tokens = _antiforgery.GetAndStoreTokens(context);
                context.Response.Headers.Add("X-CSRF-TOKEN", tokens.RequestToken);
            }

            await _next(context);
        }
    }

    // Extension method to add middleware
    public static class AntiforgeryTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAntiforgeryTokens(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiforgeryTokenMiddleware>();
        }
    }

}
