using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Extensions;
using PostalCodeApi.Persistence.Contexts;

namespace PostalCodeApi.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(PostalCodeDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedAndSortedList<User>> ListAllAsync(int pageNumber, int pageSize, string sort)
        {
            IQueryable<User> users = _context.Users;
            users = sort.ToLowerInvariant() == "desc"
                ? users.OrderByDescending(user => user.Username)
                : users.OrderBy(user => user.Username);
            return await PagedAndSortedList<User>.ToPagedListAsync(users, pageNumber, pageSize,
                sort.ToLowerInvariant(), "username");
        }
    }
}