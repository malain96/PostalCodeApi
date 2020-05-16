using Microsoft.AspNetCore.Http;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        /// <summary>
        /// Creates an success response
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="statusCode">Success status code</param>
        /// <returns>Response</returns>
        public UserResponse(User user, int statusCode = StatusCodes.Status200OK) : base(
            user, statusCode)
        {
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Error status code</param>
        /// <returns>Response</returns>
        public UserResponse(string message, int statusCode = StatusCodes.Status500InternalServerError) : base(
            message,
            statusCode)
        {
        }
    }
}