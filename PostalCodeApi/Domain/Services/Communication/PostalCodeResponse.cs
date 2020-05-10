using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class PostalCodeResponse : BaseResponse<PostalCode>
    {
        /// <summary>
        ///     Creates a success response.
        /// </summary>
        /// <param name="postalCode">Saved postal code.</param>
        /// <returns>Response.</returns>
        public PostalCodeResponse(PostalCode postalCode) : base(postalCode)
        {
        }

        /// <summary>
        ///     Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="internalServerError">Define if the error is related to the server.</param>
        /// <returns>Response.</returns>
        public PostalCodeResponse(string message, bool internalServerError = true) : base(message, internalServerError)
        {
        }
    }
}