using Microsoft.AspNetCore.Http;
using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class CityResponse : BaseResponse<City>
    {
        /// <summary>
        ///  Creates a success response
        /// </summary>
        /// <param name="city">Saved city</param>
        /// <param name="statusCode">Success status code</param>
        /// <returns>Response</returns>
        public CityResponse(City city, int statusCode = StatusCodes.Status200OK) : base(city, statusCode)
        {
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Error status code</param>
        /// <returns>Response</returns>
        public CityResponse(string message, int statusCode = StatusCodes.Status500InternalServerError) : base(message,
            statusCode)
        {
        }
    }
}