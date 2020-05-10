using System.Threading.Tasks;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;

namespace PostalCodeApi.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        void Update(User user);
        void Remove(User user);
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByIdAsync(int id);
        Task<PagedAndSortedList<User>> ListAllAsync(int pageNumber, int pageSize, string sort);
    }
}