using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Exceptions;

namespace Server.Middlewares
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == 404)
                {
                    await context.Response.WriteAsync("404 Page not found");
                }
            }
            catch (HttpStatusCodeException ex)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsync($"{ex.StatusCode} - {ex.Message}");
                return;
            }
            catch (UserDataException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("400 Server error - " + ex.Message);
            }
            catch (BusinessLogicException ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("500 Server error");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("500 Server error");
            }
        }
    }
}