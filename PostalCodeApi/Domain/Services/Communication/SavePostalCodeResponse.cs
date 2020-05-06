using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class SavePostalCodeResponse: BaseResponse<PostalCode>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="postalCode">Saved postal code.</param>
        /// <returns>Response.</returns>
        public SavePostalCodeResponse(PostalCode postalCode) : base(postalCode)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SavePostalCodeResponse(string message) : base(message)
        { }
    }
}