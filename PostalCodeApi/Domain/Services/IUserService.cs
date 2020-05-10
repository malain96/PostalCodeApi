using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services.Communication;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Services
{
    public interface IUserService
    {
        Task<PagedAndSortedList<User>> GetAll(int pageNumber, int pageSize, string sort);
        User GetById(int id);

        Task<UserResponse> UpdatePasswordAsync(int id, string oldPassword, string newPassword);

        Task<UserResponse> UpdateIsAdminAsync(int id, bool isAdmin);
        Task<UserResponse> SaveAsync(User user, string password);
        Task<UserResponse> DeleteAsync(int id);
    }
}