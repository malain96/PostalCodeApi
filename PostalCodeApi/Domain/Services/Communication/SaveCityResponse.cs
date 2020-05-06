using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class SaveCityResponse: BaseResponse<City>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="city">Saved city.</param>
        /// <returns>Response.</returns>
        public SaveCityResponse(City city) : base(city)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveCityResponse(string message) : base(message)
        { }
    }
}