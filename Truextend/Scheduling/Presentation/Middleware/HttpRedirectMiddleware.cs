using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Truextend.Scheduling.Presentation.Middleware
{
	public class HttpRedirectMiddleware
	{
        private readonly RequestDelegate _next;

        public HttpRedirectMiddleware(RequestDelegate next)
		{
			_next = next;
		}

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.ToLower() == "/api/login" ||
                context.Request.Path.Value.ToLower() == "/swagger" ||
                context.Request.Path.Value.ToLower() == "/swagger/" ||
                context.Request.Path.Value.ToLower() == "/swagger/index.html" ||
                context.Request.Path.Value.ToLower() == "/swagger/v1/swagger.json" ||
                context.Request.Path.Value.ToLower() == "/swagger/swagger-ui.css" ||
                context.Request.Path.Value.ToLower() == "/swagger/swagger-ui.css.map" ||
                context.Request.Path.Value.ToLower() == "/swagger/swagger-ui-bundle.js" ||
                context.Request.Path.Value.ToLower() == "/swagger/swagger-ui-bundle.js.map" ||
                context.Request.Path.Value.ToLower() == "/swagger/swagger-ui-standalone-preset.js.map" ||
                context.Request.Path.Value.ToLower() == "/swagger/swagger-ui-standalone-preset.js" ||
                context.Request.Path.Value.ToLower() == "/favicon.ico" ||
                context.Request.Path.Value.ToLower() == "/swagger/favicon-16x16.png" ||
                context.Request.Path.Value.ToLower() == "/swagger/favicon-32x32.png")
            {
                await _next.Invoke(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }

    public static class HttpRedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpRedirect(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpRedirectMiddleware>();
        }
    }
}

