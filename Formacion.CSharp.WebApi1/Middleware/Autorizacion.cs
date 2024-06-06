using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Threading.Tasks;

namespace Formacion.CSharp.WebApi1.Middleware
{
    public class Autorizacion
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                string clave = _configuration.GetValue<string>("Clave");
                
                StringValues apikey;
                httpContext.Request.Headers.TryGetValue("APIKey", out apikey);

                // httpContext.Request.Headers.TryGetValue("APIKey", out var apikey);

                if (apikey == clave) await _next(httpContext);
                else
                {
                    httpContext.Response.Headers.Clear();
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;  // HTTP 401

                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Error = HttpStatusCode.Unauthorized.ToString(),
                        Message = "Unauthorized"
                    });
                }

            }
            catch (Exception e)
            {
                httpContext.Response.Headers.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  // HTTP 500

                await httpContext.Response.WriteAsJsonAsync(new { 
                    Error = HttpStatusCode.InternalServerError.ToString(),
                    e.Message
                });
            }
        }

        public Autorizacion(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
    }

    public static class AutorizacionExtensions
    {
        public static IApplicationBuilder UseAutorizacion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Autorizacion>();
        }
    }
}
