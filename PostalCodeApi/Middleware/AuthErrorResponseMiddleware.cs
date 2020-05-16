using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Middleware
{
    /// <summary>
    /// Middleware to handle authentication error responses
    /// </summary>
    public class AuthErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            
            await AuthErrorResponse(context);
        }

        /// <summary>
        /// Modify the response body for 401 and 403 errors
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>Task</returns>
        private async Task AuthErrorResponse(HttpContext context)
        {
            // Create an errorResource if the status code of the response is 401 or 404
            var errorResource = context.Response.StatusCode switch
            {
                StatusCodes.Status401Unauthorized => new ErrorResource(context.Response.StatusCode, "Bad Bearer token"),
                StatusCodes.Status403Forbidden => new ErrorResource(context.Response.StatusCode, "Role not allowed"),
                _ => null
            };
            
            // Stop here if the the response's status isn't 401 or 404
            if (errorResource == null)
                return;
            
            // Otherwise modify the response body with the errorResource
            context.Response.ContentType = "application/json";
            var jsonString = JsonConvert.SerializeObject(errorResource);
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }
    }
}