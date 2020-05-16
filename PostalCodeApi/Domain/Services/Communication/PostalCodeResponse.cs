using Microsoft.AspNetCore.Http;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class PostalCodeResponse : BaseResponse<PostalCode>
    {
        /// <summary>
        ///     Creates a success response.
        /// </summary>
        /// <param name="postalCode">Saved postal code.</param>
        /// /
        /// <param name="statusCode">Success status code</param>
        /// <returns>Response.</returns>
        public PostalCodeResponse(PostalCode postalCode, int statusCode = StatusCodes.Status200OK) : base(
            postalCode, statusCode)
        {
        }

        /// <summary>
        ///     Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="statusCode">Error status code.</param>
        /// <returns>Response.</returns>
        public PostalCodeResponse(string message, int statusCode = StatusCodes.Status500InternalServerError) : base(
            message,
            statusCode)
        {
        }
    }
}