using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Repositories
{
    /// <summary>
    /// Methods to query the User table
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Created user</returns>
        Task AddAsync(User user);
        
        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">User's data to update</param>
        void Update(User user);
        
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="user">User to delete</param>
        void Remove(User user);
        
        /// <summary>
        /// Find a user by username
        /// </summary>
        /// <param name="username">User's name</param>
        /// <returns>User or null</returns>
        Task<User> FindByUsernameAsync(string username);
        
        /// <summary>
        /// Find a user by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>User or null</returns>
        Task<User> FindByIdAsync(int id);
        
        /// <summary>
        /// Sorted page of users
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Number of user per page</param>
        /// <param name="sort">Sorting direction</param>
        /// <returns>PagedAndSortedList of User</returns>
        Task<PagedAndSortedList<User>> ListAllAsync(int pageNumber, int pageSize, string sort);
    }
}