using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class CityResponse : BaseResponse<City>
    {
        /// <summary>
        ///     Creates a success response.
        /// </summary>
        /// <param name="city">Saved city.</param>
        /// <returns>Response.</returns>
        public CityResponse(City city) : base(city)
        {
        }

        /// <summary>
        ///     Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="internalServerError">Define if the error is related to the server.</param>
        /// <returns>Response.</returns>
        public CityResponse(string message, bool internalServerError = true) : base(message, internalServerError)
        {
        }
    }
}