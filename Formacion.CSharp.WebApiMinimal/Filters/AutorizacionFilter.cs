
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;

namespace Formacion.CSharp.WebApiMinimal.Filters
{
    public class AutorizacionFilter : IEndpointFilter
    {
        private readonly IConfiguration _configuration;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            string clave = _configuration.GetValue<string>("Clave");
            context.HttpContext.Request.Headers.TryGetValue("APIKey", out var apikey);

            if (apikey == clave) return await next(context);
            else
            {
                context.HttpContext.Response.Headers.Clear();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;  // HTTP 401

                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Error = HttpStatusCode.Unauthorized.ToString(),
                    Message = "Unauthorized"
                });

                return await next(context);
            }
        }

        public AutorizacionFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
