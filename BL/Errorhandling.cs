using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL
{
    public class Errorhandling
    {
        private readonly RequestDelegate next;
        public Errorhandling(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;


                message = exception.Message;
                status = HttpStatusCode.NotFound;


            var result = JsonConvert.SerializeObject(new { error = $"{exception.Message}" });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;


            return context.Response.WriteAsync(result);
        }


    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Errorhandling>();
        }
    }
}
