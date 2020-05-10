using PostalCodeApi.Domain.Models;

namespace PostalCodeApi.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        /// <summary>
        ///     Creates a success response.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Response.</returns>
        public UserResponse(User user) : base(user)
        {
        }

        /// <summary>
        ///     Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="internalServerError">Define if the error is related to the server.</param>
        /// <returns>Response.</returns>
        public UserResponse(string message, bool internalServerError = true) : base(message, internalServerError)
        {
        }
    }
}