using Microsoft.AspNetCore.Http;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class PostalCodeCityResponse : BaseResponse<PostalCodeCity>
    {
        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="postalCodeCity">Saved postal code city</param>
        /// <param name="statusCode">Success status code</param>
        /// <returns>Response</returns>
        public PostalCodeCityResponse(PostalCodeCity postalCodeCity, int statusCode = StatusCodes.Status200OK) : base(
            postalCodeCity, statusCode)
        {
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Error status code</param>
        /// <returns>Response</returns>
        public PostalCodeCityResponse(string message, int statusCode = StatusCodes.Status500InternalServerError) : base(
            message,
            statusCode)
        {
        }
    }
}