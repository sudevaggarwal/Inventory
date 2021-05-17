using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InventaryManagement.Middleware
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate next;
        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            context.Response.OnStarting(state =>
            {
                var ctx = (HttpContext)state;
                if (ctx.Response.Headers.ContainsKey("Server"))
                {
                    ctx.Response.Headers.Remove("Server");
                }

                if (ctx.Response.Headers.ContainsKey("x-powered-by") || ctx.Response.Headers.ContainsKey("X-Powered-By"))
                {
                    ctx.Response.Headers.Remove("x-powered-by");
                    ctx.Response.Headers.Remove("X-Powered-By");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    ctx.Response.Headers.Add("X-Frame-Options", "Deny");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Xss-Protection"))
                {
                    ctx.Response.Headers.Add("X-Xss-Protection", "1;mode:block");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
                    ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                }

                if (!ctx.Response.Headers.ContainsKey("Refferrer-Policy"))
                {
                    ctx.Response.Headers.Add("Refferrer-Policy", "no-refferrer");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Permitted-Cross-Domain-Policies"))
                {
                    ctx.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                }

                if (!ctx.Response.Headers.ContainsKey("Feature-Policy"))
                {
                    ctx.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; megnetometer 'none'; microphone 'none'; payment 'none'; usb 'none'");
                }

                if (!ctx.Response.Headers.ContainsKey("Content-Security-Policy"))
                {
                    ctx.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
                }

                return Task.FromResult(0);
            }, context);


            await next(context);
        }
    }
}
