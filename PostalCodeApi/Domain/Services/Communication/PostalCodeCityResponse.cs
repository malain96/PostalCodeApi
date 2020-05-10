using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class PostalCodeCityResponse : BaseResponse<PostalCodeCity>
    {
        /// <summary>
        ///     Creates a success response.
        /// </summary>
        /// <param name="postalCodeCity">Saved postal code city.</param>
        /// <returns>Response.</returns>
        public PostalCodeCityResponse(PostalCodeCity postalCodeCity) : base(postalCodeCity)
        {
        }

        /// <summary>
        ///     Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="internalServerError">Define if the error is related to the server.</param>
        /// <returns>Response.</returns>
        public PostalCodeCityResponse(string message, bool internalServerError = true) : base(message,
            internalServerError)
        {
        }
    }
}