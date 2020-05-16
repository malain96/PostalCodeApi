﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PostalCodeApi.Middleware
{
    /// <summary>
    /// Log API calls and responses
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            LogRequest(context);

            await _next(context);
            
            LogResponse(context);
        }

        /// <summary>
        /// Log a call to an API route
        /// </summary>
        /// <param name="context">The request</param>
        /// <returns>Task</returns>
        private void LogRequest(HttpContext context)
        {
            // Get the user's name
            var username = "Anonymous user";
            if (context.User.Identity is ClaimsIdentity identity)
            {
                var claim = identity.FindFirst(ClaimTypes.Name);
                if (claim != null)
                    username = claim.Value;
            }

            // Create the log string
            var logString = $"Called {context.Request.Method} {context.Request.Path} by {username}";

            // Log the call
            _logger.LogInformation(logString);
        }

        /// <summary>
        /// Log the response to an API route
        /// </summary>
        /// <param name="context">The request</param>
        private void LogResponse(HttpContext context)
        {
            // Get the user's name
            var username = "Anonymous user";
            if (context.User.Identity is ClaimsIdentity identity)
            {
                var claim = identity.FindFirst(ClaimTypes.Name);
                if (claim != null)
                    username = claim.Value;
            }

            // Create the log string
            var logString =
                $"Response Status {context.Response.StatusCode} for {context.Request.Method} {context.Request.Path} by {username}";

            // Log the response
            _logger.LogInformation(logString);
        }
    }
}