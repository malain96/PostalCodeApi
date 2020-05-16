using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get a paged and sorted list of user
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Number of postal code by page</param>
        /// <param name="sort">Sorting direction</param>
        /// <returns>PagedAndSortedList of User</returns>
        Task<PagedAndSortedList<User>> GetAllAsync(int pageNumber, int pageSize, string sort);
        
        /// <summary>
        /// Get a user with the given id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>User</returns>
        Task<User> GetByIdAsync(int id);
        
        /// <summary>
        /// Update the password of a user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="oldPassword">User's old password</param>
        /// <param name="newPassword">User's new password</param>
        /// <returns>User response object</returns>
        Task<UserResponse> UpdatePasswordAsync(int id, string oldPassword, string newPassword);
        
        /// <summary>
        /// Update the role of a user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="role">User's role</param>
        /// <returns>User response object</returns>
        Task<UserResponse> UpdateRoleAsync(int id, string role);
        
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">User's data</param>
        /// <param name="password">User's password</param>
        /// <returns>User response object</returns>
        Task<UserResponse> SaveAsync(User user, string password);
        
        /// <summary>
        /// Delete a given user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>User response object</returns>
        Task<UserResponse> DeleteAsync(int id);
        
        /// <summary>
        /// Authenticate a user 
        /// </summary>
        /// <param name="username">User's name</param>
        /// <param name="password">User's password</param>
        /// <returns>User response object</returns>
        Task<UserResponse> AuthenticateAsync(string username, string password);
    }
}