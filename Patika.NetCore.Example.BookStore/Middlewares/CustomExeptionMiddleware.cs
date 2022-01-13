using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Patika.NetCore.Example.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Middlewares
{
    public class CustomExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExeptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;

        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP" + context.Request.Method + "-" + context.Request.Path;
                _loggerService.Write(message); // Loglama

                await _next(context);
                watch.Stop();

                message = "[Response] HTTP" + context.Request.Method + "-" + context.Request.Path + "responded" + context.Response.StatusCode + "in" + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message); // Loglama
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error] HTTP" + context.Request.Method + "-" + context.Response.StatusCode + "Error Message" + ex.Message + watch.Elapsed.TotalSeconds + "ms";
            _loggerService.Write(message);
            
            //Newtonsoft.json paketini yükle

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);

        }
    }
    public static class CustomExeptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExeptionMiddleware>();
        }
    }
}
