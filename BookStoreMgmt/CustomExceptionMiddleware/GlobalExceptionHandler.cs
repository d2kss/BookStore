using BookStoreMgmt.Models;
using LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BookStoreMgmt.CustomExceptionMiddleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILoggerManager _loggerManager;
        public GlobalExceptionHandler(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var message = ex switch
            {
                Exception => "An general exception has been encountered from custom middleware",
                _ => "internal server error occured from custom middleware"
            };
            message = ex switch
            {
                AccessViolationException => "An Access Violation Exception has been encountered from custom middleware",
                _ => "internal server error occured from custom middleware"
            };
            await context.Response.WriteAsync(new ErrorDetails
            {
                statuccode = context.Response.StatusCode,
                message = message
            }.ToString());
            return true;
        }
    }
}
