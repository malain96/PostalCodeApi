using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class SavePostalCodeCityResponse: BaseResponse<PostalCodeCity>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="postalCodeCity">Saved postal code city.</param>
        /// <returns>Response.</returns>
        public SavePostalCodeCityResponse(PostalCodeCity postalCodeCity) : base(postalCodeCity)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SavePostalCodeCityResponse(string message) : base(message)
        { }
    }
}