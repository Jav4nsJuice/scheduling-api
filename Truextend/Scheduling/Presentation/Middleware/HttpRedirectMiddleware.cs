using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Truextend.Scheduling.Presentation.Middleware
{
	public class HttpRedirectMiddleware
	{
        private readonly RequestDelegate _next;

        private readonly List<string> _allowedPaths = new()
        {
            "/api/login",
            "/swagger",
            "/swagger/",
            "/swagger/index.html",
            "/swagger/v1/swagger.json",
            "/swagger/swagger-ui.css",
            "/swagger/swagger-ui.css.map",
            "/swagger/swagger-ui-bundle.js",
            "/swagger/swagger-ui-bundle.js.map",
            "/swagger/swagger-ui-standalone-preset.js.map",
            "/swagger/swagger-ui-standalone-preset.js",
            "/favicon.ico",
            "/swagger/favicon-16x16.png",
            "/swagger/favicon-32x32.png"
        };

        public HttpRedirectMiddleware(RequestDelegate next)
		{
			_next = next;
		}

        public async Task Invoke(HttpContext context)
        {
            if (_allowedPaths.Contains(context.Request.Path.Value.ToLower()))
            {
                await _next.Invoke(context);
            }
            else
            {
                //  Future Token Middleware
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

